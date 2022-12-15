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
		protected Dictionary<String,Cliente> MapClientes  { get; set; }
		protected Dictionary<String,Administrador> MapAdmins  { get; set; }
		protected Dictionary<String, Feirante> MapFeirantes { get; set; }
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


		public void Login(String user, String password)
		{
			if (password.Length < 8)
				throw new PasswordInvalidaException("Password tem de ter 8 ou mais caracteres...burro\n");
			
			//			VERIFICACAO CLIENTES
            foreach (KeyValuePair<String, Cliente> par in this.MapClientes)
			{
				Cliente s = new Cliente(par.Value);
				if (s.CheckCredenciais(user, password))
                {
                    Console.WriteLine("[CLIENTE]Login bem sucedido - Bem vindo, " + user + "!");
                    return;
                }
            }
			//			VERIFICACAO ADMINISTRADORES
			foreach (KeyValuePair<String,Administrador> par in this.MapAdmins)
			{
				Administrador a = new Administrador(par.Value);
				if (a.CheckCredenciais(user, password))
				{
					Console.WriteLine("[ADMINISTRADOR]Login bem sucedido - Bem vindo, " + user + "!");
					return;
				}
			}
			//			VERIFICACAO FEIRANTES
            foreach (KeyValuePair<String, Feirante> par in this.MapFeirantes)
            {
                Feirante f = new Feirante(par.Value);
				if (f.CheckCredenciais(user, password))
                {
                    Console.WriteLine("[FEIRANTE]Login bem sucedido - Bem vindo, " + user + "!");
                    return;
                }
            }

			throw new UsernameInvalidoException("username não existe, regista-te...burro");

        }



		public class Teste
		{
			static void Main(String[] args)
			{
                Console.WriteLine("teste");
				Cliente c = new Cliente("Eduardo","123456789","sweeper@gmail.com", DateTime.ParseExact("24/10/2000", "dd/MM/yyyy", new CultureInfo("pt-pt")),DateTime.Now);
				Feirante f = new Feirante("Jose", "bananas123", "ze@gmail.com", DateTime.MinValue, DateTime.Now, 2);
				SistemaFeiras sf = new SistemaFeiras();
				sf.MapClientes.Add("Eduardo", c.Clone());
				sf.MapFeirantes.Add("Jose", f.Clone());
				Console.WriteLine(sf.ToString());
				try
				{
				sf.Login("Eduardo","123456789");
				sf.Login("JoseBruh", "bananas123");
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


