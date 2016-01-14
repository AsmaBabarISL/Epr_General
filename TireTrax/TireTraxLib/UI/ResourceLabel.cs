using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace TireTraxLib
{
	public class ResourceLabel : Label
    {


        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = ResourceMgr.GetControlText(value);
            }
        }


        public string ControlText
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = ResourceMgr.GetControlText(value);
            }
        }

        
        public string MessageText
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = ResourceMgr.GetMessage(value);
            }
        }

        
        public string ErrorText
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = ResourceMgr.GetError(value);
            }
        }

    }
}