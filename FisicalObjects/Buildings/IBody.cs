using System;
using System.Collections.Generic;
using System.Text;

namespace Buildings
{
    interface IBody
    {
        object Create(int pozMas, int pozList, int x, int y);
        object GetTransister();
    }
}
