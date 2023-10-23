using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace therad
{
    public partial class ServerForm : Form
    {
        Socket socketserver = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> socketclient = null;
        public ServerForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls=false;
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {

        }
        public void mssg()
        {
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[1024];
                    foreach (var item in socketclient)
                    {
                        int count = item.Receive(buffer);
                        if (count > 0)
                        {
                            string msg = Encoding.Unicode.GetString(buffer, 0, count);
                            listBox1.Items.Add(msg);
                        }
                    }
                  
                }
            }
            catch
            {

                ;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("192.168.41.1"), 5050);
            socketserver.Bind(iPEndPoint);
            socketserver.Listen(10);
            socketclient.Add( socketserver.Accept());
            Thread thread = new Thread(new ThreadStart(mssg));
            thread.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] barray = new byte[1024];
            barray = Encoding.Unicode.GetBytes(textBox1.Text);
            foreach (var item in socketclient)
            {
                item.Send(barray);
            }
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (socketclient != null)
                {

                    //socketclient.Shutdown(SocketShutdown.Both);
                }
                if (socketserver != null)
                {
                    socketserver.Shutdown(SocketShutdown.Both);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
    }
}
