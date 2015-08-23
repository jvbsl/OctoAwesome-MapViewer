namespace MapLoader
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.timer = new System.Timers.Timer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.neuToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.öffnenToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.speichernToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.druckenToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.ausschneidenToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.kopierenToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.einfügenToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.hilfeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStriplblPosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStriplblSeed = new System.Windows.Forms.ToolStripStatusLabel();
            this.mapRenderer = new MapLoader.MapRenderer();
            ((System.ComponentModel.ISupportInitialize)(this.timer)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 40D;
            this.timer.SynchronizingObject = this;
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.Timer_Elapsed);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.neuToolStripButton,
            this.öffnenToolStripButton,
            this.speichernToolStripButton,
            this.druckenToolStripButton,
            this.toolStripSeparator,
            this.ausschneidenToolStripButton,
            this.kopierenToolStripButton,
            this.einfügenToolStripButton,
            this.toolStripSeparator1,
            this.hilfeToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(425, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // neuToolStripButton
            // 
            this.neuToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.neuToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("neuToolStripButton.Image")));
            this.neuToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.neuToolStripButton.Name = "neuToolStripButton";
            this.neuToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.neuToolStripButton.Text = "&Neu";
            this.neuToolStripButton.Click += new System.EventHandler(this.neuToolStripButton_Click);
            // 
            // öffnenToolStripButton
            // 
            this.öffnenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.öffnenToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("öffnenToolStripButton.Image")));
            this.öffnenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.öffnenToolStripButton.Name = "öffnenToolStripButton";
            this.öffnenToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.öffnenToolStripButton.Text = "Ö&ffnen";
            this.öffnenToolStripButton.Click += new System.EventHandler(this.öffnenToolStripButton_Click);
            // 
            // speichernToolStripButton
            // 
            this.speichernToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.speichernToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("speichernToolStripButton.Image")));
            this.speichernToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.speichernToolStripButton.Name = "speichernToolStripButton";
            this.speichernToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.speichernToolStripButton.Text = "&Speichern";
            this.speichernToolStripButton.Click += new System.EventHandler(this.speichernToolStripButton_Click);
            // 
            // druckenToolStripButton
            // 
            this.druckenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.druckenToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("druckenToolStripButton.Image")));
            this.druckenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.druckenToolStripButton.Name = "druckenToolStripButton";
            this.druckenToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.druckenToolStripButton.Text = "&Drucken";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // ausschneidenToolStripButton
            // 
            this.ausschneidenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ausschneidenToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ausschneidenToolStripButton.Image")));
            this.ausschneidenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ausschneidenToolStripButton.Name = "ausschneidenToolStripButton";
            this.ausschneidenToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.ausschneidenToolStripButton.Text = "&Ausschneiden";
            // 
            // kopierenToolStripButton
            // 
            this.kopierenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.kopierenToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("kopierenToolStripButton.Image")));
            this.kopierenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.kopierenToolStripButton.Name = "kopierenToolStripButton";
            this.kopierenToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.kopierenToolStripButton.Text = "&Kopieren";
            // 
            // einfügenToolStripButton
            // 
            this.einfügenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.einfügenToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("einfügenToolStripButton.Image")));
            this.einfügenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.einfügenToolStripButton.Name = "einfügenToolStripButton";
            this.einfügenToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.einfügenToolStripButton.Text = "&Einfügen";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // hilfeToolStripButton
            // 
            this.hilfeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.hilfeToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("hilfeToolStripButton.Image")));
            this.hilfeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.hilfeToolStripButton.Name = "hilfeToolStripButton";
            this.hilfeToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.hilfeToolStripButton.Text = "Hi&lfe";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStriplblPosition,
            this.toolStripStatusLabel1,
            this.toolStriplblSeed});
            this.statusStrip1.Location = new System.Drawing.Point(0, 361);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(425, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStriplblPosition
            // 
            this.toolStriplblPosition.Name = "toolStriplblPosition";
            this.toolStriplblPosition.Size = new System.Drawing.Size(158, 17);
            this.toolStriplblPosition.Text = "Block Position: 0, 0 (0,0 / 0,0)";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(35, 17);
            this.toolStripStatusLabel1.Text = "Seed:";
            // 
            // toolStriplblSeed
            // 
            this.toolStriplblSeed.Name = "toolStriplblSeed";
            this.toolStriplblSeed.Size = new System.Drawing.Size(12, 17);
            this.toolStriplblSeed.Text = "-";
            // 
            // mapRenderer
            // 
            this.mapRenderer.BackColor = System.Drawing.Color.White;
            this.mapRenderer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapRenderer.Location = new System.Drawing.Point(0, 25);
            this.mapRenderer.Name = "mapRenderer";
            this.mapRenderer.Size = new System.Drawing.Size(425, 336);
            this.mapRenderer.TabIndex = 2;
            this.mapRenderer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mapRenderer_MouseMove);
            // 
            // frmMain
            // 
            this.ClientSize = new System.Drawing.Size(425, 383);
            this.Controls.Add(this.mapRenderer);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.MinimumSize = new System.Drawing.Size(372, 400);
            this.Name = "frmMain";
            this.Text = "MapLoader";
            ((System.ComponentModel.ISupportInitialize)(this.timer)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Timers.Timer timer;
        private MapRenderer mapRenderer;

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton neuToolStripButton;
        private System.Windows.Forms.ToolStripButton öffnenToolStripButton;
        private System.Windows.Forms.ToolStripButton speichernToolStripButton;
        private System.Windows.Forms.ToolStripButton druckenToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton ausschneidenToolStripButton;
        private System.Windows.Forms.ToolStripButton kopierenToolStripButton;
        private System.Windows.Forms.ToolStripButton einfügenToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton hilfeToolStripButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStriplblPosition;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStriplblSeed;
    }
}