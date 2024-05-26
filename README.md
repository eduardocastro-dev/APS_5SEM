## TCPServidor e TCPCliente: Compartilhamento de Arquivos via TCP

Este repositório contém uma aplicação C# que implementa um servidor e um cliente TCP para comunicação e compartilhamento de arquivos entre múltiplos clientes. 

**Funcionalidades:**

* **Servidor:**
    * Iniciar e parar a escuta por conexões de clientes.
    * Aceitar novas conexões de clientes.
    * Gerenciar clientes conectados (armazenar informações como nome, cor de exibição e socket).
    * Receber e processar mensagens de texto e arquivos de clientes, reenviando para os outros clientes.
    * Enviar mensagens de texto para todos os clientes conectados.
    * Compartilhar imagens: receber imagens de clientes e replicar para outros clientes.
* **Cliente:**
    * Conectar a um servidor TCP.
    * Enviar nome e cor de exibição.
    * Enviar mensagens de texto para o servidor.
    * Compartilhar imagens com o servidor.
    * Receber mensagens de texto do servidor.
    * Receber imagens do servidor e salvá-las em uma pasta local.
    * Desconectar do servidor.

**Arquitetura:**

* A comunicação TCP é implementada utilizando a biblioteca `System.Net.Sockets`.
* O servidor é multithread para lidar com múltiplos clientes simultaneamente.
* O cliente é assíncrono para garantir uma interface responsiva.

**Requisitos:**

* Visual Studio 2019 ou superior.
* Framework .NET 4.7.2 ou superior.

**Instalação e Uso:**

1. Clone este repositório: 
   ```bash
   git clone https://github.com/eduardocastro-dev/APS_5SEM.git
   ```
2. Abra a solução no Visual Studio.
3. Compile o projeto do servidor e do cliente.
4. Execute o servidor.
5. Execute o cliente e configure as informações (nome, endereço IP do servidor e pasta para salvar imagens).
6. Conecte-se ao servidor.
7. Utilize as funcionalidades para enviar mensagens e compartilhar imagens.

**Observações:**

* O projeto inclui tratamento de erros para garantir a robustez das aplicações.
* A documentação detalhada da implementação está disponível nos arquivos de código fonte.
* Este projeto é um exemplo básico de um servidor e cliente TCP. Recursos adicionais, como segurança, autenticação e criptografia, podem ser implementados para atender a requisitos específicos.

**Contribuições:**

Contribuições para este projeto são bem-vindas! Por favor, abra um *issue* ou uma *pull request* para relatar bugs, sugestões ou melhorias.

**Licença:**

Este projeto está licenciado sob a licença [MIT](LICENSE).


 
