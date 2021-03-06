﻿using System;
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
        static double kwhTotal = 0;
        String daya;
        MqttClient client;
        public PowerMeter(String nama, String id, String daya)
        {
            InitializeComponent();
            try
            {
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
            }
            catch(Exception e)
            {
                MessageBox.Show("Connection Internet Not Found");
            }
            
            Nama.Content = nama;
            IDPelanggan.Content = id;
            Daya.Content = daya;
            this.daya = daya;
            Kwh.Content = 0;
            Ampere.Content = 0;
            Watt.Content = 0;
            Pf.Content = 0;
            Voltage.Content = 0;
            Hz.Content = 0;
            kwhTotal = 0;
            //Kwh.Content = kwhTotal.ToString("n6");
        }
        private void Exit(object sender, MouseButtonEventArgs e)
        {
            try
            {
                client.Disconnect();
            }
            catch(Exception Ex)
            {

            }
            
            this.Close();
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
                        float ampereSekarang = float.Parse(dataSplit[1]);
                        if(ampereSekarang <= 0.09)
                        {
                            Ampere.Content = 0;
                            Watt.Content = 0;
                            Pf.Content = 0;
                        }
                        else
                        {
                            Ampere.Content = dataSplit[1];
                            //Watt.Content = (double.Parse(dataSplit[1]) * double.Parse(dataSplit[0])).ToString("n3");
                            Watt.Content = dataSplit[2];
                            Pf.Content = dataSplit[5];
                            //double kwhSekarang = (double.Parse(dataSplit[1]) * double.Parse(dataSplit[0]))/3600000;
                            double kwhSekarang = double.Parse(dataSplit[2])  / 3600000;
                            kwhTotal = kwhTotal + kwhSekarang;
                            Console.WriteLine(kwhTotal);
                            Kwh.Content = kwhTotal.ToString("n6");
                           
                        }
                        Voltage.Content = dataSplit[0];
                        Hz.Content = dataSplit[4];
                        
                    }
                    catch(Exception err)
                    {
                        //MessageBox.Show("Format Salah !!!");
                    }
                    


                });
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            try
            {
                client.Disconnect();
            }
            catch(Exception Ex)
            {

            }
            
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
