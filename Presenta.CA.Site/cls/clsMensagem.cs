using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Controle_Acesso
{
    public class clsMensagem
    {
        public static string MensagemRetornar(int _coderro)
        {
            //17-Ago-2011 retornar mensagem de erro traduzida, a partir do código de erro
            try
            {
                clsDB _DB = new clsDB();
                SqlDataReader _dr = _DB.SQL_RetornoDR("SELECT dsmensagem FROM tbmensagem WHERE idmensagem = " + _coderro.ToString());
                if (!_dr.HasRows)
                {
                    //Retornar mensagem do banco de dados GERAL
                    // matsutami - confirmar se vai existitr essar tabela
                    //clsDB _DBGeral = new clsDB(clsDB._ConexaoTipo._TP_GERAL);
                    clsDB _DBGeral = new clsDB();
                    SqlDataReader _drGeral = _DBGeral.SQL_RetornoDR("SELECT dsmensagem FROM tbgrmensagem WHERE idmensagem = " + _coderro.ToString());
                    if (_drGeral.HasRows)
                    {
                        _drGeral.Read();
                        return _drGeral["dsmensagem"].ToString();
                    }
                    else
                        return _coderro.ToString();
                }
                else
                {
                    _dr.Read();
                    return _dr["dsmensagem"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao retornar mensagem - código " + ex.ToString());
            }
        }
    }
}