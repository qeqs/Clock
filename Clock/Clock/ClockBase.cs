using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clock
{
    //база для часов
    public interface IClockBase
    {
         void Show();
         void Update();
    }
}
