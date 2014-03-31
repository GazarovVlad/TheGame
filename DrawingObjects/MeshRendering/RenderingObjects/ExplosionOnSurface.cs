using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using System.Drawing;

namespace TheGameDrawing.MeshRendering.RenderingObjects
{
    public class ExplosionOnSurface
    {
        public MeshObject[] Stages { get; private set; }
        private Vector3 IncRotationEarth;

        private List<int> CurrentStages;
        private List<Vector3> ExplosionsTurns;//список координат, где нужно отрисовать взрыв данного типа.

        public ExplosionOnSurface(MeshObject[] stages, Vector3 incRotationEarth)
        {
            Stages = stages;
            IncRotationEarth = incRotationEarth;
            CurrentStages = new List<int>();
            ExplosionsTurns = new List<Vector3>();
        }

        private void IncTurn(Vector3 turn)
        {
            float max = (float)Math.PI * 2;
            for (int i = 0; i < ExplosionsTurns.Count; i++)
            {
                ExplosionsTurns[i] += turn;
                if (ExplosionsTurns[i].X >= max || ExplosionsTurns[i].Y >= max || ExplosionsTurns[i].Z >= max)
                {
                    float x = ExplosionsTurns[i].X, y = ExplosionsTurns[i].Y, z = ExplosionsTurns[i].Z;
                    if (x >= max)
                        x -= max;
                    if (y >= max)
                        y -= max;
                    if (z >= max)
                        z -= max;
                    ExplosionsTurns[i] = new Vector3(x, y, z);
                }
            }
        }

        public void AddExplosion(Vector3 turn)
        {
            ExplosionsTurns.Add(turn);
            CurrentStages.Add(0);
        }

        public void Render(bool earthRotate)
        {
            if (earthRotate)
                IncTurn(-IncRotationEarth);
            for (int i = 0; i < ExplosionsTurns.Count; i++)
            {
                Stages[CurrentStages[i]].Turn = ExplosionsTurns[i];
                View.DrawMesh(Stages[CurrentStages[i]], true);
                CurrentStages[i]++;
                if (CurrentStages[i] == Stages.Length)
                {
                    ExplosionsTurns.RemoveAt(i);
                    CurrentStages.RemoveAt(i);
                }
            }
        }

    }
}