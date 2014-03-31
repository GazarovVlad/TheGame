using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using FisicalObjects.Transist;

namespace FisicalObjects.Cosmos.Aliens.Base
{
    interface IAlien
    {
        //Point Hit(Point pos, int mass, int demage, string hiteffect);
        //void ExplodeHit(Point pos, int demage, int mass);
        TransAlien GetTransister();
        void Move();
        //void Clash(int mass, float vx, float vy, float v);
        Point GetCoords();
        int GetRadius();
        bool IsAlive();
        int GetMass();
        float GetVX();
        float GetVY();
        float GetAngle();
        bool IsExplode();
        int GetIntType();
        TransInfo GetInfo();
    }
}
