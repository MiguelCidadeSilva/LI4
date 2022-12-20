using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeirasEspinhoBlazorApp.SourceCode.Stand
{
    public class Stand
    {
        private int idStand;
        public int IdStand
        {
            get { return idStand; }
            set { idStand = value; }
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
        private DateTime dataCriacao;
        public DateTime DataCriacao
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

        public Stand(int idStand, bool negociavel, int consultates, DateTime dataCriacao, string emailDono, int categoria, List<Produto> produto)
        {
            IdStand = idStand;
            Negociavel = negociavel;
            Consultantes = consultates;
            DataCriacao = dataCriacao;
            EmailDono = emailDono;
            Categoria = categoria;
            Produtos = produto;
        }

        public Stand() { }

        public Stand(int idStand, bool negociavel, DateTime dataCriacao, string emailDono, int categoria)
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
