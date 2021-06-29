using System;

namespace Controle_Acesso
{
    public class clsEntOperador
    {
        public int? idoperador;
        public int idtiposenha;
        public int stoperador;
        public string cdoperador;
        public string nmoperador;
        public string dsemail; //null
	    public DateTime dtcadastro;
	    public DateTime dhsituacao;
        public string crsenha;
	    public DateTime dtsenha;
        public DateTime? dhultimologin; //null
        public int qtloginincorreto;
	    public DateTime dhatualizacao;
        public int idoperadoratualizacao;
    }
}