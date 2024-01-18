using xadrez;
using tabuleiro;

namespace xadrez_console {
     class Tela {

        public static void imprimirPartida(partidaDe_Xadrez partida) {
            imprimirTabuleiro(partida.tab);
            Console.WriteLine();
            imprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Turno: " + partida.turno);
            Console.WriteLine("Aguardando jogada: " + partida.jogadorAtual);
        }

        public static void imprimirPecasCapturadas(partidaDe_Xadrez partida) {

            Console.WriteLine("Pecas capturadas: ");
            Console.WriteLine();
            Console.Write("Amarelas: ");
            imprimirConjunto(partida.pecasCapturadas(Cor.Amarela));
            Console.WriteLine();
            Console.Write("Verdes: ");
            imprimirConjunto(partida.pecasCapturadas(Cor.Verde));
        }

        public static void imprimirConjunto(HashSet<Peca> conjunto) {

            Console.Write("[");
            foreach (Peca x in conjunto) {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }

        public static void imprimirTabuleiro(Tabuleiro tab) {

            for (int i = 0; i < tab.linhas; i++) {

                Console.Write(8 - i + "  ");

                for (int j = 0; j < tab.colunas; j++) {
                        imprimirPeca(tab.peca(i, j));
                        Console.Write("");
                }
                Console.WriteLine();
            }

            Console.WriteLine("   A B C D E F G H");
        }

        public static void imprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis) {

            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < tab.linhas; i++) {

                Console.Write(8 - i + " ");

                for (int j = 0; j < tab.colunas; j++) {

                    if (posicoesPossiveis[i, j]) {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else {
                        Console.BackgroundColor = fundoOriginal; 
                    }
                    imprimirPeca(tab.peca(i, j));
                    Console.BackgroundColor = fundoOriginal;

                }
                Console.WriteLine();
            }

            Console.WriteLine("  A B C D E F G H");
        }

        public static PosicaoXadrez lerPosicaoXadrez() {

            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + " ");
            return new PosicaoXadrez(coluna, linha);
        }

        public static void imprimirPeca(Peca peca) {

            if (peca == null) {
                Console.Write("- ");
            }
            else
            {            
                if (peca.cor == Cor.Amarela) {
                    ConsoleColor cColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = cColor;
                }
                else {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}
