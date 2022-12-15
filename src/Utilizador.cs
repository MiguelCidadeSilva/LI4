﻿

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace FeirasEspinho
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

        public override String ToString()
        {
            return ("User: " + this.Username + "\nPassword: " + this.Password + "\nEmail: " + this.Email + "\nDataNascimento: " + this.DataNascimento.ToString("dd/MM/yyyy") + "\n");
        }


        public override bool Equals(Object? obj)
        {
            if (this == obj) return true;
            if (obj == null || (obj is not Utilizador)) return false;
            Utilizador u = (Utilizador)obj;
            return (this.Username.Equals(u.Username) &&
                       this.Password.Equals(u.Password) &&
                       this.Email.Equals(u.Email) &&
                       this.DataNascimento.Equals(u.DataNascimento));
        }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        public override int GetHashCode() => (Username, Password, Email, DataNascimento).GetHashCode();

        public bool CheckCredenciais(String username, String password)
        {
            return ( this.Username.Equals(username) && this.Password.Equals(password) ) ? true : false;
        }


    }


    // ======SUBCLASSE ADMIN======


    public class Administrador : Utilizador
    {
        public Administrador() : base()
        {
        }

        public Administrador(String username, String password, String email, DateTime dataNascimento) : base(username, password, email, dataNascimento)
        {
        }

        public Administrador(Administrador u) : base(u.Username, u.Password, u.Email, u.DataNascimento)
        {
        }

        public override Administrador Clone()
        {
            return new Administrador(this);
        }

        public override String ToString()
        {
            return ("ADMINISTRADOR\n" + base.ToString());
        }

        public override Boolean Equals(Object? obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;

            return base.Equals(obj);
        }

        public override int GetHashCode() => (Username, Password, Email, DataNascimento).GetHashCode();
    }


    //=====SUBCLASSE CLIENTE=====


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

        public override Cliente Clone()
        {
            return new Cliente(this);
        }

        public override String ToString()
        {
            return ("CLIENTE\n" + base.ToString());
        }

        public override Boolean Equals(Object? obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;

            return base.Equals(obj);
        }

        public override int GetHashCode() => (Username, Password, Email, DataNascimento).GetHashCode();

    }


    //SUBCLASSE FEIRANTE


    public class Feirante : Utilizador
    {
        //no modelo lógico, a classe Feirante é a única que tem um numero de conta associado...
        protected int nr_conta {get; set;}

        public Feirante() : base()
        {
            this.nr_conta = 0;
        }

        public Feirante(String username, String password, String email, DateTime dataNascimento, int nr_conta) : base(username, password, email, dataNascimento)
        {
            this.nr_conta = nr_conta;
        }

        public Feirante(Feirante u) : base(u.Username, u.Password, u.Email, u.DataNascimento)
        {
            this.nr_conta = u.nr_conta;
        }

        public override Feirante Clone()
        {
            return new Feirante(this);
        }

        public override String ToString()
        {
            return ("FEIRANTE\n" + base.ToString() + "Numero da conta: " + this.nr_conta + "\n");
        }

        public override Boolean Equals(Object? obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;

            Feirante f = (Feirante)obj;
            return ( base.Equals(obj) && (this.nr_conta == f.nr_conta) );
        }

        public override int GetHashCode() => (Username, Password, Email, DataNascimento,nr_conta).GetHashCode();


    }
    /*
    public class Teste
    {
        static void Main(String[] args)
        {
            CultureInfo portugal = new CultureInfo("PT-pt");
            //testes
            Administrador a = new Administrador("Eduardo", "123", "braga@gmail.com", DateTime.ParseExact("24/10/2000", "dd/MM/yyyy", portugal));
            Administrador a2 = new Administrador("Eduardo", "123", "braga@gmail.com", DateTime.ParseExact("24/10/2000","dd/MM/yyyy",portugal));
            Cliente c = new Cliente("Joao", "456", "jose@hotmail.com", DateTime.ParseExact("24/10/1999", "dd/MM/yyyy", portugal));
            Feirante f = new Feirante("Miguel", "789", "miguel@sapo.pt", DateTime.ParseExact("31/10/2000", "dd/MM/yyyy", portugal),4);

            Console.WriteLine(a.ToString());
            Console.WriteLine(a2.ToString());
            Console.WriteLine(c.ToString());
            Console.WriteLine(f.ToString());

            Console.WriteLine(a.Equals(a2));


        }


    }
    */
}