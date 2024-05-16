using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPServidor
{
    public partial class TCPServidor : Form
    {
        public TCPServidor()
        {
            InitializeComponent();
        }

        // Variáveis para o servidor e a lista de clientes conectados
        private Socket _serverSocket;
        private List<Socket> _clientSockets = new List<Socket>();
        private Dictionary<string, Tuple<Socket, string>> clientes = new Dictionary<string, Tuple<Socket, string>>();

        // Timer para verificar a conectividade dos clientes
        private System.Threading.Timer _conectividadeTimer;

        // Evento para quando o botão "Iniciar" é clicado
        private void btnIniciar_Click(object sender, EventArgs e)
        {
            // Inicia o servidor
            StartServer();

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

            // Exibe o endereço IP do servidor na caixa de texto "txtIP"
            txtIP.Text = ip;

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

        // Método para iniciar o servidor
        private void StartServer()
        {
            // Cria um novo socket TCP
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Tenta vincular o socket a um endereço IP e porta
            try
            {
                _serverSocket.Bind(new IPEndPoint(IPAddress.Any, 9000));
                _serverSocket.Listen(10); // Define o backlog de conexões

                Console.WriteLine("Servidor iniciado e escutando na porta 9000.");

                // Inicia uma thread para aceitar conexões de clientes
                Thread acceptThread = new Thread(AcceptClients);
                acceptThread.Start();

                // Inicia o timer para verificar a conectividade dos clientes
                _conectividadeTimer = new System.Threading.Timer(ValidaClienteConectado, null, 30000, 30000);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao iniciar o servidor: {ex.Message}");
                MessageBox.Show($"Erro ao iniciar o servidor: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para aceitar conexões de clientes
        private void AcceptClients()
        {
            while (true)
            {
                try
                {
                    // Aceita uma conexão de cliente
                    Socket clientSocket = _serverSocket.Accept();

                    // Adiciona o socket do cliente à lista de clientes conectados
                    _clientSockets.Add(clientSocket);

                    // Inicia uma thread para lidar com o cliente
                    Thread clientThread = new Thread(() => HandleClient(clientSocket));
                    clientThread.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao aceitar conexão: {ex.Message}");
                }
            }
        }

        // Método para lidar com um cliente conectado
        private void HandleClient(Socket clientSocket)
        {
            try
            {
                // Criar um buffer para receber os dados
                byte[] buffer = new byte[1024];

                // Receber dados do cliente
                while (true)
                {
                    int bytesReceived = clientSocket.Receive(buffer);

                    // Se nenhum dado for recebido, o cliente desconectou
                    if (bytesReceived == 0)
                    {
                        Console.WriteLine("Cliente desconectado.");
                        _clientSockets.Remove(clientSocket);
                        clientSocket.Close();
                        break;
                    }

                    // Decodifica a mensagem recebida
                    string mensagemRecebida = Encoding.UTF8.GetString(buffer, 0, bytesReceived);

                    // Executa o código em uma thread separada para evitar bloqueios da interface gráfica
                    this.Invoke((MethodInvoker)delegate
                    {
                        ProcessMessage(mensagemRecebida, clientSocket.RemoteEndPoint.ToString(), clientSocket);
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no tratamento do cliente: {ex.Message}");
            }
        }

        // Método para processar a mensagem recebida
        private void ProcessMessage(string mensagemRecebida, string ipPort, Socket clientSocket)
        {
            string nome = "";
            Color cor = Color.Black;
            string mensagem = mensagemRecebida;

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
                if (clientes.ContainsKey(ipPort))
                {
                    clientes[ipPort] = Tuple.Create(clientSocket, $"{nome}|{cor.Name}");
                }
                // Se o cliente não estiver na lista, adiciona-o
                else
                {
                    clientes.Add(ipPort, Tuple.Create(clientSocket, $"{nome}|{cor.Name}"));
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
                string[] partesNomeCor = clientes[ipPort].Item2.Split('|');
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
                foreach (var cliente in clientes.Keys)
                {
                    if (cliente != ipPort)
                    {
                        Socket socket = clientes[cliente].Item1;

                        string mensagemCompleta = $"●:{cor.Name}:{nome}:{mensagem}";
                        Send(socket, mensagemCompleta);
                    }
                }
            }
        }

        // Evento para quando o botão "Mensagem" é clicado
        private void btnMensagem_Click(object sender, EventArgs e)
        {
            // Verifica se o servidor está escutando por conexões
            if (_serverSocket.IsBound)
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
                    foreach (var cliente in clientes.Keys)
                    {
                        Socket socket = clientes[cliente].Item1;

                        Send(socket, $"●:{corEnv}:{txtNomeServidor.Text}:{txtMensagem.Text}");
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
                string[] partesNomeCor = cliente.Value.Item2.Split('|');
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

        // Método para enviar uma mensagem para um cliente
        private void Send(Socket clientSocket, string message)
        {
            try
            {
                // Converte a mensagem para bytes
                byte[] data = Encoding.UTF8.GetBytes(message);

                // Envia a mensagem para o cliente
                clientSocket.Send(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar mensagem: {ex.Message}");
            }
        }

        // Método para validar a conectividade dos clientes
        private void ValidaClienteConectado(object state)
        {
            // Verifica se o servidor está escutando por conexões
            if (_serverSocket.IsBound)
            {
                // Faz uma cópia da lista de clientes conectados para evitar modificação durante a iteração
                List<string> clientesConectados = new List<string>(clientes.Keys);

                // Itera pelos clientes conectados
                foreach (var ipPort in clientesConectados)
                {
                    try
                    {
                        // Envia um ping para o cliente
                        Socket socket = clientes[ipPort].Item1;
                        Send(socket, "PING");

                        // Aguarda um tempo curto para a resposta do cliente
                        Thread.Sleep(1000);

                        // Verifica se a resposta do cliente foi recebida
                        if (socket.Available == 0)
                        {
                            // Se não houve resposta, desconecta o cliente e remove da lista
                            Console.WriteLine($"Cliente {ipPort} desconectado.");
                            DesconectarCliente(ipPort);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Se houve algum erro, desconecta o cliente e remove da lista
                        Console.WriteLine($"Erro ao validar conexão com {ipPort}: {ex.Message}");
                        DesconectarCliente(ipPort);
                    }
                }
            }
        }

        // Método para desconectar um cliente e removê-lo da lista
        private void DesconectarCliente(string ipPort)
        {
            // Remove o cliente da lista de clientes conectados
            clientes.Remove(ipPort);

            // Envia uma mensagem para todos os clientes conectados informando que o cliente foi desconectado
            foreach (var cliente in clientes.Keys)
            {
                Socket socket = clientes[cliente].Item1;
                string[] partesNomeCor = clientes[ipPort].Item2.Split('|');
                string nome = partesNomeCor[0];
                Send(socket, $"●:Black:{nome}:Desconectado.");
            }

            // Fecha a conexão do cliente
            if (clientes.ContainsKey(ipPort))
            {
                Socket socket = clientes[ipPort].Item1;
                socket.Close();
            }

            // Atualiza a lista de clientes conectados na interface gráfica (usando BeginInvoke)
            this.BeginInvoke((MethodInvoker)delegate { AtualizarListaClientes(); });
        }
    }
}