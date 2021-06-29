using System;
using System.Collections.Generic;
using System.Web;

namespace Presenta.CA.Site.MasterPages.Items
{
    public class LinkButtonPageItem
    {
        public LinkButtonPageItem(string texto, string urlImagem, string urlPage, string onClientClick, string cssClass)
        {
            Texto = texto;
            UrlImagem = urlImagem;
            UrlPage = urlPage;
            OnClientClick = onClientClick;
            CssClass = cssClass;
        }

        public string Texto { get; set; }

        public string UrlImagem { get; set; }

        public string UrlPage { get; set; }

        public string OnClientClick { get; set; }

        public string CssClass { get; set; }
    }
}