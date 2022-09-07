using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace KeyAuth
{
	// Token: 0x02000004 RID: 4
	public class api
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000020FC File Offset: 0x000002FC
		public api(string name, string ownerid, string secret, string version)
		{
			bool flag = string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(ownerid) || string.IsNullOrWhiteSpace(secret) || string.IsNullOrWhiteSpace(version);
			if (flag)
			{
				api.error("Application not setup correctly.");
				Environment.Exit(0);
			}
			this.name = name;
			this.ownerid = ownerid;
			this.secret = secret;
			this.version = version;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000219C File Offset: 0x0000039C
		public void init()
		{
			this.enckey = encryption.sha256(encryption.iv_key());
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("init"));
			nameValueCollection["ver"] = encryption.encrypt(this.version, this.secret, text);
			nameValueCollection["hash"] = api.checksum(Process.GetCurrentProcess().MainModule.FileName);
			nameValueCollection["enckey"] = encryption.encrypt(this.enckey, this.secret, text);
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			bool flag = text2 == "KeyAuth_Invalid";
			if (flag)
			{
				api.error("Application not found");
				Environment.Exit(0);
			}
			text2 = encryption.decrypt(text2, this.secret, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			bool success = response_structure.success;
			if (success)
			{
				this.load_app_data(response_structure.appinfo);
				this.sessionid = response_structure.sessionid;
				this.initzalized = true;
			}
			else
			{
				bool flag2 = response_structure.message == "invalidver";
				if (flag2)
				{
					this.app_data.downloadLink = response_structure.download;
				}
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002340 File Offset: 0x00000540
		public static bool IsDebugRelease
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002354 File Offset: 0x00000554
		public void Checkinit()
		{
			bool flag = !this.initzalized;
			if (flag)
			{
				bool isDebugRelease = api.IsDebugRelease;
				if (isDebugRelease)
				{
					api.error("Not initialized Check if KeyAuthApp.init() does exist");
				}
				else
				{
					api.error("Please initialize first");
				}
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002398 File Offset: 0x00000598
		public void register(string username, string pass, string key)
		{
			this.Checkinit();
			string value = WindowsIdentity.GetCurrent().User.Value;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("register"));
			nameValueCollection["username"] = encryption.encrypt(username, this.enckey, text);
			nameValueCollection["pass"] = encryption.encrypt(pass, this.enckey, text);
			nameValueCollection["key"] = encryption.encrypt(key, this.enckey, text);
			nameValueCollection["hwid"] = encryption.encrypt(value, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			bool success = response_structure.success;
			if (success)
			{
				this.load_user_data(response_structure.info);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000250C File Offset: 0x0000070C
		public void login(string username, string pass)
		{
			this.Checkinit();
			string value = WindowsIdentity.GetCurrent().User.Value;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("login"));
			nameValueCollection["username"] = encryption.encrypt(username, this.enckey, text);
			nameValueCollection["pass"] = encryption.encrypt(pass, this.enckey, text);
			nameValueCollection["hwid"] = encryption.encrypt(value, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			bool success = response_structure.success;
			if (success)
			{
				this.load_user_data(response_structure.info);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002664 File Offset: 0x00000864
		public void upgrade(string username, string key)
		{
			this.Checkinit();
			string value = WindowsIdentity.GetCurrent().User.Value;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("upgrade"));
			nameValueCollection["username"] = encryption.encrypt(username, this.enckey, text);
			nameValueCollection["key"] = encryption.encrypt(key, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			response_structure.success = false;
			this.load_response_struct(response_structure);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002794 File Offset: 0x00000994
		public void license(string key)
		{
			this.Checkinit();
			string value = WindowsIdentity.GetCurrent().User.Value;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("license"));
			nameValueCollection["key"] = encryption.encrypt(key, this.enckey, text);
			nameValueCollection["hwid"] = encryption.encrypt(value, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			bool success = response_structure.success;
			if (success)
			{
				this.load_user_data(response_structure.info);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000028D4 File Offset: 0x00000AD4
		public void check()
		{
			this.Checkinit();
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("check"));
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure data = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(data);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000029B4 File Offset: 0x00000BB4
		public void setvar(string var, string data)
		{
			this.Checkinit();
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("setvar"));
			nameValueCollection["var"] = encryption.encrypt(var, this.enckey, text);
			nameValueCollection["data"] = encryption.encrypt(data, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure data2 = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(data2);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002AC8 File Offset: 0x00000CC8
		public string getvar(string var)
		{
			this.Checkinit();
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("getvar"));
			nameValueCollection["var"] = encryption.encrypt(var, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			bool success = response_structure.success;
			string result;
			if (success)
			{
				result = response_structure.response;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002BE0 File Offset: 0x00000DE0
		public void ban()
		{
			this.Checkinit();
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("ban"));
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure data = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(data);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002CC0 File Offset: 0x00000EC0
		public string var(string varid)
		{
			this.Checkinit();
			string value = WindowsIdentity.GetCurrent().User.Value;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("var"));
			nameValueCollection["varid"] = encryption.encrypt(varid, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			bool success = response_structure.success;
			string result;
			if (success)
			{
				result = response_structure.message;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002DEC File Offset: 0x00000FEC
		public List<api.msg> chatget(string channelname)
		{
			this.Checkinit();
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("chatget"));
			nameValueCollection["channel"] = encryption.encrypt(channelname, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			bool success = response_structure.success;
			List<api.msg> result;
			if (success)
			{
				result = response_structure.messages;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002F04 File Offset: 0x00001104
		public bool chatsend(string msg, string channelname)
		{
			this.Checkinit();
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("chatsend"));
			nameValueCollection["message"] = encryption.encrypt(msg, this.enckey, text);
			nameValueCollection["channel"] = encryption.encrypt(channelname, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			return response_structure.success;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00003030 File Offset: 0x00001230
		public bool checkblack()
		{
			this.Checkinit();
			string value = WindowsIdentity.GetCurrent().User.Value;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("checkblacklist"));
			nameValueCollection["hwid"] = encryption.encrypt(value, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			return response_structure.success;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00003154 File Offset: 0x00001354
		public string webhook(string webid, string param, string body = "", string conttype = "")
		{
			this.Checkinit();
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("webhook"));
			nameValueCollection["webid"] = encryption.encrypt(webid, this.enckey, text);
			nameValueCollection["params"] = encryption.encrypt(param, this.enckey, text);
			nameValueCollection["body"] = encryption.encrypt(body, this.enckey, text);
			nameValueCollection["conttype"] = encryption.encrypt(conttype, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			bool success = response_structure.success;
			string result;
			if (success)
			{
				result = response_structure.response;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000032B8 File Offset: 0x000014B8
		public byte[] download(string fileid)
		{
			this.Checkinit();
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("file"));
			nameValueCollection["fileid"] = encryption.encrypt(fileid, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			bool success = response_structure.success;
			byte[] result;
			if (success)
			{
				result = encryption.str_to_byte_arr(response_structure.contents);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000033D4 File Offset: 0x000015D4
		public void log(string message)
		{
			this.Checkinit();
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("log"));
			nameValueCollection["pcuser"] = encryption.encrypt(Environment.UserName, this.enckey, text);
			nameValueCollection["message"] = encryption.encrypt(message, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			api.req(post_data);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000034C8 File Offset: 0x000016C8
		public static string checksum(string filename)
		{
			string result;
			using (MD5 md = MD5.Create())
			{
				using (FileStream fileStream = File.OpenRead(filename))
				{
					byte[] value = md.ComputeHash(fileStream);
					result = BitConverter.ToString(value).Replace("-", "").ToLowerInvariant();
				}
			}
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00003548 File Offset: 0x00001748
		public static void error(string message)
		{
			Process.Start(new ProcessStartInfo("cmd.exe", "/c start cmd /C \"color b && title Error && echo " + message + " && timeout /t 5\"")
			{
				CreateNoWindow = true,
				RedirectStandardOutput = true,
				RedirectStandardError = true,
				UseShellExecute = false
			});
			Environment.Exit(0);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000035A0 File Offset: 0x000017A0
		private static string req(NameValueCollection post_data)
		{
			string result;
			try
			{
				using (WebClient webClient = new WebClient())
				{
					byte[] bytes = webClient.UploadValues("https://keyauth.win/api/1.0/", post_data);
					result = Encoding.Default.GetString(bytes);
				}
			}
			catch (WebException ex)
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)ex.Response;
				HttpStatusCode statusCode = httpWebResponse.StatusCode;
				HttpStatusCode httpStatusCode = statusCode;
				if (httpStatusCode != (HttpStatusCode)429)
				{
					api.error("Connection failure. Please try again, or contact us for help.");
					Environment.Exit(0);
					result = "";
				}
				else
				{
					api.error("Zbyt szybko podejmujesz akcje, zwolnij troche!");
					Environment.Exit(0);
					result = "";
				}
			}
			return result;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003654 File Offset: 0x00001854
		private void load_app_data(api.app_data_structure data)
		{
			this.app_data.numUsers = data.numUsers;
			this.app_data.numOnlineUsers = data.numOnlineUsers;
			this.app_data.numKeys = data.numKeys;
			this.app_data.version = data.version;
			this.app_data.customerPanelLink = data.customerPanelLink;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000036BC File Offset: 0x000018BC
		private void load_user_data(api.user_data_structure data)
		{
			this.user_data.username = data.username;
			this.user_data.ip = data.ip;
			this.user_data.hwid = data.hwid;
			this.user_data.createdate = data.createdate;
			this.user_data.lastlogin = data.lastlogin;
			this.user_data.subscriptions = data.subscriptions;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00003736 File Offset: 0x00001936
		private void load_response_struct(api.response_structure data)
		{
			this.response.success = data.success;
			this.response.message = data.message;
		}

		// Token: 0x04000004 RID: 4
		public string name;

		// Token: 0x04000005 RID: 5
		public string ownerid;

		// Token: 0x04000006 RID: 6
		public string secret;

		// Token: 0x04000007 RID: 7
		public string version;

		// Token: 0x04000008 RID: 8
		private string sessionid;

		// Token: 0x04000009 RID: 9
		private string enckey;

		// Token: 0x0400000A RID: 10
		private bool initzalized;

		// Token: 0x0400000B RID: 11
		public api.app_data_class app_data = new api.app_data_class();

		// Token: 0x0400000C RID: 12
		public api.user_data_class user_data = new api.user_data_class();

		// Token: 0x0400000D RID: 13
		public api.response_class response = new api.response_class();

		// Token: 0x0400000E RID: 14
		private json_wrapper response_decoder = new json_wrapper(new api.response_structure());

		// Token: 0x0200000A RID: 10
		[DataContract]
		private class response_structure
		{
			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000092 RID: 146 RVA: 0x0000A99A File Offset: 0x00008B9A
			// (set) Token: 0x06000093 RID: 147 RVA: 0x0000A9A2 File Offset: 0x00008BA2
			[DataMember]
			public bool success { get; set; }

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x06000094 RID: 148 RVA: 0x0000A9AB File Offset: 0x00008BAB
			// (set) Token: 0x06000095 RID: 149 RVA: 0x0000A9B3 File Offset: 0x00008BB3
			[DataMember]
			public string sessionid { get; set; }

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000096 RID: 150 RVA: 0x0000A9BC File Offset: 0x00008BBC
			// (set) Token: 0x06000097 RID: 151 RVA: 0x0000A9C4 File Offset: 0x00008BC4
			[DataMember]
			public string contents { get; set; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000098 RID: 152 RVA: 0x0000A9CD File Offset: 0x00008BCD
			// (set) Token: 0x06000099 RID: 153 RVA: 0x0000A9D5 File Offset: 0x00008BD5
			[DataMember]
			public string response { get; set; }

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x0600009A RID: 154 RVA: 0x0000A9DE File Offset: 0x00008BDE
			// (set) Token: 0x0600009B RID: 155 RVA: 0x0000A9E6 File Offset: 0x00008BE6
			[DataMember]
			public string message { get; set; }

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x0600009C RID: 156 RVA: 0x0000A9EF File Offset: 0x00008BEF
			// (set) Token: 0x0600009D RID: 157 RVA: 0x0000A9F7 File Offset: 0x00008BF7
			[DataMember]
			public string download { get; set; }

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x0600009E RID: 158 RVA: 0x0000AA00 File Offset: 0x00008C00
			// (set) Token: 0x0600009F RID: 159 RVA: 0x0000AA08 File Offset: 0x00008C08
			[DataMember(IsRequired = false, EmitDefaultValue = false)]
			public api.user_data_structure info { get; set; }

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x060000A0 RID: 160 RVA: 0x0000AA11 File Offset: 0x00008C11
			// (set) Token: 0x060000A1 RID: 161 RVA: 0x0000AA19 File Offset: 0x00008C19
			[DataMember(IsRequired = false, EmitDefaultValue = false)]
			public api.app_data_structure appinfo { get; set; }

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x060000A2 RID: 162 RVA: 0x0000AA22 File Offset: 0x00008C22
			// (set) Token: 0x060000A3 RID: 163 RVA: 0x0000AA2A File Offset: 0x00008C2A
			[DataMember]
			public List<api.msg> messages { get; set; }
		}

		// Token: 0x0200000B RID: 11
		public class msg
		{
			// Token: 0x1700000F RID: 15
			// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000AA3C File Offset: 0x00008C3C
			// (set) Token: 0x060000A6 RID: 166 RVA: 0x0000AA44 File Offset: 0x00008C44
			public string message { get; set; }

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000AA4D File Offset: 0x00008C4D
			// (set) Token: 0x060000A8 RID: 168 RVA: 0x0000AA55 File Offset: 0x00008C55
			public string author { get; set; }

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x060000A9 RID: 169 RVA: 0x0000AA5E File Offset: 0x00008C5E
			// (set) Token: 0x060000AA RID: 170 RVA: 0x0000AA66 File Offset: 0x00008C66
			public string timestamp { get; set; }
		}

		// Token: 0x0200000C RID: 12
		[DataContract]
		private class user_data_structure
		{
			// Token: 0x17000012 RID: 18
			// (get) Token: 0x060000AC RID: 172 RVA: 0x0000AA78 File Offset: 0x00008C78
			// (set) Token: 0x060000AD RID: 173 RVA: 0x0000AA80 File Offset: 0x00008C80
			[DataMember]
			public string username { get; set; }

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x060000AE RID: 174 RVA: 0x0000AA89 File Offset: 0x00008C89
			// (set) Token: 0x060000AF RID: 175 RVA: 0x0000AA91 File Offset: 0x00008C91
			[DataMember]
			public string ip { get; set; }

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000AA9A File Offset: 0x00008C9A
			// (set) Token: 0x060000B1 RID: 177 RVA: 0x0000AAA2 File Offset: 0x00008CA2
			[DataMember]
			public string hwid { get; set; }

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x060000B2 RID: 178 RVA: 0x0000AAAB File Offset: 0x00008CAB
			// (set) Token: 0x060000B3 RID: 179 RVA: 0x0000AAB3 File Offset: 0x00008CB3
			[DataMember]
			public string createdate { get; set; }

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000AABC File Offset: 0x00008CBC
			// (set) Token: 0x060000B5 RID: 181 RVA: 0x0000AAC4 File Offset: 0x00008CC4
			[DataMember]
			public string lastlogin { get; set; }

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000AACD File Offset: 0x00008CCD
			// (set) Token: 0x060000B7 RID: 183 RVA: 0x0000AAD5 File Offset: 0x00008CD5
			[DataMember]
			public List<api.Data> subscriptions { get; set; }
		}

		// Token: 0x0200000D RID: 13
		[DataContract]
		private class app_data_structure
		{
			// Token: 0x17000018 RID: 24
			// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000AAE7 File Offset: 0x00008CE7
			// (set) Token: 0x060000BA RID: 186 RVA: 0x0000AAEF File Offset: 0x00008CEF
			[DataMember]
			public string numUsers { get; set; }

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x060000BB RID: 187 RVA: 0x0000AAF8 File Offset: 0x00008CF8
			// (set) Token: 0x060000BC RID: 188 RVA: 0x0000AB00 File Offset: 0x00008D00
			[DataMember]
			public string numOnlineUsers { get; set; }

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x060000BD RID: 189 RVA: 0x0000AB09 File Offset: 0x00008D09
			// (set) Token: 0x060000BE RID: 190 RVA: 0x0000AB11 File Offset: 0x00008D11
			[DataMember]
			public string numKeys { get; set; }

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x060000BF RID: 191 RVA: 0x0000AB1A File Offset: 0x00008D1A
			// (set) Token: 0x060000C0 RID: 192 RVA: 0x0000AB22 File Offset: 0x00008D22
			[DataMember]
			public string version { get; set; }

			// Token: 0x1700001C RID: 28
			// (get) Token: 0x060000C1 RID: 193 RVA: 0x0000AB2B File Offset: 0x00008D2B
			// (set) Token: 0x060000C2 RID: 194 RVA: 0x0000AB33 File Offset: 0x00008D33
			[DataMember]
			public string customerPanelLink { get; set; }

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000AB3C File Offset: 0x00008D3C
			// (set) Token: 0x060000C4 RID: 196 RVA: 0x0000AB44 File Offset: 0x00008D44
			[DataMember]
			public string downloadLink { get; set; }
		}

		// Token: 0x0200000E RID: 14
		public class app_data_class
		{
			// Token: 0x1700001E RID: 30
			// (get) Token: 0x060000C6 RID: 198 RVA: 0x0000AB56 File Offset: 0x00008D56
			// (set) Token: 0x060000C7 RID: 199 RVA: 0x0000AB5E File Offset: 0x00008D5E
			public string numUsers { get; set; }

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x060000C8 RID: 200 RVA: 0x0000AB67 File Offset: 0x00008D67
			// (set) Token: 0x060000C9 RID: 201 RVA: 0x0000AB6F File Offset: 0x00008D6F
			public string numOnlineUsers { get; set; }

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x060000CA RID: 202 RVA: 0x0000AB78 File Offset: 0x00008D78
			// (set) Token: 0x060000CB RID: 203 RVA: 0x0000AB80 File Offset: 0x00008D80
			public string numKeys { get; set; }

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x060000CC RID: 204 RVA: 0x0000AB89 File Offset: 0x00008D89
			// (set) Token: 0x060000CD RID: 205 RVA: 0x0000AB91 File Offset: 0x00008D91
			public string version { get; set; }

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x060000CE RID: 206 RVA: 0x0000AB9A File Offset: 0x00008D9A
			// (set) Token: 0x060000CF RID: 207 RVA: 0x0000ABA2 File Offset: 0x00008DA2
			public string customerPanelLink { get; set; }

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000ABAB File Offset: 0x00008DAB
			// (set) Token: 0x060000D1 RID: 209 RVA: 0x0000ABB3 File Offset: 0x00008DB3
			public string downloadLink { get; set; }
		}

		// Token: 0x0200000F RID: 15
		public class user_data_class
		{
			// Token: 0x17000024 RID: 36
			// (get) Token: 0x060000D3 RID: 211 RVA: 0x0000ABC5 File Offset: 0x00008DC5
			// (set) Token: 0x060000D4 RID: 212 RVA: 0x0000ABCD File Offset: 0x00008DCD
			public string username { get; set; }

			// Token: 0x17000025 RID: 37
			// (get) Token: 0x060000D5 RID: 213 RVA: 0x0000ABD6 File Offset: 0x00008DD6
			// (set) Token: 0x060000D6 RID: 214 RVA: 0x0000ABDE File Offset: 0x00008DDE
			public string ip { get; set; }

			// Token: 0x17000026 RID: 38
			// (get) Token: 0x060000D7 RID: 215 RVA: 0x0000ABE7 File Offset: 0x00008DE7
			// (set) Token: 0x060000D8 RID: 216 RVA: 0x0000ABEF File Offset: 0x00008DEF
			public string hwid { get; set; }

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x060000D9 RID: 217 RVA: 0x0000ABF8 File Offset: 0x00008DF8
			// (set) Token: 0x060000DA RID: 218 RVA: 0x0000AC00 File Offset: 0x00008E00
			public string createdate { get; set; }

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x060000DB RID: 219 RVA: 0x0000AC09 File Offset: 0x00008E09
			// (set) Token: 0x060000DC RID: 220 RVA: 0x0000AC11 File Offset: 0x00008E11
			public string lastlogin { get; set; }

			// Token: 0x17000029 RID: 41
			// (get) Token: 0x060000DD RID: 221 RVA: 0x0000AC1A File Offset: 0x00008E1A
			// (set) Token: 0x060000DE RID: 222 RVA: 0x0000AC22 File Offset: 0x00008E22
			public List<api.Data> subscriptions { get; set; }
		}

		// Token: 0x02000010 RID: 16
		public class Data
		{
			// Token: 0x1700002A RID: 42
			// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000AC34 File Offset: 0x00008E34
			// (set) Token: 0x060000E1 RID: 225 RVA: 0x0000AC3C File Offset: 0x00008E3C
			public string subscription { get; set; }

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x060000E2 RID: 226 RVA: 0x0000AC45 File Offset: 0x00008E45
			// (set) Token: 0x060000E3 RID: 227 RVA: 0x0000AC4D File Offset: 0x00008E4D
			public string expiry { get; set; }

			// Token: 0x1700002C RID: 44
			// (get) Token: 0x060000E4 RID: 228 RVA: 0x0000AC56 File Offset: 0x00008E56
			// (set) Token: 0x060000E5 RID: 229 RVA: 0x0000AC5E File Offset: 0x00008E5E
			public string timeleft { get; set; }
		}

		// Token: 0x02000011 RID: 17
		public class response_class
		{
			// Token: 0x1700002D RID: 45
			// (get) Token: 0x060000E7 RID: 231 RVA: 0x0000AC70 File Offset: 0x00008E70
			// (set) Token: 0x060000E8 RID: 232 RVA: 0x0000AC78 File Offset: 0x00008E78
			public bool success { get; set; }

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x060000E9 RID: 233 RVA: 0x0000AC81 File Offset: 0x00008E81
			// (set) Token: 0x060000EA RID: 234 RVA: 0x0000AC89 File Offset: 0x00008E89
			public string message { get; set; }
		}
	}
}
