using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Runtime.CompilerServices;
using FeirasEspinho;

using System.Xml.Linq;
using System.Globalization;

namespace FeirasEspinho
{	
	public class SistemaFeiras : Exception
	{
		public Dictionary<String,Cliente> MapClientes  { get; set; }
		public Dictionary<String,Administrador> MapAdmins  { get; set; }
		public Dictionary<String, Feirante> MapFeirantes { get; set; }
		//protected Dictionary<String,List<Feira> > MapFeiras { get; set; }

		public SistemaFeiras()
		{
			MapClientes = new Dictionary<String, Cliente>();
			MapAdmins = new Dictionary<String,Administrador>();
			MapFeirantes= new Dictionary<String,Feirante>();
			// MapFeiras = new Dictionary<String, new List<Feira>() >();
		}

		public SistemaFeiras(Dictionary<String,Cliente> MapClientes, Dictionary<String,Administrador> MapAdmins, Dictionary<String,Feirante> MapFeirantes )
		{
			this.MapClientes = MapClientes.ToDictionary(entry => entry.Key, entry => entry.Value.Clone());
            this.MapAdmins = MapAdmins.ToDictionary(entry => entry.Key, entry => entry.Value.Clone());
            this.MapFeirantes = MapFeirantes.ToDictionary(entry => entry.Key, entry => entry.Value.Clone());
        }


		public SistemaFeiras(SistemaFeiras sf)
		{
			this.MapClientes = sf.MapClientes.ToDictionary(entry => entry.Key, entry => entry.Value.Clone());
			this.MapAdmins = sf.MapAdmins.ToDictionary(entry => entry.Key, entry => entry.Value.Clone());
			this.MapFeirantes = sf.MapFeirantes.ToDictionary(entry => entry.Key, entry => entry.Value.Clone());
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

		//nao senti a necessidade de clonar a classe principal. Talvez irei implementar isso no futuro


		public void Login(String email, String password)
		{
			if (password.Length < 8)
				throw new PasswordInvalidaException("Password tem de ter 8 ou mais caracteres...burro\n");
			
			//			VERIFICACAO CLIENTES
            foreach (KeyValuePair<String, Cliente> par in this.MapClientes)
			{
				Cliente s = new Cliente(par.Value);
				if (s.CheckCredenciais(email, password))
                {
                    Console.WriteLine("[CLIENTE]Login bem sucedido - Bem vindo, " + s.Username + "!");
                    return;
                }
            }
			//			VERIFICACAO ADMINISTRADORES
			foreach (KeyValuePair<String,Administrador> par in this.MapAdmins)
			{
				Administrador a = new Administrador(par.Value);
				if (a.CheckCredenciais(email, password))
				{
					Console.WriteLine("[ADMINISTRADOR]Login bem sucedido - Bem vindo, " + a.Username + "!");
					return;
				}
			}
			//			VERIFICACAO FEIRANTES
            foreach (KeyValuePair<String, Feirante> par in this.MapFeirantes)
            {
                Feirante f = new Feirante(par.Value);
				if (f.CheckCredenciais(email, password))
                {
                    Console.WriteLine("[FEIRANTE]Login bem sucedido - Bem vindo, " + f.Username + "!");
                    return;
                }
            }

			throw new EmailInvalidoException("email não está registado, regista-te...burro");

        }

		public void Registo(Utilizador u)
		{
			String key = u.Email;
			if (MapClientes.ContainsKey(key) || MapAdmins.ContainsKey(key) || MapFeirantes.ContainsKey(key))
				throw new EmailInvalidoException("Email já está associado a uma conta...");

			if(u is FeirasEspinho.Cliente)
			{
				Cliente c = (Cliente) u;
				MapClientes[key] = c;
				Console.WriteLine("Registado " + c.Username + " com sucesso");
				return;
			}
			else if (u is FeirasEspinho.Administrador)
			{
				Administrador a = (Administrador) u;
				MapAdmins[key] = a;
                Console.WriteLine("Registado " + a.Username + " com sucesso");
				return;
            }
			else if (u is FeirasEspinho.Feirante)
			{
				Feirante f = (Feirante) u;
				MapFeirantes[key] = f;
                Console.WriteLine("Registado " + f.Username + " com sucesso");
				return;
            }

			throw new RegistoInvalido("Registo abortado, algo correu mal!");

		}





		public class Teste
		{
			static void Main(String[] args)
			{

				Cliente c = new Cliente("Eduardo","123456789","sweeper@gmail.com", DateTime.ParseExact("4/1/2000","d/M/yyyy", null),DateTime.Now);
				Feirante f = new Feirante("Jose", "bananas123", "ze@gmail.com", DateTime.MinValue, DateTime.Now, 2);
				SistemaFeiras sf = new SistemaFeiras();
				try
				{
				sf.Registo(f);
				sf.Registo(c);
				sf.Registo(f);
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
				
				Console.WriteLine(sf.ToString());
				try
				{
				sf.Login("sweeper@gmail.com","123456789");
				sf.Login("ze@gmail.com", "bananas123");
				}
				catch(Exception ex)
				{
				Console.WriteLine(ex.Message);
				}
				

				
			}

		}
		/*
		*/
	}
}


