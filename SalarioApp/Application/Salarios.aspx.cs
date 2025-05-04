using System;
using System.Configuration;
using SalarioApp.Data;

namespace SalarioApp.Application
{
    public partial class Salarios : System.Web.UI.Page
    {
        private string conexao => ConfigurationManager.ConnectionStrings["MinhaConexao"].ConnectionString;
        private SalarioRepository repository;

        protected void Page_Load(object sender, EventArgs e)
        {
            repository = new SalarioRepository(conexao);

            if (!IsPostBack)
                LoadData();
        }

        protected void Calc_Click(object sender, EventArgs e)
        {
            try
            {
                repository.AtualizarSalarios();
                msg.Text = "Salários atualizados.";
            }
            catch (Exception ex)
            {
                msg.Text = ex.Message;
            }

            LoadData();
        }

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
