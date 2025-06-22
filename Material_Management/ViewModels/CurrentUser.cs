using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Material_Management.Models;

namespace Material_Management.ViewModels
{
    public static class CurrentUser
    {
        public static string? UserID { get; set; }
        public static string? UserName { get; set; }
        public static bool IsAdmin { get; set; }
        public static void Set(UserList user)
        {
            UserID = user.UserID;
            UserName = user.UserName;
            IsAdmin = user.IsAdmin;
        }
        public static void Clear()
        {
            UserID = null;
            UserName = null;
            IsAdmin = false;
        }
    }
}
