using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeirasEspinhoBlazorApp.SourceCode.Feiras
{
    public class Leilao
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        private float valorMinimo;
        public float ValormMinimo
        {
            get { return valorMinimo; }
            set { valorMinimo = value; }
        }
        private float valorMaximo;
        public float ValormMaximo
        {
            get { return valorMaximo; }
            set { valorMaximo = value; }
        }
        private int produto;
        public int Produto
        {
            get { return produto; }
            set { produto = value; }
        }
        private int quantidade;
        public int Quantidade
        {
            get { return quantidade; }
            set { quantidade = value; }
        }
        private int stand;
        public int Stand
        {
            get { return stand; }
            set { stand = value; }
        }
        private int feira;
        public int Feira
        {
            get { return feira; }
            set { feira = value; }
        }
        private int bidAtual;
        public int BidAtual
        {
            get { return bidAtual; }
            set { bidAtual = value; }
        }
        public Leilao() { }
        public Leilao(int id, DateTime date, float valormMinimo, float valormMaximo, int produto, int quantidade, int stand, int feira, int bidAtual)
        {
            this.id = id;
            this.date = date;
            valorMinimo = valormMinimo;
            valorMaximo = valormMaximo;
            this.produto = produto;
            this.quantidade = quantidade;
            this.stand = stand;
            this.feira = feira;
            this.bidAtual = bidAtual;
        }
    }
}
