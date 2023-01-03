using static System.Net.Mime.MediaTypeNames;
using System.Data;
using Microsoft.AspNetCore.Http;
using FeirasEspinhoBlazorApp.SourceCode.Vendas;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace FeirasEspinhoBlazorApp.Data
{
    public class VendaDAO
    {
        private static VendaDAO instance = new VendaDAO();

        public static VendaDAO GetInstance()
        {
            return instance;
        }

        public void Insert(Venda venda)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("INSERT INTO [dbo].[Venda] VALUES (@idVenda, @data, @preco, @emailCl, @idFeira, @negociacao, @idStand)", connection);
                connection.Open();
                command.Parameters.AddWithValue("@idVenda", venda.IdVenda);
                command.Parameters.AddWithValue("@data", venda.Data);
                command.Parameters.AddWithValue("@preco", venda.Preco);
                command.Parameters.AddWithValue("@emailCl", venda.EmailCliente);
                command.Parameters.AddWithValue("@idFeira", venda.IdFeira);
                command.Parameters.AddWithValue("@negociacao", venda.Negociacao);
                command.Parameters.AddWithValue("@idStand", venda.IdStand);
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

        public Venda? GetVenda(int id)
        {
            try
            {
                using SqlConnection connection = new(ConnectionDAO.connectionString);
                using SqlCommand command = new("SELECT * FROM [Venda] WHERE idFeira = (@idFeira)", connection);
                connection.Open();
                command.Parameters.AddWithValue("@idFeira", id);
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                while (response.Read())
                {
                    DateTime data = response.GetFieldValue<DateTime>("data");
                    float preco = response.GetFieldValue<float>("preco");
                    string emailCl = response.GetFieldValue<string>("emailCl");
                    int idFeira = response.GetFieldValue<int>("idFeira");
                    int negociacao = response.GetFieldValue<int>("negociacao");
                    int idStand = response.GetFieldValue<int>("idStand");
                    connection.Close();
                    return new Venda(id,data,preco,emailCl,idFeira,negociacao,idStand);
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