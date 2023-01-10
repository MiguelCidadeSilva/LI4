using static System.Net.Mime.MediaTypeNames;
using System.Data;
using Microsoft.AspNetCore.Http;
using FeirasEspinhoBlazorApp.SourceCode.Feiras;
using FeirasEspinhoBlazorApp.SourceCode.Stands;
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

        public void Insert(Feira feira)
        {
            using SqlConnection connection = new(ConnectionDAO.connectionString);
            using SqlCommand command = new("INSERT INTO [dbo].[Feira] VALUES (@idFeira, @nome, @dataInicio, @dataFim, @precoCandidatura, @criadorEmail, @categoria)", connection);
            {
                connection.Open();
                command.Parameters.AddWithValue("@idFeira", feira.IDFeira);
                command.Parameters.AddWithValue("@nome", feira.Nome);
                command.Parameters.AddWithValue("@dataInicio", feira.DataInicio);
                if (feira.DataFim.HasValue)
                    command.Parameters.AddWithValue("@dataFim", feira.DataFim);
                else
                    command.Parameters.AddWithValue("@dataFim", DBNull.Value);
                command.Parameters.AddWithValue("@precoCandidatura", feira.PrecoCandidatura);
                command.Parameters.AddWithValue("@criadorEmail", feira.CriadorEmail);
                if (feira.Categoria.HasValue)
                    command.Parameters.AddWithValue("@categoria", feira.Categoria);
                else 
                    command.Parameters.AddWithValue("@categoria", DBNull.Value);
                command.ExecuteNonQuery();
                connection.Close();
            }          
        }

        public Feira? this[int id] => GetFeira(id);
        public Feira? GetFeira(int id)
        {
            using SqlConnection connection = new(ConnectionDAO.connectionString);
            using SqlCommand command = new("SELECT * FROM [Feira] WHERE idFeira = (@idFeira)", connection);
            {
                connection.Open();
                command.Parameters.AddWithValue("@idFeira", id);
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                if (response.HasRows)
                {    
                    response.Read();
                    string nome = response.GetFieldValue<string>("nome");
                    DateTime dataInicio = response.GetFieldValue<DateTime>("dataInicio"); 
                    DateTime? dataFim = null;
	    			if (!response.IsDBNull("dataFim"))
    	    			dataFim = response.GetDateTime("dataFim");
    				float precoCandidatura = (float)response.GetFieldValue<double>("precoCandidatura");
                    string criadorEmail = response.GetFieldValue<string>("criadorEmail"); 
                    int? categoria = null;
					if (!response.IsDBNull("categoria"))
						categoria = response.GetInt32("categoria");
					List<Leilao> leiloesdafeira = LeilaoDAO.GetInstance().ListLeiloesFeira(id);
                       List<Stand> standsdafeira = StandsDaFeira(id);
                       return new Feira(id, nome, dataInicio, dataFim, precoCandidatura, criadorEmail, categoria,standsdafeira,leiloesdafeira);
                }
                connection.Close();
            }
            return null;
        }

        public List<Feira> ListAllFeiras()
		{
			List<Feira> r = new();
            using (SqlConnection connection = new(ConnectionDAO.connectionString))
            using (SqlCommand command = new("SELECT * FROM [Feira]", connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                if (response.HasRows)
                {
                    while (response.Read())
                    {
                        int idFeira = response.GetInt32("idFeira");
                        String nome = response.GetString("nome");
                        DateTime dataI = response.GetDateTime("dataInicio");
                        DateTime? dataF = null;
                        if (!response.IsDBNull("dataFim"))
                            dataF = response.GetDateTime("dataFim");
                        float precoCand = (float)response.GetDouble("precoCandidatura");
                        String criadorEmail = response.GetString("criadorEmail");
                        int? categoria = null;
                        if (!response.IsDBNull("categoria"))
                            categoria = response.GetInt32("categoria");
                        List<Leilao> leiloesdafeira = LeilaoDAO.GetInstance().ListLeiloesFeira(idFeira);
                        List<Stand> standsdafeira = StandsDaFeira(idFeira);
                        r.Add(new Feira(idFeira, nome, dataI, dataF, precoCand, criadorEmail, categoria, standsdafeira, leiloesdafeira));
                    }
                }
                connection.Close();
            }
			return r;
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

        public void InsertStandParticipante(int idStand, int idFeira)
        {
            using SqlConnection connection = new(ConnectionDAO.connectionString);
            using SqlCommand command = new("INSERT INTO [dbo].[StandParticipa] VALUES (@idStand, @idFeira)", connection);
            {
                connection.Open();
                command.Parameters.AddWithValue("@idStand", idStand);
                command.Parameters.AddWithValue("@idFeira", idFeira);
				command.ExecuteNonQuery();
				connection.Close();
            }
        }
          
        public Boolean feiraTemStands(int idFeira)
        {
            bool r = false;
            using SqlConnection connection = new(ConnectionDAO.connectionString);
            using (SqlCommand command = new("SELECT * FROM [StandParticipa] WHERE idFeira = (@idFeira)", connection))
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

        public List<Stand> StandsDaFeira(int idFeira)
        {
            List<Stand> r = new();
            using (SqlConnection connection = new(ConnectionDAO.connectionString))
            using (SqlCommand command = new("SELECT * FROM [StandParticipa] WHERE idFeira = (@idFeira)", connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@idFeira", idFeira);
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                if (response.HasRows)
                {
                    while (response.Read())
                    {
                        int idStand = response.GetFieldValue<int>("idStand");
                        Stand? s = StandDAO.GetInstance()[idStand];
                        if (s != null) r.Add(s);
                    }
                }
                connection.Close();
            }
            return r;
        }

        public Dictionary<int,List<int>> FeirasStands()
        {
            Dictionary<int, List<int>> r = new();
            using (SqlConnection connection = new(ConnectionDAO.connectionString))
            using (SqlCommand command = new("SELECT * FROM [StandParticipa]", connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                if (response.HasRows)
                {
                    while (response.Read())
                    {
                        int idStand = response.GetFieldValue<int>("idStand");
                        int idFeira = response.GetFieldValue<int>("idFeira");
                        if (!r.ContainsKey(idFeira))
                            r.Add(idFeira, new());
                        r[idFeira].Add(idStand);
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
            using SqlCommand command = new("SELECT  ISNULL(MAX(idFeira)+1,0) AS MaiorID FROM [Feira]", connection);
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
    }
}