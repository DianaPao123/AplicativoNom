using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitioNominaAdmin
{
    public partial class Default : System.Web.UI.MasterPage
    {
        void Page_Init(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["userId"] == null)
                {
                    this.Response.Redirect("wfrLogin.aspx");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}