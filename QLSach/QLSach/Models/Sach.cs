//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLSach.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sach
    {
        public Sach()
        {
            this.ChiTietHopDongs = new HashSet<ChiTietHopDong>();
            this.CTDH_KH = new HashSet<CTDH_KH>();
            this.DonHang_NCC = new HashSet<DonHang_NCC>();
            this.KhaNangCungCaps = new HashSet<KhaNangCungCap>();
        }
    
        public int MaSach { get; set; }
        public string TenSach { get; set; }
        public string MoTa { get; set; }
        public Nullable<int> DanhMuc { get; set; }
        public Nullable<decimal> Gia { get; set; }
        public Nullable<int> SoLuongTon { get; set; }
        public Nullable<int> NamSanXuat { get; set; }
        public string NhaXuatBan { get; set; }
        public string TacGia { get; set; }
        public string AnhBia { get; set; }
        public Nullable<int> SoLuongCan { get; set; }
        public Nullable<bool> TinhTrang { get; set; }
        public Nullable<int> SoLuongTonToiThieu { get; set; }
        public Nullable<int> ThoiHan { get; set; }
    
        public virtual ICollection<ChiTietHopDong> ChiTietHopDongs { get; set; }
        public virtual ICollection<CTDH_KH> CTDH_KH { get; set; }
        public virtual DanhMuc DanhMuc1 { get; set; }
        public virtual ICollection<DonHang_NCC> DonHang_NCC { get; set; }
        public virtual ICollection<KhaNangCungCap> KhaNangCungCaps { get; set; }
    }
}
