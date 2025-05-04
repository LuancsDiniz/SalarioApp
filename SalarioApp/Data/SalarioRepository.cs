using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace SalarioApp.Data
{
    public class SalarioRepository
    {
        private readonly string conexao;

        public SalarioRepository(string connectionString)
        {
            conexao = connectionString;
        }

        public void AtualizarSalarios()
        {
            using (var c = new OracleConnection(conexao))
            {
                c.Open();
                var cmd = new OracleCommand("atualizar_salarios", c)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable ObterSalarios()
        {
            using (var c = new OracleConnection(conexao))
            {
                var a = new OracleDataAdapter("SELECT pessoa_nome, " +
                    "cargo_nome, " +
                    "salario " +
                    "FROM pessoa_salario " +
                    "ORDER BY pessoa_id", c);
                var d = new DataTable();
                a.Fill(d);
                return d;
            }
        }
    }
}