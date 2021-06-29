using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Controle_Acesso
{
    public partial class ucToolbar : System.Web.UI.UserControl
    {
        //Declaração dos delegates e eventos para capturar no form
        public delegate void SaveEvent(object sender, System.EventArgs e);
        public delegate void DeleteEvent(object sender, System.EventArgs e);
        public delegate void BackEvent(object sender, System.EventArgs e);
        public delegate void SearchEvent(object sender, System.EventArgs e);
        public delegate void NewEvent(object sender, System.EventArgs e);
        public event SaveEvent _Save;
        public event DeleteEvent _Delete;
        public event BackEvent _Back;
        public event SearchEvent _Search;
        public event NewEvent _New;
        //

        //Propriedades
        private bool _DeleteVisible = false;
        private bool _SaveVisible = false;
        private bool _SaveConfirmVisible = false;
        private bool _BackVisible = false;
        public bool _BackHTMLVisible = false;
        public bool _ResetVisible = false;
        private bool _SearchVisible = false;
        private bool _NewVisible = false;
        private bool _DeleteEnabled = true;
        private bool _SaveEnabled = true;
        private bool _SaveConfirmEnabled = true;
        private bool _BackEnabled = true;
        public bool _ResetEnabled = true;
        private bool _SearchEnabled = true;
        private bool _NewEnabled = true;

        public bool DeleteVisible
        {
            get { return this._DeleteVisible; }
            set { _DeleteVisible = value; this.btnDelete.Visible = _DeleteVisible; }
        }
        public bool SaveVisible
        {
            get { return this._SaveVisible; }
            set { _SaveVisible = value; this.btnSave.Visible = _SaveVisible; }
        }
        public bool SaveConfirmVisible
        {
            get { return this._SaveConfirmVisible; }
            set { _SaveConfirmVisible = value; this.btnSaveConfirm.Visible = _SaveConfirmVisible; }
        }
        public bool BackVisible
        {
            get { return this._BackVisible; }
            set { _BackVisible = value; this.btnBack.Visible = _BackVisible; }
        }
        public bool BackHTMLVisible
        {
            get { return this._BackHTMLVisible; }
            set { _BackHTMLVisible = value; }
        }
        public bool ResetVisible
        {
            get { return this._ResetVisible; }
            set { _ResetVisible = value; }
        }
        public bool SearchVisible
        {
            get { return this._SearchVisible; }
            set { _SearchVisible = value; this.btnSearch.Visible = _SearchVisible; }
        }
        public bool NewVisible
        {
            get { return this._NewVisible; }
            set { _NewVisible = value; this.btnNew.Visible = _NewVisible; }
        }
        public bool DeleteEnabled
        {
            get { return this._DeleteEnabled; }
            set { _DeleteEnabled = value; this.btnDelete.Enabled = _DeleteEnabled; btnDelete.ImageUrl = (value == true ? "../images/delete.png" : "../images/delete_pb.png"); }
        }
        public bool SaveEnabled
        {
            get { return this._SaveEnabled; }
            set { _SaveEnabled = value; this.btnSave.Enabled = _SaveEnabled; btnSave.ImageUrl = (value == true ? "../images/save.png" : "../images/save_pb.png"); }
        }
        public bool SaveConfirmEnabled
        {
            get { return this._SaveConfirmEnabled; }
            set { _SaveConfirmEnabled = value; this.btnSaveConfirm.Enabled = _SaveConfirmEnabled; btnSaveConfirm.ImageUrl = (value == true ? "../images/save.png" : "../images/save_pb.png"); }
        }
        public bool BackEnabled
        {
            get { return this._BackEnabled; }
            set { _BackEnabled = value; this.btnBack.Enabled = _BackEnabled; btnBack.ImageUrl = (value == true ? "../images/voltar.gif" : "../images/voltar_pb.gif"); }
        }
        public bool ResetEnabled
        {
            get { return this._ResetEnabled; }
            set { _ResetEnabled = value; }
        }
        public bool SearchEnabled
        {
            get { return this._SearchEnabled; }
            set { _SearchEnabled = value; this.btnSearch.Enabled = _SearchEnabled; }
        }
        public bool NewEnabled
        {
            get { return this._NewEnabled; }
            set { _NewEnabled = value; this.btnNew.Enabled = _NewEnabled; }
        }
        //

        public void ExibirBotoes(bool _save, bool _delete, bool _reset, bool _back, bool _backhtml, bool _search, bool _new, bool _saveconfirm)
        {
            this.SaveVisible = _save;
            this.DeleteVisible = _delete;
            this.ResetVisible = _reset;
            this.BackVisible = _back;
            this.BackHTMLVisible = _backhtml;
            this.SearchVisible = _search;
            this.NewVisible = _new;
            this.SaveConfirmVisible = _saveconfirm;
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (_Save != null)
                _Save(sender, e);
        }

        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            if (_Delete != null)
                _Delete(sender, e);
        }

        protected void btnBack_Click(object sender, ImageClickEventArgs e)
        {
            if (_Back != null)
                _Back(sender, e);
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (_Search != null)
                _Search(sender, e);
        }

        protected void btnNew_Click(object sender, ImageClickEventArgs e)
        {
            if (_New != null)
                _New(sender, e);
        }
    }
}