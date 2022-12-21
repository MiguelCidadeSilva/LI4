using static System.Net.Mime.MediaTypeNames;
using System.Data;
using Microsoft.AspNetCore.Http;
using FeirasEspinhoBlazorApp.SourceCode.Feiras;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace FeirasEspinhoBlazorApp.Data
{
    public class FeiraDAO
    {
        private static FeiraDAO instance = new FeiraDAO();

        public static FeiraDAO GetInstance()
        {
            return instance;
        }

        public void Create(Feira feira)
        {

        }
    }
}