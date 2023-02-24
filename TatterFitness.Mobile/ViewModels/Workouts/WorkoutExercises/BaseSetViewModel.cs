using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using TatterFitness.Mobile.Messages;
using TatterFitness.Models.Workouts;

namespace TatterFitness.App.ViewModels.Workouts.WorkoutExercises
{
    [INotifyPropertyChanged]
    public abstract partial class BaseSetViewModel :
        IRecipient<SetDeletedMessage>,
        IRecipient<SetCompletedMessage>,
        IRecipient<SetCountChangedMessage>
    {
        protected readonly int exerciseId;

        [ObservableProperty]
        private bool isCompleted;

        [ObservableProperty]
        private int setNumber;

        [ObservableProperty]
        private int totalSets;

        public WorkoutExerciseSet Set { get; set; }

        public BaseSetViewModel(int exerciseId, WorkoutExerciseSet set, int totalSets)
        {
            this.exerciseId = exerciseId;
            Set = set;
            SetNumber = set.SetNumber;
            TotalSets = totalSets;
            IsCompleted = set.Id > 0;

            WeakReferenceMessenger.Default.Register(this as IRecipient<SetDeletedMessage>);
            WeakReferenceMessenger.Default.Register(this as IRecipient<SetCompletedMessage>);
            WeakReferenceMessenger.Default.Register(this as IRecipient<SetCountChangedMessage>);
        }

        public void Receive(SetDeletedMessage message)
        {
            if (message.Value.ExerciseId != exerciseId ||
                message.Value.DeletedSetNumber > SetNumber)
            {
                return;
            }

            SetNumber--;
        }

        public void Receive(SetCompletedMessage message)
        {
            IsCompleted = Set.Id > 0;
        }

        public void Receive(SetCountChangedMessage message)
        {
            if (message.Value.ExerciseId == exerciseId)
            {
                TotalSets = message.Value.SetCount;
            }
        }
    }
}
