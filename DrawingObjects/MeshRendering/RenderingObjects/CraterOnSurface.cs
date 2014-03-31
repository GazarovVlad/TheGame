using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;

namespace TheGameDrawing.MeshRendering.RenderingObjects
{
    public class CratersOnSurface
    {
        public MeshObject Crater { get; private set; }
        private Vector3 IncRotationEarth;
        private List<Vector3> CratersTurns;//список координат, где нужно отрисовать кратер данного типа.

        public CratersOnSurface(MeshObject crater, Vector3 incRotationEarth)
        {
            Crater = crater;
            IncRotationEarth = incRotationEarth;
            CratersTurns = new List<Vector3>();
        }

        private void IncTurn(Vector3 turn)
        {
            float max = (float)Math.PI * 2;
            for (int i = 0; i < CratersTurns.Count; i++)
                CratersTurns[i] += turn;
        }

        public void AddCrater(Vector3 turn)
        {
            CratersTurns.Add(turn);
        }

        public void Render(bool earthRotate)
        {
            if (earthRotate)
                IncTurn(-IncRotationEarth);
            for (int i = 0; i < CratersTurns.Count; i++)
            {
                Crater.Turn = CratersTurns[i];
                View.DrawMesh(Crater, true);
            }
        }
    }
}
