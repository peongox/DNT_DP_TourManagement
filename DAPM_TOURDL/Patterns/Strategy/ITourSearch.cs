using DAPM_TOURDL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAPM_TOURDL
{
    internal interface ITourSearch
    {
        List<SPTOUR> FilterTours(List<SPTOUR> tours, string name, int? to, int? from);
    }
}
