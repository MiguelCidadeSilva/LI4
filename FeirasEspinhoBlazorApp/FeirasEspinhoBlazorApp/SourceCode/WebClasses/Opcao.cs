namespace FeirasEspinhoBlazorApp.SourceCode.WebClasses
{
    public class Opcao
    {
        private string texto;
        private string simbolo;
        private string link;
        public Opcao(string texto, string simbolo, string link)
        {
            this.texto = texto;
            this.simbolo = simbolo;
            this.link = link;
        }
        public string Texto
        {
            get => texto; 
        }

        public string Simbolo
        {
            get => simbolo;
        }

        public string Link
        {
            get => link;
        }
    }
}
