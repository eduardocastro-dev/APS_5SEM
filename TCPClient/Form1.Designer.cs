namespace TCPClient
{
    partial class Form1
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
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 24);
            label1.Name = "label1";
            label1.Size = new Size(67, 20);
            label1.TabIndex = 0;
            label1.Text = "Servidor:";
            // 
            // txtIP
            // 
            txtIP.Location = new Point(118, 21);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(548, 27);
            txtIP.TabIndex = 1;
            txtIP.Text = "127.0.0.1:9000";
            txtIP.TextChanged += txtIP_TextChanged;
            // 
            // btnConectar
            // 
            btnConectar.Location = new Point(563, 428);
            btnConectar.Name = "btnConectar";
            btnConectar.Size = new Size(103, 29);
            btnConectar.TabIndex = 2;
            btnConectar.Text = "Conectar-se";
            btnConectar.UseVisualStyleBackColor = true;
            btnConectar.Click += btnConectar_Click;
            // 
            // txtInfo
            // 
            txtInfo.Location = new Point(118, 54);
            txtInfo.Multiline = true;
            txtInfo.Name = "txtInfo";
            txtInfo.ReadOnly = true;
            txtInfo.ScrollBars = ScrollBars.Both;
            txtInfo.Size = new Size(548, 334);
            txtInfo.TabIndex = 3;
            // 
            // btnMensagem
            // 
            btnMensagem.Location = new Point(462, 428);
            btnMensagem.Name = "btnMensagem";
            btnMensagem.Size = new Size(94, 29);
            btnMensagem.TabIndex = 4;
            btnMensagem.Text = "Enviar";
            btnMensagem.UseVisualStyleBackColor = true;
            btnMensagem.Click += btnMensagem_Click;
            // 
            // txtMensagem
            // 
            txtMensagem.Location = new Point(118, 395);
            txtMensagem.Name = "txtMensagem";
            txtMensagem.Size = new Size(548, 27);
            txtMensagem.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(27, 398);
            label2.Name = "label2";
            label2.Size = new Size(85, 20);
            label2.TabIndex = 5;
            label2.Text = "Mensagem:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(703, 475);
            Controls.Add(txtMensagem);
            Controls.Add(label2);
            Controls.Add(btnMensagem);
            Controls.Add(txtInfo);
            Controls.Add(btnConectar);
            Controls.Add(txtIP);
            Controls.Add(label1);
            MaximizeBox = false;
            Name = "Form1";
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
    }
}
