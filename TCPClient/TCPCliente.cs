using SuperSimpleTcp;
using System.Text;

namespace TCPClient
{
    public partial class TCPCliente : Form
    {
        /// <summary>
        /// Construtor da classe TCPCliente.
        /// Inicializa os componentes da interface gráfica.
        /// </summary>
        public TCPCliente()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Instância do cliente TCP.
        /// </summary>
        SimpleTcpClient cliente;

        /// <summary>
        /// Evento de clique do botão "Conectar".
        /// Conecta o cliente ao servidor, envia o nome e cor selecionados e habilita o botão "Mensagem".
        /// </summary>
        private void btnConectar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNomeCliente.Text))
            {
                MessageBox.Show("Por Favor, insira um nome de Usuário", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// Evento de clique do botão "Mensagem".
        /// Envia a mensagem digitada pelo cliente ao servidor.
        /// </summary>
        private void btnMensagem_Click(object sender, EventArgs e)
        {
            if (cliente.IsConnected)
            {
                if (!string.IsNullOrEmpty(txtMensagem.Text)) // Verifica se a mensagem não está vazia.
                {
                    string mensagemCompleta = $"{txtNomeCliente.Text}: {txtMensagem.Text}";
                    cliente.Send(mensagemCompleta);

                    // Obtém a cor selecionada no ComboBox
                    Color corSelecionada = GetColorFromComboBox(cmbcor.SelectedItem.ToString());

                    // Adiciona a mensagem ao RichTextBox com a cor selecionada no ●
                    AppendTextToRichTextBox(txtInfo, $" ● ", corSelecionada);
                    AppendTextToRichTextBox(txtInfo, $"{mensagemCompleta}{Environment.NewLine}", txtInfo.ForeColor); // Mantém a cor do texto padrão
                    txtMensagem.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// Evento de carregamento do formulário.
        /// Inicializa o cliente TCP, configura os eventos do cliente e preenche o ComboBox com as opções de cores.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            cliente = new(txtIP.Text);
            cliente.Events.Connected += Events_Connected;
            cliente.Events.Disconnected += Events_Disconnected;
            cliente.Events.DataReceived += Events_DataReceived;
            btnMensagem.Enabled = false;

            //Cores para cmbCor
            cmbcor.Items.Add("Black");
            cmbcor.Items.Add("Blue");
            cmbcor.Items.Add("Red");
            cmbcor.Items.Add("Green");
            cmbcor.Items.Add("Yellow");
            cmbcor.Items.Add("Pink");
            cmbcor.Items.Add("Orange");
            cmbcor.Items.Add("Brown");
            cmbcor.Items.Add("Gray");
            cmbcor.Items.Add("Purple");
                


            //Cor padrão "Preto"
            cmbcor.SelectedIndex = 0;
        }

        /// <summary>
        /// Evento acionado quando o cliente recebe dados do servidor.
        /// Exibe a mensagem recebida no RichTextBox "txtInfo".
        /// </summary>
        private void Events_DataReceived(object? sender, DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                AppendTextToRichTextBox(txtInfo, $"{Encoding.UTF8.GetString(e.Data)}{Environment.NewLine}", txtInfo.ForeColor);
            });

        }

        /// <summary>
        /// Evento acionado quando o cliente se desconecta do servidor.
        /// Exibe uma mensagem no RichTextBox "txtInfo" informando a desconexão.
        /// </summary>
        private void Events_Disconnected(object? sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                AppendTextToRichTextBox(txtInfo, $"Conexão encerrada... {Environment.NewLine}", Color.Black);
                btnMensagem.Enabled = false;
                btnConectar.Enabled = true;

            });

        }

        /// <summary>
        /// Evento acionado quando o cliente se conecta ao servidor.
        /// Exibe uma mensagem no RichTextBox "txtInfo" informando a conexão.
        /// </summary>
        private void Events_Connected(object? sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                AppendTextToRichTextBox(txtInfo, $"Conexão estabelecida... {Environment.NewLine}", Color.Black);
            });
        }

        /// <summary>
        /// Evento de alteração de texto do TextBox "IP".
        /// Não possui nenhuma ação implementada.
        /// </summary>
        private void txtIP_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Evento de mudança de seleção do ComboBox "Cor".
        /// Define a cor do texto do RichTextBox "txtInfo" de acordo com a cor selecionada.
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
        /// Evento de alteração de texto do TextBox "Nome do Cliente".
        /// Não possui nenhuma ação implementada.
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

        /// <summary>
        /// Adiciona texto ao RichTextBox com a cor especificada.
        /// </summary>
        /// <param name="richTextBox">O RichTextBox para adicionar o texto.</param>
        /// <param name="text">O texto para adicionar.</param>
        /// <param name="color">A cor do texto.</param>
        private Color GetColorFromComboBox(string corString)
        {
            switch (corString)
            {
                case "Black": return Color.Black;
                case "Blue": return Color.Blue;
                case "Red": return Color.Red;
                case "Green": return Color.Green;
                case "Yellow": return Color.Yellow;
                case "Pink": return Color.Pink;
                case "Orange": return Color.Orange;
                case "Brown": return Color.Brown;
                case "Gray": return Color.Gray;
                case "Purple": return Color.Purple;
                default: return Color.Black; // Cor padrão se a string não for reconhecida
            }
        }

        /// <summary>
        /// Adiciona texto ao RichTextBox com a cor especificada.
        /// </summary>
        /// <param name="richTextBox">O RichTextBox para adicionar o texto.</param>
        /// <param name="text">O texto para adicionar.</param>
        /// <param name="color">A cor do texto.</param>
        private void AppendTextToRichTextBox(RichTextBox richTextBox, string text, Color color)
        {
            richTextBox.SelectionStart = richTextBox.TextLength;
            richTextBox.SelectionColor = color;
            richTextBox.AppendText(text);
            richTextBox.SelectionColor = richTextBox.ForeColor;
        }
    }
}