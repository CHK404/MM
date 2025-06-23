using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Material_Management.Data;
using Material_Management.Views;
using System.Windows.Input;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Material_Management.Models;

namespace Material_Management.ViewModels
{
    public partial class UserViewModel : ObservableObject
    {
        public ObservableCollection<UserList> Users { get; } = new();

        public IRelayCommand DeleteUserCommand { get; }
        public UserViewModel()
        {
            _ = LoadUsersAsync();
        }
        public async Task LoadUsersAsync()
        {
            using var db = new AppDbContext();
            var users = await db.UserInfo.ToListAsync();
            Users.Clear();
            foreach (var user in users)
            {
                Users.Add(user);
            }
        }
    }
}

