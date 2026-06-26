using System.Windows.Controls;
using ProjetoLab.ViewModels;

namespace ProjetoLab.Views
{
    public partial class GrupoView : UserControl
    {
        public GrupoView()
        {
            InitializeComponent();
            DataContext = new GrupoViewModel();
        }
    }
}
