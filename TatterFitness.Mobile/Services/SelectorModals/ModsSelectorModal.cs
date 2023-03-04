using TatterFitness.Mobile.EventArguments;
using TatterFitness.Mobile.Interfaces.Services;
using TatterFitness.Mobile.Interfaces.Services.SelectorModals;
using TatterFitness.Mobile.NavData.Select;
using TatterFitness.Mobile.ViewModels.Select;
using TatterFitness.Mobile.Views.Select;
using static TatterFitness.Mobile.Utils.SelectorModalDelegates;

namespace TatterFitness.Mobile.Services.SelectorModals
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
