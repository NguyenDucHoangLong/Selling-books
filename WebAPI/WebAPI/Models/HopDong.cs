//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HopDong
    {
        public HopDong()
        {
            this.ChiTietHopDongs = new HashSet<ChiTietHopDong>();
        }
    
        public int MaHopDong { get; set; }
        public Nullable<System.DateTime> NgayBatDau { get; set; }
        public Nullable<System.DateTime> NgayKetThuc { get; set; }
        public string TinhTrang { get; set; }
        public Nullable<int> MaNCC { get; set; }
    
        public virtual ICollection<ChiTietHopDong> ChiTietHopDongs { get; set; }
        public virtual NhaCungCap NhaCungCap { get; set; }
    }
}
