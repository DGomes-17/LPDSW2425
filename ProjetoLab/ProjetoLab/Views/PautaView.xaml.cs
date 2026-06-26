using System.Windows;
using System.Windows.Controls;
using ProjetoLab.ViewModels;

namespace ProjetoLab.Views
{
    public partial class PautaView : UserControl
    {
        public PautaView()
        {
            InitializeComponent();
            DataContext = new PautaViewModel();
        }

        private void VerHistograma_Click(object sender, RoutedEventArgs e)
        {
            var histogramaView = new HistogramaView();
            histogramaView.ShowDialog();
        }
    }
}