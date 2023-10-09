using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace _20128_Projeto2TP
{
    class Professor
    {
        const int tamanhoApelido = 6;
        const int tamanhoNome = 30;

        const int inicioApelido = 0;
        const int inicioNome = inicioApelido + tamanhoApelido;
        const int inicioUrl = inicioNome + tamanhoNome;

        string
            apelidoProf,
            nomeProf,
            urlFoto;        

        public Professor(string linhaLida)
        {
            ApelidoProf = linhaLida.Substring(inicioApelido, tamanhoApelido);
            NomeProf = linhaLida.Substring(inicioNome, tamanhoNome);
            UrlFoto = linhaLida.Substring(inicioUrl);
        }
        public Professor(string apelido, string nome, string url)
        {
            apelidoProf = apelido;
            nomeProf = nome;
            urlFoto = url;
        }
        public string FormatarParaArquivo()
        {                        
            return ApelidoProf + NomeProf + UrlFoto;
        }
        public override string  ToString()
        {
            return $"{ApelidoProf.PadRight(tamanhoApelido, ' ')}  {NomeProf.PadRight(tamanhoNome, ' ')}  {UrlFoto}";
        }
        public int CompareTo(Professor outroProfessor)
        {
            return ApelidoProf.CompareTo(outroProfessor.ApelidoProf);
        }
        public string ApelidoProf
        {
            get => apelidoProf.PadRight(tamanhoApelido, ' ');
            set => apelidoProf = value.Substring(0, tamanhoApelido).PadRight(tamanhoApelido, ' ');
        }
        public string NomeProf
        {
            get => nomeProf.PadRight(tamanhoNome, ' ');
            set => nomeProf = value.Substring(0, tamanhoNome).PadRight(tamanhoNome, ' ');
        }
        public string UrlFoto
        {
            get { return urlFoto; }
            set => urlFoto = value.Substring(0);
        }        
    }
}
