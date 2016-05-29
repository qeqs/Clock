using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clock
{
    /// <summary>
    /// Декоратор
    /// </summary>
    class ClockWithArrows:Clock
    {
        Point center;
        double R;
        public ClockWithArrows(Point location,Size size,Form form):base(location,size,form)
        {
            center = new Point(size.Width / 2, size.Height / 2);
            R = bm.Size.Width/2 < size.Height/2 ? size.Width/2 : size.Height/2;

        }
        public override void Show()
        {
            _g.Clear(background);

            _g.DrawEllipse(new Pen(titlecol), 
                new Rectangle((int)(center.X - R), (int)(center.Y - R), (int)R, (int)R));
            
            double PI = Math.PI;
            int fontsize = (int)(R * PI / 12);
            int i = 3;
            Font font = new Font("Times New Roman", fontsize);
            _g.DrawString(i.ToString(), font, titlecol, (float)(center.X + R), center.Y);


            g.DrawImage(bm as Image, location);
        }
    }
}
