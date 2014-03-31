using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FisicalObjects.Transist;
using FisicalObjects.Constructions;
using FisicalObjects.Cosmos.Minerals;
using FisicalObjects.Cosmos.Asteroids;

namespace FisicalObjects
{
	public static class AIUnits
	{
		public static bool WorldExist { get; private set; }
		public static int Minerals { get; private set; }
		public static long Score { get; private set; }
		public static Point EatrhPosition { get; private set; }
		public static int WorldSize { get; private set; }
		public static int WaveCount { get; private set; }

		private const float MineScoreMult = 0.5f;
		private const float AsterScoreMult = 0.9f;
		private const int StartScore = -100;
		private const int StartMinerals = 3000;
		private const float WaveMultipler = 1.18f;
		private const int WaveAdder = 2;
		private const int WaveAdderModifer = 2;
		private const int WaveTime = 3000;
		private const int FirstWaveStartTime = 7500;

		private static int AsterCount;
		private static int Time;

		public static void CreateWorld()
		{
			WaveCount = 0;
			AsterCount = 1;
			Time = FirstWaveStartTime;
			Score = StartScore;
			Minerals = StartMinerals;
			WorldExist = true;
			EatrhPosition = new Point(WorldSize / 2, WorldSize / 2);
			ConstrGroup.CreateWorld();
			AsterField.CreateWorld(WorldSize / 2);
			MineGroup.CreateWorld(MineCluster.Create(WorldSize, 2, 600, 2, false));
			Earth.CreateWorld();
		}

		public static void DestroyWorld()
		{
			WorldExist = false;
			Minerals = 0;
			ConstrGroup.DestroyWorld();
			MineGroup.DestroyWorld();
			AsterField.DestroyWorld();
		}

		public static void Inicialize(int worldsize,int prad)
		{
			WorldSize = worldsize;
			WorldExist = false;
			Earth.Inicialize(WorldSize, prad);
			ConstrGroup.Inicialize();
			AsterAvalible.Load();
			AsterField.Inicialize(WorldSize);
			MineAvalible.Load();
		}

		public static void Process()
		{
			int mained;
			Earth.Prepare();
			AsterField.Prepare();
			ConstrGroup.DoActions();
			mained = MineGroup.EndTurn();
			Minerals += mained;
			Score += (int)(mained * MineScoreMult);
			ConstrGroup.EndActions();
			AsterField.Process();
			ConstrGroup.CalibrateEnergy();
			Score += (int)(AsterField.GetAsterMassDestroed() * AsterScoreMult);
			if (Earth.GetHitPoints() <= 0)
				DestroyWorld();
			if (Time >= WaveTime)
			{
				Time -= WaveTime;
				AsterField.CreateWave(AsterCount);
				AsterCount = (int)(WaveMultipler * AsterCount + (WaveAdder + WaveAdderModifer * WaveCount));
			}
			else
				Time++;
		}

		public static bool AddConstr(int type, Point poz)
		{
			if (!MoneyCheck(type))
				return false;
			if (ConstrGroup.Add(type, poz))
			{
				Minerals -= ConstrGroup.GetConstrPrice(type);
				ConstrGroup.CalibrateEnergy();
				return true;
			}
			return false;
		}

		public static void DestroyConstr(int type, int pozList)
		{
			ConstrGroup.Destroy(type, pozList);
			ConstrGroup.CalibrateEnergy();
		}

		public static int GetEarthHP()
		{
			return Earth.GetHitPoints();
		}

		public static List<Point> GetExplosionsOnSurface()
		{
			List<Point> expls = new List<Point>();
			expls.AddRange(Earth.GetExplosions());
			Earth.ClearExpls();
			return expls;
		}

		public static bool AnyExplsOnSurf()
		{
			return Earth.IsExpls;
		}

		public static string GetInfoByType(int type)
		{
			string temp = ConstrGroup.ConstrInfo[type].Name + '\n';
			temp += "Цена: " + ConstrGroup.ConstrInfo[type].Price + '\n';
			int energy = ConstrGroup.InfoMas[type].Energy;
			temp += "Энергия: ";
			if (energy == 0)
				temp += "независит";
			if (energy < 0)
				temp += "поглощает " + (-energy).ToString();
			if (energy > 0)
				temp += "производит " + energy.ToString();
			return temp;
		}

		public static TransBuilding[] GetTransBuildings()
		{
			return ConstrGroup.GetTransBuildings();
		}

		public static TransTower[] GetTransTowers()
		{
			return ConstrGroup.GetTransTowers();
		}

		public static TransFire[] GetTransFire()
		{
			List<TransFire> temp = ConstrGroup.GetTransFire();
			TransFire[] data = new TransFire[temp.Count];
			for (int i = 0; i < data.Length; i++)
				data[i] = temp[i];
			return data;
		}

		public static TransWay[] GetTransWays()
		{
			return ConstrGroup.GetTransWays();
		}

		public static TransMineral[] GetTransMinerals()
		{
			return MineGroup.GetTransMinerals();
		}

		public static TransAsteroid[] GetTransAsteroids()
		{
			return AsterField.GetTransAsteroids();
		}

		public static TransConstrInfo[] GetTransConstrInfo()
		{
			return ConstrGroup.GetTransConstrInfo();
		}

		public static bool CanPlace(int type, int x, int y)
		{
			if (!MoneyCheck(type))
				return false;
			return ConstrGroup.ChekPlace(type, new Point(x, y));
		}

		public static TransInfo GetObject(Point pos)
		{
			SkipObject();
			int[] constr = ConstrGroup.GetConstr(pos.X, pos.Y);
			int ind;
			if (constr.Length == 2)	// Постройка
				return ConstrGroup.GetTransInfo(new Point(constr[0], constr[1]));
			// Минерал
			ind = MineGroup.GetMiniral(pos);
			if (ind > -1)
				return MineGroup.GetTransInfo(ind);
			// Астероид
			ind = AsterField.GetAsteroid(pos);
			if (ind > -1)
				return AsterField.GetTransInfo(ind);
			return null;
		}

		public static void SkipObject()
		{
			ConstrGroup.SkipRef();
			AsterField.SkipRef();
			MineGroup.SkipRef();
		}

		public static TransInfo CheckObject()
		{
			TransInfo data = ConstrGroup.ChekRef();
			if (data == null)
				data = AsterField.ChekRef();
			if (data == null)
				data = MineGroup.ChekRef();
			return data;
		}

		public static bool MoneyCheck(int type)
		{
			if (Minerals < ConstrGroup.GetConstrPrice(type))
				return false;
			else
				return true;
		}

		public static List<int> GetFireRangs()
		{
			return ConstrGroup.GetFireRangs();
		}

		public static List<TransWay> GetFutureWays(int type, int x, int y)
		{
			return ConstrGroup.GetFutureWays(type, x, y);
		}

		public static int[] GetConstr(Point pos)   //  если нету - int[1], иначе - [type==pozMas, pozList]
		{
			return ConstrGroup.GetConstr(pos.X, pos.Y);
		}

		public static int GetConstrCount()
		{
			return ConstrGroup.InfoMas.Length;
		}

		public static int[][] GetSpcMineralsInfo()
		{
			int[][] info = new int[3][];
			info[0] = MineAvalible.Counts;
			info[1] = MineAvalible.Stages;
			info[2] = MineAvalible.TexSizes;
			return info;
		}

		public static string[] GetMineralsTypes()
		{
			return MineAvalible.Types;
		}

		public static int[][] GetSpcAsteroidsInfo()
		{
			int[][] info = new int[2][];
			info[0] = AsterAvalible.Counts;
			info[1] = AsterAvalible.TexSizes;
			return info;
		}

		public static string[] GetAsteroidsTypes()
		{
			return AsterAvalible.Types;
		}

		public static List<int> GetBuildingRads()
		{
			List<int> rads = new List<int>();
			ConstrOption[] constrs = ConstrGroup.GetConstrInfo();
			foreach (ConstrOption constr in constrs)
				if (constr.Type == "Building")
					rads.Add(constr.RadSize);
			return rads;
		}

		public static List<int> GetTowerRads()
		{
			List<int> rads = new List<int>();
			ConstrOption[] constrs = ConstrGroup.GetConstrInfo();
			foreach (ConstrOption constr in constrs)
				if (constr.Type == "Tower")
					rads.Add(constr.RadSize);
			return rads;
		}

		public static List<int> GetMiniralRads()
		{
			List<int> rads = new List<int>();
			int[] minrads = MineAvalible.GetAllRads();
			foreach (int rad in minrads)
				rads.Add(rad);
			return rads;
		}

		public static List<int> GetAsteroidRads()
		{
			List<int> rads = new List<int>();
			int[] astrads = AsterAvalible.GetAllRads();
			foreach (int rad in astrads)
				rads.Add(rad);
			return rads;
		}
	}
}
