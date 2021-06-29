using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presenta.CA.Site.Pages.Base
{
    public interface ISecurityBase
    {
        void TratarAutorizacaoControles();
        void BindActionBarEvents();
        
        void InserirAction(object sender, EventArgs e);
        void AlterarAction(object sender, EventArgs e);
        void SalvarAction(object sender, EventArgs e);
        void CancelarAction(object sender, EventArgs e);
        void ExcluirAction(object sender, EventArgs e);
        void ImprimirAction(object sender, EventArgs e);
        void LocalizarAction(object sender, EventArgs e);
    }
}
