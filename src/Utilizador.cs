

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FeirasEspinho
{
    public abstract class Utilizador
    {
        protected String username;
        protected String password;
        protected String email;
        protected DateTime dataNascimento;

        public Utilizador()
        {
            this.username = "";
            this.password = "";
            this.email = "";
            this.dataNascimento = DateTime.MinValue;
        }

        public Utilizador(String username, String password, String email, DateTime dataNascimento)
        {
            this.username = username;
            this.password = password;
            this.email = email;
            this.dataNascimento = dataNascimento;
        }

        public String getUsername()
        {
            return this.username;
        }
        public String getPassword()
        {
            return this.password;
        }
        public String getEmail()
        {
            return this.email;
        }
        public DateTime getDataNascimento()
        {
            return this.dataNascimento;
        }

        public Utilizador(Utilizador u)
        {
            this.username = u.getUsername();
            this.password = u.getPassword();
            this.email = u.getEmail();
            this.dataNascimento = u.getDataNascimento();
        }

        public String toString()
        {
            return ("User: " + this.getUsername() + "\nPassword: " + this.getPassword() + "\nEmail: " + this.getEmail() + "\nDataNascimento: " + this.getDataNascimento() + "\n");
        }


        public Boolean equals(Object obj)
        {
            if (this == obj) return true;
            if ((obj.Equals(null)) || (obj is not Utilizador)) return false;
            Utilizador u = (Utilizador)obj;
            return (this.getUsername().Equals(u.getUsername()) &&
                       this.getPassword().Equals(u.getPassword()) &&
                       this.getEmail().Equals(u.getEmail()) &&
                       this.getDataNascimento().Equals(u.getDataNascimento()));
        }


    }

    public class Administrador : Utilizador 
    {
        public Administrador() : base()
        {
        }

        public Administrador(String username, String password, String email, DateTime dataNascimento) : base(username,password,email,dataNascimento)
        {
        }

        public Administrador(Administrador u) : base(u.getUsername(),u.getPassword(), u.getEmail(), u.getDataNascimento() )
        {
        }

        public Administrador Clone()
        {
            return new Administrador(this);
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