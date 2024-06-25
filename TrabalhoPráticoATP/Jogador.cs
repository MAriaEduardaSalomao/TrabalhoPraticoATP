using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoPráticoATP
{
    internal class Jogador
    {
        private string cor, nome;
        private int id;
        private Peao[] vetPeao = new Peao[4];

        Random r = new Random();
        public Jogador(string cor, string nome, int id)
        {
            int[] vetP2Vm = {1,  2,  3,  4,  5,  6,  7,  8,  9, 10,
   11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
   21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
   31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
   41, 42, 43, 44, 45, 46, 47, 48, 49, 50,
   51, 52, 53, 54, 55, 56, 57};

            int[] vetP1Az = {40, 41, 42, 43, 44, 45, 46, 47, 48, 49,
    50, 51, 52, 1, 2, 3, 4, 5, 6, 7,
    8, 9, 10, 11, 12, 13, 14, 15, 16, 17,
    18, 19, 20, 21, 22, 23, 24, 25, 26, 27,
    28, 29, 30, 31, 32, 33, 34, 35, 36, 37,
    38, 39, 53, 54, 55, 56, 57};

            int[] vetP3Vd = {14, 15, 16, 17, 18, 19, 20, 21, 22, 23,
    24, 25, 26, 27, 28, 29, 30, 31, 32, 33,
    34, 35, 36, 37, 38, 39, 40, 41, 42, 43,
    44, 45, 46, 47, 48, 49, 50, 51, 52, 1,
    2, 3, 4, 5, 6, 7, 8, 9, 10, 11,
    12, 13, 53, 54, 55, 56, 57 };

            int[] vetP4Am = { 27, 28, 29, 30, 31, 32, 33, 34, 35, 36,
    37, 38, 39, 40, 41, 42, 43, 44, 45, 46,
    47, 48, 49, 50, 51, 52, 1, 2, 3, 4,
    5, 6, 7, 8, 9, 10, 11, 12, 13, 14,
    15, 16, 17, 18, 19, 20, 21, 22, 23, 24,
    25, 26, 53, 54, 55, 56, 57};

            this.cor = cor;
            this.nome = nome;
            this.id = id;
            for (int i = 0; i < vetPeao.Length; i++)
            {

                this.vetPeao[i] = new Peao(cor, i + 1, (i + 1) * (-1));
                switch (i)
                {
                    case 0:
                        this.vetPeao[i].VetP = vetP1Az;
                        break;
                    case 1:
                        this.vetPeao[i].VetP = vetP2Vm;
                        break;
                    case 2:
                        this.vetPeao[i].VetP = vetP3Vd;
                        break;
                    case 3:
                        this.vetPeao[i].VetP = vetP4Am;
                        break;
                }
            }
        }



        public string Cor
        {
            get { return this.cor; }
            set { this.cor = value; }
        }

        public string Nome
        {
            get { return this.nome; }
            set { this.nome = value; }
        }

        public Peao[] VetPeao
        {
            get { return this.vetPeao; }
        }
        public int LancarDado()
        {
            return r.Next(1, 7);
        }

    }
}
