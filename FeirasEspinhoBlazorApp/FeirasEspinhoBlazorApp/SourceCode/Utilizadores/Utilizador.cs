using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace FeirasEspinhoBlazorApp.SourceCode.Utilizadores
{
    // SUPERCLASSE USER
    public abstract class Utilizador : Exception
    {
        private string? username;
        private string? password;
        private string? email;
        private DateTime dataNascimento;
        private DateTime dataCriacao;

        public string? Username
        {
            get { return username; }
            set { username = value; }
        }

        public string? Password
        {
            get { return password; }
            set { password = value; }
        }

        public string? Email
        {
            get { return email; }
            set { email = value; }
        }

        public DateTime DataNascimento
        {
            get { return dataNascimento; }
            set { dataNascimento = value; }
        }

        public DateTime DataCriacao
        {
            get { return dataCriacao; }
            set { dataCriacao = value; }
        }

        public Utilizador()
        {
            Username = "";
            Password = "";
            Email = "";
            DataNascimento = DateTime.MinValue;
            DataCriacao = DateTime.MinValue;
        }

        public Utilizador(string username, string password, string email, DateTime dataNascimento, DateTime dataCriacao)
        {
            Username = username;
            Password = password;
            Email = email;
            DataNascimento = dataNascimento;
            DataCriacao = dataCriacao;

        }

        public Utilizador(Utilizador u)
        {
            Username = u.Username;
            Password = u.Password;
            Email = u.Email;
            DataNascimento = u.DataNascimento;
            DataCriacao = u.DataCriacao;
        }

        public override string ToString()
        {
            return "User: " + Username + "\nPassword: " + Password + "\nEmail: " + Email + "\nDataNascimento: "
                    + DataNascimento.ToString("dd/MM/yyyy") + "\n"
                    + "DataCriacao: " + DataCriacao.ToString("dd/MM/yyyy") + "\n";
        }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;
            if (obj == null || obj is not Utilizador) return false;
            Utilizador u = (Utilizador)obj;
            return Username.Equals(u.Username) &&
                       Password.Equals(u.Password) &&
                       Email.Equals(u.Email) &&
                       DataNascimento.Equals(u.DataNascimento) &&
                       DataCriacao.Equals(u.DataCriacao);
        }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        public override int GetHashCode() => (Username, Password, Email, DataNascimento, DataCriacao).GetHashCode();



        // verificacao de credenciais: 1) caso (email inserido no menu de login) != (email da classe utilizador em que a funcao é invocada) retorna false                                                                                                                              
        //                             2) caso os usernames sejam iguais, retorna true se a password for igual     
        //                             3) se os users condizem mas a password seja errada, retorna uma excecao, a dizer que a password está errada
        public virtual bool CheckCredenciais(string email, string password)
        {

            if (!Email.Equals(email)) return false;

            if (Email.Equals(email) && Password.Equals(password)) return true;

            throw new PasswordInvalidaException("passwords nao condizem...");

        }


    }


    // ======SUBCLASSE ADMIN======


    public class Administrador : Utilizador
    {
        public Administrador() : base()
        {
        }

        public Administrador(string username, string password, string email, DateTime dataNascimento, DateTime dataCriacao) : base(username, password, email, dataNascimento, dataCriacao)
        {
        }

        public Administrador(Administrador u) : base(u.Username, u.Password, u.Email, u.DataNascimento, u.DataCriacao)
        {
        }

        public override Administrador Clone()
        {
            return new Administrador(this);
        }

        public override string ToString()
        {
            return "ADMINISTRADOR\n" + base.ToString();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;

            return base.Equals(obj);
        }

        public override int GetHashCode() => (Username, Password, Email, DataNascimento, DataCriacao).GetHashCode();
    }


    //=====SUBCLASSE CLIENTE=====


    public class Cliente : Utilizador
    {
        public Cliente() : base()
        {
        }

        public Cliente(string username, string password, string email, DateTime dataNascimento, DateTime dataCriacao) : base(username, password, email, dataNascimento, dataCriacao)
        {
        }

        public Cliente(Cliente u) : base(u.Username, u.Password, u.Email, u.DataNascimento, u.DataCriacao)
        {
        }

        public override Cliente Clone()
        {
            return new Cliente(this);
        }

        public override string ToString()
        {
            return "CLIENTE\n" + base.ToString();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;

            return base.Equals(obj);
        }

        public override int GetHashCode() => (Username, Password, Email, DataNascimento, DataCriacao).GetHashCode();

    }


    //SUBCLASSE FEIRANTE


    public class Feirante : Utilizador
    {
        //no modelo lógico, a classe Feirante é a única que tem um numero de conta associado...
        public int iDconta;

        public int IDconta
        {
            get { return iDconta; }
            set { iDconta = value; }
        }

        public Feirante() : base()
        {
            IDconta = 0;
        }

        public Feirante(string username, string password, string email, DateTime dataNascimento, DateTime dataCriacao, int IDconta) : base(username, password, email, dataNascimento, dataCriacao)
        {
            this.IDconta = IDconta;
        }

        public Feirante(Feirante u) : base(u.Username, u.Password, u.Email, u.DataNascimento, u.DataCriacao)
        {
            IDconta = u.IDconta;
        }

        public override Feirante Clone()
        {
            return new Feirante(this);
        }

        public override string ToString()
        {
            return "FEIRANTE\n" + base.ToString() + "Numero da conta: " + IDconta + "\n";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;

            Feirante f = (Feirante)obj;
            return base.Equals(obj) && IDconta == f.IDconta;
        }

        public override int GetHashCode() => (Username, Password, Email, DataNascimento, DataCriacao, IDconta).GetHashCode();


    }

    /*
    public class Teste
    {
        static void Main(String[] args)
        {

            CultureInfo portugal = new CultureInfo("PT-pt");
            //testes
            Administrador a = new Administrador("Eduardo", "123", "braga@gmail.com", DateTime.ParseExact("24/10/2000", "dd/MM/yyyy", portugal), DateTime.Now);
            Administrador a2 = new Administrador("Eduardo", "123", "braga@gmail.com", DateTime.ParseExact("24/10/2000","dd/MM/yyyy",portugal), DateTime.Now);
            Cliente c = new Cliente("Joao", "456", "jose@hotmail.com", DateTime.ParseExact("24/10/1999", "dd/MM/yyyy", portugal), DateTime.Now);
            Feirante f = new Feirante("Miguel", "789", "miguel@sapo.pt", DateTime.ParseExact("31/10/2000", "dd/MM/yyyy", portugal), DateTime.Now,4);

            Console.WriteLine(a.ToString());
            Console.WriteLine(a2.ToString());
            Console.WriteLine(c.ToString());
            Console.WriteLine(f.ToString());

            Console.WriteLine(a.Equals(a2));


        }


    }
    */
}