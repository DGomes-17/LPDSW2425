using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoLab.Models
{
    public class Pauta
    {
        public List<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();

        public double CalcularNotaFinal(int numeroAluno)
        {
            double somaPesos = 0;
            double somaNotas = 0;

            foreach (var av in Avaliacoes)
            {
                double nota;

                if (av.ExcecoesIndividuais != null && av.ExcecoesIndividuais.ContainsKey(numeroAluno))
                    nota = av.ExcecoesIndividuais[numeroAluno];
                else if (av.Grupo.Alunos.Any(a => a.Numero == numeroAluno))
                    nota = av.Nota;
                else
                    continue;

                somaNotas += nota * av.Tarefa.Peso;
                somaPesos += av.Tarefa.Peso;
            }

            return somaPesos > 0 ? somaNotas / somaPesos : 0;
        }

    }

}
