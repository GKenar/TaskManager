using System;
using TaskManagerCommon.Components;

namespace TaskManagerView.Components
{
    public class SortEventArgs : EventArgs
    {
        public SortType Sort { get; set; }

        public SortEventArgs(SortType sort)
        {
            Sort = sort;
        }
    }
}
