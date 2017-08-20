using MIC.Common.Date.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIC.Common.Date.Services
{
    /// <summary>
    /// 東京証券取引所の休業日をチェックします。
    /// </summary>
    public class TolyoStockExchangeHolidayCheckService : IHolidayCheckService
    {
        public bool IsHoliday(DateTime date)
        {
            //土日・祝日ならばfalse
            //WEEKDAY = 月～日 != 平日
            var dayInfo = HolidayChecker.Holiday(date);
            if (dayInfo.holiday != HolidayChecker.HolidayInfo.HOLIDAY.WEEKDAY ||
               (dayInfo.week == DayOfWeek.Saturday || dayInfo.week == DayOfWeek.Sunday) ||
               (dayInfo.name == "１２月３１日" || dayInfo.name == "１月２日" || dayInfo.name == "１月３日"))
            {
                return true;
            }
            return false;
        }
    }
}
