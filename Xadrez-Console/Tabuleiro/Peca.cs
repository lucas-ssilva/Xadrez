namespace tabuleiro
{
    abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QteMovimentos { get; protected set; }
        public Tabuleiro tab { get; protected set; }

        public Peca(Cor cor, Tabuleiro tab)
        {
            Posicao = null;
            Cor = cor;
            QteMovimentos = 0;
            this.tab = tab;
        }
        public void incrementarQtdeMovimentos()
        {
            QteMovimentos++;
        }
        public void DecrementarQtdeMovimentos()
        {
            QteMovimentos--;
        }


        public bool ExisteMovimentoPossivel()
        {
            bool[,] mat = MovimentosPossiveis();
            for(int i = 0; i < tab.Linhas; i++)
            {
                for(int j = 0; j <tab.Colunas; j++)
                {
                    if(mat[i,j] == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool movimentoPossivel(Posicao pos)
        {
            return MovimentosPossiveis()[pos.Linha, pos.Coluna];
        }

        public abstract bool[,] MovimentosPossiveis();
        

        
    }
}
