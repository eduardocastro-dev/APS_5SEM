using SuperSimpleTcp;
using System.Text;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace TCPServidor
{
    public partial class TCPServidor : Form
    {
        public TCPServidor()
        {
            InitializeComponent();
        }

        // Variáveis para o servidor e a lista de clientes conectados
        SimpleTcpServer servidor;
        private Dictionary<string, string> clientes = new Dictionary<string, string>();

        // Evento para quando o botão "Iniciar" é clicado
        private void btnIniciar_Click(object sender, EventArgs e)
        {
            // Inicia o servidor
            servidor.Start();
            // Exibe uma mensagem na caixa de texto informando que o servidor foi iniciado
            AppendText(txtInfo, $"Servidor Iniciado...{Environment.NewLine}", Color.Black);
            // Desabilita o botão "Iniciar"
            btnIniciar.Enabled = false;
            // Habilita o botão "Mensagem"
            btnMensagem.Enabled = true;
        }

        // Evento para quando o formulário é carregado
        private void Form1_Load(object sender, EventArgs e)
        {
            // Desabilita o botão "Mensagem"
            btnMensagem.Enabled = false;

            // Obtém o endereço IP da máquina local
            string ip = GetIp();

            // Cria um novo servidor TCP usando o IP e a porta 9000
            servidor = new SimpleTcpServer($"{ip}:9000"); 
            // Exibe o endereço IP do servidor na caixa de texto "txtIP"
            txtIP.Text = ip;

            // Define os eventos do servidor:
            servidor.Events.ClientConnected += Events_ClientConnected; // Quando um cliente se conecta
            servidor.Events.ClientDisconnected += Events_ClientDisconnected; // Quando um cliente se desconecta
            servidor.Events.DataReceived += Events_DataReceived; // Quando o servidor recebe dados de um cliente

            // Adiciona as cores disponíveis na caixa de combinação "cmbCor"
            cmbCor.Items.Add("Black");
            cmbCor.Items.Add("Blue");
            cmbCor.Items.Add("Red");
            cmbCor.Items.Add("Green");
            cmbCor.Items.Add("Yellow");
            cmbCor.Items.Add("Pink");
            cmbCor.Items.Add("Orange");
            cmbCor.Items.Add("Brown");
            cmbCor.Items.Add("Gray");
            cmbCor.Items.Add("Purple");

            // Define a cor padrão da caixa de combinação como "Preto"
            cmbCor.SelectedIndex = 0;
        }

        // Evento para quando o servidor recebe dados de um cliente
        private void Events_DataReceived(object? sender, DataReceivedEventArgs e)
        {
            // Executa o código em uma thread separada para evitar bloqueios da interface gráfica
            this.Invoke((MethodInvoker)delegate
            {
                // Decodifica a mensagem recebida
                string mensagemRecebida = Encoding.UTF8.GetString(e.Data);
                string nome = ""; // Variável para armazenar o nome do cliente
                Color cor = Color.Black; // Variável para armazenar a cor do cliente
                string mensagem = mensagemRecebida; // Variável para armazenar a mensagem do cliente

                // Se a mensagem começar com "Nome:", significa que é a informação do nome e cor do cliente
                if (mensagemRecebida.StartsWith("Nome:"))
                {
                    // Divide a mensagem em partes usando o ponto e vírgula como separador
                    string[] partes = mensagemRecebida.Split(';');
                    // Itera pelas partes da mensagem
                    foreach (string parte in partes)
                    {
                        // Se a parte começar com "Nome:", extraia o nome do cliente
                        if (parte.StartsWith("Nome:"))
                            nome = parte.Substring(5);
                        // Se a parte começar com "Cor:", extraia a cor do cliente
                        else if (parte.StartsWith("Cor:"))
                            cor = Color.FromName(parte.Substring(4));
                    }

                    // Se o cliente já estiver na lista de clientes, atualiza as informações
                    if (clientes.ContainsKey(e.IpPort))
                    {
                        clientes[e.IpPort] = $"{nome}|{cor.Name}";
                    }
                    // Se o cliente não estiver na lista, adiciona-o
                    else
                    {
                        clientes.Add(e.IpPort, $"{nome}|{cor.Name}");
                    }

                    // Exibe uma mensagem na caixa de texto informando que o cliente se conectou
                    AppendText(txtInfo, $"{nome} Se conectou...{Environment.NewLine}", Color.Black);
                    // Atualiza a lista de clientes conectados na interface gráfica
                    AtualizarListaClientes();
                }
                else
                {
                    // Se a mensagem não começar com "Nome:", significa que é uma mensagem comum
                    // Obtem o nome e a cor do cliente a partir da lista de clientes
                    string[] partesNomeCor = clientes[e.IpPort].Split('|');
                    nome = partesNomeCor[0];
                    cor = Color.FromName(partesNomeCor[1]);

                    // Se a mensagem começar com "●:", significa que é uma mensagem com uma cor específica
                    if (mensagemRecebida.StartsWith("●:"))
                    {
                        // Divide a mensagem em partes usando o ponto e vírgula como separador
                        string[] partesMensagem = mensagemRecebida.Split(':');
                        // Extraia a mensagem do cliente
                        mensagem = partesMensagem[3];
                    }

                    // Exibe o círculo com a cor do cliente na caixa de texto
                    AppendText(txtInfo, $" ● ", cor);
                    // Exibe a mensagem do cliente na caixa de texto
                    AppendText(txtInfo, $"{nome}: {mensagem}{Environment.NewLine}", Color.Black);

                    // Reenvia a mensagem para todos os clientes conectados, exceto o cliente que a enviou
                    foreach (string ipPort in clientes.Keys)
                    {
                        if (ipPort != e.IpPort)
                        {
                            string mensagemCompleta = $"●:{cor.Name}:{nome}:{mensagem}";
                            servidor.Send(ipPort, mensagemCompleta);
                        }
                    }
                }
            });
        }

        // Evento para quando um cliente se desconecta do servidor
        private void Events_ClientDisconnected(object? sender, ConnectionEventArgs e)
        {
            // Executa o código em uma thread separada para evitar bloqueios da interface gráfica
            this.Invoke((MethodInvoker)delegate
            {
                // Obtem o nome do cliente a partir da lista de clientes
                string[] partesNomeCor = clientes[e.IpPort].Split('|');
                string nomeCliente = partesNomeCor[0];

                // Exibe uma mensagem na caixa de texto informando que o cliente se desconectou
                AppendText(txtInfo, $"{nomeCliente} Se desconectou...{Environment.NewLine}", Color.Black);
                // Remove o cliente da lista de clientes conectados
                clientes.Remove(e.IpPort);
                // Atualiza a lista de clientes conectados na interface gráfica
                AtualizarListaClientes();
            });
        }

        // Evento para quando um cliente se conecta ao servidor
        private void Events_ClientConnected(object? sender, ConnectionEventArgs e)
        {

        }

        // Evento para quando o botão "Mensagem" é clicado
        private void btnMensagem_Click(object sender, EventArgs e)
        {
            // Verifica se o servidor está escutando por conexões
            if (servidor.IsListening)
            {
                // Verifica se o usuário digitou uma mensagem
                if (!string.IsNullOrEmpty(txtMensagem.Text))
                {
                    // Obtem a cor selecionada pelo usuário na caixa de combinação
                    Color corServidor = Color.FromName(cmbCor.SelectedItem.ToString());
                    // Exibe o círculo com a cor do servidor na caixa de texto
                    AppendText(txtInfo, " ● ", corServidor);
                    // Exibe a mensagem do servidor na caixa de texto
                    AppendText(txtInfo, $"{txtNomeServidor.Text}: {txtMensagem.Text}{Environment.NewLine}", Color.Black);
                    // Obtem o nome da cor selecionada pelo usuário
                    string corEnv = cmbCor.Text;

                    // Envia a mensagem para todos os clientes conectados
                    foreach (string ipPort in clientes.Keys)
                    {
                        servidor.Send(ipPort, $"●:{corEnv}:{txtNomeServidor.Text}:{txtMensagem.Text}");
                    }

                    // Limpa a caixa de texto de mensagem
                    txtMensagem.Text = string.Empty;
                }
            }
        }

        // Evento para quando o texto da caixa de texto "txtNomeServidor" é alterado
        private void txtNomeServidor_TextChanged(object sender, EventArgs e)
        {

        }

        // Evento para quando a seleção da caixa de combinação "cmbCor" é alterada
        private void cmbCor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // Método para adicionar texto à caixa de texto com uma cor específica
        private void AppendText(RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        // Método para atualizar a lista de clientes conectados na interface gráfica
        private void AtualizarListaClientes()
        {
            // Limpa a lista de clientes conectados
            listClienteIP.Items.Clear();
            // Itera pelos clientes conectados
            foreach (var cliente in clientes)
            {
                // Obtem o nome do cliente a partir da lista de clientes
                string[] partesNomeCor = cliente.Value.Split('|');
                string nome = partesNomeCor[0];
                // Adiciona o nome e o endereço IP do cliente à lista de clientes conectados
                listClienteIP.Items.Add($"{nome} ({cliente.Key})");
            }
        }

        // Método para obter o endereço IP da máquina local
        private string GetIp()
        {
            string ipAddress = string.Empty;
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = ip.ToString();
                    break;
                }
            }
            return ipAddress;
        }
    }
}