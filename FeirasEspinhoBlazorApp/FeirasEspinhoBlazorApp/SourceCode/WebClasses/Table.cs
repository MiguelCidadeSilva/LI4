namespace FeirasEspinhoBlazorApp.SourceCode.WebClasses
{
	public class Table<Tipo>
	{
		private Dictionary<int, (Tipo, int)> content;
		private Dictionary<int, string> cssclasses;
		private string oldcssclasse;
		private int idFeira = -1;
		public Table(List<(int, Tipo, int)> conteudo)
		{
			content = new Dictionary<int, (Tipo, int)>();
			conteudo.ForEach(c => content[c.Item1] = (c.Item2, c.Item3));
			cssclasses = new Dictionary<int, string>();
			List<int> list = content.Keys.ToList();
			for (int i = 0; i < list.Count(); i++)
			{
				if (i % 2 == 0)
					cssclasses[list[i]] = "evenRow";
				else
					cssclasses[list[i]] = "";
			}
			// buscar feiras às base de dados
		}

		public Table(List<(int, Tipo)> conteudo)
		{
			content = new Dictionary<int, (Tipo, int)>();
			conteudo.ForEach(c => content[c.Item1] = (c.Item2, 0));
			cssclasses = new Dictionary<int, string>();
			List<int> list = content.Keys.ToList();
			for (int i = 0; i < list.Count(); i++)
			{
				if (i % 2 == 0)
					cssclasses[list[i]] = "evenRow";
				else
					cssclasses[list[i]] = "";
			}
			// buscar feiras às base de dados
		}

		public Dictionary<int, (Tipo, int)> Content { get => content;}
		public Dictionary<int, string> Cssclasses { get => cssclasses; }

		public void SelectedRow(int row)
		{
			if (row != idFeira)
			{
				if (idFeira != -1)
				{
					cssclasses[idFeira] = oldcssclasse;
				}
				oldcssclasse = cssclasses[row];
				idFeira = row;
				cssclasses[row] = "selected_row";
			}
		}
		public bool IdValid()
		{
			return idFeira != -1;
		}
	}
}
