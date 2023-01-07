using FeirasEspinhoBlazorApp.SourceCode.Stands;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FeirasEspinhoBlazorApp.SourceCode.Feiras
{
    public class Candidatura
    {
        private int idCandidatura;
        private DateTime dataSubmissao;
        private bool aprovacao;
        private int idStand;
        private int idFeira;

        public int IdCandidatura
        {
            get { return idCandidatura; }
            set { idCandidatura = value; }
        }

        public DateTime DataSubmissao
        {
            get { return dataSubmissao;}
            set { dataSubmissao = value;}
        }

        public bool Aprovacao
        {
            get { return aprovacao; }
            set { aprovacao = value; }
        }

        public int IdStand
        {
            get { return idStand; }
            set { idStand = value; }
        }

        public int IdFeira
        {
            get { return idFeira;}
            set {idFeira = value;}
        }

        public Candidatura(int idCandidatura, DateTime dataSubmissao, bool aprovacao, int idStand, int idFeira)
        {
            IdCandidatura = idCandidatura;
            DataSubmissao= dataSubmissao;
            Aprovacao = aprovacao;
            IdStand = idStand;
            IdFeira = idFeira;
        }
    }
}
