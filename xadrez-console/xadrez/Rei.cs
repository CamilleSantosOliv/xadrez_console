using tabuleiro;

namespace xadrez {
    class Rei : Peca {

        public Rei(Tabuleiro tab, Cor cor) : base(tab, cor) {

        }
        public override string ToString() {
            return "R";

        }

        private bool podeMover(Posicao pos) {

            Peca p = tab.peca(pos);
            return p == null || p.cor != cor;
        }

        public override bool[,] movimentosPossiveis() {

            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            //movimentacao para cima
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;

            }
            // movimentacao pro pra posicao nordeste
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);

            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            //movimentacao pra direita
            pos.definirValores(posicao.linha, posicao.coluna + 1);

            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            //movimentacao pro sudeste
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1);

            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            //movimentacao para baixo
            pos.definirValores(posicao.linha + 1, posicao.coluna);

            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            //movimentacao pro sudoeste
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1);

            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            //movimentacao pra esquerda
            pos.definirValores(posicao.linha, posicao.coluna - 1);

            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            //movimentacao pro noroeste
            pos.definirValores(posicao.linha - 1, posicao.coluna -1);

            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            return mat;
        }
    }
}
