using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoPráticoATP
{
    internal class Peao
    {
        public int id, posicao;
        public string cor;
        public int[] vetP;

        public Peao(string cor, int id, int posicao)
        {
            this.cor = cor;
            this.id = id;
            this.posicao = posicao;

        }

        

        public int[] VetP
        {
            get { return this.vetP; }
            set { this.vetP = value; }
        }
        public string Cor
        {
            get { return this.cor; }
            set { this.cor = value; }
        }

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public int Posicao
        {
            get { return this.posicao; }
            set { this.posicao = value; }
        }

        public void MoverPeao(int dado)
        {
            this.posicao += dado;
        }
        public void RetornarInicio()
        {
            switch (this.cor)
            {
                case "Azul":
                    this.posicao = -1;
                    break;
                case "Vermelho":
                    this.posicao = -2;
                    break;
                case "Verde":
                    this.posicao = -3;
                    break;
                case "Amarelo":
                    this.posicao = -4;
                    break;
            }
            
        }

        public void EntrarJogo()
        {
            this.posicao = this.vetP[0];
        }




    }
}
