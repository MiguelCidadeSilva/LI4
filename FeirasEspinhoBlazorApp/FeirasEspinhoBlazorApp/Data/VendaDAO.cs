using static System.Net.Mime.MediaTypeNames;
using System.Data;
using Microsoft.AspNetCore.Http;
using FeirasEspinhoBlazorApp.SourceCode.Vendas;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using FeirasEspinhoBlazorApp.SourceCode.Stands;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace FeirasEspinhoBlazorApp.Data
{
    public class VendaDAO
    {
        private static VendaDAO instance = new VendaDAO();

        public static VendaDAO GetInstance()
        {
            return instance;
        }

        public bool ContainsKeyVenda(int idVenda)
        {
            bool r = false;
            using SqlConnection connection = new(ConnectionDAO.connectionString);
            using (SqlCommand command = new("SELECT * FROM [Venda] WHERE idVenda = (@idVenda)", connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@idVenda", idVenda);
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                r = response.HasRows;
                connection.Close();
            }
            return r;
        }

        public void InsertProdutosVendidos(int idVenda, List<(Produto, int)> produtos, float taxaCamara)
        {
            using SqlConnection connection = new(ConnectionDAO.connectionString);
            {
                connection.Open();
                foreach ((Produto, int) prod in produtos)
                {
                    using SqlCommand command = new("INSERT INTO [dbo].[ProdutosVendidos] VALUES (@idVenda, @idProd, @precoProd, @quantidade, @taxaCamara)", connection);
                    {
                        command.Parameters.AddWithValue("idVenda", idVenda);
                        command.Parameters.AddWithValue("idProd", prod.Item1.IdProduto);
                        command.Parameters.AddWithValue("precoProd", prod.Item1.Preco);
                        command.Parameters.AddWithValue("quantidade", prod.Item2);
                        command.Parameters.AddWithValue("taxaCamara", taxaCamara);
                        command.ExecuteNonQuery();
                    }
                }
                connection.Close();
            }
        }

        public void Insert(Venda venda)
        {
            using SqlConnection connection = new(ConnectionDAO.connectionString);
            using SqlCommand command = new("INSERT INTO [dbo].[Venda] VALUES (@idVenda, @data, @preco, @emailCl, @idFeira, @negociacao, @idStand)", connection);
            {
                connection.Open();
                command.Parameters.AddWithValue("@idVenda", venda.IdVenda);
                command.Parameters.AddWithValue("@data", venda.Data);
                command.Parameters.AddWithValue("@preco", venda.Preco);
                command.Parameters.AddWithValue("@emailCl", venda.EmailCliente);
                command.Parameters.AddWithValue("@idFeira", venda.IdFeira);
                if (venda.Negociacao.HasValue)
                    command.Parameters.AddWithValue("@negociacao", venda.Negociacao);
                else
                    command.Parameters.AddWithValue("@negociacao", DBNull.Value);
                command.Parameters.AddWithValue("@idStand", venda.IdStand);
                command.ExecuteNonQuery();
                connection.Close();
                if (venda.Produtos.Count > 0)
                {
                    InsertProdutosVendidos(venda.IdVenda, venda.Produtos, 0);
                }
            }
        }

        public List<(Produto, int)> GetProdutosVendidosNumaVenda(int idVenda)
        {
            List<(Produto, int)> r = new();
            using SqlConnection connection = new(ConnectionDAO.connectionString);
            using SqlCommand command = new("SELECT * FROM [ProdutosVendidos] WHERE idVenda = (@idVenda)", connection);
            {
                connection.Open();
                command.Parameters.AddWithValue("@idVenda", idVenda);
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                if (response.HasRows)
                {
                    while (response.Read())
                    {
                        int idProd = response.GetFieldValue<int>("idProd");
                        Produto? p = StandDAO.GetInstance().GetProduto(idProd);
                        int quantidade = response.GetFieldValue<int>("quantidade");
                        if (p != null)
                            r.Add((p, quantidade));
                    }
                }
                connection.Close();
                return r;
            }

        }


        public Venda? this[int id] => GetVenda(id);
        public Venda? GetVenda(int id)
        {
            using SqlConnection connection = new(ConnectionDAO.connectionString);
            using SqlCommand command = new("SELECT * FROM [Venda] WHERE idVenda = (@idVenda)", connection);
            {
                connection.Open();
                command.Parameters.AddWithValue("@idVenda", id);
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                if (response.HasRows)
                {
                    response.Read();
                    DateTime data = response.GetFieldValue<DateTime>("data");
                    float preco = (float)response.GetFieldValue<double>("preco");
                    string emailCl = response.GetFieldValue<string>("emailCl");
                    int idFeira = response.GetFieldValue<int>("idFeira");
                    int? negociacao = null;
                    if (!response.IsDBNull("negociacao"))
                        negociacao = response.GetFieldValue<int>("negociacao");
                    int idStand = response.GetFieldValue<int>("idStand");
                    connection.Close();
                    List<(Produto, int)> produtos = GetProdutosVendidosNumaVenda(id);
                    return new Venda(id, data, preco, emailCl, idFeira, negociacao, idStand, produtos);
                }
            }
            return null;
        }

        public List<Venda> ListAllVendas()
        {
            List<Venda> r = new();
            using (SqlConnection connection = new(ConnectionDAO.connectionString))
            using (SqlCommand command = new("SELECT * FROM [Venda]", connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                if (response.HasRows)
                {
                    while (response.Read())
                    {
                        int idVenda = response.GetFieldValue<int>("idVenda");
                        DateTime data = response.GetFieldValue<DateTime>("data");
                        float preco = (float)response.GetFieldValue<double>("preco");
                        String emailCliente = response.GetFieldValue<string>("emailCl");
                        int idFeira = response.GetFieldValue<int>("idFeira");
                        int? negociacao = null;
                        if (!response.IsDBNull("negociacao"))
                            negociacao = response.GetFieldValue<int>("negociacao");
                        int idStand = response.GetFieldValue<int>("idStand");
                        List<(Produto, int)> produtos = GetProdutosVendidosNumaVenda(idVenda);
                        r.Add(new Venda(idVenda, data, preco, emailCliente, idFeira, negociacao, idStand, produtos));
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
            using SqlCommand command = new("SELECT  ISNULL(MAX(idVenda)+1,0) AS MaiorID FROM [Venda]", connection);
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

        public Venda? GetNegocicaoVenda(int idNeg)
        {
            using (SqlConnection connection = new(ConnectionDAO.connectionString))
            using (SqlCommand command = new("SELECT * FROM [Venda] WHERE negociacao = (@negociacao)", connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@negociacao", idNeg);
                command.ExecuteNonQuery();
                SqlDataReader response = command.ExecuteReader();
                if (response.HasRows)
                {
                    response.Read();
                    int idVenda = response.GetFieldValue<int>("idVenda");
                    return GetVenda(idVenda); 
                }
                connection.Close();
                return null;
            }
        }
        public void DeleteProdutosVendidos(int idVenda)
        {
            using (SqlConnection connection = new(ConnectionDAO.connectionString))
            using (SqlCommand command = new("DELETE FROM [ProdutosVendidos] WHERE idVenda=(@idVenda)", connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@idVenda", idVenda);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void DeleteVenda(int idVenda)
        {
            DeleteProdutosVendidos(idVenda);
			using (SqlConnection connection = new(ConnectionDAO.connectionString))
			using (SqlCommand command = new("DELETE FROM [Venda] WHERE idVenda=(@idVenda)", connection))
			{
				connection.Open();
				command.Parameters.AddWithValue("@idVenda", idVenda);
                   command.ExecuteNonQuery();
                   connection.Close() ;
            }
        }
    }
}