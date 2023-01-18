using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bakiye
{

    public partial class Kategor : UserControl
    {
        public static string miktar;
        public static string kategori;
        public Kategor()
        {
            InitializeComponent();
        }

        private void Kategor_Load(object sender, EventArgs e)
        {
            label2.Text = miktar + "₺";
            label1.Text = kategori;
        }
    }
}
