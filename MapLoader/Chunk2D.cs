using System;
using System.Drawing;
using OctoAwesome;
using System.Linq;
using System.Drawing.Imaging;


namespace MapLoader
{
    [Serializable]
	public class Chunk2D
	{
        private int[] bitmapData;
		public Chunk2D (IChunk[] chunks)
		{
			IBlock[] blocks = new IBlock[Chunk.CHUNKSIZE_X*Chunk.CHUNKSIZE_Y];
            float[] heights = new float[Chunk.CHUNKSIZE_X * Chunk.CHUNKSIZE_Y];
            int maxHeight = Chunk.CHUNKSIZE_Z*chunks.Length;
			int index = 0;
			for (int y = 0; y < Chunk.CHUNKSIZE_Y; y++) {
				for (int x = 0; x < Chunk.CHUNKSIZE_X; x++,index++) {
					bool found=false;
					for (int i=chunks.Length-1;i>=0;i--)
					{
						IChunk current = chunks [i];
						for (int z = Chunk.CHUNKSIZE_Z - 1; z >= 0; z--) {
							if ((blocks [index] = current.GetBlock (x, y, z)) != null) {
                                float percent = (float)(i * Chunk.CHUNKSIZE_Z + z) / maxHeight;
                                heights[index] = percent - 0.5f;
								found = true;
								break;
							}
						}
						if (found)
							break;
					}
				}
			}
            CreateBitmap(blocks,heights);
		}
        private int CorrectColor(int color,float correctionFactor)
        {
            byte R = (byte)(color>>16 & 0xFF);
            byte G = (byte)(color>>8 & 0xFF);
            byte B = (byte)(color & 0xFF);
            float red = Math.Max(0,(255 - R) * correctionFactor + R);
            float green = Math.Max(0,(255 - G) * correctionFactor + G);
            float blue = Math.Max(0,(255 - B) * correctionFactor + B);
            unchecked
            {
                return (int)0xFF000000 | (int)red << 16 | (int)green << 8 | (int)blue;
            }
        }
		private void CreateBitmap(IBlock[] blocks,float[] heights)
		{
			/*if (bitmap == null) {
				bitmap = new Bitmap (Chunk.CHUNKSIZE_X, Chunk.CHUNKSIZE_Y);
				BitmapData bmpData = bitmap.LockBits (new Rectangle (0, 0, Chunk.CHUNKSIZE_X, Chunk.CHUNKSIZE_Y), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				unsafe {
					int* ptr = (int*)bmpData.Scan0;
					int index = 0;
					for (int y = 0; y < Chunk.CHUNKSIZE_Y; y++) {
						for (int x = 0; x < Chunk.CHUNKSIZE_X; x++,index++) {
							IBlock block = blocks [index];
							if (block == null)
								continue;

                            int col;
                            if (TypeManager.Instance.ColorMapping.TryGetValue(block.GetType(), out col))
                            {
                                *ptr = CorrectColor(col,heights[index]);
                            }
							ptr++;
						}
					}
				}
				bitmap.UnlockBits (bmpData);
			}*/
            if (bitmapData == null)
            {
                bitmapData = new int[Chunk.CHUNKSIZE_X* Chunk.CHUNKSIZE_Y];
                int index = 0;
                for (int y = 0; y < Chunk.CHUNKSIZE_Y; y++)
                {
                    for (int x = 0; x < Chunk.CHUNKSIZE_X; x++, index++)
                    {
                        IBlock block = blocks[index];
                        if (block == null)
                            continue;

                        int col;
                        if (TypeManager.Instance.ColorMapping.TryGetValue(block.GetType(), out col))
                        {
                            bitmapData[index] = CorrectColor(col, heights[index]);
                        }
                    }
                }
            }
		}
        public int[] GetBitmapData()
        {
            return bitmapData;
        }
		/*public Bitmap GetChunkBitmap()
		{
            CreateBitmap();
			return bitmap;
		}*/
	}
}

