using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TatterFitness.App.EventArguments;
using TatterFitness.App.Interfaces.Services;
using TatterFitness.App.Interfaces.Services.API;
using TatterFitness.App.NavData;
using TatterFitness.App.NavData.Select;

namespace TatterFitness.App.ViewModels.Select
{
    public partial class SelectExercisesViewModel : ViewModelBase, IQueryAttributable
    {
        private IEnumerable<int> originalExerciseIds = new List<int>();
        private IEnumerable<int> requiredExerciseIds = new List<int>();
        private readonly IExercisesApiService exerciseApiSvc;
        private Boolean allowOnlyOneSelection = false;
        private List<SelectExercisesCardViewModel> allCardVms = new();

        [ObservableProperty]
        private ObservableCollection<SelectExercisesCardViewModel> cardVms = new();

        [ObservableProperty]
        private bool doShowUpdateButton = true;

        [ObservableProperty]
        private string exerciseFilter = String.Empty;

        public event EventHandler<SelectExercisesModalClosedEventArgs> Closed;


        public SelectExercisesViewModel(ILoggingService logger, IExercisesApiService exercisesApiSvc)
            : base(logger)
        {
            this.exerciseApiSvc = exercisesApiSvc;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var navData = NavDataBase.FromNavDataDictionary<SelectExercisesNavData>(query);
            originalExerciseIds = navData.CurrentExerciseIds;
            requiredExerciseIds = navData.RequiredExerciseIds;
            allowOnlyOneSelection = navData.AllowOnlyOneSelection;
            DoShowUpdateButton = !allowOnlyOneSelection;
        }

        protected async override Task PerformLoadViewData()
        {
            var allExercises = await exerciseApiSvc.ReadAll();
            foreach (var exercise in allExercises)
            {
                allCardVms.Add(new SelectExercisesCardViewModel(logger, exercise)
                {
                    IsSelected = originalExerciseIds.Contains(exercise.Id),
                    IsRequired = requiredExerciseIds.Contains(exercise.Id)
                });
            }

            CardVms.Clear();
            foreach (var cardVm in allCardVms)
            {
                CardVms.Add(cardVm);
            }
        }

        [RelayCommand]
        private async Task ApplyExerciseFilter()
        {
            try
            {
                IEnumerable<SelectExercisesCardViewModel> foundCardVms;
                if (string.IsNullOrEmpty(ExerciseFilter))
                {
                    foundCardVms = allCardVms;
                }
                else
                {
                    foundCardVms = allCardVms.Where(cardVm => cardVm.Name.Contains(ExerciseFilter, StringComparison.OrdinalIgnoreCase));
                }

                CardVms.Clear();
                foreach (var cardVm in foundCardVms)
                {
                    CardVms.Add(cardVm);
                }
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        [RelayCommand]
        private async Task SelectExercise(SelectExercisesCardViewModel cardVm)
        {
            try
            {
                if(requiredExerciseIds.Contains(cardVm.Exercise.Id))
                {
                    return;
                }

                cardVm.IsSelected = !cardVm.IsSelected;

                if (cardVm.IsSelected && allowOnlyOneSelection)
                {
                    await CompleteAndClose();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        [RelayCommand]
        private async Task Cancel()
        {
            try
            {
                await Shell.Current.GoToAsync("..");
                Closed?.Invoke(this, new SelectExercisesModalClosedEventArgs());
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        [RelayCommand]
        private async Task Update()
        {
            try
            {
                await CompleteAndClose();
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        private async Task CompleteAndClose()
        {
            var selectedExerciseIds = CardVms
                .Where(m => m.IsSelected)
                .Select(m => m.Exercise.Id);

            var exerciseIdsToCreate = selectedExerciseIds.Except(originalExerciseIds);
            var exerciseIdsToRemove = originalExerciseIds.Except(selectedExerciseIds);

            var exercisesToCreate = cardVms
                .Where(vm => exerciseIdsToCreate.Contains(vm.Exercise.Id))
                .Select(vm => vm.Exercise);

            var exercisesToRemove = cardVms
                .Where(vm => exerciseIdsToRemove.Contains(vm.Exercise.Id))
                .Select(vm => vm.Exercise);

            await Shell.Current.GoToAsync("..");

            Closed?.Invoke(this, new SelectExercisesModalClosedEventArgs(exercisesToCreate, exercisesToRemove));
        }
    }
}
