using System;
using System.Collections;
using System.Linq;

namespace FeirasEspinho
{
    internal class Feira
    {
        private int IdFeira { get; set; }
        private string? Nome { get; set; }
        private DateTime DataInicio { get; set; }
        private DateTime DataFim { get; set; }
        private float PrecoCandidatura { get; set; }
        private string? CriadorEmail { get; set; }
        private int Categoria { get; set; }
        private Hashtable Stands { get; set; }
        private Hashtable Leiloes { get; set; }

        public Feira(int idFeira, string nome, DateTime dataI, DateTime dataF, float precoCand, string criadorEmail, int categoria)
        {
            this.IdFeira = idFeira;
            this.Nome = nome;
            this.DataInicio = dataI;
            this.DataFim = dataF;
            this.PrecoCandidatura = precoCand;
            this.CriadorEmail = criadorEmail;
            this.Categoria = categoria;
            this.Stands = new Hashtable();
            this.Leiloes = new Hashtable();
        }
        public override string ToString()
        {
            string obj = "Feira: " + IdFeira + ", Nome: " + Nome + ", Datai: " + DataInicio.ToString() + ", Dataf: " + DataFim.ToString() + ", " +
                "Preço Candidatura: " + PrecoCandidatura + ", Email Criador : " + CriadorEmail + ", Categoria: " + Categoria + "\nStands: \n";
            foreach (DictionaryEntry de in Stands)
            {
                string str = "\nKey = " + de.Key + "Value = " + de.Value;
                obj += str;
            }
            //string combinedString = string.Join("\n", Stands.Values.Cast<string>());
            //obj += combinedString;
            return obj;
        }
        public void AddStand(string feirante, string? stand)
        {
            Stands.Add(feirante, stand);
        }
        public void AddLeilao(string feirante, string? leilao)
        {
            Leiloes.Add(feirante, leilao);
        }

    }

}
