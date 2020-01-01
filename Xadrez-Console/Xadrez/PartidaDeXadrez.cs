using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;
using System.Collections.Generic;

namespace Xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            ColocarPecas();
        }
        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RetirarPeca(origem);
            p.incrementarQtdeMovimentos();
            Peca pecaCapturada = tab.RetirarPeca(destino);
            tab.ColocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            Turno++;
            MudaJogador();
        }

        public void ValidarPosicaoOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null)
            {
                throw new TabuleiroExeption("Não existe peça na posição de origem escolhida");
            }
            if (JogadorAtual != tab.peca(pos).Cor)
            {
                throw new TabuleiroExeption("A peça de origem escolhida não é sua");
            }
            if (!tab.peca(pos).ExisteMovimentoPossivel())
            {
                throw new TabuleiroExeption("Não a movimentos possiveis para a peça de origem escolhida");
            }
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroExeption("Posição de destino Invalida");
            }
        }

        private void MudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.Cor == cor)
                { aux.Add(x); }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.Cor == cor)
                { aux.Add(x); }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }
            public void ColocarNovaPeca(char coluna, int linha, Peca peca)
            {
                tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
                pecas.Add(peca);
            }


            private void ColocarPecas()
            {
                ColocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
                ColocarNovaPeca('d', 1, new Torre(tab, Cor.Branca));
                ColocarNovaPeca('e', 1, new Torre(tab, Cor.Branca));
                ColocarNovaPeca('f', 1, new Torre(tab, Cor.Branca));
                ColocarNovaPeca('c', 8, new Torre(tab, Cor.Preta));
                ColocarNovaPeca('e', 8, new Torre(tab, Cor.Preta));
                ColocarNovaPeca('d', 8, new Torre(tab, Cor.Preta));


            }
        }
    }
