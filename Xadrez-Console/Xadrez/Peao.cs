using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace Xadrez
{
    class Peao : Peca
    {
        public Peao(Tabuleiro tab, Cor cor) : base(cor, tab)
        {

        }
        public override string ToString()
        {
            return "P";
        }
    }
}