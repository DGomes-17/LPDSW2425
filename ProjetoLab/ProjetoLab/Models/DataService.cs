using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ProjetoLab.Models
{
    public static class DataService<T>
    {
        private static string PastaDados => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "GestorAvaliacoesLPDS");

        public static void Guardar(string nomeFicheiro, T dados)
        {
            try
            {
                Directory.CreateDirectory(PastaDados);
                string caminho = Path.Combine(PastaDados, nomeFicheiro);

                var serializer = new XmlSerializer(typeof(T));
                using var stream = new FileStream(caminho, FileMode.Create);
                serializer.Serialize(stream, dados);
            }
            catch (Exception ex)
            {
                // Podes fazer log aqui ou lançar uma exceção personalizada
                Console.WriteLine($"Erro ao guardar: {ex.Message}");
            }
        }

        public static T? Carregar(string nomeFicheiro)
        {
            try
            {
                string caminho = Path.Combine(PastaDados, nomeFicheiro);
                if (!File.Exists(caminho)) return default;

                var serializer = new XmlSerializer(typeof(T));
                using var stream = new FileStream(caminho, FileMode.Open);
                return (T?)serializer.Deserialize(stream);
            }
            catch (Exception ex)
            {
                // Podes tratar o erro ou devolver o default
                Console.WriteLine($"Erro ao carregar: {ex.Message}");
                return default;
            }
        }
    }
}

