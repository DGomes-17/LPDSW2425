using System.Windows;
using ProjetoLab.Views;

namespace ProjetoLab
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

     
        private void AbrirUtilizador_Click(object sender, RoutedEventArgs e)
        {
            ConteudoPrincipal.Content = new UtilizadorView();
        }

        private void AbrirAlunos_Click(object sender, RoutedEventArgs e)
        {
            ConteudoPrincipal.Content = new AlunoView();
        }

        private void AbrirGrupos_Click(object sender, RoutedEventArgs e)
        {
            ConteudoPrincipal.Content = new GrupoView();
        }

        private void AbrirTarefas_Click(object sender, RoutedEventArgs e)
        {
            ConteudoPrincipal.Content = new TarefaView();
        }

        private void AbrirAvaliacoes_Click(object sender, RoutedEventArgs e)
        {
            ConteudoPrincipal.Content = new AvaliacaoView();
        }

        private void AbrirPauta_Click(object sender, RoutedEventArgs e)
        {
            ConteudoPrincipal.Content = new PautaView();
        }

        private void AbrirHistograma_Click(object sender, RoutedEventArgs e)
        {
            ConteudoPrincipal.Content = new Views.HistogramaView();
        }

    }
}
