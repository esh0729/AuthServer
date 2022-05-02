using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace WebCommon
{
	public class PDCharacterAction : PacketData
	{
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Member variables

		public int characterId;
		public int actionId;

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Member functions

		public override void Serialize(PacketWriter writer)
		{
			base.Serialize(writer);

			writer.Write(characterId);
			writer.Write(actionId);
		}

		public override void Deserialize(PacketReader reader)
		{
			base.Deserialize(reader);

			characterId = reader.ReadInt32();
			actionId = reader.ReadInt32();
		}

		public void Set(DataRow dr)
		{
			if (dr == null)
				throw new ArgumentNullException("dr");

			characterId = Convert.ToInt32(dr["characterId"]);
			actionId = Convert.ToInt32(dr["actionId"]);
		}
	}
}
