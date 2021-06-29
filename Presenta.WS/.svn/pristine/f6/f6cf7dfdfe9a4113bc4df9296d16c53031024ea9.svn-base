using com.CryptoTools;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Presenta.WS.CA.Model
{
    public class Token
    {
        private string tokenValido { get; set; }
        private string tokenClient { get; set; }

        public Token(string _tokenValido, string _tokenClient)
        {
            tokenValido = _tokenValido;
            tokenClient = _tokenClient;

            ValidarToken();
        }
        private void ValidarToken()
        {
            try
            {
                if (string.IsNullOrEmpty(tokenClient))
                    throw new TokenException(ExcecaoWSEnum.TokenNaoInformadoCabecalho);

                if (string.IsNullOrEmpty(tokenClient))
                    throw new TokenException(ExcecaoWSEnum.TokenNaoInformado);

                if (string.IsNullOrEmpty(tokenValido))
                    throw new TokenException(ExcecaoWSEnum.RecuperarToken);

                var cryptoBase64 = new CryptoBase64();
                var cryptoDES = new CryptoDES();

                ObterTokenApp(cryptoBase64, cryptoDES);

                ObterTokenClient(cryptoBase64, cryptoDES);

                if (tokenClient != tokenValido)
                {
                    throw new TokenException(ExcecaoWSEnum.TokenInvalido);
                }
            }
            catch (TokenException tex)
            {

                throw tex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ObterTokenClient(CryptoBase64 cryptoBase64, CryptoDES cryptoDES)
        {
            try
            {
                cryptoBase64.Decrypt(tokenClient);
                cryptoDES.DeriveKeyFromPassword("present@passw$rdK&y", null, 1);
                tokenClient = cryptoDES.DecryptText(cryptoBase64.Result);
            }
            catch (Exception)
            {
                throw new TokenException(ExcecaoWSEnum.TokenInvalido);
            }
        }

        private void ObterTokenApp(CryptoBase64 cryptoBase64, CryptoDES cryptoDES)
        {
            try
            {
                cryptoBase64.Decrypt(tokenValido);
                cryptoDES.DeriveKeyFromPassword("present@passw$rdK&y", null, 1);
                tokenValido = cryptoDES.DecryptText(cryptoBase64.Result);
            }
            catch (Exception)
            {
                throw new TokenException(ExcecaoWSEnum.RecuperarToken);
            }            
        }
    }
}