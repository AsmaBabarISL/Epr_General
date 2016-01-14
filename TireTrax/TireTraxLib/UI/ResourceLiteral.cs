using System.Web.UI.WebControls;


namespace TireTraxLib
{
	public class ResourceLiteral : Literal
    {

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