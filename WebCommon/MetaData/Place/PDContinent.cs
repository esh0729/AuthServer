using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace WebCommon
{
	public class PDContinent : PacketData
	{
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Member variables

		public int continentId;
		public string sceneName;
		public float x;
		public float y;
		public float z;
		public float xSize;
		public float ySize;
		public float zSize;

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Member functions

		public override void Serialize(PacketWriter writer)
		{
			base.Serialize(writer);

			writer.Write(continentId);
			writer.Write(sceneName);
			writer.Write(x);
			writer.Write(y);
			writer.Write(z);
			writer.Write(xSize);
			writer.Write(ySize);
			writer.Write(zSize);
		}

		public override void Deserialize(PacketReader reader)
		{
			base.Deserialize(reader);

			continentId = reader.ReadInt32();
			sceneName = reader.ReadString();
			x = reader.ReadSingle();
			y = reader.ReadSingle();
			z = reader.ReadSingle();
			xSize = reader.ReadSingle();
			ySize = reader.ReadSingle();
			zSize = reader.ReadSingle();
		}

		public void Set(DataRow dr)
		{
			if (dr == null)
				throw new ArgumentNullException("dr");

			continentId = Convert.ToInt32(dr["continentId"]);
			sceneName = Convert.ToString(dr["sceneName"]);
			x = Convert.ToSingle(dr["x"]);
			y = Convert.ToSingle(dr["y"]);
			z = Convert.ToSingle(dr["z"]);
			xSize = Convert.ToSingle(dr["x"]);
			ySize = Convert.ToSingle(dr["y"]);
			zSize = Convert.ToSingle(dr["z"]);
		}
	}
}
