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
        public bool xeque { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            xeque = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            ColocarPecas();
        }
        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RetirarPeca(origem);
            p.incrementarQtdeMovimentos();
            Peca pecaCapturada = tab.RetirarPeca(destino);
            tab.ColocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem,Posicao destino, Peca PecaCapturada)
        {
            Peca p = tab.RetirarPeca(destino);
            p.DecrementarQtdeMovimentos();
            if(PecaCapturada != null)
            {
                tab.ColocarPeca(PecaCapturada,destino);
                capturadas.Remove(PecaCapturada);
            }
            tab.ColocarPeca(p, origem);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
           Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (estaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroExeption("Você não pode se colocar em xeque");
            }

            if(estaEmXeque(Adversaria(JogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }
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

        private Cor Adversaria(Cor cor)
        {
            if(cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca rei(Cor cor)
        {
            foreach(Peca x in PecasEmJogo(cor))
            {
                if(x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool estaEmXeque(Cor cor)
        {
            Peca R = rei(cor);
            if(R == null)
            {
                throw new TabuleiroExeption("não tem rei da cor " + cor + "No tabuleiro");
            }
            foreach (Peca x in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }


            public void ColocarNovaPeca(char coluna, int linha, Peca peca)
            {
                tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
                pecas.Add(peca);
            }


            private void ColocarPecas()
            {
                ColocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
                ColocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));
            ColocarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('e', 1, new Torre(tab, Cor.Branca));
                ColocarNovaPeca('f', 1, new Torre(tab, Cor.Branca));
                ColocarNovaPeca('c', 8, new Torre(tab, Cor.Preta));
                ColocarNovaPeca('e', 8, new Rei(tab, Cor.Preta));
            ColocarNovaPeca('e', 7, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('d', 8, new Torre(tab, Cor.Preta));


            }
        }
    }
