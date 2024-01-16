using tabuleiro;
using xadrez_console;

namespace xadrez {
     class partidaDe_Xadrez {

        public Tabuleiro tab {  get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada {  get; private set; }
        public partidaDe_Xadrez() {

            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Amarela;
            terminada = false;
            terminada = false;
            colocarPecas();
        }

        public void realizaJogada(Posicao origem, Posicao destino) {

            executaMovimento(origem, destino);
            turno++;
            mudaJogador();
        }

        public void validarPosicoesDeOrigem(Posicao pos) {

            if (tab.peca(pos) == null) {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida.");
            }

            if (jogadorAtual != tab.peca(pos).cor ) {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }

            if(!tab.peca(pos).existeMovimentosPossiveis()) {
                throw new TabuleiroException("Não há movimentos possíveis para a peça escolhida.");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino) {
            if (tab.peca(origem).podeMoverPara(destino)) {
                throw new TabuleiroException("Posicao de destino inválida!");
            }
        }

        private void mudaJogador() {

            if(jogadorAtual == Cor.Amarela) {
                jogadorAtual = Cor.Verde;
            }
            else {
                jogadorAtual = Cor.Amarela;
            }
        }

        public void executaMovimento(Posicao origem, Posicao destino) {

            Peca p = tab.retirarPeca(origem);
            p.incrementarQtdMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
        }

        private void colocarPecas() {

            tab.colocarPeca(new Torre(tab, Cor.Amarela), new PosicaoXadrez('c',1).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Amarela), new PosicaoXadrez('c', 2).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Amarela), new PosicaoXadrez('d', 2).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Amarela), new PosicaoXadrez('d', 1).toPosicao());

            tab.colocarPeca(new Torre(tab, Cor.Verde), new PosicaoXadrez('h', 1).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Verde), new PosicaoXadrez('e', 2).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Verde), new PosicaoXadrez('h', 2).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Verde), new PosicaoXadrez('e', 1).toPosicao());

        }
    }
}
