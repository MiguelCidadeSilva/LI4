using static FeirasEspinhoBlazorApp.Shared.NavMenu;

namespace FeirasEspinhoBlazorApp.SourceCode.WebClasses
{
    public class NavBar
    {
        private Dictionary<int, List<Opcao>> menus = new Dictionary<int, List<Opcao>>();
        public enum menusNomes { Cliente, Admin, Feirante, Login, Geral };
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
                new Opcao("Consultar Feiras", "oi oi-dashboard", ""),
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
            this.menus[ad] = new List<Opcao>();
        }
        private void InicializeGeralNav()
        {
            int cl = (int)menusNomes.Cliente;
            int ad = (int)menusNomes.Admin;
            int fe = (int)menusNomes.Feirante;
            int ge = (int)menusNomes.Geral;
            this.menus[ge] = new List<Opcao>();
            this.menus[ge].ForEach(op =>
            {
                this.menus[cl].Add(op);
                this.menus[ad].Add(op);
                this.menus[fe].Add(op);
            });
        }
        public List<Opcao> getMenu(int menuApresentar)
        {
            return this.menus[menuApresentar];
        }
        public NavBar()
        {
            this.InicializeLoginNav();
            this.InicializeClientNav();
            this.InicializeFeiranteNav();
            this.InicializeAdminNav();
            this.InicializeGeralNav();
        }
    }
}
