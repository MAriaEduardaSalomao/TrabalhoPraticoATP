using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
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

        static void SaidaJogador(Jogador jogador, int[] disponiveis, int[] disponiveisSaida, Tabuleiro tabuleiro,int qntJgd)
        {
            int qntdDisponiveis = PeoesDisponiveisSaida(jogador, disponiveisSaida);
            InformeDisponiveis(jogador, disponiveisSaida);
            int idPeao = int.Parse(Console.ReadLine());

            Console.WriteLine($"\nMovimento do jogador {jogador.Nome}");
            jogador.VetPeao[idPeao - 1].EntrarJogo();
            Console.WriteLine($"Posição atual do peão {jogador.Cor}: {jogador.VetPeao[idPeao - 1].Posicao}");

            int dado = jogador.LancarDado();
            
            Console.WriteLine($"\nDado jogado pelo jogador {jogador.Nome}: {dado}\n");
            if (dado == 6)
            {
                Console.WriteLine("\nDeseja retirar mais algum peão da casa? Digite 's' ou 'n':");
                char resposta=char.Parse(Console.ReadLine());
                if(resposta == 's')
                {
                    
                    qntdDisponiveis = PeoesDisponiveisSaida(jogador, disponiveisSaida);

                    
                    InformeDisponiveis(jogador, disponiveisSaida);
                    
                    idPeao = int.Parse(Console.ReadLine());
                    Console.WriteLine($"\nMovimento do jogador {jogador.Nome}");
                    jogador.VetPeao[idPeao - 1].EntrarJogo();
                    Console.WriteLine($"Posição atual do peão {jogador.Cor}: {jogador.VetPeao[idPeao - 1].Posicao}");
                    //VerificaCasaSegura()
                    VerificaCaptura(jogador, tabuleiro, idPeao, qntJgd);

                }
                else if(resposta == 'n')
                {
                    Console.WriteLine($"\nMovimento do jogador {jogador.Nome}");
                    jogador.VetPeao[idPeao - 1].MoverPeao(dado);
                    Console.WriteLine($"Posição atual do peão {jogador.Cor}: {jogador.VetPeao[idPeao-1].Posicao}");
                    VerificaCaptura(jogador, tabuleiro, idPeao,qntJgd);

                }
                else
                {
                    Console.WriteLine("Valor informado incorreto");
                }
            }
            else
            {
                Console.WriteLine($"\nMovimento do jogador {jogador.Nome}");
                jogador.VetPeao[idPeao - 1].MoverPeao(dado);
                Console.WriteLine($"Posição atual do peão {jogador.Cor}: {jogador.VetPeao[idPeao - 1].Posicao}");
                VerificaCaptura(jogador, tabuleiro, idPeao, qntJgd);
            }
            //dado = jogador.LancarDado();
            //Console.WriteLine($"\nDado jogado pelo jogador {jogador.Nome}: {dado}\n");
            //if (dado == 6)
            //{
            //    Console.WriteLine("Quantidade permitida de 6's atingida");
                
            //}
            //else
            //{
            //    if (PeoesDisponiveis(jogador, disponiveis) > 1)
            //    {
                    
            //        InformeDisponiveis(jogador, disponiveis);
            //        idPeao = int.Parse(Console.ReadLine());
            //        Console.WriteLine($"\nMovimento do jogador {jogador.Nome}");
            //        jogador.VetPeao[idPeao - 1].MoverPeao(dado);
            //        Console.WriteLine($"\nPosição atual do peão {jogador.Cor}: {jogador.VetPeao[idPeao-1].Posicao}");
            //        VerificaCaptura(jogador, tabuleiro, idPeao, qntJgd);

            //    }
            //    else
            //    {
            //        Console.WriteLine($"\nMovimento do jogador {jogador.Nome}");
            //        jogador.VetPeao[idPeao - 1].MoverPeao(dado);
            //        Console.WriteLine($"\nPosição atual do peão {jogador.Cor}: {jogador.VetPeao[idPeao - 1].Posicao}");
            //        VerificaCaptura(jogador, tabuleiro, idPeao, qntJgd);
            //    }
            //}

               
        }

        static bool Vitoria(Tabuleiro tabuleiro,int qntJgd)
        {
            
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
        
       static void SaidaNaoObrigatoria(Jogador jogador, int[] disponiveis, int[] disponiveisSaida, int dado, Tabuleiro tabuleiro, int qntJgd)
        {
            int idPeao = 0;
            Console.WriteLine("\nDeseja retirar mais algum peão da casa? Digite 's' ou 'n':");
            char resposta = char.Parse(Console.ReadLine());
            if (resposta == 's')
            {

                int qntdDisponiveis = PeoesDisponiveisSaida(jogador, disponiveisSaida);
                if (qntdDisponiveis > 0)
                {
                    InformeDisponiveis(jogador, disponiveisSaida);

                    idPeao = int.Parse(Console.ReadLine());
                    Console.WriteLine($"\nMovimento do jogador {jogador.Nome}");
                    jogador.VetPeao[idPeao - 1].EntrarJogo();
                    Console.WriteLine($"Posição atual do peão {jogador.Cor}: {jogador.VetPeao[idPeao - 1].Posicao}");
                    VerificaCaptura(jogador, tabuleiro, idPeao, qntJgd);

                }
                else
                {
                    Console.WriteLine("Todos os peões já foram retirados");
                }


            }
            else if (resposta == 'n')
            {
                for (int j = 0; j < jogador.VetPeao.Length; j++)
                {
                    if (disponiveis[j] > 0)
                    {
                        Console.Write("Peão disponível:" + disponiveis[j]);
                        idPeao = disponiveis[j];
                    }
                }
                Console.WriteLine($"\nMovimento do jogador {jogador.Nome}");
                jogador.VetPeao[idPeao - 1].MoverPeao(dado);
                Console.WriteLine($"Posição atual do peão {jogador.Cor}: {jogador.VetPeao[idPeao - 1].Posicao}");
                VerificaCaptura(jogador, tabuleiro, idPeao, qntJgd);

            }
            else
            {
                Console.WriteLine("Valor informado incorreto");
            }
        }

        static bool VerificaCaptura(Jogador jogador,Tabuleiro tabuleiro, int idPeao, int qntJgd)
        {

            for (int i =0; i < qntJgd; i++)
            {
                Jogador jgds = tabuleiro.VetJgd[i];
                for (int j = 0; j < jogador.VetPeao.Length; j++)
                {
                    Peao peaoRef = jogador.VetPeao[idPeao];

                    Peao peaoComparar = jgds.VetPeao[j];
                    if (peaoRef.Cor != peaoComparar.Cor)
                    {
                        if (peaoRef.Posicao == peaoComparar.Posicao)
                        {
                            Console.WriteLine($"Peão capturado:{peaoComparar.Posicao}");
                            Console.WriteLine($"Peão referência:{peaoRef.Posicao}");
                            peaoComparar.RetornarInicio();
                            Console.WriteLine($"Peão {peaoComparar.cor} do jogador {jgds.Nome} foi capturado");
                            return true;
                        }
                    }
                    
                }
            }
            return false;
        }

        static bool VerificaCasaSegura(int[] casasSeguras, Jogador jogador, int idPeao)
        {
            
            foreach(int posicao in casasSeguras)
            {
                if (jogador.VetPeao[idPeao - 1].Posicao == posicao)
                {
                    return true;
                   
                }
                
            }
            return false;
            
        }


        static void Main(string[] args)
        {
            
            try
            {
                StreamWriter arq = new StreamWriter("log.txt", false, Encoding.UTF8);

                int contQntdDisponiveisSaida = 0, contQntdDisponiveis = 0;
                int[] disponiveis = new int[4];
                int[] disponiveisSaida = new int[4];
                int rodada = 1;
                int[] casasSeguras = {1, 9, 14, 22, 27, 35, 40, 48 };

                Console.WriteLine("Informe a quantidade de jogadores:");
                int qntJgd = int.Parse(Console.ReadLine());
                if (qntJgd < 2 || qntJgd > 4)
                {
                    Console.WriteLine("Número de jogadores inválido");
                }
                else
                {
                    Tabuleiro tabuleiro = new Tabuleiro(qntJgd);
                    tabuleiro.ImprimeJgd();

                    do
                    {

                        Console.WriteLine($"-----------Rodada:{rodada}-----------");
                        arq.WriteLine($"-----------Rodada:{rodada}-----------");

                        Console.WriteLine();


                        for (int i = 0; i < qntJgd; i++)
                        {

                            Jogador jogador = tabuleiro.VetJgd[i];
                            int idPeao = 0;
                            int dado = jogador.LancarDado();
                            Console.WriteLine($"\nDado jogado pelo jogador {jogador.Nome}: {dado}\n");
                            arq.WriteLine($"\nDado jogado pelo jogador {jogador.Nome}: {dado}\n");

                            contQntdDisponiveisSaida = PeoesDisponiveisSaida(jogador, disponiveis);
                            contQntdDisponiveis = PeoesDisponiveis(jogador, disponiveis);

                            if (dado == 6 && contQntdDisponiveisSaida == 4)
                            {
                                Console.WriteLine("\nVez de: " + jogador.Nome + "\n");
                                arq.WriteLine("\nVez de: " + jogador.Nome + "\n");

                                SaidaJogador(jogador, disponiveis, disponiveisSaida, tabuleiro, qntJgd);

                            }
                            else if (contQntdDisponiveis == 1 && dado < 6)
                            {
                                Console.WriteLine($"\nMovimento do jogador {jogador.Nome}");
                                arq.WriteLine($"\nMovimento do jogador {jogador.Nome}");
                                for (int j = 0; j < jogador.VetPeao.Length; j++)
                                {
                                    if (disponiveis[j] > 0)
                                    {
                                        Console.Write("Peão disponível:" + disponiveis[j]);
                                        arq.Write("Peão disponível:" + disponiveis[j]);
                                        idPeao = disponiveis[j];
                                    }
                                }
                                jogador.VetPeao[idPeao - 1].MoverPeao(dado);
                                Console.WriteLine($"\nPosição atual do peão {jogador.Cor}: {jogador.VetPeao[idPeao - 1].Posicao}");
                                arq.WriteLine($"\nPosição atual do peão {jogador.Cor}: {jogador.VetPeao[idPeao - 1].Posicao}");
                            }
                            else if (contQntdDisponiveis == 1 && dado == 6)
                            {
                                SaidaNaoObrigatoria(jogador, disponiveis, disponiveisSaida, dado, tabuleiro, qntJgd);
                            }
                            else if (contQntdDisponiveis > 1)
                            {
                                Console.WriteLine($"\nMovimento do jogador {jogador.Nome}");
                                arq.WriteLine($"\nMovimento do jogador {jogador.Nome}");
                                InformeDisponiveis(jogador, disponiveis);
                                idPeao = int.Parse(Console.ReadLine());
                                jogador.VetPeao[idPeao - 1].MoverPeao(dado);
                                Console.WriteLine($"Posição atual do peão {jogador.Cor}: {jogador.VetPeao[idPeao - 1].Posicao}");
                                arq.WriteLine($"Posição atual do peão {jogador.Cor}: {jogador.VetPeao[idPeao - 1].Posicao}");
                                if (VerificaCasaSegura(casasSeguras, jogador, idPeao))
                                {

                                }
                                else
                                {
                                    if (VerificaCaptura(jogador, tabuleiro, idPeao, qntJgd))
                                    {
                                        jogador.LancarDado();
                                        contQntdDisponiveis = PeoesDisponiveis(jogador, disponiveis);
                                        if (contQntdDisponiveis == 1 && dado == 6)
                                        {
                                            Console.WriteLine($"\nMovimento do jogador {jogador.Nome}");
                                            arq.WriteLine($"\nMovimento do jogador {jogador.Nome}");
                                            for (int j = 0; j < jogador.VetPeao.Length; j++)
                                            {
                                                if (disponiveis[j] > 0)
                                                {
                                                    Console.Write("Peão disponível:" + disponiveis[j]);
                                                    arq.Write("Peão disponível:" + disponiveis[j]);
                                                    idPeao = disponiveis[j];
                                                }
                                            }
                                            jogador.VetPeao[idPeao - 1].MoverPeao(dado);
                                            Console.WriteLine($"\nPosição atual do peão {jogador.Cor}: {jogador.VetPeao[idPeao - 1].Posicao}");
                                            arq.WriteLine($"\nPosição atual do peão {jogador.Cor}: {jogador.VetPeao[idPeao - 1].Posicao}");
                                            if (VerificaCasaSegura(casasSeguras, jogador, idPeao))
                                            {

                                            }
                                        }

                                    }
                                }


                            }


                        }

                        rodada++;

                    } while (!Vitoria(tabuleiro, qntJgd));

                }


                arq.Close();

            }
            catch (Exception e) 
            {
                Console.WriteLine("Excepction: " + e.Message);
            }
            

            Console.ReadLine();
        }
    }
}
