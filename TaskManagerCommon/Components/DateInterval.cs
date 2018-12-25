using System;

namespace TaskManagerCommon.Components
{
    public struct DateInterval
    {
        public DateTime BeginDate;
        public DateTime EndDate;

        public DateInterval(DateTime day) : this(day, day) { }

        public DateInterval(DateTime beginDate, DateTime endDate)
        {
            BeginDate = beginDate;
            EndDate = endDate;
        }
    }
}
