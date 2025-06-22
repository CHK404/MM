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
using Material_Management.Models;
using Material_Management.ViewModels;
using Material_Management.Data;
using System.Windows.Navigation;

namespace Material_Management.Views
{
    public partial class SignInPage : Window
    {
        private readonly SignInViewModel viewModel;

        public SignInPage()
        {
            InitializeComponent();
            viewModel = new SignInViewModel();
            viewModel.LoginCallback = LoginSuccess;
            DataContext = viewModel;

            PasswordBox.PasswordChanged += PasswordBox_PasswordChanged;
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox pb && DataContext is SignInViewModel viewModel)
            {
                viewModel.Password = pb.Password;
            }
        }
        private void LoginSuccess()
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            // 시작화면 숨기고 Frame 보이게
            mainWindow.StartPanel.Visibility = Visibility.Collapsed;
            mainWindow.ContentFrame.Visibility = Visibility.Visible;
            // MaterialView 페이지로 전환
            mainWindow.ContentFrame.Navigate(new MaterialView());

            this.Close();
        }
    }
}
