using System;
using System.Collections.Generic;
using System.Globalization;
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
            Console.WriteLine($"Peão {jogador.VetPeao[idPeao - 1].Id} {jogador.Cor} entrou no jogo!\nPosição atual: {jogador.VetPeao[idPeao - 1].Posicao}");

            int dado = jogador.LancarDado();
            
            Console.WriteLine($"\nDado jogado pelo jogador {jogador.Nome}: {dado}");

            if (dado == 6)
            {
              
                qntdDisponiveis = PeoesDisponiveisSaida(jogador, disponiveisSaida);
                
                RetiraMaisPeao(jogador, disponiveisSaida, idPeao, tabuleiro, qntJgd, dado);
                jogador.LancarDado();
                Console.WriteLine($"\nDado jogado pelo jogador {jogador.Nome}: {dado}");
                if (dado == 6)
                {
                    Console.WriteLine("Quantidade de 6's atingida!\nPassa a vez");
                }
                else
                {
                    MoverPeao(jogador, idPeao, dado, tabuleiro, qntJgd);
                }
            }
            else
            {
                MoverPeao(jogador, idPeao, dado, tabuleiro, qntJgd);
            }

        }

        static int CompatibilidadePosicaoVetor(int idPeao, int posicao, Jogador jogador)
        {
            for (int i=0; i < jogador.VetPeao[idPeao - 1].vetP.Length; i++)
            {
                if(jogador.VetPeao[idPeao - 1].vetP[i] == posicao)
                {
                    return i;
                }
            }

            return -1000;
           
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

                InformeDisponiveis(jogador, disponiveisSaida);

                idPeao = int.Parse(Console.ReadLine());
                Console.WriteLine($"\nMovimento do jogador {jogador.Nome}");
                jogador.VetPeao[idPeao - 1].EntrarJogo();
                Console.WriteLine($"\nPosição atual do peão {jogador.VetPeao[idPeao - 1].Id} {jogador.Cor} : {jogador.VetPeao[idPeao - 1].Posicao}");
                VerificaCaptura(jogador, tabuleiro, idPeao, qntJgd);
                jogador.LancarDado();
                if (dado == 6)
                {
                   
                        RetiraMaisPeao(jogador, disponiveisSaida, idPeao, tabuleiro, qntJgd, dado);
                        jogador.LancarDado();
                        Console.WriteLine($"\nDado jogado pelo jogador {jogador.Nome}: {dado}");
                        

                }
                else
                {
                    MoverPeao(jogador, idPeao, dado, tabuleiro, qntJgd);
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
                MoverPeao(jogador, idPeao, dado, tabuleiro, qntJgd);

            }
            else
            {
                Console.WriteLine("Valor informado incorreto");
            }
        }


        static void MoverPeao(Jogador jogador, int idPeao, int dado, Tabuleiro tabuleiro, int qntJgd)
        {
            Console.WriteLine($"\nMovimento do jogador {jogador.Nome}");
            int posicaoPeao = jogador.VetPeao[idPeao - 1].Posicao;
            int indexPosicao = CompatibilidadePosicaoVetor(idPeao, posicaoPeao, jogador);
            jogador.VetPeao[idPeao - 1].MoverPeao(dado, indexPosicao);
            Console.WriteLine($"\nPosição atual do peão {jogador.VetPeao[idPeao - 1].Id} {jogador.Cor} : {jogador.VetPeao[idPeao - 1].Posicao}");
            VerificaCaptura(jogador, tabuleiro, idPeao, qntJgd);
        }


        static bool RetiraMaisPeao(Jogador jogador, int[] disponiveisSaida, int idPeao, Tabuleiro tabuleiro, int qntJgd, int dado)
        {
            Console.WriteLine("\nDeseja retirar mais algum peão da casa? Digite 's' ou 'n':");
            char resposta = char.Parse(Console.ReadLine());
            if (resposta == 's')
            {

                int qntdDisponiveis = PeoesDisponiveisSaida(jogador, disponiveisSaida);

                InformeDisponiveis(jogador, disponiveisSaida);

                idPeao = int.Parse(Console.ReadLine());
                Console.WriteLine($"\nMovimento do jogador {jogador.Nome}");
                jogador.VetPeao[idPeao - 1].EntrarJogo();
                Console.WriteLine($"\nPosição atual do peão {jogador.VetPeao[idPeao - 1].Id} {jogador.Cor} : {jogador.VetPeao[idPeao - 1].Posicao}");
                //VerificaCasaSegura()
                VerificaCaptura(jogador, tabuleiro, idPeao, qntJgd);
                return true;

            }
            else if (resposta == 'n')
            {
                MoverPeao(jogador, idPeao, dado, tabuleiro, qntJgd);
                return false;
            }
            else
            {
                Console.WriteLine("Valor informado incorreto");
                return false;
            }
        }
        static bool VerificaCaptura(Jogador jogador,Tabuleiro tabuleiro, int idPeao, int qntJgd)
        {

            for (int i =0; i < qntJgd; i++)
            {
                Jogador jgds = tabuleiro.VetJgd[i];
                for (int j = 0; j < jogador.VetPeao.Length; j++)
                {
                    Peao peaoRef = jogador.VetPeao[idPeao - 1];

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

        static void UmPeaoDadoMenosSeis(int idPeao, Jogador jogador, int[] disponiveis, int dado)
        {
            Console.WriteLine($"\nMovimento do jogador {jogador.Nome}");
            
            for (int j = 0; j < jogador.VetPeao.Length; j++)
            {
                if (disponiveis[j] > 0)
                {
                    Console.Write("Peão disponível:" + disponiveis[j]);
                   
                    idPeao = disponiveis[j];
                }
            }
            int posicaoPeao = jogador.VetPeao[idPeao - 1].Posicao;
            int indexPosicao = CompatibilidadePosicaoVetor(idPeao, posicaoPeao, jogador);
            jogador.VetPeao[idPeao - 1].MoverPeao(dado, indexPosicao);
            Console.WriteLine($"\nPosição atual do peão {jogador.VetPeao[idPeao - 1].Id} {jogador.Cor} : {jogador.VetPeao[idPeao - 1].Posicao}");
            

        }

        static void MaisDeUmPeao()
        {

        }



        static void VerificaCapturaECasaSegura(int[] casasSeguras, Jogador jogador, int idPeao, Tabuleiro tabuleiro, int qntJgd, int dado,StreamWriter arq, int[] disponiveis, int[] disponiveisSaida)
        {
            int contQntdDisponiveis = PeoesDisponiveis(jogador, disponiveis);
            if (VerificaCasaSegura(casasSeguras, jogador, idPeao))
            {

            }
            else
            {
                if (VerificaCaptura(jogador, tabuleiro, idPeao, qntJgd))
                {
                    jogador.LancarDado();
                    Console.WriteLine($"\nDado jogado pelo jogador {jogador.Nome}: {dado}");
                    if (dado==6&& contQntdDisponiveis == 1)
                    {
                        UmPeaoDadoMenosSeis(idPeao, jogador, disponiveis, dado);
                    }else if (contQntdDisponiveis ==1 && dado==6)
                    {
                        SaidaNaoObrigatoria(jogador, disponiveis, disponiveisSaida, dado, tabuleiro, qntJgd);
                    }
                }
                   
            }
        }
        static void Main(string[] args)
        {
            
            
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
                        //arq.WriteLine($"-----------Rodada:{rodada}-----------");

                        Console.WriteLine();


                        for (int i = 0; i < qntJgd; i++)
                        {

                            Jogador jogador = tabuleiro.VetJgd[i];
                            int idPeao = 0;
                            int dado = jogador.LancarDado();
                            Console.WriteLine($"\nDado jogado pelo jogador {jogador.Nome}: {dado}");
                            //arq.WriteLine($"\nDado jogado pelo jogador {jogador.Nome}: {dado}\n");

                            contQntdDisponiveisSaida = PeoesDisponiveisSaida(jogador, disponiveis);
                            contQntdDisponiveis = PeoesDisponiveis(jogador, disponiveis);

                            if (dado == 6 && contQntdDisponiveisSaida == 4)
                            {
                                Console.WriteLine($"\nVez de: {jogador.Nome}");
                                //arq.WriteLine("\nVez de: " + jogador.Nome + "\n");

                                SaidaJogador(jogador, disponiveis, disponiveisSaida, tabuleiro, qntJgd);

                            }
                            else if (contQntdDisponiveis == 1 && dado < 6)
                            {
                                UmPeaoDadoMenosSeis(idPeao, jogador, disponiveis, dado);
                                //Console.WriteLine($"\nMovimento do jogador {jogador.Nome}");
                                ////arq.WriteLine($"\nMovimento do jogador {jogador.Nome}");
                                //for (int j = 0; j < jogador.VetPeao.Length; j++)
                                //{
                                //    if (disponiveis[j] > 0)
                                //    {
                                //        Console.Write("Peão disponível:" + disponiveis[j]);
                                //        //arq.Write("Peão disponível:" + disponiveis[j]);
                                //        idPeao = disponiveis[j];
                                //    }
                                //}
                                //int posicaoPeao = jogador.VetPeao[idPeao - 1].Posicao;
                                //int indexPosicao = CompatibilidadePosicaoVetor(idPeao, posicaoPeao, jogador);
                                //jogador.VetPeao[idPeao - 1].MoverPeao(dado, indexPosicao);
                                //Console.WriteLine($"\nPosição atual do peão {jogador.VetPeao[idPeao - 1].Id} {jogador.Cor} : {jogador.VetPeao[idPeao - 1].Posicao}");
                                //arq.WriteLine($"\nPosição atual do peão {jogador.Cor}: {jogador.VetPeao[idPeao - 1].Posicao}");
                                
                            }
                            else if (contQntdDisponiveis == 1 && dado == 6)
                            {
                                SaidaNaoObrigatoria(jogador, disponiveis, disponiveisSaida, dado, tabuleiro, qntJgd);
                            }
                            else if (contQntdDisponiveis > 1)
                            {
                                contQntdDisponiveis = PeoesDisponiveis(jogador,disponiveis);
                                InformeDisponiveis(jogador, disponiveis);
                                idPeao = int.Parse(Console.ReadLine());
                                Console.WriteLine($"\nMovimento do jogador {jogador.Nome}");
                                //arq.WriteLine($"\nMovimento do jogador {jogador.Nome}");
                                int posicaoPeao = jogador.VetPeao[idPeao - 1].Posicao;        //
                                int indexPosicao = CompatibilidadePosicaoVetor(idPeao, posicaoPeao, jogador);
                                if (indexPosicao > 50)
                                {
                                    if (dado <= (jogador.VetPeao[idPeao].vetP.Length - indexPosicao))
                                    {
                                        Console.WriteLine($"Reta Final pra jogador {jogador.Nome}");
                                        contQntdDisponiveis = PeoesDisponiveis(jogador, disponiveis);
                                        InformeDisponiveis(jogador, disponiveis);
                                        idPeao = int.Parse(Console.ReadLine());
                                        jogador.VetPeao[idPeao - 1].MoverPeao(dado, indexPosicao);
                                        Console.WriteLine($"\nPosição atual do peão {jogador.VetPeao[idPeao - 1].Id} {jogador.Cor} : {jogador.VetPeao[idPeao - 1].Posicao}");
                                    }
                                }
                                else
                                {
                                    jogador.VetPeao[idPeao - 1].MoverPeao(dado, indexPosicao);
                                    Console.WriteLine($"\nPosição atual do peão {jogador.VetPeao[idPeao - 1].Id} {jogador.Cor} : {jogador.VetPeao[idPeao - 1].Posicao}");
                                    //arq.WriteLine($"Posição atual do peão {jogador.Cor}: {jogador.VetPeao[idPeao - 1].Posicao}");
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
                                                //arq.WriteLine($"\nMovimento do jogador {jogador.Nome}");
                                                for (int j = 0; j < jogador.VetPeao.Length; j++)
                                                {
                                                    if (disponiveis[j] > 0)
                                                    {
                                                        Console.Write("Peão disponível:" + disponiveis[j]);
                                                        //arq.Write("Peão disponível:" + disponiveis[j]);
                                                        idPeao = disponiveis[j];
                                                    }
                                                }
                                                posicaoPeao = jogador.VetPeao[idPeao - 1].Posicao;
                                                indexPosicao = CompatibilidadePosicaoVetor(idPeao, posicaoPeao, jogador);
                                                jogador.VetPeao[idPeao - 1].MoverPeao(dado, indexPosicao);
                                                Console.WriteLine($"\nPosição atual do peão {jogador.VetPeao[idPeao - 1].Id} {jogador.Cor} : {jogador.VetPeao[idPeao - 1].Posicao}");
                                                //arq.WriteLine($"\nPosição atual do peão {jogador.Cor}: {jogador.VetPeao[idPeao - 1].Posicao}");
                                                if (VerificaCasaSegura(casasSeguras, jogador, idPeao))
                                                {
                                                    
                                                }
                                            }

                                        }
                                    }
                                }
                                


                            }


                        }

                        rodada++;

                    } while (!Vitoria(tabuleiro, qntJgd));

                }


                

            
            

            Console.ReadLine();
        }
    }
}
