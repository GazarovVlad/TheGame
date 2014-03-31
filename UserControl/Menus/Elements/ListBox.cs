using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SupportedStructures;
using System.Drawing;

namespace UserControl.Menus.Elements
{
    public class ListBox
    {
        public string Name { get; set; }
        public List<Label> Values { get; set; }
        public Label State { get; set; }
        public Rectangle Rect { get; set; }//задает позицию и размер
        public bool Showed { get; set; }
        public bool Enable { get; private set; } //введено для того, чтобы когда недоступно, но отобразить надо
        public View View { get; set; }
        public OurFonts FontName { get; private set; }

        public ListBox(string name, List<Label> values, Label state, Rectangle rect, bool showed, bool enable, OurFonts fontName)
        {
            Name = name;
            Values = values;
            State = state;
            Rect = rect;
            Showed = showed;
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

        public ListBox GetCopy()
        {
            return new ListBox(Name, Values, State, Rect, Showed, Enable, FontName);
        }

    }
}
