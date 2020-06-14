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

namespace Login2
{
    /// <summary>
    /// Interaction logic for InsertData.xaml
    /// </summary>
    public partial class InsertData : Window
    {
        public InsertData()
        {
            InitializeComponent();
        }
        private void insertButton(object sender, RoutedEventArgs e)
        {
            if(IDPelanggan.Text != "" && NamaPelanggan.Text != "" && VA.Text != ""){
                DBConnection dBConnection = new DBConnection();
                dBConnection.Insert(NamaPelanggan.Text, IDPelanggan.Text, VA.Text);
                PowerMeter powerMeter = new PowerMeter(NamaPelanggan.Text,IDPelanggan.Text, VA.Text);
                powerMeter.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Data Harus di Isi");
            }
        }

        private void kembali(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();

        }
    }
}
