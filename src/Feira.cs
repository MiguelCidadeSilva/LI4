using FeirasEspinho;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FeiraEspinho
{
    public class Feira
    {
        public int IdFeira { get; set; }
        public string? Nome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public float PrecoCandidatura { get; set; }
        public string? CriadorEmail { get; set; }
        public int Categoria { get; set; }
        public List<Stands> Stands { get; set; }
        public List<Leiloes> Leiloes { get; set; }
        public Dictionary<string, int> listaAvaliacoes { get; set; }


        public Feira(int idFeira, string nome, DateTime dataI, DateTime dataF, float precoCand, string criadorEmail, int categoria)
        {
            IdFeira = idFeira;
            Nome = nome;
            DataInicio = dataI;
            DataFim = dataF;
            PrecoCandidatura = precoCand;
            CriadorEmail = criadorEmail;
            Categoria = categoria;
            Stands = new List<Stands>();
            Leiloes = new List<Leiloes>();
            listaAvaliacoes= new Dictionary<string, int>();
        }
        public override string ToString()
        {
            string obj = "Feira: " + IdFeira + ", Nome: " + Nome + ", Datai: " + DataInicio.ToString() + ", Dataf: " + DataFim.ToString() + ", " +
                "Preço Candidatura: " + PrecoCandidatura + ", Email Criador : " + CriadorEmail + ", Categoria: " + Categoria + "\nStands:";
            foreach (Stands stand in Stands)
            {
                string str = "\nstand-> stand" + stand.IdStand;
                obj += str;
            }
            return obj;
        }
        public void AddStand(Stands stand)
        {
            Stands.Add(stand);
        }
        public void AddLeilao(Leiloes leilao)
        {
            Leiloes.Add(leilao);
        }
        public void EndLeilao(Leiloes leilao)
        {
            Leiloes.Remove(leilao);
        }
        public void RemoveStand(Stands stand)
        {
            Stands.Remove(stand);
        }
        public void AddAvaliacao(string emailCliente, int avaliacao)
        {
            listaAvaliacoes.Add(emailCliente,avaliacao);
        }
        
    }

}
