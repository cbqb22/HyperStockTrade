using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIC.Common.Date.Services.Interfaces
{
    public interface IHolidayCheckService
    {
        bool IsHoliday(DateTime date);
    }
}
