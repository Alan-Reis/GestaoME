using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClickServDesktop
{
    public partial class frmPainel : Form
    {
        public frmPainel(string usuario)
        {
            InitializeComponent();
            lblUsuario.Text = usuario;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
         
            if(MessageBox.Show("Deseja sair da aplicação ?", "Sair", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                
                frmLogin login = new frmLogin();
                login.Show();
                this.Close();
            }

           
        }
    }
}
