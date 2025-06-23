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
    public class UserViewModel : ObservableObject
    {
        public ObservableCollection<UserList> Users { get; } = new();

        private UserList? _selectedUser;
        public UserList? SelectedUser
        {
            get => _selectedUser;
            set
            {
                if (SetProperty(ref _selectedUser, value))
                {
                    ((RelayCommand)DeleteUserCommand).NotifyCanExecuteChanged();
                }
            }
        }
        public IRelayCommand DeleteUserCommand { get; }
        public UserViewModel()
        {
            DeleteUserCommand = new RelayCommand(DeleteUser, () => SelectedUser != null);
            _ = LoadUsersAsync();
        }
        public async Task LoadUsersAsync()
        {
            using var db = new AppDbContext();
            var users = await db.Users.ToListAsync();
            Users.Clear();
            foreach (var user in users)
            {
                Users.Add(user);
            }
        }
        private async void DeleteUser()
        {
            if (SelectedUser == null)
                return;

            if (MessageBox.Show("정말 삭제하시겠습니까?", "삭제 확인", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            using var db = new AppDbContext();
            db.Users.Remove(SelectedUser);
            await db.SaveChangesAsync();
            Users.Remove(SelectedUser);
            SelectedUser = null;
        }
    }
}

