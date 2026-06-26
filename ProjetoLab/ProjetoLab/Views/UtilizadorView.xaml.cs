using System.Windows;
using System.Windows.Controls;
using ProjetoLab.ViewModels;

namespace ProjetoLab.Views
{
    public partial class UtilizadorView : UserControl
    {
        public UtilizadorView()
        {
            InitializeComponent();
            DataContext = new UtilizadorViewModel();
        }

        
    }
}
