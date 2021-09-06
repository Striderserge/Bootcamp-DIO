using System;

namespace appSerie
{
    public class serie : entidadeBase
    {
        private Genero genero { get; set; }
        private string titulo { get; set; }
        private string descricao { get; set; }
        private int ano { get; set; }
        private bool Excluido { get; set; }

        public serie(int id, Genero genero, string titulo, string descricao, int ano)
        {
            this.id = id;
            this.genero = genero;
            this.titulo = titulo;
            this.descricao = descricao;
            this.ano = ano;
            this.Excluido = false;
        }
        public override string ToString()
        {
            string retorno = "";
            retorno += "Genero: " + this.genero + Environment.NewLine;
            retorno += "Título: " + this.titulo + Environment.NewLine;
            retorno += "Descição: " + this.descricao + Environment.NewLine;
            retorno += "Ano de Lançamento: " + this.ano + Environment.NewLine; 
            retorno += "Excluido: " + this.Excluido; 
            return retorno;
        }
        public string retornaTitulo()
        {
            return this.titulo;
        }
        internal int retornaId()
        {
            return this.id;
        }
        internal bool retornaExlcuido()
        {
            return this.Excluido;
        }
        public void Excluir()
        {
            this.Excluido = true;
        }
    }
    
}