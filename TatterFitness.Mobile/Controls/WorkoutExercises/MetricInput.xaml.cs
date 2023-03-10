using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Mvvm.Input;
using Syncfusion.Maui.Core;

namespace TatterFitness.Mobile.Controls.WorkoutExercises;

public partial class MetricInput : SfTextInputLayout
{
    private bool isDirty = false;

    public readonly static BindableProperty MetricValueProperty = BindableProperty.Create(
        propertyName: nameof(MetricValue),
        returnType: typeof(string),
        declaringType: typeof(MetricInput),
        defaultValue: string.Empty,
        propertyChanged: OnMetricValuePropertyChanged,
        defaultBindingMode: BindingMode.TwoWay);

    public string MetricValue
    {
        get => (string)GetValue(MetricValueProperty);
        set => SetValue(MetricValueProperty, value);
    }

    public readonly static BindableProperty SetIdProperty = BindableProperty.Create(
        propertyName: nameof(SetId),
        returnType: typeof(int),
        declaringType: typeof(MetricInput),
        defaultValue: 0,
        propertyChanged: OnSetIdPropertyChanged,
        defaultBindingMode: BindingMode.OneWay);

    public int SetId
    {
        get => (int)GetValue(SetIdProperty);
        set => SetValue(SetIdProperty, value);
    }

    public readonly static BindableProperty MetricUpdatedCommandProperty = BindableProperty.Create(
        propertyName: nameof(MetricUpdatedCommand),
        returnType: typeof(IAsyncRelayCommand<int>),
        declaringType: typeof(MetricInput),
        defaultValue: null,
        propertyChanged: OnMetricUpdatedCommandPropertyChanged,
        defaultBindingMode: BindingMode.OneWay);

    public IAsyncRelayCommand<int> MetricUpdatedCommand
    {
        get => (IAsyncRelayCommand<int>)GetValue(MetricUpdatedCommandProperty);
        set => SetValue(MetricUpdatedCommandProperty, value);
    }

    public MetricInput()
    {
        InitializeComponent();
        metric.TextChanged += OnTextChanged;
        metric.Behaviors.Add(new UserStoppedTypingBehavior
        {
            StoppedTypingTimeThreshold = 1000,
            MinimumLengthThreshold = 3,
            ShouldDismissKeyboardAutomatically = false,
            Command = new Command(UserStoppedTyping)
        });
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (!IsEnabled)
        {
            return;
        }

        MetricValue = e.NewTextValue;
        isDirty = true;
    }

    private void UserStoppedTyping()
    {
        if (!IsEnabled || SetId == 0)
        {
            return;
        }

        if (isDirty && MetricUpdatedCommand != null)
        {
            MetricUpdatedCommand.Execute(SetId);
        }
        isDirty = false;
    }

    private static void OnSetIdPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var me = bindable as MetricInput;
        me.SetId = Convert.ToInt32(newValue);
    }

    private static void OnMetricValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var me = bindable as MetricInput;
        me.metric.Text = newValue.ToString();
        me.isDirty = false;
    }

    private static void OnMetricUpdatedCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var me = bindable as MetricInput;
        me.MetricUpdatedCommand = newValue as IAsyncRelayCommand<int>;
    }
}