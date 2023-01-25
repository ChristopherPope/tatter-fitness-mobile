using static TatterFitness.App.Utils.SelectorModalDelegates;

namespace TatterFitness.App.Interfaces.Services.SelectorModals
{
    public interface IModsSelectorModal
    {
        Task ShowModal(IEnumerable<int> modIds, ModsSelectionCompleted selectionCompleted);
    }
}
