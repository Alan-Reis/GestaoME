using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Painel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_ClickServ_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Users\Goldenfir\Desktop\ClickServ\ClickServ2022");
        }
    }
}
