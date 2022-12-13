

using System.ComponentModel.DataAnnotations;

namespace FeirasEspinho
{
    [Serializable]
    internal class Utilizador
    {
        private String nome;
        private String password;
        private String email;


        public Utilizador()
        {
            this.nome = "";
            this.password = "";
            this.email = "";
        }

        public void setNome(String nome)
        {
            this.nome = nome;
            this.password = password;
            this.email = email;
        } 
        
    
    }

    class Teste
    {
        static void Main(String[] args)
        {
            Utilizador u = new Utilizador("Eduardo", "123", "braga@gmail.com");
            Console.Write(u.toString());
            Console.WriteLine("Prima Enter para terminar");
            Console.ReadLine();
        }
    }
}