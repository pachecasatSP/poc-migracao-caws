using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Controle_Acesso
{
    public partial class ucData : System.Web.UI.UserControl
    {
        public string Erros;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public string Text
        {
            get
            {
                return txtData.Text;
            }
            set
            {
                txtData.Text = value;
            }
        }

        protected void btnCalendario_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (calendar.Visible)
                {
                    calendar.Visible = false;
                }
                else
                {
                    calendar.Visible = true;
                    if (txtData.Text.Length > 0)
                        calendar.SelectedDate = Convert.ToDateTime(txtData.Text);
                }
            }
            catch (Exception ex)
            {
                Erros = ex.Source + " - " + ex.Message;
            }
        }

        protected void calendar_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                txtData.Text = calendar.SelectedDate.ToString();
                calendar.Visible = false;
            }
            catch (Exception ex)
            {
                Erros = ex.Source + " - " + ex.Message;
            }
        }

        public bool Enabled
        {
            get
            {
                return txtData.Enabled;
            }
            set
            {
                txtData.Enabled = value;
                btnCalendario.Enabled = value;
            }
        }
    }
}