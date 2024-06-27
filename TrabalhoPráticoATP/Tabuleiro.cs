using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoPráticoATP
{
    internal class Tabuleiro
    {
        private int qntJgd;
        private Jogador[] vetJgd = new Jogador[4];

        public Tabuleiro(int qntJgd)
        {
            this.qntJgd = qntJgd;
            for (int i = 0; i < qntJgd; i++)
            {
                //definição dos objetos jogadores com os nomes, id's e cores
                Console.WriteLine($"Informe o nome do jogador {i + 1}");
                string nome = Console.ReadLine();
                string cor;

                switch (i + 1)
                {
                    case 1:
                        cor = "Azul";
                        break;
                    case 2:
                        cor = "Vermelho";
                        break;
                    case 3:
                        cor = "Verde";
                        break;
                    case 4:
                        cor = "Amarelo";
                        break;
                    default:
                        cor = "Não existe";
                        break;
                }
                vetJgd[i] = new Jogador(cor, nome, i + 1);
            }

        }

        public void ImprimeJgd()
        {
            for (int i = 0; i < qntJgd; i++)
            {
                Console.WriteLine("\nNome do Jogador: " + vetJgd[i].Nome);
                Console.WriteLine("Cor do Jogador: " + vetJgd[i].Cor);

            }

        }

        public Jogador[] VetJgd
        {
            get { return this.vetJgd; }

        }


    }
}
