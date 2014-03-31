using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using FisicalObjects.Transist;

namespace FisicalObjects.Cosmos.Asteroids.Base
{
	interface IAsteroid
	{
		Point Hit(Point pos, int mass, int demage, string hiteffect);
		void ExplodeHit(Point pos, int demage, int mass);
		TransAsteroid GetTransister();
		void Move();
		void Clash(int mass, float vx, float vy, float v);
		Point GetCoords();
		int GetRadius();
		bool IsAlive();
		int GetMass();
		float GetVX();
		float GetVY();
		bool IsExplode();
		int GetIntType();
		TransInfo GetInfo();
	}
}
