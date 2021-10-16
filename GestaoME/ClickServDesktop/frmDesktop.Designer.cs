
namespace ClickServDesktop
{
    partial class frmDesktop
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblData = new System.Windows.Forms.Label();
            this.menuPainel = new System.Windows.Forms.MenuStrip();
            this.clienteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNovoCliente = new System.Windows.Forms.ToolStripMenuItem();
            this.pesquisarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.atendimentoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNovoAtendimento = new System.Windows.Forms.ToolStripMenuItem();
            this.relatórioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fecharToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tbcForms = new System.Windows.Forms.TabControl();
            this.menuPainel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(11, 82);
            this.lblUsuario.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(47, 15);
            this.lblUsuario.TabIndex = 0;
            this.lblUsuario.Text = "Usuario";
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.Location = new System.Drawing.Point(11, 34);
            this.lblData.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(0, 15);
            this.lblData.TabIndex = 1;
            // 
            // menuPainel
            // 
            this.menuPainel.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuPainel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clienteToolStripMenuItem,
            this.atendimentoToolStripMenuItem,
            this.sairToolStripMenuItem,
            this.fecharToolStripMenuItem1});
            this.menuPainel.Location = new System.Drawing.Point(0, 0);
            this.menuPainel.Name = "menuPainel";
            this.menuPainel.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuPainel.Size = new System.Drawing.Size(834, 24);
            this.menuPainel.TabIndex = 3;
            this.menuPainel.Text = "menuStrip1";
            // 
            // clienteToolStripMenuItem
            // 
            this.clienteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNovoCliente,
            this.pesquisarToolStripMenuItem});
            this.clienteToolStripMenuItem.Name = "clienteToolStripMenuItem";
            this.clienteToolStripMenuItem.Size = new System.Drawing.Size(56, 22);
            this.clienteToolStripMenuItem.Text = "Cliente";
            // 
            // btnNovoCliente
            // 
            this.btnNovoCliente.Image = global::ClickServDesktop.Properties.Resources.newClient;
            this.btnNovoCliente.Name = "btnNovoCliente";
            this.btnNovoCliente.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.btnNovoCliente.Size = new System.Drawing.Size(188, 30);
            this.btnNovoCliente.Text = "Novo";
            this.btnNovoCliente.Click += new System.EventHandler(this.novoToolStripMenuItem_Click);
            // 
            // pesquisarToolStripMenuItem
            // 
            this.pesquisarToolStripMenuItem.Image = global::ClickServDesktop.Properties.Resources.pesquisar;
            this.pesquisarToolStripMenuItem.Name = "pesquisarToolStripMenuItem";
            this.pesquisarToolStripMenuItem.Size = new System.Drawing.Size(188, 30);
            this.pesquisarToolStripMenuItem.Text = "Pesquisar";
            // 
            // atendimentoToolStripMenuItem
            // 
            this.atendimentoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNovoAtendimento,
            this.relatórioToolStripMenuItem});
            this.atendimentoToolStripMenuItem.Name = "atendimentoToolStripMenuItem";
            this.atendimentoToolStripMenuItem.Size = new System.Drawing.Size(89, 22);
            this.atendimentoToolStripMenuItem.Text = "Atendimento";
            // 
            // btnNovoAtendimento
            // 
            this.btnNovoAtendimento.Image = global::ClickServDesktop.Properties.Resources.atendimento1;
            this.btnNovoAtendimento.Name = "btnNovoAtendimento";
            this.btnNovoAtendimento.Size = new System.Drawing.Size(188, 30);
            this.btnNovoAtendimento.Text = "Novo";
            this.btnNovoAtendimento.Click += new System.EventHandler(this.novoToolStripMenuItem1_Click);
            // 
            // relatórioToolStripMenuItem
            // 
            this.relatórioToolStripMenuItem.Image = global::ClickServDesktop.Properties.Resources.historico;
            this.relatórioToolStripMenuItem.Name = "relatórioToolStripMenuItem";
            this.relatórioToolStripMenuItem.Size = new System.Drawing.Size(188, 30);
            this.relatórioToolStripMenuItem.Text = "Histórico";
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(38, 22);
            this.sairToolStripMenuItem.Text = "Sair";
            this.sairToolStripMenuItem.Click += new System.EventHandler(this.sairToolStripMenuItem_Click);
            // 
            // fecharToolStripMenuItem1
            // 
            this.fecharToolStripMenuItem1.Name = "fecharToolStripMenuItem1";
            this.fecharToolStripMenuItem1.Size = new System.Drawing.Size(54, 22);
            this.fecharToolStripMenuItem1.Text = "Fechar";
            this.fecharToolStripMenuItem1.Click += new System.EventHandler(this.fecharToolStripMenuItem1_Click);
            // 
            // tbcForms
            // 
            this.tbcForms.Location = new System.Drawing.Point(232, 27);
            this.tbcForms.Name = "tbcForms";
            this.tbcForms.SelectedIndex = 0;
            this.tbcForms.Size = new System.Drawing.Size(602, 433);
            this.tbcForms.TabIndex = 4;
            // 
            // frmDesktop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 461);
            this.Controls.Add(this.tbcForms);
            this.Controls.Add(this.lblData);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.menuPainel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmDesktop";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Desktop";
            this.menuPainel.ResumeLayout(false);
            this.menuPainel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.MenuStrip menuPainel;
        private System.Windows.Forms.ToolStripMenuItem clienteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnNovoCliente;
        private System.Windows.Forms.ToolStripMenuItem pesquisarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem atendimentoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnNovoAtendimento;
        private System.Windows.Forms.ToolStripMenuItem relatórioToolStripMenuItem;
        private System.Windows.Forms.TabControl tbcForms;
        private System.Windows.Forms.ToolStripMenuItem fecharToolStripMenuItem1;
    }
}