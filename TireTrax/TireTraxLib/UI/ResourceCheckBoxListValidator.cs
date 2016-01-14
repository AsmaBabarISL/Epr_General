using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace TireTraxLib
{
    public class ResourceCheckBoxListValidator :
                        System.Web.UI.WebControls.BaseValidator
    {
        private ListControl _listctrl;

		public ResourceCheckBoxListValidator()
        {
            base.EnableClientScript = false;
        }

        protected override bool ControlPropertiesValid()
        {
            Control ctrl = FindControl(ControlToValidate);

            if (ctrl != null)
            {
                _listctrl = (ListControl)ctrl;
                return (_listctrl != null);
            }
            else
                return false;  // raise exception
        }

        protected override bool EvaluateIsValid()
        {
            return _listctrl.SelectedIndex != -1;
        }
    }
}
