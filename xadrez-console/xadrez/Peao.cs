using System.Runtime.ConstrainedExecution;
using tabuleiro;

namespace xadrez {
    class Peao : Peca {

        private partidaDe_Xadrez partida;
        public Peao(Tabuleiro tab, Cor cor, partidaDe_Xadrez partida) : base(tab, cor) {
            this.partida = partida;
        }

        public override string ToString() {
            return "P";
        }

        private bool existeInimigo(Posicao pos) {
            Peca p = tab.peca(pos);
            return p != null && p.cor != cor;
        }

        private bool livre(Posicao pos) {
            return tab.peca(pos) == null;
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);


            if (cor == Cor.Amarela) {
                pos.definirValores(posicao.linha - 1, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(posicao.linha - 2, posicao.coluna);

                if (tab.posicaoValida(pos) && livre(pos) && qtdMovimentos == 0) {
                    mat[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(posicao.linha - 1, posicao.coluna - 1);

                if (tab.posicaoValida(pos) && existeInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(posicao.linha - 1, posicao.coluna + 1);

                if (tab.posicaoValida(pos) && existeInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }


                //jogada especial 
                //en passant

                if (posicao.linha == 3) {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                    if (tab.posicaoValida(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.vulneravelEmPassant) {
                        mat[esquerda.linha -1, esquerda.coluna] = true;

                    }

                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (tab.posicaoValida(direita) && existeInimigo(direita) && tab.peca(direita) == partida.vulneravelEmPassant) {
                        mat[direita.linha -1, direita.coluna] = true;
                    }
                }
            }
            else {
                pos.definirValores(posicao.linha + 1, posicao.coluna);

                if (tab.posicaoValida(pos) && livre(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(posicao.linha + 2, posicao.coluna);

                if (tab.posicaoValida(pos) && livre(pos) && qtdMovimentos == 0) {
                    mat[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(posicao.linha + 1, posicao.coluna - 1);

                if (tab.posicaoValida(pos) && existeInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(posicao.linha + 1, posicao.coluna + 1);

                if (tab.posicaoValida(pos) && existeInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }

                //jogada especial 
                //en passant

                if (posicao.linha == 4) {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                    if (tab.posicaoValida(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.vulneravelEmPassant) {
                        mat[esquerda.linha +1, esquerda.coluna] = true;

                    }

                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (tab.posicaoValida(direita) && existeInimigo(direita) && tab.peca(direita) == partida.vulneravelEmPassant) {
                        mat[direita.linha +1, direita.coluna] = true;
                    }
                }
            }

            return mat;

        }
    }
}

