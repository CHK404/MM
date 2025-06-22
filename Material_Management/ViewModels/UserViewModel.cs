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
        public ObservableCollection<UserList> Users { get; } = new ObservableCollection<UserList>();
        public ICommand EditUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand AddUserCommand { get; }

        public UserViewModel()
        {
            EditUserCommand = new RelayCommand<UserList?>(EditUser);
            DeleteUserCommand = new RelayCommand<UserList?>(DeleteUser);
            AddUserCommand = new RelayCommand(AddUser);
        }
        public async Task LoadUsersAsync()
        {
            try
            {
                using var db = new AppDbContext();
                var users = await db.Users.ToListAsync();
                Users.Clear();
                foreach (var user in users)
                {
                    Users.Add(user);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"데이터 로드 중 오류 발생: {ex.Message}", "에러", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void EditUser(UserList? user)
        {
            if (user is null)
                return;
            try
            {
                using var db = new AppDbContext();
                db.Users.Update(user);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"수정 중 오류 발생: {ex.Message}", "에러", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void DeleteUser(UserList? user)
        {
            if (user is null)
                return;
            if (MessageBox.Show("정말 삭제하시겠습니까?", "삭제 확인", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;
            try
            {
                using var db = new AppDbContext();
                db.Users.Remove(user);
                await db.SaveChangesAsync();
                Users.Remove(user);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"삭제 중 오류 발생: {ex.Message}", "에러", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void AddUser()
        {
            var newUser = new UserList
            {
                UserID = "새 사용자ID",
                PasswordHash = "",
                UserName = "새 사용자",
                IsAdmin = false
            };

            try
            {
                using var db = new AppDbContext();
                db.Users.Add(newUser);
                await db.SaveChangesAsync();
                Users.Add(newUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"추가 중 오류 발생: {ex.Message}", "에러", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

