using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeirasEspinho
{
    public class Stands
    {
        private int idStand;
        public int IdStand
        {
            get { return IdStand; }
            set { IdStand = value; }
        }
        private bool negociavel;
        public bool Negociavel
        {
            get { return negociavel; }
            set { negociavel = value; }
        }
        private int consultantes;  // nº de visitantes de um stand
        public int Consultantes
        {
            get { return consultantes; }
            set { consultantes = value; }
        }
        public DateTime dataCriacao;
        private DateTime DataCriacao
        {
            get { return dataCriacao; }
            set { dataCriacao = value; }
        }
        private string? emailDono;
        public string? EmailDono
        {
            get { return emailDono; }
            set { emailDono = value; }
        }
        public int categoria;
        public int Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }
        private List<Produto> produtos;
        public List<Produto> Produtos
        {
            get { return produtos; }
            set { produtos = value; }
        }

        public Stands(int idStand, bool negociavel, int consultates, DateTime dataCriacao, string emailDono, int categoria, List<Produto> produto) 
        {
            IdStand = idStand;
            Negociavel = negociavel;
            Consultantes = consultates;
            DataCriacao = dataCriacao;
            EmailDono = emailDono;
            Categoria = categoria;
            Produtos = produto;
        }

        public Stands() { }

        public Stands(int idStand, bool negociavel, DateTime dataCriacao, string emailDono, int categoria)
        {
            IdStand = idStand;
            Negociavel = negociavel;
            Consultantes = 0;
            DataCriacao = dataCriacao;
            EmailDono = emailDono;
            Categoria = categoria;
            Produtos = new List<Produto>();
        }


    }
}
