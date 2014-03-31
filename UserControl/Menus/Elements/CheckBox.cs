using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SupportedStructures;

namespace UserControl.Menus.Elements
{
    public class CheckBox
    {
        public string Name { get; set; }
        public Rectangle Rect { get; set; }//задает позицию и размер
        public bool Showed { get; set; }
        public bool State { get; set; }
        public bool Enable { get; private set; } //введено для того, чтобы когда недоступно, но отобразить надо
        public View View { get; set; }
        public OurFonts FontName { get; private set; }

        public CheckBox(string name, Rectangle rect, bool showed, bool state, bool enable, OurFonts fontName)
        {
            Name = name;
            Rect = rect;
            Showed = showed;
            State = state;
            Enable = enable;
            if (enable)
                View = View.current;
            else
                View = View.unable;
            FontName = fontName;
        }

        public void EnableSet(bool value)
        {
            if (value)
                View = View.current;
            else
                View = View.unable;
            Enable = value;
        }

        public CheckBox GetCopy()
        {
            return new CheckBox(Name, Rect, Showed, State, Enable, FontName);
        }

    }
}
