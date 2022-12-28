using FeirasEspinhoBlazorApp.SourceCode.Stands;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FeirasEspinhoBlazorApp.SourceCode.Feiras
{
    public class Feira
    {
        private int iDFeira;
        private DateTime dataInicio;
        private DateTime? dataFim;
        private float precoCandidatura;
        private string? criadorEmail;
        private int categoria;
        private Dictionary <string, List<Stand>> listaStands;
        private Dictionary<string, List<Leilao>> listaLeiloes;
        private string? nome;
        public int IDFeira
        {
            get { return iDFeira; }
            set { iDFeira = value; }
        }
        public string? Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        public DateTime DataInicio
        {
            get { return dataInicio; }
            set { dataInicio = value; }
        }
        public DateTime? DataFim
        {
            get { return dataFim; }
            set { dataFim = value; }
        }
        public float PrecoCandidatura
        {
            get { return precoCandidatura; }
            set { precoCandidatura = value; }
        }
        public string? CriadorEmail
        {
            get { return criadorEmail; }
            set { criadorEmail = value; }
        }
        public int Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }
        public Dictionary<string, List<Stand>> ListaStands
        {
            get { return listaStands; }
            set { listaStands = value; }
        }
        public Dictionary<string, List<Leilao>> ListaLeiloes
        {
            get { return listaLeiloes; }
            set { listaLeiloes = value; }
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
            ListaStands = new Dictionary<string, List<Stand>>();
            ListaLeiloes = new Dictionary<string, List<Leilao>>();
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
            ListaStands = f.ListaStands;
            ListaLeiloes = f.ListaLeiloes;
        }


        public override string ToString()
        {
            string obj = "Feira: " + IDFeira + ", Nome: " + Nome + ", Datai: " + DataInicio.ToString() +
                         ", Dataf: " + (DataFim.Equals(null) ? "[FEIRA PERMANENTE]" : DataFim.ToString()) + ", " +
                         "Preço Candidatura: " + PrecoCandidatura + ", Email Criador : " + CriadorEmail +
                         ", Categoria: " + Categoria + "\nStands: \n";
            foreach (var (key, value) in ListaStands)
            {
                string str = "\nKey = " +key + "Value = " + key;
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
                   f.ListaStands.Equals(ListaStands) &&
                   f.ListaLeiloes.Equals(ListaLeiloes);

        }

        public override int GetHashCode() => (IDFeira, Nome, DataInicio, DataFim, PrecoCandidatura, CriadorEmail, Stands, Leiloes).GetHashCode();


        public void AddStand(string feirante, Stand stand)
        {
            List<Stand> lista;
            if (!ListaStands.TryGetValue(feirante, out lista))
            {
                lista = new List<Stand>();
                ListaStands[feirante] = lista;
            }
            lista.Add(stand);
        }
        public void AddLeilao(string feirante, Leilao leilao)
        {
            List<Leilao> lista;
            if (!ListaLeiloes.TryGetValue(feirante, out lista))
            {
                lista = new List<Leilao>();
                ListaLeiloes[feirante] = lista;
            }
            lista.Add(leilao);
        }

    }

}
