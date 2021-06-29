using System;

namespace Controle_Acesso
{
    public class clsEntTipoSenha
    {
        public int? idtiposenha;
        public string dstiposenha;
        public int qtmaxtentativas;
        public int qtverificacaohistorico;
        public string cdexpressaoregular; // null
        public DateTime dtiniciovigencia;
        public DateTime? dtfimvigencia; // null
        public int idoperador;
        public DateTime dhatualizacao;
    }
}