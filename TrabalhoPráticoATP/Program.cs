using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoPráticoATP
{
    internal class Program
    {

        static void PeoesDisponiveis(Tabuleiro tabuleiro, int i)
        {

            Jogador jogador = tabuleiro.VetJgd[i];
            for (int j = 0; j < jogador.VetPeao.Length; j++)
            {
                Peao peao = tabuleiro.VetJgd[i].VetPeao[j];

                if (peao.Posicao > 0)
                {
                    //teste

                }
            }
        }

        static void SaidaJogador(Tabuleiro tabuleiro)
        {
            for (int i = 0; i < tabuleiro.VetJgd.Length; i++)
            {
                Peao peao = tabuleiro.VetJgd[i].VetPeao[i];
                peao.Posicao = peao.VetP[0];

            }
        }




        static void Main(string[] args)
        {
            int contQntdDisponiveis = 0;
            int[] disponiveis = new int[4];
            Tabuleiro tabuleiro = new Tabuleiro();
            tabuleiro.ImprimeJgd();

            for (int i = 0; i < tabuleiro.VetJgd.Length; i++)
            {
                Jogador jogador = tabuleiro.VetJgd[i];

                int dado = jogador.LancarDado();



                //salvando quais peões estão disponíveis
                for (int j = 0; j < jogador.VetPeao.Length; j++)
                {
                    Peao peao = tabuleiro.VetJgd[i].VetPeao[j];

                    if (peao.Posicao > 0)
                    {
                        contQntdDisponiveis++;
                        disponiveis[i] = peao.Id;
                    }
                }
                dado = 6;

                //retirando peão da casa incial quando não houver opção para o jogador e colocando no vetor de posições reais do jogo
                if (dado == 6 && contQntdDisponiveis == 4)
                {

                    Console.WriteLine("Informe qual o número do peão que deseja retirar da casa:\nNúmeros disponpiveis:");
                    for (int k = 0; k < 4; k++)
                    {
                        if (disponiveis[i] > 0)
                        {
                            Console.Write(disponiveis[i] + ", ");
                        }
                    }

                    int idPeao = int.Parse(Console.ReadLine());
                    jogador.VetPeao[idPeao].posicao = jogador.VetPeao[idPeao].VetP[0];
                    jogador.LancarDado();
                    if (dado == 6)
                    {
                        Console.WriteLine("Deseja retirar mais algum peão? Digite 'sim' ou 'não");
                        string resposta = Console.ReadLine();
                        if (resposta == "sim")
                        {
                            Console.WriteLine("Informe qual o número do peão que deseja retirar da casa:\nNúmeros disponpiveis:");
                            for (int k = 0; k < 4; k++)
                            {
                                if (disponiveis[i] > 0)
                                {
                                    Console.Write(disponiveis[i] + ", ");
                                }
                            }
                        }
                    }
                }
                else if (dado == 6 && contQntdDisponiveis > 0)
                {
                    Console.WriteLine("Deseja retirar mais algum peão? Digite 'sim' ou 'não");
                    string resposta = Console.ReadLine();
                }

            }

            foreach (int n in disponiveis)
            {
                Console.WriteLine(n + " ");
            }


            Console.ReadLine();
        }
    }
}
