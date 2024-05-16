
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
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 18);
            label1.Name = "label1";
            label1.Size = new Size(66, 15);
            label1.TabIndex = 0;
            label1.Text = "Servidor IP:";
            // 
            // txtIP
            // 
            txtIP.Enabled = false;
            txtIP.Location = new Point(103, 16);
            txtIP.Margin = new Padding(3, 2, 3, 2);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(193, 23);
            txtIP.TabIndex = 1;
            // 
            // btnIniciar
            // 
            btnIniciar.Location = new Point(493, 321);
            btnIniciar.Margin = new Padding(3, 2, 3, 2);
            btnIniciar.Name = "btnIniciar";
            btnIniciar.Size = new Size(90, 22);
            btnIniciar.TabIndex = 2;
            btnIniciar.Text = "Iniciar";
            btnIniciar.UseVisualStyleBackColor = true;
            btnIniciar.Click += btnIniciar_Click;
            // 
            // btnMensagem
            // 
            btnMensagem.Location = new Point(404, 321);
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
            txtMensagem.Location = new Point(103, 296);
            txtMensagem.Margin = new Padding(3, 2, 3, 2);
            txtMensagem.Name = "txtMensagem";
            txtMensagem.Size = new Size(480, 23);
            txtMensagem.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 298);
            label2.Name = "label2";
            label2.Size = new Size(69, 15);
            label2.TabIndex = 5;
            label2.Text = "Mensagem:";
            // 
            // listClienteIP
            // 
            listClienteIP.FormattingEnabled = true;
            listClienteIP.ItemHeight = 15;
            listClienteIP.Location = new Point(591, 39);
            listClienteIP.Margin = new Padding(3, 2, 3, 2);
            listClienteIP.Name = "listClienteIP";
            listClienteIP.Size = new Size(260, 304);
            listClienteIP.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(591, 16);
            label3.Name = "label3";
            label3.Size = new Size(86, 15);
            label3.TabIndex = 8;
            label3.Text = "IP Conectados:";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(302, 18);
            label4.Name = "label4";
            label4.Size = new Size(43, 15);
            label4.TabIndex = 9;
            label4.Text = "Nome:";
            // 
            // txtNomeServidor
            // 
            txtNomeServidor.Location = new Point(351, 15);
            txtNomeServidor.Name = "txtNomeServidor";
            txtNomeServidor.Size = new Size(100, 23);
            txtNomeServidor.TabIndex = 10;
            txtNomeServidor.TextChanged += txtNomeServidor_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(458, 18);
            label5.Name = "label5";
            label5.Size = new Size(29, 15);
            label5.TabIndex = 11;
            label5.Text = "Cor:";
            // 
            // cmbCor
            // 
            cmbCor.FormattingEnabled = true;
            cmbCor.Location = new Point(493, 16);
            cmbCor.Name = "cmbCor";
            cmbCor.Size = new Size(89, 23);
            cmbCor.TabIndex = 12;
            cmbCor.SelectedIndexChanged += cmbCor_SelectedIndexChanged;
            // 
            // txtInfo
            // 
            txtInfo.Cursor = Cursors.IBeam;
            txtInfo.Location = new Point(103, 44);
            txtInfo.Name = "txtInfo";
            txtInfo.Size = new Size(479, 247);
            txtInfo.TabIndex = 13;
            txtInfo.Text = "";
            // 
            // TCPServidor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(910, 356);
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
            Margin = new Padding(3, 2, 3, 2);
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
    }
}
