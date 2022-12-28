using static System.Net.Mime.MediaTypeNames;
using System.Data;
using Microsoft.AspNetCore.Http;
using FeirasEspinhoBlazorApp.SourceCode.Stands;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Collections.Generic;

namespace FeirasEspinhoBlazorApp.Data
{
    public class StandDAO
    {
        private static StandDAO instance = new StandDAO();

        public static StandDAO GetInstance()
        {
            return instance;
        }

        public void InsertStand(Stand stand)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("INSERT INTO [dbo].[Stand] VALUES (@idStand, @negociavel, @consultantes, @dataCriacao, @donoEmail, @categoria)", connection);
                connection.Open();
                command.Parameters.AddWithValue("@idStand", stand.IdStand);
                command.Parameters.AddWithValue("@negociavel", stand.Negociavel);
                command.Parameters.AddWithValue("@consultantes", stand.Consultantes);
                command.Parameters.AddWithValue("@dataCriacao", stand.DataCriacao);
                command.Parameters.AddWithValue("@donoEmail", stand.EmailDono);
                command.Parameters.AddWithValue("@categoria", stand.categoria);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                Console.WriteLine(errorMessages.ToString());
            }
        }

        public void InsertProduto(Produto produto)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("INSERT INTO [dbo].[Produto] VALUES (@idProd, @nome, @idSubCategoria, @banca, @stock, @preco, @disponivel)", connection);
                connection.Open();
                command.Parameters.AddWithValue("@Prod", produto.IdProduto);
                command.Parameters.AddWithValue("@nome", produto.Nome);
                command.Parameters.AddWithValue("@idSubCategoria", produto.IdSubCategoria);
                command.Parameters.AddWithValue("@banca", produto.Stand);
                command.Parameters.AddWithValue("@stock", produto.Stock);
                command.Parameters.AddWithValue("@preco", produto.Preco);
                command.Parameters.AddWithValue("@disponivel", produto.Disponivel);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                Console.WriteLine(errorMessages.ToString());
            }
        }

        public List<Produto>? GetProdutosStand(int idStand)
		{
			List<Produto>? res = null;
			try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("SELECT * FROM [Produto] WHERE banca = (@banca)", connection);
                connection.Open();
                command.Parameters.AddWithValue("@banca", idStand);
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                if (response.HasRows)
                {
                    int idProd = response.GetFieldValue<int>("idProd");
                    string nome = response.GetFieldValue<string>("nome");
                    int idSubCategoria = response.GetFieldValue<int>("idSubCategoria");
                    int stock = response.GetFieldValue<int>("stock");
                    float preco = response.GetFieldValue<float>("preco");
                    bool disponivel = response.GetFieldValue<bool>("disponivel");
                    connection.Close();
                    res.Add(new Produto(idProd, nome, idSubCategoria, idStand, stock, preco, disponivel));
                }
                return res;
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                Console.WriteLine(errorMessages.ToString());
            }
            return res;
        }


        public Produto? GetProduto(int id)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("SELECT * FROM [Produto] WHERE idProd = (@idProd)", connection);
                connection.Open();
                command.Parameters.AddWithValue("@idProd", id);
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                if (response.HasRows)
                {
                    string nome = response.GetFieldValue<string>("nome");
                    int idSubCategoria = response.GetFieldValue<int>("idSubCategoria");
                    int stand = response.GetFieldValue<int>("banca");
                    int stock = response.GetFieldValue<int>("stock");
                    float preco = response.GetFieldValue<float>("preco");
                    bool disponivel = response.GetFieldValue<bool>("disponivel");
                    connection.Close();
                    return new Produto(id, nome, idSubCategoria, stand, stock, preco, disponivel);
                }
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                Console.WriteLine(errorMessages.ToString());
            }
            return null;
        }

        public Stand? GetStand(int id)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("SELECT * FROM [Stand] WHERE idStand = (@idStand)", connection);
                connection.Open();
                command.Parameters.AddWithValue("@idStand", id);
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                if (response.HasRows)
                {
                    bool negociavel = response.GetFieldValue<bool>("negociavel");
                    int consultantes = response.GetFieldValue<int>("consultantes");
                    DateTime dataCriacao = response.GetFieldValue<DateTime>("dataCriacao");
                    string donoEmail = response.GetFieldValue<string>("donoEmail");
                    int categoria = response.GetFieldValue<int>("categoria");
                    List<Produto> produtos = this.GetProdutosStand(id);
                    connection.Close();
                    return new Stand(id, negociavel, consultantes, dataCriacao, donoEmail, categoria, produtos);
                }
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                Console.WriteLine(errorMessages.ToString());
            }
            return null;
        }
    }
}
