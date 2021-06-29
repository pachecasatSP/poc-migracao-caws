using System;
using System.Collections.Generic;
using System.Web;
using Presenta.CA.Model;

namespace Presenta.CA.Site.Pages.Base
{
    public abstract class PageBase : System.Web.UI.Page
    {
        public int[,] funcionalidadesPagina;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
        }

        public void AtribuirAutorizacao()
        {
            if (Context != null && Context.Session != null)
            {
                int[] funcionalidades = (int[])Session[Constants.SessionFuncKey];

                if (funcionalidades != null)
                {
                    for (int i = 0; i < (funcionalidadesPagina.Length / 2); i++)
                    {
                        for (long j = 0; j < funcionalidades.LongLength; j++)
                        {
                            if (funcionalidadesPagina[i, 0] == funcionalidades[j])
                            {
                                funcionalidadesPagina[i, 1] = 1;
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void CarregarFuncionalidadesPagina(Enum controles)
        {
            this.funcionalidadesPagina = new int[Enum.GetValues(controles.GetType()).Length, 2];

            int i = 0;

            foreach (var controle in Enum.GetValues(controles.GetType()))
            {
                this.funcionalidadesPagina[i, 0] = controle.GetHashCode();
                this.funcionalidadesPagina[i, 1] = 0;
                i++;
            }
        }
    }
}