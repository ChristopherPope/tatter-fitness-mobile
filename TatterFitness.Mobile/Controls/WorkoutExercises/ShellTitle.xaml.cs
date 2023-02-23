namespace TatterFitness.App.Controls.WorkoutExercises;

public partial class ShellTitle : VerticalStackLayout
{
    public readonly static BindableProperty ExerciseNameProperty = BindableProperty.Create(
        propertyName: nameof(ExerciseName),
        returnType: typeof(string),
        declaringType: typeof(ShellTitle),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public string ExerciseName
    {
        get => (string)GetValue(ExerciseNameProperty);
        set => SetValue(ExerciseNameProperty, value);
    }


    public readonly static BindableProperty ModNamesProperty = BindableProperty.Create(
        propertyName: nameof(ModNames),
        returnType: typeof(string),
        declaringType: typeof(ShellTitle),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public string ModNames
    {
        get => (string)GetValue(ModNamesProperty);
        set => SetValue(ModNamesProperty, value);
    }

    public ShellTitle()
    {
        InitializeComponent();
    }

    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        if (propertyName == ModNamesProperty.PropertyName)
        {
            modNames.Text = ModNames;
        }
        else if (propertyName == ExerciseNameProperty.PropertyName)
        {
            exerciseName.Text = ExerciseName;
        }
    }
}