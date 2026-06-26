using ProjetoLab.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProjetoLab.ViewModels
{
    public class PautaViewModel : BaseViewModel
    {
        public ObservableCollection<AlunoNotaFinal> PautaFinal { get; set; }

        public PautaViewModel()
        {
            var alunos = DataService<List<Aluno>>.Carregar("alunos.xml") ?? new();
            var grupos = DataService<List<Grupo>>.Carregar("grupos.xml") ?? new();
            var avaliacoes = DataService<List<Avaliacao>>.Carregar("avaliacoes.xml") ?? new();

            var pauta = new List<AlunoNotaFinal>();

            foreach (var aluno in alunos)
            {
                double total = 0;
                double pesoTotal = 0;

                foreach (var avaliacao in avaliacoes)
                {
                    if (avaliacao.ExcecoesIndividuais != null &&
                        avaliacao.ExcecoesIndividuais.TryGetValue(aluno.Numero, out double notaExcecao))
                    {
                        total += notaExcecao * avaliacao.Tarefa.Peso;
                        pesoTotal += avaliacao.Tarefa.Peso;
                    }
                    else if (avaliacao.Grupo != null && avaliacao.Grupo.Alunos != null &&
                        avaliacao.Grupo.Alunos.Any(a => a.Numero == aluno.Numero))
                    {
                        total += avaliacao.Nota * avaliacao.Tarefa.Peso;
                        pesoTotal += avaliacao.Tarefa.Peso;
                    }

                }

                double notaFinal = pesoTotal > 0 ? total / pesoTotal : 0;

                pauta.Add(new AlunoNotaFinal
                {
                    Numero = aluno.Numero,
                    Nome = aluno.Nome,
                    NotaFinal = Math.Round(notaFinal, 2)
                });
            }

            PautaFinal = new ObservableCollection<AlunoNotaFinal>(pauta);
        }
    }
}
