using tabuleiro;
using xadrez_console;

namespace xadrez {
     class partidaDe_Xadrez {

        public Tabuleiro tab {  get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada {  get; private set; }

        private HashSet<Peca> pecas;

        private HashSet<Peca> capturadas;

        public partidaDe_Xadrez() {

            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Amarela;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
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
            if (!tab.peca(origem).podeMoverPara(destino)) {
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

        public HashSet<Peca> pecasCapturadas(Cor cor) {
            HashSet<Peca> aux  = new HashSet<Peca>();
            foreach (Peca x in capturadas) {
                if(x.cor == cor) {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor) {
            HashSet<Peca>aux = new HashSet<Peca>();
            foreach(Peca x in pecas) {
                if (x.cor == cor) {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }


        public void executaMovimento(Posicao origem, Posicao destino) {

            Peca p = tab.retirarPeca(origem);
            p.incrementarQtdMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null) {
                capturadas.Add(pecaCapturada);
            }
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca) {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);

        }


        private void colocarPecas() {

            colocarNovaPeca('a', 1, new Torre(tab, Cor.Amarela));
            colocarNovaPeca('b', 1, new Torre(tab, Cor.Amarela));
            colocarNovaPeca('c', 1, new Torre(tab, Cor.Amarela));
            colocarNovaPeca('d', 1, new Torre(tab, Cor.Amarela));
            colocarNovaPeca('e', 1, new Torre(tab, Cor.Amarela));
            colocarNovaPeca('f', 1, new Rei(tab, Cor.Amarela));
            colocarNovaPeca('g', 1, new Rei(tab, Cor.Amarela));
            colocarNovaPeca('h', 1, new Rei(tab, Cor.Amarela));



            colocarNovaPeca('a', 8, new Torre(tab, Cor.Verde));
            colocarNovaPeca('b', 8, new Torre(tab, Cor.Verde));
            colocarNovaPeca('c', 8, new Torre(tab, Cor.Verde));
            colocarNovaPeca('d', 8, new Torre(tab, Cor.Verde));
            colocarNovaPeca('e', 8, new Torre(tab, Cor.Verde));
            colocarNovaPeca('f', 8, new Rei(tab, Cor.Verde));
            colocarNovaPeca('g', 8, new Rei(tab, Cor.Verde));
            colocarNovaPeca('h', 8, new Rei(tab, Cor.Verde));

        }
    }
}
