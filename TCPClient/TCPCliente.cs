using SuperSimpleTcp;
using System.Text;

namespace TCPClient
{
    public partial class TCPCliente : Form
    {
        /// <summary>
        /// Construtor da classe TCPCliente.
        /// Inicializa os componentes da interface gr�fica.
        /// </summary>
        public TCPCliente()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Inst�ncia do cliente TCP.
        /// </summary>
        SimpleTcpClient cliente;

        /// <summary>
        /// Evento de clique do bot�o "Conectar".
        /// Conecta o cliente ao servidor, envia o nome e cor selecionados e habilita o bot�o "Mensagem".
        /// </summary>
        private void btnConectar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNomeCliente.Text))
            {
                MessageBox.Show("Por Favor, insira um nome de Usu�rio", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                cliente.Connect();
                //Envia dados do cliente ao conectar
                EnviarNomeECor();
                btnMensagem.Enabled = true;
                btnConectar.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Evento de clique do bot�o "Mensagem".
        /// Envia a mensagem digitada pelo cliente ao servidor.
        /// </summary>
        private void btnMensagem_Click(object sender, EventArgs e)
        {
            if (cliente.IsConnected)
            {
                if (!string.IsNullOrEmpty(txtMensagem.Text)) // Verifica se a mensagem n�o est� vazia.
                {
                    string mensagemCompleta = $"{txtNomeCliente.Text}: {txtMensagem.Text}";
                    cliente.Send(mensagemCompleta);

                    txtInfo.Text += $"Enviado: {mensagemCompleta}{Environment.NewLine}";
                    txtMensagem.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// Evento de carregamento do formul�rio.
        /// Inicializa o cliente TCP, configura os eventos do cliente e preenche o ComboBox com as op��es de cores.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            cliente = new(txtIP.Text);
            cliente.Events.Connected += Events_Connected;
            cliente.Events.Disconnected += Events_Disconnected;
            cliente.Events.DataReceived += Events_DataReceived;
            btnMensagem.Enabled = false;

            //Cores para cmbCor
            cmbcor.Items.Add("Preto");
            cmbcor.Items.Add("Azul");
            cmbcor.Items.Add("Vermelho");
            cmbcor.Items.Add("Verde");
            cmbcor.Items.Add("Amarelo");
            cmbcor.Items.Add("Rosa");
            cmbcor.Items.Add("Laranja");
            cmbcor.Items.Add("Marrom");
            cmbcor.Items.Add("Cinza");
            cmbcor.Items.Add("Violeta");

            //Cor padr�o "Preto"
            cmbcor.SelectedIndex = 0;
        }

        /// <summary>
        /// Evento acionado quando o cliente recebe dados do servidor.
        /// Exibe a mensagem recebida no TextBox "txtInfo".
        /// </summary>
        private void Events_DataReceived(object? sender, DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                txtInfo.Text += $"Recebida: {Encoding.UTF8.GetString(e.Data)}{Environment.NewLine}";
            });

        }

        /// <summary>
        /// Evento acionado quando o cliente se desconecta do servidor.
        /// Exibe uma mensagem no TextBox "txtInfo" informando a desconex�o.
        /// </summary>
        private void Events_Disconnected(object? sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                txtInfo.Text += $"Conex�o encerrada... {Environment.NewLine}";
                btnMensagem.Enabled = false;
                btnConectar.Enabled = true;

            });

        }

        /// <summary>
        /// Evento acionado quando o cliente se conecta ao servidor.
        /// Exibe uma mensagem no TextBox "txtInfo" informando a conex�o.
        /// </summary>
        private void Events_Connected(object? sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                txtInfo.Text += $"Conex�o estabelecida... {Environment.NewLine}";
            });
        }

        /// <summary>
        /// Evento de altera��o de texto do TextBox "IP".
        /// N�o possui nenhuma a��o implementada.
        /// </summary>
        private void txtIP_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Evento de mudan�a de sele��o do ComboBox "Cor".
        /// Define a cor do texto do TextBox "txtInfo" de acordo com a cor selecionada.
        /// </summary>
        private void cmbcor_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Define a cor da mensagem do cliente
            switch (cmbcor.SelectedItem.ToString())
            {
                case "Preto":
                    txtInfo.ForeColor = Color.Black;
                    break;
                case "Azul":
                    txtInfo.ForeColor = Color.Blue;
                    break;
                case "Vermelho":
                    txtInfo.ForeColor = Color.Red;
                    break;
                case "Verde":
                    txtInfo.ForeColor = Color.Green;
                    break;
                case "Amarelo":
                    txtInfo.ForeColor = Color.Yellow;
                    break;
                case "Rosa":
                    txtInfo.ForeColor = Color.Pink;
                    break;
                case "Laranja":
                    txtInfo.ForeColor = Color.Orange;
                    break;
                case "Marrom":
                    txtInfo.ForeColor = Color.Brown;
                    break;
                case "Cinza":
                    txtInfo.ForeColor = Color.Gray;
                    break;
                case "Violeta":
                    txtInfo.ForeColor = Color.Purple;
                    break;
            }
        }

        /// <summary>
        /// Evento de altera��o de texto do TextBox "Nome do Cliente".
        /// N�o possui nenhuma a��o implementada.
        /// </summary>
        private void txtNomeCliente_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Envia o nome e a cor selecionados pelo cliente para o servidor.
        /// </summary>
        private void EnviarNomeECor()
        {
            if (cliente.IsConnected)
            {
                string nome = txtNomeCliente.Text;
                string cor = cmbcor.SelectedItem.ToString();
                string mensagemInfo = $"Nome:{nome};Cor:{cor}";
                cliente.Send(mensagemInfo);
            }
        }
    }
}