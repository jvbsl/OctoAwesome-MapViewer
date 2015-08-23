using System;
namespace MapLoader
{
	class MainClass
	{
        [STAThread]
		public static void Main (string[] args)
		{
            System.Windows.Forms.Application.EnableVisualStyles();
            frmMain win = new frmMain();
			System.Windows.Forms.Application.Run (win);
		}
	}
}
