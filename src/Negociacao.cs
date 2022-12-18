using System;
using FeirasEspinho;

namespace FeirasEspinho
{
    
public class Negociacao
{

        private int idNegociacao;
        public int IdNegociacao
        {
            get { return idNegociacao; }
            set { idNegociacao = value; }
        }
        private float precoNegociacao;
        public float PrecoNegociacao
        {
            get { return precoNegociacao; }
            set { precoNegociacao = value; }
        }
        private float precoBase;
        public float PrecoBase
        {
            get { return precoBase; }
            set { precoBase = value; }
        }
        private bool sucesso;
        public bool Sucesso
        {
            get { return sucesso; }
            set { sucesso = value; }
        }
        private bool resposta;  // true: cliente propos o último preço, false: feirante propos o último preço
        public bool Resposta
        {
            get { return resposta; }
            set { resposta = value; }
        }

        public Negociacao()
        {
            IdNegociacao = 0;
            PrecoNegociacao=0;
            PrecoBase = 0;
            Sucesso = false;
            Resposta = false;
        }

        public Negociacao(int um_id, float um_preco_base, float um_preco, bool bit_sucessso, bool bit_resposta)
        {
            IdNegociacao = um_id;
            precoBase = um_preco_base;
            precoNegociacao = um_preco;
            Sucesso = bit_sucessso;
            Resposta= bit_resposta;
        }

        public Negociacao(Negociacao n)
        {
            IdNegociacao = n.IdNegociacao;
            PrecoBase = n.PrecoBase;
            PrecoNegociacao = n.PrecoNegociacao;
            Sucesso = n.Sucesso;
            Resposta = n.Resposta;
        }

        //criar negociacao no contexto da aplicacao

        public override String ToString()
        {
            string s = "";
            s += "===NEGOCIACAO===\n";
            return (s + "ID-Negociacao: " + this.IdNegociacao + "\nPreco Base: " + this.PrecoBase.ToString("c2") +
                        "\nPreco Atual: " + this.PrecoNegociacao.ToString("c2") + "\nSucesso: " + (this.Sucesso ? "SIM" : "NAO") +
                        "\nResposta do Cliente: " + (this.Resposta ? "SIM\n" : "NAO\n") );


        }

        public override int GetHashCode() => (IdNegociacao,PrecoBase,PrecoNegociacao,Sucesso,Resposta).GetHashCode();

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || (obj is not Negociacao)) return false;

            Negociacao n = (Negociacao)obj;
            if ( (n.IdNegociacao == this.IdNegociacao) && (QuaseIgual(n.PrecoBase,this.PrecoBase,0.01f)) && (QuaseIgual(n.PrecoNegociacao,this.PrecoNegociacao,0.01f))
                && (n.Sucesso.CompareTo(this.Sucesso) == 0) && (n.Resposta.CompareTo(this.Resposta) == 0) ) 
                    return true;

            return false;
        }


        //funcao auxiliar para comparar floats
        bool QuaseIgual(float x, float y, float tolerancia)
        {
            var diff = Math.Abs(x - y);
            return diff <= tolerancia ||
                   diff <= Math.Max(Math.Abs(x), Math.Abs(y)) * tolerancia;
        }

        public Negociacao Clone()
        {
            return new Negociacao(this);
        }






    }





}


