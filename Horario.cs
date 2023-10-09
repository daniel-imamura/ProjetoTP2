using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace _20128_Projeto2TP
{
    class Horario
    {
        int
            diaSemana,
            horário;

        string
            apelidoProf,
            siglaDisciplina,
            classe;

        const int tamanhoDia = 1;
        const int tamanhoApelido = 6;
        const int tamanhoHorario = 1;
        const int tamanhoSigla = 6;
        const int tamanhoClasse = 5;

        const int inicioDia = 0;
        const int inicioApelido = inicioDia + tamanhoDia;
        const int inicioHorario = inicioApelido + tamanhoApelido;
        const int inicioSigla = inicioHorario + tamanhoHorario;
        const int inicioClasse = inicioSigla + tamanhoSigla;
          
        public Horario(string linhaLida)
        {
            DiaSemana = Convert.ToInt32(linhaLida.Substring(inicioDia, tamanhoDia));
            ApelidoProf = linhaLida.Substring(inicioApelido, tamanhoApelido);
            Horário = Convert.ToInt32(linhaLida.Substring(inicioHorario, tamanhoHorario));
            SiglaDisciplina = linhaLida.Substring(inicioSigla, tamanhoSigla);
            Classe = linhaLida.Substring(inicioClasse, tamanhoClasse);
        }
        public Horario(int dia, string apelido, int hora, string sigla, string clas)
        {
            diaSemana = dia;
            apelidoProf = apelido;
            horário = hora;
            siglaDisciplina = sigla;
            classe = clas;
        }
        public string FormatarParaArquivo()
        {
            return DiaSemana + ApelidoProf + Horário + SiglaDisciplina + Classe;
        }
        public override string ToString()
        {
            return $"{DiaSemana}   {ApelidoProf}   {Horário}   {SiglaDisciplina}   {Classe}";
        }
        public int CompareTo(Horario outroHorario)
        {
            string dados = DiaSemana + ApelidoProf + Horário;
            string outroDados = outroHorario.DiaSemana + outroHorario.ApelidoProf + outroHorario.Horário;
            return dados.CompareTo(outroDados);
        }

        public int DiaSemana
        {
            get => diaSemana;
            set
            {
                if (value <= 0)
                    throw new Exception("Dia da semana inválido");

                diaSemana = value;
            }
        }                   
        public string ApelidoProf
        {
            get => apelidoProf.PadRight(tamanhoApelido, ' ');
            set
            {
                if (value == "")
                    throw new Exception("Sem apelido de professor");

                apelidoProf = value.Substring(0, tamanhoApelido).PadRight(tamanhoApelido, ' ');
            }
        }
        public int Horário
        {
            get => horário;
            set
            {
                if (value <= 0)
                    throw new Exception("Horário inválido");

                horário = value;
            }
        }             
        public string SiglaDisciplina 
        {
            get => siglaDisciplina.PadRight(tamanhoSigla, ' ');
            set
            {
                if (value == "")
                    throw new Exception("Sem sigla da disciplina");

                siglaDisciplina = value.Substring(0, tamanhoSigla).PadRight(tamanhoSigla, ' ');
            }
        }
        public string Classe
        {
            get => classe.PadRight(tamanhoClasse, ' ');
            set
            {
                if (value == "")
                    throw new Exception("Sem classe");

                classe = value.Substring(0, tamanhoClasse).PadRight(tamanhoClasse, ' ');
            }    
        }


    }
}
