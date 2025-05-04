using System;
using System.Configuration;
using SalarioApp.Data;

namespace SalarioApp.Application
{
    public partial class Salarios : System.Web.UI.Page
    {
        // Busca a string de conexão do Web.config
        private string conexao => ConfigurationManager.ConnectionStrings["MinhaConexao"].ConnectionString;

        private SalarioRepository repository;

        // Método executado sempre que a página carregar 
        protected void Page_Load(object sender, EventArgs e)
        {
            // Cria o repositório com a string de conexão
            repository = new SalarioRepository(conexao);

            // Carrega dados apenas na primeira vez (evita recarregar após postback)
            if (!IsPostBack)
                LoadData();
        }

        // Método executado quando o botão "Calcular" é clicado
        protected void Calc_Click(object sender, EventArgs e)
        {
            try
            {
                // Executa a procedure que atualiza os salários no banco
                repository.AtualizarSalarios();
                msg.Text = "Salários atualizados.";
            }
            catch (Exception ex)
            {
                // Exibe erro se algo falhar
                msg.Text = ex.Message;
            }

            // Atualiza a tabela com os novos dados
            LoadData();
        }

        // Carrega os dados de salários para exibição no GridView
        private void LoadData()
        {
            try
            {
                var dados = repository.ObterSalarios();
                grid.DataSource = dados;
                grid.DataBind();
            }
            catch (Exception ex)
            {
         
                msg.Text = ex.Message;
            }
        }
    }
}
