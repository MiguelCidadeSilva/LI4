using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Runtime.CompilerServices;
using FeirasEspinho;


namespace FeirasEspinho
{
	public class SistemaFeiras
	{
		protected Dictionary<String,Cliente> MapClientes  { get; set; }
		protected Dictionary<String,Administrador> MapAdmins  { get; set; }
		protected Dictionary<String, Feirante> MapFeirantes { get; set; }


		public SistemaFeiras()
		{
			MapClientes = new Dictionary<String, Cliente>();
			MapAdmins = new Dictionary<String,Administrador>();
			MapFeirantes= new Dictionary<String,Feirante>();
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

            return s;

		}

		public bool Login(String user, String password)
		{
			if(password.Length <= 8)
				return false;
			

            foreach (KeyValuePair<String, Cliente> par in this.MapClientes)
			{
				Cliente s = new Cliente(par.Value);
				if (s.CheckCredenciais(user, password))
					return true;
			}

			return false;

        }



		public class Teste
		{
			static void Main(String[] args)
			{
				Console.WriteLine("teste");
				Cliente c = new Cliente("Eduardo","123456789","sweeper@gmail.com",DateTime.Now);
				SistemaFeiras sf = new SistemaFeiras();
				sf.MapClientes.Add("Eduardo", c);
				Console.WriteLine(sf.ToString());
				Console.Write(sf.Login("Eduardo","123456789"));

				
					     
			}

		}
		/*
		*/
	}
}


