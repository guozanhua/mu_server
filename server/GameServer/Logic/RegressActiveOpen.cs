﻿using System;
using System.Collections.Generic;
using System.Xml.Linq;
using GameServer.Core.Executor;
using Server.Tools;

namespace GameServer.Logic
{
	
	public class RegressActiveOpen : Activity
	{
		
		public bool Init()
		{
			RegressActiveOpen.OpenStateVavle = 0;
			this.ActivityType = 110;
			string fileName = Global.GameResPath("Config\\HuiGuiHuoDong.xml");
			XElement xml = XElement.Load(fileName);
			if (null == xml)
			{
				LogManager.WriteLog(LogTypes.Fatal, string.Format("加载系统xml配置文件:{0}, 失败。没有找到相关XML配置文件!", fileName), null, true);
			}
			try
			{
				DateTime nowDateTime = TimeUtil.NowDateTime();
				int CurrDay = Global.GetOffsetDay(nowDateTime);
				IEnumerable<XElement> xmlItems = xml.Elements();
				foreach (XElement xmlItem in xmlItems)
				{
					RegressActiveOpenXML Regress = new RegressActiveOpenXML();
					Regress.ID = Convert.ToInt32(Global.GetSafeAttributeStr(xmlItem, "ID"));
					Regress.HuoDongLevel = Convert.ToInt32(Global.GetSafeAttributeStr(xmlItem, "HuoDongLevel"));
					Regress.BeginTime = Global.GetSafeAttributeStr(xmlItem, "BeginTime");
					Regress.FinishTime = Global.GetSafeAttributeStr(xmlItem, "FinishTime");
					int Start = Global.GetOffsetDay(DateTime.Parse(Regress.BeginTime));
					int End = Global.GetOffsetDay(DateTime.Parse(Regress.FinishTime));
					if (Start <= CurrDay && CurrDay <= End)
					{
						RegressActiveOpen.OpenStateVavle = 1;
					}
					Regress.RegisterBegin = Global.GetSafeAttributeStr(xmlItem, "RegisterBegin");
					if (Regress.RegisterBegin.Equals(""))
					{
						Regress.RegisterBegin = "2000-01-01 00:00:00";
					}
					Regress.RegisterFinish = Global.GetSafeAttributeStr(xmlItem, "RegisterFinish");
					if (Regress.RegisterFinish.Equals(""))
					{
						Regress.RegisterFinish = "3000-01-01 00:00:00";
					}
					this.FromDate = Regress.BeginTime;
					this.ToDate = Regress.FinishTime;
					this.AwardStartDate = Regress.BeginTime;
					this.AwardEndDate = Regress.FinishTime;
					this.regressActiveOpenXML.Add(Regress.ID, Regress);
				}
				if (this.regressActiveOpenXML == null)
				{
					return false;
				}
				base.PredealDateTime();
			}
			catch (Exception ex)
			{
				LogManager.WriteException(ex.ToString());
			}
			return true;
		}

		
		public void OnRoleLogin(GameClient client)
		{
			if (!this.InActivityTime())
			{
				string strcmd = string.Format("{0}:{1}:{2}:{3}:{4}", new object[]
				{
					16,
					0,
					"",
					0,
					0
				});
				client.sendCmd(770, strcmd, false);
			}
			else
			{
				string strcmd = string.Format("{0}:{1}:{2}:{3}:{4}", new object[]
				{
					16,
					RegressActiveOpen.OpenStateVavle,
					"",
					0,
					0
				});
				client.sendCmd(770, strcmd, false);
			}
		}

		
		public int GetUserActiveFile(string Regtime, out int ConfID)
		{
			ConfID = 0;
			foreach (RegressActiveOpenXML it in this.regressActiveOpenXML.Values)
			{
				long RegisterBegin = DataHelper.ConvertToTicks(it.RegisterBegin);
				long RegisterFinish = DataHelper.ConvertToTicks(it.RegisterFinish);
				long RegtimeLong = DataHelper.ConvertToTicks(Regtime);
				if (RegisterBegin <= RegtimeLong && RegtimeLong < RegisterFinish)
				{
					ConfID = it.ID;
					return it.HuoDongLevel;
				}
			}
			return 0;
		}

		
		protected const string RegressActiveOpenXml = "Config\\HuiGuiHuoDong.xml";

		
		private Dictionary<int, RegressActiveOpenXML> regressActiveOpenXML = new Dictionary<int, RegressActiveOpenXML>();

		
		public static int OpenStateVavle = 0;
	}
}
