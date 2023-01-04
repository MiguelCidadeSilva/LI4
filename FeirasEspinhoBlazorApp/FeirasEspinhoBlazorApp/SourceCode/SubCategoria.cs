namespace FeirasEspinhoBlazorApp.SourceCode
{
	public class SubCategoria : Categoria
	{
		private int idS;
		private float imposto;
		public SubCategoria(int id, string name, int idS, float imposto) : base(id, name)
		{
			this.idS = idS;
			this.imposto = imposto;
		}

		public int IdS { get => idS; }
		public float Imposto { get => imposto; set => imposto = value; }

        public override string ToString()
        {
            return "IdCategoria: " + Id + "\n" + "Nome: " + Name + "\n" +"IdSubCategoria: "+IdS+"\n"+"Imposto: "+Imposto;
        }

    }
}
