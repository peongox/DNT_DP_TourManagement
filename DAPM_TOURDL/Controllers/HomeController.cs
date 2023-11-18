﻿using DAPM_TOURDL.Models;
using PagedList;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace DAPM_TOURDL.Controllers
{
    public class HomeController : Controller
    {
        private TourDLEntities db = new TourDLEntities();

        public ActionResult Index()
        {
            return View(db.TOURs.ToList());
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult HomePage()
        {
            return View(db.TOURs.ToList());
        }

        public ActionResult DanhMucSanPham(string id)
        {
            var data = db.TOURs.ToList();
            return View(data);
        }

        public ActionResult ChiTietTour(string id)
        {
            var data = db.SPTOURs.Where(s => s.ID_SPTour == id);
            return View(data);
        }
        public ActionResult DangKy()
        {

            return View();
        }
        [HttpPost]
        public ActionResult DangKy(KHACHHANG khachhang)
        {
            DateTime ngayHienTai = DateTime.Now;
            DateTime ngaySinh18 = ngayHienTai.AddYears(-15);
            if (db.KHACHHANGs.Any(x => x.Mail_KH == khachhang.Mail_KH))
            {
                ViewBag.Notification = "Tài khoản đã tồn tại";
            }
            else if (string.IsNullOrEmpty(khachhang.Mail_KH) || khachhang.GioiTinh_KH == null || khachhang.NgaySinh_KH == null || string.IsNullOrEmpty(khachhang.MatKhau) || string.IsNullOrEmpty(khachhang.CCCD) || string.IsNullOrEmpty(khachhang.SDT_KH) || string.IsNullOrEmpty(khachhang.HoTen_KH))
            {
                ViewBag.Notification = "Vui lòng nhập đủ thông tin nhé ! Xin cảm ơn";
            }
            else if (!(khachhang.GioiTinh_KH == "Nam" || khachhang.GioiTinh_KH == "Nữ"))
            {
                ViewBag.Notification = "Giới tính chỉ có thể là 'Nam' hoặc 'Nữ'";
            }
            else if (khachhang.NgaySinh_KH > ngaySinh18)
            {
                ViewBag.Notification = "Yêu cầu lớn hơn 15+";
            }
            else if (khachhang.CCCD.Length != 12 || !Regex.IsMatch(khachhang.CCCD, @"^[0-9]+$"))
            {
                ViewBag.Notification = "Căn Cước Công Dân vui lòng nhập đủ 12 số và không bao gồm chữ,kí tự";
            }
            else if (khachhang.SDT_KH.Length != 10 || !Regex.IsMatch(khachhang.SDT_KH, @"^[0-9]+$"))
            {
                ViewBag.Notification = "Số điện thoại phải có 10 số và không bao gồm chữ,kí tự";
            }
            else if(db.KHACHHANGs.Any(x=>x.CCCD == khachhang.CCCD))
            {
                ViewBag.Notification = "Căn Cước Công Dân này đã tồn tại";
            }
            else if(db.KHACHHANGs.Any(x=>x.SDT_KH == khachhang.SDT_KH))
            {
                ViewBag.Notification = "Số Điện Thoại này đã tồn tại";
            }
            else if (!MatKhauManh(khachhang.MatKhau))
            {
                ViewBag.Notification = "Mật khẩu phải có ít nhất 8 ký tự, bao gồm ít nhất 1 số, 1 chữ thường, 1 chữ hoa, 1 ký tự đặc biệt";
            }
            else
            {
                db.KHACHHANGs.Add(khachhang);
                db.SaveChanges();
                Session["IDUser"] = khachhang.ID_KH.ToString();
                Session["EmailUserSS"] = khachhang.Mail_KH.ToString();
                Session["UsernameSS"] = khachhang.HoTen_KH.ToString();
                Session["SDT"] = khachhang.SDT_KH.ToString();
                return RedirectToAction("DangNhap", "Home");
            }
            return View();
        }
        private bool MatKhauManh(string password)
        {
            return password.Length >= 8 &&
                Regex.IsMatch(password, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+{}\[\]:;<>,.?~\\-]).*$");
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(KHACHHANG khachhang)
        {

            var kiemTraDangNhap = db.KHACHHANGs.Where(x => x.Mail_KH.Equals(khachhang.Mail_KH) && x.MatKhau.Equals(khachhang.MatKhau)).FirstOrDefault();

            if (kiemTraDangNhap != null)
            {
                Session["UsernameSS"] = kiemTraDangNhap.HoTen_KH.ToString();
                Session["IDUser"] = kiemTraDangNhap.ID_KH;
                /*Session["EmailUserSS"] = kiemTraDangNhap.Mail_KH.ToString();
                Session["GioiTinh"] = kiemTraDangNhap.GioiTinh_KH;
                Session["SDT"] = kiemTraDangNhap.SDT_KH.ToString();
                Session["CCCD"] = kiemTraDangNhap.CCCD.ToString();
                ViewBag.idkh = kiemTraDangNhap.ID_KH;*/
                // Kiểm tra xem có thông tin tour trong Session không
                if (Session["TourInfo"] != null)
                {
                    var tour = (SPTOUR)Session["TourInfo"];
                    Session.Remove("TourInfo");

                    return RedirectToAction("DatTour", new { id = tour.ID_SPTour });
                }

                return RedirectToAction("HomePage", "Home", new { id = kiemTraDangNhap.ID_KH });
            }
            else
            {
                ViewBag.Notification = "Tài khoản và mật khẩu không đúng";
            }

            return View();
        }
        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("HomePage", "Home");
        }
        public ActionResult ThongTinCaNhan(int id)
        {
            var data = db.KHACHHANGs.Find(id);
            return View(data);
        }
        [HttpGet]
        public ActionResult ChinhSuaThongTinCaNhan(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG khachhang = db.KHACHHANGs.Find(id);
            if (khachhang == null)
            {
                return HttpNotFound();
            }
            return View(khachhang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChinhSuaThongTinCaNhan([Bind(Include = "ID_KH,HoTen_KH,GioiTinh_KH,NgaySinh_KH,MatKhau,CCCD,SDT_KH,Mail_KH,Diem")] KHACHHANG khachhang)
        {
            DateTime ngayTruocKhiDu16Tuoi = DateTime.Now.AddYears(-16);
            if (!(khachhang.GioiTinh_KH == "Nam" || khachhang.GioiTinh_KH == "Nữ"))
            {
                ViewBag.Notification = "Giới tính chỉ có thể là 'Nam' hoặc 'Nữ'";
            }
            else if (khachhang.NgaySinh_KH > ngayTruocKhiDu16Tuoi)
            {
                ViewBag.Notification = "Ngày sinh phải đủ 16 tuổi";
            }
            else if (khachhang.CCCD.Length != 12 || !Regex.IsMatch(khachhang.CCCD, @"^[0-9]+$"))
            {
                ViewBag.Notification = "Căn Cước Công Dân vui lòng nhập đủ 12 số và không bao gồm chữ,kí tự";
            }
            else if (khachhang.SDT_KH.Length != 10 || !Regex.IsMatch(khachhang.SDT_KH, @"^[0-9]+$"))
            {
                ViewBag.Notification = "Số điện thoại phải có 10 số và không bao gồm chữ,kí tự";
            }
            else if (db.KHACHHANGs.Any(x => x.CCCD == khachhang.CCCD && x.ID_KH != khachhang.ID_KH))
            {
                ViewBag.Notification = "Căn cước công dân này đã được đăng ký";
            }
            else if (db.KHACHHANGs.Any(x => x.SDT_KH == khachhang.SDT_KH && x.ID_KH != khachhang.ID_KH))
            {
                ViewBag.Notification = "Số điện thoại này đã có người sử dụng";
            }
            else if (khachhang.HoTen_KH.Length > 32)
            {
                ViewBag.Notification = "Tên không được quá 64 ký tự";
            }
            else if (db.KHACHHANGs.Any(x => x.MatKhau != khachhang.MatKhau && x.ID_KH == khachhang.ID_KH))
            {
                ViewBag.Notification = "Mật khẩu xác nhận không chính xác";
            }
            else if (ModelState.IsValid)
            {
                db.Entry(khachhang).State = EntityState.Modified;
                db.SaveChanges();
                //Session["UsernameSS"] = khachhang.HoTen_KH.ToString();
                return RedirectToAction("ThongTinCaNhan", "Home", new { id = khachhang.ID_KH });
            }
            return View(khachhang);
        }
        public ActionResult LichSuDatTour(int id)
        {
            var data = db.HOADONs.Where(t => t.ID_KH == id).ToList();
            return View(data);
        }
        public ActionResult HuyTourDaDat(int? id)
        {
            if (Request.IsAjaxRequest())
            {
                Session["idhoadon"] = id;
                return PartialView();
            }
            else
            {
                return PartialView("Error");
            }
        }
        public ActionResult HuyTour(int id)
        {
            HOADON data = db.HOADONs.Find(id);
            db.HOADONs.Remove(data);
            db.SaveChanges();
            return RedirectToAction("LichSuDatTour", new { id = Session["IDUser"] });
        }
        public ActionResult ThanhToanMomo(int id)
        {
            var data = db.HOADONs.Find(id);
            return PartialView(data);
        }
        [HttpGet]
        public ActionResult DatTour(string id)
        {
            var data = db.SPTOURs.Find(id);
            Session["TourInfo"] = data;
            return View(data);
        }
        [HttpPost]
        public ActionResult DatTour(FormCollection form, string id)
        {
            HOADON hOADON = new HOADON();
            var sptour = db.SPTOURs.FirstOrDefault(s => s.ID_SPTour == id);
            if (Session["IDUser"] == null)
            {
                return View(sptour);
            }
            else if (sptour.SoNguoi <= 0)
            {
                ViewBag.Noti = "Hết số lượng chỗ ngồi";
                return View(sptour);
            }
            else
            {
                hOADON.ID_SPTour = form["idsptour"];
                hOADON.NgayDat = DateTime.Now;
                hOADON.TinhTrang = "Chưa TT";
                hOADON.ID_KH = int.Parse(form["idkh"]);

                hOADON.SLNguoiLon = int.Parse(form["songuoilon"]);
                hOADON.SLTreEm = int.Parse(form["sotreem"]);

                int slnguoilon = int.Parse(form["songuoilon"]);
                int sltreem = int.Parse(form["sotreem"]);

                int giaguoilon = int.Parse(form["gianguoilon"]);
                int giatreem = int.Parse(form["giatreem"]);

                int soluong = slnguoilon + sltreem;
                Session["SoLuong"] = soluong;
                ////////////////

                int SoLuongSPTOUR = (int)sptour.SoNguoi;
                if (SoLuongSPTOUR < soluong)
                {
                    ViewBag.SoLuong = "Vui lòng đặt ít hơn hoặc bằng số lượng chỗ hiện có !";
                    return View(sptour);
                }
                else
                {
                    int tongtien = (slnguoilon * giaguoilon) + (sltreem * giatreem);
                    int VAT = (int)(tongtien * 0.05);
                    hOADON.TongTienTour = tongtien;
                    hOADON.TienPhaiTra = tongtien - hOADON.TienKhuyenMai + VAT;

                    SoLuongSPTOUR -= soluong;
                    sptour.SoNguoi = SoLuongSPTOUR;
                    db.Entry(sptour).State = EntityState.Modified;
                    db.SaveChanges();
                    db.HOADONs.Add(hOADON);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("HoaDon", "Home", new { id = hOADON.ID_HoaDon });
        }
        public ActionResult PartialDanhMucTour()
        {
            return PartialView();
        }
        [HttpGet]
        public ActionResult DanhMucTour(string name, int? to, int? from, int page = 1)
        {
            ViewBag.TourNameList = db.TOURs.ToList();

            page = page < 1 ? 1 : page;/////
            int pageSize = 9;/////////
            var tours = from t in db.SPTOURs select t;

            if (!string.IsNullOrEmpty(name))
            {
                tours = tours.Where(x => x.TenSPTour.Contains(name));
                if (to != null)
                {
                    tours = tours.Where(x => x.TenSPTour.Contains(name) && x.GiaNguoiLon >= to);
                }
                if (from != null)
                {
                    tours = tours.Where(x => x.TenSPTour.Contains(name) && x.GiaNguoiLon <= from);
                }
            }
            else
            {
                if (to != null || from != null)
                {
                    if (to != null && from != null)
                    {
                        tours = tours.Where(x => x.GiaNguoiLon >= to && x.GiaNguoiLon <= from);
                    }
                    else if (to != null)
                    {
                        tours = tours.Where(x => x.GiaNguoiLon >= to);
                    }
                    else if (from != null)
                    {
                        tours = tours.Where(x => x.GiaNguoiLon <= from);
                    }
                }
            }
            //sắp xếp theo giá tiền
            tours = tours.OrderBy(item => item.GiaNguoiLon);

            //kiểm tra để thông báo lỗi 
            if(tours.Count() == 0)
            {
                ViewBag.MesSearch = "Không tìm thấy tour nào với lựa chọn của bạn !";
            }
            var toursPage = tours.ToPagedList(page, pageSize);
            return View(toursPage);
        }
        public ActionResult HoaDon(int id)
        {
            var data = db.HOADONs.Find(id);
            return View(data);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}