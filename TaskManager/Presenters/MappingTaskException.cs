﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Presenters
{
    public class MappingTaskException : Exception
    {
        public MappingTaskException() { }
        public MappingTaskException(string message, Exception ex) : base(message, ex) { }
    }
}
