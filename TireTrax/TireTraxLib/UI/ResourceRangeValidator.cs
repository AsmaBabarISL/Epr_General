using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace TireTraxLib
{
	public class ResourceRangeValidator : RangeValidator
    {


        public string ErrorControlText
        {
            get
            {
                return base.ErrorMessage;
            }
            set
            {
                base.ErrorMessage = ResourceMgr.GetControlText(value);
            }
        }


        public string ErrorMessageText
        {
            get
            {
                return base.ErrorMessage;
            }
            set
            {
                base.ErrorMessage = ResourceMgr.GetMessage(value);
            }
        }


        public string ErrorText
        {
            get
            {
                return base.ErrorMessage;
            }
            set
            {
                base.ErrorMessage = ResourceMgr.GetError(value);
            }
        }
    }
}

