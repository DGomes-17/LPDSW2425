using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using ProjetoLab.Models;
using SkiaSharp;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProjetoLab.ViewModels
{
    public class HistogramaViewModel
    {
        public ISeries[] Series { get; set; }
        public Axis[] XAxes { get; set; }
        public Axis[] YAxes { get; set; }

        public HistogramaViewModel()
        {
            var pauta = DataService<Pauta>.Carregar("pauta.xml") ?? new Pauta();
            var alunos = DataService<List<Aluno>>.Carregar("alunos.xml") ?? new();

            var distribuicao = new Dictionary<int, int>();
            foreach (var aluno in alunos)
            {
                double nota = pauta.CalcularNotaFinal(aluno.Numero);
                int bin = (int)(nota / 2) * 2;
                if (!distribuicao.ContainsKey(bin)) distribuicao[bin] = 0;
                distribuicao[bin]++;
            }

            var valores = distribuicao.OrderBy(kv => kv.Key).Select(kv => (double)kv.Value).ToArray();
            var etiquetas = distribuicao.OrderBy(kv => kv.Key).Select(kv => $"{kv.Key}-{kv.Key + 1}").ToArray();

            Series = new ISeries[]
            {
                new ColumnSeries<double>
                {
                    Values = valores,
                    Fill = new SolidColorPaint(SKColors.DarkRed)
                }
            };

            XAxes = new Axis[]
            {
                new Axis
                {
                    Labels = etiquetas,
                    LabelsPaint = new SolidColorPaint(SKColors.Black),
                    Name = "Nota Final"
                }
            };

            YAxes = new Axis[]
            {
                new Axis
                {
                    Name = "N.º Alunos",
                    LabelsPaint = new SolidColorPaint(SKColors.Black)
                }
            };
        }
    }
}
