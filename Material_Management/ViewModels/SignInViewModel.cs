using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Material_Management.Data;
using Material_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Material_Management.ViewModels
{
    public class SignInViewModel
    {
        public string? UserID { get; set; }
        public string? Password { get; set; }

        public ICommand SignInCommand { get; }
        public Action? LoginCallback { get; set; }
        public SignInViewModel()
        {
            SignInCommand = new RelayCommand(async () => await Login());
        }
        private async Task Login()
        {
            if (string.IsNullOrWhiteSpace(UserID) || string.IsNullOrWhiteSpace(Password))
            {
                ShowError("아이디 또는 비밀번호를 입력해주세요.");
                return;
            }

            await using var db = new AppDbContext();
            var user = await db.Users.FirstOrDefaultAsync(u => u.UserID == UserID);

            if (user is null)
            {
                ShowError("존재하지 않는 사용자입니다.");
                return;
            }

            if (!Encrypter.VerifyPW(Password, user.PasswordHash!))
            {
                ShowError("비밀번호가 일치하지 않습니다.");
                return;
            }

            CurrentUser.Set(user);
            LoginCallback?.Invoke();
        }
        private void ShowError(string message)
        {
            MessageBox.Show(message, "로그인 오류", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
