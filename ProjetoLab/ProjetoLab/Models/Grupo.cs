using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ProjetoLab.Models
{


    [Serializable]
    public class Grupo
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        private List<Aluno> _alunos;

        [XmlArray("Alunos")]
        [XmlArrayItem("Aluno")]
        public List<Aluno> Alunos
        {
            get => _alunos ??= new List<Aluno>();
            set => _alunos = value;
        }
    }
}
