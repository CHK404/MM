using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Material_Management.Models;

namespace Material_Management.Models
{
    public class Material
    {
        [Key]
        public string? SerialNumber { get; set; }
        public string? MaterialName { get; set; }
        public string? MaterialType { get; set; }
        public string? UserID { get; set; }
        public string? InfoURL { get; set; }
        public string? Content { get; set; }
        public string? Manufacturer { get; set; }
        public int Quantity { get; set; }
    }
}
