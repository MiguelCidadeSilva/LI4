using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeirasEspinho
{
    public class Venda
    {
        public int IdVenda { get; set; }
        public DateTime Data { get; set; }
        public float Preco { get; set; }
        public string? EmailCliente { get; set; }
        public int IdFeira { get; set; }
        public int? Negociacao { get; set; }
        public int IdStand { get; set; }
        public Venda() { }
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
    }
}
