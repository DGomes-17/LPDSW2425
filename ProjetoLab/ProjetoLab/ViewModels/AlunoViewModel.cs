using ProjetoLab.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace ProjetoLab.ViewModels
{
    public class AlunoViewModel : BaseViewModel
    {
        private string _numeroPesquisa;
        public string NumeroPesquisa
        {
            get => _numeroPesquisa;
            set
            {
                _numeroPesquisa = value;
                OnPropertyChanged();
                FiltrarAlunos();
            }
        }

        public ObservableCollection<Aluno> Alunos { get; set; }
        public ObservableCollection<Aluno> AlunosFiltrados { get; set; }
        public Aluno AlunoSelecionado { get; set; }

        public ICommand AdicionarCommand { get; }
        public ICommand RemoverCommand { get; }
        public ICommand GuardarCommand { get; }
        public ICommand ImportarAlunosCommand { get; }

        public AlunoViewModel()
        {
            Alunos = new ObservableCollection<Aluno>(
                DataService<List<Aluno>>.Carregar("alunos.xml") ?? new());
            AlunosFiltrados = new ObservableCollection<Aluno>(Alunos);

            AdicionarCommand = new RelayCommand(AdicionarAluno);
            RemoverCommand = new RelayCommand(RemoverAluno, () => AlunoSelecionado != null);
            GuardarCommand = new RelayCommand(GuardarAlunos);
            ImportarAlunosCommand = new RelayCommand(ImportarAlunos);
        }

        private void AdicionarAluno()
        {
            var novo = new Aluno { Numero = 0, Nome = "Novo Aluno", Email = "email@exemplo.com" };
            Alunos.Add(novo);
            FiltrarAlunos();
            AlunoSelecionado = novo;
            OnPropertyChanged(nameof(AlunoSelecionado));
        }

        private void RemoverAluno()
        {
            if (AlunoSelecionado != null)
                Alunos.Remove(AlunoSelecionado);
            FiltrarAlunos();
        }

        private void GuardarAlunos()
        {
            DataService<List<Aluno>>.Guardar("alunos.xml", new List<Aluno>(Alunos));
        }

        private void FiltrarAlunos()
        {
            if (string.IsNullOrWhiteSpace(NumeroPesquisa))
            {
                AlunosFiltrados = new ObservableCollection<Aluno>(Alunos);
            }
            else
            {
                AlunosFiltrados = new ObservableCollection<Aluno>(
                    Alunos.Where(a => a.Numero.ToString().Contains(NumeroPesquisa)));
            }
            OnPropertyChanged(nameof(AlunosFiltrados));
        }

        private void ImportarAlunos()
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                DefaultExt = ".csv",
                Filter = "Ficheiros CSV (*.csv)|*.csv"
            };

            if (dlg.ShowDialog() == true)
            {
                string[] linhas = File.ReadAllLines(dlg.FileName);

                foreach (var linha in linhas)
                {
                    string[] partes = linha.Split(',');
                    if (partes.Length >= 3)
                    {
                        string nome = partes[0].Trim();
                        if (int.TryParse(partes[1], out int numero))
                        {
                            string email = partes[2].Trim();

                            if (!Alunos.Any(a => a.Numero == numero))
                            {
                                Alunos.Add(new Aluno
                                {
                                    Nome = nome,
                                    Numero = numero,
                                    Email = email
                                });
                            }
                        }
                    }
                }

                GuardarAlunos();
                FiltrarAlunos();
                MessageBox.Show("Importação concluída com sucesso!", "Importar Alunos", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
