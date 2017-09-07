using System;

namespace DangKyModels
{
	public class CModelDef
	{
		public string Name;

		public string XFile;

		public float DegreeX;

		public float DegreeY;

		public float DegreeZ;

		public float Scale;

		public float ShiftX;

		public float ShiftY;

		public float ShiftZ;

		public int AutoTurn;

		public CModelDef()
		{
			this.Name = "Name";
			this.XFile = "";
			this.DegreeX = 0f;
			this.DegreeY = 0f;
			this.DegreeZ = 0f;
			this.Scale = 1f;
			this.ShiftX = 0f;
			this.ShiftY = 0f;
			this.ShiftZ = 0f;
			this.AutoTurn = 0;
		}

		public override string ToString()
		{
			return this.Name + ": " + this.XFile;
		}
	}
}
