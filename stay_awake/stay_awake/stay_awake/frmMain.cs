using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stay_awake
{
    public partial class frmMain : Form
    {
        Random myRandom = new Random();
        

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                lblText.Text = "waiting";
                btnMain.Text = "Stay Awake";
                timer1.Stop();
            }
            else
            {
                lblText.Text = "started";
                timer1.Start();
                btnMain.Text = "Stop";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var distancex = myRandom.Next(-1000, 1000);
            var distancey = myRandom.Next(-1000, 1000);
            lblText.Text = $"moving: {distancex}, {distancey}";
            this.Cursor = new Cursor(Cursor.Current.Handle);
            Cursor.Position = new Point(Cursor.Position.X + distancex, Cursor.Position.Y + distancey);
        }
    }
}
