using System;
using OctoAwesome;

namespace MapLoader
{
	public 	class OrientationPlanetResManager : IPlanetResourceManager
	{
		#region IPlanetResourceManager implementation

		public IChunk GetChunk (Index3 index)
		{
			throw new NotImplementedException ();
		}

		public ushort GetBlock (Index3 index)
		{
			throw new NotImplementedException ();
		}

		public ushort GetBlock (int x, int y, int z)
		{
			throw new NotImplementedException ();
		}

		public void SetBlock (Index3 index, ushort block)
		{
			throw new NotImplementedException ();
		}

		public void SetBlock (int x, int y, int z, ushort block)
		{
			throw new NotImplementedException ();
		}

		public int GetBlockMeta (int x, int y, int z)
		{
			return x;
		}

		public void SetBlockMeta (int x, int y, int z, int meta)
		{
			throw new NotImplementedException ();
		}

		#endregion


	}
}

