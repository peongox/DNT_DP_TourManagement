using DAPM_TOURDL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAPM_TOURDL
{
    internal class StrategyMethod
    {
        private ITourSearch _strategy;

        public StrategyMethod() {}
        public void SetStrategy(ITourSearch _searchStrategy)
        {
            _strategy = _searchStrategy;
        }
        public List<SPTOUR> FilterTours(List<SPTOUR> tours, string name, int? to, int? from)
        {
            return _strategy.FilterTours(tours, name, to, from);
        }
    }
}