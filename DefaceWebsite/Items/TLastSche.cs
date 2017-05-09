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
    public partial class TLastSche : UserControl
    {
        public TLastSche()
        {
            InitializeComponent();
        }

        public void SetValues(DateTime? date, int? times, decimal? domain, decimal? link)
        {
            if (date != null)
                this.lblDate.Text = "Ngày " + date.Value.Date.ToString(StaticClass.formatShortDate) + ":";
            else
                this.lblDate.Text = ":";
            this.lblInfo.Text = (times == null ? "0" : times.ToString()) + " lần, "
                + (domain == null ? "0" : domain.ToString()) + " domains, "
                + (link == null ? "0" : link.ToString()) + " links";
        }
    }
}
