using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using TatterFitness.Mobile.Messages;
using TatterFitness.Models.Workouts;

namespace TatterFitness.App.ViewModels.Workouts.WorkoutExercises
{
    [INotifyPropertyChanged]
    public abstract partial class BaseSetViewModel :
        IRecipient<SetCompletedMessage>,
        IRecipient<SetAddedMessage>,
        IRecipient<SetDeletedMessage>
    {
        [ObservableProperty]
        private bool isCompleted;

        [ObservableProperty]
        private int setNumber;

        [ObservableProperty]
        private int totalSets;

        public WorkoutExerciseSet Set { get; set; }

        public BaseSetViewModel(WorkoutExerciseSet set, int totalSets)
        {
            Set = set;
            SetNumber = set.SetNumber;
            TotalSets = totalSets;
            IsCompleted = set.Id > 0;

            WeakReferenceMessenger.Default.Register(this as IRecipient<SetCompletedMessage>);
            WeakReferenceMessenger.Default.Register(this as IRecipient<SetAddedMessage>);
            WeakReferenceMessenger.Default.Register(this as IRecipient<SetDeletedMessage>);
        }

        public void Receive(SetCompletedMessage message)
        {
            IsCompleted = Set.Id > 0;
        }

        public void Receive(SetAddedMessage message)
        {
            TotalSets++;
        }

        public void Receive(SetDeletedMessage message)
        {
            TotalSets--;
        }
    }
}
