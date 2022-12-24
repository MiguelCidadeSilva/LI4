using FeirasEspinhoBlazorApp.SourceCode.Feiras;
using FeirasEspinhoBlazorApp.SourceCode.Stands;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace FeirasEspinhoBlazorApp.SourceCode
{
    public class Notificacao
    {
        private int id;
        private string? email;
        private string? assunto;
        private string? mensagem;
        private bool vista;
        private DateTime data;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Assunto
        {
            get { return assunto; }
            set { assunto = value; }
        }
        public string Mensagem
        {
            get { return mensagem; }
            set { mensagem = value; }
        }
        public bool Vista
        {
            get { return vista; }
            set { vista = value; }
        }
        public DateTime Data
        {
            get { return data; }
            set { data = value; }
        }
        public Notificacao() { }
        public Notificacao(int id, string email, string assunto, string mensagem, bool vista, DateTime data)
        {
            Id = id;
            Email = email;
            Assunto = assunto;
            Mensagem = mensagem;
            Vista = vista;
            Data = data;
        }
        public Notificacao(int id, string email, string assunto, string mensagem, DateTime data)
        {
            Id = id;
            Email = email;
            Assunto = assunto;
            Mensagem = mensagem;
            Vista = false;
            Data = data;
        }
        public Notificacao(Notificacao notificacao)
        {
            Id= notificacao.Id;
            Email=notificacao.Email;
            Assunto=notificacao.Assunto;
            Mensagem=notificacao.Mensagem;
            Vista=notificacao.Vista;
            Data = notificacao.Data;
        }
    }
}
