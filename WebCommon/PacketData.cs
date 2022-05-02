using System;
using System.IO;
using System.Data;

namespace WebCommon
{
	public abstract class PacketData
	{
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Member functions

		public byte[] SerializeRaw()
		{
			MemoryStream stream = new MemoryStream();
			PacketWriter writer = new PacketWriter(stream);

			Serialize(writer);

			return stream.ToArray();
		}

		public virtual void Serialize(PacketWriter writer)
		{
		}

		public void DeserializeRaw(byte[] bytes)
		{
			MemoryStream stream = new MemoryStream(bytes);
			PacketReader reader = new PacketReader(stream);

			Deserialize(reader);
		}

		public virtual void Deserialize(PacketReader reader)
		{
		}
	}
}
