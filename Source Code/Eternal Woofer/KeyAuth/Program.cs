using System;
using System.Windows.Forms;

namespace KeyAuth
{
	// Token: 0x02000009 RID: 9
	internal static class Program
	{
		// Token: 0x06000091 RID: 145 RVA: 0x0000A97F File Offset: 0x00008B7F
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Login());
		}
	}
}
