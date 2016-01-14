using System.Web.UI.WebControls;

namespace TireTraxLib
{
	public class ResourceLinkButton : LinkButton
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
        public string TextMessage
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
    }

}
