using SuperSimpleTcp;
using System.Text;

namespace TCPServidor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        SimpleTcpServer servidor;
        private void btnIniciar_Click(object sender, EventArgs e)
        {
            servidor.Start();
            txtInfo.Text += $"Servidor Iniciado...{Environment.NewLine}";
            btnIniciar.Enabled = false;
            btnMensagem.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnMensagem.Enabled = false;
            servidor = new SimpleTcpServer(txtIP.Text);
            servidor.Events.ClientConnected += Events_ClientConnected;
            servidor.Events.ClientDisconnected += Events_ClientDisconnected;
            servidor.Events.DataReceived += Events_DataReceived;
        }

        private void Events_DataReceived(object? sender, DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                string mensagem = $"{e.IpPort}: {Encoding.UTF8.GetString(e.Data)}{Environment.NewLine}";
                txtInfo.Text += mensagem;

                // Envia a mensagem para todos os clientes conectados, exceto o remetente
                foreach (string ipPort in listClienteIP.Items)
                {
                    if (ipPort != e.IpPort)
                    {
                        servidor.Send(ipPort, mensagem);
                    }
                }
            });
        }

        private void Events_ClientDisconnected(object? sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                txtInfo.Text += $"{e.IpPort} Se desconectou...{Environment.NewLine}";
                listClienteIP.Items.Remove(e.IpPort);
            });

        }

        private void Events_ClientConnected(object? sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                txtInfo.Text += $"{e.IpPort} Se conectou...{Environment.NewLine}";
                listClienteIP.Items.Add(e.IpPort);
            });
        }

        private void btnMensagem_Click(object sender, EventArgs e)
        {
            if (servidor.IsListening)
            {
                if (!string.IsNullOrEmpty(txtMensagem.Text)) // Verifica se a mensagem não está vazia
                {
                    // Envia a mensagem para todos os clientes conectados
                    foreach (string ipPort in listClienteIP.Items)
                    {
                        servidor.Send(ipPort, txtMensagem.Text);
                    }

                    txtInfo.Text += $"Servidor: {txtMensagem.Text}{Environment.NewLine}";
                    txtMensagem.Text = string.Empty;
                }
            }
        }
    }
}