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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Material_Management.ViewModels;

namespace Material_Management.Views
{
    public partial class UserView : Page
    {
        public UserView()
        {
            InitializeComponent();
            DataContext = new UserViewModel();
        }
        private async void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is UserViewModel vm)
            {
                await vm.LoadUsersAsync();
            }
        }
    }
}
