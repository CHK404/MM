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
    public partial class MaterialView : Page
    {
        public MaterialView()
        {
            InitializeComponent(); InitializeComponent();
            DataContext = new MaterialViewModel();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is MaterialViewModel vm)
            {
                await vm.LoadMaterialsAsync();
            }
        }
        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGrid dg && dg.SelectedItem is Models.Material selectedMaterial)
            {
                if (DataContext is MaterialViewModel vm && vm.ViewDetailsCommand.CanExecute(selectedMaterial))
                {
                    vm.ViewDetailsCommand.Execute(selectedMaterial);
                }
            }
        }
    }
}
