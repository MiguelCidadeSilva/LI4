﻿namespace FeirasEspinhoBlazorApp.SourceCode.WebClasses
{
	public class Table<Tipo>
	{
		private List<(Tipo, float?)> content;
		private Dictionary<int, string> cssclasses;
		private string oldcssclasse;
		private int index = -1;

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
		public Table(List<(Tipo, float?)> conteudo)
		{
			content = new List<(Tipo, float?)>(conteudo);
			cssclasses = new Dictionary<int, string>();
			defineCss();
		}
		public Table(List<(Tipo, int)> conteudo)
		{
			content = new();
			conteudo.ForEach(c => content.Add((c.Item1, (float)c.Item2)));
			cssclasses = new Dictionary<int, string>();
			defineCss();
		}

		public Table(List<Tipo> conteudo)
		{
			content = new List<(Tipo, float?)>();
			conteudo.ForEach(c => content.Add((c,null)));
			cssclasses = new Dictionary<int, string>();
			defineCss();
		}

		public List<(int, Tipo, float?)> Content 
		{
			get {
				List<(int, Tipo, float?)> res = new List<(int, Tipo, float?)>();
				int i = 0;
				foreach (var item in content)
				{
					res.Add((i, item.Item1, item.Item2));
					i++;
				}
				return res;
			}
		}
		public int GetSelected()
		{
			return index;
		}
		public Dictionary<int, string> Cssclasses { get => cssclasses; }

		public void SelectedRow(int row)
		{
			if (row != index)
			{
				if (index != -1)
				{
					cssclasses[index] = oldcssclasse;
				}
				oldcssclasse = cssclasses[row];
				index = row;
				cssclasses[row] = "selected_row";
			}
		}
		public void Unselect()
		{
			if(index != -1)
			{
				if (index % 2 == 0)
					cssclasses[index] = "evenRow";
				else
					cssclasses[index] = "";
				index = -1;
			}
		}
		public Tipo GetElement()
		{
			return this.content[index].Item1;
		}
		public bool IdValid()
		{
			return index != -1;
		}
		public int AddElement(Tipo item)
		{
			int pos = content.Count;
			this.content.Add((item,0)); 
			if (pos % 2 == 0)
				cssclasses[pos] = "evenRow";
			else
				cssclasses[pos] = "";
			return pos;

		}
	}
}
