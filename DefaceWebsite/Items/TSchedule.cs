using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DefaceWebsite.Items
{
    public partial class TSchedule : UserControl
    {
        public TSchedule()
        {
            InitializeComponent();
        }

        public void SetValues(TimeSpan? time, decimal? domain, decimal? link, decimal? done, decimal? warn, decimal? alr)
        {
            this.lblTime.Text = "Lúc " + time.Value.ToString(StaticClass.formatTime);
            this.lblTotal.Text = (domain == null ? "0" : domain.ToString()) + " domains, " + (link == null ? "0" : link.ToString()) + " links";
            this.lblResult.Text = (done == null ? "0" : done.ToString()) + " done, "
                + (warn == null ? "0" : warn.ToString()) + " waring, "
                + (alr == null ? "0" : alr.ToString()) + " alert";
        }
    }
}
