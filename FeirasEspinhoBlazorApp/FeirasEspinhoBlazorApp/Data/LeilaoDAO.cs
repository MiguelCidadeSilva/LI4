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
    public class LeilaoDAO
    {
        private static LeilaoDAO instance = new LeilaoDAO();

        public static LeilaoDAO GetInstance()
        {
            return instance;
        }

        public bool ContainsKeyLeilao(int id)
        {
            bool r = false;
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using (SqlCommand command = new("SELECT * FROM [Leilao] WHERE id = (@id)", connection))
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

        public bool LeilaoHasBids(int id)
        {
            bool r = false;
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using (SqlCommand command = new("SELECT * FROM [PrecosLeilao] WHERE leilao = (@leilao)", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@leilao", id);
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

        public bool LeilaoHasBidFromCliente(int id,String clienteEmail)
        {
            bool r = false;
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using (SqlCommand command = new("SELECT * FROM [PrecosLeilao] WHERE leilao = (@leilao) AND clienteEmail = (@clienteEmail)", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@leilao", id);
                    command.Parameters.AddWithValue("@clienteEmail", clienteEmail);
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

        public void InsertLeilao(Leilao leilao)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("INSERT INTO [dbo].[Leilao] VALUES (@id, @dataLimite, @valorMinimo, @valorMaximo, @produto, @quantidade, @stand, @feira)", connection);
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@id", leilao.Id);
                    command.Parameters.AddWithValue("@dataLimite", leilao.Date);
                    command.Parameters.AddWithValue("@valorMinimo", leilao.ValormMinimo);
                    command.Parameters.AddWithValue("@valorMaximo", leilao.ValormMaximo);
                    command.Parameters.AddWithValue("@produto", leilao.ValormMaximo);
                    command.Parameters.AddWithValue("@quantidade", leilao.Quantidade);
                    command.Parameters.AddWithValue("@stand", leilao.Stand);
                    command.Parameters.AddWithValue("@feira", leilao.Feira);
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

        public void InsertBid(int leilao, String clienteEmail, float quantia)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("INSERT INTO [dbo].[PrecosLeilao] VALUES (@leilao, @clienteEmail, @precoProposto)", connection);
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@leilao", leilao);
                    command.Parameters.AddWithValue("@clienteEmail", clienteEmail);
                    command.Parameters.AddWithValue("@precoProposto", quantia);
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

        public float? GetBid(int leilao, String clienteEmail)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("SELECT * FROM [PrecosLeilao] WHERE leilao = (@leilao) AND clienteEmail = (@clienteEmail)", connection);
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@leilao", leilao);
                    command.Parameters.AddWithValue("clienteEmail", clienteEmail);
                    command.ExecuteNonQuery();
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {   
                        float r = (float)response.GetFieldValue<double>("precoProposto");
                        return r;
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

        public float GetMaiorBid(int leilao)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("SELECT * FROM [PrecosLeilao] WHERE leilao = (@leilao)", connection);
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@leilao", leilao);
                    command.ExecuteNonQuery();
                    SqlDataReader response = command.ExecuteReader();
                    float r = 0;
                    while (response.Read())
                    {
                        float bid = (float)response.GetFieldValue<double>("precoProposto");
                        if (bid > r) r = bid;
                    }
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
            return 0;
        }


        public Leilao? GetLeilao(int id)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("SELECT * FROM [Leilao] WHERE id = (@id)", connection);
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {
                        DateTime dataLimite = response.GetFieldValue<DateTime>("dataLimite");
                        float valorMinimo = (float)response.GetFieldValue<double>("valorMinimo");
                        float valorMaximo = (float)response.GetFieldValue<double>("valorMaximo");
                        int produto = response.GetFieldValue<int>("produto");
                        int quantidade = response.GetFieldValue<int>("quantidade");
                        int stand = response.GetFieldValue<int>("stand");
                        int feira = response.GetFieldValue<int>("feira");
                        float bid = GetMaiorBid(id);
                        connection.Close();
                        return new Leilao(id,dataLimite,valorMinimo,valorMaximo,produto,quantidade,stand,feira,bid);
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

        public List<Leilao>? ListAllLeiloes()
        {
            try
            {
                List<Leilao> r = new();
                using (SqlConnection connection = new(ConnectionDAO.connectionString))
                using (SqlCommand command = new("SELECT * FROM [Leilao]", connection))
                {
                    connection.Open();
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {
                        int id = response.GetFieldValue<int>("id");
                        DateTime dataLimite = response.GetFieldValue<DateTime>("dataLimite");
                        float valorMinimo = (float)response.GetFieldValue<double>("valorMinimo");
                        float valorMaximo = (float)response.GetFieldValue<double>("valorMaximo");
                        int produto = response.GetFieldValue<int>("produto");
                        int quantidade = response.GetFieldValue<int>("quantidade");
                        int stand = response.GetFieldValue<int>("stand");
                        int feira = response.GetFieldValue<int>("feira");
                        float bid = GetMaiorBid(id);
                        r.Add(new Leilao(id, dataLimite, valorMinimo, valorMaximo, produto, quantidade, stand, feira, bid));
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

        public Dictionary<(int,String),float> GetAllBids()
        {
            try
            {
                Dictionary<(int, String), float> r = new();
                using (SqlConnection connection = new(ConnectionDAO.connectionString))
                using (SqlCommand command = new("SELECT * FROM [PrecosLeilao]", connection))
                {
                    connection.Open();
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {
                        int leilao = response.GetFieldValue<int>("leilao");
                        String clienteEmail = response.GetFieldValue<String>("clienteEmail");
                        float bid = (float)response.GetFieldValue<double>("precoProposto");
                        r.Add((leilao,clienteEmail),bid);
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

        public Dictionary<String, float> GetAllBidsLeilao(int leilao)
        {
            try
            {
                Dictionary<String, float> r = new();
                using (SqlConnection connection = new(ConnectionDAO.connectionString))
                using (SqlCommand command = new("SELECT * FROM [PrecosLeilao] WHERE leilao = (@leilao)", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@leilao", leilao);
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {
                      
                        String clienteEmail = response.GetFieldValue<String>("clienteEmail");
                        float bid = (float)response.GetFieldValue<double>("precoProposto");
                        r.Add(clienteEmail, bid);
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