using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TCPServidor
{
    public partial class TCPServidor : Form
    {
        private Socket _serverSocket;
        private List<Socket> _clientSockets = new List<Socket>();
        private Dictionary<string, Tuple<Socket, string>> clientes = new Dictionary<string, Tuple<Socket, string>>();
        private string pastaCompartilhada = "";

        public TCPServidor()
        {
            InitializeComponent();

            Image imageLogo = Image.FromFile("Resources/logo_150.png");
            Image imageAnexo = Image.FromFile("Resources/anexo_18.png");

            label8.Image = imageLogo;
            btnAnexo.Image = imageAnexo;
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNomeServidor.Text) || string.IsNullOrEmpty(pastaCompartilhada))
            {
                MessageBox.Show("Por Favor, insira um nome de Usuário e selecione a pasta para salvar as imagens", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                StartServer();
                AppendText(txtInfo, $"Servidor Iniciado...{Environment.NewLine}", Color.Black);
                btnIniciar.Enabled = false;
                btnMensagem.Enabled = true;
                btnAnexo.Enabled = true;
                btnFecharConexao.Enabled = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao iniciar o servidor: {ex.Message}");
                MessageBox.Show($"Erro ao iniciar o servidor: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnMensagem.Enabled = false;
            txtIP.Text = GetIp();

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
            cmbCor.SelectedIndex = 0;
        }

        private void StartServer()
        {
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                _serverSocket.Bind(new IPEndPoint(IPAddress.Any, 9000));
                _serverSocket.Listen(10);
                Console.WriteLine("Servidor iniciado e escutando na porta 9000.");
                Thread acceptThread = new Thread(AcceptClients);
                acceptThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao iniciar o servidor: {ex.Message}");
                MessageBox.Show($"Erro ao iniciar o servidor: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AcceptClients()
        {
            while (true)
            {
                try
                {
                    Socket clientSocket = _serverSocket.Accept();
                    _clientSockets.Add(clientSocket);
                    Thread clientThread = new Thread(() => HandleClient(clientSocket));
                    clientThread.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao aceitar conexão: {ex.Message}");
                }
            }
        }

        private void HandleClient(Socket clientSocket)
        {
            try
            {
                byte[] buffer = new byte[1024];

                while (true)
                {
                    int bytesReceived = clientSocket.Receive(buffer);

                    if (bytesReceived == 0)
                    {
                        Console.WriteLine("Cliente desconectado.");
                        _clientSockets.Remove(clientSocket);
                        clientSocket.Close();
                        break;
                    }

                    string mensagemRecebida = Encoding.UTF8.GetString(buffer, 0, bytesReceived);
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

        private void ProcessMessage(string mensagemRecebida, string ipPort, Socket clientSocket)
        {
            string nome = "";
            Color cor = Color.Black;
            string mensagem = mensagemRecebida;

            if (mensagemRecebida.StartsWith("Nome:"))
            {
                string[] partes = mensagemRecebida.Split(';');

                foreach (string parte in partes)
                {
                    if (parte.StartsWith("Nome:"))
                        nome = parte.Substring(5);
                    else if (parte.StartsWith("Cor:"))
                        cor = Color.FromName(parte.Substring(4));
                }

                if (clientes.ContainsKey(ipPort))
                {
                    clientes[ipPort] = Tuple.Create(clientSocket, $"{nome}|{cor.Name}");
                }
                else
                {
                    clientes.Add(ipPort, Tuple.Create(clientSocket, $"{nome}|{cor.Name}"));
                }

                AppendText(txtInfo, $"{nome} Se conectou...{Environment.NewLine}", Color.Black);
                AtualizarListaClientes();
            }
            else if (mensagemRecebida.StartsWith("Arquivo:"))
            {
                string[] partes = mensagemRecebida.Split(':');
                string fileName = partes[1];

                byte[] headerBytes = new byte[4];
                clientSocket.Receive(headerBytes);
                int fileSize = BitConverter.ToInt32(headerBytes, 0);

                byte[] fileBytes = new byte[fileSize];
                int bytesRead = 0;
                while (bytesRead < fileSize)
                {
                    bytesRead += clientSocket.Receive(fileBytes, bytesRead, fileSize - bytesRead, SocketFlags.None);
                }

                string[] partesNomeCor = clientes[ipPort].Item2.Split('|');
                nome = partesNomeCor[0];

                string novoNomeArquivo = $"{nome}_{DateTime.Now:yyyyMMddHHmmss}{Path.GetExtension(fileName)}";

                string savePath = Path.Combine(pastaCompartilhada, novoNomeArquivo);
                File.WriteAllBytes(savePath, fileBytes);

                AppendText(txtInfo, $"{nome} Enviou uma imagem!{Environment.NewLine}", Color.DarkGreen);

                foreach (var cliente in clientes.Keys)
                {
                    if (cliente != ipPort)
                    {
                        Socket socket = clientes[cliente].Item1;
                        Send(socket, $"Arquivo:{fileName}");
                        socket.Send(headerBytes);
                        socket.Send(fileBytes);
                        AppendText(txtInfo, $"{cliente} recebeu o anexo! {Environment.NewLine}", Color.Black);
                    }
                }
            }
            else
            {
                string[] partesNomeCor = clientes[ipPort].Item2.Split('|');
                nome = partesNomeCor[0];
                cor = Color.FromName(partesNomeCor[1]);

                if (mensagemRecebida.StartsWith("●:"))
                {
                    string[] partesMensagem = mensagemRecebida.Split(':');
                    mensagem = partesMensagem[3];
                }

                AppendText(txtInfo, $" ● ", cor);
                AppendText(txtInfo, $"{nome}: {mensagem}{Environment.NewLine}", Color.Black);

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

            if (mensagemRecebida == " Se desconectou...")
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();

                _clientSockets.Remove(clientSocket);
                clientes.Remove(ipPort);

                AppendText(txtInfo, $"Cliente {ipPort} removido da lista.{Environment.NewLine}", Color.Black);
                AtualizarListaClientes();
            }
        }

        private void btnMensagem_Click(object sender, EventArgs e)
        {
            if (_serverSocket.IsBound)
            {
                if (!string.IsNullOrEmpty(txtMensagem.Text))
                {
                    Color corServidor = Color.FromName(cmbCor.SelectedItem.ToString());
                    AppendText(txtInfo, " ● ", corServidor);
                    AppendText(txtInfo, $"{txtNomeServidor.Text}: {txtMensagem.Text}{Environment.NewLine}", Color.Black);
                    string corEnv = cmbCor.Text;

                    foreach (var cliente in clientes.Keys)
                    {
                        Socket socket = clientes[cliente].Item1;
                        Send(socket, $"●:{corEnv}:{txtNomeServidor.Text}:{txtMensagem.Text}");
                    }

                    txtMensagem.Text = string.Empty;
                }
            }
        }

        private void txtNomeServidor_TextChanged(object sender, EventArgs e)
        {
        }

        private void cmbCor_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void AppendText(RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        private void AtualizarListaClientes()
        {
            listClienteIP.Items.Clear();

            foreach (var cliente in clientes)
            {
                string[] partesNomeCor = cliente.Value.Item2.Split('|');
                string nome = partesNomeCor[0];
                listClienteIP.Items.Add($"{nome} ({cliente.Key})");
            }
        }

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

        private void Send(Socket clientSocket, string message)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                clientSocket.Send(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar mensagem: {ex.Message}");
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
        }

        private void btnFecharConexao_Click(object sender, EventArgs e)
        {
            if (_serverSocket != null && _serverSocket.IsBound)
            {
                foreach (var cliente in clientes.Keys)
                {
                    Socket socket = clientes[cliente].Item1;
                    Send(socket, $"Servidor encerrado...");
                }

                foreach (Socket socket in _clientSockets)
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }

                _serverSocket.Close();
                _clientSockets.Clear();
                clientes.Clear();
                AtualizarListaClientes();
                btnMensagem.Enabled = false;
                btnAnexo.Enabled = false;
                btnIniciar.Enabled = true;
                btnFecharConexao.Enabled = false;
                AppendText(txtInfo, $"Finalizado....{Environment.NewLine}", Color.Black);
            }
        }

        private void txtInfo_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnSelecionarArquivo_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                pastaCompartilhada = folderDialog.SelectedPath;
                txtPastaCompartilhada.Text = pastaCompartilhada;
            }
        }

        private void btnAnexo_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Todos os arquivos|*.*|Imagens (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                if (_serverSocket != null && _serverSocket.IsBound)
                {
                    try
                    {
                        byte[] fileBytes = File.ReadAllBytes(filePath);
                        byte[] header = BitConverter.GetBytes(fileBytes.Length);

                        foreach (var cliente in clientes.Keys)
                        {
                            Socket socket = clientes[cliente].Item1;
                            string fileName = Path.GetFileName(filePath);
                            Send(socket, $"Arquivo:{fileName}");
                            socket.Send(header);
                            socket.Send(fileBytes);
                            AppendText(txtInfo, $"{cliente} recebeu o anexo! {Environment.NewLine}", Color.Black);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Você precisa iniciar o servidor para enviar um anexo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TCPServidor_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var cliente in clientes.Keys)
            {
                Socket socket = clientes[cliente].Item1;
                Send(socket, $"Servidor encerrado...");
            }

            foreach (Socket socket in _clientSockets)
            {
                try
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao fechar o socket: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (_serverSocket != null)
            {
                try
                {
                    _serverSocket.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao fechar o socket do servidor: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            _clientSockets.Clear();
            clientes.Clear();
            AtualizarListaClientes();
            btnMensagem.Enabled = false;
            btnAnexo.Enabled = false;
            btnIniciar.Enabled = true;
            btnFecharConexao.Enabled = false;
            AppendText(txtInfo, $"Finalizado....{Environment.NewLine}", Color.Black);
        }
    }
}