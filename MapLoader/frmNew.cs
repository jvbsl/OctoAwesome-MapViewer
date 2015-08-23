using OctoAwesome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapLoader
{
    public partial class frmNew : Form
    {
        private Random rnd = new Random();
        public frmNew()
        {
            InitializeComponent();

            cmbMapGenerator.Items.AddRange(OctoAwesome.Runtime.MapGeneratorManager.GetMapGenerators().ToArray());

        }

        public int Seed { get; private set; }
        public IMapGenerator MapGenerator { get; private set; }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSeed.Text))
                Seed = rnd.Next(int.MinValue, int.MaxValue);
            else
            {
                int seed=0;
                if (!int.TryParse(txtSeed.Text,out seed))
                {
                    foreach(char c in txtSeed.Text)
                    {
                        seed ^= ((int)c);
                        seed <<= 4;
                    }
                }
                Seed = seed;
            }
            MapGenerator = (IMapGenerator)cmbMapGenerator.SelectedItem;
        }

        private void cmbMapGenerator_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = cmbMapGenerator.SelectedItem != null;
        }
    }
}
