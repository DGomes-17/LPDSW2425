using ProjetoLab.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjetoLab.Views
{
    
    public partial class TarefaView : UserControl
    {
        public TarefaView()
        {
            InitializeComponent();
            DataContext = new TarefaViewModel(); // Conectar a View ao ViewModel
        }
    }
}