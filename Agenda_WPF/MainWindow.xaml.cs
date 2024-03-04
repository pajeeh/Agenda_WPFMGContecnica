using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Agenda_WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
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
                using (agendaEntities ctx = new agendaEntities())
                {
                    contato c = new contato();
                    c.nome = tNome.Text;
                    c.email = tEmail.Text;
                    c.apelido = tApelido.Text;
                    c.telefone = tTelefone.Text;

                    ctx.contatos.Add(c);
                    ctx.SaveChanges();

                    MessageBox.Show("Contato salvo com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao salvar o contato: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para adicionar um novo contato
            MessageBox.Show("Botão Adicionar Novo Contato clicado!");
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para editar um contato
            MessageBox.Show("Botão Editar clicado!");
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para excluir um contato
            MessageBox.Show("Botão Excluir clicado!");
        }
    }
}