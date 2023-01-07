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

        public bool ContainsKey(int idFeira)
        {
            bool r = false;
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using (SqlCommand command = new("SELECT * FROM [Feira] WHERE idFeira = (@idFeira)", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@idFeira", idFeira);
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
				string sql = @"INSERT INTO Feira (idFeira, nome, dataInicio, dataFim, precoCandidatura, criadorEmail, categoria)
                        VALUES
                            (1, 'Feira de frutas', '2022-12-16', '2022-12-18', 10.00, 'isaura@hotmail.com', 1),
                            (2, 'Feira de roupas', '2022-12-19', '2022-12-21', 15.00, 'cristiano@hotmail.com', 2),
                            (3, 'Feira de brinquedos', '2022-12-22', '2022-12-24', 20.00, 'eduarda@hotmail.com', 3),
                            (4, 'Feira de eletrônicos', '2022-12-25', '2022-12-27', 25.00, 'isaura@hotmail.com', 4),
                            (5, 'Feira de livros', '2022-12-28', '2022-12-30', 30.00, 'marco@hotmail.com', 5),
                            (6, 'Feira de móveis', '2022-12-31', '2023-01-02', 35.00, 'eduarda@hotmail.com', 6),
                            (7, 'Feira de cosméticos', '2023-01-03', '2023-01-05', 40.00, 'rui@hotmail.com', 7),
                            (8, 'Feira de ferramentas', '2023-01-06', '2023-01-08', 45.00, 'evandro@hotmail.com', 8),
                            (9, 'Feira de desporto', '2023-01-09', '2023-01-11', 50.00, 'diogo@hotmail.com', 9),
                            (10, 'Feira de jardinagem', '2023-01-12', '2023-01-14', 55.00, 'filipa@hotmail.com', 10),
                            (11, 'Feira de jardinagem', '2023-01-12',  null, 60.00, 'manuel@hotmail.com', null),
                            (12, 'Feira de jardinagem', '2023-01-12', '2023-02-20', 65.00, 'angelico@hotmail.com', null)";
				SqlCommand command = new(sql, connection);
				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
			}
		}
        public void Insert(Feira feira)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("INSERT INTO [dbo].[Feira] VALUES (@idFeira, @nome, @dataInicio, @dataFim, @precoCandidatura, @criadorEmail, @categoria)", connection);
                {
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
        public Feira? this[int id] => GetFeira(id);
        public Feira? GetFeira(int id)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("SELECT * FROM [Feira] WHERE idFeira = (@idFeira)", connection);
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@idFeira", id);
                    command.ExecuteNonQuery();
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {
                        int iDFeira = response.GetFieldValue<int>("idFeira");
                        string nome = response.GetFieldValue<string>("nome");
                        DateTime dataInicio = response.GetFieldValue<DateTime>("dataInicio");
                        DateTime dataFim = response.GetFieldValue<DateTime>("dataFim");
                        float precoCandidatura = (float)response.GetFieldValue<double>("precoCandidatura");
                        string criadorEmail = response.GetFieldValue<string>("criadorEmail");
                        int categoria = response.GetFieldValue<int>("categoria");
                        connection.Close();
                        return new Feira(iDFeira, nome, dataInicio, dataFim, precoCandidatura, criadorEmail, categoria);
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

        public List<Feira>? ListAllFeiras()
        {
            try
            {
                List<Feira> r = new();
                using (SqlConnection connection = new(ConnectionDAO.connectionString))
                using (SqlCommand command = new("SELECT * FROM [Feira]", connection))
                {
                    connection.Open();
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {
                        int idFeira = response.GetInt32("idFeira");
                        String nome = response.GetString("nome");
                        DateTime dataI = response.GetDateTime("dataInicio");
                        DateTime? dataF = null;
                        if(!response.IsDBNull("dataFim"))
                            dataF = response.GetDateTime("dataFim");
                        float precoCand = (float)response.GetDouble("precoCandidatura");
                        String criadorEmail = response.GetString("criadorEmail");
                        int? categoria = null;
						if (!response.IsDBNull("categoria")) 
                            categoria = response.GetInt32("categoria");
                        r.Add(new Feira(idFeira,nome,dataI,dataF,precoCand,criadorEmail,categoria));
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