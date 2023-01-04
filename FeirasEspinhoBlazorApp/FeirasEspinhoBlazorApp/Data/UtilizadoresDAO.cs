using static System.Net.Mime.MediaTypeNames;
using System.Data;
using FeirasEspinhoBlazorApp.SourceCode.Utilizadores;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Runtime.CompilerServices;

namespace FeirasEspinhoBlazorApp.Data
{
	public class UtilizadoresDAO
	{
		private static UtilizadoresDAO instance = new();
		
		public static UtilizadoresDAO GetInstance()
		{
			return instance;
		}

        public bool ContainsKey(String tabela, String email)
        {
            bool r = false;
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using (SqlCommand command = new("SELECT * FROM ["+tabela+"] WHERE email = (@email)", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@email", email);
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

        public bool ContainsKey(String email)
        {
            bool r = ContainsKey("Cliente", email);
            if (!r)
                r = ContainsKey("Feirante", email);
            if (!r)
                r = ContainsKey("Admin", email);
            return r;
        }

        //Insere utilizador na tabela
        public void Insert(Utilizador utilizador)
        {
            try
            {
                if (utilizador is Cliente)
                {
                    Cliente? cliente = utilizador as Cliente;
                    using SqlConnection connection = new(ConnectionDAO.connectionString);
                    using SqlCommand command = new("INSERT INTO [dbo].[Cliente] VALUES (@email, @nome, @password, @dataNascimento, @dataCriacao)", connection);
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@email", cliente.Email);
                        command.Parameters.AddWithValue("@nome", cliente.Username);
                        command.Parameters.AddWithValue("@password", cliente.Password);
                        command.Parameters.AddWithValue("@dataNascimento", cliente.DataNascimento);
                        command.Parameters.AddWithValue("@dataCriacao", cliente.DataCriacao);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                else if (utilizador is Feirante)
                {
                    Feirante? feirante = utilizador as Feirante;
                    using SqlConnection connection = new(ConnectionDAO.connectionString);
                    using SqlCommand command = new("INSERT INTO [dbo].[Feirante] VALUES (@email, @nome, @password, @dataNascimento, @dataCriacao, @nrconta)", connection);
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@email", feirante.Email);
                        command.Parameters.AddWithValue("@nome", feirante.Username);
                        command.Parameters.AddWithValue("@password", feirante.Password);
                        command.Parameters.AddWithValue("@dataNascimento", feirante.DataNascimento);
                        command.Parameters.AddWithValue("@dataCriacao", feirante.DataCriacao);
                        command.Parameters.AddWithValue("@nrconta", feirante.IDconta);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                else if (utilizador is Administrador)
                {
                    Administrador? admin = utilizador as Administrador;
                    using SqlConnection connection = new(ConnectionDAO.connectionString);
                    using SqlCommand command = new("INSERT INTO [dbo].[Administrador] VALUES (@email, @nome, @password, @dataNascimento, @dataCriacao)", connection);
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@email", admin.Email);
                        command.Parameters.AddWithValue("@nome", admin.Username);
                        command.Parameters.AddWithValue("@password", admin.Password);
                        command.Parameters.AddWithValue("@dataNascimento", admin.DataNascimento);
                        command.Parameters.AddWithValue("@dataCriacao", admin.DataCriacao);
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

        //Obtem utilizador com base no seu email
        public Utilizador? this[string email] => GetUtilizador(email);
        public Utilizador? GetUtilizador(String email) {
            try
            {
                using (SqlConnection connection = new(ConnectionDAO.connectionString))
                using (SqlCommand command = new("SELECT * FROM [Cliente] WHERE email = (@email)", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@email", email);
                    command.ExecuteNonQuery();
                    SqlDataReader response = command.ExecuteReader();
                    while(response.Read())
                    {
                        Cliente c = new()
                        {
                            Email = response.GetFieldValue<string>("email"),
                            Username = response.GetFieldValue<string>("nome"),
                            Password = response.GetFieldValue<string>("password"),
                            DataNascimento = response.GetFieldValue<DateTime>("dataNascimento"),
                            DataCriacao = response.GetFieldValue<DateTime>("dataCriacao")
                        };
                        connection.Close();
                        return c;
                    }
                }
                using (SqlConnection connection = new(ConnectionDAO.connectionString))
                using (SqlCommand command = new("SELECT * FROM [Administrador] WHERE email = (@email)", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@email", email);
                    command.ExecuteNonQuery();
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {
                        Administrador a = new()
                        {
                            Email = response.GetFieldValue<string>("email"),
                            Username = response.GetFieldValue<string>("nome"),
                            Password = response.GetFieldValue<string>("password"),
                            DataNascimento = response.GetFieldValue<DateTime>("dataNascimento"),
                            DataCriacao = response.GetFieldValue<DateTime>("dataCriacao")
                        };
                        connection.Close();
                        return a;
                    }
                }
                using (SqlConnection connection = new(ConnectionDAO.connectionString))
                using (SqlCommand command = new("SELECT * FROM [Feirante] WHERE email = (@email)", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@email", email);
                    command.ExecuteNonQuery();
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {
                        Feirante f = new()
                        {
                            Email = response.GetFieldValue<string>("email"),
                            Username = response.GetFieldValue<string>("nome"),
                            Password = response.GetFieldValue<string>("password"),
                            DataNascimento = response.GetFieldValue<DateTime>("dataNascimento"),
                            DataCriacao = response.GetFieldValue<DateTime>("dataCriacao"),
                            iDconta = response.GetFieldValue<int>("nrconta")
                        };
                        connection.Close();
                        return f;
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

        //METODOS DE TESTE
		public List<Cliente>? ListAllClientes()
		{
            try
            {
                List<Cliente> r = new();
                using (SqlConnection connection = new(ConnectionDAO.connectionString))
                using (SqlCommand command = new("SELECT * FROM [Cliente]", connection))
                {
                    connection.Open();
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {
                        Cliente cliente = new()
                        {
                            Email = response.GetFieldValue<string>("email"),
                            Username = response.GetFieldValue<string>("nome"),
                            Password = response.GetFieldValue<string>("password"),
                            DataNascimento = response.GetFieldValue<DateTime>("dataNascimento"),
                            DataCriacao = response.GetFieldValue<DateTime>("dataCriacao")
                        };
                        r.Add(cliente);
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

        public List<Administrador>? ListAllAdmins()
        {
            try
            {
                List<Administrador> r = new();
                using (SqlConnection connection = new(ConnectionDAO.connectionString))
                using (SqlCommand command = new("SELECT * FROM [Administrador]", connection))
                {
                    connection.Open();
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {
                        Administrador admin = new()
                        {
                            Email = response.GetFieldValue<string>("email"),
                            Username = response.GetFieldValue<string>("nome"),
                            Password = response.GetFieldValue<string>("password"),
                            DataNascimento = response.GetFieldValue<DateTime>("dataNascimento"),
                            DataCriacao = response.GetFieldValue<DateTime>("dataCriacao")
                        };
                        r.Add(admin);
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

        public List<Feirante>? ListAllFeirantes()
        {
            try
            {
                List<Feirante> r = new();
                using (SqlConnection connection = new(ConnectionDAO.connectionString))
                using (SqlCommand command = new("SELECT * FROM [Feirante]", connection))
                {
                    connection.Open();
                    SqlDataReader response = command.ExecuteReader();
                    while (response.Read())
                    {
                        Feirante feirante = new()
                        {
                            Email = response.GetFieldValue<string>("email"),
                            Username = response.GetFieldValue<string>("nome"),
                            Password = response.GetFieldValue<string>("password"),
                            DataNascimento = response.GetFieldValue<DateTime>("dataNascimento"),
                            DataCriacao = response.GetFieldValue<DateTime>("dataCriacao"),
                            iDconta = response.GetFieldValue<int>("nrconta")
                        };
                        r.Add(feirante);
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


        //Testado
        public void GenerateDataClientes()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionDAO.connectionString))
            {
                string sql = @"
                INSERT INTO [dbo].[Cliente] (email, nome, password, dataNascimento, dataCriacao)
                VALUES
                    ('joao@gmail.com', 'João Silva', 'joao123', '1980-01-01', '2022-12-15'),
                    ('maria@gmail.com', 'Maria Santos', 'maria123', '1985-03-20', '2022-12-15'),
                    ('pedro@gmail.com', 'Pedro Rodrigues', 'pedro123', '1990-06-05', '2022-12-15'),
                    ('carlos@gmail.com', 'Carlos Martins', 'carlos123', '1995-09-15', '2022-12-15'),
                    ('ana@gmail.com', 'Ana Costa', 'ana123', '2000-11-30', '2022-12-15'),
                    ('rui@gmail.com', 'Rui Pereira', 'rui123', '2005-02-12', '2022-12-15'),
                    ('joana@gmail.com', 'Joana Ferreira', 'joana123', '2002-04-25', '2022-12-15'),
                    ('paulo@gmail.com', 'Paulo Moreira', 'paulo123', '2001-07-10', '2022-12-15'),
                    ('sara@gmail.com', 'Sara Oliveira', 'sara123', '2000-09-20', '2022-12-15'),
                    ('miguel@gmail.com', 'Miguel Gomes', 'miguel123', '1975-12-31', '2022-12-15'),
                    ('david@gmail.com', 'David Sousa', 'david123', '1980-03-01', '2022-12-15'),
                    ('carla@gmail.com', 'Carla Almeida', 'carla123', '1985-05-15', '2022-12-15'),
                    ('ricardo@gmail.com', 'Ricardo Fonseca', 'ricardo123', '1990-08-01', '2022-12-15'),
                    ('isabel@gmail.com', 'Isabel Costa', 'isabel123', '1995-10-20', '2022-12-15'),
                    ('francisco@gmail.com', 'Francisco Pinto', 'francisco123', '2000-12-31', '2022-12-15'),
                    ('catarina@gmail.com', 'Catarina Teixeira', 'catarina123', '2000-02-14', '2022-12-15');
                ";
                SqlCommand command = new(sql, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void GenerateDataAdmins()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionDAO.connectionString))
            {
                string sql = @"
                INSERT INTO [dbo].[Administrador] (email, nome, password, dataNascimento, dataCriacao)
                VALUES
                    ('rui@hotmail.com', 'Rui Jorge', 'rui12345678', '2002-12-12', '2022-12-15'),
                    ('diogo@hotmail.com', 'Diogo Santos', 'diogo12345678', '1988-03-20', '2022-12-15'),
                    ('marco@hotmail.com', 'Marco Rodrigues', 'marco12345678', '1978-06-05', '2022-12-15'),
                    ('joaquim@hotmail.com', 'Joaquim Martins', 'joaquim12345678', '1995-09-15', '2022-12-15'),
                    ('marta@hotmail.com', 'Marta Costa', 'marta12345678', '2000-11-30', '2022-12-15'),
                    ('filipa@hotmail.com', 'Filipa Pereira', 'filipa12345678', '2003-02-12', '2022-12-15'),
                    ('evandro@hotmail.com', 'Evandro Ferreira', 'evandro12345678', '2002-04-25', '2022-12-15'),
                    ('cristiano@hotmail.com', 'Cristiano Moreira', 'cristiano12345678', '2001-07-10', '2022-12-15'),
                    ('manuel@hotmail.com', 'Manuel Oliveira', 'manuel12345678', '1981-09-20', '2022-12-15'),
                    ('vitor@hotmail.com', 'Vitor Gomes', 'vitor12345678', '1977-12-31', '2022-12-15'),
                    ('margarida@hotmail.com', 'Margarida Sousa', 'margarida12345678', '1980-01-03', '2022-12-15'),
                    ('angelico@hotmail.com', 'Angélico Almeida', 'angelico12345678', '1985-07-15', '2022-12-15'),
                    ('eduarda@hotmail.com', 'Eduarda Fonseca', 'eduarda12345678', '1990-08-21', '2022-12-15'),
                    ('isaura@hotmail.com', 'Isaura Costa', 'isaura12345678', '1990-10-20', '2022-12-15'),
                    ('francisca@hotmail.com', 'Francisca Pinto', 'francisca12345678', '2001-12-31', '2022-12-15'),
                    ('carlos@hotmail.com', 'Carlos Teixeira', 'carlos12345678', '1994-05-14', '2022-12-15');
                ";
                SqlCommand command = new(sql, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void GenerateDataFeirantes()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionDAO.connectionString))
            {
                string sql = @"
                INSERT INTO [dbo].[Feirante] (email, nome, password, dataNascimento, dataCriacao, nrconta)
                VALUES
                    ('adriana@espinho.com', 'Adriana Paiva', 'adriana12345678', '2002-12-12', '2022-12-15',1),
                    ('amelia@espinho.com', 'Emilia Cardoso', 'amelia12345678', '1988-03-20', '2022-12-15',2),
                    ('elvira@espinho.com', 'Elvira Santos', 'elvira12345678', '1978-06-05', '2022-12-15',3),
                    ('esmeralda@espinho.com', 'Esmeralda Braga', 'esmeralda12345678', '1995-09-15', '2022-12-15',4),
                    ('fernanda@espinho.com', 'Fernanda Eusébio', 'fernanda12345678', '2000-11-30', '2022-12-15',5),
                    ('gertrudes@espinho.com', 'Gertrudes Almeida', 'gertrudes12345678', '2003-02-12', '2022-12-15',6),
                    ('elisio@espinho.com', 'Elísio Mendes', 'elisio12345678', '2002-04-25', '2022-12-15',7),
                    ('alexandre@espinho.com', 'Alexandre Santos', 'alexandre12345678', '2001-07-10', '2022-12-15',8),
                    ('rogerio@espinho.com', 'Rogério Alves', 'rogerio12345678', '1981-09-20', '2022-12-15',9),
                    ('alcibiades@espinho.com', 'Alcibiades Silva', 'alcibiades12345678', '1977-12-31', '2022-12-15',10),
                    ('francelina@espinho.com', 'Francelina Rosa', 'francelina12345678', '1980-01-03', '2022-12-15',11),
                    ('camilo@espinho.com', 'Camilo Ferreira', 'camilo12345678', '1985-07-15', '2022-12-15',12),
                    ('isidro@espinho.com', 'Isídro Lopes', 'isidro12345678', '1990-08-21', '2022-12-15',13),
                    ('olga@espinho.com', 'Olga Filipe', 'olga12345678', '1990-10-20', '2022-12-15',14),
                    ('orlando@espinho.com', 'Orlando Pinto', 'orlando12345678', '2001-12-31', '2022-12-15',15),
                    ('arsenio@espinho.com', 'Arsénio Quaresma', 'arsenio12345678', '2000-05-14', '2022-12-15',16);
                ";
                SqlCommand command = new(sql, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
