using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SupportedStructures;
using System.Drawing;

namespace UserControl.Menus.Elements
{
    public class Label
    {
        public string Name { get; set; }
        public Rectangle Rect { get; set; }//задает позицию и размер
        public bool Showed { get; set; }
        public bool Enable { get; private set; } //введено для того, чтобы когда недоступно, но отобразить надо
        public View View { get; set; }
        public OurFonts FontName { get; private set; }

        public Label(string name, Rectangle rect, bool showed, bool enable, OurFonts fontName)
        {
            Name = name;
            Rect = rect;
            Showed = showed;
            Enable = enable;
            if (enable)
                View = View.current;
            else
                View = View.unable;
            FontName = fontName;
        }

        public Label(string name, Rectangle rect, bool showed, View view , OurFonts fontName)
        {
            Name = name;
            Rect = rect;
            Showed = showed;
            Enable = true;
            View = view;
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

        public Label GetCopy()
        {
            return new Label(Name, Rect, Showed, Enable, FontName);
        }
    }
}
