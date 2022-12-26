using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Globalization;
using FeirasEspinhoBlazorApp.SourceCode.Stands;
using FeirasEspinhoBlazorApp.SourceCode.Feiras;
using FeirasEspinhoBlazorApp.SourceCode.Vendas;
using FeirasEspinhoBlazorApp.SourceCode.Utilizadores;
using System.Linq;

namespace FeirasEspinhoBlazorApp.SourceCode
{
	public class SistemaFeiras : Exception
	{

		private Dictionary<String, Cliente> mapClientes; //todos os clientes		||
		private Dictionary<String, Administrador> mapAdmins; // todos os feirantes  || UTILIZADORES(key --> email)
		private Dictionary<String, Feirante> mapFeirantes; // todos os admins		||

		private Dictionary<String, List<Notificacao>> mapNotificacao;
		private Dictionary<int, Feira> mapFeiras;
		private Dictionary<int, Stand> mapStands;
		private Dictionary<int, Produto> mapProdutos;

		private static SistemaFeiras instance = new SistemaFeiras();

		public static SistemaFeiras GetInstance()
		{
			return instance;
		}


		public Dictionary<String, Cliente> MapClientes
		{
			get { return mapClientes; }

			set { mapClientes = value.ToDictionary(entry => entry.Key, entry => entry.Value.Clone()); }
		}

		public Dictionary<String, Administrador> MapAdmins
		{
			get { return mapAdmins; }

			set { mapAdmins = value.ToDictionary(entry => entry.Key, entry => entry.Value.Clone()); }
		}

		public Dictionary<String, Feirante> MapFeirantes
		{
			get { return mapFeirantes; }

			set { mapFeirantes = value.ToDictionary(entry => entry.Key, entry => entry.Value.Clone()); }
		}
		public Dictionary<String, List<Notificacao>> MapNotificacao
		{
			get { return mapNotificacao; }

			set { mapNotificacao = value.ToDictionary(entry => entry.Key, entry => new List<Notificacao>(entry.Value)); }
		}
		public Dictionary<int, Feira> MapFeiras
		{
			get { return mapFeiras; }

			set { mapFeiras = value.ToDictionary(entry => entry.Key, entry => entry.Value.Clone()); }
		}

		public Dictionary<int, Stand> MapStands
		{
			get { return mapStands; }

			set { mapStands = value.ToDictionary(entry => entry.Key, entry => entry.Value); }
		}
		public Dictionary<int, Produto> MapProdutos
		{
			get { return mapProdutos; }

			set { mapProdutos = value.ToDictionary(entry => entry.Key, entry => entry.Value); }
		}


		private SistemaFeiras()
		{
			MapClientes = new Dictionary<String, Cliente>();
			MapAdmins = new Dictionary<String, Administrador>();
			MapFeirantes = new Dictionary<String, Feirante>();
			MapFeiras = new Dictionary<int, Feira>();
			MapStands = new Dictionary<int, Stand>();
			MapProdutos = new Dictionary<int, Produto>();
			MapNotificacao = new();
		}

		private SistemaFeiras(Dictionary<String, Cliente> MapClientes, Dictionary<String, Administrador> MapAdmins,
							 Dictionary<String, Feirante> MapFeirantes, Dictionary<int, Feira> MapFeiras,
							 Dictionary<int, Stand> MapStands, Dictionary<int, Produto> MapProdutos)
		{
			this.MapClientes = MapClientes.ToDictionary(entry => entry.Key, entry => entry.Value.Clone());
			this.MapAdmins = MapAdmins.ToDictionary(entry => entry.Key, entry => entry.Value.Clone());
			this.MapFeirantes = MapFeirantes.ToDictionary(entry => entry.Key, entry => entry.Value.Clone());
			this.MapFeiras = MapFeiras.ToDictionary(entry => entry.Key, entry => entry.Value.Clone());
			this.MapStands = MapStands.ToDictionary(entry => entry.Key, entry => entry.Value);
			this.MapProdutos = MapProdutos.ToDictionary(entry => entry.Key, entry => entry.Value);
		}

		private SistemaFeiras(SistemaFeiras sf)
		{
			this.MapClientes = sf.MapClientes;
			this.MapAdmins = sf.MapAdmins;
			this.MapFeirantes = sf.MapFeirantes;
			this.MapFeiras = sf.MapFeiras;
			this.MapStands = sf.MapStands;
			this.MapProdutos = sf.MapProdutos;
		}

		public override String ToString()
		{
			String s = "=====CLIENTES=====\n";
			int i = 1;
			foreach (KeyValuePair<String, Cliente> par in this.MapClientes)
			{
				s += ("\n" + i + "] ->");
				s += par.Key;
				s += ("\n" + par.Value.ToString());
			}
			i = 1;
			s += "=====FEIRANTES=====\n";
			foreach (KeyValuePair<String, Feirante> par in this.MapFeirantes)
			{
				s += ("\n" + i + "] ->");
				s += par.Key;
				s += ("\n" + par.Value.ToString());
			}
			i = 1;
			s += "=====ADMINISTRADORES=====\n";
			foreach (KeyValuePair<String, Administrador> par in this.MapAdmins)
			{
				s += ("\n" + i + "] ->");
				s += par.Key;
				s += ("\n" + par.Value.ToString());
			}


			return s;

		}



		//
		//	LOGIN: Verifica se a password é válida,e verifica as credenciais com a função verificaCredenciais da classe Utilizador
		//
		public int Login(String email, String password)
		{
			if (password.Length < 8)
				throw new PasswordInvalidaException("Password tem de ter 8 ou mais caracteres\n");

			//			VERIFICACAO CLIENTES
			foreach (KeyValuePair<String, Cliente> par in this.MapClientes)
			{
				Cliente s = new Cliente(par.Value);
				if (s.CheckCredenciais(email, password))
				{
					return 0;
				}
			}
			//			VERIFICACAO ADMINISTRADORES
			foreach (KeyValuePair<String, Administrador> par in this.MapAdmins)
			{
				Administrador a = new Administrador(par.Value);
				if (a.CheckCredenciais(email, password))
				{
					return 1;
				}
			}
			//			VERIFICACAO FEIRANTES
			foreach (KeyValuePair<String, Feirante> par in this.MapFeirantes)
			{
				Feirante f = new Feirante(par.Value);
				if (f.CheckCredenciais(email, password))
				{
					return 2;
				}
			}

			throw new EmailInvalidoException("email não está registado, regista-te");

		}

		//		Registo: Da maneira que fiz, não existem emails repetidos no sistema, mesmo que sejam tipos de utilizador diferentes.
		//				 Se o email ainda nao estiver registado no sistema, adiciona ao dicionario do tipo de utilizador que queres ser ao registar-te	
		//
		public void Registo(Utilizador u)
		{
			String key = u.Email;
			if (MapClientes.ContainsKey(key) || MapAdmins.ContainsKey(key) || MapFeirantes.ContainsKey(key))
				throw new EmailInvalidoException("Email já está associado a uma conta...");
			if (u.Password.Length < 8)
				throw new PasswordInvalidaException("Password tem menos de 8 caracteres...");

			if (u is Cliente)
			{
				Cliente c = (Cliente)u;
				MapClientes[key] = c;
				return;
			}
			else if (u is Administrador)
			{
				Administrador a = (Administrador)u;
				MapAdmins[key] = a;
				return;
			}
			else if (u is Feirante)
			{
				Feirante f = (Feirante)u;
				MapFeirantes[key] = f;
				return;
			}

			throw new RegistoInvalidoException("Registo abortado, algo correu mal...");

		}

		//Quando a data final é null, é uma feira permanente
		//Não precisamos de verificar se já acabou quando implementarmos essa funcionalidade no sistema
		public void CriarFeira(Utilizador u, int id_feira, String nome_Feira, DateTime? data_Final, float preco_candidatura, int categoria_feira)
		{
			if (u is not Administrador)
				throw new PermissaoInvalidaException("Funcionalidade restrita a Administradores.");

			//Caso o admin em questão já adicionou feiras no passado
			int key = id_feira;
			MapFeiras[key] = new Feira(id_feira, nome_Feira, DateTime.Now, data_Final,
										  preco_candidatura, u.Email, categoria_feira);
		}

		public Stand GetStand(int idStand)
		{
			return mapStands[idStand];
		}

		public Feira GetFeira(int idFeira)
		{
			return mapFeiras[idFeira];
		}
		public List<Notificacao> GetNotificacaos(string email)
		{
			if(mapNotificacao.ContainsKey(email))
				return MapNotificacao[email];
			return new();
		}

		public void AddNotificacao(string emailUser,Notificacao not)
		{
            List<Notificacao> lista;
            if (!mapNotificacao.TryGetValue(emailUser, out lista))
            {
                lista = new List<Notificacao>();
                mapNotificacao[emailUser] = lista;
            }
            lista.Add(not);
        }
		public Utilizador GetUtilizador(string emailUtilizador)
		{
			Utilizador user = mapAdmins.GetValueOrDefault(emailUtilizador);
			if(user == null)
			{
				user = mapClientes.GetValueOrDefault(emailUtilizador);
			}
			if(user == null)
			{
				user = mapClientes.GetValueOrDefault(emailUtilizador);
			}
			return user;
		}

		public List<Leilao> GetLeiloesFeiraStand(int idFeira, int idStand)
		{
			Stand s = SistemaFeiras.GetInstance().GetStand(idStand);
			Feira f = SistemaFeiras.GetInstance().GetFeira(idFeira);
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


