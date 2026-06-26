using System.Windows;
using System.Windows.Controls;
using ProjetoLab.ViewModels;

namespace ProjetoLab.Views
{
    public partial class AlunoView : UserControl
    {
        public AlunoView()
        {
            InitializeComponent();
            DataContext = new AlunoViewModel();
        }
    }
}
