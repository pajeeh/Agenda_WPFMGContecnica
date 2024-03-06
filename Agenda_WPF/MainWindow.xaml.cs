using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Agenda_WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            contexto = new agendaEntities();
            AtualizarDataGrid();
        }

        private readonly agendaEntities contexto;
        private string opCrud;

        private void AtualizarDataGrid()
        {
            dgContatos.ItemsSource = contexto.contatos.ToList();
        }

        private void ListarContatos()
        {
            try
            {
                dgContatos.ItemsSource = contexto.contatos.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao listar os contatos: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
            try
            {
                if (!string.IsNullOrWhiteSpace(termoPesquisa))
                {
                    dgContatos.ItemsSource = contexto.contatos.Where(c => c.nome.Contains(termoPesquisa) || c.email.Contains(termoPesquisa) || c.apelido.Contains(termoPesquisa) || c.telefone.Contains(termoPesquisa)).ToList();
                }
                else
                {
                    ListarContatos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao pesquisar os contatos: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Variável para armazenar o ID do contato selecionado para edição
        private int contatoEditadoId;

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            opCrud = "Editar";

            // Verifica se um contato está selecionado no DataGrid
            if (dgContatos.SelectedItem != null)
            {
                // Obtém o contato selecionado no DataGrid
                contato contatoSelecionado = (contato)dgContatos.SelectedItem;
                // Preenche os campos do formulário com os dados do contato selecionado
                tNome.Text = contatoSelecionado.nome;
                tEmail.Text = contatoSelecionado.email;
                tApelido.Text = contatoSelecionado.apelido;
                tTelefone.Text = contatoSelecionado.telefone;
                // Armazena o ID do contato selecionado
                contatoEditadoId = contatoSelecionado.id;
                // Define a operação como "Editar"
                opCrud = "Editar";
            }
            else
            {
                MessageBox.Show("Selecione um contato para editar.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnExcluir_Click(object sender, RoutedEventArgs e)
        {
            // Verifica se um contato está selecionado no DataGrid
            if (dgContatos.SelectedItem != null)
            {
                try
                {
                    // Obtém o contato selecionado no DataGrid
                    contato contatoSelecionado = (contato)dgContatos.SelectedItem;
                    // Remove o contato do contexto do banco de dados
                    contexto.contatos.Remove(contatoSelecionado);
                    // Salva as alterações no banco de dados
                    contexto.SaveChanges();
                    MessageBox.Show("Contato excluído com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Atualiza o DataGrid com os contatos atualizados do banco de dados
                    AtualizarDataGrid();
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

            // Limpa os campos de texto
            LimparCampos();

            // Define a operação como "Adicionar"
            opCrud = "Adicionar";
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            // Verifica se os campos obrigatórios estão preenchidos
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
                    // Adicionar um novo contato
                    contato c = new contato();
                    c.nome = tNome.Text;
                    c.email = tEmail.Text;
                    c.apelido = tApelido.Text;
                    c.telefone = tTelefone.Text;
                    contexto.contatos.Add(c);
                    MessageBox.Show("Contato adicionado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (opCrud == "Editar")
                {
                    // Encontrar o contato existente pelo ID
                    var contatoEditado = contexto.contatos.FirstOrDefault(c => c.id == contatoEditadoId);
                    if (contatoEditado != null)
                    {
                        // Atualizar os dados do contato existente com os dados do formulário
                        contatoEditado.nome = tNome.Text;
                        contatoEditado.email = tEmail.Text;
                        contatoEditado.apelido = tApelido.Text;
                        contatoEditado.telefone = tTelefone.Text;
                        MessageBox.Show("Contato editado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }

                // Salvar as alterações no banco de dados
                contexto.SaveChanges();

                // Limpa os campos do formulário
                LimparCampos();

                // Atualiza o DataGrid com os contatos atualizados do banco de dados
                AtualizarDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao salvar o contato: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnLimpar_Click(object sender, RoutedEventArgs e)
        {
            // Limpa os campos do formulário
            LimparCampos();
        }

        private void tPesquisa_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
