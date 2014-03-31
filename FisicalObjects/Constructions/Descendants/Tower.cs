using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using FisicalObjects.Transist;
using FisicalObjects.Cosmos.Minerals;
using FisicalObjects.Cosmos.Asteroids;

namespace FisicalObjects.Constructions.Descendants
{
    class Tower:Body,IBody
	{

		public const string ConstrType = "Tower";

        public int TextureBaseIndex { get; private set; }
        public int TextureTurretIndex { get; private set; }
		public float TurretAngle { get; private set; }
		public int Recharge { get; protected set; }				// Время перезарядки (но присваевается в случае выстрела)
		public int Recharged { get; protected set; }			// Оставшееся время до перезарядки, 0 - можно стрелять
		public int TargetMineralInd { get; private set; }
		public int FireRang { get; private set; }
		public string TargetType { get; private set; }
		public string FireType { get; private set; }			// Однозначный тип выстрела
		public int FireDemage { get; private set; }				// Урон при попадании
		public int FireMass { get; private set; }			// Велечина толчка при попадании
		public int RodRadius { get; private set; }
		public string RodEffect { get; private set; }
		public string HitEffect { get; private set; }

		private bool Attacking = false;			// Стреляла ли данная пушка
		private Point Target;

		private const string TargetMineral = "Mineral";
		private const string TargetEnemy = "Enemy";
		private const string TargetAlly = "Ally";
		private const string TargetRusMineral = "Миниралы";
		private const string TargetRusEnemy = "Враги";
		private const string TargetRusAlly = "Свои";

		public Tower(int pozList, int x, int y, Body body, int firer, int firem, int fired, string firet, string targt, int rech, int tbi, int tti, int rr, string re, string he)
        {
			Group = -1;
			Slots = 0;
			TurretAngle = 0;
			X = x;
			Y = y;
            PozList = pozList;
			TextureBaseIndex = tbi;
			TextureTurretIndex = tti;
			HitPoints = body.HitPoints;
			Energy = body.Energy;
			Angle = body.Angle;
			Radius = body.Radius;
			Shield = body.Shield;
			Mass = body.Mass;
			RodRadius = rr;
			RodEffect = re;
			HitEffect = he;
			TargetType = targt;
			TargetMineralInd = -1;
			FireType = firet;
			FireDemage = fired;
			Recharge = rech;
			Recharged = 0;
			FireMass = firem;
			FireRang = firer;
			Powered = false;
        }

		public Body Create(int pozList, int x, int y, int maxHP)
		{
			Body body = new Body(maxHP, Energy, Radius, Shield, Mass);
			return new Tower(pozList, x, y, body, FireRang, FireMass, FireDemage, FireType, TargetType, Recharge, TextureBaseIndex, TextureTurretIndex, RodRadius, RodEffect, HitEffect);
        }

        public object GetTransister()
        {
			return new Transist.TransTower(X, Y, TextureBaseIndex, Angle, TextureTurretIndex, TurretAngle, Radius);
		}

		public TransFire GetFireTransister()
		{
			if (Attacking)
				return new TransFire(TurretAngle, new Point(X, Y), Target, FireType, RodRadius, RodEffect);
			else
				return null;
		}

		public void DoAction()
		{
			Attacking = false;
			Powered = false;
			if ((Group != -1) && (ConstrGroup.PowerList[Group] > 0))
			{
				Powered = true;
				Angle += 0.01f;
				if (Angle >= 2 * Math.PI)
					Angle -= (float)(2 * Math.PI);
				if (Recharged == 0)
				{
					bool flag = false;
					if (TargetType == TargetEnemy)
						flag = Attack();
					if (TargetType == TargetMineral)
						flag = Mine();
					if (TargetType == TargetAlly)
						flag = Heal();
					if (flag)
					{
						Recharged = Recharge;
						Attacking = true;
						int x1, x2, y1, y2;
						x1 = 0;
						y1 = 1;
						x2 = X - Target.X;
						y2 = Y - Target.Y;
						TurretAngle = (float)Math.Acos((x1 * x2 + y1 * y2) / (Math.Sqrt((x1 * x1 + y1 * y1) * (x2 * x2 + y2 * y2))));
						if (Target.X - X < 0)
							TurretAngle *= -1;
					}
				}
				else
					Recharged--;
			}
			HitPoints -= (int)(AsterField.TryClash(new Point(X, Y), Radius, Mass) / Shield);
		}

		public void EndAction()
		{
			if ((TargetMineralInd != -1) && (TargetType == TargetMineral))
				TargetMineralInd = MineGroup.ModifyTarget(TargetMineralInd);
		}

		public void CalibrateEnergy()
		{
			if (Group != -1)
				ConstrGroup.PowerList[Group] += Energy;
		}

		public void ExplodeHit(int demage)
		{
			HitPoints -= demage;
		}

		public TransInfo GetInfo(string name, int maxhp, int maxslots, int price)
		{
			string firstinfo, secondinfo;
			firstinfo = "Прочность: " + HitPoints.ToString() + '\n' + "Максимум: " + maxhp.ToString();
			secondinfo = "Энергия - ";
			if (Energy == 0)
				secondinfo += "независит";
			if (Energy < 0)
				secondinfo += "поглощает " + (-Energy).ToString();
			if (Energy > 0)
				secondinfo += "производит " + Energy.ToString();
			secondinfo += '\n';
			secondinfo += "Энергия сети - (" + ConstrGroup.PowerList[Group].ToString() + ")" + '\n';
			secondinfo += "Цель - ";
			if (TargetType.ToString() == TargetAlly)
				secondinfo += TargetRusAlly.ToString() + '\n';
			if (TargetType.ToString() == TargetEnemy)
				secondinfo += TargetRusEnemy.ToString() + '\n';
			if (TargetType.ToString() == TargetMineral)
				secondinfo += TargetRusMineral.ToString() + '\n';
			secondinfo += "Урон - " + FireDemage.ToString() + '\n';
			secondinfo += "Масса атаки - " + FireMass.ToString() + '\n';
			secondinfo += "Дальность - " + FireRang.ToString();
			List<int> tind = new List<int>();
			tind.Add(TextureBaseIndex);
			tind.Add(TextureTurretIndex);
			return new TransInfo(name, firstinfo, secondinfo, Radius, true, tind, ConstrType, new Point(X, Y), FireRang);
		}

		private bool Attack()
		{
			Target = AsterField.Attack(new Point(X, Y), FireRang, FireDemage, FireMass, HitEffect);
			if (Target != AsterField.Null)
				return true;
			return false;
		}

		private bool Mine()
		{
			if (TargetMineralInd == -1)
				TargetMineralInd = MineGroup.GetTarget(new Point(X, Y), FireRang);
			if (TargetMineralInd != -1)
			{
				Target = MineGroup.Mine(new Point(X, Y), FireRang, FireDemage, TargetMineralInd);
				if (Target != MineGroup.Null)
					return true;
				else
					TargetMineralInd = -1;
			}
			return false;
		}

		private bool Heal()
		{
			return false;
		}
    }
}
