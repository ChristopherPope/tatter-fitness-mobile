namespace TatterFitness.Mobile.Controls.WorkoutExercises;

public partial class FtoInfo : StackLayout
{
    public readonly static BindableProperty TrainingMaxProperty = BindableProperty.Create(
    propertyName: nameof(TrainingMax),
    returnType: typeof(int),
    declaringType: typeof(FtoInfo),
    defaultValue: null,
    propertyChanged: OnTrainingMaxChanged,
    defaultBindingMode: BindingMode.TwoWay);

    public int TrainingMax
    {
        get { return (int)GetValue(TrainingMaxProperty); }
        set { SetValue(TrainingMaxProperty, value); }
    }

    public readonly static BindableProperty WeekNumberProperty = BindableProperty.Create(
    propertyName: nameof(WeekNumber),
    returnType: typeof(int),
    declaringType: typeof(FtoInfo),
    defaultValue: null,
    propertyChanged: OnWeekNumberChanged,
    defaultBindingMode: BindingMode.TwoWay);

    public int WeekNumber
    {
        get { return (int)GetValue(WeekNumberProperty); }
        set { SetValue(WeekNumberProperty, value); }
    }


    public FtoInfo()
    {
        InitializeComponent();
    }

    private static void OnWeekNumberChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var me = bindable as FtoInfo;
        me.WeekNumber = Convert.ToInt32(newValue);
        me.UpdateUi();
    }

    private static void OnTrainingMaxChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var me = bindable as FtoInfo;
        me.TrainingMax = Convert.ToInt32(newValue);
        me.UpdateUi();
    }

    private void UpdateUi()
    {
        //ftoInfo.IsVisible = TrainingMax == 0 && WeekNumber == 0;

        ftoText.Text = $"531 - Week Number: {WeekNumber}, Training Max: {TrainingMax:#,0}";
    }
}