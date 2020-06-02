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
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Login2
{
    /// <summary>
    /// Interaction logic for PowerMeter.xaml
    /// </summary>
    public partial class PowerMeter : Window
    {
        ///private TcpListener listener;
        static String controlKWH = "";
        static double harga = 0;
        String daya;
        MqttClient client;
        public PowerMeter(String nama, String id, String daya)
        {
            InitializeComponent();
            ///listener = new TcpListener(IPAddress.Any, 1337);
            ///listener.Start();
            ///StartAccept();
            client = new MqttClient("broker.hivemq.com");
            //client.ProtocolVersion = MqttProtocolVersion.Version_3_1_1;
            string clientId = Guid.NewGuid().ToString();
            byte code = client.Connect(clientId);
            if (code == 0)
            {
                //Subcribe Topic
                client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
                client.Subscribe(new string[] { "tugasakhir/status" }, new byte[] { 0 });

            }
            else
            {

            }
            Nama.Content = nama;
            IDPelanggan.Content = id;
            Daya.Content = daya;
            this.daya = daya;
        }

        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            //ReceiveAction = Receive;
            try
            {
                string message = Encoding.UTF8.GetString(e.Message);
                //MessageBox.Show(message);
                string[] dataSplit = message.Split('_');
                Dispatcher.Invoke(() =>
                {
                    try
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
                        }
                        else if (daya.Contains("900 VA Subsidi"))
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
                        hargaSekarang = hargaSekarang + (hargaSekarang * (60 / 100));

                        harga += hargaSekarang;
                        Rp.Content = harga;
                    }
                    catch(Exception err)
                    {
                        MessageBox.Show("Format Salah !!!");
                    }
                    


                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            ///listener.Stop();
            this.Close();
        }

        private void KWHOFF(object sender, RoutedEventArgs e)
        {
            string topic = "tugasakhir/control";
            ushort ush = client.Publish(topic, Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
        }


        private void KWHON(object sender, RoutedEventArgs e)
        {
            string topic = "tugasakhir/control";
            ushort ush = client.Publish(topic, Encoding.UTF8.GetBytes("1"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
        }
    }
}
