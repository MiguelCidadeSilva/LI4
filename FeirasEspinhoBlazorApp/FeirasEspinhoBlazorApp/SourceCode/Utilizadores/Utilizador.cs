using System.ComponentModel.DataAnnotations;

namespace FeirasEspinhoBlazorApp.SourceCode.Utilizadores
{
	public class Utilizador
	{
		public String? username { get; set; }
		public String? password { get; set; }
		public String? email { get; set; }
		public DateTime dataNascimento { get; set; }
		public DateTime dataCriacao { get; set; }
	}
}
