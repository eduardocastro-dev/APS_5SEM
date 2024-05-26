using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TCPClient
{
    public partial class TCPCliente : Form
    {
        private Socket _clientSocket;
        private string _serverIp;
        private int _port = 9000;
        private string pastaCompartilhada = "";

        public TCPCliente()
        {
            InitializeComponent();
        }

        private async void btnConectar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNomeCliente.Text) || string.IsNullOrEmpty(txtIP.Text) || string.IsNullOrEmpty(pastaCompartilhada))
            {
                MessageBox.Show("Por Favor, insira um nome de Usuário, o endereço IP do servidor e selecione a pasta para salvar as imagens", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _serverIp = txtIP.Text;

            try
            {
                _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                await Task.Run(() => _clientSocket.Connect(IPAddress.Parse(_serverIp), _port));
                EnviarNomeECor();
                btnMensagem.Enabled = true;
                btnDesconectar.Enabled = true;
                btnAnexo.Enabled = true;
                cmbcor.Enabled = false;
                btnConectar.Enabled = false;
                AppendTextToRichTextBox(txtInfo, $"Conexão estabelecida... {Environment.NewLine}", Color.Black);
                Task.Run(ReceiveMessages);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMensagem_Click(object sender, EventArgs e)
        {
            if (_clientSocket != null && _clientSocket.Connected && txtNomeCliente.Text != "")
            {
                if (!string.IsNullOrEmpty(txtMensagem.Text))
                {
                    string corEnv = cmbcor.SelectedItem.ToString();
                    string mensagemCompleta = $"●:{corEnv}:{txtNomeCliente.Text}:{txtMensagem.Text}";
                    Send(mensagemCompleta);
                    Color corSelecionada = GetColorFromComboBox(cmbcor.SelectedItem.ToString());
                    AppendTextToRichTextBox(txtInfo, $" ● ", corSelecionada);
                    AppendTextToRichTextBox(txtInfo, $"{txtNomeCliente.Text}: {txtMensagem.Text}{Environment.NewLine}", Color.Black);
                    txtMensagem.Text = string.Empty;
                }
            }
            else
            {
                MessageBox.Show("Teste");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnMensagem.Enabled = false;

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
            cmbcor.SelectedIndex = 0;
        }

        private async Task ReceiveMessages()
        {
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[1024];

                    if (!_clientSocket.Connected)
                    {
                        AppendTextToRichTextBox(txtInfo, $"Desconectando... {Environment.NewLine}", Color.Red);
                        return;
                    }

                    int bytesReceived = await Task.Run(() => _clientSocket.Receive(buffer));

                    if (bytesReceived == 0)
                    {
                        AppendTextToRichTextBox(txtInfo, $"Desconectando... {Environment.NewLine}", Color.Black);
                        return;
                    }

                    string mensagemRecebida = Encoding.UTF8.GetString(buffer, 0, bytesReceived);
                    await Task.Run(() => ProcessMessage(mensagemRecebida));
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.ConnectionReset)
                    {
                        AppendTextToRichTextBox(txtInfo, $"Conexão encerrada pelo servidor (código de erro 10053). {Environment.NewLine}", Color.Red);
                    }
                    else
                    {
                        AppendTextToRichTextBox(txtInfo, $"Desconectado do servidor: {Environment.NewLine}", Color.Red);
                    }

                    btnMensagem.Enabled = false;
                    btnDesconectar.Enabled = false;
                    btnConectar.Enabled = true;

                    if (_clientSocket != null)
                    {
                        _clientSocket.Close();
                    }

                    return;
                }
                catch (ObjectDisposedException)
                {
                    AppendTextToRichTextBox(txtInfo, $"Desconectado do servidor: O socket foi fechado. {Environment.NewLine}", Color.Red);
                    btnMensagem.Enabled = false;
                    btnDesconectar.Enabled = false;
                    btnConectar.Enabled = true;
                    return;
                }
            }
        }

        private void ProcessMessage(string mensagemRecebida)
        {
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

                if (mensagemRecebida == "Servidor encerrado...")
                {
                    AppendTextToRichTextBox(txtInfo, $"O servidor foi encerrado. A conexão foi fechada. {Environment.NewLine}", Color.Red);
                    DesconectarCliente();
                }

                else if (mensagemRecebida.StartsWith("Arquivo:"))
                {
                    string[] partes = mensagemRecebida.Split(':');
                    string fileName = partes[1];

                    byte[] headerBytes = new byte[4];
                    _clientSocket.Receive(headerBytes);
                    int fileSize = BitConverter.ToInt32(headerBytes, 0);

                    byte[] fileBytes = new byte[fileSize];
                    int bytesRead = 0;
                    while (bytesRead < fileSize)
                    {
                        bytesRead += _clientSocket.Receive(fileBytes, bytesRead, fileSize - bytesRead, SocketFlags.None);
                    }

                    string savePath = Path.Combine(pastaCompartilhada, fileName);
                    File.WriteAllBytes(savePath, fileBytes);
                    AppendTextToRichTextBox(txtInfo, $"Nova imagem recebida! {Environment.NewLine}", Color.DarkGreen);
                }
            });
        }

        private void Send(string message)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                _clientSocket.Send(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtIP_TextChanged(object sender, EventArgs e)
        {
        }

        private void cmbcor_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        private void txtNomeCliente_TextChanged(object sender, EventArgs e)
        {
        }

        private void EnviarNomeECor()
        {
            string nome = txtNomeCliente.Text;
            string cor = cmbcor.SelectedItem.ToString();
            string mensagemInfo = $"Nome:{nome};Cor:{cor}";
            Send(mensagemInfo);
        }

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
                default: return Color.Black;
            }
        }

        private void AppendTextToRichTextBox(RichTextBox richTextBox, string text, Color color)
        {
            if (richTextBox.InvokeRequired)
            {
                richTextBox.Invoke((MethodInvoker)delegate
                {
                    AppendTextToRichTextBox(richTextBox, text, color);
                });
            }
            else
            {
                richTextBox.SelectionStart = richTextBox.TextLength;
                richTextBox.SelectionColor = color;
                richTextBox.AppendText(text);
                richTextBox.SelectionColor = richTextBox.ForeColor;
            }
        }

        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            DesconectarCliente();
        }

        private void DesconectarCliente()
        {
            try
            {
                string nomeCliente = txtNomeCliente.Text;
                string mensagemDesconexao = $" Se desconectou...";
                Send(mensagemDesconexao);
                _clientSocket.Shutdown(SocketShutdown.Both);
                _clientSocket.Close();
                btnMensagem.Enabled = false;
                btnDesconectar.Enabled = false;
                btnConectar.Enabled = true;
                cmbcor.Enabled = true;
                AppendTextToRichTextBox(txtInfo, $"Você foi desconectado do servidor. {Environment.NewLine}", Color.Red);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAnexo_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Todos os arquivos|*.*|Imagens (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                if (_clientSocket != null && _clientSocket.Connected)
                {
                    try
                    {
                        byte[] fileBytes = File.ReadAllBytes(filePath);
                        byte[] header = BitConverter.GetBytes(fileBytes.Length);
                        string fileName = Path.GetFileName(filePath);
                        Send($"Arquivo:{fileName}");
                        _clientSocket.Send(header);
                        _clientSocket.Send(fileBytes);
                        AppendTextToRichTextBox(txtInfo, $"Anexo enviado com sucesso! {Environment.NewLine}", Color.Black);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Você precisa estar conectado ao servidor para enviar um anexo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSelecionarArquivo_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                pastaCompartilhada = folderDialog.SelectedPath;
                txtPastaAnexo.Text = pastaCompartilhada;
            }
        }

        private void TCPCliente_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_clientSocket != null && _clientSocket.Connected)
            {
                try
                {
                    string mensagemDesconexao = $" Se desconectou...";
                    Send(mensagemDesconexao);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao enviar mensagem de desconexão: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (_clientSocket != null)
            {
                try
                {
                    _clientSocket.Shutdown(SocketShutdown.Both);
                    _clientSocket.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao fechar o socket: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}