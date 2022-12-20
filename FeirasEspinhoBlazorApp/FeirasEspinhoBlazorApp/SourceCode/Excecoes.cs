using System;

namespace FeirasEspinhoBlazorApp.SourceCode
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

    public class RegistoInvalidoException : Exception
    {
        public RegistoInvalidoException() { }

        public RegistoInvalidoException(string mensagem) : base(mensagem) { }

        public RegistoInvalidoException(string message, Exception innerException) : base(message, innerException) { }

    }

    public class PermissaoInvalidaException : Exception
    {
        public PermissaoInvalidaException() { }

        public PermissaoInvalidaException(string mensagem) : base(mensagem) { }

        public PermissaoInvalidaException(string message, Exception innerException) : base(message, innerException) { }

    }









}

