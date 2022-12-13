

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
            //  Converter de str "DD/MM/AAAA" p/ Datetime -> Convert.ToDateTime(str);

            Administrador a = new Administrador("Eduardo", "123", "braga@gmail.com",DateTime.Now);
            Administrador a2 = a.Clone();
            Console.Write(a.toString());

            Console.WriteLine("Prima Enter para terminar");
            Console.ReadLine();
        }
    }
}