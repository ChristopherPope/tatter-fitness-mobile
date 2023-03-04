namespace TatterFitness.Mobile.Controls.WorkoutExercises;

public partial class SetNumber : Border
{
    public readonly static BindableProperty IsCompletedProperty = BindableProperty.Create(
        propertyName: nameof(IsCompleted),
        returnType: typeof(bool),
        declaringType: typeof(SetNumber),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public bool IsCompleted
    {
        get => (bool)GetValue(IsCompletedProperty);
        set => SetValue(IsCompletedProperty, value);
    }


    public readonly static BindableProperty SetNumProperty = BindableProperty.Create(
        propertyName: nameof(SetNum),
        returnType: typeof(int),
        declaringType: typeof(SetNumber),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public int SetNum
    {
        get => (int)GetValue(SetNumProperty);
        set => SetValue(SetNumProperty, value);
    }


    public readonly static BindableProperty TotalSetsProperty = BindableProperty.Create(
        propertyName: nameof(TotalSets),
        returnType: typeof(int),
        declaringType: typeof(SetNumber),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public int TotalSets
    {
        get => (int)GetValue(TotalSetsProperty);
        set => SetValue(TotalSetsProperty, value);
    }

    public SetNumber()
    {
        InitializeComponent();
    }

    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        if (propertyName == TotalSetsProperty.PropertyName ||
            propertyName == SetNumProperty.PropertyName)
        {
            setNumber.Text = $"{SetNum} of {TotalSets}";
        }
    }
}