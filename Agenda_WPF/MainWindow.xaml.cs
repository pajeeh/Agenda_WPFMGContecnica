using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using Dapper;
using Agenda_WPF.Models;
using System.Windows.Controls;

namespace Agenda_WPF
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Contato> _contatos;
        private readonly string connectionString;
        private readonly DataModel dataModel;

        public MainWindow()
        {
            InitializeComponent();

            connectionString = ConfigurationManager.ConnectionStrings["agendaEntities"].ConnectionString;
            dataModel = new DataModel(connectionString);

        }

        private string opCrud;

        private void AtualizarDataGrid()
        {
            _contatos = dataModel.ObterContatos();
            dgContatos.ItemsSource = _contatos;
        }

        private void LimparCampos()
        {
            tNome.Text = "";
            tEmail.Text = "";
            tApelido.Text = "";
            tTelefone.Text = "";
        }

        private void BtnPesquisar_Click(object sender, RoutedEventArgs e)
        {
            string termoPesquisa = tPesquisa.Text;
            string sql = "SELECT * FROM Contatos WHERE Nome LIKE @Termo OR Email LIKE @Termo OR Apelido LIKE @Termo OR Telefone LIKE @Termo";

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    _contatos = new ObservableCollection<Contato>(connection.Query<Contato>(sql, new { Termo = "%" + termoPesquisa + "%" }));
                    dgContatos.ItemsSource = _contatos;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao pesquisar os contatos: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            opCrud = "Editar";

            if (dgContatos.SelectedItem != null)
            {
                Contato contatoSelecionado = (Contato)dgContatos.SelectedItem;
                tNome.Text = contatoSelecionado.Nome;
                tEmail.Text = contatoSelecionado.Email;
                tApelido.Text = contatoSelecionado.Apelido;
                tTelefone.Text = contatoSelecionado.Telefone;
                opCrud = "Editar";
            }
            else
            {
                MessageBox.Show("Selecione um contato para editar.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (dgContatos.SelectedItem != null)
            {
                try
                {
                    Contato contatoSelecionado = (Contato)dgContatos.SelectedItem;
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        connection.Execute("DELETE FROM Contatos WHERE id = @Id", new { contatoSelecionado.Id });
                        MessageBox.Show("Contato excluído com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        AtualizarDataGrid();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao excluir o contato: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Selecione um contato para excluir.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            opCrud = "Adicionar";
            LimparCampos();
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tNome.Text) || string.IsNullOrWhiteSpace(tEmail.Text) ||
                string.IsNullOrWhiteSpace(tApelido.Text) || string.IsNullOrWhiteSpace(tTelefone.Text))
            {
                MessageBox.Show("Por favor, preencha todos os campos antes de salvar.", "Campos obrigatórios vazios",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (opCrud == "Adicionar")
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        connection.Execute("INSERT INTO Contatos (Nome, Email, Apelido, Telefone) VALUES (@Nome, @Email, @Apelido, @Telefone)",
                            new { Nome = tNome.Text, Email = tEmail.Text, Apelido = tApelido.Text, Telefone = tTelefone.Text });
                    }
                    MessageBox.Show("Contato adicionado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (opCrud == "Editar")
                {
                    if (dgContatos.SelectedItem != null)
                    {
                        Contato contatoEditado = (Contato)dgContatos.SelectedItem;
                        using (var connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            connection.Execute("UPDATE Contatos SET Nome = @Nome, Email = @Email, Apelido = @Apelido, Telefone = @Telefone WHERE id = @Id",
                                new { Nome = tNome.Text, Email = tEmail.Text, Apelido = tApelido.Text, Telefone = tTelefone.Text, contatoEditado.Id });
                        }
                        MessageBox.Show("Contato editado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Selecione um contato para editar.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }

                LimparCampos();
                AtualizarDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao salvar o contato: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnLimpar_Click(object sender, RoutedEventArgs e)
        {
            LimparCampos();
        }

        private void TPesquisa_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Seu código para lidar com o evento TextChanged aqui
        }

    }
}
