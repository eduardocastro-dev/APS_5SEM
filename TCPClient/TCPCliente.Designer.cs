namespace TCPClient
{
    partial class TCPCliente
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
            btnConectar = new Button();
            txtInfo = new TextBox();
            btnMensagem = new Button();
            txtMensagem = new TextBox();
            label2 = new Label();
            label3 = new Label();
            txtNomeCliente = new TextBox();
            label4 = new Label();
            cmbcor = new ComboBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 18);
            label1.Name = "label1";
            label1.Size = new Size(53, 15);
            label1.TabIndex = 0;
            label1.Text = "Servidor:";
            // 
            // txtIP
            // 
            txtIP.Location = new Point(103, 16);
            txtIP.Margin = new Padding(3, 2, 3, 2);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(163, 23);
            txtIP.TabIndex = 1;
            txtIP.Text = "127.0.0.1:9000";
            txtIP.TextChanged += txtIP_TextChanged;
            // 
            // btnConectar
            // 
            btnConectar.Location = new Point(493, 321);
            btnConectar.Margin = new Padding(3, 2, 3, 2);
            btnConectar.Name = "btnConectar";
            btnConectar.Size = new Size(90, 22);
            btnConectar.TabIndex = 2;
            btnConectar.Text = "Conectar-se";
            btnConectar.UseVisualStyleBackColor = true;
            btnConectar.Click += btnConectar_Click;
            // 
            // txtInfo
            // 
            txtInfo.Location = new Point(103, 40);
            txtInfo.Margin = new Padding(3, 2, 3, 2);
            txtInfo.Multiline = true;
            txtInfo.Name = "txtInfo";
            txtInfo.ReadOnly = true;
            txtInfo.ScrollBars = ScrollBars.Both;
            txtInfo.Size = new Size(480, 252);
            txtInfo.TabIndex = 3;
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
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(272, 18);
            label3.Name = "label3";
            label3.Size = new Size(43, 15);
            label3.TabIndex = 7;
            label3.Text = "Nome:";
            // 
            // txtNomeCliente
            // 
            txtNomeCliente.Location = new Point(321, 15);
            txtNomeCliente.Name = "txtNomeCliente";
            txtNomeCliente.Size = new Size(100, 23);
            txtNomeCliente.TabIndex = 8;
            txtNomeCliente.TextChanged += txtNomeCliente_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(427, 19);
            label4.Name = "label4";
            label4.Size = new Size(29, 15);
            label4.TabIndex = 9;
            label4.Text = "Cor:";
            // 
            // cmbcor
            // 
            cmbcor.FormattingEnabled = true;
            cmbcor.Location = new Point(462, 16);
            cmbcor.Name = "cmbcor";
            cmbcor.Size = new Size(121, 23);
            cmbcor.TabIndex = 10;
            cmbcor.SelectedIndexChanged += cmbcor_SelectedIndexChanged;
            // 
            // TCPCliente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(615, 356);
            Controls.Add(cmbcor);
            Controls.Add(label4);
            Controls.Add(txtNomeCliente);
            Controls.Add(label3);
            Controls.Add(txtMensagem);
            Controls.Add(label2);
            Controls.Add(btnMensagem);
            Controls.Add(txtInfo);
            Controls.Add(btnConectar);
            Controls.Add(txtIP);
            Controls.Add(label1);
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
        private TextBox txtInfo;
        private Button btnMensagem;
        private TextBox txtMensagem;
        private Label label2;
        private Label label3;
        private TextBox txtNomeCliente;
        private Label label4;
        private ComboBox cmbcor;
    }
}
