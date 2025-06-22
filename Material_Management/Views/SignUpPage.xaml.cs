using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Material_Management.ViewModels;

namespace Material_Management.Views
{
    public partial class SignUpPage : Window
    {
        private readonly SignUpViewModel viewModel;
        public SignUpPage()
        {
            InitializeComponent();
            viewModel = new SignUpViewModel();
            this.DataContext = viewModel;

            viewModel.SignUpCallback = SignedUp;
            PasswordBox.PasswordChanged += (s, e) =>
            {
                viewModel.Password = PasswordBox.Password;
            };
            radioButton_isUser.Checked += (s, e) => viewModel.IsAdmin = false;
            radioButton_isAdmin.Checked += (s, e) => viewModel.IsAdmin = true;
        }
        public void SignedUp()
        {
            MessageBox.Show("회원가입이 완료되었습니다", "완료", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
