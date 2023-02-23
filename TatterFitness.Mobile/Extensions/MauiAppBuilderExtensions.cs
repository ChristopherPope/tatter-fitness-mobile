using Flurl.Http.Configuration;
using TatterFitness.App.Interfaces.Services;
using TatterFitness.App.Interfaces.Services.API;
using TatterFitness.App.Interfaces.Services.ContextMenu;
using TatterFitness.App.Interfaces.Services.SelectorModals;
using TatterFitness.App.Mapping;
using TatterFitness.App.Models.Popups;
using TatterFitness.App.Services;
using TatterFitness.App.Services.API;
using TatterFitness.App.Services.ContextMenu;
using TatterFitness.App.Services.SelectorModals;
using TatterFitness.App.ViewModels;
using TatterFitness.App.ViewModels.History.EventCalendar;
using TatterFitness.App.ViewModels.History.ExHistory;
using TatterFitness.App.ViewModels.Home;
using TatterFitness.App.ViewModels.Routines;
using TatterFitness.App.ViewModels.Select;
using TatterFitness.App.ViewModels.Workouts;
using TatterFitness.App.ViewModels.Workouts.WorkoutExercises;
using TatterFitness.App.ViewModels.WorkoutSnapshot;
using TatterFitness.App.Views;
using TatterFitness.App.Views.History;
using TatterFitness.App.Views.Routines;
using TatterFitness.App.Views.Select;
using TatterFitness.App.Views.Workouts;
using TatterFitness.App.Views.Workouts.WorkoutExercises;
using TatterFitness.Mobile.ViewModels;

namespace TatterFitness.App.Extensions
{
    internal static class MauiAppBuilderExtensions
    {
        public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<IRoutinesApiService, RoutinesApiService>();
            builder.Services.AddTransient<IRoutineExercisesApiService, RoutineExercisesApiService>();
            builder.Services.AddTransient<IExercisesApiService, ExercisesApiService>();
            builder.Services.AddTransient<IExerciseModifiersApiService, ExerciseModifiersApiService>();
            builder.Services.AddTransient<ILoggingService, LoggingService>();
            builder.Services.AddTransient<IWorkoutsApiService, WorkoutsApiService>();
            builder.Services.AddTransient<IWorkoutExercisesApiService, WorkoutExercisesApiService>();
            builder.Services.AddTransient<IWorkoutExerciseModifiersApiService, WorkoutExerciseModifiersApiService>();
            builder.Services.AddTransient<IWorkoutExerciseSetsApiService, WorkoutExerciseSetsApiService>();
            builder.Services.AddTransient<IModsSelectorModal, ModsSelectorModal>();
            builder.Services.AddTransient<IExercisesSelectorModal, ExercisesSelectorModal>();
            builder.Services.AddTransient<IWorkoutExerciseContextMenuService, WorkoutExerciseContextMenuService>();
            builder.Services.AddTransient<IRoutineContextMenuService, RoutineContextMenuService>();
            builder.Services.AddTransient<IHistoriesApiService, HistoriesApiService>();
            builder.Services.AddSingleton<IFlurlClientFactory, PerBaseUrlFlurlClientFactory>();
            builder.Services.AddSingleton<IRoutineExerciseContextMenuService, RoutineExerciseContextMenuService>();
            builder.Services.AddSingleton<IWorkoutSnapshotContextMenuService, WorkoutSnapshotContextMenuService>();

            builder.Services.AddAutoMapper(typeof(ModelMapping));

            return builder;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<ErrorView>();
            builder.Services.AddTransient<HomeView>();
            builder.Services.AddTransient<ShowRoutinesView>();
            builder.Services.AddTransient<EditRoutineView>();
            builder.Services.AddTransient<SelectModsView>();
            builder.Services.AddTransient<WorkoutView>();
            builder.Services.AddTransient<SelectExercisesView>();
            builder.Services.AddTransient<ExerciseHistoryView>();
            builder.Services.AddTransient<WorkoutEventView>();
            builder.Services.AddTransient<WorkoutSnapshotView>();

            builder.Services.AddTransient<CardioWorkoutExerciseView>();
            builder.Services.AddTransient<RepsAndWeightWorkoutExerciseView>();
            builder.Services.AddTransient<RepsOnlyWorkoutExerciseView>();
            builder.Services.AddTransient<DurationAndWeightWorkoutExerciseView>();


            return builder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<ShowRoutinesViewModel>();
            builder.Services.AddTransient<EditRoutineViewModel>();
            builder.Services.AddTransient<ErrorViewModel>();
            builder.Services.AddTransient<SelectModsViewModel>();
            builder.Services.AddTransient<SelectExercisesViewModel>();
            builder.Services.AddTransient<WorkoutViewModel>();
            builder.Services.AddTransient<ExerciseHistoryViewModel>();
            builder.Services.AddTransient<WorkoutEventViewModel>();
            builder.Services.AddTransient<WorkoutSnapshotViewModel>();
            builder.Services.AddTransient<TotalEffortViewModel>();

            builder.Services.AddTransient<CardioSetViewModel>();
            builder.Services.AddTransient<RepsAndWeightSetViewModel>();
            builder.Services.AddTransient<RepsOnlySetViewModel>();
            builder.Services.AddTransient<DurationAndWeightSetViewModel>();

            builder.Services.AddTransient<CardioWorkoutExerciseViewModel>();
            builder.Services.AddTransient<RepsOnlyWorkoutExerciseViewModel>();
            builder.Services.AddTransient<RepsAndWeightWorkoutExerciseViewModel>();
            builder.Services.AddTransient<DurationAndWeightWorkoutExerciseViewModel>();


            return builder;
        }

        public static MauiAppBuilder RegisterEssentials(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<PopupSizeConstants>();
            builder.Services.AddSingleton<IDeviceDisplay>(DeviceDisplay.Current);

            return builder;
        }
    }
}
