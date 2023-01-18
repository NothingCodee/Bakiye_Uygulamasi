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
    public partial class Bakiye : Form
    {
        public static string kad;
        public Bakiye()
        {
            InitializeComponent();
        }
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "-",
            BasePath = "-"
        };
        IFirebaseClient client;
        private void Bakiye_Load(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(config);
            }
            catch
            {
                MessageBox.Show("Sunucuya bağlanırken bir sorun oluştu! Lütfen daha sonra tekrar deneyiniz.");
            }
            reload();
        }
        int k_sayi;
        int baki;
        int c_sayi;
        private void reload()
        {
            flowLayoutPanel1.Controls.Clear();
            var bakiye = client.Get($"Üye/{kad}/");
            bak cash = bakiye.ResultAs<bak>();
            label1.Text = cash.Bakiye + "₺";
            baki = Convert.ToInt32(cash.Bakiye);
            //combobox//
            comboBox1.Controls.Clear();
            var kats = client.Get($"Üye/{kad}/Kategoriler/");
            Kategoriler k = kats.ResultAs<Kategoriler>();
            k_sayi = Convert.ToInt32(k.k_sayi);

            for(int i = 0; i != k_sayi; i++)
            {
                var kts = client.Get($"Üye/{kad}/Kategoriler/k{i}");
                Kategoriler kt = kts.ResultAs<Kategoriler>();

                comboBox1.Items.Add(kt.ismi);
            }
            

            //cüzdan//
            var cüzdan = client.Get($"Üye/{kad}/Cüzdan");
            cü cüz = cüzdan.ResultAs<cü>();
            c_sayi = Convert.ToInt32(cüz.c_sayi);

            for (int i = c_sayi - 1; i != -1; i--)
            {
                var cz = client.Get($"Üye/{kad}/Cüzdan/c{i}");
                cü c = cz.ResultAs<cü>();

                if(c.Tür == "Kazanç")
                {
                    Kategor kategorr = new Kategor();
                    Kategor.miktar = c.Miktar;
                    Kategor.kategori = c.Kat;

                    flowLayoutPanel1.Controls.Add(kategorr);
                }
                else
                {
                    kategor2 kategorrr = new kategor2();
                    kategor2.miktar = c.Miktar;
                    kategor2.kategori = c.Kat;

                    flowLayoutPanel1.Controls.Add(kategorrr);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cü c = new cü
            {
                Tür=comboBox2.Text,
                Kat=comboBox1.Text,
                Miktar=textBox1.Text
            };
            var create = client.Set($"Üye/{kad}/Cüzdan/c{c_sayi}", c);
            if(comboBox2.Text == "Kazanç")
            {
                yeni_bakiye = baki + Convert.ToInt32(textBox1.Text);
            }
            else 
            {
                yeni_bakiye = baki - Convert.ToInt32(textBox1.Text);
            }
            cc_sayi = c_sayi + 1;
            
            bakiye_guncel();
        }
        int cc_sayi;
        int yeni_bakiye;
        private void bakiye_guncel()
        {
            bak b = new bak
            {
                Bakiye = yeni_bakiye.ToString()
            };
            var create = client.Update($"Üye/{kad}", b);
            guncelle();
        }
        private void guncelle()
        {
            cü c = new cü
            {
                c_sayi = cc_sayi.ToString()
            };
            var create = client.Update($"Üye/{kad}/Cüzdan", c);
            reload();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
