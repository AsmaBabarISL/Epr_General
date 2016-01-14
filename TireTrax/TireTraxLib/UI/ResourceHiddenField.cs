using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;


namespace TireTraxLib
{
	public class ResourceHiddenField : HiddenField

    {
        public string ControlText
        {
            get
            {
                return base.Value;
            }
            set
            {
                base.Value = ResourceMgr.GetControlText(value);
            }
        }


        public string MessageText
        {
            get
            {
                return base.Value;
            }
            set
            {
                base.Value = ResourceMgr.GetMessage(value);
            }
        }

    }
}
