using System;
using System.Collections.Generic;
using System.Text;

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
        private int agencia;
        private int numero;

        private double saldo;
        private TIPO_CONTA tipoConta { get; set; }
        private TIPO_CLIENTE tipoCliente { get; set; }
        private List<string> listaMovimentacaoes;

        Conta()
        {
            this.saldo = 0;
            this.listaMovimentacaoes = new List<string>();
        }
        Conta(
                int agencia,
                int numero,
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

        public int Agencia
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
        
        public int Numero
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
            set
            {
                this.tipoConta = value;
            }
        }
        
        public TIPO_CLIENTE TipoCliente
        { 
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

        Cliente() { }
        Cliente
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
        
        Controlador()
        {
            this.clientes = new List<Cliente>();
        }
    }
}
