namespace FeirasEspinhoBlazorApp.SourceCode.WebClasses
{
	public class Table<Tipo>
	{
		private List<(Tipo, int)> content;
		private Dictionary<int, string> cssclasses;
		private string oldcssclasse;
		private int idFeira = -1;

		private void defineCss()
		{
			for (int i = 0; i < content.Count(); i++)
			{
				if (i % 2 == 0)
					cssclasses[i] = "evenRow";
				else
					cssclasses[i] = "";
			}
		}
		public Table(List<(Tipo, int)> conteudo)
		{
			content = new List<(Tipo, int)>(conteudo);
			cssclasses = new Dictionary<int, string>();
			defineCss();
			// buscar feiras às base de dados
		}

		public Table(List<Tipo> conteudo)
		{
			content = new List<(Tipo, int)>();
			conteudo.ForEach(c => content.Add((c,0)));
			cssclasses = new Dictionary<int, string>();
			defineCss();
			// buscar feiras às base de dados
		}

		public List<(int, Tipo, int)> Content 
		{
			get {
				List<(int, Tipo, int)> res = new List<(int, Tipo, int)>();
				int i = 0;
				foreach (var item in content)
				{
					res.Add((i, item.Item1, item.Item2));
					i++;
				}
				return res;
			}
		}
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
