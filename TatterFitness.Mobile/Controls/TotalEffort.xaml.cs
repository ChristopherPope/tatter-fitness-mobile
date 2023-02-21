using System.Collections.ObjectModel;
using TatterFitness.App.Utils;
using TatterFitness.Models.Workouts;

namespace TatterFitness.App.Controls;

public partial class TotalEffort : Grid
{
    public readonly static BindableProperty WorkoutExercisesProperty = BindableProperty.Create(
        propertyName: nameof(WorkoutExercises),
        returnType: typeof(IEnumerable<WorkoutExercise>),
        declaringType: typeof(TotalEffort),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public readonly static BindableProperty SetsProperty = BindableProperty.Create(
        propertyName: nameof(Sets),
        returnType: typeof(ObservableCollection<WorkoutExerciseSet>),
        declaringType: typeof(TotalEffort),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public readonly static BindableProperty WorkoutExerciseProperty = BindableProperty.Create(
        propertyName: nameof(WorkoutExercise),
        returnType: typeof(WorkoutExercise),
        declaringType: typeof(TotalEffort),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public IEnumerable<WorkoutExercise> WorkoutExercises
    {
        get { return (IEnumerable<WorkoutExercise>)GetValue(WorkoutExercisesProperty); }
        set { SetValue(WorkoutExercisesProperty, value); }
    }

    public ObservableCollection<WorkoutExerciseSet> Sets
    {
        get { return (ObservableCollection<WorkoutExerciseSet>)GetValue(SetsProperty); }
        set { SetValue(SetsProperty, value); }
    }

    public WorkoutExercise WorkoutExercise
    {
        get { return (WorkoutExercise)GetValue(WorkoutExerciseProperty); }
        set { SetValue(WorkoutExerciseProperty, value); }
    }

    public TotalEffort()
    {
        InitializeComponent();
    }

    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if ((propertyName == WorkoutExercisesProperty.PropertyName && WorkoutExercises != null) ||
            (propertyName == WorkoutExerciseProperty.PropertyName && WorkoutExercise != null))
        {
            CalculateEffort();
        }
    }

    private void CalculateEffort()
    {
        var effortCalculator = new EffortCalculator();
        if (WorkoutExercise == null)
        {
            effortCalculator.Calculate(WorkoutExercises);
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
    }

    private void CalculateEffortOnSets()
    {
        var effortCalculator = new EffortCalculator();
        if (WorkoutExercise == null)
        {
            effortCalculator.Calculate(WorkoutExercises);
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
    }
}
