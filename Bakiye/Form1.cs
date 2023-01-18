using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace Bakiye
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "-",
            BasePath = "-"
        };
        IFirebaseClient client;
        private void button1_Click(object sender, EventArgs e)
        {
            var login = client.Get($"Üye/{textBox1.Text}");
            giris log = login.ResultAs<giris>();

            if(log != null)
            {
                if(textBox2.Text == log.Password)
                {
                    Bakiye.kad = textBox1.Text;
                    Bakiye fr2 = new Bakiye();
                    fr2.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Kullanıcı Adı veya Parola Yanlış!");
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı Adı veya Parola Yanlış!");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(config);
            }
            catch
            {
                MessageBox.Show("Sunucuya bağlanırken bir sorun oluştu! Lütfen daha sonra tekrar deneyiniz.");
            }
        }
    }
}
