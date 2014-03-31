using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SupportedStructures;
namespace UserControl.Menus.Elements
{
    public class Button
    {
        public string Name { get; set; }
        public Rectangle Rect { get; set; }//задает позицию и размер
        public bool Showed { get; set; }
        public bool Pressed { get; set; }
        public bool Enable { get; private set; } //введено для того, чтобы когда недоступно, но отобразить надо
        public View View { get; set; }
        public OurFonts FontName { get; private set; }
        public Label Hint { get; set; }

        public Button(string name, Rectangle rect, bool showed, bool enable, OurFonts fontName)
        {
            Name = name;
            Rect = rect;
            Showed = showed;
            Pressed = false;
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

        public Button GetCopy()
        {
            return new Button(Name, Rect, Showed, Enable, FontName);
        }
    }
}
