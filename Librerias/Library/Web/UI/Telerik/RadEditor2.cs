using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;


namespace Telerik.Web.UI
{

    public class RadEditor2 : Telerik.Web.UI.RadEditor
    {

        public RadEditor2(): base()
        {
            this.ToolsFile = "~/App_code/Telerik/xml/BasicTools.xml";
        }

       
    }

}
