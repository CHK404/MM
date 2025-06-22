using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Material_Management.Models;

namespace Material_Management.Models
{
    public class UserList
    {
        [Key]
        public string? UserID { get; set; }
        public string? PasswordHash { get; set; }
        public string? UserName { get; set; }
        public bool IsAdmin { get; set; }
    }
}
