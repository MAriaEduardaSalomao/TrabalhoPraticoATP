using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoPráticoATP
{
    internal class Program
    {


        static int PeoesDisponiveis(Jogador jogador, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveisSaida)
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

        static int PeoesDisponiveisSaida(Jogador jogador, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveisSaida)
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

        static int IndexVetor(Jogador jogador, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida)
        {
            for (int i = 0; i < jogador.VetPeao[idPeao - 1].vetP.Length; i++)
            {
                if (jogador.VetPeao[idPeao - 1].vetP[i] == posicao)
                {
                    return i;
                }
            }

            return -1000;

        }

        static bool Vitoria(Jogador jogador, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida)
        {

            for (int i = 0; i < qntJgd; i++)
            {
                Jogador jgd = tabuleiro.VetJgd[i];
                bool jogadorVenceu = true;
                for (int j = 0; j < jgd.VetPeao.Length; j++)
                {
                    Peao peao = jgd.VetPeao[j];


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

        static int InformeDisponiveisEId(Jogador jogador, int[] disponiveis, int[] disponiveisSaida, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida)
        {
            Console.WriteLine("Informe o número do peão que deseja mover\nNúmeros Disponíveis:");
            for (int i = 0; i < jogador.VetPeao.Length; i++)
            {
                if (disponiveis[i] > 0)
                {
                    Console.Write($"({disponiveis[i]}) ");
                }
            }
            int idPeao=int.Parse(Console.ReadLine());
        }

        static void EntrarJogo(Jogador jogador, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida)
        {
            Console.WriteLine($"\nMovimento do jogador {jogador.Nome}");
            jogador.VetPeao[idPeao - 1].EntrarJogo();
            Console.WriteLine($"\nPosição atual do peão {jogador.VetPeao[idPeao - 1].Id} {jogador.Cor} : {jogador.VetPeao[idPeao - 1].Posicao}");
            VerificaCapturaECasaSegura(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida);

        }

        static void MoverPeao(Jogador jogador, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida)
        {
            Console.WriteLine($"\nMovimento do jogador {jogador.Nome}");
            int posicaoPeao = jogador.VetPeao[idPeao - 1].Posicao;
            int indexPosicao = IndexVetor(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida);
            jogador.VetPeao[idPeao - 1].MoverPeao(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida);
            Console.WriteLine($"\nPosição atual do peão {jogador.VetPeao[idPeao - 1].Id} {jogador.Cor} : {jogador.VetPeao[idPeao - 1].Posicao}");
            VerificaCapturaECasaSegura(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida);
        }


        static void SaidaJogador(Jogador jogador, int[] disponiveis, int[] disponiveisSaida, int posicao, Tabuleiro tabuleiro, int qntJgd, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida)
        {
            int qntdDisponiveis = PeoesDisponiveisSaida(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida);
            int idPeao= InformeDisponiveisEId(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida);
            //int idPeao = int.Parse(Console.ReadLine());

            EntrarJogo(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida);
            
            int dado = jogador.LancarDado();   
            Console.WriteLine($"\nDado jogado pelo jogador {jogador.Nome}: {dado}");

            if (dado == 6)
            {
              
                qntdDisponiveis = PeoesDisponiveisSaida(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida);
                
                RetiraMaisPeao(jogador, disponiveisSaida, idPeao, tabuleiro, qntJgd, dado);
                jogador.LancarDado();
                Console.WriteLine($"\nDado jogado pelo jogador {jogador.Nome}: {dado}");
                if (dado == 6)
                {
                    Console.WriteLine("Quantidade de 6's atingida!\nPassa a vez");
                }
                else
                {
                    MoverPeao(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);
                   
                }
            }
            else
            {
                MoverPeao(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);
               
            }

        }

        static void RetiraMaisPeao(Jogador jogador, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida)
        {
            Console.WriteLine("\nDeseja retirar mais algum peão da casa? Digite 's' ou 'n':");
            char resposta = char.Parse(Console.ReadLine());
            if (resposta == 's')
            {
                int qntdDisponiveis = PeoesDisponiveisSaida(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);

                idPeao=InformeDisponiveisEId(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);
                //idPeao = int.Parse(Console.ReadLine());
                Console.WriteLine($"\nMovimento do jogador {jogador.Nome}");
                EntrarJogo(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);
            }
            else if (resposta == 'n')
            {
                MoverPeao(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);

            }
            else
            {
                Console.WriteLine("Valor informado incorreto");

            }
        }


        static bool VerificaCaptura(Jogador jogador, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida)
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
                            Console.WriteLine($"Peão captor:{peaoRef.Posicao}");
                            peaoComparar.RetornarInicio();
                            Console.WriteLine($"Peão {peaoComparar.Id} {peaoComparar.cor} do jogador {jgds.Nome} foi capturado");
                            return true;
                        }
                    }
                    
                }
            }
            return false;
        }

        static bool VerificaCasaSegura(Jogador jogador, int[] disponiveis, int[] disponiveisSaida, int idPeao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida)
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

        static bool VerificaCapturaECasaSegura(Jogador jogador, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida)
        {
            if (VerificaCasaSegura(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado))
            {
                return false;
            }
            else
            {
                if (VerificaCaptura(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        static void UmPeaoDadoSeis(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida)
        {
            int idPeao = 0;
            Console.WriteLine("\nDeseja retirar mais algum peão da casa? Digite 's' ou 'n':");
            char resposta = char.Parse(Console.ReadLine());
            if (resposta == 's')
            {

                int qntdDisponiveis = PeoesDisponiveisSaida(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);

                idPeao=InformeDisponiveisEId(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);

                //idPeao = int.Parse(Console.ReadLine());
                Console.WriteLine($"\nMovimento do jogador {jogador.Nome}");
                EntrarJogo(jogador, idPeao);
                
                jogador.LancarDado();
                if (dado == 6)
                {

                    RetiraMaisPeao(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);
                    jogador.LancarDado();
                    Console.WriteLine($"\nDado jogado pelo jogador {jogador.Nome}: {dado}");

                    if(dado == 6)
                    {
                        Console.WriteLine("Quantidade de 6's alcançada");
                    }
                    else
                    {
                        MoverPeao(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);
                    }

                }
                else
                {
                    MoverPeao(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);
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

                MoverPeao(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);

            }
            else
            {
                Console.WriteLine("Valor informado incorreto");
            }
        }


        static void UmPeaoDadoMenosSeis(Jogador jogador, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida)
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
            MoverPeao(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);
           
            

        }

        static void MaisDeUmPeaoDadoMenosSeis(Jogador jogador, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida)
        {
            contQntdDisponiveis = PeoesDisponiveis(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);
            idPeao = InformeDisponiveisEId(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);
            //idPeao = int.Parse(Console.ReadLine());
            Console.WriteLine($"\nMovimento do jogador {jogador.Nome}");

            int posicaoPeao = jogador.VetPeao[idPeao - 1].Posicao;
            int indexPosicao = IndexVetor(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);

            if (indexPosicao > 50)
            {
                RetaFinal(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);
            }
            else
            {
                MoverPeao(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);
            }

        }

        

        

        static void Jogo(Jogador jogador, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida)
        {
            jogador.LancarDado();
            contQntdDisponiveis = PeoesDisponiveis(jogador, disponiveis);
            if (dado == 6 && contQntdDisponiveisSaida == 4)
            {
                Console.WriteLine($"\nVez de: {jogador.Nome}");

                SaidaJogador(jogador, disponiveis, disponiveisSaida, tabuleiro, qntJgd);


            }
            else if (contQntdDisponiveis == 1 && dado < 6)
            {
                UmPeaoDadoMenosSeis(idPeao, jogador, disponiveis, dado, qntJgd, tabuleiro);
            }
            else if (contQntdDisponiveis == 1 && dado == 6)
            {
                UmPeaoDadoSeis(jogador, disponiveis, disponiveisSaida, dado, tabuleiro, qntJgd);
            }
            else if (contQntdDisponiveis > 1)
            {
                contQntdDisponiveis = PeoesDisponiveis(jogador, disponiveis);
                InformeDisponiveis(jogador, disponiveis);
                idPeao = int.Parse(Console.ReadLine());
                Console.WriteLine($"\nMovimento do jogador {jogador.Nome}");

                int posicaoPeao = jogador.VetPeao[idPeao - 1].Posicao;
                int indexPosicao = CompatibilidadePosicaoVetor(idPeao, posicaoPeao, jogador);

                if (indexPosicao > 50)
                {
                    RetaFinal(jogador, dado, idPeao, indexPosicao, contQntdDisponiveis, disponiveis);
                }
                else
                {
                    MoverPeao(jogador, idPeao, dado);
                }
            }
        }

        static void RetaFinal(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida)
        {
            if (dado <= (jogador.VetPeao[idPeao - 1].vetP.Length - indexPosicao))
            {
                Console.WriteLine($"Reta Final pra jogador {jogador.Nome}");
                contQntdDisponiveis = PeoesDisponiveis(jogador, disponiveis);
                InformeDisponiveis(jogador, disponiveis);
                idPeao = int.Parse(Console.ReadLine());
                jogador.VetPeao[idPeao - 1].MoverPeao(dado, indexPosicao);

            }
            else
            {
                Console.WriteLine("Valor do dado inválido");
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
                            int dado = jogador.LancarDado(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);
                            Console.WriteLine($"\nDado jogado pelo jogador {jogador.Nome}: {dado}");
                            //arq.WriteLine($"\nDado jogado pelo jogador {jogador.Nome}: {dado}\n");

                            contQntdDisponiveisSaida = PeoesDisponiveisSaida(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);
                            contQntdDisponiveis = PeoesDisponiveis(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);

                            if (dado == 6 && contQntdDisponiveisSaida == 4)
                            {
                                Console.WriteLine($"\nVez de: {jogador.Nome}");
                                //arq.WriteLine("\nVez de: " + jogador.Nome + "\n");

                                SaidaJogador(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);
                            while (VerificaCapturaECasaSegura(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado))
                            {
                                jogador.LancarDado();
                                contQntdDisponiveis = PeoesDisponiveis(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);
                                if (dado == 6 && contQntdDisponiveisSaida == 4)
                                {
                                    Console.WriteLine($"\nVez de: {jogador.Nome}");

                                    SaidaJogador(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);
                                    

                                }
                                else if (contQntdDisponiveis == 1 && dado < 6)
                                {
                                    UmPeaoDadoMenosSeis(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);
                                }
                                else if (contQntdDisponiveis == 1 && dado == 6)
                                {
                                    UmPeaoDadoSeis(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);
                                }
                                else if (contQntdDisponiveis > 1 && dado<6)
                                {
                                    contQntdDisponiveis = PeoesDisponiveis(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);
                                    InformeDisponiveis(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);
                                    idPeao = int.Parse(Console.ReadLine());
                                    Console.WriteLine($"\nMovimento do jogador {jogador.Nome}");

                                    int posicaoPeao = jogador.VetPeao[idPeao - 1].Posicao;
                                    int indexPosicao = CompatibilidadePosicaoVetor(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);

                                    if (indexPosicao > 50)
                                    {
                                        RetaFinal(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);
                                    }
                                    else
                                    {
                                        MoverPeao(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);
                                    }
                                }
                            }
                        }
                            else if (contQntdDisponiveis == 1 && dado < 6)
                            {
                                UmPeaoDadoMenosSeis(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);
                                
                                
                            }
                            else if (contQntdDisponiveis == 1 && dado == 6)
                            {
                                UmPeaoDadoSeis(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);
                            }
                            else if (contQntdDisponiveis > 1)
                            {
                                MaisDeUmPeao(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado);


                            }


                        }

                        rodada++;

                    } while (!Vitoria(Jogador jogador, int indexPosicao, int[] disponiveis, int[] disponiveisSaida, int idPeao, int posicao, Tabuleiro tabuleiro, int qntJgd, int dado, int[] casasSeguras, int contQntdDisponiveis, int contQntdDisponiveisSaida, jogador, idPeao, dado));

                }



                // console.WriteLine("Criando registros do jogo.");

                // try
                // {
                //     StreamWriter arquivoLog = new StreamWriter("arquivoLog.txt", true, Encoding.UTF8);
                //     // O arquivo com os registros do jogo foi criado. Não coloquei um caminho certo, entao ele vai ser criado no mesmo lugar em que o código estiver, provavelmente nas pastas "bin/debug/".

                //     // Começa aqui a série de C.WL() para redigir o código todo.
                //     // arquivoLog.WriteLine("Informe o número do peão que deseja mover\nNúmeros Disponíveis:");
                //     // arquivoLog.WriteLine($"Opçao escolhida:{idpeao}");
                //     // Console.WriteLine("Informe o número do peão que deseja mover\nNúmeros Disponíveis:");
                //     // Console.WriteLine($"\nMovimento do jogador {jogador.Nome}");
                //     // Console.WriteLine($"\nPosição atual do peão {jogador.VetPeao[idPeao - 1].Id} {jogador.Cor} : {jogador.VetPeao[idPeao - 1].Posicao}");
                //     // Console.WriteLine($"\nDado jogado pelo jogador {jogador.Nome}: {dado}");
                //     // Console.WriteLine("Quantidade de 6's atingida!\nPassa a vez");
                //     // Console.WriteLine("\nDeseja retirar mais algum peão da casa? Digite 's' ou 'n':");
                //     // Console.WriteLine($"\nMovimento do jogador {jogador.Nome}");


                //     arquivoLog.Write(Main);

                //     //Fecha o arquivo
                //     arquivoLog.Close();
                //     Console.WriteLine("Arquivo com os registros do jogo foi criado ou atualiado!");
                // }
                // catch (Exception e)
                // { Console.WriteLine("Exception: " + e.Message);
                // } catch (FileNotFoundException e)
                // { Console.WriteLine("O arquivo não foi encontrado: " + e.Message);
                // } catch (DirectoryNotFoundException e)
                // { Console.WriteLine("O diretório não foi encontrado: " + e.Message);
                // } catch (IOException e)
                // { Console.WriteLine("O arquivo não pode ser aberto: " + e.Message);
                // } // Todas as mensagens em caso de erro.

            
            

            Console.ReadLine();
        }
    }
}
