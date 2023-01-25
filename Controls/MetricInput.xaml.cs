using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using TatterFitness.App.Interfaces.Services;
using TatterFitness.App.NavData;
using TatterFitness.App.Views;

namespace TatterFitness.App.Controls;

public partial class MetricInput : Entry, INotifyPropertyChanged
{
    public readonly static BindableProperty MetricModifiedCommandProperty = BindableProperty.Create(
        propertyName: nameof(MetricModifiedCommand),
        returnType: typeof(IAsyncRelayCommand),
        declaringType: typeof(MetricInput),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public readonly static BindableProperty MetricModifiedCommandParameterProperty = BindableProperty.Create(
        propertyName: nameof(MetricModifiedCommandParameter),
        returnType: typeof(object),
        declaringType: typeof(MetricInput),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public readonly static BindableProperty MetricValueProperty = BindableProperty.Create(
        propertyName: nameof(MetricValue),
        returnType: typeof(double),
        declaringType: typeof(MetricInput),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay);

    public IAsyncRelayCommand MetricModifiedCommand
    {
        get
        {
            return (IAsyncRelayCommand)GetValue(MetricModifiedCommandProperty);
        }

        set
        {
            SetValue(MetricModifiedCommandProperty, value);
        }
    }

    public object MetricModifiedCommandParameter
    {
        get
        {
            return GetValue(MetricModifiedCommandParameterProperty);
        }

        set
        {
            SetValue(MetricModifiedCommandParameterProperty, value);
        }
    }

    public double MetricValue
    {
        get
        {
            return (double)GetValue(MetricValueProperty);
        }

        set
        {
            if (! originalValue.HasValue)
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
            if (MetricModifiedCommand != null)
            {
                await MetricModifiedCommand.ExecuteAsync(MetricModifiedCommandParameter);
            }

            originalValue = MetricValue;
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(ex);
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

    private async Task HandleExceptionAsync(Exception ex)
    {
        var error = $"{ex.Message}{Environment.NewLine}";
        error += FormatCallStack(ex);
        var navData = new ErrorViewNavData(error);
        await Shell.Current.GoToAsync(nameof(ErrorView), true, navData.ToNavDataDictionary());
    }

    private string FormatCallStack(Exception ex)
    {
        var callStack = ex.ToString().Replace("at ", $"{Environment.NewLine}at ");
        return $"{Environment.NewLine}{callStack}{Environment.NewLine}";
    }
}