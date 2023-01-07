using static System.Net.Mime.MediaTypeNames;
using System.Data;
using Microsoft.AspNetCore.Http;
using FeirasEspinhoBlazorApp.SourceCode.Stands;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Collections.Generic;
using FeirasEspinhoBlazorApp.SourceCode.Utilizadores;

namespace FeirasEspinhoBlazorApp.Data
{
    public class StandDAO
    {
        private static StandDAO instance = new StandDAO();

        public static StandDAO GetInstance()
        {
            return instance;
        }

        public bool ContainsKeyStand(int idStand)
        {
            bool r = false;
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using (SqlCommand command = new("SELECT * FROM [Stand] WHERE idStand = (@idStand)", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@idStand", idStand);
                    command.ExecuteNonQuery();
                    SqlDataReader response = command.ExecuteReader();
                    r = response.HasRows;
                    connection.Close();
                }
                return r;
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
                return r;
            }
        }

        public bool ContainsKeyProduto(int idProd)
        {
            bool r = false;
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using (SqlCommand command = new("SELECT * FROM [Produto] WHERE idProd = (@idprod)", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@idProd", idProd);
                    command.ExecuteNonQuery();
                    SqlDataReader response = command.ExecuteReader();
                    r = response.HasRows;
                    connection.Close();
                }
                return r;
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
                return r;
            }
        }

        public void InsertStand(Stand stand)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("INSERT INTO [dbo].[Stand] VALUES (@idStand, @negociavel, @consultantes, @dataCriacao, @donoEmail, @categoria)", connection);
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@idStand", stand.IdStand);
                    command.Parameters.AddWithValue("@negociavel", stand.Negociavel);
                    command.Parameters.AddWithValue("@consultantes", stand.Consultantes);
                    command.Parameters.AddWithValue("@dataCriacao", stand.DataCriacao);
                    command.Parameters.AddWithValue("@donoEmail", stand.EmailDono);
                    command.Parameters.AddWithValue("@categoria", stand.Categoria);
                    command.ExecuteNonQuery();
                    connection.Close();
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
        }

        public void InsertProduto(Produto produto)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("INSERT INTO [dbo].[Produto] VALUES (@idProd, @nome, @idSubCategoria, @banca, @stock, @preco, @disponivel)", connection);
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@idProd", produto.IdProduto);
                    command.Parameters.AddWithValue("@nome", produto.Nome);
                    command.Parameters.AddWithValue("@idSubCategoria", produto.IdSubCategoria);
                    command.Parameters.AddWithValue("@banca", produto.Stand);
                    command.Parameters.AddWithValue("@stock", produto.Stock);
                    command.Parameters.AddWithValue("@preco", produto.Preco);
                    command.Parameters.AddWithValue("@disponivel", produto.Disponivel);
                    command.ExecuteNonQuery();
                    connection.Close();
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
        }

        public List<Produto>? GetProdutosStand(int idStand)
		{
			List<Produto> res = new();
			try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("SELECT * FROM [Produto] WHERE banca = (@banca)", connection);
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@banca", idStand);
                    command.ExecuteNonQuery();
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {
                        int idProd = response.GetFieldValue<int>("idProd");
                        string nome = response.GetFieldValue<string>("nome");
                        int idSubCategoria = response.GetFieldValue<int>("idSubCategoria");
                        int stock = response.GetFieldValue<int>("stock");
                        float preco = (float)response.GetFieldValue<double>("preco");
                        bool disponivel = response.GetFieldValue<bool>("disponivel");
                        res.Add(new Produto(idProd, nome, idSubCategoria, idStand, stock, preco, disponivel));
                    }
                    connection.Close();
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
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@idProd", id);
                    command.ExecuteNonQuery();
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {
                        string nome = response.GetFieldValue<string>("nome");
                        int idSubCategoria = response.GetFieldValue<int>("idSubCategoria");
                        int stand = response.GetFieldValue<int>("banca");
                        int stock = response.GetFieldValue<int>("stock");
                        float preco = (float)response.GetFieldValue<double>("preco");
                        bool disponivel = response.GetFieldValue<bool>("disponivel");
                        connection.Close();
                        return new Produto(id, nome, idSubCategoria, stand, stock, preco, disponivel);
                    }
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

        public Stand? this[int id] => GetStand(id);
        public Stand? GetStand(int id)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("SELECT * FROM [Stand] WHERE idStand = (@idStand)", connection);
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@idStand", id);
                    command.ExecuteNonQuery();
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {
                        bool negociavel = response.GetFieldValue<bool>("negociavel");
                        int consultantes = response.GetFieldValue<int>("consultantes");
                        DateTime dataCriacao = response.GetFieldValue<DateTime>("dataCriacao");
                        string donoEmail = response.GetFieldValue<string>("donoEmail");
                        int categoria = response.GetFieldValue<int>("categoria");
                        List<Produto> produtos = GetProdutosStand(id);
                        connection.Close();
                        return new Stand(id, negociavel, consultantes, dataCriacao, donoEmail, categoria, produtos);
                    }
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

        public List<Stand>? ListAllStands()
        {
            try
            {
                List<Stand> r = new();
                using (SqlConnection connection = new(ConnectionDAO.connectionString))
                using (SqlCommand command = new("SELECT * FROM [Stand]", connection))
                {
                    connection.Open();
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {
                        Stand s = new()
                        {
                            IdStand = response.GetFieldValue<int>("idStand"),
                            Negociavel = response.GetFieldValue<bool>("negociavel"),
                            Consultantes = response.GetFieldValue<int>("consultantes"),
                            DataCriacao = response.GetFieldValue<DateTime>("dataNascimento"),
                            EmailDono = response.GetFieldValue<string>("donoEmail"),
                            Categoria = response.GetFieldValue<int>("categoria")
                        };
                        r.Add(s);
                    }
                    connection.Close();
                    return r;
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

        public List<Produto>? ListAllProdutos()
        {
            try
            {
                List<Produto> r = new();
                using (SqlConnection connection = new(ConnectionDAO.connectionString))
                using (SqlCommand command = new("SELECT * FROM [Produto]", connection))
                {
                    connection.Open();
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {
                        Produto p = new()
                        {
                            IdProduto = response.GetFieldValue<int>("idProd"),
                            Nome = response.GetFieldValue<String>("nome"),
                            IdSubCategoria = response.GetFieldValue<int>("idSubCategoria"),
                            Stand = response.GetFieldValue<int>("banca"),
                            Stock = response.GetFieldValue<int>("stock"),
                            Preco = (float)response.GetFieldValue<double>("preco"),
                            Disponivel = response.GetFieldValue<bool>("disponivel")
                        };
                        r.Add(p);
                    }
                    connection.Close();
                    return r;
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
