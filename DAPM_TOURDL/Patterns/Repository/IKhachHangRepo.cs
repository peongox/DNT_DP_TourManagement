using DAPM_TOURDL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAPM_TOURDL.Patterns.Repository
{
    internal interface IKhachHangRepo :IDisposable
    {
        IEnumerable<KHACHHANG> GetAllKHACHHANGs();
        IEnumerable<NHANVIEN> GetAllNhanViens();
        KHACHHANG GetKHACHHANGById(int? id);
        IEnumerable<KHACHHANG> SearchKHACHHANGs(string searchString);
        void CreateKHACHHANG(KHACHHANG kHACHHANG);
        void UpdateKHACHHANG(KHACHHANG kHACHHANG);
        void DeleteKHACHHANG(int id);
    }
}
