using System;
using System.Collections;
using System.Linq;
using FeirasEspinho;

namespace FeirasEspinho
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

        public Feira(int idFeira, string nome, DateTime dataI, DateTime? dataF, float precoCand, string criadorEmail, int categoria)
        {
            this.IDFeira = idFeira;
            this.Nome = nome;
            this.DataInicio = dataI;
            this.DataFim = dataF;
            this.PrecoCandidatura = precoCand;
            this.CriadorEmail = criadorEmail;
            this.Categoria = categoria;
            this.Stands = new Hashtable();
            this.Leiloes = new Hashtable();
        }

        public Feira(Feira f)
        {
            this.IDFeira = f.IDFeira;
            this.Nome = f.Nome;
            this.DataInicio= f.DataInicio;
            this.DataFim= f.DataFim;
            this.PrecoCandidatura= f.PrecoCandidatura;
            this.CriadorEmail= f.CriadorEmail;
            this.Categoria= f.Categoria;
            this.Stands = f.Stands;
            this.Leiloes = f.Leiloes;
        }

        
        public override string ToString()
        {
            string obj = "Feira: " + IDFeira + ", Nome: " + Nome + ", Datai: " + DataInicio.ToString() +
                         ", Dataf: " + ( DataFim.Equals(null) ? "[FEIRA PERMANENTE]" : DataFim.ToString() ) + ", " +
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

            return (f.IDFeira.Equals(this.IDFeira) &&
                   f.Nome.Equals(this.Nome) &&
                   f.DataInicio.Equals(this.DataInicio) &&
                   f.DataFim.Equals(this.DataFim) &&
                   f.PrecoCandidatura.Equals(this.PrecoCandidatura) &&
                   f.CriadorEmail.Equals(this.CriadorEmail) &&
                   f.Stands.Equals(this.Stands) &&
                   f.Leiloes.Equals(this.Leiloes));
            
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
