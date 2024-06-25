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

        public Peao(int[] vetP)
        {
            this.vetP = vetP;

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
            posicao += dado;
        }
        public void RetornarInicio()
        {
            posicao = -id;
        }


    }
}
