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
    
    public partial class NhaCungCap
    {
        public NhaCungCap()
        {
            this.HopDongs = new HashSet<HopDong>();
            this.KhaNangCungCaps = new HashSet<KhaNangCungCap>();
            this.Tokens = new HashSet<Token>();
        }
    
        public int MaNCC { get; set; }
        public string TenNCC { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string TaiKhoan { get; set; }
        public string Email { get; set; }
    
        public virtual ICollection<HopDong> HopDongs { get; set; }
        public virtual ICollection<KhaNangCungCap> KhaNangCungCaps { get; set; }
        public virtual ICollection<Token> Tokens { get; set; }
    }
}
