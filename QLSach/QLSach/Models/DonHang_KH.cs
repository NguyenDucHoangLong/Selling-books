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
    
    public partial class DonHang_KH
    {
        public DonHang_KH()
        {
            this.CTDH_KH = new HashSet<CTDH_KH>();
        }
    
        public int MaDonHang { get; set; }
        public Nullable<System.DateTime> NgayGiao { get; set; }
        public string DiaChiGiao { get; set; }
        public string TinhTrang { get; set; }
        public Nullable<bool> DaThanhToan { get; set; }
        public Nullable<System.DateTime> NgayLap { get; set; }
        public Nullable<decimal> TongTien { get; set; }
        public Nullable<int> MaKH { get; set; }
    
        public virtual ICollection<CTDH_KH> CTDH_KH { get; set; }
        public virtual KhachHang KhachHang { get; set; }
    }
}