using System;
using System.Collections.Generic;
using System.Web;

namespace Controle_Acesso
{
    /// <summary>
    /// Classe de funções genéricas do Controle de Acesso(CA)
    /// </summary>
    public static class clsCA
    {
        public const int _IDOPERADOR = 1;   //Teste - pegar o operador do login e controle de acesso

        public enum _AmbienteTipo { _AT_Desenv = 1, _AT_Homolog = 2, _AT_Prod = 3 };

        //Constantes para utilização de parâmetros via QueryString e Session
        public const string _PARAM_RET_ID = "backid";
        public const string _PARAM_RET_DSC = "backdsc";
        public const string _PARAM_RET_DT = "backdt";
        public const string _PARAM_VOLTAR = "voltar";
        public const string _PARAM_ZERARSESSIONVOLTAR = "newsession";
        public const string _PARAM_SAVE = "save";
        public const string _PARAM_DELETE = "delete";
        public const string _PARAM_NOVO = "add";

        /// <summary>
        /// Inserir o valor da Session atual
        /// </summary>
        /// <param name="_chave">Nome da chave</param>
        /// <param name="_valor">Valor</param>
        public static void SessionInserir(string _chave, string _valor)
        {
            System.Web.HttpContext.Current.Session[_chave] = _valor;
        }

        /// <summary>
        /// Retornar valor da Session atual
        /// </summary>
        /// <param name="_chave">Nome da chave</param>
        /// <returns>Se existir retorna string, caso contrário retorna NULL</returns>
        public static string SessionRetornar(string _chave)
        {
            if (System.Web.HttpContext.Current.Session[_chave] != null)
                return System.Web.HttpContext.Current.Session[_chave].ToString();
            else
                return null;
        }
        
        /// <summary>
        /// Excluir valor de Session atual
        /// </summary>
        /// <param name="_chave">Nome da chave</param>
        public static void SessionExcluir(string _chave)
        {
            if (System.Web.HttpContext.Current.Session[_chave] != null)
                System.Web.HttpContext.Current.Session.Remove(_chave);
        }

        /// <summary>
        /// Excluir todos os valores utilizados pelos forms na Session
        /// </summary>
        public static void SessionExcluir()
        {
            System.Web.HttpContext.Current.Session.Remove(_PARAM_RET_DSC);
            System.Web.HttpContext.Current.Session.Remove(_PARAM_RET_DT);
            System.Web.HttpContext.Current.Session.Remove(_PARAM_RET_ID);
        }

        /// <summary>
        /// Retornar valor vindo de querystring
        /// </summary>
        /// <param name="_chave">Nome da chave</param>
        /// <returns>int</returns>
        public static int QuerystringIntRetornar(string _chave)
        {
            if (System.Web.HttpContext.Current.Request.QueryString[_chave] != null)
                return Convert.ToInt32(System.Web.HttpContext.Current.Request.QueryString[_chave].ToString());
            else
                return 0;
        }

        /// <summary>
        /// Retornar data formatada em dd/MM/yyyy para o frontend
        /// </summary>
        /// <param name="p_Data">Valor int vindo do banco de dados</param>
        /// <returns></returns>
        public static string FormatData(DateTime p_Data)
        {
            if (p_Data == DateTime.MinValue || p_Data == DateTime.MaxValue)
                return string.Empty;
            else
                return String.Format("{0:dd/MM/yyyy}", p_Data);
        }

        /// <summary>
        /// Retornar data formatada em dd/MM/yyyy HH:mm:ss  para o frontend
        /// </summary>
        /// <param name="p_Data">Valor int vindo do banco de dados</param>
        /// <returns></returns>
        public static string FormatDataHora(DateTime p_Data)
        {
            if (p_Data == DateTime.MinValue || p_Data == DateTime.MaxValue)
                return string.Empty;
            else
                return String.Format("{0:dd/MM/yyyy HH:mm:ss}", p_Data);
        }

        /// <summary>
        /// Retornar data formatada em dd/MM/yyyy HH:mm:ss  para o frontend
        /// </summary>
        /// <param name="p_Data">Valor int vindo do banco de dados</param>
        /// <returns></returns>
        public static string FormatDataHora(DateTime? p_Data)
        {
            if (p_Data == null)
                return string.Empty;
            if (p_Data == DateTime.MinValue || p_Data == DateTime.MaxValue)
                return string.Empty;
            else
                return String.Format("{0:dd/MM/yyyy HH:mm:ss}", p_Data);
        }






    }

    public enum TipoSituacaoFuncionalidadePerfil 
    {
        // matsutami verificar
        Ativo = 1, Inativo = 2
    }

    public enum TipoSituacaoPerfilOperador
    {
        // matsutami verificar
        Ativo = 1, Inativo = 2
    }




}