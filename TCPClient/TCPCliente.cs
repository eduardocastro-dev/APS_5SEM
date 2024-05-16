using SuperSimpleTcp;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace TCPClient
{
    public partial class TCPCliente : Form
    {
        public TCPCliente()
        {
            InitializeComponent();
        }

        // Variável para armazenar a conexão do cliente com o servidor
        SimpleTcpClient cliente;

        // Evento que ocorre quando o botão "Conectar" é clicado
        private void btnConectar_Click(object sender, EventArgs e)
        {
            // Verifica se o usuário digitou um nome de usuário
            if (string.IsNullOrEmpty(txtNomeCliente.Text))
            {
                // Exibe uma mensagem de erro se o nome de usuário estiver vazio
                MessageBox.Show("Por Favor, insira um nome de Usuário", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Retorna para evitar continuar a execução do código
                return;
            }

            // Verifica se o usuário digitou o endereço IP do servidor
            if (string.IsNullOrEmpty(txtIP.Text))
            {
                // Exibe uma mensagem de erro se o endereço IP estiver vazio
                MessageBox.Show("Por Favor, insira o endereço IP do servidor", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Retorna para evitar continuar a execução do código
                return;
            }

            try
            {
                // Cria uma nova conexão com o servidor usando o endereço IP e a porta 9000
                cliente = new(txtIP.Text + ":9000");

                // Define os eventos da conexão:
                // - Events_Connected: Ocorre quando a conexão é estabelecida
                // - Events_Disconnected: Ocorre quando a conexão é encerrada
                // - Events_DataReceived: Ocorre quando o cliente recebe dados do servidor
                cliente.Events.Connected += Events_Connected;
                cliente.Events.Disconnected += Events_Disconnected;
                cliente.Events.DataReceived += Events_DataReceived;

                // Tenta estabelecer a conexão com o servidor
                cliente.Connect();
                // Envia o nome e a cor do cliente para o servidor
                EnviarNomeECor();
                // Habilita o botão "Mensagem" para enviar mensagens
                btnMensagem.Enabled = true;
                // Desabilita o botão "Conectar" para evitar conexões duplicadas
                btnConectar.Enabled = false;
            }
            catch (Exception ex)
            {
                // Exibe uma mensagem de erro se a conexão falhar
                MessageBox.Show(ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento que ocorre quando o botão "Mensagem" é clicado
        private void btnMensagem_Click(object sender, EventArgs e)
        {
            // Verifica se a conexão com o servidor está ativa
            if (cliente.IsConnected)
            {
                // Verifica se o usuário digitou uma mensagem
                if (!string.IsNullOrEmpty(txtMensagem.Text))
                {
                    // Obtem a cor selecionada pelo usuário
                    string corEnv = cmbcor.SelectedItem.ToString();
                    // Cria a mensagem completa com o nome do cliente, a cor e a mensagem
                    string mensagemCompleta = $"●:{corEnv}:{txtNomeCliente.Text}:{txtMensagem.Text}";
                    // Envia a mensagem para o servidor
                    cliente.Send(mensagemCompleta);

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
            // A lógica de conexão foi movida para o btnConectar_Click
            // cliente = new("192.168.0.110:9000"); // Define IP e porta padrão
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

        // Evento que ocorre quando o cliente recebe dados do servidor
        private void Events_DataReceived(object? sender, DataReceivedEventArgs e)
        {
            // Executa o código em uma thread separada para evitar bloqueios da interface gráfica
            this.Invoke((MethodInvoker)delegate
            {
                // Decodifica a mensagem recebida do servidor
                string mensagemRecebida = Encoding.UTF8.GetString(e.Data);
                // Divide a mensagem em partes usando o caractere ':' como separador
                string[] partesMensagem = mensagemRecebida.Split(':');

                // Verifica se a mensagem possui pelo menos 4 partes
                if (partesMensagem.Length >= 4)
                {
                    // Extrai a cor do círculo, o nome do servidor e a mensagem
                    string corCirculo = partesMensagem[1].TrimStart('●');
                    string nomeServidor = partesMensagem[2].Trim();
                    string mensagem = partesMensagem[3].Trim();

                    // Define a cor do texto como preta
                    Color corTexto = Color.Black;
                    // Exibe o círculo com a cor da mensagem na caixa de texto "txtInfo"
                    AppendTextToRichTextBox(txtInfo, $" ● ", Color.FromName(corCirculo));
                    // Exibe a mensagem do servidor na caixa de texto "txtInfo"
                    AppendTextToRichTextBox(txtInfo, $"{nomeServidor}: {mensagem}{Environment.NewLine}", corTexto);
                }
            });
        }

        // Evento que ocorre quando a conexão com o servidor é encerrada
        private void Events_Disconnected(object? sender, ConnectionEventArgs e)
        {
            // Executa o código em uma thread separada para evitar bloqueios da interface gráfica
            this.Invoke((MethodInvoker)delegate
            {
                // Exibe uma mensagem na caixa de texto "txtInfo" informando que a conexão foi encerrada
                AppendTextToRichTextBox(txtInfo, $"Conexão encerrada... {Environment.NewLine}", Color.Black);
                // Desabilita o botão "Mensagem"
                btnMensagem.Enabled = false;
                // Habilita o botão "Conectar"
                btnConectar.Enabled = true;
            });
        }

        // Evento que ocorre quando a conexão com o servidor é estabelecida
        private void Events_Connected(object? sender, ConnectionEventArgs e)
        {
            // Executa o código em uma thread separada para evitar bloqueios da interface gráfica
            this.Invoke((MethodInvoker)delegate
            {
                // Exibe uma mensagem na caixa de texto "txtInfo" informando que a conexão foi estabelecida
                AppendTextToRichTextBox(txtInfo, $"Conexão estabelecida... {Environment.NewLine}", Color.Black);
            });
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
            // Verifica se a conexão com o servidor está ativa
            if (cliente.IsConnected)
            {
                // Obtem o nome do cliente e a cor selecionada
                string nome = txtNomeCliente.Text;
                string cor = cmbcor.SelectedItem.ToString();
                // Cria a mensagem com o nome e a cor
                string mensagemInfo = $"Nome:{nome};Cor:{cor}";
                // Envia a mensagem para o servidor
                cliente.Send(mensagemInfo);
            }
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
            richTextBox.SelectionStart = richTextBox.TextLength;
            richTextBox.SelectionColor = color;
            richTextBox.AppendText(text);
            richTextBox.SelectionColor = richTextBox.ForeColor;
        }
    }
}