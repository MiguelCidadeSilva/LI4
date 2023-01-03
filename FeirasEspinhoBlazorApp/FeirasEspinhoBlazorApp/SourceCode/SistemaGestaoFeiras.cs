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
			String key = u.Email;
			if (u.Password.Length < 8)
				throw new PasswordInvalidaException("Password tem de conter pelo menos 8 caracteres.");
			//if (users.)
			//	throw new EmailInvalidoException("Email já está associado a uma conta...");
			users.Insert(u);

		}

		//Quando a data final é null, é uma feira permanente
		//Não precisamos de verificar se já acabou quando implementarmos essa funcionalidade no sistema
		public void CriarFeira(Utilizador u, int id_feira, String nome_Feira, DateTime? data_Final, float preco_candidatura, int categoria_feira)
		{
			if (u is not Administrador)
				throw new PermissaoInvalidaException("Funcionalidade restrita a Administradores.");

			//Caso o admin em questão já adicionou feiras no passado
			int key = id_feira;
			feiras.Insert(new Feira(id_feira, nome_Feira, DateTime.Now, data_Final,
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
			return new();
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
			return new();
		}
		public void AddStandFeira(int stand, int feira)
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
			Feira f = SistemaFeiras.GetInstance().GetFeira(idFeira);
			List<Leilao> res = new();
			f.ListaLeiloes.Values.ToList().ForEach(res.AddRange);
			return res;
		}
		// TO DO
		public List<Leilao> GetLeiloesCL(string email)
		{
			return new();
		}
		public List<Stand> GetStandsFeira( int idFeira)
		{
			Feira f = SistemaFeiras.GetInstance().GetFeira(idFeira);
			List<Stand> stands = new();
			f.ListaStands.Values.ToList().ForEach(stands.AddRange);
			return stands;
		}
		// TO DO
		public List<Venda> GetNegociacoes(string email)
		{
			return new();
		}
		// TO DO
		public Negociacao GetNegociacao(int idNegociacao)
		{
			return new Negociacao();
		}
		// TO DO
		public void RegistaSucesso(int idNegociacao)
		{

		}
		// TO DO
		public void RegistaInsucesso(int idNegociacao)
		{

		}
		// TO DO
		public void RegistaNovoPreco(int idNegociacao, float novoPreco)
		{

		}
		public Venda GetVenda(int idVenda)
		{
			return new();
		}
		// TO DO
		public void AddVenda(Venda venda)
		{

		}

		// TO DO
		public void AddNegociacaoVenda(Venda venda, Negociacao negociacao)
		{

		}

		public List<Feira> FeirasNotStarted()
		{
			//return feiras..Values.Where(f => f.DataInicio.CompareTo(DateTime.Now) < 0).ToList();
			return new();
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
				sf.CriarFeira(a, 1, "Feira do Congo", null, 19.90f, 3);

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


