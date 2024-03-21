using DAPM_TOURDL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAPM_TOURDL
{
    public class AllStrategy : ITourSearch
    {
        public List<SPTOUR> FilterTours(List<SPTOUR> tours, string name, int? to, int? from)
        {
                if (!string.IsNullOrEmpty(name))
                {
                    tours = tours.Where(x => x.TenSPTour.Contains(name)).ToList();
                }
                if (to != null)
                {
                    tours = tours.Where(x => x.GiaNguoiLon >= to).ToList();
                }
                if (from != null)
                {
                    tours = tours.Where(x => x.GiaNguoiLon <= from).ToList();
                }
                return tours;
        }
    }
}