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
using System.Net;
using System.Net.Sockets;

namespace Login2
{
    /// <summary>
    /// Interaction logic for PowerMeter.xaml
    /// </summary>
    public partial class PowerMeter : Window
    {
        private TcpListener listener;
        static String controlKWH = "";
        static double harga = 0;
        String daya;
        public PowerMeter(String nama, String id, String daya)
        {
            InitializeComponent();
            listener = new TcpListener(IPAddress.Any, 1337);
            listener.Start();
            StartAccept();
            Nama.Content = nama;
            IDPelanggan.Content = id;
            Daya.Content = daya;
            this.daya = daya;
        }
        private void StartAccept()
        {
            try
            {
                
                listener.BeginAcceptTcpClient(HandleAsyncConnection, listener);
                //MessageBox.Show("muncul");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //MessageBox.Show(e.Message);
            }

        }
        private void HandleAsyncConnection(IAsyncResult res)
        {
            try
            {
                StartAccept();
                //MessageBox.Show("data");
                TcpClient clientSocket = listener.EndAcceptTcpClient(res);
                byte[] bytesFrom = new byte[65555];
                String dataFromClient;
                NetworkStream networkStream = clientSocket.GetStream();
                int lengthh = networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                
                if (networkStream.CanWrite)
                {
                    byte[] myWriteBuffer = Encoding.ASCII.GetBytes(controlKWH);
                    networkStream.Write(myWriteBuffer, 0, myWriteBuffer.Length);
                    bytesFrom = setNewArray(bytesFrom, lengthh);
                    controlKWH = "";
                }
                
                dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                Console.WriteLine(dataFromClient);
                string[] dataSplit = dataFromClient.Split('_');
                Dispatcher.Invoke(() =>
                {
                    Voltage.Content = dataSplit[0];
                    Ampere.Content = dataSplit[1];
                    Watt.Content = dataSplit[2];
                    Kwh.Content = dataSplit[3];
                    Hz.Content = dataSplit[4];
                    Pf.Content = dataSplit[5];
                    float hargaDaya = 0;
                    if (daya.Contains("450 VA"))
                    {
                        hargaDaya = 452;
                    }else if (daya.Contains("900 VA Subsidi"))
                    {
                        hargaDaya = 750;
                    }
                    else if (daya.Contains("900 VA Non Subsidi"))
                    {
                        hargaDaya = 1352;
                    }
                    else if (daya.Contains("1300 VA"))
                    {
                        hargaDaya = 1452;
                    }
                    float kwhSekarang = float.Parse(dataSplit[3]);
                    //float kwhSekarang = 1000;

                    float hargaSekarang = (hargaDaya / 3600) * kwhSekarang;
                    //Console.WriteLine(hargaSekarang);
                    hargaSekarang =hargaSekarang + (hargaSekarang * (60 / 100));
                    
                    harga += hargaSekarang;
                    Rp.Content = harga;


                });
                

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        public byte[] setNewArray(byte[] data, int panjangBaru)
        {
            byte[] dataBaru = new byte[panjangBaru];
            for (int i = 0; i < panjangBaru; i++)
            {
                dataBaru[i] = data[i];
            }
            return dataBaru;
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            listener.Stop();
            this.Close();
        }

        private void KWHOFF(object sender, RoutedEventArgs e)
        {
            controlKWH = "mati";
        }
        

        private void KWHON(object sender, RoutedEventArgs e)
        {
            controlKWH = "nyala";
        }
    }
}
