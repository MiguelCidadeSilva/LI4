using FeirasEspinhoBlazorApp.SourceCode.Stands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeirasEspinhoBlazorApp.SourceCode.Vendas
{
    public class Venda
    {
        private int idVenda;
        public int IdVenda
        {
            get { return idVenda; }
            set { idVenda = value; }
        }
        private DateTime data;
        public DateTime Data
        {
            get { return data; }
            set { data = value; }
        }
        private float preco;
        public float Preco
        {
            get { return preco; }
            set { preco = value; }
        }
        private string emailCliente;
        public string EmailCliente
        {
            get { return emailCliente; }
            set { emailCliente = value; }
        }
        private int idFeira;
        public int IdFeira
        {
            get { return idFeira; }
            set { idFeira = value; }
        }
        private int? negociacao;
        public int? Negociacao
        {
            get { return negociacao; }
            set { negociacao = value; }
        }
        private int idStand;
        public int IdStand
        {
            get { return idStand; }
            set { idStand = value; }
        }
        private List<(Produto, int)> produtos;
        public List<(Produto, int)> Produtos
        {
            get { return produtos; }
        }

        public Venda()
        {
            IdVenda = 0;
            Data = DateTime.MinValue;
            Preco = 0;
            EmailCliente = "";
            IdFeira = 0;
            Negociacao = null;
            IdStand = 0;
        }

        public Venda(int idVenda, DateTime data, float preco, string emailCliente, int idFeira, int? negociacao, int idStand, List<(Produto, int)> produtos)
        {
            IdVenda = idVenda;
            Data = data;
            Preco = preco;
            EmailCliente = emailCliente;
            IdFeira = idFeira;
            Negociacao = negociacao;
            IdStand = idStand;
            this.produtos = new();
            produtos.ForEach(p => this.produtos.Add((new Produto(p.Item1),p.Item2)));
        }

        public Venda(Venda v)
        {
            IdVenda = v.IdVenda;
            Data = v.Data;
            Preco = v.Preco;
            EmailCliente = v.EmailCliente;
            IdFeira = v.IdFeira;
            Negociacao = v.Negociacao;
            IdStand = v.IdStand;
            this.produtos = v.Produtos;

        }

        public override string ToString()
        {
            string s = "====VENDA===\n";
            return s + "ID da Venda: " + IdVenda + "\nData: " + Data.ToString("dd/MM/yyyy") + "\nPreco " + Preco.ToString("c2") +
                        "\nEmail do Cliente: " + EmailCliente + "\nID da Feira :" + IdFeira +
                        "\nNegociacao: " + (Negociacao == null ? "nao existe" : Negociacao) +
                        "\nID do Stand: " + IdStand + "\n";
        }

        public Venda Clone()
        {
            return new Venda(this);
        }

        public override int GetHashCode() => (IdVenda, Data, Preco, EmailCliente, IdFeira, Negociacao, IdStand).GetHashCode();

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;

            Negociacao n = new Negociacao();

            Venda v = (Venda)obj;
            return IdVenda == v.IdVenda && Data.Equals(v.Data) && n.QuaseIgual(v.Preco, Preco, 0.001f)
                    && EmailCliente.Equals(v.EmailCliente) && IdFeira == v.IdFeira
                    && Negociacao == v.Negociacao
                    && IdStand == v.IdStand;
        }

    }
}
