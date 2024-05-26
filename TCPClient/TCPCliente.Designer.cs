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
            label1.Location = new Point(121, 207);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(127, 32);
            label1.TabIndex = 0;
            label1.Text = "Endereço:";
            // 
            // txtIP
            // 
            txtIP.Location = new Point(267, 205);
            txtIP.Margin = new Padding(6, 4, 6, 4);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(299, 39);
            txtIP.TabIndex = 1;
            txtIP.TextChanged += txtIP_TextChanged;
            // 
            // btnConectar
            // 
            btnConectar.ForeColor = SystemColors.ActiveCaptionText;
            btnConectar.Location = new Point(578, 201);
            btnConectar.Margin = new Padding(6, 4, 6, 4);
            btnConectar.Name = "btnConectar";
            btnConectar.Size = new Size(167, 47);
            btnConectar.TabIndex = 2;
            btnConectar.Text = "Conectar";
            btnConectar.UseVisualStyleBackColor = true;
            btnConectar.Click += btnConectar_Click;
            // 
            // btnMensagem
            // 
            btnMensagem.ForeColor = SystemColors.ActiveCaptionText;
            btnMensagem.Location = new Point(878, 924);
            btnMensagem.Margin = new Padding(6, 4, 6, 4);
            btnMensagem.Name = "btnMensagem";
            btnMensagem.Size = new Size(152, 47);
            btnMensagem.TabIndex = 4;
            btnMensagem.Text = "Enviar";
            btnMensagem.UseVisualStyleBackColor = true;
            btnMensagem.Click += btnMensagem_Click;
            // 
            // txtMensagem
            // 
            txtMensagem.Location = new Point(197, 928);
            txtMensagem.Margin = new Padding(6, 4, 6, 4);
            txtMensagem.Name = "txtMensagem";
            txtMensagem.Size = new Size(604, 39);
            txtMensagem.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.ForeColor = Color.DarkGreen;
            label2.Location = new Point(50, 932);
            label2.Margin = new Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new Size(146, 32);
            label2.TabIndex = 5;
            label2.Text = "Mensagem:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.ForeColor = Color.DarkGreen;
            label3.Location = new Point(121, 875);
            label3.Margin = new Padding(6, 0, 6, 0);
            label3.Name = "label3";
            label3.Size = new Size(90, 32);
            label3.TabIndex = 7;
            label3.Text = "Nome:";
            // 
            // txtNomeCliente
            // 
            txtNomeCliente.Location = new Point(212, 868);
            txtNomeCliente.Margin = new Padding(6, 6, 6, 6);
            txtNomeCliente.Name = "txtNomeCliente";
            txtNomeCliente.Size = new Size(182, 39);
            txtNomeCliente.TabIndex = 8;
            txtNomeCliente.TextChanged += txtNomeCliente_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.ForeColor = Color.DarkGreen;
            label4.Location = new Point(405, 871);
            label4.Margin = new Padding(6, 0, 6, 0);
            label4.Name = "label4";
            label4.Size = new Size(62, 32);
            label4.TabIndex = 9;
            label4.Text = "Cor:";
            // 
            // cmbcor
            // 
            cmbcor.FormattingEnabled = true;
            cmbcor.Location = new Point(479, 866);
            cmbcor.Margin = new Padding(6, 6, 6, 6);
            cmbcor.Name = "cmbcor";
            cmbcor.Size = new Size(151, 40);
            cmbcor.TabIndex = 10;
            cmbcor.SelectedIndexChanged += cmbcor_SelectedIndexChanged;
            // 
            // txtInfo
            // 
            txtInfo.Location = new Point(121, 269);
            txtInfo.Margin = new Padding(6, 6, 6, 6);
            txtInfo.Name = "txtInfo";
            txtInfo.Size = new Size(910, 522);
            txtInfo.TabIndex = 11;
            txtInfo.Text = "";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            label5.ForeColor = Color.DarkGreen;
            label5.Location = new Point(50, 823);
            label5.Margin = new Padding(6, 0, 6, 0);
            label5.Name = "label5";
            label5.Size = new Size(126, 41);
            label5.TabIndex = 12;
            label5.Text = "Usuário";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            label6.ForeColor = Color.DarkGreen;
            label6.Location = new Point(50, 147);
            label6.Margin = new Padding(6, 0, 6, 0);
            label6.Name = "label6";
            label6.Size = new Size(138, 41);
            label6.TabIndex = 13;
            label6.Text = "Servidor";
            // 
            // btnAnexo
            // 
            btnAnexo.Enabled = false;
            btnAnexo.ForeColor = SystemColors.ActiveCaptionText;
            btnAnexo.Location = new Point(810, 924);
            btnAnexo.Margin = new Padding(4, 2, 4, 2);
            btnAnexo.Name = "btnAnexo";
            btnAnexo.Size = new Size(59, 47);
            btnAnexo.TabIndex = 14;
            btnAnexo.UseVisualStyleBackColor = true;
            btnAnexo.Click += btnAnexo_Click;
            // 
            // label7
            // 
            label7.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label7.ForeColor = Color.DarkGreen;
            label7.Location = new Point(48, 36);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(150, 70);
            label7.TabIndex = 15;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            label8.ForeColor = Color.DarkGreen;
            label8.Location = new Point(325, 36);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(599, 47);
            label8.TabIndex = 16;
            label8.Text = "Canal de conversa Interno (Cliente)";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 7F);
            label9.ForeColor = Color.DarkGreen;
            label9.Location = new Point(453, 94);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(291, 25);
            label9.TabIndex = 17;
            label9.Text = "Desenvolvido pelo time Dev Unip";
            // 
            // btnDesconectar
            // 
            btnDesconectar.Enabled = false;
            btnDesconectar.ForeColor = SystemColors.ActiveCaptionText;
            btnDesconectar.Location = new Point(754, 201);
            btnDesconectar.Margin = new Padding(4, 2, 4, 2);
            btnDesconectar.Name = "btnDesconectar";
            btnDesconectar.Size = new Size(154, 47);
            btnDesconectar.TabIndex = 18;
            btnDesconectar.Text = "Desconectar";
            btnDesconectar.UseVisualStyleBackColor = true;
            btnDesconectar.Click += btnDesconectar_Click;
            // 
            // btnSelecionarArquivo
            // 
            btnSelecionarArquivo.ForeColor = SystemColors.ActiveCaptionText;
            btnSelecionarArquivo.Location = new Point(642, 864);
            btnSelecionarArquivo.Margin = new Padding(6, 6, 6, 6);
            btnSelecionarArquivo.Name = "btnSelecionarArquivo";
            btnSelecionarArquivo.Size = new Size(139, 43);
            btnSelecionarArquivo.TabIndex = 19;
            btnSelecionarArquivo.Text = "Pasta Anexo";
            btnSelecionarArquivo.UseVisualStyleBackColor = true;
            btnSelecionarArquivo.Click += btnSelecionarArquivo_Click;
            // 
            // txtPastaAnexo
            // 
            txtPastaAnexo.Enabled = false;
            txtPastaAnexo.Location = new Point(793, 868);
            txtPastaAnexo.Margin = new Padding(6, 6, 6, 6);
            txtPastaAnexo.Name = "txtPastaAnexo";
            txtPastaAnexo.Size = new Size(238, 39);
            txtPastaAnexo.TabIndex = 20;
            // 
            // TCPCliente
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(192, 255, 192);
            ClientSize = new Size(1148, 1003);
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
            Margin = new Padding(6, 4, 6, 4);
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
