
namespace TCPServidor
{
    partial class TCPServidor
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TCPServidor));
            label1 = new Label();
            txtIP = new TextBox();
            btnIniciar = new Button();
            btnMensagem = new Button();
            txtMensagem = new TextBox();
            label2 = new Label();
            listClienteIP = new ListBox();
            label3 = new Label();
            label4 = new Label();
            txtNomeServidor = new TextBox();
            label5 = new Label();
            cmbCor = new ComboBox();
            txtInfo = new RichTextBox();
            label6 = new Label();
            btnAnexo = new Button();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            btnFecharConexao = new Button();
            btnSelecionarArquivo = new Button();
            txtPastaCompartilhada = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.ForeColor = Color.DarkGreen;
            label1.Location = new Point(141, 196);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(127, 32);
            label1.TabIndex = 0;
            label1.Text = "Endereço:";
            // 
            // txtIP
            // 
            txtIP.Enabled = false;
            txtIP.Location = new Point(271, 194);
            txtIP.Margin = new Padding(6, 4, 6, 4);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(355, 39);
            txtIP.TabIndex = 1;
            // 
            // btnIniciar
            // 
            btnIniciar.Location = new Point(639, 190);
            btnIniciar.Margin = new Padding(6, 4, 6, 4);
            btnIniciar.Name = "btnIniciar";
            btnIniciar.Size = new Size(167, 47);
            btnIniciar.TabIndex = 2;
            btnIniciar.Text = "Iniciar";
            btnIniciar.UseVisualStyleBackColor = true;
            btnIniciar.Click += btnIniciar_Click;
            // 
            // btnMensagem
            // 
            btnMensagem.Location = new Point(1174, 994);
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
            txtMensagem.Location = new Point(217, 998);
            txtMensagem.Margin = new Padding(6, 4, 6, 4);
            txtMensagem.Name = "txtMensagem";
            txtMensagem.Size = new Size(888, 39);
            txtMensagem.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.ForeColor = Color.DarkGreen;
            label2.Location = new Point(72, 1003);
            label2.Margin = new Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new Size(146, 32);
            label2.TabIndex = 5;
            label2.Text = "Mensagem:";
            // 
            // listClienteIP
            // 
            listClienteIP.FormattingEnabled = true;
            listClienteIP.Location = new Point(1174, 254);
            listClienteIP.Margin = new Padding(6, 4, 6, 4);
            listClienteIP.Name = "listClienteIP";
            listClienteIP.Size = new Size(283, 612);
            listClienteIP.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.ForeColor = Color.DarkGreen;
            label3.Location = new Point(1168, 196);
            label3.Margin = new Padding(6, 0, 6, 0);
            label3.Name = "label3";
            label3.Size = new Size(177, 32);
            label3.TabIndex = 8;
            label3.Text = "IP Conectados";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.ForeColor = Color.DarkGreen;
            label4.Location = new Point(141, 943);
            label4.Margin = new Padding(6, 0, 6, 0);
            label4.Name = "label4";
            label4.Size = new Size(90, 32);
            label4.TabIndex = 9;
            label4.Text = "Nome:";
            // 
            // txtNomeServidor
            // 
            txtNomeServidor.Location = new Point(232, 941);
            txtNomeServidor.Margin = new Padding(6);
            txtNomeServidor.Name = "txtNomeServidor";
            txtNomeServidor.Size = new Size(326, 39);
            txtNomeServidor.TabIndex = 10;
            txtNomeServidor.TextChanged += txtNomeServidor_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label5.ForeColor = Color.DarkGreen;
            label5.Location = new Point(570, 944);
            label5.Margin = new Padding(6, 0, 6, 0);
            label5.Name = "label5";
            label5.Size = new Size(62, 32);
            label5.TabIndex = 11;
            label5.Text = "Cor:";
            // 
            // cmbCor
            // 
            cmbCor.FormattingEnabled = true;
            cmbCor.Location = new Point(644, 941);
            cmbCor.Margin = new Padding(6);
            cmbCor.Name = "cmbCor";
            cmbCor.Size = new Size(162, 40);
            cmbCor.TabIndex = 12;
            cmbCor.SelectedIndexChanged += cmbCor_SelectedIndexChanged;
            // 
            // txtInfo
            // 
            txtInfo.Cursor = Cursors.IBeam;
            txtInfo.Location = new Point(145, 254);
            txtInfo.Margin = new Padding(6);
            txtInfo.Name = "txtInfo";
            txtInfo.Size = new Size(1022, 612);
            txtInfo.TabIndex = 13;
            txtInfo.Text = "";
            txtInfo.TextChanged += txtInfo_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            label6.ForeColor = Color.DarkGreen;
            label6.Location = new Point(72, 894);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(126, 41);
            label6.TabIndex = 14;
            label6.Text = "Usuário";
            // 
            // btnAnexo
            // 
            btnAnexo.Enabled = false;
            btnAnexo.Location = new Point(1114, 994);
            btnAnexo.Margin = new Padding(4, 2, 4, 2);
            btnAnexo.Name = "btnAnexo";
            btnAnexo.Size = new Size(50, 47);
            btnAnexo.TabIndex = 15;
            btnAnexo.UseVisualStyleBackColor = true;
            btnAnexo.Click += btnAnexo_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            label7.ForeColor = Color.DarkGreen;
            label7.Location = new Point(72, 134);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(138, 41);
            label7.TabIndex = 16;
            label7.Text = "Servidor";
            // 
            // label8
            // 
            label8.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label8.ForeColor = Color.DarkGreen;
            label8.Location = new Point(67, 41);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(150, 70);
            label8.TabIndex = 17;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 7F);
            label9.ForeColor = Color.DarkGreen;
            label9.Location = new Point(680, 98);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(291, 25);
            label9.TabIndex = 19;
            label9.Text = "Desenvolvido pelo time Dev Unip";
            label9.Click += label9_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            label10.ForeColor = Color.DarkGreen;
            label10.Location = new Point(552, 41);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(590, 47);
            label10.TabIndex = 18;
            label10.Text = "Canal de conversa Interno (Server)";
            // 
            // btnFecharConexao
            // 
            btnFecharConexao.Enabled = false;
            btnFecharConexao.Location = new Point(815, 190);
            btnFecharConexao.Margin = new Padding(4, 2, 4, 2);
            btnFecharConexao.Name = "btnFecharConexao";
            btnFecharConexao.Size = new Size(150, 47);
            btnFecharConexao.TabIndex = 20;
            btnFecharConexao.Text = "Fechar";
            btnFecharConexao.UseVisualStyleBackColor = true;
            btnFecharConexao.Click += btnFecharConexao_Click;
            // 
            // btnSelecionarArquivo
            // 
            btnSelecionarArquivo.Location = new Point(841, 939);
            btnSelecionarArquivo.Margin = new Padding(6);
            btnSelecionarArquivo.Name = "btnSelecionarArquivo";
            btnSelecionarArquivo.Size = new Size(156, 42);
            btnSelecionarArquivo.TabIndex = 21;
            btnSelecionarArquivo.Text = "Pasta Anexo";
            btnSelecionarArquivo.UseVisualStyleBackColor = true;
            btnSelecionarArquivo.Click += btnSelecionarArquivo_Click_1;
            // 
            // txtPastaCompartilhada
            // 
            txtPastaCompartilhada.Enabled = false;
            txtPastaCompartilhada.Location = new Point(1009, 940);
            txtPastaCompartilhada.Margin = new Padding(6);
            txtPastaCompartilhada.Name = "txtPastaCompartilhada";
            txtPastaCompartilhada.Size = new Size(448, 39);
            txtPastaCompartilhada.TabIndex = 22;
            // 
            // TCPServidor
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(192, 255, 192);
            ClientSize = new Size(1594, 1074);
            Controls.Add(txtPastaCompartilhada);
            Controls.Add(btnSelecionarArquivo);
            Controls.Add(btnFecharConexao);
            Controls.Add(label9);
            Controls.Add(label10);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(btnAnexo);
            Controls.Add(label6);
            Controls.Add(txtInfo);
            Controls.Add(cmbCor);
            Controls.Add(label5);
            Controls.Add(txtNomeServidor);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(listClienteIP);
            Controls.Add(txtMensagem);
            Controls.Add(label2);
            Controls.Add(btnMensagem);
            Controls.Add(btnIniciar);
            Controls.Add(txtIP);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(6, 4, 6, 4);
            MaximizeBox = false;
            Name = "TCPServidor";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TCP/IP Servidor";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private Label label1;
        private TextBox txtIP;
        private Button btnIniciar;
        private Button btnMensagem;
        private TextBox txtMensagem;
        private Label label2;
        private ListBox listClienteIP;
        private Label label3;
        private Label label4;
        private TextBox txtNomeServidor;
        private Label label5;
        private ComboBox cmbCor;
        private RichTextBox txtInfo;
        private Label label6;
        private Button btnAnexo;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Button btnFecharConexao;
        private Button btnSelecionarArquivo;
        private TextBox txtPastaCompartilhada;
    }
}
