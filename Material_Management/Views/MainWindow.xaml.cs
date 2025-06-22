using System.Text;
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
using Material_Management.Views;

namespace Material_Management
{
    public partial class MainWindow : Window
    {
        public MainWindow(string? userId, bool isAdmin)
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
        public void NavigateToMaterialPage()
        {
            StartPanel.Visibility = Visibility.Collapsed;   // 초기 로그인/회원가입 UI 숨김
            ContentFrame.Visibility = Visibility.Visible;   // Frame을 보이게 하고 Material 페이지 로드
            ContentFrame.Navigate(new MaterialView());
        }
    }
}