using System;
using System.Drawing;
using System.Windows.Forms;

using OctoAwesome;
using OctoAwesome.Runtime;
using System.Collections.Generic;

using System.Threading.Tasks;
using System.Threading;
using System.Drawing.Imaging;
using System.Diagnostics;


namespace MapLoader
{
    public partial class MapRenderer : UserControl
    {
        private BlockingQueue<Rectangle> regionQueue;
        public Map Map { get; private set; }

        private int sizeX, sizeY;
        private float scale = 1.5f;
        private int wheel = 25;
        private float offsetX, offsetY;

        private Point oldMousePos;

        private List<Thread> loadingThreads;

        private Bitmap backBuffer;
        public MapRenderer()
        {
            InitializeComponent();

            this.HandleDestroyed += MapRenderer_HandleDestroyed;
            this.MouseWheel += MapRenderer_MouseWheel;

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

            regionQueue = new BlockingQueue<Rectangle>();
            loadingThreads = new List<Thread>();
            for (int i = 0; i < 4; i++)
            {
                Thread thread = new Thread(new ThreadStart(QueueThread));
                loadingThreads.Add(thread);
                thread.Start();
            }
        }
        public void LoadMap(Map map)
        {
            this.Map = map;
        }
        public Index2 PointToMapPosition(Point pt)
        {
            if (Map == null)
                return new Index2();
            Index2 ind = new Index2((int)(pt.X / scale + offsetX), (int)(pt.Y / scale + offsetY));
            ind.NormalizeXY(new Index2(Map.Planet.Size.X * Chunk.CHUNKSIZE_X, Map.Planet.Size.Y * Chunk.CHUNKSIZE_Y));
            return ind;
        }
        void MapRenderer_HandleDestroyed(object sender, EventArgs e)
        {
            regionQueue.Close();
        }
        private void QueueThread()
        {
            Rectangle rec;
            while (regionQueue.TryDequeue(out rec))
            {
                LoadViewRange(rec.X, rec.Y, rec.Width, rec.Height);
            }
        }
        void MapRenderer_Resize(object sender, EventArgs e)
        {
            sizeX = (int)Math.Ceiling(this.ClientSize.Width / (Chunk.CHUNKSIZE_X * scale)) + 1;
            sizeY = (int)Math.Ceiling(this.ClientSize.Height / (Chunk.CHUNKSIZE_Y * scale)) + 1;
            Size newBackBufferSize = new Size((int)(sizeX * scale * Chunk.CHUNKSIZE_X), (int)(sizeY * scale * Chunk.CHUNKSIZE_Y));
            if (backBuffer == null || backBuffer.Size != newBackBufferSize)
                backBuffer = new Bitmap(newBackBufferSize.Width, newBackBufferSize.Height);
            if (Map != null)
                UpdateQueue();
        }
        public void LoadGenerator(IMapGenerator generator,int seed)
        {
            Map = new Map(generator,seed);
            //offsetX = offsetY = 0;
            UpdateQueue();
        }
        public void ThreadPoolCallback(Rectangle r)
        {
            LoadViewRange(r.X, r.Y, r.Width, r.Height);
        }
        private void RenderToBackBuffer()
        {
            int offsetX = (int)(this.offsetX / Chunk.CHUNKSIZE_X), offsetY = (int)(this.offsetY / Chunk.CHUNKSIZE_Y);
            BitmapData bmpData = backBuffer.LockBits(new Rectangle(new Point(), backBuffer.Size), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            int[] emptyData = new int[Chunk.CHUNKSIZE_X * Chunk.CHUNKSIZE_Y];
            unsafe
            {
                int* ptr = (int*)bmpData.Scan0;
                for (int y = offsetY ; y < offsetY + sizeY; y++)
                {
                    for (int x = offsetX ; x < offsetX + sizeX; x++)
                    {
                        if ((y - offsetY) < 0 || (x - offsetX) < 0)
                            continue;
                        Index2 normalized = new Index2(x, y);
                        normalized.NormalizeXY(Map.Planet.Size);
                        int index = normalized.Y * Map.Planet.Size.X + normalized.X;
                        int offset = (y - offsetY) * Chunk.CHUNKSIZE_Y * backBuffer.Width + (x - offsetX) * Chunk.CHUNKSIZE_X;
                        if (Map.Chunks[index] == null)
                        {
                            CopyRegion(backBuffer, ptr, offset, Chunk.CHUNKSIZE_X, Chunk.CHUNKSIZE_Y, emptyData);
                            continue;
                        }
                        int[] bmp = Map.Chunks[index].GetBitmapData();

                        if (bmp == null)
                            continue;

                        CopyRegion(backBuffer, ptr, offset, Chunk.CHUNKSIZE_X, Chunk.CHUNKSIZE_Y, bmp);
                        //Chunk.CHUNKSIZE_X, Chunk.CHUNKSIZE_Y
                        index++;
                    }
                }
            }
            backBuffer.UnlockBits(bmpData);
        }
        private unsafe void CopyRegion(Bitmap destination, int* ptr, int startOffset, int width, int height, int[] data)
        {
            int maxIndex = destination.Width * destination.Height;
            int offset = 0;
            int index = 0;
            for (int y = 0; y < height; y++)
            {
                offset = y * destination.Width;
                for (int x = 0; x < width; x++, index++)
                {
                    if (startOffset+offset >= maxIndex)
                        return;
                    ptr[startOffset + offset] = data[index];
                    offset++;
                }
            }
        }
        private void RenderToBackBufferGraphics()
        {
            /*Stopwatch st = new Stopwatch();
            st.Start();
            int offsetX = (int)(this.offsetX / Chunk.CHUNKSIZE_X), offsetY = (int)(this.offsetY / Chunk.CHUNKSIZE_Y);
            using (Graphics graphics = Graphics.FromImage(backBuffer))
            {
                graphics.Clear(this.BackColor);
                for (int y = offsetY; y < offsetY + sizeY; y++)
                {
                    for (int x = offsetX; x < offsetX + sizeX; x++)
                    {
                        Index2 normalized = new Index2(x, y);
                        normalized.NormalizeXY(planet.Size);
                        int index = normalized.Y * planet.Size.X + normalized.X;
                        if (chunks[index] == null)
                            continue;
                        Bitmap bmp = chunks[index].GetChunkBitmap();
                        if (bmp == null)
                            continue;
                        graphics.DrawImage(bmp, new RectangleF((x - offsetX) * Chunk.CHUNKSIZE_X, (y - offsetY) * Chunk.CHUNKSIZE_Y, Chunk.CHUNKSIZE_X, Chunk.CHUNKSIZE_Y));
                        index++;
                    }
                }
            }
            st.Stop();

            Debug.WriteLine(st.ElapsedMilliseconds);*/
        }
        private void UpdateQueue()
        {
            int offsetX = (int)(this.offsetX / Chunk.CHUNKSIZE_X), offsetY = (int)(this.offsetY / Chunk.CHUNKSIZE_Y);
            for (int y = offsetY; y < offsetY + sizeY; y += 8)
            {
                for (int x = offsetX; x < offsetX + sizeX; x += 8)
                {
                    regionQueue.Enqueue(new Rectangle(x, y, 8, 8));
                }
            }
        }
        private void MapRenderer_Paint(object sender, PaintEventArgs e)
        {
            if (Map == null || backBuffer == null)
                return;
            RenderToBackBuffer();
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            System.Drawing.Drawing2D.Matrix matrix = new System.Drawing.Drawing2D.Matrix();
            matrix.Scale(scale, scale);
            e.Graphics.Transform = matrix;
            e.Graphics.DrawImage(backBuffer, new PointF(-offsetX % Chunk.CHUNKSIZE_X, -offsetY % Chunk.CHUNKSIZE_Y));
            // e.Graphics.DrawImage(backBuffer, new Point());
            e.Graphics.Transform = new System.Drawing.Drawing2D.Matrix();
            e.Graphics.DrawLine(Pens.Red, new Point(-(int)(offsetX * scale), 0), new Point(-(int)(offsetX * scale), ClientSize.Height));
            e.Graphics.DrawLine(Pens.Red, new Point(0, -(int)(offsetY * scale)), new Point(ClientSize.Width, -(int)(offsetY * scale)));
        }
        void LoadViewRange(int offsetX, int offsetY, int sizeX, int sizeY)
        {
            for (int y = offsetY; y < offsetY + sizeY; y++)
            {
                for (int x = offsetX; x < offsetX + sizeX; x++)
                {
                    
                    Index2 normalized = new Index2(x, y);
                    normalized.NormalizeXY(Map.Planet.Size);
                    int index = normalized.Y * Map.Planet.Size.X + normalized.X;
                    if (Map.Chunks[index] != null)
                        continue;
					
					IChunk[] tempChunks = Map.Generator.GenerateChunk (BlockDefinitionManager.GetBlockDefinitions(), Map.Planet, normalized);
                    Map.Chunks[index] = new Chunk2D(tempChunks);
                    if (!IsHandleCreated)
                        return;

                }

            }
        }



        private void MapRenderer_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta != 0)
            {
                int scroll = e.Delta / 120;
                wheel += scroll;

                wheel = Math.Min(Math.Max(0, wheel), 50);

                scale = ((float)Math.Sin(wheel / 50.0 * Math.PI + Math.PI * 3 / 2) + 1);
                if (scale > 1f)
                {
                    scale *= 2;
                }

                scale = Math.Max(0.1f,scale);

                sizeX = (int)Math.Ceiling(this.ClientSize.Width / (Chunk.CHUNKSIZE_X * scale)) + 1;
                sizeY = (int)Math.Ceiling(this.ClientSize.Height / (Chunk.CHUNKSIZE_Y * scale)) + 1;
                Size newBackBufferSize = new Size((int)(sizeX * scale * Chunk.CHUNKSIZE_X), (int)(sizeY * scale * Chunk.CHUNKSIZE_Y));
                if (backBuffer == null || backBuffer.Size != newBackBufferSize)
                    backBuffer = new Bitmap(newBackBufferSize.Width, newBackBufferSize.Height);
                if (Map != null)
                    UpdateQueue();
                Invalidate();
            }
        }

        private void MapRenderer_MouseMove(object sender, MouseEventArgs e)
        {
            if (Map == null)
                return;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point delta = new Point(e.Location.X - oldMousePos.X, e.Location.Y - oldMousePos.Y);

                offsetX -= delta.X;
                offsetY -= delta.Y;

                offsetX = Math.Max(-Map.Planet.Size.X * Chunk.CHUNKSIZE_X, offsetX);
                offsetY = Math.Max(-Map.Planet.Size.Y * Chunk.CHUNKSIZE_Y, offsetY);

                oldMousePos = e.Location;

                UpdateQueue();

                Invalidate();
            }
        }
        private void MapRenderer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                oldMousePos = e.Location;
        }

    }
}

