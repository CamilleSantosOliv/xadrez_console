namespace tabuleiro {
    class Peca {

        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int qtdMovimentos { get; protected set; }
        public Tabuleiro tab { get; protected set; }

        public Peca(Posicao posicao, Tabuleiro tab, Cor cor) {

            this.Posicao = posicao;
            this.Cor = cor;
            this.tab = tab;
            this.qtdMovimentos = 0;
        }
    }
}
