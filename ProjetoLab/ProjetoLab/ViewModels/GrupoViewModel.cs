using ProjetoLab.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Collections.Generic;

namespace ProjetoLab.ViewModels
{
    public class GrupoViewModel : BaseViewModel
    {
        public ObservableCollection<Grupo> Grupos { get; set; }
        public ObservableCollection<Aluno> AlunosDisponiveis { get; set; }

        private Grupo _grupoSelecionado;
        public Grupo GrupoSelecionado
        {
            get => _grupoSelecionado;
            set
            {
                _grupoSelecionado = value;
                OnPropertyChanged();
                AtualizarAlunosNoGrupo();
            }
        }

        private ObservableCollection<Aluno> _alunosNoGrupo = new();
        public ObservableCollection<Aluno> AlunosNoGrupo
        {
            get => _alunosNoGrupo;
            set
            {
                _alunosNoGrupo = value;
                OnPropertyChanged();
            }
        }

        private Aluno _alunoSelecionadoParaAdicionar;
        public Aluno AlunoSelecionadoParaAdicionar
        {
            get => _alunoSelecionadoParaAdicionar;
            set { _alunoSelecionadoParaAdicionar = value; OnPropertyChanged(); }
        }

        private Aluno _alunoSelecionadoParaRemover;
        public Aluno AlunoSelecionadoParaRemover
        {
            get => _alunoSelecionadoParaRemover;
            set { _alunoSelecionadoParaRemover = value; OnPropertyChanged(); }
        }

        private string _numeroPesquisa;
        public string NumeroPesquisa
        {
            get => _numeroPesquisa;
            set
            {
                _numeroPesquisa = value;
                OnPropertyChanged();
                FiltrarAlunoPorNumero();
            }
        }

        public ICommand AdicionarGrupoCommand { get; }
        public ICommand RemoverGrupoCommand { get; }
        public ICommand GuardarGruposCommand { get; }
        public ICommand AdicionarAlunoAoGrupoCommand { get; }
        public ICommand RemoverAlunoDoGrupoCommand { get; }

        public GrupoViewModel()
        {
            Grupos = new ObservableCollection<Grupo>(
                DataService<List<Grupo>>.Carregar("grupos.xml") ?? new());

            AlunosDisponiveis = new ObservableCollection<Aluno>(
                DataService<List<Aluno>>.Carregar("alunos.xml") ?? new());

            AdicionarGrupoCommand = new RelayCommand(AdicionarGrupo);
            RemoverGrupoCommand = new RelayCommand(RemoverGrupo);
            GuardarGruposCommand = new RelayCommand(GuardarGrupos);
            AdicionarAlunoAoGrupoCommand = new RelayCommand(AdicionarAlunoAoGrupo);
            RemoverAlunoDoGrupoCommand = new RelayCommand(RemoverAlunoDoGrupo);
        }

        private void AdicionarGrupo()
        {
            var novo = new Grupo
            {
                Id = Grupos.Count > 0 ? Grupos.Max(g => g.Id) + 1 : 1,
                Nome = "Novo Grupo",
                Alunos = new List<Aluno>()
            };
            Grupos.Add(novo);
            GrupoSelecionado = novo;
        }

        private void RemoverGrupo()
        {
            if (GrupoSelecionado != null)
            {
                Grupos.Remove(GrupoSelecionado);
                GrupoSelecionado = null;
                AtualizarAlunosNoGrupo();
            }
        }

        private void GuardarGrupos()
        {
            DataService<List<Grupo>>.Guardar("grupos.xml", Grupos.ToList());
        }

        private void AdicionarAlunoAoGrupo()
        {
            if (GrupoSelecionado == null || AlunoSelecionadoParaAdicionar == null)
                return;

            var existe = GrupoSelecionado.Alunos.Any(a => a.Numero == AlunoSelecionadoParaAdicionar.Numero);

            if (!existe)
            {
                GrupoSelecionado.Alunos.Add(AlunoSelecionadoParaAdicionar);
                AtualizarAlunosNoGrupo();
                AlunoSelecionadoParaAdicionar = null;
                NumeroPesquisa = string.Empty;
            }
        }

        private void RemoverAlunoDoGrupo()
        {
            if (GrupoSelecionado == null || AlunoSelecionadoParaRemover == null)
                return;

            var alunoARemover = GrupoSelecionado.Alunos.FirstOrDefault(a => a.Numero == AlunoSelecionadoParaRemover.Numero);
            if (alunoARemover != null)
            {
                GrupoSelecionado.Alunos.Remove(alunoARemover);
                AtualizarAlunosNoGrupo();
                AlunoSelecionadoParaRemover = null;
            }
        }

        private void AtualizarAlunosNoGrupo()
        {
            AlunosNoGrupo = GrupoSelecionado != null
                ? new ObservableCollection<Aluno>(GrupoSelecionado.Alunos)
                : new ObservableCollection<Aluno>();
        }

        private void FiltrarAlunoPorNumero()
        {
            if (int.TryParse(NumeroPesquisa, out int numero))
            {   
                AlunoSelecionadoParaAdicionar = AlunosDisponiveis.FirstOrDefault(a => a.Numero == numero);
            }
            else
            {
                AlunoSelecionadoParaAdicionar = null;
            }
        }
    }
}
