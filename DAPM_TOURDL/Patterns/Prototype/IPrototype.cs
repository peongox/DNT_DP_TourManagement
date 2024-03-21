using DAPM_TOURDL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAPM_TOURDL.Patterns.Prototype
{
    public interface IPrototype
    {
        IPrototype Clone();
    }
}
