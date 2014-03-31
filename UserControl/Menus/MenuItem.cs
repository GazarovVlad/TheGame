using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserControl.Menus.Elements;
using SupportedStructures;
using ProgramObjects.InputDevices;

namespace UserControl.Menus
{
    public class MenuItem
    {
        public bool Showed { get; private set; }
        public List<Button> Buttons;
        public List<List<CheckBox>> CheckBoxes;//в каждом списке лишь один в текущий момент активный
        public List<Label> Labels;

        public MenuItem()
        {
            Showed = false;
            Buttons = new List<Button>();
            CheckBoxes = new List<List<CheckBox>>();
            Labels = new List<Label>();
        }

        public void SetState(bool state)
        {
            Showed = state;
        }

        public void CleanPressedElements()
        {
            foreach (Button b in Buttons)
            {
                b.Pressed = false;
                if (b.Enable)
                {
                    b.View = View.current;
                    if (b.Hint != null)
                        b.Hint.Showed = false;
                }
            }
            foreach (List<CheckBox> lc in CheckBoxes)
                foreach (CheckBox c in lc)
                    if (c.Enable)
                        c.View = View.current;
        }

        public bool MousePositionControl()
        {
            int mX = Mouse.DX;
            int mY = Mouse.DY;
            int mpX = Mouse.PresedDX;
            int mpY = Mouse.PresedDY;
            foreach (Button b in Buttons)
            {
                if (b.Enable)
                {
                    if ((mX >= b.Rect.X) && (mX <= b.Rect.X + b.Rect.Width)
                        && (mY >= b.Rect.Y) && (mY <= b.Rect.Y + b.Rect.Height))
                    {
                        b.View = View.shine;
                        if (b.Hint != null)
                            b.Hint.Showed = true;
                    }

                    if ((Mouse.Pressed) &&
                        (mpX >= b.Rect.X) && (mpX <= b.Rect.X + b.Rect.Width) &&
                        (mpY >= b.Rect.Y) && (mpY <= b.Rect.Y + b.Rect.Height))
                        b.Pressed = true;
                }
            }
            bool changed = false;
            foreach (List<CheckBox> lc in CheckBoxes)
            {
                int i = -1;
                foreach (CheckBox c in lc)
                {
                    i++;
                    if (c.Enable)
                    {
                        if ((mX >= c.Rect.X) && (mX <= c.Rect.X + c.Rect.Width)
                            && (mY >= c.Rect.Y) && (mY <= c.Rect.Y + c.Rect.Height))
                            c.View = View.shine;

                        if ((Mouse.Pressed) &&
                            (mpX >= c.Rect.X) && (mpX <= c.Rect.X + c.Rect.Width) &&
                            (mpY >= c.Rect.Y) && (mpY <= c.Rect.Y + c.Rect.Height))
                        {
                            if (c.State == true)
                                continue;
                            c.State = true;
                            changed = true;
                            for (int j = 0; j < lc.Count; j++)
                                if (j != i)
                                    lc[j].State = false;
                        }
                    }
                }
            }
            return changed;
        }
    }
}
