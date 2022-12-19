using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeirasEspinho;

namespace FeirasEspinho
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
        private string? emailCliente;
        public string? EmailCliente
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

        public Venda()
        {
            this.IdVenda = 0;
            this.Data = DateTime.MinValue;
            this.Preco = 0;
            this.EmailCliente = "";
            this.IdFeira = 0;
            this.Negociacao= null;
            this.IdStand= 0;
        }

        public Venda(int idVenda, DateTime data, float preco, string emailCliente, int idFeira, int negociacao, int idStand)
        {
            IdVenda = idVenda;
            Data = data;
            Preco = preco;
            EmailCliente = emailCliente;
            IdFeira = idFeira;
            Negociacao = negociacao;
            IdStand = idStand;
        }

        public Venda(Venda v)
        {
            IdVenda= v.IdVenda;
            Data = v.Data;
            Preco= v.Preco;
            EmailCliente= v.EmailCliente;
            IdFeira= v.IdFeira;
            Negociacao = v.Negociacao;
            IdStand = v.IdStand;

        }

        public override string ToString()
        {
            String s = "====VENDA===\n";
            return (s + "ID da Venda: " + IdVenda + "\nData: " + this.Data.ToString("dd/MM/yyyy") + "\nPreco " + Preco.ToString("c2") +
                        "\nEmail do Cliente: " + EmailCliente + "\nID da Feira :" + IdFeira +
                        "\nNegociacao: " + (Negociacao == null ? "nao existe" : Negociacao) +
                        "\nID do Stand: " + IdStand + "\n"); 
        }

        public Venda Clone()
        {
            return new Venda(this);
        }

        public override int GetHashCode() => (IdVenda,Data,Preco,EmailCliente, IdFeira, Negociacao,IdStand).GetHashCode();

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;

            Negociacao n = new Negociacao();

            Venda v = (Venda) obj;
            return (this.IdVenda == v.IdVenda && this.Data.Equals(v.Data) && n.QuaseIgual(v.Preco, this.Preco, 0.001f)
                    && this.EmailCliente.Equals(v.EmailCliente) && this.IdFeira == v.IdFeira
                    && this.Negociacao == v.Negociacao
                    && this.IdStand == v.IdStand);
        }

    }
}
