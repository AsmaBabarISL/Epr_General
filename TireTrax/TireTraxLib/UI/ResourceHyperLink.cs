// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PowerbarHyperLink.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the PowerbarHyperLink type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TireTraxLib
{
    using System.Web.UI.WebControls;

	public class ResourceHyperLink : HyperLink
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

    }
}