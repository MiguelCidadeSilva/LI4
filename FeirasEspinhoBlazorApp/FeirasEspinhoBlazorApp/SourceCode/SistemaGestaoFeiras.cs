using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Globalization;
using FeirasEspinhoBlazorApp.Data;
using FeirasEspinhoBlazorApp.SourceCode.Stands;
using FeirasEspinhoBlazorApp.SourceCode.Feiras;
using FeirasEspinhoBlazorApp.SourceCode.Vendas;
using FeirasEspinhoBlazorApp.SourceCode.Utilizadores;
using System.Linq;
using System.Collections.Generic;

namespace FeirasEspinhoBlazorApp.SourceCode
{
	public class SistemaFeiras : Exception
	{
		private UtilizadoresDAO users;
		private FeiraDAO feiras;
		private StandDAO stands;
		private VendaDAO vendas;
		private LeilaoDAO leiloes;
		private NegociacaoDAO negociacoes;
		private CategoriaDAO categorias;
		private CandidaturaDAO candidaturas;
		private int categoriaCounter;
		private int subCategoriaCounter;
		private int feirasCounter;
        private int vendasCounter;
        private int standsCounter;
		private int negociacoesCounter;
		private int leiloesCounter;
		private int produtosCounter;
		private int candidaturasCounter;
		private int notificacaoCounter;
		private static SistemaFeiras instance = new SistemaFeiras();

		public static SistemaFeiras GetInstance()
		{
			if(instance == null)
				instance= new SistemaFeiras();
			return instance;
		}


		private SistemaFeiras()
		{
			users = UtilizadoresDAO.GetInstance();
			feiras = FeiraDAO.GetInstance();
			stands = StandDAO.GetInstance();
			vendas = VendaDAO.GetInstance();
			categorias = CategoriaDAO.GetInstance();
			leiloes = LeilaoDAO.GetInstance();
			negociacoes = NegociacaoDAO.GetInstance();
			candidaturas = CandidaturaDAO.GetInstance();
			feirasCounter = feiras.GetNextId();
			vendasCounter = vendas.GetNextId();
			standsCounter = stands.GetNextIdStand();
			negociacoesCounter = negociacoes.GetNextId();
			leiloesCounter = leiloes.GetNextId();
			produtosCounter = stands.GetNextIdProduto();
			categoriaCounter = categorias.GetNextIdCategoria();
			subCategoriaCounter = categorias.GetNextSubCategoria();
			candidaturasCounter = candidaturas.GetNextId();
			notificacaoCounter = users.GetNextIdNotifCliente()+users.GetNextIdNotifFeirante();

        }


		//
		//	LOGIN: Verifica se a password � v�lida,e verifica as credenciais com a fun��o verificaCredenciais da classe Utilizador
		//
		public Utilizador Login(String email, String password)
		{
			if (password.Length < 8)
				throw new PasswordInvalidaException("Password tem de ter 8 ou mais caracteres\n");
			Utilizador? user = users[email];
			if(user == null || !user.CheckCredenciais(email, password))
				throw new EmailInvalidoException("Email n�o est� registado.");
			return user;
		}

		//		Registo: Da maneira que fiz, n�o existem emails repetidos no sistema, mesmo que sejam tipos de utilizador diferentes.
		//				 Se o email ainda nao estiver registado no sistema, adiciona ao dicionario do tipo de utilizador que queres ser ao registar-te	
		//
		public void Registo(Utilizador u)
		{
			if (u.Password.Length < 8)
				throw new PasswordInvalidaException("Password tem de conter pelo menos 8 caracteres.");
			if (users.ContainsKey(u.Email))
				throw new EmailInvalidoException("Email j� est� associado a uma conta...");
			users.Insert(u);

		}

		public Stand GetStand(int idStand)
		{
			return stands[idStand];
		}

		public Feira GetFeira(int idFeira)
		{
			return feiras[idFeira];
		}
		public List<Feira> GetFeiras() 
		{
			return feiras.ListAllFeiras().Where(f => f.DataInicio.CompareTo(DateTime.Today) <= 0 && (!f.DataFim.HasValue || f.DataFim.Value.CompareTo(DateTime.Today) >= 0)).ToList();
		}
		public float? GetAvaliacoesFeira(int feira)
		{
			return users.GetAvaliacaoFeira(feira);
		}
		public float? GetAvaliacoesStands(int stand)
		{
			Stand s = stands[stand];
			return users.GetAvaliacaoFeirante(s.EmailDono);
		}
		public void AddAvaliacaoFeira(string email, int feira, int aval)
		{
			users.AvaliaFeira(email, feira, aval);
		}
		public void AddAvaliacaoFeirante(string email, string feirante, int aval)
		{
			users.AvaliaFeirante(email, feirante, aval);
		}
		public Categoria? GetCategoria(int id)
		{
			return categorias[id];
		}

		public List<Feira> FeirasNotStarted()
		{
			return feiras.ListAllFeiras().Where(f => f.DataInicio.CompareTo(DateTime.Today) > 0).ToList();
		}
		public List<Notificacao> GetNotificacaos(string email)
		{
			Utilizador user = users[email];
			List<Notificacao> lista = new List<Notificacao>();

			if (user is Cliente)
			{
				lista = users.ListAllNotificacoesCliente(email);
				users.ClienteVeNotifs(email);
			}
			else if (user is Feirante)
			{
				lista = users.ListAllNotificacoesFeirante(email);
				users.FeiranteVeNotifs(email);
			}
			return lista;
		}

		public void AddNotificacao(string emailUser,Notificacao not)
		{
            Utilizador user = users[emailUser];
            if (user is Cliente)
            {
				users.InsertNotificacaoCliente(not);
            }
            else if (user is Feirante)
            {
				users.InsertNotificacaoFeirante(not);
            }
        }
		public Utilizador GetUtilizador(string emailUtilizador)
		{
			return users[emailUtilizador];
		}
		public List<Stand> GetStandFeirante(string feirante)
		{
			return stands.ListAllStands().Where(s => s.EmailDono.Equals(feirante)).ToList();
		}
		public List<Stand> GetStandFeirante(string feirante, int feira)
		{
			Feira f = feiras[feira];
			List<Stand> res = GetStandFeirante(feirante);
			return f.Categoria.HasValue ? res.Where(s => s.Categoria == f.Categoria.Value).ToList() : res;
		}
		public List<Feira> GetFeiranteParticipacoes(string feirante)
		{
			List<Feira> feiraList = new();
			List<Stand> list = GetStandFeirante(feirante);
			Dictionary<int, List<int>> dict = feiras.FeirasStands();
			foreach (int idfeira in dict.Keys) 
			{
				List<int> standsFeira = dict[idfeira];
				if(list.Where(s => standsFeira.Contains(s.IdStand)).Count() > 0)
					feiraList.Add(GetFeira(idfeira));
			}
			return feiraList;
		}
		public void AddLeilao(Leilao leilao)
		{
			leilao.Id = leiloesCounter;
			leiloesCounter++;
			leiloes.InsertLeilao(leilao);
			stands.DiminuiStockProduto(leilao.Produto, leilao.Quantidade);
		}

		public void AddFeira(Feira feira, string cat)
		{
			if (cat.Length > 0)
			{
				int catID = AddCategoria(cat);
				feira.Categoria = catID;
			}
			feira.IDFeira = feirasCounter;
			feirasCounter++;
			feiras.Insert(feira);
		}
		public void AprovarCandidatura(int candidatura)
		{
			Candidatura c = candidaturas.GetCandidatura(candidatura);
			int idF = c.IdFeira;
			int stand = c.IdStand;
			feiras.InsertStandParticipante(stand, idF);
			candidaturas.Aprova(c.IdCandidatura, true);
		}
		public void RemoveCandidatura(int candidatura)
		{
			candidaturas.Aprova(candidatura, false);
		}
		public List<Candidatura> GetCandidaturasAnalise(string email)
		{
			return candidaturas.ListAllCandidatura().Where(c => feiras[c.IdFeira].CriadorEmail.Equals(email) && c.Aprovacao == false).ToList();
		}
		public void CriaCandidatura (Candidatura candidatura)
		{
			candidatura.IdCandidatura = candidaturasCounter;
			candidaturasCounter++;
			candidaturas.InsertCandidatura(candidatura);
		}

		public List<Leilao> GetLeiloesFeiraStand(int idFeira, int idStand)
		{
			return leiloes.ListLeiloesFeira(idFeira)
				.Where(lei => lei.Stand == idStand)
				.Where(lei => !lei.ValormMaximo.HasValue || lei.BidAtual <= lei.ValormMaximo.Value)
				.Where(lei => lei.Date.CompareTo(DateTime.Today) >= 0)
				.ToList();
		}

		public List<Leilao> GetLeiloesFeira(int idFeira)
		{
			return leiloes.ListLeiloesFeira(idFeira)
				.Where(lei => !lei.ValormMaximo.HasValue || lei.BidAtual <= lei.ValormMaximo.Value)
				.Where(lei => lei.Date.CompareTo(DateTime.Today) >= 0)
				.ToList(); ;
		}
		// TO DO
		public List<Leilao> GetLeiloesCL(string email)
		{
			Utilizador cliente = users[email];
			//verificar
			List<Leilao> r = new();
			if(cliente is Cliente)
			{
				r = leiloes.GetLeiloesCliente(email);
			}
			else if (cliente is Feirante)
			{
				r = leiloes.ListAllLeiloes().Where(l => stands[l.Stand].EmailDono.Equals(email)).ToList();
			}
			return r;
		}

		public List<Stand> GetStandsFeira(int idFeira)
		{
			return feiras.StandsDaFeira(idFeira);
		}
		// TO DO
		public List<Venda> GetNegociacoes(string email)
		{
			Utilizador? cliente = users[email];
			//receber todas as vendas
			//for vendas verifica se � uma negocia�ao, se sim adicionar a lista
			return negociacoes.ListAllNegociacoes()
				.Where(n => !n.Sucesso)
				.Select(n => vendas.GetNegocicaoVenda(n.IdNegociacao))
				.Where(v => v.EmailCliente.Equals(email) || stands[v.IdStand].EmailDono.Equals(email)).ToList();
		}
		// TO DO
		public Negociacao GetNegociacao(int idNegociacao)
		{
            return negociacoes.Get(idNegociacao);
		}
		// TO DO
		public void RegistaSucesso(int idNegociacao)
		{
			negociacoes.AlteraSucesso(idNegociacao);
			List<Venda> listaVendas = vendas.ListAllVendas();
			Venda venda = vendas.GetNegocicaoVenda(idNegociacao);
			DateTime data = DateTime.Now;
			string emailCL = venda.EmailCliente;
			string emailFei = stands[venda.IdStand].EmailDono;
            string mensagem = string.Format("A negociacao {0} entre o cliente {1} e o feirante {2} foi bem sucedida",
                 idNegociacao, emailCL, emailFei);
            Notificacao notifCliente = new Notificacao(notificacaoCounter, emailCL, "Negociao bem sucedida", mensagem, data);
            notificacaoCounter++;
            Notificacao notifFeirante = new Notificacao(notificacaoCounter, emailFei, "Negociao bem sucedida", mensagem, data);
            notificacaoCounter++;
            users.InsertNotificacaoCliente(notifCliente);
			users.InsertNotificacaoFeirante(notifFeirante);
        }
        // TO DO
        public void RegistaInsucesso(int idNegociacao)
		{
			Venda venda = vendas.GetNegocicaoVenda(idNegociacao);
			vendas.DeleteVenda(venda.IdVenda);
			negociacoes.Insucesso(idNegociacao);
			venda.Produtos.ForEach(p => stands.AumentaStockProduto(p.Item1.IdProduto, p.Item2));
            DateTime data = DateTime.Now;
            string emailCL = venda.EmailCliente;
            string emailFei = stands[venda.IdStand].EmailDono;
            string mensagem = string.Format("A negociacao {0} entre o cliente {1} e o feirante {2} foi cancelada",
                 idNegociacao, emailCL, emailFei);
            Notificacao notifCliente = new Notificacao(notificacaoCounter, emailCL, "Negociao cancelada", mensagem, data);
            notificacaoCounter++;
            Notificacao notifFeirante = new Notificacao(notificacaoCounter, emailFei, "Negociao cancelada", mensagem, data);
            notificacaoCounter++;
            users.InsertNotificacaoCliente(notifCliente);
            users.InsertNotificacaoFeirante(notifFeirante);
        }
        // TO DO
        public void RegistaNovoPreco(int idNegociacao, float novoPreco)
		{
			negociacoes.NovaProposta(idNegociacao,novoPreco);
			Venda venda = vendas.GetNegocicaoVenda(idNegociacao);
			DateTime data = DateTime.Now;
            string emailCL = venda.EmailCliente;
            string emailFei = stands[venda.IdStand].EmailDono;
            string mensagem = string.Format("Nova proposta na negociacao ({0}) entre o cliente {1} e o feirante {2}\nPre�o: {3}",
                 idNegociacao, emailCL, emailFei,novoPreco);
            Notificacao notifCliente = new Notificacao(notificacaoCounter, emailCL, "Novo pre�o", mensagem, data);
            notificacaoCounter++;
            Notificacao notifFeirante = new Notificacao(notificacaoCounter, emailFei, "Novo pre�o", mensagem, data);
            notificacaoCounter++;
            users.InsertNotificacaoCliente(notifCliente);
            users.InsertNotificacaoFeirante(notifFeirante);
        }
		// TO DO
		public bool VendaCompativel(Venda venda)
		{
			return venda.Produtos.Where(p => p.Item1.Stock < p.Item2).Count() == 0;
		}
		public void AddVenda(Venda venda)
		{
			List<(Produto, int)> prods = venda.Produtos;
			venda.IdVenda = vendasCounter;
			vendas.Insert(venda);
			prods.ForEach(p => stands.DiminuiStockProduto(p.Item1.IdProduto, p.Item2));
			vendasCounter++;
		}
		// TO DO
		public void AddNegociacaoVenda(Venda venda, Negociacao negociacao)
		{
			venda.Negociacao = negociacoesCounter;
			negociacao.IdNegociacao = negociacoesCounter;
			negociacoes.Insert(negociacao);
			negociacoesCounter++;
			AddVenda(venda);
            DateTime data = DateTime.Now;
            string emailCL = venda.EmailCliente;
			string emailFei = stands[venda.IdStand].EmailDono;
            string mensagem = string.Format("Novo pedido de negociacao ({0}) entre o cliente {1} e o feirante {2}",
                         negociacao.IdNegociacao, emailCL, emailFei);
            Notificacao notifFeirante = new Notificacao(notificacaoCounter, emailFei, "Nova negociacao", mensagem, data);
            notificacaoCounter++;
			users.InsertNotificacaoFeirante(notifFeirante);
        }
		public Venda GetVenda(int idVenda)
		{
			return vendas[idVenda];
		}
		public Produto GetProduto(int idProduto)
		{
			return stands.GetProduto(idProduto);
		}
		public void AddProduto(Produto produto, float imposto)
		{
			Stand s = stands.GetStand(produto.Stand);
			int categoria = s.Categoria;
			int id = AddSubCategoria(categoria, imposto);
			produto.IdSubCategoria = id;
			produto.IdProduto = produtosCounter;
			produtosCounter++;
			stands.InsertProduto(produto);
		}
		public int AddSubCategoria(int categoria, float imposto)
		{
			int? id = categorias.GetSubCategoriaImposto(categoria, imposto);
			if(!id.HasValue)
			{
				Categoria cat = categorias.GetCategoria(categoria);
				id = subCategoriaCounter;
				categorias.InsertCategoria(new SubCategoria(cat.Id, cat.Name,id.Value, imposto));
				subCategoriaCounter++;
			}
			return id.Value;
		}
		public int AddCategoria(string nome)
		{
			int? id = categorias.GetIdCategoria(nome);
			if (!id.HasValue) 
			{
				id = categoriaCounter;
				categorias.InsertCategoria(new Categoria(id.Value, nome));
				categoriaCounter++;
			}
			return id.Value;
		}
		public void Licitar(int idLeilao, string email, float licitacao)
		{
			Leilao l = leiloes.GetLeilao(idLeilao);
            bool aux = false;
			if (l.ValormMaximo.HasValue && l.ValormMaximo.Value <= l.BidAtual)
				aux = true;
			if (!aux && l.BidAtual < licitacao && l.ValormMinimo <= licitacao)
			{
                string emailFei = stands[l.Stand].EmailDono;
                DateTime data = DateTime.Now;
                string mensagem = string.Format("Nova licitacao no leilao ({0}) do cliente {1}",
                         idLeilao, email);
                Notificacao notifFeirante = new Notificacao(notificacaoCounter, emailFei, "Nova licitacao", mensagem, data);
				notificacaoCounter++;
				users.InsertNotificacaoFeirante(notifFeirante);
                if (leiloes.LeilaoHasBidFromCliente(idLeilao, email))
					leiloes.UpdateBid(idLeilao, email, licitacao);
				else
					leiloes.InsertBid(idLeilao, email, licitacao);
			}
			else if (aux)
				throw new BidValueInvalid("Valor m�ximo j� foi atingido");
			else if (l.ValormMinimo > licitacao)
				throw new BidValueInvalid("Valor menor que o valor m�nimo");
			else
				throw new BidValueInvalid("Valor menor que a �ltima licita��o");
		}
		public void AddStand(Stand s, string cat, List<float> impostos)
		{
			s.IdStand = standsCounter;
			standsCounter++;
			int catID = AddCategoria(cat);
			impostos.ForEach(i => AddSubCategoria(catID, i));
			s.Categoria= catID;
			stands.InsertStand(s);
			s.Produtos.ForEach(p => { p.IdProduto = produtosCounter; produtosCounter++; });
			s.Produtos.ForEach(p => p.Stand = s.IdStand);
			s.Produtos.ForEach(p => stands.InsertProduto(p));

		}
		public void IncrementConsultantes(int stand)
		{
			stands.AumentaConsultantesNoStand(stand);
		}
		public void AumentaStock(int produto, int quantidade)
		{
			stands.AumentaStockProduto(produto, quantidade);
		}
		public void EliminaProduto(int produto)
		{
			stands.TrocaDisponibilidadeProduto(produto);
		}
		private List<Venda> GetVendas()
		{
			return vendas.ListAllVendas().Where(v => v.Negociacao.HasValue ? negociacoes.GetSucesso(v.Negociacao.Value) : true).ToList();
		}
		public List<Venda> GetVendasStand(int stand)
		{
			return GetVendas().Where(v => v.IdStand == stand).ToList();
		}
		public List<Venda> GetVendasFeira(int feira)
		{
			return GetVendas().Where(v => v.IdFeira == feira).ToList();
		}
		public List<Feira> GetFeirasAdmin(string admin)
		{
			return feiras.ListAllFeiras().Where(f => f.CriadorEmail.Equals(admin)).ToList();
		}
		public void IncrementConsultantesFeira(int feira)
		{
			feiras.AumentaConsultantesNaFeira(feira);
		}
		public class Teste
		{
			static void Main(String[] args)
			{

				Cliente c = new Cliente("Eduardo","123456789","sweeper@gmail.com", DateTime.ParseExact("4/1/2000","d/M/yyyy", null),DateTime.Now);
				Feirante f = new Feirante("Jose", "bananas123", "ze@gmail.com", DateTime.MinValue, DateTime.Now, 2);
				Administrador a = new Administrador("Maria", "whatinthefuck", "maria@gmail.com", DateTime.ParseExact("12/12/1994", "d/M/yyyy", null), DateTime.Now);
				SistemaFeiras sf = new SistemaFeiras();
				sf.Registo(f);
				sf.Registo(c);
				sf.Registo(a);
				sf.Login("sweeper@gmail.com","123456789");
				sf.Login("ze@gmail.com", "bananas123");
				sf.Login("maria@gmail.com", "whatinthefuck");

				Venda v = new Venda();
				Venda v2 = v.Clone();
				v2.Preco = 3.65f;
				v.Preco = 3.64f;
				Console.WriteLine(v.ToString());
				Console.WriteLine(v2.ToString());
				Console.WriteLine(v.Equals(v2));



            }

		}
		/*
		*/
	}
}


