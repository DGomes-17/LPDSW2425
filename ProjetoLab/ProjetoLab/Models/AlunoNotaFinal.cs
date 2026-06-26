using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoLab.Models
{
    [Serializable]
    public class AlunoNotaFinal
    {
        public int Numero { get; set; }
        public string Nome { get; set; }
        public double NotaFinal { get; set; }
    }

}