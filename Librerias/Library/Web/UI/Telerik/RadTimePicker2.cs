using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Globalization;


namespace Telerik.Web.UI
{

    public class RadTimePicker2 : Telerik.Web.UI.RadTimePicker
    {
        
        public RadTimePicker2() : base()
        {

            this.CssClass = "RadTimePicker";
            this.TimeView.Interval = new TimeSpan(0, 15, 0);
            this.TimeView.StartTime = new TimeSpan(8, 0, 0);
            this.TimeView.EndTime = new TimeSpan(21, 0, 0);
        }
        
    }

}