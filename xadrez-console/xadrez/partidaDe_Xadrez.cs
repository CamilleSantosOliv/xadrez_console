using tabuleiro;
using xadrez_console;

namespace xadrez {
     class partidaDe_Xadrez {

        public Tabuleiro tab {  get; private set; }
        private int turno;
        private Cor jogadoeAtual;
        public bool terminada {  get; private set; }
        public partidaDe_Xadrez() {

            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadoeAtual = Cor.Branca;
            terminada = false;
            terminada = false;
            colocarPecas();
        }

        public void executaMovimento(Posicao origem, Posicao destino) {

            Peca p = tab.retirarPeca(origem);
            p.incrementarQtdMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
        }

        private void colocarPecas() {

            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('c',1).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('c', 2).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Branca), new PosicaoXadrez('d', 2).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Branca), new PosicaoXadrez('d', 1).toPosicao());

            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('h', 1).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('e', 2).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Preta), new PosicaoXadrez('h', 2).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Preta), new PosicaoXadrez('e', 1).toPosicao());

        }
    }
}
