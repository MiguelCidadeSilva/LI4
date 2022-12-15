using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeirasEspinho
{
    public class Produto
    {
        public int IdProduto { get; set; }
        public string? Nome { get; set; }
        public int IdSubCategoria { get; set; }
        public int Stand { get; set; }
        public int Stock { get; set; }
        public float Preco { get; set; }
        public bool Disponivel { get; set; }
        public Produto() { }
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
    }
}
