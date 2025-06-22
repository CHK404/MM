using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Material_Management.Data;
using Material_Management.Models;
using System.Windows.Input;
using System.Windows;
using Material_Management.Views;

namespace Material_Management.ViewModels
{
    public class MainViewModel
    {
        public ICommand SignInCommand { get; }
        public ICommand SignUpCommand { get; }

        public MainViewModel()
        {
            SignInCommand = new RelayCommand(OnSignIn);
            SignUpCommand = new RelayCommand(OnSignUp);
        }
        private void OnSignIn()
        {
            var signInWindow = new SignInPage();
            signInWindow.Show();
        }
        private void OnSignUp()
        {
            var signUpWindow = new SignUpPage();
            signUpWindow.Show();
        }
    }
}
