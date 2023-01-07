using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeirasEspinhoBlazorApp.SourceCode.Stands
{
    public class Stand
    {
        private int idStand;
        private bool negociavel;
        private int consultantes;  // nº de visitantes de um stand
        private DateTime dataCriacao;
        private string emailDono;
		private int categoria;
        private List<Produto> produtos;
        public int IdStand
        {
            get { return idStand; }
            set { idStand = value; }
        }
        public bool Negociavel
        {
            get { return negociavel; }
            set { negociavel = value; }
        }
        
        public int Consultantes
        {
            get { return consultantes; }
            set { consultantes = value; }
        }
        
        public DateTime DataCriacao
        {
            get { return dataCriacao; }
            set { dataCriacao = value; }
        }
        
        public string EmailDono
        {
            get { return emailDono; }
            set { emailDono = value; }
        }
        
        public int Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }
        
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

        public override string ToString()
        {
            return "IdStand: " + IdStand + "\n" + "Negociavel: " + Negociavel + "\n" + "Consultantes: " + Consultantes + "\n" +
                "DataCriacao: " + DataCriacao + "\n" + "EmailDono: " + EmailDono + "\n" + "Categoria: " + Categoria + "\n" +
                "Produtos: " + Produtos + "\n";
        }

    }
}
