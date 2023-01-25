using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TatterFitness.App.Controls.Popups;
using TatterFitness.App.Interfaces.Services;
using TatterFitness.App.Interfaces.Services.API;
using TatterFitness.App.Interfaces.Services.ContextMenu;
using TatterFitness.App.Models.Popups;
using TatterFitness.App.NavData;
using TatterFitness.App.Views.Routines;
using TatterFitness.App.Views.Workouts;
using TatterFitness.Models;

namespace TatterFitness.App.ViewModels.Routines
{
    public partial class ShowRoutinesViewModel : ViewModelBase
    {
        private readonly IRoutinesApiService routinesSvc;
        private readonly IRoutineContextMenuService contextMenu;

        [ObservableProperty]
        private ObservableCollection<ShowRoutinesCardViewModel> cardVms = new();

        public ShowRoutinesViewModel(ILoggingService logger, IRoutinesApiService routinesSvc, IRoutineContextMenuService contextMenu)
            : base(logger)
        {
            this.routinesSvc = routinesSvc;
            this.contextMenu = contextMenu;
        }

        protected override async Task PerformLoadViewData()
        {
            await RefreshView();

            Title = "Routines";
        }

        public override async Task RefreshView()
        {
            CardVms.Clear();
            var routines = await routinesSvc.ReadAll();
            foreach (var routine in routines)
            {
                var vm = new ShowRoutinesCardViewModel(logger, routine);
                CardVms.Add(vm);
                await vm.LoadViewData();
            }
        }

        [RelayCommand]
        private async Task EditRoutine(ShowRoutinesCardViewModel routineVm)
        {
            try
            {
                var navData = new EditRouitineNavData(routineVm.Routine);
                await Shell.Current.GoToAsync(nameof(EditRoutineView), true, navData.ToNavDataDictionary());
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        [RelayCommand]
        private async Task CreateRoutine()
        {
            try
            {
                var metadata = new ValueEntryPopupMetadata
                {
                    Title = "Routine Name"
                };
                var popup = new ValueEntryPopup(metadata);
                var routineName = await Shell.Current.ShowPopupAsync(popup) as string;
                if (routineName == null)
                {
                    return;
                }

                var newRoutine = new Routine { Name = routineName };
                newRoutine = await routinesSvc.Create(newRoutine);

                var vm = new ShowRoutinesCardViewModel(logger, newRoutine);
                CardVms.Add(vm);

                var navData = new EditRouitineNavData(newRoutine);
                await Shell.Current.GoToAsync(nameof(EditRoutineView), true, navData.ToNavDataDictionary());
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        [RelayCommand]
        private async Task ShowRoutineContextMenu(ShowRoutinesCardViewModel routineVm)
        {
            try
            {
                var selection = await contextMenu.Show(routineVm);
                switch (selection)
                {
                    case IRoutineContextMenuService.DeleteRoutineMenuId:
                        await DeleteRoutine(routineVm);
                        break;

                    case IRoutineContextMenuService.RenameRoutineMenuId:
                        await RenameRoutine(routineVm);
                        break;

                    case IRoutineContextMenuService.WorkoutRoutineMenuId:
                        await WorkoutRoutine(routineVm);
                        break;
                }
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        private async Task WorkoutRoutine(ShowRoutinesCardViewModel routineVm)
        {
            var navData = new WorkoutNavData(routineVm.Routine.Id);
            await Shell.Current.GoToAsync(nameof(WorkoutView), true, navData.ToNavDataDictionary());
        }

        private async Task RenameRoutine(ShowRoutinesCardViewModel routineVm)
        {
            var metadata = new ValueEntryPopupMetadata
            {
                Title = "Routine Name",
                Value = routineVm.RoutineName
            };
            var popup = new ValueEntryPopup(metadata);
            var routineName = await Shell.Current.ShowPopupAsync(popup) as string;
            if (routineName == null)
            {
                return;
            }

            routineVm.RoutineName = routineName;
            await routinesSvc.Update(routineVm.Routine);
        }

        private async Task DeleteRoutine(ShowRoutinesCardViewModel routineVm)
        {
            await routinesSvc.Delete(routineVm.Routine.Id);
            CardVms.Remove(routineVm);
        }
    }
}
