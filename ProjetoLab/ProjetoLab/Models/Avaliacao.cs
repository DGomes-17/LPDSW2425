using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ProjetoLab.Models
{
    public class Avaliacao
    {
        public Tarefa Tarefa { get; set; }
        public Grupo Grupo { get; set; }
        public double Nota { get; set; }

        [XmlIgnore]
        public Dictionary<int, double> ExcecoesIndividuais { get; set; } = new();
    }
}
