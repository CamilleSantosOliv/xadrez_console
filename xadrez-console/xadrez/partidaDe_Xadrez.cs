﻿using tabuleiro;
using xadrez_console;

namespace xadrez {
    class partidaDe_Xadrez {

        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }

        private HashSet<Peca> pecas;

        private HashSet<Peca> capturadas;
        public bool xeque { get; private set; }

        public Peca vulneravelEmPassant { get; private set; }


        public partidaDe_Xadrez() {

            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Amarela;
            terminada = false;
            xeque = false;
            vulneravelEmPassant = null;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public void realizaJogada(Posicao origem, Posicao destino) {

            Peca pecaCapturada = executaMovimento(origem, destino);

            if (estaEmXeque(jogadorAtual)) {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não deve se colocar em Xeque Match.");
            }

            if (estaEmXeque(adversaria(jogadorAtual))) {
                xeque = true;
            }
            else {
                xeque = false;
            }
            if (testeXequeMate(adversaria(jogadorAtual))) {
                terminada = true;
            }
            else {
                turno++;
                mudaJogador();

            }

            Peca p = tab.peca(destino);

            //#jogada especial
            //en Passant

            if (p is Peao && (destino.linha == origem.linha - 2 || destino.linha == origem.linha + 2)) {
                vulneravelEmPassant = p;
            }
            else {
                vulneravelEmPassant = null;
            }
        }



        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada) {
            Peca p = tab.retirarPeca(destino);
            p.decrementarQtdMovimentos();
            if (pecaCapturada != null) {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p, origem);

            // #jogada especial
            //roque

            //roque pequeno
            if (p is Rei && destino.coluna == origem.coluna + 2) {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.retirarPeca(destinoT);
                T.decrementarQtdMovimentos();
                tab.colocarPeca(T, origemT);
            }

            //roque grande
            if (p is Rei && destino.coluna == origem.coluna - 2) {
                Posicao origemT = new Posicao(origem.linha, origem.coluna- 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tab.retirarPeca(destinoT);
                T.decrementarQtdMovimentos();
                tab.colocarPeca(T, origemT);
            }

            //#jogada especial
            //en Passant

            if (p is Peao) {
                if (origem.coluna != destino.coluna && pecaCapturada == vulneravelEmPassant) {
                    Peca peao = tab.retirarPeca(destino);
                    Posicao posP;
                    if (p.cor == Cor.Amarela) {
                        posP = new Posicao(3, destino.coluna);

                    }

                    else {
                        posP = new Posicao(4, destino.coluna);
                    }

                    tab.colocarPeca(peao, posP);
                }
            }
        }

        public bool testeXequeMate(Cor cor) {
            if (!estaEmXeque(cor)) {
                return false;
            }

            foreach (Peca x in pecasEmJogo(cor)) {
                bool[,] mat = x.movimentosPossiveis();

                for (int i = 0; i < tab.linhas; i++) {
                    for (int j = 0; j < tab.colunas; j++) {
                        if (mat[i, j]) {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque) {
                                return false;
                            }
                        }
                    }

                }
            }
            return true;
        }

        public void validarPosicoesDeOrigem(Posicao pos) {

            if (tab.peca(pos) == null) {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida.");
            }

            if (jogadorAtual != tab.peca(pos).cor) {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }

            if (!tab.peca(pos).existeMovimentosPossiveis()) {
                throw new TabuleiroException("Não há movimentos possíveis para a peça escolhida.");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino) {
            if (!tab.peca(origem).movimentoPossivel(destino)) {
                throw new TabuleiroException("Posicao de destino inválida!");
            }
        }

        private void mudaJogador() {

            if (jogadorAtual == Cor.Amarela) {
                jogadorAtual = Cor.Verde;
            }
            else {
                jogadorAtual = Cor.Amarela;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas) {
                if (x.cor == cor) {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas) {
                if (x.cor == cor) {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        private Cor adversaria(Cor cor) {
            if (cor == Cor.Amarela) {
                return Cor.Verde;

            }
            else {
                return Cor.Amarela;
            }
        }

        private Peca rei(Cor cor) {
            foreach (Peca x in pecasEmJogo(cor)) {
                if (x is Rei) {
                    return x;
                }
            }
            return null;
        }

        public bool estaEmXeque(Cor cor) {

            Peca R = rei(cor);
            if (R == null) {
                throw new TabuleiroException("Não existe rei da cor " + cor + "nesta partida.");
            }

            foreach (Peca x in pecasEmJogo(adversaria(cor))) {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna]) {
                    return true;
                }
            }
            return false;
        }

        public Peca executaMovimento(Posicao origem, Posicao destino) {

            Peca p = tab.retirarPeca(origem);
            p.incrementarQtdMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null) {
                capturadas.Add(pecaCapturada);
            }


            // #jogada especial
            //roque

            //roque pequeno
            if (p is Rei && destino.coluna == origem.coluna + 2) {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQtdMovimentos();
                tab.colocarPeca(T, destinoT);
            }

            //roque grande
            if (p is Rei && destino.coluna == origem.coluna - 2) {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQtdMovimentos();
                tab.colocarPeca(T, destinoT);
            }


            //#jogada especial
            //en Passant

            if (p is Peao) {
                if (origem.coluna != destino.coluna && pecaCapturada == null) {
                    Posicao posP;
                    if (p.cor == Cor.Amarela) {
                        posP = new Posicao(destino.linha + 1, destino.coluna);
                    }
                    else {
                        posP = new Posicao(destino.linha - 1, destino.coluna);
                    }
                    pecaCapturada = tab.retirarPeca(posP);
                    capturadas.Add(pecaCapturada);
                }
            }

            return pecaCapturada;

        }

            public void colocarNovaPeca(char coluna, int linha, Peca peca) {
                tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
                pecas.Add(peca);

            }


            private void colocarPecas() {

                colocarNovaPeca('a', 1, new Torre(tab, Cor.Amarela));
                colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Amarela));
                colocarNovaPeca('c', 1, new Bispo(tab, Cor.Amarela));
                colocarNovaPeca('d', 1, new Dama(tab, Cor.Amarela));
                colocarNovaPeca('e', 1, new Rei(tab, Cor.Amarela, this));
                colocarNovaPeca('f', 1, new Bispo(tab, Cor.Amarela));
                colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Amarela));
                colocarNovaPeca('h', 1, new Torre(tab, Cor.Amarela));
                colocarNovaPeca('a', 2, new Peao(tab, Cor.Amarela, this));
                colocarNovaPeca('b', 2, new Peao(tab, Cor.Amarela, this));
                colocarNovaPeca('c', 2, new Peao(tab, Cor.Amarela, this));
                colocarNovaPeca('d', 2, new Peao(tab, Cor.Amarela, this));
                colocarNovaPeca('e', 2, new Peao(tab, Cor.Amarela, this));
                colocarNovaPeca('f', 2, new Peao(tab, Cor.Amarela, this));
                colocarNovaPeca('g', 2, new Peao(tab, Cor.Amarela, this));
                colocarNovaPeca('h', 2, new Peao(tab, Cor.Amarela, this));


                colocarNovaPeca('a', 8, new Torre(tab, Cor.Verde));
                colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Verde));
                colocarNovaPeca('c', 8, new Bispo(tab, Cor.Verde));
                colocarNovaPeca('d', 8, new Dama(tab, Cor.Verde));
                colocarNovaPeca('e', 8, new Rei(tab, Cor.Verde, this));
                colocarNovaPeca('f', 8, new Bispo(tab, Cor.Verde));
                colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Verde));
                colocarNovaPeca('h', 8, new Torre(tab, Cor.Verde));
                colocarNovaPeca('a', 7, new Peao(tab, Cor.Verde, this));
                colocarNovaPeca('b', 7, new Peao(tab, Cor.Verde, this));
                colocarNovaPeca('c', 7, new Peao(tab, Cor.Verde, this));
                colocarNovaPeca('d', 7, new Peao(tab, Cor.Verde, this));
                colocarNovaPeca('e', 7, new Peao(tab, Cor.Verde, this));
                colocarNovaPeca('f', 7, new Peao(tab, Cor.Verde, this));
                colocarNovaPeca('g', 7, new Peao(tab, Cor.Verde, this));
                colocarNovaPeca('h', 7, new Peao(tab, Cor.Verde, this));


            }
        }
    }
