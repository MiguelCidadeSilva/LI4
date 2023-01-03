using static System.Net.Mime.MediaTypeNames;
using System.Data;
//using Microsoft.AspNetCore.Http;
using FeirasEspinhoBlazorApp.SourceCode.Vendas;
using Microsoft.Data.SqlClient;
//using Microsoft.AspNetCore.Mvc;
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

        public void InsertCategoria(int idCategoria, string nome)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("INSERT INTO [dbo].[Categoria] VALUES (@idCategoria, @nome)", connection);
                connection.Open();
                command.Parameters.AddWithValue("@idCategoria", idCategoria);
                command.Parameters.AddWithValue("@nome", nome);
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

        public string? GetCategoria(int id)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("SELECT * FROM [Categoria] WHERE idCategoria = (@idCategoria)", connection);
                connection.Open();
                command.Parameters.AddWithValue("@idCategoria", id);
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                while (response.Read())
                {
                    string nome = response.GetFieldValue<string>("nome");
                    connection.Close();
                    return nome;
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

        public void InsertSubCategoria(int idSC, float imposto, string nome)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("INSERT INTO [dbo].[SubCategoria] VALUES (@idSubCategoria, @imposto, @nome)", connection);
                connection.Open();
                command.Parameters.AddWithValue("@idSubCategoria", idSC);
                command.Parameters.AddWithValue("@imposto", imposto);
                command.Parameters.AddWithValue("@nome", nome);
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

        public float? GetImpostoSubCategoria(int id)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("SELECT * FROM [SubCategoria] WHERE idSubCategoria = (@idSubCategoria)", connection);
                connection.Open();
                command.Parameters.AddWithValue("@idSubCategoria", id);
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                while (response.Read())
                {
                    float imposto = response.GetFieldValue<float>("imposto");
                    connection.Close();
                    return imposto;
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

        public int? GetIdCategoriaSubCategoria(int id)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("SELECT * FROM [SubCategoria] WHERE idSubCategoria = (@idSubCategoria)", connection);
                connection.Open();
                command.Parameters.AddWithValue("@idSubCategoria", id);
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                while (response.Read())
                {
                    int categoria = response.GetFieldValue<int>("categoria");
                    connection.Close();
                    return categoria;
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
