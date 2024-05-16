using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPClient
{
    public partial class TCPCliente : Form
    {
        public TCPCliente()
        {
            InitializeComponent();
        }

        // Variáveis para o cliente e a conexão
        private Socket _clientSocket;
        private string _serverIp;
        private int _port = 9000;

        // Evento que ocorre quando o botão "Conectar" é clicado
        private async void btnConectar_Click(object sender, EventArgs e)
        {
            // Verifica se o usuário digitou um nome de usuário
            if (string.IsNullOrEmpty(txtNomeCliente.Text))
            {
                MessageBox.Show("Por Favor, insira um nome de Usuário", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Verifica se o usuário digitou o endereço IP do servidor
            if (string.IsNullOrEmpty(txtIP.Text))
            {
                MessageBox.Show("Por Favor, insira o endereço IP do servidor", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Armazena o IP do servidor
            _serverIp = txtIP.Text;

            try
            {
                // Cria um novo socket TCP
                _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Tenta se conectar ao servidor
                await Task.Run(() => _clientSocket.Connect(IPAddress.Parse(_serverIp), _port));

                // Envia o nome e a cor do cliente para o servidor
                EnviarNomeECor();

                // Habilita o botão "Mensagem" para enviar mensagens
                btnMensagem.Enabled = true;

                // Desabilita o botão "Conectar" para evitar conexões duplicadas
                btnConectar.Enabled = false;

                // Exibe uma mensagem na caixa de texto "txtInfo" informando que a conexão foi estabelecida
                AppendTextToRichTextBox(txtInfo, $"Conexão estabelecida... {Environment.NewLine}", Color.Black);

                // Inicia uma tarefa para receber mensagens do servidor
                Task.Run(ReceiveMessages);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento que ocorre quando o botão "Mensagem" é clicado
        private void btnMensagem_Click(object sender, EventArgs e)
        {
            // Verifica se a conexão com o servidor está ativa
            if (_clientSocket != null && _clientSocket.Connected)
            {
                // Verifica se o usuário digitou uma mensagem
                if (!string.IsNullOrEmpty(txtMensagem.Text))
                {
                    // Obtem a cor selecionada pelo usuário
                    string corEnv = cmbcor.SelectedItem.ToString();

                    // Cria a mensagem completa com o nome do cliente, a cor e a mensagem
                    string mensagemCompleta = $"●:{corEnv}:{txtNomeCliente.Text}:{txtMensagem.Text}";

                    // Envia a mensagem para o servidor
                    Send(mensagemCompleta);

                    // Obtem a cor correspondente à seleção do usuário
                    Color corSelecionada = GetColorFromComboBox(cmbcor.SelectedItem.ToString());

                    // Exibe o círculo com a cor da mensagem na caixa de texto "txtInfo"
                    AppendTextToRichTextBox(txtInfo, $" ● ", corSelecionada);

                    // Exibe a mensagem do cliente na caixa de texto "txtInfo"
                    AppendTextToRichTextBox(txtInfo, $"{txtNomeCliente.Text}: {txtMensagem.Text}{Environment.NewLine}", Color.Black);

                    // Limpa a caixa de texto "txtMensagem"
                    txtMensagem.Text = string.Empty;
                }
            }
        }

        // Evento que ocorre quando o formulário é carregado
        private void Form1_Load(object sender, EventArgs e)
        {
            // Desabilita o botão "Mensagem" até que a conexão seja estabelecida
            btnMensagem.Enabled = false;

            // Adiciona as cores disponíveis na caixa de combinação "cmbcor"
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

            // Define a cor padrão da caixa de combinação como "Preto"
            cmbcor.SelectedIndex = 0;
        }

        // Método para receber mensagens do servidor
        private async Task ReceiveMessages()
        {
            while (_clientSocket.Connected)
            {
                try
                {
                    // Cria um buffer para receber os dados
                    byte[] buffer = new byte[1024];

                    // Recebe dados do servidor
                    int bytesReceived = await Task.Run(() => _clientSocket.Receive(buffer));

                    // Se nenhum dado for recebido, o servidor desconectou
                    if (bytesReceived == 0)
                    {
                        // Exibe uma mensagem na caixa de texto "txtInfo" informando que a conexão foi encerrada
                        AppendTextToRichTextBox(txtInfo, $"Conexão encerrada... {Environment.NewLine}", Color.Black);

                        // Desabilita o botão "Mensagem"
                        btnMensagem.Enabled = false;

                        // Habilita o botão "Conectar"
                        btnConectar.Enabled = true;

                        // Fecha a conexão
                        _clientSocket.Close();
                        break;
                    }

                    // Decodifica a mensagem recebida do servidor
                    string mensagemRecebida = Encoding.UTF8.GetString(buffer, 0, bytesReceived);

                    // Processa a mensagem recebida na thread principal
                    await Task.Run(() => ProcessMessage(mensagemRecebida));
                }
                catch (Exception ex)
                {
                    // Exibe uma mensagem na caixa de texto "txtInfo" informando que a conexão foi encerrada
                    AppendTextToRichTextBox(txtInfo, $"Conexão encerrada... {Environment.NewLine}", Color.Black);

                    // Desabilita o botão "Mensagem"
                    btnMensagem.Enabled = false;

                    // Habilita o botão "Conectar"
                    btnConectar.Enabled = true;

                    // Fecha a conexão
                    _clientSocket.Close();
                    break;
                }
            }
        }

        // Método para processar a mensagem recebida
        private void ProcessMessage(string mensagemRecebida)
        {
            // Atualiza o RichTextBox na thread principal usando Invoke
            this.Invoke((MethodInvoker)delegate
            {
                string[] partesMensagem = mensagemRecebida.Split(':');

                if (partesMensagem.Length >= 4)
                {
                    string corCirculo = partesMensagem[1].TrimStart('●');
                    string nomeServidor = partesMensagem[2].Trim();
                    string mensagem = partesMensagem[3].Trim();

                    AppendTextToRichTextBox(txtInfo, $" ● ", Color.FromName(corCirculo));
                    AppendTextToRichTextBox(txtInfo, $"{nomeServidor}: {mensagem}{Environment.NewLine}", Color.Black);
                }
            });
        }

        // Método para enviar uma mensagem para o servidor
        private void Send(string message)
        {
            try
            {
                // Converte a mensagem para bytes
                byte[] data = Encoding.UTF8.GetBytes(message);

                // Envia a mensagem para o servidor
                _clientSocket.Send(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento que ocorre quando o texto na caixa de texto "txtIP" é alterado
        private void txtIP_TextChanged(object sender, EventArgs e)
        {

        }

        // Evento que ocorre quando a seleção na caixa de combinação "cmbcor" é alterada
        private void cmbcor_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Define a cor do texto na caixa de texto "txtInfo" de acordo com a cor selecionada
            switch (cmbcor.SelectedItem.ToString())
            {
                case "Black":
                    txtInfo.ForeColor = Color.Black;
                    break;
                case "Blue":
                    txtInfo.ForeColor = Color.Blue;
                    break;
                case "Red":
                    txtInfo.ForeColor = Color.Red;
                    break;
                case "Green":
                    txtInfo.ForeColor = Color.Green;
                    break;
                case "Yellow":
                    txtInfo.ForeColor = Color.Yellow;
                    break;
                case "Pink":
                    txtInfo.ForeColor = Color.Pink;
                    break;
                case "Orange":
                    txtInfo.ForeColor = Color.Orange;
                    break;
                case "Brown":
                    txtInfo.ForeColor = Color.Brown;
                    break;
                case "Gray":
                    txtInfo.ForeColor = Color.Gray;
                    break;
                case "Purple":
                    txtInfo.ForeColor = Color.Purple;
                    break;
            }
        }

        // Evento que ocorre quando o texto na caixa de texto "txtNomeCliente" é alterado
        private void txtNomeCliente_TextChanged(object sender, EventArgs e)
        {

        }

        // Método para enviar o nome e a cor do cliente para o servidor
        private void EnviarNomeECor()
        {
            // Obtem o nome do cliente e a cor selecionada
            string nome = txtNomeCliente.Text;
            string cor = cmbcor.SelectedItem.ToString();
            // Cria a mensagem com o nome e a cor
            string mensagemInfo = $"Nome:{nome};Cor:{cor}";
            // Envia a mensagem para o servidor
            Send(mensagemInfo);
        }

        // Método para obter a cor correspondente à seleção na caixa de combinação "cmbcor"
        private Color GetColorFromComboBox(string corString)
        {
            // Retorna a cor correspondente à string da cor
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
                default: return Color.Black;
            }
        }

        // Método para adicionar texto à caixa de texto "richTextBox" com uma cor específica
        private void AppendTextToRichTextBox(RichTextBox richTextBox, string text, Color color)
        {
            // Verifica se a operação de atualização precisa ser feita na thread principal
            if (richTextBox.InvokeRequired)
            {
                // Chama o método na thread principal se necessário
                richTextBox.Invoke((MethodInvoker)delegate
                {
                    AppendTextToRichTextBox(richTextBox, text, color);
                });
            }
            else
            {
                // Atualiza o RichTextBox diretamente se estiver na thread principal
                richTextBox.SelectionStart = richTextBox.TextLength;
                richTextBox.SelectionColor = color;
                richTextBox.AppendText(text);
                richTextBox.SelectionColor = richTextBox.ForeColor;
            }
        }
    }
}