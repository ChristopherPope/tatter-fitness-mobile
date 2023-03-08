using CommunityToolkit.Mvvm.Messaging;
using Syncfusion.Maui.Core;
using TatterFitness.Mobile.Interfaces.Services;
using TatterFitness.Mobile.Messages;
using TatterFitness.Mobile.Services;

namespace TatterFitness.Mobile.Controls.WorkoutExercises;

public partial class MetricInput : SfTextInputLayout
{
    private readonly ILoggingService logger = new LoggingService();
    private bool isDirty = false;

    public readonly static BindableProperty MetricValueProperty = BindableProperty.Create(
        propertyName: nameof(MetricValue),
        returnType: typeof(string),
        declaringType: typeof(MetricInput),
        defaultValue: string.Empty,
        propertyChanged: OnMetricValuePropertyChanged,
        defaultBindingMode: BindingMode.TwoWay);

    public readonly static BindableProperty SetIdProperty = BindableProperty.Create(
        propertyName: nameof(SetId),
        returnType: typeof(int),
        declaringType: typeof(MetricInput),
        defaultValue: 0,
        propertyChanged: OnSetIdPropertyChanged,
        defaultBindingMode: BindingMode.OneWay);

    public readonly static BindableProperty SetNumberProperty = BindableProperty.Create(
        propertyName: nameof(SetNumber),
        returnType: typeof(int),
        declaringType: typeof(MetricInput),
        defaultValue: 0,
        propertyChanged: OnSetNumberPropertyChanged,
        defaultBindingMode: BindingMode.OneWay);

    public string MetricValue
    {
        get => (string)GetValue(MetricValueProperty);
        set => SetValue(MetricValueProperty, value);
    }

    public int SetNumber
    {
        get => (int)GetValue(SetNumberProperty);
        set => SetValue(SetNumberProperty, value);
    }

    public int SetId
    {
        get => (int)GetValue(SetIdProperty);
        set => SetValue(SetIdProperty, value);
    }

    public MetricInput()
    {
        InitializeComponent();
        metric.TextChanged += OnTextChanged;
        metric.Behaviors.Add(new UserStoppedTypingBehavior
        {
            StoppedTypingTimeThreshold = 1000,
            MinimumLengthThreshold = 3,
            ShouldDismissKeyboardAutomatically = true,
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
        LogAction($"OnTextChanged Old='{e.OldTextValue}', New='{e.NewTextValue}'");
    }

    private void UserStoppedTyping()
    {
        LogAction("UserStoppedTyping");
        if (!IsEnabled || SetId == 0)
        {
            return;
        }

        if (isDirty)
        {
            LogAction("Will Send CompletedSetMetricsChangedMessage");
            WeakReferenceMessenger.Default.Send(new CompletedSetMetricsChangedMessage(SetId));
            isDirty = false;
        }
    }

    public override string ToString()
    {
        return $"{SetNumber}: {Hint} = {metric.Text}, SetId = {SetId}, IsDirty? = {isDirty}";
    }

    private static void OnSetNumberPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var me = bindable as MetricInput;
        me.SetNumber = Convert.ToInt32(newValue);

        me.LogAction("OnSetNumberPropertyChanged");
    }

    private static void OnSetIdPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var me = bindable as MetricInput;
        me.SetId = Convert.ToInt32(newValue);

        me.LogAction("OnSetIdPropertyChanged");
    }

    private static void OnMetricValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var me = bindable as MetricInput;
        me.metric.Text = newValue.ToString();
        me.isDirty = false;

        me.LogAction("OnMetricValuePropertyChanged");
    }

    private void LogAction(string action)
    {
        if (!IsEnabled)
        {
            return;
        }

        //logger.Info($"{action} - {ToString()}");
    }
}