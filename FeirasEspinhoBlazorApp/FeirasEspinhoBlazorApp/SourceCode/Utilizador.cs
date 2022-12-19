

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace FeirasEspinho
{
    // SUPERCLASSE USER
    public abstract class Utilizador : Exception
    {
        private String? username;
        private String? password;
        private String? email;
        private DateTime dataNascimento;
        private DateTime dataCriacao;

        public String? Username
        {
            get { return username; }
            set { username = value; }
        }

        public String? Password
        {
            get { return password; }
            set { password = value; }
        }

        public String? Email
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
            this.Username = "";
            this.Password = "";
            this.Email = "";
            this.DataNascimento = DateTime.MinValue;
            this.DataCriacao = DateTime.MinValue;
        }

        public Utilizador(String username, String password, String email, DateTime dataNascimento, DateTime dataCriacao)
        {
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.DataNascimento = dataNascimento;
            this.DataCriacao = dataCriacao;

        }

        public Utilizador(Utilizador u)
        {
            this.Username = u.Username;
            this.Password = u.Password;
            this.Email = u.Email;
            this.DataNascimento = u.DataNascimento;
            this.DataCriacao = u.DataCriacao;
        }

        public override String ToString()
        {
            return ("User: " + this.Username + "\nPassword: " + this.Password + "\nEmail: " + this.Email + "\nDataNascimento: "
                    + this.DataNascimento.ToString("dd/MM/yyyy") + "\n"
                    + "DataCriacao: " + this.DataCriacao.ToString("dd/MM/yyyy") + "\n");
        }

        public override bool Equals(Object? obj)
        {
            if (this == obj) return true;
            if (obj == null || (obj is not Utilizador)) return false;
            Utilizador u = (Utilizador)obj;
            return (this.Username.Equals(u.Username) &&
                       this.Password.Equals(u.Password) &&
                       this.Email.Equals(u.Email) &&
                       this.DataNascimento.Equals(u.DataNascimento) &&
                       this.DataCriacao.Equals(u.DataCriacao));
        }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        public override int GetHashCode() => (Username, Password, Email, DataNascimento,DataCriacao).GetHashCode();



        // verificacao de credenciais: 1) caso (email inserido no menu de login) != (email da classe utilizador em que a funcao é invocada) retorna false                                                                                                                              
        //                             2) caso os usernames sejam iguais, retorna true se a password for igual     
        //                             3) se os users condizem mas a password seja errada, retorna uma excecao, a dizer que a password está errada
        public virtual bool CheckCredenciais(String email, String password)
        {

            if (!(this.Email.Equals(email))) return false;

            if(this.Email.Equals(email) && this.Password.Equals(password)) return true;

            throw new PasswordInvalidaException("passwords nao condizem...");
            
        }


    }


    // ======SUBCLASSE ADMIN======


    public class Administrador : Utilizador
    {
        public Administrador() : base()
        {
        }

        public Administrador(String username, String password, String email, DateTime dataNascimento, DateTime dataCriacao) : base(username, password, email, dataNascimento,dataCriacao)
        {
        }

        public Administrador(Administrador u) : base(u.Username, u.Password, u.Email, u.DataNascimento,u.DataCriacao)
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

        public override int GetHashCode() => (Username, Password, Email, DataNascimento,DataCriacao).GetHashCode();
    }


    //=====SUBCLASSE CLIENTE=====


    public class Cliente : Utilizador
    {
        public Cliente() : base()
        {
        }

        public Cliente(String username, String password, String email, DateTime dataNascimento, DateTime dataCriacao) : base(username, password, email, dataNascimento, dataCriacao)
        {
        }

        public Cliente(Cliente u) : base(u.Username, u.Password, u.Email, u.DataNascimento,u.DataCriacao)
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

        public override int GetHashCode() => (Username, Password, Email, DataNascimento,DataCriacao).GetHashCode();

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
            this.IDconta = 0;
        }

        public Feirante(String username, String password, String email, DateTime dataNascimento,DateTime dataCriacao, int IDconta) : base(username, password, email, dataNascimento,dataCriacao)
        {
            this.IDconta = IDconta;
        }

        public Feirante(Feirante u) : base(u.Username, u.Password, u.Email, u.DataNascimento,u.DataCriacao)
        {
            this.IDconta = u.IDconta;
        }

        public override Feirante Clone()
        {
            return new Feirante(this);
        }

        public override String ToString()
        {
            return ("FEIRANTE\n" + base.ToString() + "Numero da conta: " + this.IDconta + "\n");
        }

        public override Boolean Equals(Object? obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;

            Feirante f = (Feirante)obj;
            return ( base.Equals(obj) && (this.IDconta == f.IDconta) );
        }

        public override int GetHashCode() => (Username, Password, Email, DataNascimento,DataCriacao,IDconta).GetHashCode();


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