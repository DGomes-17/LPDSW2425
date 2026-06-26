using System.Windows.Controls;
using ProjetoLab.ViewModels;

namespace ProjetoLab.Views
{
    public partial class AvaliacaoView : UserControl
    {
        public AvaliacaoView()
        {
            InitializeComponent();
            DataContext = new AvaliacaoViewModel();

            this.Unloaded += AvaliacaoView_Unloaded;
        }

        private void AvaliacaoView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is AvaliacaoViewModel vm && vm.Avaliacoes.Any())
            {
                vm.GuardarAvaliacoes();
            }
        }
    }
}
