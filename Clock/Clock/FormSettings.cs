using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clock
{
    public partial class FormSettings : Form
    {
        public FormSettings(Clock clock)
        {
            InitializeComponent();
            this.clock = clock;
            dateTimePicker.Value = clock.Time;
        }
        Clock clock;
        private void buttonOK_Click(object sender, EventArgs e)
        {
            clock.Time = this.dateTimePicker.Value;
            TimeSpan hh = TimeSpan.Parse(textBoxOffset.Text);
            clock.Offset = hh;
            if(radioButtonClockFace.Checked)
            {
                
            }
            Close();
        }
    }
}
