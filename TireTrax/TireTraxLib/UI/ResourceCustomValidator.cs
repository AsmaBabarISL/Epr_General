using System.Web.UI.WebControls;


namespace TireTraxLib
{
	public class ResourceCustomValidator : CustomValidator
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
