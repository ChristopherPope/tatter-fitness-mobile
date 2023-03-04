using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TatterFitness.Mobile.Controls.Popups;
using TatterFitness.Mobile.Interfaces.Services;
using TatterFitness.Mobile.Interfaces.Services.API;
using TatterFitness.Mobile.Interfaces.Services.ContextMenu;
using TatterFitness.Mobile.Interfaces.Services.SelectorModals;
using TatterFitness.Mobile.Models.Popups;
using TatterFitness.Mobile.NavData;
using TatterFitness.Mobile.Views.History;
using TatterFitness.Mobile.Views.Workouts;
using TatterFitness.Models;
using TatterFitness.Models.Exercises;

namespace TatterFitness.Mobile.ViewModels.Routines
{
    public partial class EditRoutineViewModel : ViewModelBase, IQueryAttributable
    {
        private readonly IRoutinesApiService routinesSvc;
        private readonly IRoutineExercisesApiService routineExSvc;
        private readonly IExercisesSelectorModal exercisesSelectorModal;
        private readonly IRoutineExerciseContextMenuService contextMenu;
        private int routineId;

        [ObservableProperty]
        private ObservableCollection<EditRoutineCardViewModel> cardVms = new();

        public EditRoutineViewModel(ILoggingService logger, IRoutinesApiService routinesSvc, IRoutineExercisesApiService routineExSvc, IExercisesSelectorModal exercisesSelectorModal, IRoutineExerciseContextMenuService contextMenu)
            : base(logger)
        {
            this.routinesSvc = routinesSvc;
            this.exercisesSelectorModal = exercisesSelectorModal;
            this.routineExSvc = routineExSvc;
            this.contextMenu = contextMenu;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var navData = NavDataBase.FromNavDataDictionary<EditRouitineNavData>(query);

            Title = navData.Routine.Name;
            routineId = navData.Routine.Id;
        }

        protected override async Task PerformLoadViewData()
        {
            CardVms.Clear();
            var exercises = await routinesSvc.ReadExercises(routineId);
            foreach (var exercise in exercises)
            {
                CardVms.Add(new EditRoutineCardViewModel(logger, exercise));
            }
        }

        [RelayCommand]
        private async Task RenameRoutine()
        {
            try
            {
                var metadata = new ValueEntryPopupMetadata
                {
                    Title = "Routine Name",
                    Value = Title
                };
                var popup = new ValueEntryPopup(metadata);
                var routineName = await Shell.Current.ShowPopupAsync(popup) as string;
                if (routineName == null)
                {
                    return;
                }

                var routine = new Routine { Id = routineId, Name = routineName };
                await routinesSvc.Update(routine);
                Title = routineName;
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        [RelayCommand]
        private async Task WorkoutRoutine()
        {
            try
            {
                var navData = new WorkoutNavData(routineId);
                await Shell.Current.GoToAsync(nameof(WorkoutView), true, navData.ToNavDataDictionary());
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        [RelayCommand]
        private async Task DeleteRoutine()
        {
            try
            {
                var metadata = new DecisionPopupMetadata { Title = "Delete Routine", Prompt = $"Are you sure you wish to delete the {Title} routine?" };
                var popup = new DecisionPopup(metadata);
                var choice = await Shell.Current.CurrentPage.ShowPopupAsync(popup);
                if (!Convert.ToBoolean(choice))
                {
                    return;
                }

                await routinesSvc.Delete(routineId);
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        [RelayCommand]
        private async Task EditExercises()
        {
            try
            {
                await exercisesSelectorModal.ShowModal(CardVms.Select(vm => vm.RoutineExercise.ExerciseId), ExercisesModalClosed);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        private async Task ExercisesModalClosed(IEnumerable<Exercise> exercisesToAdd, IEnumerable<Exercise> exercisesToRemove)
        {
            try
            {
                if (exercisesToRemove.Any())
                {
                    await RemoveExercises(exercisesToRemove.Select(e => e.Id));
                }

                if (exercisesToAdd.Any())
                {
                    await AddExercises(exercisesToAdd);
                }
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        private async Task AddExercises(IEnumerable<Exercise> exercises)
        {
            var routineExercises = await routineExSvc.Create(routineId, exercises.Select(e => e.Id));
            foreach (var routineExercise in routineExercises)
            {
                var cardVm = new EditRoutineCardViewModel(logger, routineExercise);
                for (var i = 0; i < CardVms.Count(); i++)
                {
                    if (routineExercise.ExerciseName.CompareTo(CardVms[i].Name) == -1)
                    {
                        CardVms.Insert(i, cardVm);
                        break;
                    }
                }
            }

        }

        private async Task RemoveExercises(IEnumerable<int> exerciseIds)
        {
            await routineExSvc.Delete(routineId, exerciseIds);
            foreach (var exerciseId in exerciseIds)
            {
                var cardVm = CardVms.First(vm => vm.RoutineExercise.ExerciseId == exerciseId);
                CardVms.Remove(cardVm);
            }

        }

        [RelayCommand]
        private async Task ShowExerciseContextMenu(EditRoutineCardViewModel cardVm)
        {
            try
            {
                var selection = await contextMenu.Show(cardVm);
                switch (selection)
                {
                    case IRoutineExerciseContextMenuService.RemoveExerciseMenuId:
                        await RemoveExercises(Enumerable.Repeat(cardVm.RoutineExercise.ExerciseId, 1));
                        break;

                    case IRoutineExerciseContextMenuService.ShowExerciseHistoryMenuId:
                        await ShowExerciseHistory(cardVm);
                        break;
                }
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        private async Task ShowExerciseHistory(EditRoutineCardViewModel cardVm)
        {
            var navData = new ExerciseHistoryNavData(cardVm.RoutineExercise.ExerciseId);
            await Shell.Current.GoToAsync(nameof(ExerciseHistoryView), true, navData.ToNavDataDictionary());
        }
    }
}
