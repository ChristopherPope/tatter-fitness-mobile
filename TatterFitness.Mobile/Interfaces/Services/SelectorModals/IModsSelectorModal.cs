using static TatterFitness.Mobile.Utils.SelectorModalDelegates;

namespace TatterFitness.Mobile.Interfaces.Services.SelectorModals
{
    public interface IModsSelectorModal
    {
        Task ShowModal(IEnumerable<int> modIds, ModsSelectionCompleted selectionCompleted);
    }
}
