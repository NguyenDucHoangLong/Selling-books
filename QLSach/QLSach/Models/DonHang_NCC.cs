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
    
    public partial class DonHang_NCC
    {
        public int MaDonHang { get; set; }
        public int MaSach { get; set; }
        public System.DateTime NgayGiao { get; set; }
        public int SoLuong { get; set; }
        public string TinhTrang { get; set; }
        public Nullable<decimal> TongTien { get; set; }
        public Nullable<int> MaHopDong { get; set; }
    
        public virtual HopDong HopDong { get; set; }
        public virtual Sach Sach { get; set; }
    }
}
