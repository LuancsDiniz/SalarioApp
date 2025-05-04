using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace SalarioApp.Data
{
    public class SalarioRepository
    {
        private readonly string conexao;

        // Construtor recebe a string de conexão
        public SalarioRepository(string connectionString)
        {
            conexao = connectionString;
        }

        // Método que chama a procedure para atualizar os salários
        public void AtualizarSalarios()
        {
            using (var c = new OracleConnection(conexao))
            {
                c.Open();
                var comandoProcedure = new OracleCommand("atualizar_salarios", c)
                {
                    CommandType = CommandType.StoredProcedure // Define que é uma procedure
                };
                comandoProcedure.ExecuteNonQuery(); // Executa sem retorno
            }
        }

        // Método que consulta os salários para exibição
        public DataTable ObterSalarios()
        {
            using (var c = new OracleConnection(conexao))
            {
                var a = new OracleDataAdapter("SELECT pessoa_nome, cargo_nome, salario FROM pessoa_salario ORDER BY pessoa_id", c);
                var d = new DataTable();
                a.Fill(d); // Preenche o DataTable com os dados do banco
                return d;
            }
        }
    }
}
