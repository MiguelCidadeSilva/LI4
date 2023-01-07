﻿using static System.Net.Mime.MediaTypeNames;
using System.Data;
using Microsoft.AspNetCore.Http;
using FeirasEspinhoBlazorApp.SourceCode;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using FeirasEspinhoBlazorApp.SourceCode.Feiras;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace FeirasEspinhoBlazorApp.Data
{
    public class CategoriaDAO
    {
        private static CategoriaDAO instance = new CategoriaDAO();

        public static CategoriaDAO GetInstance()
        {
            return instance;
        }

        public bool ContainsKey(int id)
        {
            bool r = false;
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using (SqlCommand command = new("SELECT * FROM [Categoria] WHERE idCategoria = (@id)", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@id", id);
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

        public void InsertCategoria(Categoria categoria)
        {
            try
            {
                if (!this.ContainsKey(categoria.Id))
                {
                    using SqlConnection connection = new(ConnectionDAO.connectionString);
                    using SqlCommand command = new("INSERT INTO [dbo].[Categoria] VALUES (@idCategoria, @nome)", connection);
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@idCategoria", categoria.Id);
                        command.Parameters.AddWithValue("@nome", categoria.Name);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                if (categoria is SubCategoria)
                {
                    SubCategoria? sc = categoria as SubCategoria;
                    using SqlConnection connection = new(ConnectionDAO.connectionString);
                    using SqlCommand command = new("INSERT INTO [dbo].[SubCategoria] VALUES (@idSC, @imposto, @categoria)", connection);
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@idSC", value: sc.IdS);
                        command.Parameters.AddWithValue("@imposto", sc.Imposto);
                        command.Parameters.AddWithValue("@categoria", sc.Id);
                        command.ExecuteNonQuery();
                        connection.Close();
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
		}
		public Categoria? this[int id] => GetCategoria(id);

		public Categoria? GetCategoria(int id)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("SELECT * FROM [Categoria] WHERE idCategoria = (@idCategoria)", connection);
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@idCategoria", id);
                    command.ExecuteNonQuery();
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {
                        string nome = response.GetFieldValue<string>("nome");
                        connection.Close();
                        return new Categoria(id,nome);
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

        public String? GetNomeCategoria(int id)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("SELECT * FROM [Categoria] WHERE idCategoria = (@idCategoria)", connection);
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@idCategoria", id);
                    command.ExecuteNonQuery();
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {
                        string nome = response.GetFieldValue<string>("nome");
                        connection.Close();
                        return  nome;
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

        public int? GetIdCategoria(String nome)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("SELECT * FROM [Categoria] WHERE nome = (@nome)", connection);
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@nome", nome);
                    command.ExecuteNonQuery();
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {
                        int id = response.GetFieldValue<int>("idCategoria");
                        connection.Close();
                        return id;
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







        public List<Categoria> ListAllCategoria()
        {
            List<Categoria> r = new();
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("SELECT * FROM [Categoria]", connection);
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {
                        int idCategoria = response.GetFieldValue<int>("idCategoria");
                        String nome = response.GetFieldValue<String>("nome");
                        connection.Close();
                        r.Add(new Categoria(idCategoria,nome));
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
            return r;
        }

        public SubCategoria? GetSubCategoria(int id)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("SELECT * FROM [SubCategoria] WHERE idSC = (@idSC)", connection);
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@idSC", id);
                    command.ExecuteNonQuery();
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {
                        float imposto = (float)response.GetFieldValue<double>("imposto");
                        int categoria = response.GetFieldValue<int>("categoria");
                        string nome = GetNomeCategoria(categoria);
                        connection.Close();
                        return new SubCategoria(categoria,nome,id,imposto);
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

        public List<SubCategoria> ListAllSubCategoria()
        {
            List<SubCategoria> r = new();
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("SELECT * FROM [SubCategoria]", connection);
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {
                        int idSC = response.GetFieldValue<int>("idSC");
                        float imposto = (float)response.GetFieldValue<double>("imposto");
                        int categoria = response.GetFieldValue<int>("categoria");
                        string nome = GetNomeCategoria(categoria);
                        connection.Close();
                        r.Add(new SubCategoria(categoria,nome,idSC,imposto));
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
            return r;
        }

        private void GenerateDataCat()
		{
			using (SqlConnection connection = new SqlConnection(ConnectionDAO.connectionString))
			{
				string sql = @"INSERT INTO Categoria (idCategoria, nome)
                VALUES
                    (1, 'Fruta'),
                    (2, 'Vestuário'),
                    (3, 'Brinquedos'),
                    (4, 'Eletrônicos'),
                    (5, 'Livros'),
                    (6, 'Móveis'),
                    (7, 'Cosméticos'),
                    (8, 'Ferramentas'),
                    (9, 'Desporto'),
                    (10, 'Jardinagem')";
				SqlCommand command = new(sql, connection);
				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
			}
		}
		private void GenerateDataSC()
		{
			using (SqlConnection connection = new SqlConnection(ConnectionDAO.connectionString))
			{

				string sql = @"INSERT INTO SubCategoria (idSC, imposto, categoria)
                            VALUES
                                (1, 0.01, 1),
                                (2, 0.02, 2),
                                (3, 0.03, 3),
                                (4, 0.04, 4),
                                (5, 0.05, 5),
                                (6, 0.06, 6),
                                (7, 0.07, 7),
                                (8, 0.08, 8),
                                (9, 0.09, 9),
                                (10, 0.10, 10),
                                (11, 0.11, 1),
                                (12, 0.12, 2),
                                (13, 0.13, 3),
                                (14, 0.14, 4),
                                (15, 0.15, 5),
                                (16, 0.16, 6),
                                (17, 0.17, 7),
                                (18, 0.18, 8),
                                (19, 0.19, 9),
                                (20, 0.20, 10),
                                (21, 0.21, 1),
                                (22, 0.22, 2),
                                (23, 0.23, 3),
                                (24, 0.01, 4),
                                (25, 0.02, 5)";
				SqlCommand command = new(sql, connection);
				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
			}

		}
		public void GenerateData()
		{
			GenerateDataCat();
			GenerateDataSC();
		}
	}
}
