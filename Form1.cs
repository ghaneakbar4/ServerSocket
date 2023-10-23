using System.Net;
using System.Threading;

namespace therad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        public void setdata()
        {
            while (true)
            {

                listBox1.Items.Add("number is:" + listBox1.Items.Count.ToString());
                System.Threading.Thread.Sleep(1000);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(setdata));
            thread.Start();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = Dns.GetHostName();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IPHostEntry entry = Dns.Resolve(Dns.GetHostName());
            foreach(var item in entry.AddressList)
            {
                if (item.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    listBox1.Items.Add(item.ToString());
                }
                
            }
        }
    }
}