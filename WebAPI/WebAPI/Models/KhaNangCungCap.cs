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
    
    public partial class KhaNangCungCap
    {
        public int MaNCC { get; set; }
        public int MaSach { get; set; }
        public Nullable<decimal> DonGia { get; set; }
        public Nullable<int> Soluong { get; set; }
    
        public virtual NhaCungCap NhaCungCap { get; set; }
        public virtual Sach Sach { get; set; }
    }
}
