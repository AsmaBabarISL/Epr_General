using System.Web.UI.WebControls;

namespace TireTraxLib
{

	public class ResourceButton : Button
    {
        public string ControlText
        {
            get
            {
                return Text;
            }
            set
            {
                Text = ResourceMgr.GetControlText(value);
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
