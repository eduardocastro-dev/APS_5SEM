namespace TCPServidor
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
            btnIniciar = new Button();
            txtInfo = new TextBox();
            btnMensagem = new Button();
            txtMensagem = new TextBox();
            label2 = new Label();
            listClienteIP = new ListBox();
            label3 = new Label();
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
            // 
            // btnIniciar
            // 
            btnIniciar.Location = new Point(563, 428);
            btnIniciar.Name = "btnIniciar";
            btnIniciar.Size = new Size(103, 29);
            btnIniciar.TabIndex = 2;
            btnIniciar.Text = "Iniciar";
            btnIniciar.UseVisualStyleBackColor = true;
            btnIniciar.Click += btnIniciar_Click;
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
            // listClienteIP
            // 
            listClienteIP.FormattingEnabled = true;
            listClienteIP.Location = new Point(675, 52);
            listClienteIP.Name = "listClienteIP";
            listClienteIP.Size = new Size(297, 404);
            listClienteIP.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(675, 21);
            label3.Name = "label3";
            label3.Size = new Size(106, 20);
            label3.TabIndex = 8;
            label3.Text = "IP Conectados:";
            label3.Click += label3_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1040, 475);
            Controls.Add(label3);
            Controls.Add(listClienteIP);
            Controls.Add(txtMensagem);
            Controls.Add(label2);
            Controls.Add(btnMensagem);
            Controls.Add(txtInfo);
            Controls.Add(btnIniciar);
            Controls.Add(txtIP);
            Controls.Add(label1);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TCP/IP Servidor";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtIP;
        private Button btnIniciar;
        private TextBox txtInfo;
        private Button btnMensagem;
        private TextBox txtMensagem;
        private Label label2;
        private ListBox listClienteIP;
        private Label label3;
    }
}
