using Syncfusion.Maui.Core;

namespace TatterFitness.App.Controls.WorkoutExercises;

public partial class MetricInput : SfTextInputLayout
{
    public readonly static BindableProperty MetricValueProperty = BindableProperty.Create(
        propertyName: nameof(MetricValue),
        returnType: typeof(string),
        declaringType: typeof(MetricInput),
        defaultValue: string.Empty,
        propertyChanged: OnMetricValueChanged,
        defaultBindingMode: BindingMode.TwoWay);

    public string MetricValue
    {
        get { return (string)GetValue(MetricValueProperty); }
        set { SetValue(MetricValueProperty, value); }
    }

    public MetricInput()
    {
        InitializeComponent();
        metricEntry.TextChanged += OnTextChanged;
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        MetricValue = e.NewTextValue;
    }

    public static void OnMetricValueChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var metricInput = (MetricInput)bindable;
        metricInput.metricEntry.Text = newValue.ToString();
    }
}