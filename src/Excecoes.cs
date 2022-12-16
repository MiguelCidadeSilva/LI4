using System;

namespace FeirasEspinho
{

    public class PasswordInvalidaException : Exception
    {
        public PasswordInvalidaException() { }

        public PasswordInvalidaException(string mensagem) :base(mensagem) { }

        public PasswordInvalidaException(string message, Exception innerException   ) :base(message, innerException) { }

    }

    public class EmailInvalidoException : Exception
    {
        public EmailInvalidoException() { }

        public EmailInvalidoException(string mensagem) : base(mensagem) { }

        public EmailInvalidoException(string message, Exception innerException) : base(message, innerException) { }

    }

    public class RegistoInvalido : Exception
    {
        public RegistoInvalido() { }

        public RegistoInvalido(string mensagem) : base(mensagem) { }

        public RegistoInvalido(string message, Exception innerException) : base(message, innerException) { }

    }









}

