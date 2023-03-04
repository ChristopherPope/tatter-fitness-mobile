using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TatterFitness.Mobile.Interfaces.Services;
using TatterFitness.Mobile.Interfaces.Services.API;
using TatterFitness.Mobile.Interfaces.Services.SelectorModals;
using TatterFitness.Mobile.NavData;
using TatterFitness.Models.Exercises;

namespace TatterFitness.Mobile.ViewModels.History.ExHistory
{
    public partial class ExerciseHistoryViewModel : ViewModelBase, IQueryAttributable
    {
        private int exerciseId;
        private readonly IHistoriesApiService historiesApi;
        private readonly IExercisesApiService exercisesApi;
        private readonly IExercisesSelectorModal exercisesSelectorModal;
        private readonly string storageKey = "ExerciseHistoryId";

        [ObservableProperty]
        private ObservableCollection<ExerciseHistoryCardViewModel> cardVms = new();

        public ExerciseHistoryViewModel(ILoggingService logger, IHistoriesApiService historiesApi, IExercisesApiService exercisesApi, IExercisesSelectorModal exercisesSelectorModal)
            : base(logger)
        {
            this.historiesApi = historiesApi;
            this.exercisesApi = exercisesApi;
            this.exercisesSelectorModal = exercisesSelectorModal;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var navData = NavDataBase.FromNavDataDictionary<ExerciseHistoryNavData>(query);
            exerciseId = navData.ExerciseId;
        }

        protected async override Task PerformLoadViewData()
        {
            if (exerciseId == 0)
            {
                exerciseId = Preferences.Get(storageKey, 0);
                if (exerciseId == 0)
                {
                    return;
                }
            }

            Preferences.Set(storageKey, exerciseId);

            Title = (await exercisesApi.Read(exerciseId)).Name;
            var histories = await historiesApi.ReadExercise(exerciseId);
            foreach (var history in histories)
            {
                var cardVm = new ExerciseHistoryCardViewModel(logger, history);
                cardVms.Add(cardVm);

                await cardVm.LoadViewData();
            }
        }

        [RelayCommand]
        private async Task SelectExercise()
        {
            try
            {
                await exercisesSelectorModal.ShowModal(exerciseId, ExercisesModalClosed);
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
                if (!exercisesToAdd.Any())
                {
                    return;
                }

                var exercise = exercisesToAdd.First();
                Title = exercise.Name;
                exerciseId = exercise.Id;
                cardVms.Clear();
                await PerformLoadViewData();
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }
    }
}
