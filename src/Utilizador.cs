

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

        public Utilizador(String nome, String password, String email)
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
            Console.WriteLine("Hello, World!");
            Console.WriteLine("yes");
        }
    }
}