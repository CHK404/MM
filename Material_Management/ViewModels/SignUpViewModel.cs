using Material_Management.Models;
using Material_Management.Data;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;

namespace Material_Management.ViewModels
{
    public partial class SignUpViewModel : ObservableObject
    {
        [ObservableProperty] private string userID = string.Empty;
        [ObservableProperty] private string password = string.Empty;
        [ObservableProperty] private string userName = string.Empty;
        [ObservableProperty] private bool isAdmin = false;

        public Action? SignUpCallback { get; set; }

        [RelayCommand]
        private async Task SignUp()
        {
            if (string.IsNullOrWhiteSpace(UserID))
            {
                ShowError("아이디를 입력하세요.");
                return;
            }

            if (UserID.Length < 6 || UserID.Length > 16)
            {
                ShowError("아이디는 6자 이상 16자 이하여야 합니다.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                ShowError("비밀번호를 입력하세요.");
                return;
            }

            if (Password.Length < 8 || Password.Length > 20)
            {
                ShowError("비밀번호는 8자 이상 20자 이하여야 합니다.");
                return;
            }

            if (!Regex.IsMatch(Password, @"[A-Za-z]") || !Regex.IsMatch(Password, @"\d"))
            {
                ShowError("비밀번호에는 영문자와 숫자가 모두 포함되어야 합니다.");
                return;
            }

            if (string.IsNullOrWhiteSpace(UserName))
            {
                ShowError("이름을 입력하세요.");
                return;
            }

            await using var db = new AppDbContext();
            if (await db.Users.AnyAsync(u => u.UserID == UserID))
            {
                ShowError("이미 존재하는 아이디입니다. 다른 아이디를 입력해주세요.");
                return;
            }

            var newUser = new UserList
            {
                UserID = UserID,
                PasswordHash = Encrypter.HashPW(Password),
                UserName = UserName,
                IsAdmin = IsAdmin
            };

            await db.Users.AddAsync(newUser);
            await db.SaveChangesAsync();

            SignUpCallback?.Invoke();
        }
        private void ShowError(string msg) =>
            MessageBox.Show(msg, "입력 오류", MessageBoxButton.OK, MessageBoxImage.Warning);
    }
}
