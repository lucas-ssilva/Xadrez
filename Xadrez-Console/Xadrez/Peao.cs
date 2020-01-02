using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace Xadrez
{
    class Peao : Peca
    {
        PartidaDeXadrez Partida;
        public Peao(Tabuleiro tab, Cor cor,PartidaDeXadrez partida) : base(cor, tab)
        {
            Partida = partida;
        }
        public override string ToString()
        {
            return "P";
        }

        private bool ExisteInimigo(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p != null && p.Cor != this.Cor;
        }

        private bool livre(Posicao pos)
        {
            return tab.peca(pos) == null;
        }


        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[tab.Linhas, tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            if (Cor == Cor.Branca)
            {
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (tab.PosicaoValida(pos) && livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                if (tab.PosicaoValida(pos) && livre(pos) && QteMovimentos == 0) 
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // en passant
                if (Posicao.Linha == 3)
                {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (tab.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && tab.peca(esquerda) == Partida.vulneravelEnPassant);
                    {
                        mat[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }
                }

                if (Posicao.Linha == 3)
                {
                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (tab.PosicaoValida(direita) && ExisteInimigo(direita) && tab.peca(direita) == Partida.vulneravelEnPassant);
                    {
                        mat[direita.Linha - 1, direita.Coluna] = true;
                    }
                }
            }
            else
            {
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (tab.PosicaoValida(pos) && livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                if (tab.PosicaoValida(pos) && livre(pos) && QteMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // en passant
                if(Posicao.Linha == 4)
                {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (tab.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && tab.peca(esquerda) == Partida.vulneravelEnPassant);
                    {
                        mat[esquerda.Linha + 1, esquerda.Coluna] = true;
                    }
                }

                if (Posicao.Linha == 4)
                {
                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (tab.PosicaoValida(direita) && ExisteInimigo(direita) && tab.peca(direita) == Partida.vulneravelEnPassant);
                    {
                        mat[direita.Linha + 1, direita.Coluna] = true;
                    }
                }
            }

            return mat;
        }
    }
}