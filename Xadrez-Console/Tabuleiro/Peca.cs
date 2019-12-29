﻿namespace Tabuleiro
{
    class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QteMovimentos { get; protected set; }
        public Tabuleiro tab { get; protected set; }

        public Peca(Posicao posicao, Cor cor, int qteMovimentos, Tabuleiro tab)
        {
            Posicao = posicao;
            Cor = cor;
            this.QteMovimentos = 0;
            this.tab = tab;
        }
    }
}
