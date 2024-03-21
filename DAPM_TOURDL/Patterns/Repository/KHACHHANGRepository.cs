using DAPM_TOURDL.Controllers;
using DAPM_TOURDL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DAPM_TOURDL.Patterns.Repository
{
    public class KHACHHANGRepository : IKhachHangRepo
    {
        private TourDLEntities db;
        public KHACHHANGRepository(TourDLEntities db)
        {
            this.db = db;
        }
        public IEnumerable<KHACHHANG> GetAllKHACHHANGs()
        {
            return db.KHACHHANGs.ToList();
        }
        public IEnumerable<NHANVIEN> GetAllNHANVIENs()
        {
            return db.NHANVIENs.ToList();
        }
        public KHACHHANG GetKHACHHANGById(int? id)
        {
            return db.KHACHHANGs.Find(id);
        }

        public IEnumerable<KHACHHANG> SearchKHACHHANGs(string searchString)
        {
            return db.KHACHHANGs.Where(s => s.HoTen_KH.Contains(searchString) || s.Mail_KH.Contains(searchString)).ToList();
        }

        public void CreateKHACHHANG(KHACHHANG kHACHHANG)
        {
            db.KHACHHANGs.Add(kHACHHANG);
            db.SaveChanges();
        }

        public void UpdateKHACHHANG(KHACHHANG kHACHHANG)
        {
            db.Entry(kHACHHANG).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteKHACHHANG(int id)
        {
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            db.KHACHHANGs.Remove(kHACHHANG);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

       
    }
}