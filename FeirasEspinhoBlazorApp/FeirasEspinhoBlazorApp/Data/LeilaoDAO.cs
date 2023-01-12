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

        public bool LeilaoHasBids(int id)
        {
            bool r = false;
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

        public bool LeilaoHasBidFromCliente(int id,String clienteEmail)
        {
            bool r = false;
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

        public void InsertLeilao(Leilao leilao)
        {
            using SqlConnection connection = new(ConnectionDAO.connectionString);
            using SqlCommand command = new("INSERT INTO [dbo].[Leilao] VALUES (@id, @dataLimite, @valorMinimo, @valorMaximo, @produto, @quantidade, @stand, @feira)", connection);
            {
                connection.Open();
                command.Parameters.AddWithValue("@id", leilao.Id);
                command.Parameters.AddWithValue("@dataLimite", leilao.Date);
                command.Parameters.AddWithValue("@valorMinimo", leilao.ValormMinimo);
                if(leilao.ValormMaximo.HasValue)
                    command.Parameters.AddWithValue("@valorMaximo", leilao.ValormMaximo);
                else
                    command.Parameters.AddWithValue("@valorMaximo", DBNull.Value);
                command.Parameters.AddWithValue("@produto", leilao.Produto);
                command.Parameters.AddWithValue("@quantidade", leilao.Quantidade);
                command.Parameters.AddWithValue("@stand", leilao.Stand);
                command.Parameters.AddWithValue("@feira", leilao.Feira);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void InsertBid(int leilao, string clienteEmail, float quantia)
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
		public void UpdateBid(int leilao, string clienteEmail, float quantia)
        {

			using (SqlConnection connection = new(ConnectionDAO.connectionString))
			using (SqlCommand command = new("UPDATE [PrecosLeilao] SET precoProposto = (@precoProposto) WHERE leilao = (@idleilao) AND clienteEmail = (@clienteEmail)", connection))
			{
				connection.Open();
				command.Parameters.AddWithValue("@precoProposto", quantia);
				command.Parameters.AddWithValue("@idleilao", leilao);
				command.Parameters.AddWithValue("@clienteEmail", clienteEmail);
				command.ExecuteNonQuery();
				connection.Close();
			}
		}


		public float? GetBid(int leilao, String clienteEmail)
        {
            using SqlConnection connection = new(ConnectionDAO.connectionString);
            using SqlCommand command = new("SELECT * FROM [PrecosLeilao] WHERE leilao = (@leilao) AND clienteEmail = (@clienteEmail)", connection);
            {
                connection.Open();
                command.Parameters.AddWithValue("@leilao", leilao);
                command.Parameters.AddWithValue("clienteEmail", clienteEmail);
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                if (response.HasRows)
                {
                    response.Read();
                    float r = (float)response.GetFieldValue<double>("precoProposto");
                    return r;
                }
                connection.Close();
            }
            return null;
        }

        public float GetMaiorBid(int leilao)
        {
            float r = 0;
            using SqlConnection connection = new(ConnectionDAO.connectionString);
            using SqlCommand command = new("SELECT * FROM [PrecosLeilao] WHERE leilao = (@leilao)", connection);
            {
                connection.Open();
                command.Parameters.AddWithValue("@leilao", leilao);
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                if (response.HasRows)
                {
                    while (response.Read())
                    {
                        float bid = (float)response.GetFieldValue<double>("precoProposto");
                        if (bid > r) r = bid;
                    }
                }
                connection.Close();
            }
            return r;
        }

        public Leilao? GetLeilao(int id)
        {
            using SqlConnection connection = new(ConnectionDAO.connectionString);
            using SqlCommand command = new("SELECT * FROM [Leilao] WHERE id = (@id)", connection);
            {
                connection.Open();
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                if (response.HasRows)
                {
                    response.Read();
                    DateTime dataLimite = response.GetFieldValue<DateTime>("dataLimite");
                    float valorMinimo = (float)response.GetFieldValue<double>("valorMinimo");
                    float? valorMaximo = null;
                    if (!response.IsDBNull("valorMaximo"))
                        valorMaximo = (float)response.GetFieldValue<double>("valorMaximo");
                    int produto = response.GetFieldValue<int>("produto");
                    int quantidade = response.GetFieldValue<int>("quantidade");
                    int stand = response.GetFieldValue<int>("stand");
                    int feira = response.GetFieldValue<int>("feira");
                    float bid = GetMaiorBid(id);
                    connection.Close();
                    return new Leilao(id,dataLimite,valorMinimo,valorMaximo,produto,quantidade,stand,feira,bid);
                }
            }
            return null;
        }

        public List<Leilao> ListAllLeiloes()
        {
            List<Leilao> r = new();
            using (SqlConnection connection = new(ConnectionDAO.connectionString))
            using (SqlCommand command = new("SELECT * FROM [Leilao]", connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                if (response.HasRows)
                {
                    while (response.Read())
                    {
                        int id = response.GetFieldValue<int>("id");
                        DateTime dataLimite = response.GetFieldValue<DateTime>("dataLimite");
                        float valorMinimo = (float)response.GetFieldValue<double>("valorMinimo");
                        float? valorMaximo = null;
                        if (!response.IsDBNull("valorMaximo"))
				            valorMaximo = (float)response.GetFieldValue<double>("valorMaximo");
			            int produto = response.GetFieldValue<int>("produto");
                        int quantidade = response.GetFieldValue<int>("quantidade");
                        int stand = response.GetFieldValue<int>("stand");
                        int feira = response.GetFieldValue<int>("feira");
                        float bid = GetMaiorBid(id);
                        r.Add(new Leilao(id, dataLimite, valorMinimo, valorMaximo, produto, quantidade, stand, feira, bid));
                    }
                }
                connection.Close();
            }
            return r;
        }

        public Dictionary<(int,String),float> GetAllBids()
        {
            Dictionary<(int, String), float> r = new();
            using (SqlConnection connection = new(ConnectionDAO.connectionString))
            using (SqlCommand command = new("SELECT * FROM [PrecosLeilao]", connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                if (response.HasRows)
                {
                    while (response.Read())
                    {
                        int leilao = response.GetFieldValue<int>("leilao");
                        String clienteEmail = response.GetFieldValue<String>("clienteEmail");
                        float bid = (float)response.GetFieldValue<double>("precoProposto");
                        r.Add((leilao, clienteEmail), bid);
                    }
                }
                connection.Close();
            }
            return r;
        }

        public Dictionary<String, float> GetAllBidsLeilao(int leilao)
        {
            Dictionary<String, float> r = new();
            using (SqlConnection connection = new(ConnectionDAO.connectionString))
            using (SqlCommand command = new("SELECT * FROM [PrecosLeilao] WHERE leilao = (@leilao)", connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@leilao", leilao);
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                if (response.HasRows)
                {
                    while (response.Read())
                    {
                        String clienteEmail = response.GetFieldValue<String>("clienteEmail");
                        float bid = (float)response.GetFieldValue<double>("precoProposto");
                        r.Add(clienteEmail, bid);
                    }
                }
                connection.Close();
            }
            return r;
        }

        public List<Leilao> ListLeiloesFeira(int feira)
        {
            List<Leilao> r = new();
            using (SqlConnection connection = new(ConnectionDAO.connectionString))
            using (SqlCommand command = new("SELECT * FROM [Leilao] WHERE feira = (@feira)", connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@feira", feira);
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                if (response.HasRows)
                {
                    while (response.Read())
                    {
                        int id = response.GetFieldValue<int>("id");
                        DateTime dataLimite = response.GetFieldValue<DateTime>("dataLimite");
                        float valorMinimo = (float)response.GetFieldValue<double>("valorMinimo");
			            float? valorMaximo = null;
			            if (!response.IsDBNull("valorMaximo"))
				            valorMaximo = (float)response.GetFieldValue<double>("valorMaximo");
                        int produto = response.GetFieldValue<int>("produto");
                        int quantidade = response.GetFieldValue<int>("quantidade");
                        int stand = response.GetFieldValue<int>("stand");
                        float bid = GetMaiorBid(id);
                        r.Add(new Leilao(id, dataLimite, valorMinimo, valorMaximo, produto, quantidade, stand, feira, bid));
                    }
                }
                connection.Close();
            }
            return r;
        }

        public List<Leilao> GetLeiloesCliente(String clienteEmail)
        {
            List<Leilao> r = new List<Leilao>();
            using (SqlConnection connection = new(ConnectionDAO.connectionString))
            using (SqlCommand command = new("SELECT * FROM [PrecosLeilao] WHERE clienteEmail = (@clienteEmail)", connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@clienteEmail", clienteEmail);
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                if (response.HasRows)
                {
                    while (response.Read())
                    {
                        int leilao = response.GetFieldValue<int>("leilao");
                        r.Add(GetLeilao(leilao));
                    }
                }
                connection.Close();
            }
            return r;
        }

        public int GetNextId()
        {
            int r = 0;
            using SqlConnection connection = new(ConnectionDAO.connectionString);
            using SqlCommand command = new("SELECT  ISNULL(MAX(id)+1,0) AS MaiorID FROM [Leilao]", connection);
            {
                connection.Open();
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                if (response.HasRows)
                {
                    response.Read();
					if (response.IsDBNull(0))
						r = response.GetFieldValue<int>("MaiorID")+1;
                }
                connection.Close();
                return r;
            }
        }
    }
}