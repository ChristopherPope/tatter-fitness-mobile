using TatterFitness.Models.Enums;

namespace TatterFitness.Mobile.Controls;

public partial class ExerciseEffort : Grid
{
    public readonly static BindableProperty RWVolumeProperty = BindableProperty.Create(
        propertyName: nameof(RWVolume),
        returnType: typeof(string),
        declaringType: typeof(ExerciseEffort),
        defaultValue: null,
        propertyChanged: OnRwVolumeChanged,
        defaultBindingMode: BindingMode.OneWay);

    public string RWVolume
    {
        get { return (string)GetValue(RWVolumeProperty); }
        set { SetValue(RWVolumeProperty, value); }
    }


    public readonly static BindableProperty RORepsProperty = BindableProperty.Create(
        propertyName: nameof(ROReps),
        returnType: typeof(string),
        declaringType: typeof(ExerciseEffort),
        defaultValue: null,
        propertyChanged: OnRoRepsChanged,
        defaultBindingMode: BindingMode.OneWay);

    public string ROReps
    {
        get { return (string)GetValue(RORepsProperty); }
        set { SetValue(RORepsProperty, value); }
    }


    public readonly static BindableProperty DWDurationProperty = BindableProperty.Create(
        propertyName: nameof(DWDuration),
        returnType: typeof(string),
        declaringType: typeof(ExerciseEffort),
        defaultValue: null,
        propertyChanged: OnDwDurationChanged,
        defaultBindingMode: BindingMode.OneWay);

    public string DWDuration
    {
        get { return (string)GetValue(DWDurationProperty); }
        set { SetValue(DWDurationProperty, value); }
    }


    public readonly static BindableProperty DWVolumeProperty = BindableProperty.Create(
        propertyName: nameof(DWVolume),
        returnType: typeof(string),
        declaringType: typeof(ExerciseEffort),
        defaultValue: null,
        propertyChanged: OnDwVolumeChanged,
        defaultBindingMode: BindingMode.OneWay);

    public string DWVolume
    {
        get { return (string)GetValue(DWVolumeProperty); }
        set { SetValue(DWVolumeProperty, value); }
    }


    public readonly static BindableProperty CDurationProperty = BindableProperty.Create(
        propertyName: nameof(CDuration),
        returnType: typeof(string),
        declaringType: typeof(ExerciseEffort),
        defaultValue: null,
        propertyChanged: OnCDurationChanged,
        defaultBindingMode: BindingMode.OneWay);

    public string CDuration
    {
        get { return (string)GetValue(CDurationProperty); }
        set { SetValue(CDurationProperty, value); }
    }


    public readonly static BindableProperty CMilesProperty = BindableProperty.Create(
        propertyName: nameof(CMiles),
        returnType: typeof(string),
        declaringType: typeof(ExerciseEffort),
        defaultValue: null,
        propertyChanged: OnCMilesChanged,
        defaultBindingMode: BindingMode.OneWay);

    public string CMiles
    {
        get { return (string)GetValue(CMilesProperty); }
        set { SetValue(CMilesProperty, value); }
    }


    public readonly static BindableProperty AverageBpmProperty = BindableProperty.Create(
        propertyName: nameof(AverageBpm),
        returnType: typeof(string),
        declaringType: typeof(ExerciseEffort),
        defaultValue: null,
        propertyChanged: OnAverageBpmChanged,
        defaultBindingMode: BindingMode.OneWay);

    public string AverageBpm
    {
        get { return (string)GetValue(AverageBpmProperty); }
        set { SetValue(AverageBpmProperty, value); }
    }


    public readonly static BindableProperty ExerciseTypeProperty = BindableProperty.Create(
        propertyName: nameof(ExerciseType),
        returnType: typeof(ExerciseTypes),
        declaringType: typeof(ExerciseEffort),
        defaultValue: ExerciseTypes.Cardio,
        propertyChanged: OnExerciseTypeChanged,
        defaultBindingMode: BindingMode.OneWay);

    public ExerciseTypes ExerciseType
    {
        get { return (ExerciseTypes)GetValue(ExerciseTypeProperty); }
        set { SetValue(ExerciseTypeProperty, value); }
    }


    public ExerciseEffort()
    {
        InitializeComponent();
    }

    private static void OnExerciseTypeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var me = bindable as ExerciseEffort;
        me.ExerciseType = (ExerciseTypes)newValue;
        me.rwEffortGrid.IsVisible = me.ExerciseType == ExerciseTypes.RepsAndWeight;
        me.roEffortGrid.IsVisible = me.ExerciseType == ExerciseTypes.RepsOnly;
        me.dwEffortGrid.IsVisible = me.ExerciseType == ExerciseTypes.DurationAndWeight;
        me.cEffortGrid.IsVisible = me.ExerciseType == ExerciseTypes.Cardio;
    }

    private static void OnRwVolumeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (newValue is null)
        {
            return;
        }

        var me = bindable as ExerciseEffort;
        me.RWVolume = newValue.ToString();
        me.rwVolume.Text = me.RWVolume;
    }

    private static void OnRoRepsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (newValue is null)
        {
            return;
        }

        var me = bindable as ExerciseEffort;
        me.ROReps = newValue.ToString();
        me.roReps.Text = me.ROReps;
    }

    private static void OnDwVolumeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (newValue is null)
        {
            return;
        }

        var me = bindable as ExerciseEffort;
        me.DWVolume = newValue.ToString();
        me.dwVolume.Text = me.DWVolume;
    }

    private static void OnCDurationChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (newValue is null)
        {
            return;
        }

        var me = bindable as ExerciseEffort;
        me.CDuration = newValue.ToString();
        me.cDuration.Text = me.CDuration;
    }

    private static void OnCMilesChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (newValue is null)
        {
            return;
        }

        var me = bindable as ExerciseEffort;
        me.CMiles = newValue.ToString();
        me.cMiles.Text = me.CMiles;
    }

    private static void OnAverageBpmChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (newValue is null)
        {
            return;
        }

        var me = bindable as ExerciseEffort;
        me.AverageBpm = newValue.ToString();
        me.cBpm.Text = me.AverageBpm;
    }

    private static void OnDwDurationChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (newValue is null)
        {
            return;
        }

        var me = bindable as ExerciseEffort;
        me.DWDuration = newValue.ToString();
        me.dwDuration.Text = me.DWDuration;
    }
}