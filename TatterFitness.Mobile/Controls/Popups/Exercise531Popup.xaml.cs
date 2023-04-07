using CommunityToolkit.Maui.Views;
using TatterFitness.Mobile.Models;
using TatterFitness.Mobile.Models.Popups;
using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.Controls.Popups;

public partial class Exercise531Popup : Popup
{
    public string Title { get; set; }
    public int TrainingMax { get; set; }
    public int SelectedWeekIndex { get; set; }
    public List<int> WeekNumbers { get; set; }

    private readonly Exercise531PopupMetadata metadata;
    private readonly string storageKey;
    private readonly Dictionary<int, List<FTOSetCalculations>> ftoSetsCalculations = new();
    private const int BENCH_PRESS_ID = 1;

    private int WeekNumber
    {
        get { return SelectedWeekIndex + 1; }
    }

    public Exercise531Popup(Exercise531PopupMetadata metadata)
    {
        this.metadata = metadata;
        PopulateFTOSetCalculations();

        storageKey = $"531_{metadata.ExerciseId}_TrainingMax";
        Title = metadata.ExerciseName;
        SelectedWeekIndex = 0;
        TrainingMax = Preferences.Get(storageKey, 100);
        WeekNumbers = new List<int> { 1, 2, 3, 4 };

        InitializeComponent();

        BindingContext = this;
    }

    private void CancelButtonPressed(object sender, EventArgs e) => Close(null);

    private void CalculateButtonPressed(object sender, EventArgs e)
    {
        var sets = new List<WorkoutExerciseSet>();
        sets.AddRange(Get531Sets());
        sets.AddRange(GetBBBSets());
        var setNumber = 1;
        foreach (var set in sets)
        {
            set.SetNumber = setNumber++;
            set.ExerciseType = metadata.ExerciseType;
        }

        var results = new FTOResults
        {
            TrainingMax = TrainingMax,
            WeekNumber = WeekNumbers[SelectedWeekIndex],
            ExerciseSets = sets
        };
        Close(results);
    }

    private IEnumerable<WorkoutExerciseSet> Get531Sets()
    {
        var setCalcs = ftoSetsCalculations[WeekNumber];
        var sets = new List<WorkoutExerciseSet>();
        foreach (var setCalc in setCalcs)
        {
            sets.Add(new WorkoutExerciseSet
            {
                RepCount = setCalc.repCount,
                Weight = CalculateWeight(setCalc.tmPercent)
            });
        }

        return sets;
    }

    private IEnumerable<WorkoutExerciseSet> GetBBBSets()
    {
        if (WeekNumber == 4)
        {
            return Enumerable.Empty<WorkoutExerciseSet>();
        }

        var sets = new List<WorkoutExerciseSet>();
        var numSets = metadata.ExerciseId == BENCH_PRESS_ID ? 5 : 3;
        while (sets.Count < numSets)
        {
            sets.Add(new WorkoutExerciseSet
            {
                RepCount = 10,
                Weight = CalculateWeight(.50)
            });
        }

        return sets;
    }

    private int CalculateWeight(double pct)
    {
        Preferences.Set(storageKey, TrainingMax);
        var weight = (TrainingMax * pct);
        var fiveLbsMultiplier = (int)Math.Round(weight / 5);
        weight = fiveLbsMultiplier * 5;

        return (int)weight;
    }

    private void PopulateFTOSetCalculations()
    {
        ftoSetsCalculations.Add(1, new List<FTOSetCalculations>
        {
            new FTOSetCalculations { repCount = 5, tmPercent = .65 },
            new FTOSetCalculations { repCount = 5, tmPercent = .75 },
            new FTOSetCalculations { repCount = 5, tmPercent = .85 },
        });

        ftoSetsCalculations.Add(2, new List<FTOSetCalculations>
        {
            new FTOSetCalculations { repCount = 3, tmPercent = .70 },
            new FTOSetCalculations { repCount = 3, tmPercent = .80 },
            new FTOSetCalculations { repCount = 3, tmPercent = .90 },
        });

        ftoSetsCalculations.Add(3, new List<FTOSetCalculations>
        {
            new FTOSetCalculations { repCount = 5, tmPercent = .75 },
            new FTOSetCalculations { repCount = 3, tmPercent = .85 },
            new FTOSetCalculations { repCount = 1, tmPercent = .95 },
        });

        ftoSetsCalculations.Add(4, new List<FTOSetCalculations>
        {
            new FTOSetCalculations { repCount = 5, tmPercent = .40 },
            new FTOSetCalculations { repCount = 5, tmPercent = .50 },
            new FTOSetCalculations { repCount = 5, tmPercent = .60 },
        });
    }
}