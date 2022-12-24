using static System.Net.Mime.MediaTypeNames;
using System.Data;
using Microsoft.AspNetCore.Http;
using FeirasEspinhoBlazorApp.SourceCode.Feiras;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace FeirasEspinhoBlazorApp.Data
{
    public class FeiraDAO
    {
        private static FeiraDAO instance = new FeiraDAO();

        public static FeiraDAO GetInstance()
        {
            return instance;
        }

        public void Insert(Feira feira)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("INSERT INTO [dbo].[Feira] VALUES (@idFeira, @nome, @dataInicio, @dataFim, @precoCandidatura, @criadorEmail, @categoria)", connection);
                connection.Open();
                command.Parameters.AddWithValue("@idFeira", feira.IDFeira);
                command.Parameters.AddWithValue("@nome", feira.Nome);
                command.Parameters.AddWithValue("@dataInicio", feira.DataInicio);
                command.Parameters.AddWithValue("@dataFim", feira.DataFim);
                command.Parameters.AddWithValue("@precoCandidatura", feira.PrecoCandidatura);
                command.Parameters.AddWithValue("@criadorEmail", feira.CriadorEmail);
                command.Parameters.AddWithValue("@categoria", feira.Categoria);
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

        public Feira? GetFeira(int id)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("SELECT * FROM [Feira] WHERE idFeira = (@idFeira)", connection);
                connection.Open();
                command.Parameters.AddWithValue("@idFeira", id);
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                if (response.HasRows)
                {
                    int iDFeira = response.GetFieldValue<int>("idFeira");
                    string nome = response.GetFieldValue<string>("nome");
                    DateTime dataInicio = response.GetFieldValue<DateTime>("dataInicio");
                    DateTime dataFim = response.GetFieldValue<DateTime>("dataNascimento");
                    float precoCandidatura = response.GetFieldValue<float>("precoCandidatura");
                    string criadorEmail = response.GetFieldValue<string>("criadorEmail");
                    int categoria = response.GetFieldValue<int>("categoria");
                    connection.Close();
                    return new Feira(iDFeira, nome, dataInicio, dataFim, precoCandidatura, criadorEmail, categoria);
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