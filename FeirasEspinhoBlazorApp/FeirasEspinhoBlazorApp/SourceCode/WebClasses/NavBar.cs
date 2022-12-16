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
            this.menus[lo] = new List<Opcao>
            {
                new Opcao("Início", "oi oi-home", ""),
                new Opcao("Login", "oi oi-account-login", "login"),
                new Opcao("Criar Conta", "oi oi-person", "createacc")
            };
        }
        private void InicializeClientNav()
        {
            int cl = (int)menusNomes.Cliente;
            this.menus[cl] = new List<Opcao>
            {
                new Opcao("Consultar Feiras", "oi oi-list", ""),
                new Opcao("Leilões", "oi oi-euro", ""),
                new Opcao("Negociações", "oi oi-transfer", "")
            };
        }

        private void InicializeFeiranteNav()
        {
            int fe = (int)menusNomes.Feirante;
            this.menus[fe] = new List<Opcao>();
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
                new Opcao("Logout", "oi oi-account-logout", "login")
            };
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
