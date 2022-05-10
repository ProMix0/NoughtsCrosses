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
    /// Логика взаимодействия для RepeatDialog.xaml
    /// </summary>
    public partial class RepeatDialog : Window
    {
        public RepeatDialog()
        {
            InitializeComponent();
        }
        private void Ok(object sender, RoutedEventArgs e) =>
            DialogResult = true;

        private void Cancel(object sender, RoutedEventArgs e) =>
            DialogResult = false;

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) =>
            DialogResult = false;
    }
}
