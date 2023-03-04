using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TatterFitness.Mobile.EventArguments;
using TatterFitness.Mobile.Interfaces.Services;
using TatterFitness.Mobile.Interfaces.Services.API;
using TatterFitness.Mobile.NavData;
using TatterFitness.Mobile.NavData.Select;

namespace TatterFitness.Mobile.ViewModels.Select
{
    public partial class SelectModsViewModel : ViewModelBase, IQueryAttributable
    {
        private IEnumerable<int> originalModifierIds = new List<int>();
        private readonly IExerciseModifiersApiService modsApiSvc;
        
        [ObservableProperty]
        private ObservableCollection<SelectModsCardViewModel> cardVms = new();

        public event EventHandler<SelectModsModalClosedEventArgs> Closed;

        public SelectModsViewModel(ILoggingService logger, IExerciseModifiersApiService modsApiSvc)
            : base(logger)
        {
            this.modsApiSvc = modsApiSvc;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var navData = NavDataBase.FromNavDataDictionary<SelectModsNavData>(query);
            originalModifierIds = navData.currentModIds;
        }

        protected async override Task PerformLoadViewData()
        {
            CardVms.Clear();
            var allModifiers = await modsApiSvc.ReadAll();
            foreach (var modifier in allModifiers)
            {
                CardVms.Add(new SelectModsCardViewModel(logger, modifier)
                {
                    IsSelected = originalModifierIds.Contains(modifier.Id)
                });
            }
        }

        [RelayCommand]
        private void SelectMod(SelectModsCardViewModel cardVm)
        {
            try
            {
                cardVm.IsSelected = !cardVm.IsSelected;
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
                Closed?.Invoke(this, new SelectModsModalClosedEventArgs());
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
                await Shell.Current.GoToAsync("..");

                var selectedModIds = CardVms
                    .Where(m => m.IsSelected)
                    .Select(m => m.Modifier.Id);

                var modIdsToAdd = selectedModIds.Except(originalModifierIds);
                var modIdsToRemove = originalModifierIds.Except(selectedModIds);

                var modsToAdd = CardVms
                    .Where(vm => modIdsToAdd.Contains(vm.Modifier.Id))
                    .Select(vm => vm.Modifier);

                var modsToRemove = CardVms
                    .Where(vm => modIdsToRemove.Contains(vm.Modifier.Id))
                    .Select(vm => vm.Modifier);

                Closed?.Invoke(this, new SelectModsModalClosedEventArgs(modsToAdd, modsToRemove));
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }
    }
}
