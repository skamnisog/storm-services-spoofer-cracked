﻿namespace KeyAuth
{
	// Token: 0x02000008 RID: 8
	public partial class Login : global::System.Windows.Forms.Form
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00009D4C File Offset: 0x00007F4C
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			bool flag2 = flag;
			if (flag2)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00009D88 File Offset: 0x00007F88
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::KeyAuth.Login));
			this.siticoneControlBox1 = new global::Siticone.UI.WinForms.SiticoneControlBox();
			this.siticoneControlBox2 = new global::Siticone.UI.WinForms.SiticoneControlBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.guna2BorderlessForm1 = new global::Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
			this.username = new global::System.Windows.Forms.TextBox();
			this.textBox1 = new global::System.Windows.Forms.TextBox();
			this.guna2Button1 = new global::Guna.UI2.WinForms.Guna2Button();
			this.guna2Button2 = new global::Guna.UI2.WinForms.Guna2Button();
			this.timer1 = new global::System.Windows.Forms.Timer(this.components);
			this.textBox4 = new global::System.Windows.Forms.TextBox();
			base.SuspendLayout();
			this.siticoneControlBox1.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.siticoneControlBox1.BackColor = global::System.Drawing.Color.Transparent;
			this.siticoneControlBox1.BorderRadius = 5;
			this.siticoneControlBox1.FillColor = global::System.Drawing.Color.Transparent;
			this.siticoneControlBox1.HoveredState.FillColor = global::System.Drawing.Color.FromArgb(232, 17, 35);
			this.siticoneControlBox1.HoveredState.IconColor = global::System.Drawing.Color.White;
			this.siticoneControlBox1.HoveredState.Parent = this.siticoneControlBox1;
			this.siticoneControlBox1.IconColor = global::System.Drawing.Color.White;
			this.siticoneControlBox1.Location = new global::System.Drawing.Point(717, 5);
			this.siticoneControlBox1.Name = "siticoneControlBox1";
			this.siticoneControlBox1.ShadowDecoration.Parent = this.siticoneControlBox1;
			this.siticoneControlBox1.Size = new global::System.Drawing.Size(45, 29);
			this.siticoneControlBox1.TabIndex = 1;
			this.siticoneControlBox1.Click += new global::System.EventHandler(this.siticoneControlBox1_Click);
			this.siticoneControlBox2.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.siticoneControlBox2.BackColor = global::System.Drawing.Color.Transparent;
			this.siticoneControlBox2.BorderColor = global::System.Drawing.Color.Transparent;
			this.siticoneControlBox2.BorderRadius = 10;
			this.siticoneControlBox2.ControlBoxType = global::Siticone.UI.WinForms.Enums.ControlBoxType.MinimizeBox;
			this.siticoneControlBox2.FillColor = global::System.Drawing.Color.Transparent;
			this.siticoneControlBox2.HoveredState.FillColor = global::System.Drawing.Color.FromArgb(255, 255, 255);
			this.siticoneControlBox2.HoveredState.IconColor = global::System.Drawing.Color.FromArgb(0, 0, 0);
			this.siticoneControlBox2.HoveredState.Parent = this.siticoneControlBox2;
			this.siticoneControlBox2.IconColor = global::System.Drawing.Color.White;
			this.siticoneControlBox2.Location = new global::System.Drawing.Point(666, 5);
			this.siticoneControlBox2.Name = "siticoneControlBox2";
			this.siticoneControlBox2.ShadowDecoration.Parent = this.siticoneControlBox2;
			this.siticoneControlBox2.Size = new global::System.Drawing.Size(45, 29);
			this.siticoneControlBox2.TabIndex = 2;
			this.label1.AutoSize = true;
			this.label1.Font = new global::System.Drawing.Font("Segoe UI Light", 10f);
			this.label1.ForeColor = global::System.Drawing.Color.White;
			this.label1.Location = new global::System.Drawing.Point(-5, 136);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(0, 19);
			this.label1.TabIndex = 22;
			this.label2.AutoSize = true;
			this.label2.BackColor = global::System.Drawing.Color.FromArgb(9, 9, 9);
			this.label2.Font = new global::System.Drawing.Font("Segoe UI Semibold", 10.2f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label2.ForeColor = global::System.Drawing.SystemColors.ButtonFace;
			this.label2.Location = new global::System.Drawing.Point(8, 11);
			this.label2.Margin = new global::System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(103, 19);
			this.label2.TabIndex = 27;
			this.label2.Text = "Eternal Woofer";
			this.guna2BorderlessForm1.AnimationType = global::Guna.UI2.WinForms.Guna2BorderlessForm.AnimateWindowType.AW_CENTER;
			this.guna2BorderlessForm1.BorderRadius = 30;
			this.guna2BorderlessForm1.ContainerControl = this;
			this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6;
			this.guna2BorderlessForm1.DragStartTransparencyValue = 0.3;
			this.guna2BorderlessForm1.ResizeForm = false;
			this.guna2BorderlessForm1.TransparentWhileDrag = true;
			this.username.BackColor = global::System.Drawing.Color.FromArgb(9, 9, 9);
			this.username.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
			this.username.Font = new global::System.Drawing.Font("Arial", 11.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 238);
			this.username.ForeColor = global::System.Drawing.Color.Firebrick;
			this.username.Location = new global::System.Drawing.Point(312, 193);
			this.username.Multiline = true;
			this.username.Name = "username";
			this.username.Size = new global::System.Drawing.Size(181, 20);
			this.username.TabIndex = 35;
			this.username.TextChanged += new global::System.EventHandler(this.textBox1_TextChanged);
			this.textBox1.BackColor = global::System.Drawing.Color.FromArgb(9, 9, 9);
			this.textBox1.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
			this.textBox1.Font = new global::System.Drawing.Font("Arial", 11.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 238);
			this.textBox1.ForeColor = global::System.Drawing.Color.Firebrick;
			this.textBox1.Location = new global::System.Drawing.Point(312, 248);
			this.textBox1.Name = "textBox1";
			this.textBox1.PasswordChar = '*';
			this.textBox1.Size = new global::System.Drawing.Size(181, 18);
			this.textBox1.TabIndex = 36;
			this.textBox1.UseSystemPasswordChar = true;
			this.guna2Button1.Animated = true;
			this.guna2Button1.BackColor = global::System.Drawing.Color.Transparent;
			this.guna2Button1.BorderColor = global::System.Drawing.Color.SandyBrown;
			this.guna2Button1.BorderRadius = 9;
			this.guna2Button1.DisabledState.BorderColor = global::System.Drawing.Color.DarkGray;
			this.guna2Button1.DisabledState.CustomBorderColor = global::System.Drawing.Color.DarkGray;
			this.guna2Button1.DisabledState.FillColor = global::System.Drawing.Color.FromArgb(169, 169, 169);
			this.guna2Button1.DisabledState.ForeColor = global::System.Drawing.Color.FromArgb(141, 141, 141);
			this.guna2Button1.FillColor = global::System.Drawing.Color.FromArgb(187, 2, 0);
			this.guna2Button1.Font = new global::System.Drawing.Font("Arial Black", 9.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.guna2Button1.ForeColor = global::System.Drawing.Color.White;
			this.guna2Button1.Location = new global::System.Drawing.Point(270, 417);
			this.guna2Button1.Name = "guna2Button1";
			this.guna2Button1.PressedColor = global::System.Drawing.Color.FromArgb(187, 2, 0);
			this.guna2Button1.Size = new global::System.Drawing.Size(245, 36);
			this.guna2Button1.TabIndex = 43;
			this.guna2Button1.Text = "Register";
			this.guna2Button1.Click += new global::System.EventHandler(this.guna2Button1_Click_1);
			this.guna2Button2.Animated = true;
			this.guna2Button2.BackColor = global::System.Drawing.Color.Transparent;
			this.guna2Button2.BorderColor = global::System.Drawing.Color.Transparent;
			this.guna2Button2.BorderRadius = 9;
			this.guna2Button2.DisabledState.BorderColor = global::System.Drawing.Color.DarkGray;
			this.guna2Button2.DisabledState.CustomBorderColor = global::System.Drawing.Color.DarkGray;
			this.guna2Button2.DisabledState.FillColor = global::System.Drawing.Color.FromArgb(169, 169, 169);
			this.guna2Button2.DisabledState.ForeColor = global::System.Drawing.Color.FromArgb(141, 141, 141);
			this.guna2Button2.FillColor = global::System.Drawing.Color.FromArgb(187, 2, 0);
			this.guna2Button2.Font = new global::System.Drawing.Font("Arial Black", 9.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 238);
			this.guna2Button2.ForeColor = global::System.Drawing.Color.White;
			this.guna2Button2.Location = new global::System.Drawing.Point(270, 375);
			this.guna2Button2.Name = "guna2Button2";
			this.guna2Button2.PressedColor = global::System.Drawing.Color.FromArgb(187, 2, 0);
			this.guna2Button2.Size = new global::System.Drawing.Size(245, 36);
			this.guna2Button2.TabIndex = 44;
			this.guna2Button2.Text = "Login";
			this.guna2Button2.Click += new global::System.EventHandler(this.guna2Button2_Click);
			this.timer1.Enabled = true;
			this.timer1.Interval = 1000;
			this.timer1.Tick += new global::System.EventHandler(this.timer1_Tick);
			this.textBox4.BackColor = global::System.Drawing.Color.FromArgb(9, 9, 9);
			this.textBox4.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
			this.textBox4.Font = new global::System.Drawing.Font("Arial", 11.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 238);
			this.textBox4.ForeColor = global::System.Drawing.Color.Firebrick;
			this.textBox4.Location = new global::System.Drawing.Point(312, 302);
			this.textBox4.Name = "textBox4";
			this.textBox4.PasswordChar = '*';
			this.textBox4.Size = new global::System.Drawing.Size(181, 18);
			this.textBox4.TabIndex = 42;
			this.textBox4.UseSystemPasswordChar = true;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.AutoValidate = global::System.Windows.Forms.AutoValidate.Disable;
			this.BackColor = global::System.Drawing.Color.FromArgb(35, 39, 42);
			this.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.None;
			base.ClientSize = new global::System.Drawing.Size(769, 480);
			base.Controls.Add(this.textBox4);
			base.Controls.Add(this.guna2Button2);
			base.Controls.Add(this.guna2Button1);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.username);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.siticoneControlBox2);
			base.Controls.Add(this.siticoneControlBox1);
			this.DoubleBuffered = true;
			this.ForeColor = global::System.Drawing.Color.Transparent;
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.None;
			base.Name = "Login";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Loader";
			base.TransparencyKey = global::System.Drawing.Color.Maroon;
			base.Load += new global::System.EventHandler(this.Login_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000048 RID: 72
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000049 RID: 73
		private global::Siticone.UI.WinForms.SiticoneControlBox siticoneControlBox1;

		// Token: 0x0400004A RID: 74
		private global::Siticone.UI.WinForms.SiticoneControlBox siticoneControlBox2;

		// Token: 0x0400004B RID: 75
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400004C RID: 76
		private global::System.Windows.Forms.Label label2;

		// Token: 0x0400004D RID: 77
		private global::Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;

		// Token: 0x0400004E RID: 78
		private global::System.Windows.Forms.TextBox username;

		// Token: 0x0400004F RID: 79
		private global::System.Windows.Forms.TextBox textBox1;

		// Token: 0x04000050 RID: 80
		private global::Guna.UI2.WinForms.Guna2Button guna2Button2;

		// Token: 0x04000051 RID: 81
		private global::Guna.UI2.WinForms.Guna2Button guna2Button1;

		// Token: 0x04000052 RID: 82
		private global::System.Windows.Forms.Timer timer1;

		// Token: 0x04000053 RID: 83
		private global::System.Windows.Forms.TextBox textBox4;
	}
}
