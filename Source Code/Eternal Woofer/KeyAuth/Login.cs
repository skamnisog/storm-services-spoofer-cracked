using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Siticone.UI.WinForms;
using Siticone.UI.WinForms.Enums;

namespace KeyAuth
{
	// Token: 0x02000008 RID: 8
	public partial class Login : Form
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00009771 File Offset: 0x00007971
		public Login()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00009789 File Offset: 0x00007989
		private void siticoneControlBox1_Click(object sender, EventArgs e)
		{
			Environment.Exit(0);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00009794 File Offset: 0x00007994
		private void timer1_Tick(object sender, EventArgs e)
		{
			Process[] processesByName = Process.GetProcessesByName("ida64");
			Process[] processesByName2 = Process.GetProcessesByName("ida32");
			Process[] processesByName3 = Process.GetProcessesByName("ollydbg");
			Process[] processesByName4 = Process.GetProcessesByName("ollydbg64");
			Process[] processesByName5 = Process.GetProcessesByName("loaddll");
			Process[] processesByName6 = Process.GetProcessesByName("httpdebugger");
			Process[] processesByName7 = Process.GetProcessesByName("windowrenamer");
			Process[] processesByName8 = Process.GetProcessesByName("processhacker");
			Process[] processesByName9 = Process.GetProcessesByName("Process Hacker");
			Process[] processesByName10 = Process.GetProcessesByName("ProcessHacker");
			Process[] processesByName11 = Process.GetProcessesByName("HxD");
			Process[] processesByName12 = Process.GetProcessesByName("parsecd");
			Process[] processesByName13 = Process.GetProcessesByName("ida");
			Process[] processesByName14 = Process.GetProcessesByName("dnSpy");
			Process[] processesByName15 = Process.GetProcessesByName("MegaDumper");
			bool flag = processesByName.Length != 0 || processesByName2.Length != 0 || processesByName3.Length != 0 || processesByName4.Length != 0 || processesByName5.Length != 0 || processesByName6.Length != 0 || processesByName7.Length != 0 || processesByName8.Length != 0 || processesByName9.Length != 0 || processesByName10.Length != 0 || processesByName11.Length != 0 || processesByName13.Length != 0 || processesByName12.Length != 0 || processesByName14.Length != 0 || processesByName15.Length != 0;
			if (flag)
			{
				SendKeys.Send("{PRTSC}");
				Image image = Clipboard.GetImage();
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000098B8 File Offset: 0x00007AB8
		private void Login_Load(object sender, EventArgs e)
		{
			Login.KeyAuthApp.init();
			bool flag = Login.KeyAuthApp.response.message == "invalidver";
			if (flag)
			{
				bool flag2 = !string.IsNullOrEmpty(Login.KeyAuthApp.app_data.downloadLink);
				if (flag2)
				{
					DialogResult dialogResult = MessageBox.Show("Yes to open file in browser\nNo to download file automatically", "Auto update", MessageBoxButtons.YesNo);
					DialogResult dialogResult2 = dialogResult;
					DialogResult dialogResult3 = dialogResult2;
					if (dialogResult3 != DialogResult.Yes)
					{
						if (dialogResult3 != DialogResult.No)
						{
							MessageBox.Show("Invalid option");
							Environment.Exit(0);
						}
						else
						{
							WebClient webClient = new WebClient();
							string text = Application.ExecutablePath;
							string str = Login.random_string();
							text = text.Replace(".exe", "-" + str + ".exe");
							webClient.DownloadFile(Login.KeyAuthApp.app_data.downloadLink, text);
							Process.Start(text);
							Process.Start(new ProcessStartInfo
							{
								Arguments = "/C choice /C Y /N /D Y /T 3 & Del \"" + Application.ExecutablePath + "\"",
								WindowStyle = ProcessWindowStyle.Hidden,
								CreateNoWindow = true,
								FileName = "cmd.exe"
							});
							Environment.Exit(0);
						}
					}
					else
					{
						Process.Start(Login.KeyAuthApp.app_data.downloadLink);
						Environment.Exit(0);
					}
				}
				MessageBox.Show("Posiadasz star¹ wersjê programu, pobierz now¹ za pomoc¹ komendy !download z kana³u #cmds na discordzie discord.gg/uran");
				Thread.Sleep(2500);
				Environment.Exit(0);
			}
			bool flag3 = !Login.KeyAuthApp.response.success;
			if (flag3)
			{
				MessageBox.Show(Login.KeyAuthApp.response.message);
				Environment.Exit(0);
			}
			Login.KeyAuthApp.check();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00009A70 File Offset: 0x00007C70
		private static string random_string()
		{
			string text = null;
			Random random = new Random();
			for (int i = 0; i < 5; i++)
			{
				text += Convert.ToChar(Convert.ToInt32(Math.Floor(26.0 * random.NextDouble() + 65.0))).ToString();
			}
			return text;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00009AD9 File Offset: 0x00007CD9
		private void UpgradeBtn_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00009ADC File Offset: 0x00007CDC
		private void LoginBtn_Click(object sender, EventArgs e)
		{
			Login.KeyAuthApp.login(this.username.Text, this.textBox1.Text);
			bool success = Login.KeyAuthApp.response.success;
			if (success)
			{
				Main main = new Main();
				main.Show();
				base.Hide();
			}
			else
			{
				MessageBox.Show("Username or password is invalid!");
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00009B43 File Offset: 0x00007D43
		private void RgstrBtn_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00009B46 File Offset: 0x00007D46
		private void LicBtn_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00009B49 File Offset: 0x00007D49
		private void username_TextChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00009B4C File Offset: 0x00007D4C
		private void textBox1_TextChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00009B50 File Offset: 0x00007D50
		private void siticoneRoundedButton1_Click(object sender, EventArgs e)
		{
			Login.KeyAuthApp.register(this.username.Text, this.textBox1.Text, this.textBox4.Text);
			bool success = Login.KeyAuthApp.response.success;
			if (success)
			{
				Main main = new Main();
				main.Show();
				base.Hide();
			}
			else
			{
				MessageBox.Show("License is invalid!");
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00009BC0 File Offset: 0x00007DC0
		private void guna2Button6_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00009BC3 File Offset: 0x00007DC3
		private void guna2Button1_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00009BC6 File Offset: 0x00007DC6
		private void siticoneRoundedButton2_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00009BCC File Offset: 0x00007DCC
		private void siticoneRoundedButton3_Click(object sender, EventArgs e)
		{
			Login.KeyAuthApp.register(this.username.Text, this.textBox1.Text, this.textBox4.Text);
			bool success = Login.KeyAuthApp.response.success;
			if (success)
			{
				Main main = new Main();
				main.Show();
				base.Hide();
			}
			else
			{
				MessageBox.Show("License is invalid!");
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00009C3C File Offset: 0x00007E3C
		private void guna2Panel1_Paint(object sender, PaintEventArgs e)
		{
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00009C40 File Offset: 0x00007E40
		private void guna2Button2_Click(object sender, EventArgs e)
		{
			Login.KeyAuthApp.login(this.username.Text, this.textBox1.Text);
			bool success = Login.KeyAuthApp.response.success;
			if (success)
			{
				Main main = new Main();
				main.Show();
				base.Hide();
			}
			else
			{
				MessageBox.Show("Username or password is invalid!");
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00009CAC File Offset: 0x00007EAC
		private void guna2Button1_Click_1(object sender, EventArgs e)
		{
			bool flag = this.guna2Button1.BorderThickness == 0;
			if (flag)
			{
				MessageBox.Show("To register please fill in username, password and license (Click the button again)");
				this.guna2Button1.BorderThickness = 1;
			}
			else
			{
				Login.KeyAuthApp.register(this.username.Text, this.textBox1.Text, this.textBox4.Text);
				bool success = Login.KeyAuthApp.response.success;
				if (success)
				{
					Main main = new Main();
					main.Show();
					base.Hide();
				}
				else
				{
					MessageBox.Show("License is invalid!");
				}
			}
		}

		// Token: 0x04000047 RID: 71
		public static api KeyAuthApp = new api("Eternal Woofer", "oVgAULGd6v", "620b587d26b87c566fc84ee722c010e7f5d24dc2102aafc838fd90fba1dd6435", "1.0");
	}
}
