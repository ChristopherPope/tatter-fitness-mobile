namespace TatterFitness.Mobile.Controls;

public partial class TotalEffort : Grid
{
    public readonly static BindableProperty RWVolumeProperty = BindableProperty.Create(
        propertyName: nameof(RWVolume),
        returnType: typeof(string),
        declaringType: typeof(TotalEffort),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public readonly static BindableProperty RORepsProperty = BindableProperty.Create(
        propertyName: nameof(ROReps),
        returnType: typeof(string),
        declaringType: typeof(TotalEffort),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public readonly static BindableProperty DWDurationProperty = BindableProperty.Create(
        propertyName: nameof(DWDuration),
        returnType: typeof(string),
        declaringType: typeof(TotalEffort),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public readonly static BindableProperty DWVolumeProperty = BindableProperty.Create(
        propertyName: nameof(DWVolume),
        returnType: typeof(string),
        declaringType: typeof(TotalEffort),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public readonly static BindableProperty CDurationProperty = BindableProperty.Create(
        propertyName: nameof(CDuration),
        returnType: typeof(string),
        declaringType: typeof(TotalEffort),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public readonly static BindableProperty CMilesProperty = BindableProperty.Create(
        propertyName: nameof(CMiles),
        returnType: typeof(string),
        declaringType: typeof(TotalEffort),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public readonly static BindableProperty AverageBpmProperty = BindableProperty.Create(
        propertyName: nameof(AverageBpm),
        returnType: typeof(string),
        declaringType: typeof(TotalEffort),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public string AverageBpm
    {
        get { return (string)GetValue(AverageBpmProperty); }
        set { SetValue(AverageBpmProperty, value); }
    }

    public string RWVolume
    {
        get { return (string)GetValue(RWVolumeProperty); }
        set { SetValue(RWVolumeProperty, value); }
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

    public TotalEffort()
    {
        InitializeComponent();
    }

    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName == RWVolumeProperty.PropertyName)
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
        }
    }
}
