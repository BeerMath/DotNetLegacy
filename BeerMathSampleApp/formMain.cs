using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using BeerMath;

namespace BeerMath.Sample
{
    public partial class formMain : Form
    {
        public formMain()
        {
            InitializeComponent();
        }

        private void MCU_Click(object sender, EventArgs e)
        {
            decimal lbsGrain = decimal.Parse(textBox1.Text);
            decimal degLovibond = decimal.Parse(textBox2.Text);
            decimal totalVolume = decimal.Parse(textBox3.Text);

            decimal MCUs = Malt.CalculateMcu(lbsGrain, degLovibond, totalVolume);

            label1.Text = MCUs.ToString();
        }
    }
}
