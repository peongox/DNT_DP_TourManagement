﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAPM_TOURDL.Patterns.Observer
{
    public interface IObserver
    {
        void Update(string message);
    }
}