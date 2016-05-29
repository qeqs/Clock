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
    class ClockWithArrows:IClockBase
    {
        Point center;
        double R;
        Clock clock;
        Graphics _g;
        Bitmap bm;
        public ClockWithArrows(IClockBase clockbase)
        {
            clock = (Clock)clockbase;
            center = new Point(clock.DrawingSize.Width / 2, clock.DrawingSize.Height / 2);
            R = clock.DrawingSize.Width/2 < clock.DrawingSize.Height/2 ? clock.DrawingSize.Width/2 : clock.DrawingSize.Height/2;
            bm = new Bitmap(clock.DrawingSize.Width, clock.DrawingSize.Height);
            _g = Graphics.FromImage(bm);
        }
        public void Show()
        {
            _g.Clear(clock.Background);

            _g.DrawEllipse(new Pen(clock.Titlecol), 
                new Rectangle((int)(center.X - R), (int)(center.Y - R), (int)R, (int)R));
            
            double PI = Math.PI;
            int fontsize = (int)(R * PI / 12);
            Font font = new Font("Times New Roman", fontsize);

            double[] angles = new double[] { PI / 3, PI / 6, 0, 11 * PI / 6, 5 * PI / 3, 3 * PI / 2, 4 * PI / 3, 7 * PI / 6, PI, 5 * PI / 6, 2 * PI / 3, PI / 2 };
           
            //рисуем цифры на циферблате
            for (int i =0;i< angles.Length;i++)
            {
                _g.DrawString((i+1).ToString(), font, clock.Titlecol, (float)(center.X + R * Math.Cos(180*angles[i]/PI)), (float)(center.Y + R * Math.Sin(180*angles[i]/PI)));

            }
           
            //рисуем стрелочки
            int hour = ((clock.Time.Hour>=12)?clock.Time.Hour-12:clock.Time.Hour)-1;
            int min = 360 / 60 * clock.Time.Minute;
            int sec = 360 / 60 * clock.Time.Second;
            
            //часовая
            _g.DrawLine(new Pen(clock.Titlecol,fontsize/5),center.X,center.Y, 
                (float)(center.X + R/2 * Math.Cos((180*angles[hour]/PI)+12*min/360)), (float)(center.Y + R/2 * Math.Sin((180*angles[hour]/ PI)+12*min/360)));
            
            //минутная
            _g.DrawLine(new Pen(clock.Titlecol, fontsize / 7), center.X, center.Y,
               (float)(center.X + R * Math.Cos(min)), (float)(center.Y + R * Math.Sin(min)));
           
            //секундная
            _g.DrawLine(new Pen(clock.Titlecol, fontsize / 10), center.X, center.Y,
               (float)(center.X + R * Math.Cos(sec)), (float)(center.Y + R * Math.Sin(sec)));

            clock.Graphic.DrawImage(bm as Image, clock.Location);
        }

        public void Update()
        {
            ((IClockBase)clock).Update();
        }
    }
}
