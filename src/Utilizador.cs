

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Utilizadores
{
    // SUPERCLASSE USER
    public abstract class Utilizador
    {
        protected String Username  // user
        { get; set; }
        protected String Password  // password
        { get; set; }
        protected String Email  // mail
        { get; set; }
        protected DateTime DataNascimento  // data nascimento(DD/MM/AAAA)
        { get; set; }

        public Utilizador()
        {
            this.Username = "";
            this.Password = "";
            this.Email = "";
            this.DataNascimento = DateTime.MinValue;
        }

        public Utilizador(String username, String password, String email, DateTime dataNascimento)
        {
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.DataNascimento = dataNascimento;
        }

        public Utilizador(Utilizador u)
        {
            this.Username = u.Username;
            this.Password = u.Password;
            this.Email = u.Email;
            this.DataNascimento = u.DataNascimento;
        }

        public virtual String toString()
        {
            return ("User: " + this.Username + "\nPassword: " + this.Password + "\nEmail: " + this.Email + "\nDataNascimento: " + this.DataNascimento.ToString("dd/MM/YYYY") + "\n");
        }


        public virtual Boolean equals(Object obj)
        {
            if (this == obj) return true;
            if ((obj.Equals(null)) || (obj is not Utilizador)) return false;
            Utilizador u = (Utilizador)obj;
            return (this.Username.Equals(u.Username) &&
                       this.Password.Equals(u.Password) &&
                       this.Email.Equals(u.Email) &&
                       this.DataNascimento.Equals(u.DataNascimento));
        }


    }

    // SUBCLASSE ADMIN
    public class Administrador : Utilizador 
    {
        public Administrador() : base()
        {
        }

        public Administrador(String username, String password, String email, DateTime dataNascimento) : base(username,password,email,dataNascimento)
        {
        }

        public Administrador(Administrador u) : base(u.Username,u.Password, u.Email, u.DataNascimento )
        {
        }

        public Administrador Clone()
        {
            return new Administrador(this);
        }

        public override String toString()
        {
            return ("ADMINISTRADOR\n" + base.toString()); 
        }

        public override Boolean equals(Object obj)
        {
            if(obj == null) return false;
            if (this == obj) return true;

            return base.equals(obj);
        }


    }

    //SUBCLASSE CLIENTE


    public class Cliente : Utilizador
    {
        public Cliente() : base()
        {
        }

        public Cliente(String username, String password, String email, DateTime dataNascimento) : base(username, password, email, dataNascimento)
        {
        }

        public Cliente(Cliente u) : base(u.Username, u.Password, u.Email, u.DataNascimento)
        {
        }

        public Cliente Clone()
        {
            return new Cliente(this);
        }

        public override String ToString()
        {
            return ("CLIENTE\n" + base.ToString());
        }

        public override Boolean Equals(Object obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;

            return base.equals(obj);
        }


    }


    //SUBCLASSE FEIRANTE

    public class Feirante : Utilizador
    {
        public Feirante() : base()
        {
        }

        public Feirante(String username, String password, String email, DateTime dataNascimento) : base(username, password, email, dataNascimento)
        {
        }

        public Feirante(Feirante u) : base(u.Username, u.Password, u.Email, u.DataNascimento)
        {
        }

        public Feirante Clone()
        {
            return new Feirante(this);
        }

        public override String toString()
        {
            return ("CLIENTE\n" + base.toString());
        }

        public override Boolean equals(Object obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;

            return base.equals(obj);
        }


    }
    /*
    public class Teste
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
    */
     

}