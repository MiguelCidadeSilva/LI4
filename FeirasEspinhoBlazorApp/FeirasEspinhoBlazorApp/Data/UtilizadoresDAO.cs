using static System.Net.Mime.MediaTypeNames;
using System.Data;
using FeirasEspinhoBlazorApp.SourceCode.Utilizadores;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace FeirasEspinhoBlazorApp.Data
{
	public class UtilizadoresDAO
	{
		private static UtilizadoresDAO instance = new UtilizadoresDAO();
		
		public static UtilizadoresDAO GetInstance()
		{
			return instance;
		}
		public void Create(Utilizador utilizador)
		{
			using (SqlConnection connection = new SqlConnection(ConnectionDAO.connectionString))
			using (SqlCommand command = new SqlCommand("INSERT INTO [dbo].[Cliente] VALUES (@email, @nome, @password, @dataNascimento, @dataCriacao)", connection))
			{
				connection.Open();
				command.Parameters.AddWithValue("@email", utilizador.email);
				command.Parameters.AddWithValue("@nome", utilizador.username);
				command.Parameters.AddWithValue("@password", utilizador.password);
				command.Parameters.AddWithValue("@dataNascimento", utilizador.dataCriacao);
				command.Parameters.AddWithValue("@dataCriacao", utilizador.dataNascimento);
				command.ExecuteNonQuery();
				connection.Close();
			}
		}
		/*
		public Task<Utilizador> GetById(string email)
		{
			Task<Utilizador> client = Task.FromResult(connectionDAO.Get<Utilizador>($"SELECT * FROM [Cliente] where email = {email}", null));
			return client;
		}
		public Task<int> Count()
		{
			Task<int> countClients = Task.FromResult(connectionDAO.Get<int>($"SELECT COUNT(*) FROM [Cliente]", null));
			return countClients;
		}
		*/
		public List<Utilizador> ListAll()
		{
			List<Utilizador> r = new List<Utilizador>();
			using (SqlConnection connection = new SqlConnection(ConnectionDAO.connectionString))
			using (SqlCommand command = new SqlCommand("SELECT * FROM [Cliente]", connection))
			{
				connection.Open();
				SqlDataReader response = command.ExecuteReader();
				while (response.Read())
				{
					Utilizador utilizador = new Utilizador();
					utilizador.email = response.GetFieldValue<string>("email");
					utilizador.username = response.GetFieldValue<string>("nome");
					utilizador.password = response.GetFieldValue<string>("password");
					utilizador.dataNascimento = response.GetFieldValue<DateTime>("dataNascimento");
					utilizador.dataCriacao = response.GetFieldValue<DateTime>("dataCriacao");
					r.Add(utilizador);
				}
				connection.Close();
			}
			return r;
		}
	}
}
