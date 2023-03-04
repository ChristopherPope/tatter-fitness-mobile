using TatterFitness.Mobile.EventArguments;
using TatterFitness.Mobile.Interfaces.Services;
using TatterFitness.Mobile.Interfaces.Services.SelectorModals;
using TatterFitness.Mobile.NavData.Select;
using TatterFitness.Mobile.ViewModels.Select;
using TatterFitness.Mobile.Views.Select;
using static TatterFitness.Mobile.Utils.SelectorModalDelegates;

namespace TatterFitness.Mobile.Services.SelectorModals
{
    public class ExercisesSelectorModal : SelectorModalBase, IExercisesSelectorModal
    {
        private ExercisesSelectionCompleted selectionCompleted;
        private SelectExercisesViewModel selectExercisesVm = null;

        public ExercisesSelectorModal(ILoggingService logger)
            : base(logger)
        {
        }

        public async Task ShowModal(IEnumerable<int> currentExerciseIds, ExercisesSelectionCompleted selectionCompleted)
        {
            var navData = new SelectExercisesNavData(currentExerciseIds);
            await ShowModal(navData, selectionCompleted);
        }

        public async Task ShowModal(IEnumerable<int> currentExerciseIds, IEnumerable<int> requiredExerciseIds, ExercisesSelectionCompleted selectionCompleted)
        {
            var navData = new SelectExercisesNavData(currentExerciseIds, requiredExerciseIds);
            await ShowModal(navData, selectionCompleted);
        }

        public async Task ShowModal(int currentExerciseId, ExercisesSelectionCompleted selectionCompleted)
        {
            var navData = new SelectExercisesNavData(currentExerciseId);
            await ShowModal(navData, selectionCompleted);
        }


        private async Task ShowModal(SelectExercisesNavData navData, ExercisesSelectionCompleted selectionCompleted)
        {
            try
            {
                this.selectionCompleted = selectionCompleted;
                Shell.Current.Navigated += OnNavigated;
                await Shell.Current.GoToAsync(nameof(SelectExercisesView), true, navData.ToNavDataDictionary());
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        private void OnNavigated(object sender, ShellNavigatedEventArgs e)
        {
            if (Shell.Current.CurrentPage is SelectExercisesView view)
            {
                selectExercisesVm = view.ViewModel;
                selectExercisesVm.Closed += OnSelectExercisesModalClosed;
            }

            Shell.Current.Navigated -= OnNavigated;
        }

        private async void OnSelectExercisesModalClosed(object sender, SelectExercisesModalClosedEventArgs e)
        {
            try
            {
                selectExercisesVm.Closed -= OnSelectExercisesModalClosed;
                selectExercisesVm = null;
                await selectionCompleted(e.ExercisesToAdd, e.ExercisesToRemove);
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }
    }
}
