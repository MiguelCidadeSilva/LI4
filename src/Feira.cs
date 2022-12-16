using System;
using System.Collections;
using System.Linq;

namespace FeirasEspinho
{
    internal class Feira
    {
        private int iDFeira;
        public int IDFeira
        {
            get { return iDFeira; }
            set { iDFeira = value; }
        }
        private string? nome;
        public string? Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        private DateTime dataInicio;
        public DateTime DataInicio
        {
            get { return dataInicio; }
            set { dataInicio = value; }
        }
        private DateTime dataFim;
        public DateTime DataFim
        {
            get { return dataFim; }
            set { dataFim = value; }
        }
        private float precoCandidatura;
        public float PrecoCandidatura
        {
            get { return precoCandidatura; }
            set { precoCandidatura = value; }
        }
        private string? criadorEmail;
        public string? CriadorEmail
        {
            get { return criadorEmail;}
            set { criadorEmail = value; }
        }
        private int categoria;
        public int Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }
        private Hashtable stands;
        public Hashtable Stands
        {
            get { return stands; }
            set { stands = value; }
        }
        private Hashtable leiloes;
        public Hashtable Leiloes
        {
            get { return leiloes; }
            set { leiloes = value; }
        }

        public Feira(int idFeira, string nome, DateTime dataI, DateTime dataF, float precoCand, string criadorEmail, int categoria)
        {
            this.iDFeira = idFeira;
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
            string obj = "Feira: " + IDFeira + ", Nome: " + Nome + ", Datai: " + DataInicio.ToString() + ", Dataf: " + DataFim.ToString() + ", " +
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
