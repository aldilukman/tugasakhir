using HamburgerMenuApp.Core.Database;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace HamburgerMenuApp
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : MetroWindow
    {
        private TcpListener listener;
        public MainWindow()
        {
            InitializeComponent();
            listener = new TcpListener(IPAddress.Any, 12346);
            listener.Start();
            StartAccept();
        }
        private void StartAccept()
        {
            try
            {
                listener.BeginAcceptTcpClient(HandleAsyncConnection, listener);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                DbConnection dbConnection = new DbConnection();
                dbConnection.InsertHistory("Koneksi Server ada masalah");
            }
        }
        private void HandleAsyncConnection(IAsyncResult res)
        {
            try
            {
                StartAccept();
                TcpClient clientSocket = listener.EndAcceptTcpClient(res);
                byte[] bytesFrom = new byte[65555];
                String dataFromClient;
                NetworkStream networkStream = clientSocket.GetStream();
                int lengthh = networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                bytesFrom = setNewArray(bytesFrom, lengthh);
                dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                //MessageBox.Show(dataFromClient);
                DbConnection dbConnection = new DbConnection();
                if (dataFromClient.Equals("Sukses"))
                {
                    dbConnection.UpdateStatus(1);
                }
                else
                {
                    List<string>[] dataCheck = dbConnection.SelectFingerPrint(dataFromClient);
                    bool found = false;
                    if (dataCheck[0].Count != 0)
                    {
                        for (int i = 0; i < dataCheck[0].Count; i++)
                        {
                            if (dataCheck[6][i].Equals(System.DateTime.Now.ToString("dddd")))
                            {
                                DateTime dateTime = DateTime.Now;
                                int dateTimeMasukDetik = int.Parse(dataCheck[4][i].Split(':')[2]);
                                int dateTimeMasukMenit = int.Parse(dataCheck[4][i].Split(':')[1]);
                                int dateTimeMasukJam = int.Parse(dataCheck[4][i].Split(':')[0]);
                                int dateTimeKeluarDetik = int.Parse(dataCheck[5][i].Split(':')[2]);
                                int dateTimeKeluarMenit = int.Parse(dataCheck[5][i].Split(':')[1]);
                                int dateTimeKeluarJam = int.Parse(dataCheck[5][i].Split(':')[0]);
                                var dateTimeMasuk = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dateTimeMasukJam, dateTimeMasukMenit, dateTimeMasukDetik);
                                var dateTimeKeluar = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, dateTimeKeluarJam, dateTimeKeluarMenit, dateTimeKeluarDetik);
                                if ((TimeSpan.Compare(dateTime.TimeOfDay, dateTimeMasuk.TimeOfDay) > 0) && (TimeSpan.Compare(dateTime.TimeOfDay, dateTimeKeluar.TimeOfDay) < 0))
                                {
                                    found = true;
                                    break;
                                }
                            }
                        }
                        if (found)
                        {
                            dbConnection.InsertHistory("Sukses membuka kunci " + dataCheck[0][1] + " bernama " + dataCheck[0][0]);
                            //send connection open
                            sendData(dataCheck[0][0]);
                        }
                        else
                        {
                            dbConnection.InsertHistory("Mencoba membuka kunci " + dataCheck[0][1] + " bernama " + dataCheck[0][0] + " tapi di luar jadwal");
                        }
                    }
                    else
                    {
                        dbConnection.InsertHistory("Ada yang mencoba masuk, ID Tidak dikenali");
                    }
                }
                
                
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }

        public void sendData(String nama)
        {
            try
            {
                TcpClient clientSocket = new TcpClient();
                //mengirim ack
                clientSocket.Connect("127.0.0.1", 12345);
                NetworkStream serverStream = clientSocket.GetStream();
                byte[] outStream = System.Text.Encoding.ASCII.GetBytes("O_"+nama);
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();

                //menunggu balasan server
                serverStream.Close();
                clientSocket.Close();
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
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

        private void HamburgerMenuControl_OnItemClick(object sender, ItemClickEventArgs e)
        {
            this.HamburgerMenuControl.Content = e.ClickedItem;
            this.HamburgerMenuControl.IsPaneOpen = false;
        }
    }
}
