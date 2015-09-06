using System;

using OctoAwesome.Runtime;
using System.Collections.Generic;
using OctoAwesome;

using System.Linq;
using System.Collections.Concurrent;
using System.Drawing;
using System.Drawing.Imaging;


namespace MapLoader
{
	public class TypeManager
	{

		public TypeManager ()
		{
			var definitions = BlockDefinitionManager.GetBlockDefinitions();
            ColorMapping = new Dictionary<ushort, int>();
			ushort definitionIndex = 1;
			IPlanetResourceManager res = new OrientationPlanetResManager ();

			foreach (var definition in definitions)
			{
				int topIndex = definition.GetTopTextureIndex(res,(int)OrientationFlags.None,0,0);//TODO: orientation
                Bitmap bmp = definition.Textures.ElementAt(topIndex);
                BitmapData readBmp = bmp.LockBits(new Rectangle(0, 0, 1, 1), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                unsafe
                {
                    int* firstPixel = (int*)readBmp.Scan0;

					ColorMapping.Add(definitionIndex,*firstPixel);
                }
                bmp.UnlockBits(readBmp);
				definitionIndex++;
			}
		}
		public Dictionary<ushort, int> ColorMapping { get; private set; }
		private static TypeManager instance;
        private static object locking = new object();
		public static TypeManager Instance{
			get{
                lock(locking){
                    if (instance == null)
                        instance = new TypeManager();
				    return instance;
                }
			}
		}
	}
}

