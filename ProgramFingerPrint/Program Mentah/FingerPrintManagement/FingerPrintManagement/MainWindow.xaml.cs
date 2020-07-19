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

namespace FingerPrintManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void searchButton(object sender, RoutedEventArgs e)
        {
            DBConnection dBConnection = new DBConnection();
            List<string>[] list = dBConnection.Select(Pelanggan.Text);
            //MessageBox.Show(list[0].Count.ToString());
            if (list[0].Count > 0)
            {
                PowerMeter powerMeter = new PowerMeter(list[0][0], list[1][0], list[2][0]);
                powerMeter.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Data Tidak Ditemukan");
            }
        }
        
        private void Exit(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
