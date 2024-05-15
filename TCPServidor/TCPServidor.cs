using SuperSimpleTcp;
using System.Text;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TCPServidor
{
    public partial class TCPServidor : Form
    {
        /// <summary>
        /// Construtor da classe TCPServidor.
        /// Inicializa os componentes da interface gráfica.
        /// </summary>
        public TCPServidor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento de clique do label3.
        /// Não possui nenhuma ação implementada.
        /// </summary>
        private void label3_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Instância do servidor TCP.
        /// </summary>
        SimpleTcpServer servidor;

        /// <summary>
        /// Dicionário para armazenar informações dos clientes conectados, incluindo nome e cor.
        /// </summary>
        private Dictionary<string, string> clientes = new Dictionary<string, string>();

        /// <summary>
        /// Evento de clique do botão "Iniciar".
        /// Inicia o servidor TCP, habilita o botão "Mensagem" e desabilita o botão "Iniciar".
        /// </summary>
        private void btnIniciar_Click(object sender, EventArgs e)
        {
            servidor.Start();
            AppendText(txtInfo, $"Servidor Iniciado...{Environment.NewLine}", Color.Black);
            btnIniciar.Enabled = false;
            btnMensagem.Enabled = true;
        }

        /// <summary>
        /// Evento de carregamento do formulário.
        /// Inicializa o servidor TCP, configura os eventos do servidor e preenche o ComboBox com as opções de cores.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            btnMensagem.Enabled = false;
            servidor = new SimpleTcpServer(txtIP.Text);
            servidor.Events.ClientConnected += Events_ClientConnected;
            servidor.Events.ClientDisconnected += Events_ClientDisconnected;
            servidor.Events.DataReceived += Events_DataReceived;

            //Cores para cmbCor
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


            //Cor padrão "Preto"
            cmbCor.SelectedIndex = 0;
        }

        /// <summary>
        /// Evento acionado quando o servidor recebe dados de um cliente.
        /// Processa a mensagem recebida, extraindo nome e cor do cliente, e a exibe no RichTextBox.
        /// Reenvia a mensagem para os outros clientes conectados.
        /// </summary>
        private void Events_DataReceived(object? sender, DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                string mensagemRecebida = Encoding.UTF8.GetString(e.Data);

                // Extrair nome e cor da mensagem (se presente)
                string nome = "";
                Color cor = Color.Black; // Cor padrão
                if (mensagemRecebida.StartsWith("Nome:"))
                {
                    // Mensagem de informação do cliente
                    string[] partes = mensagemRecebida.Split(';');
                    foreach (string parte in partes)
                    {
                        if (parte.StartsWith("Nome:"))
                            nome = parte.Substring(5);
                        else if (parte.StartsWith("Cor:"))
                            cor = Color.FromName(parte.Substring(4));
                    }

                    // Verifica se o IP já está no dicionário
                    if (clientes.ContainsKey(e.IpPort))
                    {
                        // Atualiza o nome do cliente no dicionário
                        clientes[e.IpPort] = $"{nome}|{cor.Name}";
                    }
                    else
                    {
                        // Adiciona o cliente ao dicionário
                        clientes.Add(e.IpPort, $"{nome}|{cor.Name}");
                    }

                    // Exibe a mensagem de conexão após salvar as informações do cliente
                    AppendText(txtInfo, $"{nome} Se conectou...{Environment.NewLine}", Color.Black);
                    AtualizarListaClientes();
                }
                else
                {
                    // Mensagem de chat normal
                    string[] partesNomeCor = clientes[e.IpPort].Split('|');
                    nome = partesNomeCor[0];
                    cor = Color.FromName(partesNomeCor[1]); // Correção: Usa a cor do cliente

                    AppendText(txtInfo, $" ● ", cor); // Círculo colorido com a cor do cliente
                    AppendText(txtInfo, $"{mensagemRecebida}{Environment.NewLine}", Color.Black);

                    // Reenviar para outros clientes
                    foreach (string ipPort in clientes.Keys)
                    {
                        if (ipPort != e.IpPort)
                        {
                            servidor.Send(ipPort, $" {nome}: {mensagemRecebida}");
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Evento acionado quando um cliente se desconecta do servidor.
        /// Exibe uma mensagem no RichTextBox informando a desconexão do cliente.
        /// </summary>
        private void Events_ClientDisconnected(object? sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                string[] partesNomeCor = clientes[e.IpPort].Split('|');
                string nomeCliente = partesNomeCor[0];

                AppendText(txtInfo, $"{nomeCliente} Se desconectou...{Environment.NewLine}", Color.Black);
                clientes.Remove(e.IpPort);
                AtualizarListaClientes();
            });

        }

        /// <summary>
        /// Evento acionado quando um cliente se conecta ao servidor.
        /// Exibe uma mensagem no RichTextBox informando a conexão do cliente.
        /// </summary>
        private void Events_ClientConnected(object? sender, ConnectionEventArgs e)
        {

        }

        /// <summary>
        /// Evento de clique do botão "Mensagem".
        /// Envia a mensagem digitada pelo servidor para todos os clientes conectados.
        /// </summary>
        private void btnMensagem_Click(object sender, EventArgs e)
        {
            if (servidor.IsListening)
            {
                if (!string.IsNullOrEmpty(txtMensagem.Text))
                {
                    Color corServidor = Color.FromName(cmbCor.SelectedItem.ToString());
                    AppendText(txtInfo, " ● ", corServidor); // Círculo colorido
                    AppendText(txtInfo, $"{txtNomeServidor.Text}: {txtMensagem.Text}{Environment.NewLine}", Color.Black);

                    // Envia a mensagem para todos os clientes conectados
                    foreach (string ipPort in clientes.Keys)
                    {
                        servidor.Send(ipPort, $" ● {txtNomeServidor.Text}: {txtMensagem.Text}");
                    }

                    txtMensagem.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// Evento de alteração de texto do TextBox "Nome do Servidor".
        /// Não possui nenhuma ação implementada.
        /// </summary>
        private void txtNomeServidor_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Evento de mudança de seleção do ComboBox "Cor".
        /// Não possui nenhuma ação implementada.
        /// </summary>
        private void cmbCor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Adiciona texto ao RichTextBox com a cor especificada.
        /// </summary>
        /// <param name="box">RichTextBox para adicionar o texto.</param>
        /// <param name="text">Texto a ser adicionado.</param>
        /// <param name="color">Cor do texto.</param>
        private void AppendText(RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        /// <summary>
        /// Atualiza a ListBox com a lista de clientes conectados e seus respectivos nomes.
        /// </summary>
        private void AtualizarListaClientes()
        {
            listClienteIP.Items.Clear();
            foreach (var cliente in clientes)
            {
                string[] partesNomeCor = cliente.Value.Split('|');
                string nome = partesNomeCor[0];
                listClienteIP.Items.Add($"{nome} ({cliente.Key})");
            }
        }
    }
}