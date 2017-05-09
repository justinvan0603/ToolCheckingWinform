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
    public partial class TNewUser : UserControl
    {
        public TNewUser()
        {
            InitializeComponent();
        }

        public void SetValues(DateTime? date, string userName, string domain)
        {
            if (date != null)
                this.lblDate.Text = "Ngày " + date.Value.Date.ToString(StaticClass.formatShortDate) + ":";
            else
                this.lblDate.Text = ":";
            this.lblInfo.Text = userName + " - " + domain;
        }
    }
}
