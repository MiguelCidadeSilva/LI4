using static System.Net.Mime.MediaTypeNames;
using System.Data;
using Microsoft.AspNetCore.Http;
using FeirasEspinhoBlazorApp.SourceCode;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using System.Text;

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

        public void GenerateData()
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
                        string nome = response.GetFieldValue<string>("nome");
                        float imposto = (float)response.GetFieldValue<double>("imposto");
                        int categoria = response.GetFieldValue<int>("categoria");
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
    }
}
