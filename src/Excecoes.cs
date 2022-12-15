using System;

namespace FeirasEspinho
{

    public class PasswordInvalidaException : Exception
    {
        public PasswordInvalidaException() { }

        public PasswordInvalidaException(string mensagem) :base(mensagem) { }

        public PasswordInvalidaException(string message, Exception innerException   ) :base(message, innerException) { }

    }

    public class UsernameInvalidoException : Exception
    {
        public UsernameInvalidoException() { }

        public UsernameInvalidoException(string mensagem) : base(mensagem) { }

        public UsernameInvalidoException(string message, Exception innerException) : base(message, innerException) { }

    }





}

