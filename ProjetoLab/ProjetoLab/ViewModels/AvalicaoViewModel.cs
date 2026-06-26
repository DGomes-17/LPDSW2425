    using ProjetoLab.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ProjetoLab.ViewModels;
using System.Windows;

namespace ProjetoLab.ViewModels
{
    public class AvaliacaoViewModel : BaseViewModel
    {
        public ObservableCollection<Avaliacao> Avaliacoes { get; set; }
        public Avaliacao AvaliacaoSelecionada { get; set; }

        public ObservableCollection<Grupo> Grupos { get; set; }
        public ObservableCollection<Tarefa> Tarefas { get; set; }
        public Grupo GrupoSelecionado { get; set; }
        public Tarefa TarefaSelecionada { get; set; }
        public double Nota { get; set; }

        public ICommand AdicionarAvaliacaoCommand { get; }
        public ICommand RemoverAvaliacaoCommand { get; }
        public ICommand GuardarAvaliacoesCommand { get; }

        public AvaliacaoViewModel()
        {
            var avaliacoesCarregadas = DataService<List<Avaliacao>>.Carregar("avaliacoes.xml") ?? new();

            // Forçar que o grupo e a lista de alunos não sejam null
            foreach (var av in avaliacoesCarregadas)
            {
                if (av.Grupo == null)
                    av.Grupo = new Grupo();

                if (av.Grupo.Alunos == null)
                    av.Grupo.Alunos = new List<Aluno>();

                if (av.ExcecoesIndividuais == null)
                    av.ExcecoesIndividuais = new Dictionary<int, double>();
            }

            Avaliacoes = new ObservableCollection<Avaliacao>(
                DataService<List<Avaliacao>>.Carregar("avaliacoes.xml") ?? new List<Avaliacao>());


            Grupos = new ObservableCollection<Grupo>(
                DataService<List<Grupo>>.Carregar("grupos.xml") ?? new());

            Tarefas = new ObservableCollection<Tarefa>(
                DataService<List<Tarefa>>.Carregar("tarefas.xml") ?? new());

            AdicionarAvaliacaoCommand = new RelayCommand(AdicionarAvaliacao);
            RemoverAvaliacaoCommand = new RelayCommand(RemoverAvaliacao, () => AvaliacaoSelecionada != null);
            GuardarAvaliacoesCommand = new RelayCommand(GuardarAvaliacoes);
        }

        private void AdicionarAvaliacao()
        {
            if (GrupoSelecionado != null && TarefaSelecionada != null)
            {
                var grupoCompleto = new Grupo
                {
                    Id = GrupoSelecionado.Id,
                    Nome = GrupoSelecionado.Nome,
                    Alunos = GrupoSelecionado.Alunos.ToList()
                };

                var nova = new Avaliacao
                {
                    Grupo = grupoCompleto,
                    Tarefa = TarefaSelecionada,
                    Nota = Nota,
                    ExcecoesIndividuais = new Dictionary<int, double>()
                };

                Avaliacoes.Add(nova);
            }
        }

        




        private void RemoverAvaliacao()
        {
            if (AvaliacaoSelecionada != null)
                Avaliacoes.Remove(AvaliacaoSelecionada);
        }

        public void GuardarAvaliacoes()
        {

            MessageBox.Show($"Guardadas {Avaliacoes.Count} avaliações.");

            DataService<List<Avaliacao>>.Guardar("avaliacoes.xml", Avaliacoes.ToList());

            MessageBox.Show("Avaliações guardadas com sucesso!");
        }

    }


}
