using TatterFitness.App.EventArguments;
using TatterFitness.App.Interfaces.Services;
using TatterFitness.App.Interfaces.Services.SelectorModals;
using TatterFitness.App.NavData.Select;
using TatterFitness.App.ViewModels.Select;
using TatterFitness.App.Views.Select;
using static TatterFitness.App.Utils.SelectorModalDelegates;

namespace TatterFitness.App.Services.SelectorModals
{
    public class ModsSelectorModal : SelectorModalBase, IModsSelectorModal
    {
        private ModsSelectionCompleted selectionCompleted;
        private SelectModsViewModel selectModsVm = null;

        public ModsSelectorModal(ILoggingService logger)
            : base(logger)
        {
        }

        public async Task ShowModal(IEnumerable<int> currrentModIds, ModsSelectionCompleted selectionCompleted)
        {
            try
            {
                this.selectionCompleted = selectionCompleted;

                Shell.Current.Navigated += OnNavigated;
                var navData = new SelectModsNavData(currrentModIds);
                await Shell.Current.GoToAsync(nameof(SelectModsView), true, navData.ToNavDataDictionary());
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        private void OnNavigated(object sender, ShellNavigatedEventArgs e)
        {
            if (Shell.Current.CurrentPage is SelectModsView view)
            {
                selectModsVm = view.ViewModel;
                selectModsVm.Closed += OnSelectModsModalClosed;
            }

            Shell.Current.Navigated -= OnNavigated;
        }

        private async void OnSelectModsModalClosed(object sender, SelectModsModalClosedEventArgs e)
        {
            try
            {
                selectModsVm.Closed -= OnSelectModsModalClosed;
                selectModsVm = null;
                await selectionCompleted(e.ModifiersToAdd, e.ModifiersToRemove);
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }
    }
}
