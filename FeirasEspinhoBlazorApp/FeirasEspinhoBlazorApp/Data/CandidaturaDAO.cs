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
    public class CandidaturaDAO
    {
        private static CandidaturaDAO instance = new CandidaturaDAO();

        public static CandidaturaDAO GetInstance()
        {
            return instance;
        }

        public bool ContainsKey(int idCandidatura)
        {
            bool r = false;
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

        public void InsertCandidatura(Candidatura candidatura)
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

        public Candidatura? GetCandidatura(int idCandidatura)
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
                return null;
            }
        }

        public List<Candidatura> ListAllCandidatura()
        {
            List<Candidatura> r = new List<Candidatura>();
            using SqlConnection connection = new(ConnectionDAO.connectionString);
            using SqlCommand command = new("SELECT * FROM [Candidatura]", connection);
            {
                connection.Open();
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                if (response.HasRows)
                {
                    while (response.Read())
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
      
        public int GetNextId()
        {
            int r = 0;
            using SqlConnection connection = new(ConnectionDAO.connectionString);
            using SqlCommand command = new("SELECT  ISNULL(MAX(idCandidatura)+1,0) AS MaiorID FROM [Candidatura]", connection);
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

        public void DeleteCandidatura(int idCandidatura)
        {
            using (SqlConnection connection = new(ConnectionDAO.connectionString))
            using (SqlCommand command = new("DELETE FROM [Candidatura] WHERE idCandidatura=(@idCandidatura)", connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@idCandidatura", idCandidatura);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Aprova(int idCandidatura, bool aprovacao)
        {
            if (aprovacao)
            {
                using (SqlConnection connection = new(ConnectionDAO.connectionString))
                using (SqlCommand command = new("UPDATE [Candidatura] SET aprovacao = (@aprovacao) WHERE idCandidatura = (@idCandidatura)", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@aprovacao", true);
                    command.Parameters.AddWithValue("@idCandidatura", idCandidatura);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            else DeleteCandidatura(idCandidatura);
        }


    }
}
