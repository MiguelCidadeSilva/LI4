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

namespace FeirasEspinhoBlazorApp.SourceCode
{
    public class SistemaFeiras : Exception
	{

		private Dictionary<String, Cliente> mapClientes; //todos os clientes		||
		private Dictionary<String, Administrador> mapAdmins; // todos os feirantes  || UTILIZADORES(key --> email)
		private Dictionary<String, Feirante> mapFeirantes; // todos os admins		||

		private Dictionary<String, List<Feira>> mapFeiras;
		private Dictionary<int, Stand> mapStands;
		private Dictionary<int, Produto> mapProdutos;


        public Dictionary<String,Cliente> MapClientes
		{
			get { return mapClientes; }

			set { mapClientes = value.ToDictionary(entry => entry.Key, entry => entry.Value.Clone()); }
		}

		public Dictionary<String,Administrador> MapAdmins
        {
            get { return mapAdmins; }

            set { mapAdmins = value.ToDictionary(entry => entry.Key, entry => entry.Value.Clone()); }
        }

        public Dictionary<String, Feirante> MapFeirantes
        {
            get { return mapFeirantes; }

            set { mapFeirantes = value.ToDictionary(entry => entry.Key, entry => entry.Value.Clone()); }
        }

       public Dictionary<String, List<Feira>> MapFeiras
		{ 
            get { return mapFeiras; }

			set { mapFeiras = value.ToDictionary(entry => entry.Key, entry => new List<Feira>(entry.Value)); }
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

        public SistemaFeiras()
		{
			MapClientes = new Dictionary<String, Cliente>();
			MapAdmins = new Dictionary<String,Administrador>();
			MapFeirantes= new Dictionary<String,Feirante>();
			MapFeiras = new Dictionary<String,List<Feira>>();
			MapStands = new Dictionary<int, Stand>();
			MapProdutos = new Dictionary<int, Produto>();
		}

		public SistemaFeiras(Dictionary<String,Cliente> MapClientes, Dictionary<String,Administrador> MapAdmins,
							 Dictionary<String,Feirante> MapFeirantes, Dictionary<String,List<Feira>> MapFeiras,
							 Dictionary<int,Stand> MapStands, Dictionary<int,Produto> MapProdutos)
		{
			this.MapClientes = MapClientes.ToDictionary(entry => entry.Key, entry => entry.Value.Clone());
            this.MapAdmins = MapAdmins.ToDictionary(entry => entry.Key, entry => entry.Value.Clone());
            this.MapFeirantes = MapFeirantes.ToDictionary(entry => entry.Key, entry => entry.Value.Clone());
			this.MapFeiras = MapFeiras.ToDictionary(entry => entry.Key, entry => new List<Feira>(entry.Value));
			this.MapStands = MapStands.ToDictionary(entry => entry.Key, entry => entry.Value);
			this.MapProdutos = MapProdutos.ToDictionary(entry => entry.Key, entry => entry.Value);
        }

		public SistemaFeiras(SistemaFeiras sf)
		{
			this.MapClientes = sf.MapClientes;
			this.MapAdmins = sf.MapAdmins;
			this.MapFeirantes = sf.MapFeirantes;
			this.MapFeiras = sf.MapFeiras;
			this.MapStands= sf.MapStands;
			this.MapProdutos= sf.MapProdutos;
        }

		public override String ToString()
		{
			String s = "=====CLIENTES=====\n";
			int i = 1;
			foreach(KeyValuePair<String,Cliente> par in this.MapClientes)
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
			foreach (KeyValuePair<String,Administrador> par in this.MapAdmins)
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

			if(u is Cliente)
			{
				Cliente c = (Cliente) u;
				MapClientes[key] = c;
				return;
			}
			else if (u is Administrador)
			{
				Administrador a = (Administrador) u;
				MapAdmins[key] = a;
				return;
            }
			else if (u is Feirante)
			{
				Feirante f = (Feirante) u;
				MapFeirantes[key] = f;
				return;
            }

			throw new RegistoInvalidoException("Registo abortado, algo correu mal...");

		}

        //Quando a data final é null, é uma feira permanente
        //Não precisamos de verificar se já acabou quando implementarmos essa funcionalidade no sistema
        public void CriarFeira(Utilizador u, int id_feira, String nome_Feira,DateTime? data_Final, float preco_candidatura, int categoria_feira )
		{
			if (u is not Administrador)
				throw new PermissaoInvalidaException("Funcionalidade restrita a Administradores.");
			
			//Caso o admin em questão já adicionou feiras no passado
			String key = u.Email;
			if(MapFeiras.ContainsKey(key))
			{
				MapFeiras[key].Add( new Feira(id_feira, nome_Feira, DateTime.Now, data_Final,
											  preco_candidatura, key, categoria_feira) );
			}
			else
			{
				MapFeiras.Add(key,new List<Feira>());
				MapFeiras[key].Add( new Feira(id_feira, nome_Feira, DateTime.Now, data_Final,
                                              preco_candidatura, key, categoria_feira) );
            }
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


