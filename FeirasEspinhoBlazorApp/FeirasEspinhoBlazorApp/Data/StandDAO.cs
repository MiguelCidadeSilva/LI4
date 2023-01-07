using static System.Net.Mime.MediaTypeNames;
using System.Data;
using Microsoft.AspNetCore.Http;
using FeirasEspinhoBlazorApp.SourceCode.Stands;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Collections.Generic;

namespace FeirasEspinhoBlazorApp.Data
{
    public class StandDAO
    {
        private static StandDAO instance = new StandDAO();

        public static StandDAO GetInstance()
        {
            return instance;
        }

        public bool ContainsKeyStand(int idStand)
        {
            bool r = false;
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using (SqlCommand command = new("SELECT * FROM [Stand] WHERE idStand = (@idStand)", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@idStand", idStand);
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

        public bool ContainsKeyProduto(int idProd)
        {
            bool r = false;
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using (SqlCommand command = new("SELECT * FROM [Produto] WHERE idProd = (@idprod)", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@idProd", idProd);
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

        public void InsertStand(Stand stand)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("INSERT INTO [dbo].[Stand] VALUES (@idStand, @negociavel, @consultantes, @dataCriacao, @donoEmail, @categoria)", connection);
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@idStand", stand.IdStand);
                    command.Parameters.AddWithValue("@negociavel", stand.Negociavel);
                    command.Parameters.AddWithValue("@consultantes", stand.Consultantes);
                    command.Parameters.AddWithValue("@dataCriacao", stand.DataCriacao);
                    command.Parameters.AddWithValue("@donoEmail", stand.EmailDono);
                    command.Parameters.AddWithValue("@categoria", stand.Categoria);
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

        public void InsertProduto(Produto produto)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("INSERT INTO [dbo].[Produto] VALUES (@idProd, @nome, @idSubCategoria, @banca, @stock, @preco, @disponivel)", connection);
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@idProd", produto.IdProduto);
                    command.Parameters.AddWithValue("@nome", produto.Nome);
                    command.Parameters.AddWithValue("@idSubCategoria", produto.IdSubCategoria);
                    command.Parameters.AddWithValue("@banca", produto.Stand);
                    command.Parameters.AddWithValue("@stock", produto.Stock);
                    command.Parameters.AddWithValue("@preco", produto.Preco);
                    command.Parameters.AddWithValue("@disponivel", produto.Disponivel);
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

        public List<Produto> GetProdutosStand(int idStand)
		{
			List<Produto> res = new();
			try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("SELECT * FROM [Produto] WHERE banca = (@banca)", connection);
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@banca", idStand);
                    command.ExecuteNonQuery();
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {
                        int idProd = response.GetFieldValue<int>("idProd");
                        string nome = response.GetFieldValue<string>("nome");
                        int idSubCategoria = response.GetFieldValue<int>("idSubCategoria");
                        int stock = response.GetFieldValue<int>("stock");
                        float preco = (float)response.GetFieldValue<double>("preco");
                        bool disponivel = response.GetFieldValue<bool>("disponivel");
                        res.Add(new Produto(idProd, nome, idSubCategoria, idStand, stock, preco, disponivel));
                    }
                    connection.Close();
                }
                return res;
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
            return res;
        }

        public Produto? GetProduto(int id)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("SELECT * FROM [Produto] WHERE idProd = (@idProd)", connection);
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@idProd", id);
                    command.ExecuteNonQuery();
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {
                        string nome = response.GetFieldValue<string>("nome");
                        int idSubCategoria = response.GetFieldValue<int>("idSubCategoria");
                        int stand = response.GetFieldValue<int>("banca");
                        int stock = response.GetFieldValue<int>("stock");
                        float preco = (float)response.GetFieldValue<double>("preco");
                        bool disponivel = response.GetFieldValue<bool>("disponivel");
                        connection.Close();
                        return new Produto(id, nome, idSubCategoria, stand, stock, preco, disponivel);
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

        public Stand? this[int id] => GetStand(id);
        public Stand? GetStand(int id)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("SELECT * FROM [Stand] WHERE idStand = (@idStand)", connection);
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@idStand", id);
                    command.ExecuteNonQuery();
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {
                        bool negociavel = response.GetFieldValue<bool>("negociavel");
                        int consultantes = response.GetFieldValue<int>("consultantes");
                        DateTime dataCriacao = response.GetFieldValue<DateTime>("dataCriacao");
                        string donoEmail = response.GetFieldValue<string>("donoEmail");
                        int categoria = response.GetFieldValue<int>("categoria");
                        List<Produto> produtos = GetProdutosStand(id);
                        connection.Close();
                        return new Stand(id, negociavel, consultantes, dataCriacao, donoEmail, categoria, produtos);
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

        public List<Stand> ListAllStands()
        {
            List<Stand> r = new();
            try
            {
                using (SqlConnection connection = new(ConnectionDAO.connectionString))
                using (SqlCommand command = new("SELECT * FROM [Stand]", connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {
                        Stand s = new()
                        {
                            IdStand = response.GetFieldValue<int>("idStand"),
                            Negociavel = response.GetFieldValue<bool>("negociavel"),
                            Consultantes = response.GetFieldValue<int>("consultantes"),
                            DataCriacao = response.GetFieldValue<DateTime>("dataCriacao"),
                            EmailDono = response.GetFieldValue<string>("donoEmail"),
                            Categoria = response.GetFieldValue<int>("categoria")
                        };
                        r.Add(s);
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

        public List<Produto> ListAllProdutos()
        {
            List<Produto> r = new();
            try
            {
                using (SqlConnection connection = new(ConnectionDAO.connectionString))
                using (SqlCommand command = new("SELECT * FROM [Produto]", connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {
                        Produto p = new()
                        {
                            IdProduto = response.GetFieldValue<int>("idProd"),
                            Nome = response.GetFieldValue<String>("nome"),
                            IdSubCategoria = response.GetFieldValue<int>("idSubCategoria"),
                            Stand = response.GetFieldValue<int>("banca"),
                            Stock = response.GetFieldValue<int>("stock"),
                            Preco = (float)response.GetFieldValue<double>("preco"),
                            Disponivel = response.GetFieldValue<bool>("disponivel")
                        };
                        r.Add(p);
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
		private void GenerateDataStand()
		{
			using (SqlConnection connection = new SqlConnection(ConnectionDAO.connectionString))
			{
				string sql = @"INSERT INTO Stand (idStand, negociavel, consultantes, dataCriacao, donoEmail, categoria)
                            VALUES
                                (1, 1, 5, '2022-12-15', 'adriana@espinho.com', 1),
                                (2, 0, 10, '2022-12-15', 'amelia@espinho.com', 2),
                                (3, 1, 15, '2022-12-15', 'elvira@espinho.com', 3),
                                (4, 0, 20, '2022-12-15', 'esmeralda@espinho.com', 4),
                                (5, 1, 25, '2022-12-15', 'rogerio@espinho.com', 5),
                                (6, 0, 30, '2022-12-15', 'olga@espinho.com', 6),
                                (7, 1, 35, '2022-12-15', 'elisio@espinho.com', 7),
                                (8, 0, 40, '2022-12-15', 'orlando@espinho.com', 8),
                                (9, 1, 45, '2022-12-15', 'arsenio@espinho.com', 9),
                                (10, 0, 50, '2022-12-15', 'adriana@espinho.com', 10),
                                (11, 0, 55, '2022-12-15', 'amelia@espinho.com', 1),
                                (12, 1, 60, '2022-12-15', 'elvira@espinho.com', 2),
                                (13, 0, 65, '2022-12-15', 'esmeralda@espinho.com', 3),
                                (14, 1, 70, '2022-12-15', 'rogerio@espinho.com', 4),
                                (15, 0, 75, '2022-12-15', 'olga@espinho.com', 5),
                                (16, 1, 80, '2022-12-15', 'elisio@espinho.com', 6),
                                (17, 0, 85, '2022-12-15', 'orlando@espinho.com', 7),
                                (18, 1, 90, '2022-12-15', 'arsenio@espinho.com', 8),
                                (19, 0, 95, '2022-12-15', 'adriana@espinho.com', 9),
                                (20, 1, 100, '2022-12-15', 'amelia@espinho.com', 10)";

				SqlCommand command = new(sql, connection);
				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
			}

		}
        private void GenerateDataProdutos()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionDAO.connectionString))
            {
                string sql = @"INSERT INTO Produto (idProd, nome, idSubCategoria, banca, stock, preco, disponivel)
                                VALUES
                                    (1, 'Laranja', 1, 1, 100, 10, 1),
                                    (2, 'Banana', 2, 1, 101, 11, 1),
                                    (3, 'Maçã', 3, 1, 102, 12, 1),
                                    (4, 'Pera', 4, 1, 103, 13, 1),
                                    (5, 'Uva', 5, 1, 104, 14, 1),
                                    (6, 'Morango', 6, 1, 105, 15, 1),
                                    (7, 'Abacaxi', 7, 1, 106, 16, 1),
                                    (8, 'Manga', 8, 1, 107, 17, 1),
                                    (9, 'Pêssego', 9, 1, 108, 18, 1),
                                    (10, 'Amora', 10, 1, 109, 19, 1),
                                    (11, 'Kiwi', 1, 1, 110, 20, 1),
                                    (12, 'Mamão', 2, 1, 111, 21, 1),
                                    (13, 'Tangerina', 3, 1, 112, 22, 1),
                                    (14, 'Acerola', 4, 1, 113, 23, 1),
                                    (15, 'Graviola', 5, 1, 114, 24, 1),
                                    (16, 'Pitanga', 6, 1, 115, 25, 1),
                                    (17, 'Jabuticaba', 7, 1, 116, 26, 1),
                                    (18, 'Lichia', 8, 1, 117, 27, 1),
                                    (19, 'Goiaba', 9, 1, 118, 28, 1),
                                    (20, 'Caju', 10, 1, 119, 29, 1),
                                    (21, 'Vestido', 11, 12, 120, 30, 1),
                                    (22, 'Camisa', 12, 12, 121, 31, 1),
                                    (23, 'Calça', 13, 12, 122, 32, 1),
                                    (24, 'Sapato', 14, 12, 123, 33, 1),
                                    (25, 'Bolsa', 15, 12, 124, 34, 1),
                                    (26, 'Cinto', 16, 12, 125, 35, 1),
                                    (27, 'Carteira', 17, 12, 126, 36, 1),
                                    (28, 'Pulseira', 18, 12, 127, 37, 1),
                                    (29, 'Anel', 19, 12, 128, 38, 1),
                                    (30, 'Colar', 20, 12, 129, 39, 1),
                                    (31, 'Oculos', 11, 12, 130, 40, 1),
                                    (32, 'Relógio', 12, 12, 131, 41, 1),
                                    (33, 'Boné', 13, 12, 132, 42, 1),
                                    (34, 'Meia', 14, 12, 133, 43, 1),
                                    (35, 'Cueca', 15, 12, 134, 44, 1),
                                    (36, 'Sutiã', 16, 12, 135, 45, 1),
                                    (37, 'Lingerie', 12, 17, 136, 46, 1),
                                    (38, 'Lenço', 18, 12, 137, 47, 1),
                                    (39, 'Chapéu', 19, 12, 138, 48, 1),
                                    (40, 'Biquíni', 20, 12, 139, 49, 1),
                                    (41, 'Mochila', 21, 12, 140, 50, 1)";
                SqlCommand command = new(sql, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

		public void GenerateData()
        {
            GenerateDataStand();
            GenerateDataProdutos();

		}

        public int GetStockProduto(int idProd) {
            int r = 0;
            try
            {
                using (SqlConnection connection = new(ConnectionDAO.connectionString))
                using (SqlCommand command = new("SELECT * FROM [Produto] WHERE idProd = (@idProd)", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@idProd", idProd);
                    command.ExecuteNonQuery();
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {
                        r = response.GetFieldValue<int>("stock");
                        return r;
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


        public bool GetDisponibilidadeProduto(int idProd)
        {
            bool r = false;
            try
            {
                using (SqlConnection connection = new(ConnectionDAO.connectionString))
                using (SqlCommand command = new("SELECT * FROM [Produto] WHERE idProd = (@idProd)", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@idProd", idProd);
                    command.ExecuteNonQuery();
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {
                        r = response.GetFieldValue<bool>("disponivel");
                        return r;
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

        public void TrocaDisponibilidadeProduto(int idProd)
        {
            try
            {
                using (SqlConnection connection = new(ConnectionDAO.connectionString))
                using (SqlCommand command = new("UPDATE [Produto] SET disponivel = (@disponivel), WHERE idProd = (@idProd)", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@disponivel",!GetDisponibilidadeProduto(idProd));
                    command.Parameters.AddWithValue("@idProd", idProd);
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

        public void AumentaStockProduto(int idProd, int incremento){
            try
            {
                using (SqlConnection connection = new(ConnectionDAO.connectionString))
                using (SqlCommand command = new("UPDATE [Produto] SET stock = (@stock), WHERE idProd = (@idProd)", connection))
                {
                    connection.Open();
                    int stockOriginal = GetStockProduto(idProd);
                    if (stockOriginal == 0)
                    {
                        TrocaDisponibilidadeProduto(idProd);
                    }
                    command.Parameters.AddWithValue("@stock",stockOriginal+incremento);
                    command.Parameters.AddWithValue("@idProd", idProd);
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

        public void DiminuiStockProduto(int idProd, int decremento)
        {
            try
            {
                using (SqlConnection connection = new(ConnectionDAO.connectionString))
                using (SqlCommand command = new("UPDATE [Produto] SET stock = (@stock), WHERE idProd = (@idProd)", connection))
                {
                    connection.Open();
                    int stockOriginal = GetStockProduto(idProd);
                    int novoValor;
                    if (stockOriginal-decremento <= 0)
                    {
                        TrocaDisponibilidadeProduto(idProd);
                        novoValor = 0;
                    }
                    else novoValor = stockOriginal-decremento;
                    command.Parameters.AddWithValue("@stock", novoValor);
                    command.Parameters.AddWithValue("@idProd", idProd);
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

    }
}
