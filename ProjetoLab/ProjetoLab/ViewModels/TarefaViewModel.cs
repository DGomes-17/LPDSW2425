using ProjetoLab.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ProjetoLab.ViewModels
{
    public class TarefaViewModel : BaseViewModel
    {
        public ObservableCollection<Tarefa> Tarefas { get; set; }
        public Tarefa TarefaSelecionada { get; set; }

        public double SomaPesos => Tarefas.Sum(t => t.Peso);

        public bool PodeGuardar => Math.Abs(SomaPesos - 100.0) < 0.01;

        public ICommand AdicionarTarefaCommand { get; }
        public ICommand RemoverTarefaCommand { get; }
        public ICommand GuardarTarefasCommand { get; }

        public TarefaViewModel()
        {
            Tarefas = new ObservableCollection<Tarefa>(
                DataService<List<Tarefa>>.Carregar("tarefas.xml") ?? new List<Tarefa>());

            AdicionarTarefaCommand = new RelayCommand(AdicionarTarefa);
            RemoverTarefaCommand = new RelayCommand(RemoverTarefa, () => TarefaSelecionada != null);
            GuardarTarefasCommand = new RelayCommand(GuardarTarefas, () => Tarefas.Any());
        }

        private void AdicionarTarefa()
        {
            var nova = new Tarefa
            {
                Id = GerarNovoId(),
                Titulo = "Nova Tarefa",
                DataHoraInicio = DateTime.Now,
                DataHoraFim = DateTime.Now.AddDays(7),
                Peso = 0
            };

            Tarefas.Add(nova);
            TarefaSelecionada = nova;

            OnPropertyChanged(nameof(TarefaSelecionada));
            OnPropertyChanged(nameof(SomaPesos));
            OnPropertyChanged(nameof(PodeGuardar));
        }

        private void RemoverTarefa()
        {
            if (TarefaSelecionada != null)
            {
                Tarefas.Remove(TarefaSelecionada);
                TarefaSelecionada = null;

                OnPropertyChanged(nameof(TarefaSelecionada));
                OnPropertyChanged(nameof(SomaPesos));
                OnPropertyChanged(nameof(PodeGuardar));
            }
        }

        private void GuardarTarefas()
        {
            DataService<List<Tarefa>>.Guardar("tarefas.xml", Tarefas.ToList());
        }

        private int GerarNovoId()
        {
            return Tarefas.Any() ? Tarefas.Max(t => t.Id) + 1 : 1;
        }
    }
}
