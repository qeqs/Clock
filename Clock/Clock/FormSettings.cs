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
    //здесь все так запутанно потому что я сначала написал а потом вспомнил про декоратор!!! DONT WORK!!!!
    public partial class FormSettings : Form
    {
        public FormSettings(IClockBase clock)
        {
            InitializeComponent();
            this.ClockBase = clock;
            this.clock = (Clock)ClockBase;
            dateTimePicker.Value = ((Clock)ClockBase).Time;
        }
        
        Clock temp;
        IClockBase ClockBase;
        Clock clock;
        private void buttonOK_Click(object sender, EventArgs e)
        {
           
            temp = new Clock(clock.Location, clock.DrawingSize, clock.ParentForm);
            ((Clock)temp).Time = this.dateTimePicker.Value;
            TimeSpan hh = TimeSpan.Parse(textBoxOffset.Text);
            ((Clock)temp).Offset = hh;

            if (radioButtonClockFace.Checked)
            {
                ClockBase = (new ClockWithArrows(temp));
            }
            else
            {
                ClockBase = temp;
            }

            
            Close();
        }
    }
}
