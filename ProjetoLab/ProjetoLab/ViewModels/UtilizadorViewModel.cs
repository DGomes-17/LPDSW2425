using ProjetoLab.Models;
using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Win32;
using System.IO;

namespace ProjetoLab.ViewModels
{
    public class UtilizadorViewModel : BaseViewModel
    {
        private Utilizador _utilizador;

        public string Nome
        {
            get => _utilizador.Nome;
            set { _utilizador.Nome = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get => _utilizador.Email;
            set { _utilizador.Email = value; OnPropertyChanged(); }
        }

        public string Funcao
        {
            get => _utilizador.Funcao;
            set { _utilizador.Funcao = value; OnPropertyChanged(); }
        }

        public string CaminhoFoto
        {
            get => _utilizador.CaminhoFoto;
            set { _utilizador.CaminhoFoto = value; OnPropertyChanged(); }
        }

        public List<string> Funcoes { get; } = new() { "Aluno", "Professor" };

        public ICommand GuardarCommand { get; }
        public ICommand SelecionarFotoCommand { get; }

        public UtilizadorViewModel()
        {
            _utilizador = DataService<Utilizador>.Carregar("utilizador.xml") ?? new Utilizador();

            GuardarCommand = new RelayCommand(Guardar);
            SelecionarFotoCommand = new RelayCommand(SelecionarFoto);
        }

        private void Guardar()
        {
            DataService<Utilizador>.Guardar("utilizador.xml", _utilizador);
        }

        private void SelecionarFoto()
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Imagens (*.jpg;*.png)|*.jpg;*.png"
            };

            if (dlg.ShowDialog() == true)
            {
                CaminhoFoto = dlg.FileName;
            }
        }
    }
}
