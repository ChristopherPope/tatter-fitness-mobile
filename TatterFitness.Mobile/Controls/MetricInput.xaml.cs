using CommunityToolkit.Mvvm.Messaging;
using TatterFitness.Mobile.NavData;
using TatterFitness.Mobile.Views;
using TatterFitness.Mobile.Messages;
using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.Controls;

public partial class MetricInput : Entry
{
    public readonly static BindableProperty MetricValueProperty = BindableProperty.Create(
        propertyName: nameof(MetricValue),
        returnType: typeof(double),
        declaringType: typeof(MetricInput),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay);

    public readonly static BindableProperty SetProperty = BindableProperty.Create(
        propertyName: nameof(Set),
        returnType: typeof(WorkoutExerciseSet),
        declaringType: typeof(MetricInput),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay);

    public WorkoutExerciseSet Set
    {
        get { return (WorkoutExerciseSet)GetValue(SetProperty); }
        set { SetValue(SetProperty, value); }
    }

    public double MetricValue
    {
        get
        {
            return (double)GetValue(MetricValueProperty);
        }

        set
        {
            if (!originalValue.HasValue)
            {
                originalValue = value;
            }
            SetValue(MetricValueProperty, value);
        }
    }

    private double? originalValue = null;

    public MetricInput()
    {
        InitializeComponent();
        TextChanged += OnTextChanged;
        Unfocused += OnUnfocused;
        Focused += OnFocused;
    }

    private bool IsDirty => (originalValue.HasValue && originalValue.Value != MetricValue);

    private void OnFocused(object sender, FocusEventArgs e)
    {
        CursorPosition = 0;
        if (Text != null)
        {
            SelectionLength = Text.Length;
        }
    }

    private async void OnUnfocused(object sender, FocusEventArgs e)
    {
        if (!IsDirty)
        {
            return;
        }

        try
        {
            if (Set.Id > 0)
            {
                WeakReferenceMessenger.Default.Send(new CompletedSetMetricsChangedMessage(Set));
            }

            originalValue = MetricValue;
        }
        catch (Exception ex)
        {
            var error = $"{ex.Message}{Environment.NewLine}";
            error += FormatCallStack(ex);
            var navData = new ErrorViewNavData(error);
            await Shell.Current.GoToAsync(nameof(ErrorView), true, navData.ToNavDataDictionary());
        }
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        double.TryParse(Text, out double val);
        MetricValue = val;
    }

    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName == MetricValueProperty.PropertyName)
        {
            Text = MetricValue.ToString();
        }
    }

    private string FormatCallStack(Exception ex)
    {
        var callStack = ex.ToString().Replace("at ", $"{Environment.NewLine}at ");
        return $"{Environment.NewLine}{callStack}{Environment.NewLine}";
    }
}