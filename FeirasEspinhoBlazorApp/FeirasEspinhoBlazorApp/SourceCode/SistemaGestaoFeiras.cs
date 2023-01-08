using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Globalization;
using FeirasEspinhoBlazorApp.Data;
using FeirasEspinhoBlazorApp.SourceCode.Stands;
using FeirasEspinhoBlazorApp.SourceCode.Feiras;
using FeirasEspinhoBlazorApp.SourceCode.Vendas;
using FeirasEspinhoBlazorApp.SourceCode.Utilizadores;
using System.Linq;

namespace FeirasEspinhoBlazorApp.SourceCode
{
	public class SistemaFeiras : Exception
	{
		private UtilizadoresDAO users;
		private FeiraDAO feiras;
		private StandDAO stands;
		private VendaDAO vendas;
		private LeilaoDAO leiloes;
		private CategoriaDAO categorias;
		private int feirasCounter;
        private int vendasCounter;
        private int standsCounter;
		private int negociacoesCounter;
        private static SistemaFeiras instance = new SistemaFeiras();

		public static SistemaFeiras GetInstance()
		{
			return instance;
		}


		private SistemaFeiras()
		{
			users = UtilizadoresDAO.GetInstance();
			feiras = FeiraDAO.GetInstance();
			stands = StandDAO.GetInstance();
			vendas = VendaDAO.GetInstance();
			categorias = CategoriaDAO.GetInstance();
			leiloes = LeilaoDAO.GetInstance();
			feirasCounter = 0;
			vendasCounter = 0;
			standsCounter = 0;
			negociacoesCounter = 0;
		}


		//
		//	LOGIN: Verifica se a password é válida,e verifica as credenciais com a função verificaCredenciais da classe Utilizador
		//
		public Utilizador Login(String email, String password)
		{
			if (password.Length < 8)
				throw new PasswordInvalidaException("Password tem de ter 8 ou mais caracteres\n");
			Utilizador? user = users[email];
			if(user == null || !user.CheckCredenciais(email, password))
				throw new EmailInvalidoException("Email não está registado.");
			return user;
		}

		//		Registo: Da maneira que fiz, não existem emails repetidos no sistema, mesmo que sejam tipos de utilizador diferentes.
		//				 Se o email ainda nao estiver registado no sistema, adiciona ao dicionario do tipo de utilizador que queres ser ao registar-te	
		//
		public void Registo(Utilizador u)
		{
			if (u.Password.Length < 8)
				throw new PasswordInvalidaException("Password tem de conter pelo menos 8 caracteres.");
			if (users.ContainsKey(u.Email))
				throw new EmailInvalidoException("Email já está associado a uma conta...");
			users.Insert(u);

		}

		//Quando a data final é null, é uma feira permanente
		//Não precisamos de verificar se já acabou quando implementarmos essa funcionalidade no sistema
		public void CriarFeira(Utilizador u, String nome_Feira, DateTime? data_Final, float preco_candidatura, int categoria_feira)
		{
			if (u is not Administrador)
				throw new PermissaoInvalidaException("Funcionalidade restrita a Administradores.");

			//Caso o admin em questão já adicionou feiras no passado
			int key = feirasCounter;
			feirasCounter++;
			feiras.Insert(new Feira(key, nome_Feira, DateTime.Now, data_Final,
										  preco_candidatura, u.Email, categoria_feira));
		}

		public Stand GetStand(int idStand)
		{
			return stands[idStand];
		}

		public Feira GetFeira(int idFeira)
		{
			return feiras[idFeira];
		}
		public List<Feira> GetFeiras() 
		{
			return feiras.ListAllFeiras().Where(f => f.DataInicio.CompareTo(DateTime.Today) <= 0 && (!f.DataFim.HasValue || f.DataFim.Value.CompareTo(DateTime.Today) >= 0)).ToList();
		}
		public Categoria? GetCategoria(int id)
		{
			return categorias[id];
		}

		public List<Feira> FeirasNotStarted()
		{
			return feiras.ListAllFeiras().Where(f => f.DataInicio.CompareTo(DateTime.Today) > 0).ToList();
		}
		public List<Notificacao> GetNotificacaos(string email)
		{
			//if(mapNotificacao.ContainsKey(email))
				//return MapNotificacao[email];
			return new();
		}

		public void AddNotificacao(string emailUser,Notificacao not)
		{
            //List<Notificacao> lista;
            //if (!mapNotificacao.TryGetValue(emailUser, out lista))
            //{
            //    lista = new List<Notificacao>();
            //    mapNotificacao[emailUser] = lista;
            //}
            //lista.Add(not);
        }
		public Utilizador GetUtilizador(string emailUtilizador)
		{
			return users[emailUtilizador];
		}
		public List<Stand> GetStandFeirante(string feirante)
		{
			return stands.ListAllStands().Where(s => s.EmailDono.Equals(feirante)).ToList();
		}
		public void AprovarCandidatura(int candidatura)
		{

		}
		public void CriaCandidatura (int stand, int feira)
		{

		}

		public List<Leilao> GetLeiloesFeiraStand(int idFeira, int idStand)
		{
			Stand s = stands[idStand];
			Feira f = feiras[idFeira];
			List<Leilao> res = f.ListaLeiloes[s.EmailDono];
			res.Where(lei => lei.Feira == idFeira && lei.Stand == idStand);
			return res;
		}

		public List<Leilao> GetLeiloesFeira(int idFeira)
		{
			return leiloes.ListLeiloesFeira(idFeira);
		}
		// TO DO
		public List<Leilao> GetLeiloesCL(string email)
		{
			Utilizador? cliente = users[email];
			//verificar 
			return leiloes.ListAllLeiloes().Where();
		}

		public List<Stand> GetStandsFeira(int idFeira)
		{
			return feiras.StandsDaFeira(idFeira);
		}
		// TO DO
		public List<Venda> GetNegociacoes(string email)
		{
			Utilizador? cliente = users[email];
			//receber todas as vendas
			//for vendas verifica se é uma negociaçao, se sim adicionar a lista
			return new();
		}
		// TO DO
		public Negociacao GetNegociacao(int idNegociacao)
		{
            //receber todas as vendas
            //for vendas verifica se é a negociaçao, se sim devolve a negociacao
            return new Negociacao();
		}
		// TO DO
		public void RegistaSucesso(int idNegociacao)
		{
            //receber todas as vendas
            //for vendas verifica se é a negociaçao, se sim devolve a negociacao
        }
        // TO DO
        public void RegistaInsucesso(int idNegociacao)
		{
            //receber todas as vendas
            //for vendas verifica se é a negociaçao, se sim devolve a negociacao
        }
        // TO DO
        public void RegistaNovoPreco(int idNegociacao, float novoPreco)
		{
			//receber todas as vendas
			//for vendas verifica se é a negociaçao, se sim devolve a negociacao
		}
		// TO DO
		public void AddVenda(Venda venda)
		{
			venda.IdVenda = vendasCounter;
			vendas.Insert(venda);
			vendasCounter++;
		}
		// TO DO
		public void AddNegociacaoVenda(Venda venda, Negociacao negociacao)
		{
			int idNeg = negociacao.IdNegociacao;
			venda.Negociacao = idNeg;
			AddVenda(venda);
			negociacao.IdNegociacao = negociacoesCounter;
			// insert negociacao
			negociacoesCounter++;
		}
		public Venda GetVenda(int idVenda)
		{
			return vendas[idVenda];
		}
		public Produto GetProduto(int idProduto)
		{
			return stands.GetProduto(idProduto);
		}

		public int AddSubCategoria(int categoria, float imposto)
		{
			return 0;
		}
		public int AddCategoria(string nome)
		{
			return 0;
		}
		public void Licitar(int idLeilao, string email, float licitacao)
		{
			if (leiloes.GetMaiorBid(idLeilao) < licitacao)
			{
				if (leiloes.LeilaoHasBidFromCliente(idLeilao, email))
					leiloes.UpdateBid(idLeilao, email, licitacao);
				else
					leiloes.InsertBid(idLeilao, email, licitacao);
			}
			else
				throw new BidValueInvalid("Valor menor que a última exceção");
		}
		public void AddStand(Stand s)
		{
			// adicionar produto + stand, cuidado com os ids
			// associar aos produtos a sub-categoria + id stand
		}


		public class Teste
		{
			static void Main(String[] args)
			{

				Cliente c = new Cliente("Eduardo","123456789","sweeper@gmail.com", DateTime.ParseExact("4/1/2000","d/M/yyyy", null),DateTime.Now);
				Feirante f = new Feirante("Jose", "bananas123", "ze@gmail.com", DateTime.MinValue, DateTime.Now, 2);
				Administrador a = new Administrador("Maria", "whatinthefuck", "maria@gmail.com", DateTime.ParseExact("12/12/1994", "d/M/yyyy", null), DateTime.Now);
				SistemaFeiras sf = new SistemaFeiras();
				sf.Registo(f);
				sf.Registo(c);
				sf.Registo(a);
				sf.Login("sweeper@gmail.com","123456789");
				sf.Login("ze@gmail.com", "bananas123");
				sf.Login("maria@gmail.com", "whatinthefuck");
				sf.CriarFeira(a, "Feira do Congo", null, 19.90f, 3);

				Venda v = new Venda();
				Venda v2 = v.Clone();
				v2.Preco = 3.65f;
				v.Preco = 3.64f;
				Console.WriteLine(v.ToString());
				Console.WriteLine(v2.ToString());
				Console.WriteLine(v.Equals(v2));



            }

		}
		/*
		*/
	}
}


