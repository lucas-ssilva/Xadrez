using System;
using tabuleiro;
using Xadrez;

namespace Xadrez_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.Terminada)
                {
                    try
                    {
                        Console.Clear();
                        tela.ImprimirPartida(partida);


                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Posicao origem = tela.LerPosicaoXadrez().ToPosicao();
                        partida.ValidarPosicaoOrigem(origem);

                        bool[,] posicoesPossiveis = partida.tab.peca(origem).MovimentosPossiveis();

                        Console.Clear();
                        tela.ImprimirTabuleiro(partida.tab, posicoesPossiveis);
                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Posicao destino = tela.LerPosicaoXadrez().ToPosicao();
                        partida.ValidarPosicaoDestino(origem, destino);

                        partida.RealizaJogada(origem, destino);
                    }
                    catch(TabuleiroExeption e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
               


            }
            catch(TabuleiroExeption e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
