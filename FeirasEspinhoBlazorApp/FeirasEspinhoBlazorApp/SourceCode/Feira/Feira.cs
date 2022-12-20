using System;
using System.Collections;
using System.Linq;

namespace FeirasEspinhoBlazorApp.SourceCode.Feira
{
    public class Feira
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
        private DateTime? dataFim;
        public DateTime? DataFim
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
            get { return criadorEmail; }
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

        public Feira(int idFeira, string nome, DateTime dataI, DateTime? dataF, float precoCand, string criadorEmail, int categoria)
        {
            IDFeira = idFeira;
            Nome = nome;
            DataInicio = dataI;
            DataFim = dataF;
            PrecoCandidatura = precoCand;
            CriadorEmail = criadorEmail;
            Categoria = categoria;
            Stands = new Hashtable();
            Leiloes = new Hashtable();
        }

        public Feira(Feira f)
        {
            IDFeira = f.IDFeira;
            Nome = f.Nome;
            DataInicio = f.DataInicio;
            DataFim = f.DataFim;
            PrecoCandidatura = f.PrecoCandidatura;
            CriadorEmail = f.CriadorEmail;
            Categoria = f.Categoria;
            Stands = f.Stands;
            Leiloes = f.Leiloes;
        }


        public override string ToString()
        {
            string obj = "Feira: " + IDFeira + ", Nome: " + Nome + ", Datai: " + DataInicio.ToString() +
                         ", Dataf: " + (DataFim.Equals(null) ? "[FEIRA PERMANENTE]" : DataFim.ToString()) + ", " +
                         "Preço Candidatura: " + PrecoCandidatura + ", Email Criador : " + CriadorEmail +
                         ", Categoria: " + Categoria + "\nStands: \n";
            foreach (DictionaryEntry de in Stands)
            {
                string str = "\nKey = " + de.Key + "Value = " + de.Value;
                obj += str;
            }
            //string combinedString = string.Join("\n", Stands.Values.Cast<string>());
            //obj += combinedString;
            return obj;
        }

        public Feira Clone()
        {
            return new Feira(this);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;

            Feira f = obj as Feira;

            return f.IDFeira.Equals(IDFeira) &&
                   f.Nome.Equals(Nome) &&
                   f.DataInicio.Equals(DataInicio) &&
                   f.DataFim.Equals(DataFim) &&
                   f.PrecoCandidatura.Equals(PrecoCandidatura) &&
                   f.CriadorEmail.Equals(CriadorEmail) &&
                   f.Stands.Equals(Stands) &&
                   f.Leiloes.Equals(Leiloes);

        }

        public override int GetHashCode() => (IDFeira, Nome, DataInicio, DataFim, PrecoCandidatura, CriadorEmail, Stands, Leiloes).GetHashCode();


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
