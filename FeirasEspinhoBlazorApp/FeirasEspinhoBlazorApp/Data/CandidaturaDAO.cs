using static System.Net.Mime.MediaTypeNames;
using System.Data;
using Microsoft.AspNetCore.Http;
using FeirasEspinhoBlazorApp.SourceCode.Feiras;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using FeirasEspinhoBlazorApp.SourceCode.Stands;


namespace FeirasEspinhoBlazorApp.Data
{
    public class CandidadturaDAO
    {
        private static CandidadturaDAO instance = new CandidadturaDAO();

        public static CandidadturaDAO GetInstance()
        {
            return instance;
        }

        public bool ContainsKey(int idCandidatura)
        {
            bool r = false;
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using (SqlCommand command = new("SELECT * FROM [Candidatura] WHERE idCandidatura = (@idCandidatura)", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@idCandidatura", idCandidatura);
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

        public void InsertCandidatura(Candidatura candidatura)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("INSERT INTO [dbo].[Candidatura] VALUES (@idCandidatura, @dataSubmissao, @aprovacao, @idStand, @idFeira)", connection);
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@idCandidatura", candidatura.IdCandidatura);
                    command.Parameters.AddWithValue("@dataSubmissao", candidatura.DataSubmissao);
                    command.Parameters.AddWithValue("@aprovacao", candidatura.Aprovacao);
                    command.Parameters.AddWithValue("@idStand", candidatura.IdStand);
                    command.Parameters.AddWithValue("@idFeira", candidatura.IdFeira);
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

        public Candidatura? GetCandidatura(int idCandidatura)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("SELECT * FROM [Candidatura] WHERE idCandidatura = (@idCandidatura)", connection);
                {
                    connection.Open();
                    command.Parameters.AddWithValue("idCandidatura", idCandidatura);
                    command.ExecuteNonQuery();
                    SqlDataReader response = command.ExecuteReader();
                    if (response.HasRows)
                    {
                        response.Read();
                        DateTime DataSubmissao = response.GetFieldValue<DateTime>("dataSubmissao");
                        bool Aprovacao = response.GetFieldValue<bool>("aprovacao");
                        int IdStand = response.GetFieldValue<int>("idStand");
                        int IdFeira = response.GetFieldValue<int>("idFeira");
                        return new Candidatura(idCandidatura, DataSubmissao, Aprovacao, IdStand, IdFeira);
                    }
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
            return null;
        }

        public List<Candidatura> ListAllCandidatura()
        {
            List<Candidatura> r = new List<Candidatura>();
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("SELECT * FROM [Candidatura]", connection);
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    SqlDataReader response = command.ExecuteReader();
                    if (response.HasRows)
                    {
                        while (response.HasRows)
                        {
                            int idCandidatura = response.GetFieldValue<int>("idCandidatura");
                            DateTime DataSubmissao = response.GetFieldValue<DateTime>("dataSubmissao");
                            bool Aprovacao = response.GetFieldValue<bool>("aprovacao");
                            int IdStand = response.GetFieldValue<int>("idStand");
                            int IdFeira = response.GetFieldValue<int>("idFeira");
                            r.Add(new Candidatura(idCandidatura, DataSubmissao, Aprovacao, IdStand, IdFeira));
                        }
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
            return r;
        }

        public int GetNextId()
        {
            int r = 1;
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("SELECT MAX idCandidatura AS MaiorID FROM [Candidatura]", connection);
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
                    return r+1;
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
                return r;
            }
        }
    }
}
