using SuperSimpleTcp;
using System.Text;

namespace TCPClient
{
    public partial class TCPCliente : Form
    {
        public TCPCliente()
        {
            InitializeComponent();
        }

        SimpleTcpClient cliente;

        private void btnConectar_Click(object sender, EventArgs e)
        {
            try
            {
                cliente.Connect();
                btnMensagem.Enabled = true;
                btnConectar.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnMensagem_Click(object sender, EventArgs e)
        {
            if (cliente.IsConnected)
            {
                if (!string.IsNullOrEmpty(txtMensagem.Text)) // Verifica se a mensagem não está vazia.
                {
                    cliente.Send(txtMensagem.Text);
                    txtInfo.Text += $"Enviado: {txtMensagem.Text}{Environment.NewLine}";
                    txtMensagem.Text = string.Empty;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cliente = new(txtIP.Text);
            cliente.Events.Connected += Events_Connected;
            cliente.Events.Disconnected += Events_Disconnected;
            cliente.Events.DataReceived += Events_DataReceived;
            btnMensagem.Enabled = false;
        }

        private void Events_DataReceived(object? sender, DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                txtInfo.Text += $"Recebida: {Encoding.UTF8.GetString(e.Data)}{Environment.NewLine}";
            });

        }

        private void Events_Disconnected(object? sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                txtInfo.Text += $"Conexão encerrada... {Environment.NewLine}";
                btnMensagem.Enabled = false;
                btnConectar.Enabled = true;

            });

        }

        private void Events_Connected(object? sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                txtInfo.Text += $"Conexão estabelecida... {Environment.NewLine}";
            });
        }

        private void txtIP_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
