using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Environment;

namespace _20128_Projeto2TP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int diaDaSemana = 0;
        int aula = 0;
        string arquivoDadosProfessores;
        string arquivoHorario;
        StreamWriter escreverArquivo;
        StreamReader lerArquivo;

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtApelido.Text = "";
            txtNome.Text = "";
            picFoto.Image = null;
            lbUrlFoto.Text = "";
            tlbMensagem.Text = "Mensagem: Digite os dados do professor, duplo clique na imagem para selecioná-la";

            btnIncluir.Enabled = true;
            btnParar.Enabled = true;

            txtApelido.Focus();
        }

        private void picFoto_DoubleClick(object sender, EventArgs e)
        {
            if (dlgAbrir.ShowDialog() == DialogResult.OK)
            {
                picFoto.ImageLocation = dlgAbrir.FileName;
                picFoto.Load();
                lbUrlFoto.Text = $"{picFoto.ImageLocation}";
            }
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {            
            Professor dadosProf = new Professor(txtApelido.Text, txtNome.Text, lbUrlFoto.Text);

            escreverArquivo = new StreamWriter(arquivoDadosProfessores, true);//Inicializa uma nova instância para append
            escreverArquivo.WriteLine(dadosProf.FormatarParaArquivo());
            escreverArquivo.Close();

            btnIncluir.Enabled = false;
            btnParar.Enabled = false;
            btnLimpar.Focus();
        }

        private void professoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbControl1.SelectedTab = tpProfessor;

            dlgAbrir.Title = "Abrir arquivo de professores para inclusão";
            dlgAbrir.InitialDirectory = @"C:\Windows\Temp\dados";
            if (dlgAbrir.ShowDialog() == DialogResult.OK)
            {
                btnLimpar.Enabled = true;
                btnParar.Enabled = true;
                btnIniciar.Enabled = true;

                btnIncluir.Enabled = false;
                btnProximo.Enabled = false;
                btnFim.Enabled = false;

                btnLimpar.Focus();

                tlbMensagem.Text = "Mensagem: Pressione [Limpar] para iniciar uma inclusão";
                arquivoDadosProfessores = dlgAbrir.FileName;
            }
        }

        private void btnParar_Click(object sender, EventArgs e)
        {
            btnLimpar.Enabled = false;
            btnIncluir.Enabled = false;
            btnParar.Enabled = false;
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            lerArquivo = new StreamReader(arquivoDadosProfessores);
            tlbMensagem.Text = "Mensagem: Clique em [Próximo] para mostrar o registro seguinte";

            btnProximo.Enabled = true;
            btnFim.Enabled = true;
            btnProximo.Focus();
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            Professor dadosProf = new Professor(lerArquivo.ReadLine());

            txtApelido.Text = $"{dadosProf.ApelidoProf}";
            txtNome.Text = $"{dadosProf.NomeProf}";
            lbUrlFoto.Text = $"{dadosProf.UrlFoto}";

            picFoto.ImageLocation = lbUrlFoto.Text;
            try
            {
                picFoto.Load();
            }
            catch
            {
                throw new Exception("Não há foto para ser carregada");
            }

            if (lerArquivo.EndOfStream)
            {
                tlbMensagem.Text = "Mensagem: Fim de arquivo de professores";
                btnProximo.Enabled = false;
            }
        }

        private void btnFim_Click(object sender, EventArgs e)
        {
            lerArquivo.Close();

            btnLimpar.Enabled = false;
            btnIncluir.Enabled = false;
            btnParar.Enabled = false;
            btnIniciar.Enabled = false;
            btnProximo.Enabled = false;
            btnFim.Enabled = false;
        }

        private void horáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbxProfessor.Items.Clear();
            dlgAbrir.InitialDirectory = @"C:\Windows\Temp\dados";
            dlgAbrir.Title = "Abrir arquivo de dados de professores para leitura";
            if (dlgAbrir.ShowDialog() == DialogResult.OK)
            {
                StreamReader lerArquivo = new StreamReader(dlgAbrir.FileName);
                arquivoHorario = dlgAbrir.FileName;
                while (!lerArquivo.EndOfStream)
                {
                    string linhaLida = lerArquivo.ReadLine();

                    if (linhaLida == "")//condição necessária para consertar um erro onde ele terminava o loop na hora errada
                        break;

                    Professor dadosLinha = new Professor(linhaLida);
                    cbxProfessor.Items.Add(dadosLinha.ApelidoProf);
                }
                lerArquivo.Close();
            }
            dlgAbrir.Title = "Abrir arquivo de horários para inclusão";

            if(dlgAbrir.ShowDialog() == DialogResult.OK)
            {
                tbControl1.SelectedTab = tpHorario;
                btnLimparHorario.Enabled = true;
                btnPararHorario.Enabled = true;
                btnIncluirHorario.Enabled = true;
                btnLimparHorario.Focus();
            }            
        }

        private void btnLimparHorario_Click(object sender, EventArgs e)
        {
            cbxProfessor.Focus();
            tlbMensagem.Text = "Mensagem: Preencha os campos";
        }

        private void btnPararHorario_Click(object sender, EventArgs e)
        {
            btnLimparHorario.Enabled = false;
            btnPararHorario.Enabled = false;
            btnIncluirHorario.Enabled = false;
        }

        private void btnIncluirHorario_Click(object sender, EventArgs e)
        {
            Horario dadosHorario = new Horario(diaDaSemana, cbxProfessor.Text, aula, txtSiglaDisciplina.Text, txtClasse.Text);
            escreverArquivo = new StreamWriter(dlgAbrir.FileName, true);

            escreverArquivo.WriteLine(dadosHorario.FormatarParaArquivo());
            escreverArquivo.Close();
        }        
        //Para saber qual radiobutton foi selecionado
        private void rbSegunda_CheckedChanged(object sender, EventArgs e)
        {
            diaDaSemana = 2;
        }

        private void rbTerca_CheckedChanged(object sender, EventArgs e)
        {
            diaDaSemana = 3;
        }

        private void rbQuarta_CheckedChanged(object sender, EventArgs e)
        {
            diaDaSemana = 4;
        }

        private void rbQuinta_CheckedChanged(object sender, EventArgs e)
        {
            diaDaSemana = 5;
        }

        private void rbSexta_CheckedChanged(object sender, EventArgs e)
        {
            diaDaSemana = 6;
        }

        private void rbSabado_CheckedChanged(object sender, EventArgs e)
        {
            diaDaSemana = 7;
        }

        private void rbAula1_CheckedChanged(object sender, EventArgs e)
        {
            aula = 1;
        }

        private void rbAula2_CheckedChanged(object sender, EventArgs e)
        {
            aula = 2;
        }

        private void rbAula3_CheckedChanged(object sender, EventArgs e)
        {
            aula = 3;
        }

        private void rbAula4_CheckedChanged(object sender, EventArgs e)
        {
            aula = 4;
        }

        private void rbAula5_CheckedChanged(object sender, EventArgs e)
        {
            aula = 5;
        }

        private void rbAula6_CheckedChanged(object sender, EventArgs e)
        {
            aula = 6;
        }

        private void tsListagem_Click(object sender, EventArgs e)
        {
            //Ordenar para dados horários
            dlgAbrir.Title = "Abrir arquivo de horários para listagem";
            if (dlgAbrir.ShowDialog() == DialogResult.OK)
            {
                lerArquivo = new StreamReader(dlgAbrir.FileName);
                arquivoHorario = dlgAbrir.FileName;
                lsbOrdenador.Items.Clear();
                lsbOrdenador.Sorted = false;
                while(!lerArquivo.EndOfStream)
                {
                    lsbOrdenador.Items.Add(lerArquivo.ReadLine());
                }
                lerArquivo.Close();
                lsbOrdenador.Sorted = true;

                escreverArquivo = new StreamWriter(dlgAbrir.FileName, false);
                foreach(var item in lsbOrdenador.Items)
                {
                    escreverArquivo.WriteLine(item);
                }
                escreverArquivo.Close();
            }
            //Ordenar para dados professores
            dlgAbrir.Title = "Abrir arquivo de professores para listagem";
            if(dlgAbrir.ShowDialog() == DialogResult.OK)
            {
                lerArquivo = new StreamReader(dlgAbrir.FileName);
                arquivoDadosProfessores = dlgAbrir.FileName;
                lsbOrdenador.Items.Clear();
                lsbOrdenador.Sorted = false;
                while (!lerArquivo.EndOfStream)
                {
                    lsbOrdenador.Items.Add(lerArquivo.ReadLine());
                }
                lerArquivo.Close();
                lsbOrdenador.Sorted = true;

                escreverArquivo = new StreamWriter(dlgAbrir.FileName, false);
                foreach (var item in lsbOrdenador.Items)
                {
                    escreverArquivo.WriteLine(item);
                }
                escreverArquivo.Close();
            }
        }
        
        public void RealizarCasamento(string diaSemana, int diaDaSemana)
        {
            txtListagem.Text =
               $"Dia da Semana: {diaSemana}   -   Ponto de Professores" + NewLine + NewLine +
                "Professor                                          1                    2                    3                    4                    5                    6                 Aulas     Assinatura" + NewLine + NewLine;

            int qntsProfessores = 1;//Total de professores
            int qntdeAula = 2;//Total de aulas dadas
            bool terminou = false;//quando terminar o loop vira true
            StreamReader lerHorario = new StreamReader(arquivoHorario);                        
            Horario casarInfoHora = new Horario(lerHorario.ReadLine());
            while (terminou == false)
            {
                while (casarInfoHora.DiaSemana != diaDaSemana)//Ler linhas do arquivo horário até achar o dia da semana correspondente ao escolhido
                    casarInfoHora = new Horario(lerHorario.ReadLine());

                if (casarInfoHora.DiaSemana == diaDaSemana)
                {
                    int qntsAulas = 0;//quantidade de aulas por linha
                    int horario = 1;
                    int aulasTotais = 0;//aulas dadas + aulas vagas                    
                    bool mudouNomeProf = false;      
                    //Cada antigo receberá os dados do arquivo horário
                    string apelidoProfAntigo = "";
                    string siglaDisciplinaAntiga = "";
                    string classeAntiga = "";
                    int horarioAntigo = 0;

                    bool aulaMarcada = true;//caso a última aula não foi vaga
                    string apelidoProf = casarInfoHora.ApelidoProf;

                    lerArquivo = new StreamReader(arquivoDadosProfessores);
                    Professor casarInfoProf = new Professor(lerArquivo.ReadLine());                                        
                    while (horario != 100)//loop infinito porque possui um break no meio
                    {
                        if (aulasTotais < 6 && mudouNomeProf == false && aulaMarcada == true)//ler nova linha e passar valores
                        {
                            apelidoProfAntigo = casarInfoHora.ApelidoProf;
                            horarioAntigo = casarInfoHora.Horário;
                            siglaDisciplinaAntiga = casarInfoHora.SiglaDisciplina;
                            classeAntiga = casarInfoHora.Classe;

                            string linhaALer = lerHorario.ReadLine();
                            if (linhaALer != null)
                            {
                                casarInfoHora = new Horario(linhaALer);
                                aulaMarcada = false;
                            }
                        }
                        if (casarInfoProf.ApelidoProf == apelidoProf || lerHorario.EndOfStream)
                        {
                            if (horario == 1)//Colocar o nome do professor correspondente                                                                
                                txtListagem.AppendText($"{casarInfoProf.NomeProf}");                                
                                                            
                            if (casarInfoProf.ApelidoProf != casarInfoHora.ApelidoProf)//se mudou de professor
                            {
                                mudouNomeProf = true;
                            }
                            if (horarioAntigo == horario && casarInfoProf.ApelidoProf == apelidoProfAntigo)//caso o professor tenha dado aula
                            {                                                                                                                                                                
                                txtListagem.AppendText($" {classeAntiga} {siglaDisciplinaAntiga} ");

                                qntsAulas++;
                                aulasTotais++;
                                aulaMarcada = true;
                            }
                            else
                            {                                                                
                                txtListagem.AppendText(" - - - - - - - - - - ");
                                aulasTotais++;
                            }
                            horario++;
                            if (mudouNomeProf == true && aulasTotais == 6 && casarInfoHora.DiaSemana == diaDaSemana)//se mudou de professor
                            {
                                txtListagem.AppendText($"   {qntsAulas}         ");
                                txtListagem.AppendText("____________" + NewLine + NewLine);
                                casarInfoProf = new Professor(lerArquivo.ReadLine());
                                horario = 1;
                                qntdeAula += qntsAulas;
                                qntsAulas = 0;
                                aulasTotais = 0;
                                qntsProfessores++;
                                mudouNomeProf = false;                                
                                apelidoProf = casarInfoHora.ApelidoProf;
                            }
                            if (casarInfoHora.DiaSemana != diaDaSemana && aulasTotais == 6 || lerHorario.EndOfStream && aulasTotais == 6)//Se terminou todos os professores
                            {
                                txtListagem.AppendText($"   {qntsAulas}         ");
                                txtListagem.AppendText("____________" + NewLine + NewLine);
                                lerArquivo.Close();
                                lerHorario.Close();
                                terminou = true;
                                break;
                            }
                        }
                    }
                }
            }
            txtListagem.AppendText(NewLine + NewLine + $"Número de Professores: {qntsProfessores}             Aulas: {qntdeAula}");
        }

        private void rbSegundaFeira_Click(object sender, EventArgs e)
        {
            RealizarCasamento("Segunda-feira", 2);
        }

        private void rbTercaFeira_Click(object sender, EventArgs e)
        {
            RealizarCasamento("Terça-feira", 3);
        }

        private void rbQuartaFeira_Click(object sender, EventArgs e)
        {
            RealizarCasamento("Quarta-feira", 4);
        }

        private void rbQuintaFeira_Click(object sender, EventArgs e)
        {
            RealizarCasamento("Quinta-feira", 5);
        }

        private void rbSextaFeira_Click(object sender, EventArgs e)
        {
            RealizarCasamento("Sexta-feira", 6);
        }

        private void rbSabadoListagem_Click(object sender, EventArgs e)
        {
            RealizarCasamento("Sábado", 7);
        }

        private void btnAbrirArquivo_Click(object sender, EventArgs e)
        {
            dlgAbrir.Title = "Abrir arquivo de horários";
            dlgAbrir.InitialDirectory = @"C:\Windows\Temp\dados";
            if(dlgAbrir.ShowDialog() == DialogResult.OK)
            {
                arquivoHorario = dlgAbrir.FileName;

                dlgAbrir.Title = "Abrir arquivo de professores";
                if(dlgAbrir.ShowDialog() == DialogResult.OK)
                {
                    arquivoDadosProfessores = dlgAbrir.FileName;
                }
            }
        }        
        private void tsRelatorio_Click(object sender, EventArgs e)
        {
            dlgAbrir.Title = "Abrir arquivo de professores";
            dlgAbrir.InitialDirectory = @"C:\Windows\Temp\dados";
            if(dlgAbrir.ShowDialog() == DialogResult.OK)
            {
                dlgSalvar.Title = "Salvar arquivo HTML";
                if(dlgSalvar.ShowDialog() == DialogResult.OK)
                {
                    tbControl1.SelectedTab = tpRelatorio;

                    int qntsLinhas = File.ReadLines(dlgAbrir.FileName).Count();
                    lerArquivo = new StreamReader(dlgAbrir.FileName);
                    escreverArquivo = new StreamWriter(dlgSalvar.FileName);

                    escreverArquivo.WriteLine("<!DOCTYPE html>");
                    escreverArquivo.WriteLine("<html lang=\"pt-BR\">");
                    escreverArquivo.WriteLine("<head>");
                    escreverArquivo.WriteLine("<meta charset=\"UTF-8\">");
                    escreverArquivo.WriteLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
                    escreverArquivo.WriteLine("<title>tabela professores</title>");
                    escreverArquivo.WriteLine("<style>");
                    escreverArquivo.WriteLine("table, th, td{");
                    escreverArquivo.WriteLine("border: solid 1px black; ");
                    escreverArquivo.WriteLine("border-collapse: collapse; ");
                    escreverArquivo.WriteLine("text-align: center;");
                    escreverArquivo.WriteLine("}");
                    escreverArquivo.WriteLine(".corDeFundo{");
                    escreverArquivo.WriteLine("background-color: yellow;");
                    escreverArquivo.WriteLine("}");
                    escreverArquivo.WriteLine("</style>");
                    escreverArquivo.WriteLine("</head>");
                    escreverArquivo.WriteLine("<body>");
                    escreverArquivo.WriteLine("<table>");
                    escreverArquivo.WriteLine("<tr>");
                    escreverArquivo.WriteLine("<th colspan=\"3\">Relatório de Professores</th>");
                    escreverArquivo.WriteLine("</tr>");
                    escreverArquivo.WriteLine("<tr>");
                    escreverArquivo.WriteLine("<th>Apelido</th>");
                    escreverArquivo.WriteLine("<th>Nome</th>");
                    escreverArquivo.WriteLine("<th>Foto</th>");
                    escreverArquivo.WriteLine("</tr>");

                    int linha = 1;
                    for (int linhas = qntsLinhas; linhas > 0; linhas--)
                    {
                        Professor dadosProf = new Professor(lerArquivo.ReadLine());

                        //para cada linha ímpar adicionar uma cor de fundo
                        if (linha % 2 == 0)
                            escreverArquivo.WriteLine("<tr>");
                        else
                            escreverArquivo.WriteLine("<tr class=\"corDeFundo\">");

                        escreverArquivo.WriteLine($"<td>{dadosProf.ApelidoProf}</td>");
                        escreverArquivo.WriteLine($"<td>{dadosProf.NomeProf}</td>");
                        escreverArquivo.WriteLine($"<td><img src=\"{dadosProf.UrlFoto}\"></td>");//Para carregar as imagens elas devem estar no local correto
                        escreverArquivo.WriteLine("</tr>");
                        linha++;
                    }
                    lerArquivo.Close();
                    escreverArquivo.Close();
                    wbProfessores.Url = new System.Uri(dlgSalvar.FileName, System.UriKind.Absolute);
                }                               
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }      
}
