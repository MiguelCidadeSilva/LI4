namespace FeirasEspinhoBlazorApp.SourceCode
{
	public class Categoria
	{
		private int id;
		private string name;
		public Categoria(int id, string name) 
		{
			this.id = id;
			this.name = name;
		}

		public int Id { get => id; }
		public string Name { get => name; }


		public override string ToString()
		{
			return "Id: " + Id + "\n" + "Nome: " + Name + "\n";
		}
	}
}
