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
            TypeMapping = new Dictionary<Type, int>();
            DefinitionMapping = new Dictionary<Type, IBlockDefinition>();
            ColorMapping = new Dictionary<Type, int>();
			int definitionIndex = 0;
            
			foreach (var definition in definitions)
			{
                IBlock block = definition.GetInstance(OrientationFlags.None);
				int textureCount = definition.Textures.Count();
				TypeMapping.Add(definition.GetBlockType(), definitionIndex);
                int topIndex = definition.GetTopTextureIndex(block);//TODO: orientation
                Bitmap bmp = definition.Textures.ElementAt(topIndex);
                BitmapData readBmp = bmp.LockBits(new Rectangle(0, 0, 1, 1), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                unsafe
                {
                    int* firstPixel = (int*)readBmp.Scan0;

                    ColorMapping.Add(definition.GetBlockType(),*firstPixel);
                }
                bmp.UnlockBits(readBmp);
				DefinitionMapping.Add(definition.GetBlockType(), definition);
				definitionIndex += textureCount;
			}
		}
		public Dictionary<Type, IBlockDefinition> DefinitionMapping{ get; private set; }
		public Dictionary<Type, int> TypeMapping{ get; private set; }
        public Dictionary<Type, int> ColorMapping { get; private set; }
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

