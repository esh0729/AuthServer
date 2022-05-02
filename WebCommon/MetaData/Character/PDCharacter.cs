using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace WebCommon
{
	public class PDCharacter : PacketData
	{
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Member variables

		public int characterId;
		public float moveSpeed;

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Member functions

		public override void Serialize(PacketWriter writer)
		{
			base.Serialize(writer);

			writer.Write(characterId);
			writer.Write(moveSpeed);
		}

		public override void Deserialize(PacketReader reader)
		{
			base.Deserialize(reader);

			characterId = reader.ReadInt32();
			moveSpeed = reader.ReadSingle();
		}

		public void Set(DataRow dr)
		{
			if (dr == null)
				throw new ArgumentNullException("dr");

			characterId = Convert.ToInt32(dr["characterId"]);
			moveSpeed = Convert.ToSingle(dr["moveSpeed"]);
		}
	}
}
