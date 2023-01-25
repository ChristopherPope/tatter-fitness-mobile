using TatterFitness.App.Utils;
using TatterFitness.Models.Enums;
using TatterFitness.Models.Exercises;
using TatterFitness.Models.Workouts;

namespace TatterFitness.App.Controls;

public partial class ExerciseEffort : Grid
{
    public readonly static BindableProperty ExerciseHistoryProperty = BindableProperty.Create(
        propertyName: nameof(ExerciseHistory),
        returnType: typeof(ExerciseHistory),
        declaringType: typeof(ExerciseEffort),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public readonly static BindableProperty WorkoutExerciseProperty = BindableProperty.Create(
        propertyName: nameof(WorkoutExercise),
        returnType: typeof(WorkoutExercise),
        declaringType: typeof(ExerciseEffort),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public WorkoutExercise WorkoutExercise
    {
        get { return (WorkoutExercise)GetValue(WorkoutExerciseProperty); }
        set { SetValue(WorkoutExerciseProperty, value); }
    }

    public ExerciseHistory ExerciseHistory
    {
        get { return (ExerciseHistory)GetValue(ExerciseHistoryProperty); }
        set { SetValue(ExerciseHistoryProperty, value); }
    }

    public ExerciseEffort()
    {
        InitializeComponent();
    }

    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        if ((propertyName == ExerciseHistoryProperty.PropertyName && ExerciseHistory != null) ||
            (propertyName == WorkoutExerciseProperty.PropertyName && WorkoutExercise != null))
        {
            ShowEffort();
        }
    }

    private void ShowEffort()
    {
        ToggleEffortDisplay();

        var effortCalculator = new EffortCalculator();
        if (WorkoutExercise == null)
        {
            effortCalculator.Calculate(ExerciseHistory);
        }
        else
        {
            effortCalculator.Calculate(WorkoutExercise);
        }

        var effortFormatter = new EffortFormatter();
        effortFormatter.FormatEffort(effortCalculator);

        rwVolume.Text = effortFormatter.RWVolume;
        roReps.Text = effortFormatter.ROReps;
        dwDuration.Text = effortFormatter.DWDuration;
        dwVolume.Text = effortFormatter.DWVolume;
        cDuration.Text = effortFormatter.CDuration;
        cMiles.Text = effortFormatter.CMiles;
        cBpm.Text = effortFormatter.CBpm;
    }

    private void ToggleEffortDisplay()
    {
        var exType = ExerciseHistory != null ? ExerciseHistory.ExerciseType : WorkoutExercise.ExerciseType;

        rwEffortGrid.IsVisible = exType == ExerciseTypes.RepsAndWeight;
        roEffortGrid.IsVisible = exType == ExerciseTypes.RepsOnly;
        dwEffortGrid.IsVisible = exType == ExerciseTypes.DurationAndWeight;
        cEffortGrid.IsVisible = exType == ExerciseTypes.Cardio;
    }
}