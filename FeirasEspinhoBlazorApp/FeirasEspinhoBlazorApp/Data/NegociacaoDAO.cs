using static System.Net.Mime.MediaTypeNames;
using System.Data;
using Microsoft.AspNetCore.Http;
using FeirasEspinhoBlazorApp.SourceCode.Vendas;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Collections.Generic;
using FeirasEspinhoBlazorApp.Data;
using Microsoft.IdentityModel.Tokens;
using FeirasEspinhoBlazorApp.SourceCode.Stands;


namespace FeirasEspinhoBlazorApp.Data
{
    public class NegociacaoDAO
    {
        private static NegociacaoDAO instance = new NegociacaoDAO();

        public static NegociacaoDAO GetInstance()
        {
            return instance;
        }

        public bool ContaintsKey(int negociacao)
        {
            bool r = false;
            using SqlConnection connection = new(ConnectionDAO.connectionString);
            using (SqlCommand command = new("SELECT * FROM [Negociacao] WHERE idNeg = (@idNeg)", connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@idNeg", negociacao);
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                r = response.HasRows;
                connection.Close();
            }
            return r;
        }

        public void Insert(Negociacao negociacao)
        {
            using SqlConnection connection = new(ConnectionDAO.connectionString);
            using SqlCommand command = new("INSERT INTO [dbo].[Negociacao] VALUES (@idNeg, @precoBase, @precoNeg, @sucesso, @ultimoPropor)", connection);
            {
                connection.Open();
                command.Parameters.AddWithValue("idNeg",negociacao.IdNegociacao);
                command.Parameters.AddWithValue("precoBase", negociacao.PrecoBase);
                command.Parameters.AddWithValue("precoNeg", negociacao.PrecoNegociacao);
                command.Parameters.AddWithValue("sucesso", negociacao.Sucesso);
                command.Parameters.AddWithValue("ultimoPropor", negociacao.Resposta);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        public Negociacao? Get(int idNeg)
        {
            using SqlConnection connection = new(ConnectionDAO.connectionString);
            using SqlCommand command = new("SELECT * FROM [Negociacao] WHERE idNeg = (@idNeg)", connection);
            {
                connection.Open();
                command.Parameters.AddWithValue("idNeg", idNeg);
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                if (response.HasRows)
                {
                    response.Read();
                    float precoBase = (float)response.GetFieldValue<double>("precoBase");
                    float precoNeg = (float)response.GetFieldValue<double>("precoNeg");
                    bool sucesso = response.GetFieldValue<bool>("sucesso");
                    bool resposta = response.GetFieldValue<bool>("ultimoPropor");
                    return new Negociacao(idNeg,precoBase, precoNeg, sucesso,resposta);
                }
                response.Close();
            }
            return null;
        }

        public List<Negociacao> ListAllNegociacoes()
        {
            List<Negociacao> r = new();
            using (SqlConnection connection = new(ConnectionDAO.connectionString))
            using (SqlCommand command = new("SELECT * FROM [Negociacao]", connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                if (response.HasRows)
                {
                    while (response.Read())
                    {
                        int idNegociacao = response.GetFieldValue<int>("idNeg");
                        float precoBase = (float)response.GetFieldValue<double>("precoBase");
                        float precoNeg = (float)response.GetFieldValue<double>("precoNeg");
                        bool sucesso = response.GetFieldValue<bool>("sucesso");
                        bool resposta = response.GetFieldValue<bool>("ultimoPropor");
                        r.Add(new Negociacao(idNegociacao, precoBase, precoNeg, sucesso, resposta));
                    }
                }
                connection.Close();
                return r;
            }
        }

        public int GetNextId()
        {
            int r = 0;
            using SqlConnection connection = new(ConnectionDAO.connectionString);
            using SqlCommand command = new("SELECT ISNULL(MAX(idNeg)+1,0) AS MaiorID FROM [Negociacao]", connection);
            {
                connection.Open();
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                if (response.HasRows)
                {
                    response.Read();
                    r = response.GetFieldValue<int>("MaiorID");
                }
                connection.Close();
                return r;
            }
        }


        public bool GetUltimoPropor(int idNegociacao)
        {
            using SqlConnection connection = new(ConnectionDAO.connectionString);
            using SqlCommand command = new("SELECT * FROM [Negociacao] WHERE idNeg = (@idNeg)", connection);
            {
                connection.Open();
                command.Parameters.AddWithValue("idNeg", idNegociacao);
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                if (response.HasRows)
                {
                    response.Read();
                    bool resposta = response.GetFieldValue<bool>("ultimoPropor");
                    return resposta;
                }
                response.Close();
            }
            return false;
        }


        public void AlteraUltimoPropor(int idNegociacao)
        {
            using (SqlConnection connection = new(ConnectionDAO.connectionString))
            using (SqlCommand command = new("UPDATE [Negociacao] SET ultimoPropor = (@ultimoPropor) WHERE idNeg = (@idNeg)", connection))
            {
                connection.Open();
                bool ultimoPropor = GetUltimoPropor(idNegociacao);
                command.Parameters.AddWithValue("@ultimoPropor", !ultimoPropor);
                command.Parameters.AddWithValue("@idNeg", idNegociacao);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public bool GetSucesso(int idNegociacao)
        {
            using SqlConnection connection = new(ConnectionDAO.connectionString);
            using SqlCommand command = new("SELECT * FROM [Negociacao] WHERE idNeg = (@idNeg)", connection);
            {
                connection.Open();
                command.Parameters.AddWithValue("idNeg", idNegociacao);
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                if (response.HasRows)
                {
                    response.Read();
                    bool resposta = response.GetFieldValue<bool>("sucesso");
                    return resposta;
                }
                response.Close();
            }
            return false;
        }

        public void AlteraSucesso(int idNegociacao)
        {
            using (SqlConnection connection = new(ConnectionDAO.connectionString))
            using (SqlCommand command = new("UPDATE [Negociacao] SET sucesso = (@sucesso) WHERE idNeg = (@idNeg)", connection))
            {
                connection.Open();
                bool sucesso = GetSucesso(idNegociacao);
                command.Parameters.AddWithValue("@sucesso", !sucesso);
                command.Parameters.AddWithValue("@idNeg", idNegociacao);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

		public void Insucesso(int idNegociacao)
		{
			using (SqlConnection connection = new(ConnectionDAO.connectionString))
			using (SqlCommand command = new("DELETE FROM [Negociacao] WHERE idNeg = (@idNegociacao)", connection))
			{
                connection.Open();
				command.Parameters.AddWithValue("@idNegociacao", idNegociacao);
                command.ExecuteNonQuery();
                connection.Close();
			}
		}

		public void NovaProposta(int idNegociacao, float proposta)
        {
            using (SqlConnection connection = new(ConnectionDAO.connectionString))
            using (SqlCommand command = new("UPDATE [Negociacao] SET precoNeg = (@precoNeg) WHERE idNeg = (@idNeg)", connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@precoNeg",proposta);
                command.Parameters.AddWithValue("@idNeg", idNegociacao);
                command.ExecuteNonQuery();
                AlteraUltimoPropor(idNegociacao);
                connection.Close();
            }
        }



    }
}

