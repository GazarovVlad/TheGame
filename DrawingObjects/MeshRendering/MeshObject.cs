using System;
using System.Collections.Generic;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace TheGameDrawing.MeshRendering
{
    public class MeshObject
    {
        public Mesh meshObject { get; set; }
        public Material[] meshMaterials { get; set; }
        public Texture[] meshTextures { get; set; }
        public Vector2 Pos = new Vector2();//{ get; private set; } //абсолютные координаты
        public Vector3 Turn = new Vector3();//{ get; private set; } //в радианах от 0 до 2*Math.PI по каждой оси.

        public void AddTurn(Vector3 incTurn)
        {
            float max = 2 * (float)Math.PI;
            Turn.X += incTurn.X;
            if (Turn.X >= max)
                Turn.X -= max;
            Turn.Y += incTurn.Y;
            if (Turn.Y >= max)
                Turn.Y -= max;
            Turn.Z += incTurn.Z;
            if (Turn.Z >= max)
                Turn.Z -= max;
        }

    }
}
