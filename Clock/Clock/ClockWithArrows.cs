using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clock
{

    // Декоратор
    class ClockWithArrows : IClockBase
    {
        Point center;
        double R;
        Clock clock;
        Graphics _g;
        Bitmap bm;


        public IClockBase Clone
        {
            get
            {
                return clock;
            }
        }

        public ClockWithArrows(IClockBase clockbase)
        {
            clock = (Clock)clockbase;
            center = new Point(clock.DrawingSize.Width / 2, clock.DrawingSize.Height / 2);
            R =( clock.DrawingSize.Width / 2 < clock.DrawingSize.Height / 2 ? clock.DrawingSize.Width / 2 : clock.DrawingSize.Height / 2)-50;
            bm = new Bitmap(clock.DrawingSize.Width, clock.DrawingSize.Height);
            _g = Graphics.FromImage(bm);
            clock.ParentForm.Resize += new EventHandler(OnResize);
        }
        public void Show()
        {
            _g.Clear(clock.Background);

            _g.DrawEllipse(new Pen(clock.Titlecol),
                new Rectangle((int)(center.X - R), (int)(center.Y - R), (int)R * 2, (int)R * 2));

            double PI = Math.PI;
            int fontsize = (int)(R * PI / 12);
            //Font font = new Font("Times New Roman", fontsize);

            double[] angles = new double[12];//{PI / 3, PI / 6, 0, 11 * PI / 6, 5 * PI / 3, 3 * PI / 2, 4 * PI / 3, 7 * PI / 6, PI, 5 * PI / 6, 2 * PI / 3, PI / 2 };

            for(int i = 0; i<angles.Length;i++)
            {
                angles[i] = ( i*PI)+(PI / 2.0) + (i) * ( 2*PI / 12.0);//что-то тут надо придумать чтобы правильно рисовались значения на циферблате
            }
            //рисуем цифры на циферблате
            for (int i = 0; i < angles.Length; i++)
            {
                _g.DrawLine(new Pen(clock.Titlecol,fontsize/10), (float)(center.X + R * Math.Cos(180 * angles[i] / PI)), (float)(center.Y - R * Math.Sin(180 * angles[i] / PI)),(float)(center.X + (R-fontsize) * Math.Cos(180 * angles[i] / PI)), (float)(center.Y - (R-fontsize) * Math.Sin(180 * angles[i] / PI)));

            }
            
            //рисуем стрелочки
            int hour = ((clock.Time.Hour >= 12) ? clock.Time.Hour - 12 : clock.Time.Hour) - 1;
            double min = 2*PI / 59 *clock.Time.Minute;
            double sec = 2*PI / 59 * clock.Time.Second;

            //часовая
            _g.DrawLine(new Pen(clock.Titlecol, fontsize / 5), center.X, center.Y,
                (float)(center.X + R / 2 * Math.Cos((180 * angles[hour] / PI))),//+ 12 * min / 360)), 
                (float)(center.Y - R / 2 * Math.Sin((180 * angles[hour] / PI))));// + 12 * min / 360)));

            //минутная
            _g.DrawLine(new Pen(clock.Titlecol, fontsize / 7), center.X, center.Y,
               (float)(center.X + R * Math.Cos(180*min/PI)), (float)(center.Y - R * Math.Sin(180*min/PI)));

            //секундная
            _g.DrawLine(new Pen(clock.Titlecol, fontsize / 10), center.X, center.Y,
               (float)(center.X + R * Math.Cos(180*sec/PI)), (float)(center.Y - R * Math.Sin(180*sec/PI)));

            clock.Graphic.DrawImage(bm as Image, clock.Location);
        }

        public void Update()
        {
            ((IClockBase)clock).Update();
        }
        private void OnResize(object sender, EventArgs e)
        {
            Form form = (Form)sender;
            Size size = form.Size;
            bm = new Bitmap(size.Width, size.Height);
            _g = Graphics.FromImage(bm);
            center = new Point(clock.DrawingSize.Width / 2, clock.DrawingSize.Height / 2);
            R =( clock.DrawingSize.Width / 2 < clock.DrawingSize.Height / 2 ? clock.DrawingSize.Width / 2 : clock.DrawingSize.Height / 2)-50;


        }
    }
}
