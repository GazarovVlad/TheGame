using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Globalization;
using System.IO;
using SupportedStructures;
using System.Drawing;

namespace TheGameDrawing.MeshRendering.RenderingObjects
{
    public class PlanetIndicator
    {
        public MeshObject Indicator { get; private set; }
        public bool Rotate { get; private set; }
        private Vector2 Pos;

        private float IncRotation = 0.002f;

        public PlanetIndicator(MeshObject indicator, Vector2 pos)
        {
            Indicator = indicator;
            Rotate = true;
            Indicator.Pos = pos;
            Pos = pos;
            Indicator.AddTurn(new Vector3(0.4f, 0, 0));
        }

        public void Render()
        {
            Rotate = !UserControl.Menus.MenuMain.Showed;
            if (Rotate)
                Indicator.AddTurn(new Vector3(IncRotation, 0.0f, 0.0f));

            ///важна последовательность отрисовки
            View.DrawMesh(Indicator, false);
        }
    }
}
