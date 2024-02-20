using System;
using System.Collections.Generic;

namespace Banco
{
    /*
     * https://www.linkedin.com/learning/c-sharp-formacao-basica
     * Criar uma classe clientes pessoa fisica e juridica
     * criar uma lista de movimentações para cada cliente
     * descontar uma taxa de 2,00 (para cada movimentação) para pessoa juridica e 1,00 (para cada movimentação) para pessoa fisica
     * operações saque e deposito
     */

    interface IConta
    {
        void sacar(double valor);
        void depositar(double valor);
        void transferir(Conta contaDestino, double valor);
        List<string> extrato();
    }

    abstract class Conta : IConta
    {
        private string agencia;
        private string numero;
        private double saldo;
        private List<string> listaMovimentacaoes;

        public Conta() 
        {
            this.listaMovimentacaoes = new List<string>();
        }

        public void sacar(double valor) {
            if (valor > this.saldo)
            {
                this.saldo -= valor;
            }
        }

        public void depositar(double valor) {
            this.saldo += valor;
        }

        public void transferir(Conta contaDestino, double valor)
        {
            this.sacar(valor);
            contaDestino.depositar(valor);
        }

        public List<string> extrato() {
            return this.listaMovimentacaoes;
        }

        public void adicionaMovimentacao(string item)
        {
            this.listaMovimentacaoes.Add(item);
        }

        public string toString()
        {
            return "Agência: " + this.agencia + "\n" +
                   "Número: " + this.numero;
        }
    }

    class ContaCorrente : Conta
    {
        
    }

    class ContaPoupanca : Conta
    {

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

        public string toString()
        {
            return "Código: " + this.codigo + "\n" +
                   "Nome: " + this.nome + "\n" +
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
        
        //Menus
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
                   "1-Cadastro\n" +
                   "2-Alterar\n" +
                   "3-Excluir\n" +
                   "4-Listar todos\n" +
                   "5-Voltar";
        }
        
        private string menuOperacoes()
        {
            return this.cabecalho() +
                   "Informe a opção desejada: \n" +
                   "1-Saque\n" +
                   "2-Despósito\n" +
                   "3-Transferência\n" +
                   "4-Extrato\n" +
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

        //Cliente
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
                Console.WriteLine($"Nenhum cliente encontrado com o código {codigo} informado!");
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
                Console.WriteLine(cliente.toString());
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
                Console.WriteLine($"Nenhum cliente encontrado com o código {codigo} informado!");
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
                    Console.WriteLine(cliente.toString());
                    Console.WriteLine("------------------------------------");
                }
            } else {
                Console.WriteLine("Sem clientes para listar!");
            }
            this.voltarMenu();
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
        
        //Operações
        private void sacar() {
            int codigo = 0;
            int indiceCliente = -1;
            Cliente cliente;
            double valor;

            Console.Clear();
            Console.WriteLine(this.cabecalho());
            Console.WriteLine("Saque\n\n");
            Console.WriteLine("Informe o código do cliente: ");
            codigo = Int32.Parse(Console.ReadLine());
            indiceCliente = this.getIndiceClienteByCodigo(codigo);

            if (indiceCliente >= 0)
            {
                cliente = this.clientes[indiceCliente];
                Console.WriteLine($"Código cliente: {cliente.Codigo}");
                Console.WriteLine("Informe o valor de saque (####.##): ");
                valor = Double.Parse(Console.ReadLine());
                cliente.Conta.sacar(valor);
            }
            else
            {
                Console.WriteLine($"Nenhum cliente encontrado com o código {codigo} informado!");
            }

            this.voltarMenu();
        }
        
        private void depositar() {
            int codigo = 0;
            int indiceCliente = -1;
            Cliente cliente;
            double valor;

            Console.Clear();
            Console.WriteLine(this.cabecalho());
            Console.WriteLine("Depósito\n\n");
            Console.WriteLine("Informe o código do cliente: ");
            codigo = Int32.Parse(Console.ReadLine());
            indiceCliente = this.getIndiceClienteByCodigo(codigo);

            if (indiceCliente >= 0)
            {
                cliente = this.clientes[indiceCliente];
                Console.WriteLine($"Código cliente: {cliente.Codigo}");
                Console.WriteLine("Informe o valor de depósito (####.##): ");
                valor = Double.Parse(Console.ReadLine());
                cliente.Conta.depositar(valor);
            }
            else
            {
                Console.WriteLine($"Nenhum cliente encontrado com o código {codigo} informado!");
            }

            this.voltarMenu();
        }
        
        private void transferir() { }
        
        private void extrato() {
            int codigo = 0;
            int indiceCliente = -1;
            Cliente cliente;
            double valor;

            Console.Clear();
            Console.WriteLine(this.cabecalho());
            Console.WriteLine("Depósito\n\n");
            Console.WriteLine("Informe o código do cliente: ");
            codigo = Int32.Parse(Console.ReadLine());
            indiceCliente = this.getIndiceClienteByCodigo(codigo);

            if (indiceCliente >= 0)
            {
                cliente = this.clientes[indiceCliente];
                Console.WriteLine(cliente.toString());
                Console.WriteLine("---------------------------");
                foreach (string item in cliente.Conta.extrato())
                    Console.WriteLine($"{item}");
            }
            else
            {
                Console.WriteLine($"Nenhum cliente encontrado com o código {codigo} informado!");
            }

            this.voltarMenu();
        }

        //Gerenciadores
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

        private void gerenciarOperacoes()
        {
            char opcao = '0';
            do
            {
                Console.Clear();
                Console.WriteLine(this.menuOperacoes());
                opcao = Console.ReadLine().ToCharArray()[0];
                switch (opcao)
                {
                    case '1':
                        this.sacar();
                        break;
                    case '2':
                        this.depositar();
                        break;
                    case '3':
                        //this.transferir();
                        break;
                    case '4':
                       this.extrato();
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
                        this.gerenciarOperacoes();
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


