namespace FeirasEspinhoBlazorApp.SourceCode.WebClasses
{
    public class NavBar
    {
        private Dictionary<int, List<Opcao>> menus = new Dictionary<int, List<Opcao>>();
        public enum menusNomes { Cliente, Admin, Feirante, Login, Geral };
        private static NavBar instance = null;
        private void InicializeLoginNav()
        {
            int lo = (int)menusNomes.Login;
            this.menus[lo] = new()
			{
                new Opcao("Início", "oi oi-home", ""),
                new Opcao("Login", "oi oi-account-login", "login"),
                new Opcao("Criar Conta", "oi oi-plus", "createacc")
            };
        }
        private void InicializeClientNav()
		{
			int cl = (int)menusNomes.Cliente;
            this.menus[cl] = new();
        }
        public void LoginCL(string email)
        {
            int cl = (int)menusNomes.Cliente;
            this.menus[cl] = new()
			{
				new Opcao("Página inicial", "oi oi-home", "user/"  +email),
				new Opcao("Consultar Feiras", "oi oi-list", "showfeiras/" + email),
                new Opcao("Leilões", "oi oi-euro", "showleiloesuser/" + email),
                new Opcao("Negociações", "oi oi-transfer", "shownegociacoes/" + email),
                new Opcao("Notificações", "oi oi-bell", "notifications/" + email)
            };
		}
        public void Logout()
        {
			int cl = (int)menusNomes.Cliente;
			int fe = (int)menusNomes.Feirante;
			int ad = (int)menusNomes.Admin;
            this.menus[cl].Clear();
			this.menus[fe].Clear();
			this.menus[ad].Clear();
		}

        private void InicializeFeiranteNav()
        {
            int fe = (int)menusNomes.Feirante;
            this.menus[fe] = new();
		}
		public void LoginFeirante(string email)
		{
			int fe = (int)menusNomes.Feirante;
			this.menus[fe] = new()
			{
				new Opcao("Página inicial", "oi oi-home", "user/" +email ),
				new Opcao("Os meus stands","oi oi-home","showstandsfeirante/" + email),
				new Opcao("Inscrição numa feira", "oi oi-list", "showfeiras/" + email),
				new Opcao("Leilões", "oi oi-euro", "showleiloesuser/" + email),
				new Opcao("Negociações", "oi oi-transfer", "shownegociacoes/" + email),
				new Opcao("Notificações", "oi oi-bell", "notifications/" + email)
			};
		}
		private void InicializeAdminNav()
        {
            int ad = (int)menusNomes.Admin;
            menus[ad] = new List<Opcao>();
        }
        private void InicializeGeralNav()
        {
            int ge = (int)menusNomes.Geral;
            this.menus[ge] = new List<Opcao>
			{

			};
            int cl = (int)menusNomes.Cliente;
            int ad = (int)menusNomes.Admin;
            int fe = (int)menusNomes.Feirante;
            menus[cl].AddRange(menus[ge]);
            menus[ad].AddRange(menus[ge]);
            menus[fe].AddRange(menus[ge]);
        }
        public List<Opcao> GetMenu(int menuApresentar)
        {
            return this.menus[menuApresentar];
        }
        private NavBar()
        {
            this.InicializeLoginNav();
            this.InicializeClientNav();
            this.InicializeFeiranteNav();
            this.InicializeAdminNav();
            this.InicializeGeralNav();
        }
        public static NavBar GetInstance()
        {
            if (instance == null)
                instance = new NavBar();
            return instance;
        }
    }
}
