using ClickServDesktop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestaoMELogin
{
    public partial class frmPainel : Form
    {
        public frmPainel(string usuario)
        {
            InitializeComponent();
            lblUsuario.Text = usuario;
            frmDesktop frm = new frmDesktop(usuario);
        }

      
        private void clickServToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string usuario = lblUsuario.Text;
            frmDesktop frm = new frmDesktop(usuario);
            frm.Show();
            Hide();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja sair da aplicação ?", "Sair", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                frmLogin login = new frmLogin();
                login.Show();
                this.Close();
            }
        }
    }
}
