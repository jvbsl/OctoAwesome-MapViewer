using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapLoader
{
    public partial class MapRenderer
    {
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MapRenderer
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Name = "MapRenderer";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MapRenderer_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MapRenderer_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MapRenderer_MouseMove);
            this.Resize += new System.EventHandler(this.MapRenderer_Resize);
            this.ResumeLayout(false);

        }
    }
}
