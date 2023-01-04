using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeirasEspinhoBlazorApp.SourceCode.Vendas;

namespace FeirasEspinhoBlazorApp.SourceCode.Stands
{
    public class Produto
    {
        public int idProduto;
        public int IdProduto
        {
            get { return idProduto; }
            set { idProduto = value; }
        }
        public string? nome;
        public string? Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        private int idSubCategoria;
        public int IdSubCategoria
        {
            get { return idSubCategoria; }
            set { idSubCategoria = value; }
        }
        public int stand;
        public int Stand
        {
            get { return stand; }
            set { stand = value; }
        }
        private int stock;
        public int Stock
        {
            get { return stock; }
            set { stock = value; }
        }
        private float preco;
        public float Preco
        {
            get { return preco; }
            set { preco = value; }
        }
        private bool disponivel;
        public bool Disponivel
        {
            get { return disponivel; }
            set { disponivel = value; }
        }

        public Produto()
        {
            IdProduto = 0;
            Nome = "";
            IdSubCategoria = 0;
            Stand = 0;
            Stock = 0;
            Preco = 0;
            Disponivel = false;
        }

        public Produto(int idProduto, string nome, int idSubCategoria, int stand, int stock, float preco, bool disponivel)
        {
            IdProduto = idProduto;
            Nome = nome;
            IdSubCategoria = idSubCategoria;
            Stand = stand;
            Stock = stock;
            Preco = preco;
            Disponivel = disponivel;
        }

        public Produto(Produto p)
        {
            IdProduto = p.IdProduto;
            Nome = p.Nome;
            IdSubCategoria = p.IdSubCategoria;
            Stand = p.Stand;
            Stock = p.Stock;
            Preco = p.Preco;
            Disponivel = p.Disponivel;
        }

        public override string ToString()
        {
            return "IdProduto: " + IdProduto + "\n" + "Nome: " + Nome + "\n" + "IdSubCategoria: " + IdSubCategoria + "\n" + "IdStand: " + Stand +
                "Stock: " +Stock+"\n" + "Preco: " +Preco +"\n"+"Disponivel: "+Disponivel;
        }

        public Produto Clone()
        {
            return new Produto(this);
        }

        public override int GetHashCode() => (IdProduto, Nome, IdSubCategoria, Stand, Stock, Preco, Disponivel).GetHashCode();

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;

            Produto p = (Produto)obj;
            Negociacao n = new Negociacao(); // a classe negociacao tem o método para comparar dois floats, estou a instanciar a classe para usar a funcao

            return p.Disponivel.CompareTo(Disponivel) == 0 && n.QuaseIgual(Preco, p.Preco, 0.01f)
                    && Nome.Equals(p.Nome) && IdProduto == p.IdProduto
                    && IdSubCategoria == p.IdSubCategoria && Stand == p.Stand
                    && Stock == p.Stock;
        }

    }
}
