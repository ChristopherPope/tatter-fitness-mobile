using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NodaTime;
using System.Collections.ObjectModel;
using System.Globalization;
using TatterFitness.App.Interfaces.Services;

namespace TatterFitness.App.ViewModels.History.EventCalendar
{
    public abstract partial class EventCalendarViewModelBase : ViewModelBase
    {
        private readonly List<YearMonth> yearMonths = new();

        private int CurrentYear { get; set; }
        private YearMonth CurrentMonth { get; set; }
        private IOrderedEnumerable<YearMonth> MonthsOfCurrentYear => yearMonths.Where(ym => ym.Year == CurrentYear).OrderBy(ym => ym.Month);

        [ObservableProperty]
        private ObservableCollection<int> years = new();

        [ObservableProperty]
        private ObservableCollection<string> monthNames = new();

        #region Day Properties
        [ObservableProperty]
        private EventCalendarDayViewModel day1 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day2 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day3 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day4 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day5 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day6 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day7 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day8 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day9 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day10 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day11 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day12 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day13 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day14 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day15 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day16 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day17 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day18 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day19 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day20 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day21 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day22 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day23 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day24 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day25 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day26 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day27 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day28 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day29 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day30 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day31 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day32 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day33 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day34 = new();
        [ObservableProperty]
        private EventCalendarDayViewModel day35 = new();
        #endregion

        public EventCalendarViewModelBase(ILoggingService logger) : base(logger)
        {
        }

        protected abstract Task ProcessDayClicked(int eventId);
        protected abstract Task<Dictionary<LocalDate, int>> LoadEvents(DateInterval interval);
        protected abstract Task<DateInterval> GetAllEventsInterval();

        protected override async Task PerformLoadViewData()
        {
            if (yearMonths.Any())
            {
                return;
            }

            IsBusy = true;
            var allEventsInterval = await GetAllEventsInterval();
            LoadYearMonths(allEventsInterval);
            PopulateListOfYears();
            IsBusy = false;
        }

        private void PopulateListOfYears()
        {
            var years = yearMonths.Select(ym => ym.Year).Distinct().OrderBy(y => y);
            foreach (var year in years)
            {
                Years.Add(year);
            }
        }

        private void LoadYearMonths(DateInterval allEventsInterval)
        {
            var month = allEventsInterval.End.ToYearMonth();
            var startMonth = allEventsInterval.Start.ToYearMonth();
            while (month >= startMonth)
            {
                yearMonths.Add(month);
                month = month.PlusMonths(-1);
            }
        }

        private DateInterval GetIntervalForMonth(YearMonth forMonth)
        {
            var start = forMonth.OnDayOfMonth(1);
            while (start.DayOfWeek != IsoDayOfWeek.Sunday)
            {
                start = start.PlusDays(-1);
            }

            var end = forMonth.OnDayOfMonth(1).With(DateAdjusters.EndOfMonth);
            while (end.DayOfWeek != IsoDayOfWeek.Saturday)
            {
                end = end.PlusDays(1);
            }

            return new DateInterval(start, end);
        }

        [RelayCommand]
        private void YearChanged(int year)
        {
            try
            {
                CurrentYear = year;
                monthNames.Clear();
                foreach (var m in MonthsOfCurrentYear)
                {
                    monthNames.Add(m.ToString("MMMM", CultureInfo.CurrentCulture));
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        [RelayCommand]
        private async Task MonthChanged(int monthIdx)
        {
            try
            {
                CurrentMonth = MonthsOfCurrentYear.ElementAt(monthIdx);
                var dateRange = GetIntervalForMonth(CurrentMonth);
                var dateToEventId = await LoadEvents(dateRange);

                var dayIdx = 0;
                #region Populate Day VMs
                PopulateDayVm(Day1, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day2, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day3, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day4, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day5, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day6, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day7, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day8, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day9, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day10, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day11, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day12, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day13, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day14, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day15, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day16, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day17, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day18, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day19, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day20, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day21, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day22, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day23, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day24, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day25, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day26, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day27, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day28, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day29, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day30, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day31, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day32, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day33, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day34, dateRange.ElementAt(dayIdx++), dateToEventId);
                PopulateDayVm(Day35, dateRange.ElementAt(dayIdx++), dateToEventId);
                #endregion
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        private void PopulateDayVm(EventCalendarDayViewModel dayVm, LocalDate forDay, Dictionary<LocalDate, int> dateToEventId)
        {
            dayVm.DayOfMonth = forDay.Day;

            dateToEventId.TryGetValue(forDay, out int eventId);
            dayVm.EventId = eventId;
            dayVm.HasEvent = eventId > 0;

            dayVm.IsSelectedMonth = forDay.ToYearMonth() == CurrentMonth;
        }

        [RelayCommand]
        private async Task DayClicked(int eventId)
        {
            try
            {
                if (eventId == 0)
                {
                    return;
                }

                await ProcessDayClicked(eventId);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }
    }
}
