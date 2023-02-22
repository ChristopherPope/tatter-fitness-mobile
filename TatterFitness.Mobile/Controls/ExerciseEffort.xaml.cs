using TatterFitness.Models.Exercises;
using TatterFitness.Models.Workouts;

namespace TatterFitness.App.Controls;

public partial class ExerciseEffort : Grid
{
    #region OldProps
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
    #endregion


    public readonly static BindableProperty RWVolumeProperty = BindableProperty.Create(
    propertyName: nameof(RWVolume),
    returnType: typeof(string),
    declaringType: typeof(ExerciseEffort),
    defaultValue: null,
    defaultBindingMode: BindingMode.OneWay);

    public readonly static BindableProperty RORepsProperty = BindableProperty.Create(
        propertyName: nameof(ROReps),
        returnType: typeof(string),
        declaringType: typeof(ExerciseEffort),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public readonly static BindableProperty DWDurationProperty = BindableProperty.Create(
        propertyName: nameof(DWDuration),
        returnType: typeof(string),
        declaringType: typeof(ExerciseEffort),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public readonly static BindableProperty DWVolumeProperty = BindableProperty.Create(
        propertyName: nameof(DWVolume),
        returnType: typeof(string),
        declaringType: typeof(ExerciseEffort),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public readonly static BindableProperty CDurationProperty = BindableProperty.Create(
        propertyName: nameof(CDuration),
        returnType: typeof(string),
        declaringType: typeof(ExerciseEffort),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public readonly static BindableProperty CMilesProperty = BindableProperty.Create(
        propertyName: nameof(CMiles),
        returnType: typeof(string),
        declaringType: typeof(ExerciseEffort),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public readonly static BindableProperty AverageBpmProperty = BindableProperty.Create(
        propertyName: nameof(AverageBpm),
        returnType: typeof(string),
        declaringType: typeof(ExerciseEffort),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public readonly static BindableProperty IsRepsAndWeightGridVisibleProperty = BindableProperty.Create(
        propertyName: nameof(IsRepsAndWeightGridVisible),
        returnType: typeof(bool),
        declaringType: typeof(ExerciseEffort),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public readonly static BindableProperty IsRepsOnlyGridVisibleProperty = BindableProperty.Create(
        propertyName: nameof(IsRepsOnlyGridVisible),
        returnType: typeof(bool),
        declaringType: typeof(ExerciseEffort),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public readonly static BindableProperty IsDurationAndWeightGridVisibleProperty = BindableProperty.Create(
        propertyName: nameof(IsDurationAndWeightGridVisible),
        returnType: typeof(bool),
        declaringType: typeof(ExerciseEffort),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public readonly static BindableProperty IsCardioGridVisibleProperty = BindableProperty.Create(
        propertyName: nameof(IsCardioGridVisible),
        returnType: typeof(bool),
        declaringType: typeof(ExerciseEffort),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public bool IsRepsAndWeightGridVisible
    {
        get { return (bool)GetValue(IsRepsAndWeightGridVisibleProperty); }
        set { SetValue(IsRepsAndWeightGridVisibleProperty, value); }
    }

    public bool IsRepsOnlyGridVisible
    {
        get { return (bool)GetValue(IsRepsOnlyGridVisibleProperty); }
        set { SetValue(IsRepsOnlyGridVisibleProperty, value); }
    }

    public bool IsDurationAndWeightGridVisible
    {
        get { return (bool)GetValue(IsDurationAndWeightGridVisibleProperty); }
        set { SetValue(IsDurationAndWeightGridVisibleProperty, value); }
    }

    public bool IsCardioGridVisible
    {
        get { return (bool)GetValue(IsCardioGridVisibleProperty); }
        set { SetValue(IsCardioGridVisibleProperty, value); }
    }

    public string RWVolume
    {
        get { return (string)GetValue(RWVolumeProperty); }
        set { SetValue(RWVolumeProperty, value); }
    }

    public string AverageBpm
    {
        get { return (string)GetValue(AverageBpmProperty); }
        set { SetValue(AverageBpmProperty, value); }
    }

    public string ROReps
    {
        get { return (string)GetValue(RORepsProperty); }
        set { SetValue(RORepsProperty, value); }
    }

    public string DWDuration
    {
        get { return (string)GetValue(DWDurationProperty); }
        set { SetValue(DWDurationProperty, value); }
    }

    public string DWVolume
    {
        get { return (string)GetValue(DWVolumeProperty); }
        set { SetValue(DWVolumeProperty, value); }
    }

    public string CDuration
    {
        get { return (string)GetValue(CDurationProperty); }
        set { SetValue(CDurationProperty, value); }
    }

    public string CMiles
    {
        get { return (string)GetValue(CMilesProperty); }
        set { SetValue(CMilesProperty, value); }
    }

    public ExerciseEffort()
    {
        InitializeComponent();
        IsRepsAndWeightGridVisible = true;
        IsRepsOnlyGridVisible = true;
        IsDurationAndWeightGridVisible = true;
        IsCardioGridVisible = true;
    }

    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        if (propertyName == IsRepsAndWeightGridVisibleProperty.PropertyName)
        {
            rwEffortGrid.IsVisible = IsRepsAndWeightGridVisible;
        }
        else if (propertyName == IsRepsOnlyGridVisibleProperty.PropertyName)
        {
            roEffortGrid.IsVisible = IsRepsOnlyGridVisible;
        }
        else if (propertyName == IsDurationAndWeightGridVisibleProperty.PropertyName)
        {
            dwEffortGrid.IsVisible = IsDurationAndWeightGridVisible;
        }
        else if (propertyName == IsCardioGridVisibleProperty.PropertyName)
        {
            cEffortGrid.IsVisible = IsCardioGridVisible;
        }
        else if (propertyName == RWVolumeProperty.PropertyName)
        {
            rwVolume.Text = RWVolume;
        }
        else if (propertyName == RORepsProperty.PropertyName)
        {
            roReps.Text = ROReps;
        }
        else if (propertyName == DWDurationProperty.PropertyName)
        {
            dwDuration.Text = DWDuration;
        }
        else if (propertyName == DWVolumeProperty.PropertyName)
        {
            dwVolume.Text = DWVolume;
        }
        else if (propertyName == CDurationProperty.PropertyName)
        {
            cDuration.Text = CDuration;
        }
        else if (propertyName == CMilesProperty.PropertyName)
        {
            cMiles.Text = CMiles;
        }
        else if (propertyName == AverageBpmProperty.PropertyName)
        {
            cBpm.Text = AverageBpm;
        }
    }
}