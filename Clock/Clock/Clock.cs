using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clock
{
    public class Clock
    {
        protected Bitmap bm;
        protected Graphics g;
        protected Graphics _g;
        protected Color background = SystemColors.Control;
        protected Brush titlecol = Brushes.Black;
        protected Point location;
        protected DateTime time;
        protected TimeSpan offset;
        /// <summary>
        /// Выполняется после того как переменная времени изменилась
        /// </summary>
        public ClockEvent TimeChanged = new ClockEvent();
        //public event EventHandler TimeChanged;
        /// <summary>
        /// Выполняется полсе изменения часового пояса
        /// </summary>
        public ClockEvent OffsetChanged = new ClockEvent();
        //public event EventHandler OffsetChanged;

        public Color Background
        {
            get
            {
                return background;
            }

            set
            {
                background = value;
            }
        }

        public Point Location
        {
            get
            {
                return location;
            }

            set
            {
                location = value;
            }
        }

        public DateTime Time
        {
            get
            {
                return time+Offset;
            }

            set
            {
                time = value;
                //if (TimeChanged != null)
                //TimeChanged(this,EventArgs.Empty);
                TimeChanged.Action(this);
            }
        }

        public TimeSpan Offset
        {
            get
            {
                return offset;
            }

            set
            {
                offset = value;
                //if (OffsetChanged != null)
                //OffsetChanged(this, EventArgs.Empty);
                OffsetChanged.Action(this);
            }
        }

        public Clock(Point location,Size size,Form form)
        {
            bm = new Bitmap(size.Width, size.Height);
            g = form.CreateGraphics();
            _g = Graphics.FromImage(bm);
            this.Location = location;
            Time = DateTime.UtcNow;
            Offset = TimeSpan.Zero;
            form.Resize += new EventHandler(OnResize);
        }
        /// <summary>
        /// Отрисовка должна быть на всю форму
        /// </summary>
        public virtual void Show()
        {
            _g.Clear(background);

            string timestring = Time.ToShortTimeString();
            float fontsize = (bm.Size.Width<bm.Size.Height?bm.Size.Width:bm.Size.Height) / timestring.Length;
            Font font = new Font("Times New Roman", fontsize);
            _g.DrawString(timestring,font,titlecol, bm.Size.Width / 2 - (fontsize*(timestring.Length-1))/2, bm.Size.Height / 2 - fontsize);

            g.DrawImage(bm as Image, location);
        }
        /// <summary>
        /// Маштабирование
        /// </summary>
        protected void OnResize(object sender, EventArgs e)
        {
            Form form = (Form)sender;
            Size size = form.Size;
            bm = new Bitmap(size.Width, size.Height);
            g = form.CreateGraphics();
            _g = Graphics.FromImage(bm);

        }

        
    }
}
