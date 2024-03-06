using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using Dapper;

namespace Agenda_WPF.Models
{
    public class DataModel
    {
        private readonly string connectionString;

        public DataModel(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public ObservableCollection<Contato> ObterContatos()
        {
            ObservableCollection<Contato> contatos = new ObservableCollection<Contato>();

            string sql = "SELECT * FROM Contatos";

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    contatos = new ObservableCollection<Contato>(connection.Query<Contato>(sql));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ocorreu um erro ao obter os contatos: " + ex.Message);
                }
            }

            return contatos;
        }

        // Outros métodos aqui, como adicionar, atualizar ou excluir contatos
    }

    public class Contato
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Apelido { get; set; }
        public string Telefone { get; set; }
    }
}
