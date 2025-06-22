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
            MessageBox.Show("Sign In 창을 열게 해주세요");
        }
        private void OnSignUp()
        {
            MessageBox.Show("Sign Up 창을 열게 해주세요");
        }
    }
}
