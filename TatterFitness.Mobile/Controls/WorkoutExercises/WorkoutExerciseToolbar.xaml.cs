using CommunityToolkit.Mvvm.Input;

namespace TatterFitness.Mobile.Controls.WorkoutExercises;

public partial class WorkoutExerciseToolbar : StackLayout
{
    public readonly static BindableProperty Is531ButtonVisibleProperty = BindableProperty.Create(
        propertyName: nameof(Is531ButtonVisible),
        returnType: typeof(bool),
        declaringType: typeof(WorkoutExerciseToolbar),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public bool Is531ButtonVisible
    {
        get => (bool)GetValue(Is531ButtonVisibleProperty);
        set => SetValue(Is531ButtonVisibleProperty, value);
    }


    public readonly static BindableProperty IsCompleteSetButtonVisibleProperty = BindableProperty.Create(
        propertyName: nameof(IsCompleteSetButtonVisible),
        returnType: typeof(bool),
        declaringType: typeof(WorkoutExerciseToolbar),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public bool IsCompleteSetButtonVisible
    {
        get => (bool)GetValue(IsCompleteSetButtonVisibleProperty);
        set => SetValue(IsCompleteSetButtonVisibleProperty, value);
    }


    public readonly static BindableProperty AddSetCommandProperty = BindableProperty.Create(
        propertyName: nameof(AddSetCommand),
        returnType: typeof(IAsyncRelayCommand),
        declaringType: typeof(WorkoutExerciseToolbar),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public IAsyncRelayCommand AddSetCommand
    {
        get => (IAsyncRelayCommand)GetValue(AddSetCommandProperty);
        set => SetValue(AddSetCommandProperty, value);
    }


    public readonly static BindableProperty CompleteSetCommandProperty = BindableProperty.Create(
        propertyName: nameof(CompleteSetCommand),
        returnType: typeof(IAsyncRelayCommand),
        declaringType: typeof(WorkoutExerciseToolbar),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public IAsyncRelayCommand CompleteSetCommand
    {
        get => (IAsyncRelayCommand)GetValue(CompleteSetCommandProperty);
        set => SetValue(CompleteSetCommandProperty, value);
    }


    public readonly static BindableProperty EditNotesCommandProperty = BindableProperty.Create(
        propertyName: nameof(EditNotesCommand),
        returnType: typeof(IAsyncRelayCommand),
        declaringType: typeof(WorkoutExerciseToolbar),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public IAsyncRelayCommand EditNotesCommand
    {
        get => (IAsyncRelayCommand)GetValue(EditNotesCommandProperty);
        set => SetValue(EditNotesCommandProperty, value);
    }


    public readonly static BindableProperty SelectModsCommandProperty = BindableProperty.Create(
        propertyName: nameof(SelectModsCommand),
        returnType: typeof(IAsyncRelayCommand),
        declaringType: typeof(WorkoutExerciseToolbar),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public IAsyncRelayCommand SelectModsCommand
    {
        get => (IAsyncRelayCommand)GetValue(SelectModsCommandProperty);
        set => SetValue(SelectModsCommandProperty, value);
    }


    public readonly static BindableProperty DeleteSetCommandProperty = BindableProperty.Create(
        propertyName: nameof(DeleteSetCommand),
        returnType: typeof(IAsyncRelayCommand),
        declaringType: typeof(WorkoutExerciseToolbar),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public IAsyncRelayCommand DeleteSetCommand
    {
        get => (IAsyncRelayCommand)GetValue(DeleteSetCommandProperty);
        set => SetValue(DeleteSetCommandProperty, value);
    }


    public readonly static BindableProperty ViewHistoryCommandProperty = BindableProperty.Create(
        propertyName: nameof(ViewHistoryCommand),
        returnType: typeof(IAsyncRelayCommand),
        declaringType: typeof(WorkoutExerciseToolbar),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public IAsyncRelayCommand ViewHistoryCommand
    {
        get => (IAsyncRelayCommand)GetValue(ViewHistoryCommandProperty);
        set => SetValue(ViewHistoryCommandProperty, value);
    }


    public readonly static BindableProperty Calculate531CommandProperty = BindableProperty.Create(
        propertyName: nameof(Calculate531Command),
        returnType: typeof(IAsyncRelayCommand),
        declaringType: typeof(WorkoutExerciseToolbar),
        defaultValue: null,
        defaultBindingMode: BindingMode.OneWay);

    public IAsyncRelayCommand Calculate531Command
    {
        get => (IAsyncRelayCommand)GetValue(Calculate531CommandProperty);
        set => SetValue(Calculate531CommandProperty, value);
    }

    public WorkoutExerciseToolbar()
    {
        InitializeComponent();
    }

    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        if (propertyName == IsCompleteSetButtonVisibleProperty.PropertyName)
        {
            completeSetButton.IsVisible = IsCompleteSetButtonVisible;
        }
        else if (propertyName == Is531ButtonVisibleProperty.PropertyName)
        {
            calculate531Button.IsVisible = Is531ButtonVisible;
        }
    }

    private void OnAddSet(object sender, EventArgs e)
    {
        AddSetCommand?.Execute(this);
    }

    private void OnDeleteSet(object sender, EventArgs e)
    {
        DeleteSetCommand?.Execute(this);
    }

    private void OnViewHistory(object sender, EventArgs e)
    {
        ViewHistoryCommand?.Execute(this);
    }

    private void OnSelectMods(object sender, EventArgs e)
    {
        SelectModsCommand?.Execute(this);
    }

    private void OnEditNotes(object sender, EventArgs e)
    {
        EditNotesCommand?.Execute(this);
    }

    private void OnCompleteSet(object sender, EventArgs e)
    {
        CompleteSetCommand?.Execute(this);
    }

    private void OnCalculate531(object sender, EventArgs e)
    {
        Calculate531Command?.Execute(this);
    }
}