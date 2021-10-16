using ClickServDesktop.Forms;
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
    public partial class frmDesktop : Form
    {
        public frmDesktop()
        {
        }

        public frmDesktop(string usuario)
        {
            InitializeComponent();
            var data = "Salvador, " + DateTime.Now.ToString("dddd, dd MMMM yyyy" + "\n" + "HH:mm");
            string horas = DateTime.Now.ToString(" HH");
            int saudacoes = Int32.Parse(horas);
            lblData.Text = data;

            if(saudacoes > 12)
            {
                lblUsuario.Text = "Boa tarde! " + usuario;
            }
            else
            {
                lblUsuario.Text = "Bom dia! " + usuario;
            }
            
        }


        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja sair da aplicação ?", "Sair", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }

        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCliente cliente = new frmCliente();
            TabPage tb = new TabPage();
            tb.Name = "Cliente";
            tb.Text = "Novo Cliente";
            tb.Controls.Add(cliente);
            tbcForms.TabPages.Add(tb);

            if (tb.Text == "Novo Cliente")
            {
                btnNovoCliente.Enabled = false;
            }
        }

        private void novoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAtendimento atendimento = new frmAtendimento();
            TabPage tb = new TabPage();
            tb.Name = "Atendimento";
            tb.Text = "Novo atendimento";
            tb.Controls.Add(atendimento);
            tbcForms.TabPages.Add(tb);

            if (tb.Text == "Novo atendimento")
            {
                btnNovoAtendimento.Enabled = false;
            }

        }

        private void fecharToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!(tbcForms.SelectedTab == null))
            {
                tbcForms.TabPages.Remove(tbcForms.SelectedTab);
            }

            btnNovoCliente.Enabled = true;
            btnNovoAtendimento.Enabled = true;

        }
    }
}
