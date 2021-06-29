using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presenta.CA.Site.UserControls
{
    public partial class ActionBar : System.Web.UI.UserControl
    {
        #region Delegates
        public delegate void InserirDelegate(object sender, EventArgs e);
        public delegate void AlterarDelegate(object sender, EventArgs e);
        public delegate void SalvarDelegate(object sender, EventArgs e);
        public delegate void CancelarDelegate(object sender, EventArgs e);
        public delegate void ExcluirDelegate(object sender, EventArgs e);
        public delegate void ImprimirDelegate(object sender, EventArgs e);
        public delegate void LocalizarDelegate(object sender, EventArgs e);
        #endregion

        #region Events
        public event InserirDelegate InserirEvent;
        public event AlterarDelegate AlterarEvent;
        public event SalvarDelegate SalvarEvent;
        public event CancelarDelegate CancelarEvent;
        public event ExcluirDelegate ExcluirEvent;
        public event ImprimirDelegate ImprimirEvent;
        public event LocalizarDelegate LocalizarEvent;
        #endregion

        #region Properties
        public bool ShowInserir
        {
            get { return this.lkbInserir.Visible; }
            set { this.lkbInserir.Visible = value; }
        }

        public bool ShowAlterar
        {
            get { return this.lkbAlterar.Visible; }
            set { this.lkbAlterar.Visible = value; }
        }

        public bool ShowSalvar
        {
            get { return this.lkbSalvar.Visible; }
            set { this.lkbSalvar.Visible = value; }
        }

        public bool ShowCancelar
        {
            get { return this.lkbCancelar.Visible; }
            set { this.lkbCancelar.Visible = value; }
        }

        public bool ShowExcluir
        {
            get { return this.lkbExcluir.Visible; }
            set { this.lkbExcluir.Visible = value; }
        }

        public bool ShowImprimir
        {
            get { return this.lkbImprimir.Visible; }
            set { this.lkbImprimir.Visible = value; }
        }

        public bool ShowLocalizar
        {
            get { return this.lkbLocalizar.Visible; }
            set { this.lkbLocalizar.Visible = value; }
        }

        public bool ShowOcultarReport
        {
            set 
            {
                if (value)
                {
                    this.lblImprimir.Text = "Ocultar";
                }
                else
                {
                    this.lblImprimir.Text = "Imprimir";
                }
            }
        }

        public bool EnableInserir
        {
            get { return this.lkbInserir.Enabled; }
            set
            {
                this.lblInserir.Enabled = this.lkbInserir.Enabled = value;

                if (this.lblInserir.Enabled)
                {
                    this.lblInserir.ForeColor = System.Drawing.ColorTranslator.FromHtml("#10428C");
                    this.imgInserir.ImageUrl = this.imgInserir.ImageUrl.Replace("21-pb", "21");
                }
                else
                {
                    this.lblInserir.ForeColor = System.Drawing.Color.Gray;
                    this.imgInserir.ImageUrl = this.imgInserir.ImageUrl.Replace("21", "21-pb");
                }
            }
        }

        public bool EnableAlterar
        {
            get { return this.lkbAlterar.Enabled; }
            set
            {
                this.lblAlterar.Enabled = this.lkbAlterar.Enabled = value; 
                
                if (lblAlterar.Enabled)
                {
                    this.lblAlterar.ForeColor = System.Drawing.ColorTranslator.FromHtml("#10428C");
                    this.imgAlterar.ImageUrl = this.imgAlterar.ImageUrl.Replace("24-pb", "24");
                }
                else
                {
                    this.lblAlterar.ForeColor = System.Drawing.Color.Gray;
                    this.imgAlterar.ImageUrl = this.imgAlterar.ImageUrl.Replace("24", "24-pb");
                }
            }
        }

        public bool EnableSalvar
        {
            get { return this.lkbSalvar.Enabled; }
            set
            { 
                this.lblSalvar.Enabled = this.lkbSalvar.Enabled = value; 
                
                if (this.lblSalvar.Enabled)
                {
                    this.lblSalvar.ForeColor = System.Drawing.ColorTranslator.FromHtml("#10428C");
                    this.imgSalvar.ImageUrl = this.imgSalvar.ImageUrl.Replace("45-pb", "45");
                }
                else
                {
                    this.lblSalvar.ForeColor = System.Drawing.Color.Gray;
                    this.imgSalvar.ImageUrl = this.imgSalvar.ImageUrl.Replace("45", "45-pb");
                }
            }
        }

        public bool EnableCancelar
        {
            get { return this.lkbCancelar.Enabled; }
            set
            {
                this.lblCancelar.Enabled = this.lkbCancelar.Enabled = value;

                if (this.lblCancelar.Enabled)
                {
                    this.lblCancelar.ForeColor = System.Drawing.ColorTranslator.FromHtml("#10428C");
                    this.imgCancelar.ImageUrl = this.imgCancelar.ImageUrl.Replace("cancel-pb", "cancel");
                }
                else
                {
                    this.lblCancelar.ForeColor = System.Drawing.Color.Gray;
                    this.imgCancelar.ImageUrl = this.imgCancelar.ImageUrl.Replace("cancel", "cancel-pb");
                }
            }
        }

        public bool EnableExcluir
        {
            get { return this.lkbExcluir.Enabled; }
            set
            {
                this.lblExcluir.Enabled = this.lkbExcluir.Enabled = value;

                if (lblExcluir.Enabled)
                {
                    this.lblExcluir.ForeColor = System.Drawing.ColorTranslator.FromHtml("#10428C");
                    this.imgCancelar.ImageUrl = this.imgCancelar.ImageUrl.Replace("bin_closed-pb", "bin_closed");
                }
                else
                {
                    this.lblExcluir.ForeColor = System.Drawing.Color.Gray;
                    this.imgCancelar.ImageUrl = this.imgCancelar.ImageUrl.Replace("bin_closed", "bin_closed-pb");
                }
            }
        }

        public bool EnableImprimir
        {
            get { return this.lkbImprimir.Enabled; }
            set
            {
                this.lblImprimir.Enabled = this.lkbImprimir.Enabled = value;

                if (this.lblImprimir.Enabled)
                {
                    this.lblImprimir.ForeColor = System.Drawing.ColorTranslator.FromHtml("#10428C");
                    this.imgImprimir.ImageUrl = this.imgImprimir.ImageUrl.Replace("printer-pb", "printer");
                }
                else
                {
                    this.lblImprimir.ForeColor = System.Drawing.Color.Gray;
                    this.imgImprimir.ImageUrl = this.imgImprimir.ImageUrl.Replace("printer", "printer-pb");
                }
            }
        }

        public bool EnableLocalizar
        {
            get { return this.lkbLocalizar.Enabled; }
            set
            {
                this.lblLocalizar.Enabled = this.lkbLocalizar.Enabled = value;

                if (this.lblLocalizar.Enabled)
                {
                    this.lblLocalizar.ForeColor = System.Drawing.ColorTranslator.FromHtml("#10428C");
                    this.imgLocalizar.ImageUrl = this.imgLocalizar.ImageUrl.Replace("find-pb", "find");
                }
                else
                {
                    this.lblLocalizar.ForeColor = System.Drawing.Color.Gray;
                    this.imgLocalizar.ImageUrl = this.imgLocalizar.ImageUrl.Replace("find", "find-pb");
                }
            }
        }
        #endregion

        #region Methods
        protected override void OnInit(EventArgs e)
        {
            try
            {
                this.ShowAlterar = false;
                this.ShowCancelar = false;
                this.ShowExcluir = false;
                this.ShowImprimir = false;
                this.ShowInserir = false;
                this.ShowOcultarReport = false;
                this.ShowSalvar = false;
                this.ShowLocalizar = false;

                base.OnInit(e);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // TODO
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lkbInserir_Click(object sender, EventArgs e)
        {
            try
            {
                if (InserirEvent != null)
                {
                    InserirEvent(sender, e);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lkbAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                if (AlterarEvent != null)
                {
                    AlterarEvent(sender, e);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lkbSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (SalvarEvent != null)
                {
                    SalvarEvent(sender, e);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lkbCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                if (CancelarEvent != null)
                {
                    CancelarEvent(sender, e);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lkbExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (ExcluirEvent != null)
                {
                    ExcluirEvent(sender, e);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lkbImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if (ImprimirEvent != null)
                {
                    ImprimirEvent(sender, e);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lkbLocalizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (LocalizarEvent != null)
                {
                    LocalizarEvent(sender, e);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}