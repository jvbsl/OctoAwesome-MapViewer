using OctoAwesome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
namespace MapLoader
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();


        }
        void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            mapRenderer.Invalidate();
        }

        private void mapRenderer_MouseMove(object sender, MouseEventArgs e)
        {
            Index2 absolute = mapRenderer.PointToMapPosition(e.Location);
            Index2 chunkPosition = new Index2((int)(absolute.X / Chunk.CHUNKSIZE_X), (int)(absolute.Y / Chunk.CHUNKSIZE_Y));
            Index2 blockPosition = new Index2(absolute.X % Chunk.CHUNKSIZE_X, absolute.Y % Chunk.CHUNKSIZE_Y);
            toolStriplblPosition.Text = "Block Position: " + absolute.X + ", " + absolute.Y + " (" + chunkPosition.X + ", " + chunkPosition.Y + " / " + blockPosition.X + ", " + blockPosition.Y + ")";
        }

        private void neuToolStripButton_Click(object sender, EventArgs e)
        {
            frmNew frmNew = new frmNew();
            if (frmNew.ShowDialog() == DialogResult.OK)
            {
                mapRenderer.LoadGenerator(frmNew.MapGenerator, frmNew.Seed);
                toolStriplblSeed.Text = frmNew.Seed.ToString();
            }
        }

        private void speichernToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sv = new SaveFileDialog();
            sv.Filter = "MapFile|*.map";
            if (sv.ShowDialog() == DialogResult.OK)
            {
                if (mapRenderer.Map == null)
                    return;
                using (FileStream fs = new FileStream(sv.FileName,FileMode.OpenOrCreate,FileAccess.Write))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, mapRenderer.Map);
                }

            }
        }

        private void öffnenToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "MapFile|*.map";
            if (op.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(op.FileName, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    Map map = (Map)bf.Deserialize(fs);
                    map.ReloadGenerator();
                    mapRenderer.LoadMap(map);
                }

            }
        }
    }
}
