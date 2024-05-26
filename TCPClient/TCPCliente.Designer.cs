namespace TCPClient
{
    partial class TCPCliente
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TCPCliente));
            label1 = new Label();
            txtIP = new TextBox();
            btnConectar = new Button();
            btnMensagem = new Button();
            txtMensagem = new TextBox();
            label2 = new Label();
            label3 = new Label();
            txtNomeCliente = new TextBox();
            label4 = new Label();
            cmbcor = new ComboBox();
            txtInfo = new RichTextBox();
            label5 = new Label();
            label6 = new Label();
            btnAnexo = new Button();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            btnDesconectar = new Button();
            btnSelecionarArquivo = new Button();
            txtPastaAnexo = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.ForeColor = Color.DarkGreen;
            label1.Location = new Point(65, 97);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 0;
            label1.Text = "Endereço:";
            // 
            // txtIP
            // 
            txtIP.Location = new Point(144, 96);
            txtIP.Margin = new Padding(3, 2, 3, 2);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(163, 23);
            txtIP.TabIndex = 1;
            txtIP.TextChanged += txtIP_TextChanged;
            // 
            // btnConectar
            // 
            btnConectar.ForeColor = SystemColors.ActiveCaptionText;
            btnConectar.Location = new Point(311, 94);
            btnConectar.Margin = new Padding(3, 2, 3, 2);
            btnConectar.Name = "btnConectar";
            btnConectar.Size = new Size(90, 22);
            btnConectar.TabIndex = 2;
            btnConectar.Text = "Conectar";
            btnConectar.UseVisualStyleBackColor = true;
            btnConectar.Click += btnConectar_Click;
            // 
            // btnMensagem
            // 
            btnMensagem.ForeColor = SystemColors.ActiveCaptionText;
            btnMensagem.Location = new Point(473, 433);
            btnMensagem.Margin = new Padding(3, 2, 3, 2);
            btnMensagem.Name = "btnMensagem";
            btnMensagem.Size = new Size(82, 22);
            btnMensagem.TabIndex = 4;
            btnMensagem.Text = "Enviar";
            btnMensagem.UseVisualStyleBackColor = true;
            btnMensagem.Click += btnMensagem_Click;
            // 
            // txtMensagem
            // 
            txtMensagem.Location = new Point(106, 435);
            txtMensagem.Margin = new Padding(3, 2, 3, 2);
            txtMensagem.Name = "txtMensagem";
            txtMensagem.Size = new Size(327, 23);
            txtMensagem.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.ForeColor = Color.DarkGreen;
            label2.Location = new Point(27, 437);
            label2.Name = "label2";
            label2.Size = new Size(71, 15);
            label2.TabIndex = 5;
            label2.Text = "Mensagem:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.ForeColor = Color.DarkGreen;
            label3.Location = new Point(65, 410);
            label3.Name = "label3";
            label3.Size = new Size(44, 15);
            label3.TabIndex = 7;
            label3.Text = "Nome:";
            // 
            // txtNomeCliente
            // 
            txtNomeCliente.Location = new Point(114, 407);
            txtNomeCliente.Name = "txtNomeCliente";
            txtNomeCliente.Size = new Size(100, 23);
            txtNomeCliente.TabIndex = 8;
            txtNomeCliente.TextChanged += txtNomeCliente_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.ForeColor = Color.DarkGreen;
            label4.Location = new Point(219, 408);
            label4.Name = "label4";
            label4.Size = new Size(29, 15);
            label4.TabIndex = 9;
            label4.Text = "Cor:";
            // 
            // cmbcor
            // 
            cmbcor.FormattingEnabled = true;
            cmbcor.Location = new Point(258, 406);
            cmbcor.Name = "cmbcor";
            cmbcor.Size = new Size(121, 23);
            cmbcor.TabIndex = 10;
            cmbcor.SelectedIndexChanged += cmbcor_SelectedIndexChanged;
            // 
            // txtInfo
            // 
            txtInfo.Location = new Point(65, 126);
            txtInfo.Name = "txtInfo";
            txtInfo.Size = new Size(492, 247);
            txtInfo.TabIndex = 11;
            txtInfo.Text = "";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            label5.ForeColor = Color.DarkGreen;
            label5.Location = new Point(27, 386);
            label5.Name = "label5";
            label5.Size = new Size(63, 20);
            label5.TabIndex = 12;
            label5.Text = "Usuário";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            label6.ForeColor = Color.DarkGreen;
            label6.Location = new Point(27, 69);
            label6.Name = "label6";
            label6.Size = new Size(68, 20);
            label6.TabIndex = 13;
            label6.Text = "Servidor";
            // 
            // btnAnexo
            // 
            btnAnexo.Enabled = false;
            btnAnexo.ForeColor = SystemColors.ActiveCaptionText;
            btnAnexo.Location = new Point(436, 433);
            btnAnexo.Margin = new Padding(2, 1, 2, 1);
            btnAnexo.Name = "btnAnexo";
            btnAnexo.Size = new Size(32, 22);
            btnAnexo.TabIndex = 14;
            btnAnexo.Text = "!";
            btnAnexo.UseVisualStyleBackColor = true;
            btnAnexo.Click += btnAnexo_Click;
            // 
            // label7
            // 
            label7.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label7.ForeColor = Color.DarkGreen;
            label7.Location = new Point(26, 17);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(81, 33);
            label7.TabIndex = 15;
            label7.Text = "SEMA";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            label8.ForeColor = Color.DarkGreen;
            label8.Location = new Point(175, 17);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(312, 25);
            label8.TabIndex = 16;
            label8.Text = "Canal de conversa Interno (Cliente)";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 7F);
            label9.ForeColor = Color.DarkGreen;
            label9.Location = new Point(244, 44);
            label9.Margin = new Padding(2, 0, 2, 0);
            label9.Name = "label9";
            label9.Size = new Size(152, 12);
            label9.TabIndex = 17;
            label9.Text = "Desenvolvido pelo time Dev Unip";
            // 
            // btnDesconectar
            // 
            btnDesconectar.Enabled = false;
            btnDesconectar.ForeColor = SystemColors.ActiveCaptionText;
            btnDesconectar.Location = new Point(406, 94);
            btnDesconectar.Margin = new Padding(2, 1, 2, 1);
            btnDesconectar.Name = "btnDesconectar";
            btnDesconectar.Size = new Size(83, 22);
            btnDesconectar.TabIndex = 18;
            btnDesconectar.Text = "Desconectar";
            btnDesconectar.UseVisualStyleBackColor = true;
            btnDesconectar.Click += btnDesconectar_Click;
            // 
            // btnSelecionarArquivo
            // 
            btnSelecionarArquivo.ForeColor = SystemColors.ActiveCaptionText;
            btnSelecionarArquivo.Location = new Point(385, 404);
            btnSelecionarArquivo.Name = "btnSelecionarArquivo";
            btnSelecionarArquivo.Size = new Size(75, 26);
            btnSelecionarArquivo.TabIndex = 19;
            btnSelecionarArquivo.Text = "Pasta Anexo";
            btnSelecionarArquivo.UseVisualStyleBackColor = true;
            btnSelecionarArquivo.Click += btnSelecionarArquivo_Click;
            // 
            // txtPastaAnexo
            // 
            txtPastaAnexo.Enabled = false;
            txtPastaAnexo.Location = new Point(466, 405);
            txtPastaAnexo.Name = "txtPastaAnexo";
            txtPastaAnexo.Size = new Size(100, 23);
            txtPastaAnexo.TabIndex = 20;
            // 
            // TCPCliente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(192, 255, 192);
            ClientSize = new Size(605, 470);
            Controls.Add(txtPastaAnexo);
            Controls.Add(btnSelecionarArquivo);
            Controls.Add(btnDesconectar);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(btnAnexo);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(txtInfo);
            Controls.Add(cmbcor);
            Controls.Add(label4);
            Controls.Add(txtNomeCliente);
            Controls.Add(label3);
            Controls.Add(txtMensagem);
            Controls.Add(label2);
            Controls.Add(btnMensagem);
            Controls.Add(btnConectar);
            Controls.Add(txtIP);
            Controls.Add(label1);
            ForeColor = SystemColors.ControlLightLight;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "TCPCliente";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TCP/IP Cliente";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtIP;
        private Button btnConectar;
        private Button btnMensagem;
        private TextBox txtMensagem;
        private Label label2;
        private Label label3;
        private TextBox txtNomeCliente;
        private Label label4;
        private ComboBox cmbcor;
        private RichTextBox txtInfo;
        private Label label5;
        private Label label6;
        private Button btnAnexo;
        private Label label7;
        private Label label8;
        private Label label9;
        private Button btnDesconectar;
        private Button btnSelecionarArquivo;
        private TextBox txtPastaAnexo;
    }
}
