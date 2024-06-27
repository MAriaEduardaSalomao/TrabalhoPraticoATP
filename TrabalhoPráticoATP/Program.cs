using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoPráticoATP
{
    internal class Program
    {

        static int PeoesDisponiveis(Jogador jogador, int[] disponiveis)
        {
            int contQntdDisponiveis = 0;
            for(int i = 0; i < disponiveis.Length; i++)
            {
                disponiveis[i] = 0;
            }
            
            for (int j = 0; j < jogador.VetPeao.Length; j++)
            {
                Peao peao = jogador.VetPeao[j];

                if (peao.Posicao > 0)
                {
                    contQntdDisponiveis++;
                    disponiveis[j] = peao.Id;

                }
            }
            return contQntdDisponiveis;
        }

        static int PeoesDisponiveisSaida(Jogador jogador, int[] disponiveisSaida)
        {
            int contQntdDisponiveis = 0;
            for (int i = 0; i < disponiveisSaida.Length; i++)
            {
                disponiveisSaida[i] = 0;
            }

            for (int j = 0; j < jogador.VetPeao.Length; j++)
            {
                Peao peao = jogador.VetPeao[j];

                if (peao.Posicao < 0)
                {
                    contQntdDisponiveis++;
                    disponiveisSaida[j] = peao.Id;

                }
            }
            return contQntdDisponiveis;
        }

        static void SaidaJogador(Jogador jogador, int[] disponiveis)
        {
            Console.WriteLine($"{jogador.Nome}, é a sua vez!Informe qual o número do peão que deseja retirar da casa\nNúmeros disponpiveis:");
            for (int k = 0; k < 4; k++)
            {
                Console.Write((k+1) + " ");
            }
            int idPeao = int.Parse(Console.ReadLine());

            jogador.VetPeao[idPeao - 1].EntrarJogo();

            //jogador.VetPeao[idPeao-1].Posicao = jogador.VetPeao[idPeao-1].VetP[0];
            Console.WriteLine($"\nJogador:{jogador.Nome}\nPosição do peão {jogador.Cor}: {jogador.VetPeao[idPeao - 1].Posicao}");

            int dado = jogador.LancarDado();
            Console.WriteLine("\n Valor do Dado: " + dado + "\n");
            if (dado == 6)
            {
                Console.WriteLine("\nDeseja retirar mais algum peão da casa? Digite 's' ou 'n':");
                char resposta=char.Parse(Console.ReadLine());
                if(resposta == 's')
                {
                    int[] disponiveisSaida= new int[4];
                    int qntdDisponiveis = PeoesDisponiveisSaida(jogador, disponiveisSaida);

                    InformeDisponiveis(jogador, disponiveisSaida);
                    //Console.WriteLine("\nInforme qual o número do peão que deseja retirar da casa\nNúmeros disponpiveis:");
                    //for (int i =0; i < 4; i++)
                    //{
                    //    if (disponiveisSaida[i] > 0)
                    //    {
                    //        Console.Write(disponiveisSaida[i] + " ");
                    //    }
                    //}
                    idPeao = int.Parse(Console.ReadLine());
                    jogador.VetPeao[idPeao - 1].EntrarJogo();
                    Console.WriteLine($"\nJogador:{jogador.Nome}\nPosição atual do peão {jogador.Cor}: {jogador.VetPeao[idPeao - 1].Posicao}");

                }
                else if(resposta == 'n')
                {

                    jogador.VetPeao[idPeao - 1].MoverPeao(dado);
                    Console.WriteLine($"\nJogador:{jogador.Nome}\nPosição atual do peão {jogador.Cor}: {jogador.VetPeao[idPeao-1].Posicao}");

                }else
                {
                    Console.WriteLine("Valor informado incorreto");
                }
            }
            dado = jogador.LancarDado();
            Console.WriteLine(dado);
            if (dado == 6)
            {
                Console.WriteLine("Quantidade permitida de 6's atingida");
                
            }
            else
            {
                if (PeoesDisponiveis(jogador, disponiveis) > 1)
                {
                    InformeDisponiveis(jogador, disponiveis);
                    idPeao = int.Parse(Console.ReadLine());
                    jogador.VetPeao[idPeao - 1].MoverPeao(dado);
                    Console.WriteLine($"Jogador:{jogador.Nome}\nPosição do peão {jogador.Cor}: {jogador.VetPeao[idPeao-1].Posicao}");

                }
                else
                {
                    jogador.VetPeao[idPeao - 1].MoverPeao(dado);
                    Console.WriteLine($"Jogador:{jogador.Nome}\nPosição do peão {jogador.Cor}: {jogador.VetPeao[idPeao - 1].Posicao}");
                }
            }

               
        }

        static bool Vitoria(Tabuleiro tabuleiro,int qntJgd)
        {
            int cont = 0;
            for(int i = 0; i < qntJgd; i++)
            {
                Jogador jgd = tabuleiro.VetJgd[i];
                bool jogadorVenceu = true;
                for (int j=0; j < jgd.VetPeao.Length; j++)
                {
                    Peao peao= jgd.VetPeao[j];

                    
                        if (peao.Posicao != 57)
                        {
                        jogadorVenceu = false;
                        break;
                        }
                    
                }
                if (jogadorVenceu)
                {
                    return true;
                }
            }

            return false;

        }

        static void InformeDisponiveis(Jogador jogador, int[] disponiveis)
        {
            Console.WriteLine("Informe o número do peão que deseja mover\nNúmeros Disponíveis:");
            for (int i = 0; i < 4; i++)
            {
                if (disponiveis[i] > 0)
                {
                    Console.Write(disponiveis[i] + " ");
                }
            }
        }
        




        static void Main(string[] args)
        {
            int contQntdDisponiveis = 0;
            int[] disponiveis = new int[4];
            int rodada=1;

            Console.WriteLine("Informe a quantidade de jogadores:");
            int qntJgd=int.Parse(Console.ReadLine());
            if (qntJgd < 2 || qntJgd>4) 
            {
                Console.WriteLine("Número de jogadores inválido");
            }
            else
            {
                Tabuleiro tabuleiro = new Tabuleiro(qntJgd);
                tabuleiro.ImprimeJgd();

                do
                {
                    Console.WriteLine($"Rodada:{rodada}");

                    



                    for (int i = 0; i < qntJgd; i++)
                    {

                        Jogador jogador = tabuleiro.VetJgd[i];

                        int dado=jogador.LancarDado();
                        Console.WriteLine($"Dado jogado pelo jogador {jogador.Nome}:  {dado}");

                        contQntdDisponiveis = PeoesDisponiveisSaida(jogador, disponiveis);


                        if (dado == 6 && contQntdDisponiveis == 4)
                        {
                            Console.WriteLine("\nVez de: " + jogador.Nome + "\n");
                            
                            SaidaJogador(jogador, disponiveis);
                            Console.WriteLine("teste");
                        }
                     
                        contQntdDisponiveis=PeoesDisponiveis(jogador,disponiveis);
                        //if (contQntdDisponiveis > 0)
                        //{
                            
                        //    InformeDisponiveis(jogador, disponiveis);
                        //    int idPeao = int.Parse(Console.ReadLine());
                        //    jogador.VetPeao[idPeao].MoverPeao(dado);
                        //    if (dado == 6)
                        //    {
                        //        jogador.LancarDado();
                        //        InformeDisponiveis(jogador, disponiveis);
                        //        idPeao = int.Parse(Console.ReadLine());
                        //        jogador.VetPeao[idPeao].MoverPeao(dado);
                        //        if (dado == 6)
                        //        {
                        //            jogador.LancarDado();
                        //            if (dado == 6)
                        //            {

                        //            }
                        //            else
                        //            {
                        //                InformeDisponiveis(jogador, disponiveis);
                        //                idPeao = int.Parse(Console.ReadLine());
                        //                jogador.VetPeao[idPeao].MoverPeao(dado);
                        //            }
                                    
                        //        }
                        //    }

                        //}



                    }
                    rodada++;

                } while (!Vitoria(tabuleiro, qntJgd));
                
            }

            

            Console.ReadLine();
        }
    }
}
