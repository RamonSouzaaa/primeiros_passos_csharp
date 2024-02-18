using System;
using System.Collections.Generic;

namespace Banco
{
    /*
     * Criar uma classe clientes pessoa fisica e juridica
     * criar uma lista de movimentações para cada cliente
     * descontar uma taxa de 2,00 (para cada movimentação) para pessoa juridica e 1,00 (para cada movimentação) para pessoa fisica
     * operações saque e deposito
     */

    enum TIPO_CLIENTE
    {
        PESSOA_FISICA = 1,
        PESSOA_JURICA = 2
    }

    enum TIPO_CONTA
    {
        POUPANCA = 1,
        CORRENTE = 2
    }

    interface IConta
    {
        void sacar(double valor);
        void depositar(double valor);
        void transferir(Conta contaDestino, double valor);
        void extrato();
    }

    class Conta : IConta
    {
        private string agencia;
        private string numero;

        private double saldo;
        private TIPO_CONTA tipoConta { get; set; }
        private TIPO_CLIENTE tipoCliente { get; set; }
        private List<string> listaMovimentacaoes;

        public Conta()
        {
            this.saldo = 0;
            this.listaMovimentacaoes = new List<string>();
        }
        public Conta(
                string agencia,
                string numero,
                TIPO_CONTA tipoConta,
                TIPO_CLIENTE tipoCliente
            )
        {
            this.agencia = agencia;
            this.numero = numero;
            this.tipoConta = tipoConta;
            this.tipoCliente = tipoCliente;
            this.saldo = 0;
            this.listaMovimentacaoes = new List<string>();
        }

        public string Agencia
        {
            get
            {
                return this.agencia;
            }
            set
            {
                this.agencia = value;
            }
        }
        
        public string Numero
        {
            get 
            {
                return this.numero;
            }
            set 
            {
                this.numero = value;
            }
        }
        
        public TIPO_CONTA TipoConta
        {
            get
            {
                return this.tipoConta;
            }
            set
            {
                this.tipoConta = value;
            }
        }
        
        public TIPO_CLIENTE TipoCliente
        { 
            get
            {
                return this.tipoCliente;
            }
            set 
            {
                this.tipoCliente = value;
            }
        }

        private double getValorTaxa()
        {
            double valor = 0;
            switch (this.tipoCliente)
            {
                case TIPO_CLIENTE.PESSOA_JURICA:
                    valor = 2.00;
                    break;
                case TIPO_CLIENTE.PESSOA_FISICA:
                    valor = 1.00;
                    break;
                default: break;
            }

            return valor;
        }

        public void sacar(double valor) {
            if (valor > this.saldo)
            {
                Console.WriteLine("[AVISO]=Valor de saque maior que saldo.");
                return;
            }
            this.saldo -= valor;
            this.setMovimentacao($"[SAQUE] valor: {valor}");
            this.sacar(this.getValorTaxa());
        }

        public void depositar(double valor) {
            this.saldo += valor;
            this.setMovimentacao($"[DEPOSITO] valor: {valor}");
            this.sacar(this.getValorTaxa());
        }

        public void transferir(Conta contaDestino, double valor)
        {
            this.setMovimentacao($"[TED] valor: {valor} conta: [Número: {contaDestino.Numero} - Agência: {contaDestino.Agencia}]");
            this.sacar(valor);
            contaDestino.depositar(valor);
        }

        public void extrato() {
            this.toString();
            Console.WriteLine($"Saldo: {this.saldo}");
        }

        public List<string> getMovimentacoes()
        {
            return this.listaMovimentacaoes;
        }

        private void setMovimentacao(string item)
        {
            this.listaMovimentacaoes.Add(item);
        }

        public void toString()
        {
            Console.WriteLine($"Agência: {this.agencia}");
            Console.WriteLine($"Número: {this.numero}");
            Console.WriteLine($"Tipo conta: {this.tipoConta}");
            Console.WriteLine($"Tipo cliente: {this.tipoCliente}");
        }

    }

    class Cliente
    {
        private int codigo;
        private string nome;
        private Conta conta;

        public Cliente() { }
        public Cliente
            (
                int codigo,
                string nome,
                Conta conta
            )
        {
            this.codigo = codigo;
            this.nome = nome;
            this.conta = conta;
        }

        public int Codigo 
        {
            get 
            {
                return this.codigo;
            }
            set 
            {
                this.codigo = value;
            }
        }

        public string Nome
        {
            get
            {
                return this.nome;
            }
            set
            {
                this.nome = value;
            }
        }

        public Conta Conta
        {
            get
            {
                return this.conta;
            }

            set
            {
                this.conta = value;
            }
        }

        public void toString()
        {
            Console.WriteLine($"Código: {this.codigo}");
            Console.WriteLine($"Nome: {this.nome}");
            this.conta.toString();
        }
    }

    class Controlador
    {
        private List<Cliente> clientes;
        private int idenficadorClientes = 1;

        public Controlador()
        {
            this.clientes = new List<Cliente>();
        }

        private string cabecalho()
        {
            return "---------------------------------------------------------------\n" +
                   "|                         BANCO .NET                           |\n" +
                   "---------------------------------------------------------------\n";
        }
        
        private string menuPrincipal()
        {
            return this.cabecalho() +
                   "Informe a opção desejada: \n" +
                   "1-Clientes\n" +
                   "2-Operações\n" +
                   "3-Sair";
        }

        private string menuClientes()
        {
            return this.cabecalho() +
                   "Informe a opção desejada: \n" +
                   "1-Cadatrar\n" +
                   "2-Alterar\n" +
                   "3-Excluir\n" +
                   "4-Listar todos\n" +
                   "5-Voltar";
        }
        
        private void voltarMenu()
        {
            char opcao = '0';
            do
            {
                Console.WriteLine("Digite 1 para voltar ao menu principal");
                opcao = Console.ReadLine().ToCharArray()[0];
                if (opcao == '1')
                    break;

            } while (opcao != '1');
            return;
        }
        
        private Cliente getClienteDados()
        {
            char tipoModalidade = ' ';
            char tipoConta = ' ';
            string nome = "";
            string agencia = "";
            string numero = "";
            TIPO_CONTA conta;
            TIPO_CLIENTE modalidade;

            Console.WriteLine("Informe seu nome: ");
            nome = Console.ReadLine();

            Console.WriteLine("Informe a modalidade da conta: (1-Pessoa física/2-Pessoa júridica)");
            tipoModalidade = Console.ReadLine().ToCharArray()[0];
            modalidade = tipoModalidade == '1' ? TIPO_CLIENTE.PESSOA_FISICA : TIPO_CLIENTE.PESSOA_JURICA;

            Console.WriteLine("Informe o tipo de conta: (1-Corrente/2-Poupança)");
            tipoConta = Console.ReadLine().ToCharArray()[0];
            conta = tipoConta == '1' ? TIPO_CONTA.CORRENTE : TIPO_CONTA.POUPANCA;

            Console.WriteLine("Informe a agência: (####)");
            agencia = Console.ReadLine();

            Console.WriteLine("Informe a agência: ######-#)");
            numero = Console.ReadLine();

            return new Cliente(0, nome, new Conta(agencia, numero,conta, modalidade));
        }
        
        private void cadastrarCliente()
        {
            Console.Clear();
            Console.WriteLine(this.cabecalho());
            Console.WriteLine("Cadastro de clientes\n\n");
            Cliente cliente = this.getClienteDados();
            cliente.Codigo = this.idenficadorClientes;
            this.clientes.Add(cliente);
            this.idenficadorClientes++;
        }
        
        private void alterarCliente()
        {
            int codigo = 0;
            int indiceCliente = -1;
            Cliente cliente;
            Cliente clienteAlterado;

            Console.Clear();
            Console.WriteLine(this.cabecalho());
            Console.WriteLine("Alteração de cliente\n\n");
            Console.WriteLine("Informe o código do cliente: ");
            codigo = Int32.Parse(Console.ReadLine());
            indiceCliente = this.getIndiceClienteByCodigo(codigo);

            if (indiceCliente >= 0)
            {
                cliente = this.clientes[indiceCliente];
                Console.WriteLine($"Código cliente: {cliente.Codigo}");
                clienteAlterado = getClienteDados();
                cliente.Nome = clienteAlterado.Nome;
                cliente.Conta.Agencia = clienteAlterado.Conta.Agencia;
                cliente.Conta.Numero = clienteAlterado.Conta.Numero;
                cliente.Conta.TipoCliente = clienteAlterado.Conta.TipoCliente;
                cliente.Conta.TipoConta = clienteAlterado.Conta.TipoConta;
            } else {
                Console.WriteLine($"Nenhum usuário encontrado com o código {codigo} informado!");
            }

            this.voltarMenu();
        }

        private void excluirCliente()
        {
            int codigo = 0;
            int indiceCliente = -1;
            int opcao = -1;

            Cliente cliente;
            Console.Clear();
            Console.WriteLine(this.cabecalho());
            Console.WriteLine("Exclusão de cliente\n\n");
            Console.WriteLine("Informe o código do cliente: ");
            codigo = Int32.Parse(Console.ReadLine());
            indiceCliente = this.getIndiceClienteByCodigo(codigo);
            
            if (indiceCliente >= 0)
            {
                cliente = this.clientes[indiceCliente];
                this.mostrarDadosCliente(cliente);
                Console.WriteLine("\n");
                Console.WriteLine($"Deseja prosseguir com a exclusão do cliente de código {cliente.Codigo}? (1-Sim/2-Não)");
                opcao = Int32.Parse(Console.ReadLine());
                if(opcao == 1)
                {
                    this.clientes.Remove(cliente);
                }
            }
            else
            {
                Console.WriteLine($"Nenhum usuário encontrado com o código {codigo} informado!");
            }

            this.voltarMenu();
        }
       
        private void listarClientes() {
            Console.Clear();
            Console.WriteLine(this.cabecalho());
            Console.WriteLine("Listagem de clientes\n\n");
            if (this.clientes.Count > 0)
            {
                foreach (Cliente cliente in this.clientes)
                {
                    this.mostrarDadosCliente(cliente);
                    Console.WriteLine("------------------------------------");
                }
            } else {
                Console.WriteLine("Sem clientes para listar!");
            }
            this.voltarMenu();
        }
        
        private void mostrarDadosCliente(Cliente cliente)
        {
            Console.WriteLine($"Código: {cliente.Codigo }");
            Console.WriteLine($"Nome: {cliente.Nome }");
            Console.WriteLine($"Modalidade: {(cliente.Conta.TipoCliente == TIPO_CLIENTE.PESSOA_FISICA ? "Pessoa física" : "Pessoa júridica")}");
            Console.WriteLine($"Conta: {(cliente.Conta.TipoConta == TIPO_CONTA.CORRENTE ? "Conta Corrente" : "Conta Poupança")}");
            Console.WriteLine($"Agência: {cliente.Conta.Agencia}");
            Console.WriteLine($"Número: {cliente.Conta.Numero}");
        }
        
        private int getIndiceClienteByCodigo(int codigo)
        {
            int indice = -1;
            if (this.clientes.Count > 0)
            {
                for (int i=0; i<=(this.clientes.Count-1); i++)
                {
                    if (this.clientes[0].Codigo == codigo)
                    {
                        indice = i;
                        break;
                    }
                }

            }

            return indice;
        }

        private void gerenciarClientes()
        {
            char opcao = '0';
            do
            {
                Console.Clear();
                Console.WriteLine(this.menuClientes());
                opcao = Console.ReadLine().ToCharArray()[0];
                switch (opcao)
                {
                    case '1':
                        this.cadastrarCliente();
                        break;
                    case '2':
                        this.alterarCliente();
                        break;
                    case '3':
                        this.excluirCliente();
                        break;
                    case '4':
                        this.listarClientes();
                        break;
                    default: break;
                }
            } while (opcao != '5');

            return;
        }

        public void gerenciar()
        {
            char opcao = '0';
            do
            {
                Console.Clear();
                Console.WriteLine(this.menuPrincipal());
                opcao = Console.ReadLine().ToCharArray()[0];
                switch (opcao)
                {
                    case '1':
                        this.gerenciarClientes();
                        break;
                    case '2':
                        break;
                    default: break;
                }
            } while (opcao != '3');

            return;
        }
    }
    
    class Principal
    {
        static void Main(string[] args)
        {
            new Controlador().gerenciar();
        }
    }
}


