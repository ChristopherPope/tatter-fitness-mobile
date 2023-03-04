using Flurl.Http.Configuration;
using TatterFitness.Mobile.Interfaces.Services;
using TatterFitness.Mobile.Interfaces.Services.API;
using TatterFitness.Mobile.Interfaces.Services.ContextMenu;
using TatterFitness.Mobile.Interfaces.Services.SelectorModals;
using TatterFitness.Mobile.Mapping;
using TatterFitness.Mobile.Models.Popups;
using TatterFitness.Mobile.Services;
using TatterFitness.Mobile.Services.API;
using TatterFitness.Mobile.Services.ContextMenu;
using TatterFitness.Mobile.Services.SelectorModals;
using TatterFitness.Mobile.ViewModels;
using TatterFitness.Mobile.ViewModels.History.EventCalendar;
using TatterFitness.Mobile.ViewModels.History.ExHistory;
using TatterFitness.Mobile.ViewModels.Home;
using TatterFitness.Mobile.ViewModels.Routines;
using TatterFitness.Mobile.ViewModels.Select;
using TatterFitness.Mobile.ViewModels.Workouts;
using TatterFitness.Mobile.ViewModels.Workouts.WorkoutExercises;
using TatterFitness.Mobile.ViewModels.WorkoutSnapshot;
using TatterFitness.Mobile.Views;
using TatterFitness.Mobile.Views.History;
using TatterFitness.Mobile.Views.Routines;
using TatterFitness.Mobile.Views.Select;
using TatterFitness.Mobile.Views.Workouts;
using TatterFitness.Mobile.Views.Workouts.WorkoutExercises;

namespace TatterFitness.Mobile.Extensions
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
            builder.Services.AddTransient<WorkoutCalendarView>();
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
            builder.Services.AddTransient<WorkoutCalendarViewModel>();
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
