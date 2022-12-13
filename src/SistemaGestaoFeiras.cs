using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Runtime.CompilerServices;
using  Utilizadores;

namespace FeirasEspinho
{
	public class SistemaFeiras
	{
		private Hashtable? InfoUtilizadores  { get; set; }
		private Hashtable? InfoFeiras  { get; set; }


		public SistemaFeiras()
		{
			InfoUtilizadores = new Hashtable();
			InfoFeiras = new Hashtable();
		}

		public SistemaFeiras(Hashtable InfoUtilizadores, Hashtable InfoFeiras)
		{
			this.InfoUtilizadores = (Hashtable)InfoUtilizadores.Clone();
			this.InfoFeiras = (Hashtable)InfoFeiras.Clone();
		}


		public SistemaFeiras(SistemaFeiras sf)
		{
			this.InfoUtilizadores = sf.InfoUtilizadores;
			this.InfoFeiras = sf.InfoFeiras;

		}

		public String toString()
		{
			String s = "=====USERS=====\n";
			int i = 1;
			foreach (DictionaryEntry  dc in InfoUtilizadores)
			{
                if(dc.Key is not null)
				{
                    s += i + ". -> \n" + ((Utilizador) dc.Value).toString();
                    i++;
                }
			}
			i = 1;
		/*	foreach (String key in this.InfoFeiras)
			{
				s += (i + ". -> \n" + InfoFeiras[key].ToString());
				i++;
			}
		*/
			return s;

		}


		public void addUser(String nome_User, Utilizador u)
		{
			this.InfoUtilizadores.Add(nome_User, u);
		}


		public class Teste
		{
			static void Main(String[] args)
			{
				Console.WriteLine("teste");
				Administrador a = new Administrador("Eduardo","123","sweeper@gmail.com",DateTime.Now);
				SistemaFeiras sf = new SistemaFeiras();
				sf.addUser("Edurado", a);
				Console.WriteLine(sf.toString());
					     
			}

		}

	}
}
