using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeirasEspinho
{
    public class Stands
    {
        public int IdStand { get; set; }
        public bool Negociavel { get; set; }
        public int Consultantes { get; set; } // nº de visitantes de um stand
        public DateTime DataCriacao { get; set; }
        public string? EmailDono { get; set; }
        public int Categoria { get; set; }
        public List <Produto> Produtos { get; set; }
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
