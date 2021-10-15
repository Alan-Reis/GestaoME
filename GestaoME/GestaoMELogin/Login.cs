using ClickServClassLibrary;
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
    public partial class frmLogin : Form
    {
        //Variavel para mostrar e ocultar a senha

        bool ver = false;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            string verificar;
            verificar = txtUsuario.Text;
            
            if(verificar == "")
            {
                MessageBox.Show("Digite um usuário!");
            }
            else
            {
                Authentication autenticacao = new Authentication();

                if ((txtUsuario.Text == autenticacao.Usuario) && (txtSenha.Text == autenticacao.Senha))
                {
                    frmPainel painel = new frmPainel(autenticacao.Usuario);
                    painel.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show("Usuário ou senha digitado errado!", "Erro!", MessageBoxButtons.OK);
                }

            }


        }
        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            if(ver == false)
            {
                txtSenha.PasswordChar = '\0';
                ver = true;
            }
            else
            {
                txtSenha.PasswordChar = '*';
                ver = false;
            }
            
        }
    }
}
