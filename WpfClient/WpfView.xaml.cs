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

namespace WpfClient
{
    /// <summary>
    /// Логика взаимодействия для WpfClient.xaml
    /// </summary>
    public partial class WpfView : Window, IWpfView
    {
        public WpfView()
        {
            InitializeComponent();
        }

        public TextBlock StateLine => stateLine;

        public Grid Field { get => field; set => field = value; }
    }
}
