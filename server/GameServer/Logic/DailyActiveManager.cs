﻿using System;
using System.Collections.Generic;
using System.Linq;
using GameServer.Core.Executor;
using Server.Data;
using Server.Tools;

namespace GameServer.Logic
{
	
	public class DailyActiveManager
	{
		
		public static void InitDailyActiveFlagIndex()
		{
			DailyActiveManager.m_DailyActiveInfo.Clear();
			int index = 0;
			DailyActiveManager.m_DailyActiveInfo.Add(100, index);
			index += 2;
			DailyActiveManager.m_DailyActiveInfo.Add(200, index);
			index += 2;
			DailyActiveManager.m_DailyActiveInfo.Add(300, index);
			index += 2;
			for (int i = 400; i <= 401; i++)
			{
				DailyActiveManager.m_DailyActiveInfo.Add(i, index);
				index += 2;
			}
			for (int i = 500; i <= 500; i++)
			{
				DailyActiveManager.m_DailyActiveInfo.Add(i, index);
				index += 2;
			}
			for (int i = 600; i <= 600; i++)
			{
				DailyActiveManager.m_DailyActiveInfo.Add(i, index);
				index += 2;
			}
			for (int i = 700; i <= 700; i++)
			{
				DailyActiveManager.m_DailyActiveInfo.Add(i, index);
				index += 2;
			}
			DailyActiveManager.m_DailyActiveInfo.Add(800, index);
			index += 2;
			DailyActiveManager.m_DailyActiveInfo.Add(900, index);
			index += 2;
			DailyActiveManager.m_DailyActiveInfo.Add(1000, index);
			index += 2;
			DailyActiveManager.m_DailyActiveInfo.Add(1100, index);
			index += 2;
			DailyActiveManager.m_DailyActiveInfo.Add(1200, index);
			index += 2;
			for (int i = 1300; i <= 1302; i++)
			{
				DailyActiveManager.m_DailyActiveInfo.Add(i, index);
				index += 2;
			}
			DailyActiveManager.m_DailyActiveInfo.Add(1400, index);
			index += 2;
			DailyActiveManager.m_DailyActiveInfo.Add(1500, index);
			index += 2;
			DailyActiveManager.m_DailyActiveInfo.Add(1600, index);
			index += 2;
		}

		
		public static void InitRoleDailyActiveData(GameClient client)
		{
			client.ClientData.DailyActiveValues = (int)DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveValue);
			client.ClientData.DailyActiveDayLginCount = DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveDayLoginNum);
			client.ClientData.DailyTotalKillMonsterNum = DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveTotalKilledMonsterNum);
			client.ClientData.DailyTotalKillKillBossNum = DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveTotalKilledBossNum);
			client.ClientData.DailyCompleteDailyTaskCount = DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveCompleteDailyTask);
			client.ClientData.DailyActiveDayBuyItemInMall = (int)DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveBuyItemInMall);
		}

		
		public static void SaveRoleDailyActiveData(GameClient client)
		{
			DailyActiveManager.ModifyDailyActiveInfor(client, client.ClientData.DailyTotalKillMonsterNum, DailyActiveDataField1.DailyActiveTotalKilledMonsterNum, true);
		}

		
		protected static ushort GetDailyActiveIDByIndex(int index)
		{
			for (int i = 0; i < DailyActiveManager.m_DailyActiveInfo.Count; i++)
			{
				if (DailyActiveManager.m_DailyActiveInfo.ElementAt(i).Value == index)
				{
					return (ushort)DailyActiveManager.m_DailyActiveInfo.ElementAt(i).Key;
				}
			}
			return 0;
		}

		
		protected static int GetCompletedFlagIndex(int DailyActiveID)
		{
			int index = -1;
			int result;
			if (DailyActiveManager.m_DailyActiveInfo.TryGetValue(DailyActiveID, out index))
			{
				result = index;
			}
			else
			{
				result = -1;
			}
			return result;
		}

		
		protected static int GetAwardFlagIndex(int DailyActiveID)
		{
			int index = -1;
			int result;
			if (DailyActiveManager.m_DailyActiveInfo.TryGetValue(DailyActiveID, out index))
			{
				result = index + 1;
			}
			else
			{
				result = -1;
			}
			return result;
		}

		
		public static void AddDailyActivePoints(GameClient client, int DailyActiveID, SystemXmlItem itemDailyActive, bool writeToDB = false)
		{
			int awardDailyActiveValue = Math.Max(0, itemDailyActive.GetIntValue("Award", -1));
			int nVipLev = client.ClientData.VipLevel;
			if (nVipLev > 0 && nVipLev <= VIPEumValue.VIPENUMVALUE_MAXLEVEL)
			{
				int[] nAddNum = GameManager.systemParamsList.GetParamValueIntArrayByName("VIPHuoYueAdd", ',');
				if (nAddNum != null && nAddNum.Length > 0 && nAddNum.Length > VIPEumValue.VIPENUMVALUE_MAXLEVEL)
				{
					awardDailyActiveValue += nAddNum[nVipLev];
				}
			}
			if (0 != awardDailyActiveValue)
			{
				client.ClientData.DailyActiveValues += awardDailyActiveValue;
				if (client.ClientData.DailyActiveValues >= 100)
				{
					WebOldPlayerManager.getInstance().ChouJiangAddCheck(client.ClientData.RoleID, 1);
				}
				client.ClientData.OnlineActiveVal += awardDailyActiveValue;
				DailyActiveManager.ModifyDailyActiveInfor(client, (uint)client.ClientData.DailyActiveValues, DailyActiveDataField1.DailyActiveValue, writeToDB);
				if (writeToDB)
				{
				}
			}
		}

		
		public static uint GetDailyActiveDataByField(GameClient client, DailyActiveDataField1 field)
		{
			List<uint> lsUint = Global.GetRoleParamsUIntListFromDB(client, "DailyActiveInfo1");
			uint result;
			if (field >= (DailyActiveDataField1)lsUint.Count)
			{
				result = 0U;
			}
			else
			{
				result = lsUint[(int)field];
			}
			return result;
		}

		
		public static void ModifyDailyActiveInfor(GameClient client, uint value, DailyActiveDataField1 field, bool writeToDB = false)
		{
			List<uint> lsUint = Global.GetRoleParamsUIntListFromDB(client, "DailyActiveInfo1");
			while (lsUint.Count < (int)(field + 1))
			{
				lsUint.Add(0U);
			}
			lsUint[(int)field] = value;
			Global.SaveRoleParamsUintListToDB(client, lsUint, "DailyActiveInfo1", writeToDB);
		}

		
		public int GetDailyActiveValue(GameClient client)
		{
			client.ClientData.DailyActiveValues = (int)DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveValue);
			return client.ClientData.DailyActiveValues;
		}

		
		public static bool IsDailyActiveCompleted(GameClient client, int DailyActiveID)
		{
			return DailyActiveManager.IsFlagIsTrue(client, DailyActiveID, false);
		}

		
		public static int IsDailyActiveAwardFetched(GameClient client, int nID)
		{
			int nFlag = Global.GetRoleParamsInt32FromDB(client, "DailyActiveAwardFlag");
			return nFlag & Global.GetBitValue(nID + 1);
		}

		
		public static void OnDailyActiveCompleted(GameClient client, int DailyActiveID)
		{
			DailyActiveManager.UpdateDailyActiveFlag(client, DailyActiveID);
			DailyActiveManager.NotifyClientDailyActiveData(client, DailyActiveID, false);
			if (client._IconStateMgr.CheckFuLiMeiRiHuoYue(client))
			{
				client._IconStateMgr.SendIconStateToClient(client);
			}
		}

		
		public static void NotifyClientDailyActiveData(GameClient client, int justCompleteddailyactive = -1, bool bRefresh = false)
		{
			if (client.ClientData.MyRoleDailyData != null && !bRefresh)
			{
				int nKillBoss = client.ClientData.MyRoleDailyData.TodayKillBoss;
			}
			DailyActiveData dailyactiveData = new DailyActiveData
			{
				RoleID = client.ClientData.RoleID,
				DailyActiveValues = (long)client.ClientData.DailyActiveValues,
				TotalKilledMonsterCount = (long)((ulong)client.ClientData.DailyTotalKillMonsterNum),
				DailyActiveTotalLoginCount = (long)((ulong)client.ClientData.DailyActiveDayLginCount),
				DailyActiveOnLineTimer = client.ClientData.DayOnlineSecond,
				DailyActiveInforFlags = DailyActiveManager.GetDailyActiveInfoArray(client),
				NowCompletedDailyActiveID = justCompleteddailyactive,
				TotalKilledBossCount = (int)client.ClientData.DailyTotalKillKillBossNum,
				PassNormalCopySceneNum = (int)DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveCompleteCopyMap1),
				PassHardCopySceneNum = (int)DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveCompleteCopyMap2),
				PassDifficultCopySceneNum = (int)DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveCompleteCopyMap3),
				BuyItemInMall = client.ClientData.DailyActiveDayBuyItemInMall,
				CompleteDailyTaskCount = (int)DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveCompleteDailyTask),
				CompleteBloodCastleCount = (int)DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveCompleteBloodCastle),
				CompleteDaimonSquareCount = (int)DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveCompleteDaimonSquare),
				CompleteBattleCount = (int)DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveCompleteBattle),
				EquipForge = (int)DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveEquipForge),
				EquipAppend = (int)DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveEquipAppend),
				ChangeLife = (int)DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveChangeLife),
				MergeFruit = (int)DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveMergeFruit),
				GetAwardFlag = Global.GetRoleParamsInt32FromDB(client, "DailyActiveAwardFlag")
			};
			byte[] bytesData = DataHelper.ObjectToBytes<DailyActiveData>(dailyactiveData);
			GameManager.ClientMgr.SendToClient(client, bytesData, 558);
		}

		
		protected static List<ushort> GetDailyActiveInfoArray(GameClient client)
		{
			List<ulong> lsLong = Global.GetRoleParamsUlongListFromDB(client, "DailyActiveFlag");
			int curIndex = 0;
			List<ushort> lsUshort = new List<ushort>();
			for (int i = 0; i < lsLong.Count; i++)
			{
				ulong uValue = lsLong[i];
				for (int subIndex = 0; subIndex < 64; subIndex += 2)
				{
					ulong flag = 3UL << subIndex;
					ushort realFlag = (ushort)((uValue & flag) >> subIndex);
					ushort dailyactiveID = DailyActiveManager.GetDailyActiveIDByIndex(curIndex);
					ushort preFix = (ushort)(dailyactiveID << 2);
					ushort dailyactive = (ushort)(preFix | realFlag);
					lsUshort.Add(dailyactive);
					curIndex += 2;
				}
			}
			return lsUshort;
		}

		
		public static int GiveDailyActiveAward(GameClient client, int nid)
		{
			int awardDailyActiveValue = 0;
			SystemXmlItem itemDailyActive = null;
			if (GameManager.systemDailyActiveAward.SystemXmlItemDict.TryGetValue(nid, out itemDailyActive))
			{
				awardDailyActiveValue = Math.Max(0, itemDailyActive.GetIntValue("NeedhuoYue", -1));
			}
			int result;
			if (awardDailyActiveValue > client.ClientData.DailyActiveValues)
			{
				result = -3;
			}
			else if (DailyActiveManager.IsDailyActiveAwardFetched(client, nid) > 0)
			{
				result = -2;
			}
			else
			{
				DailyActiveManager.ModifyDailyActiveInfor(client, (uint)client.ClientData.DailyActiveValues, DailyActiveDataField1.DailyActiveValue, true);
				List<GoodsData> goodsDataList = new List<GoodsData>();
				string strGoods = itemDailyActive.GetStringValue("GoodsID");
				if (!string.IsNullOrEmpty(strGoods))
				{
					string[] fields = strGoods.Split(new char[]
					{
						'|'
					});
					if (null != fields)
					{
						for (int i = 0; i < fields.Length; i++)
						{
							string strID = fields[i];
							string[] strinfro = fields[i].Split(new char[]
							{
								','
							});
							if (strinfro != null && strinfro.Length == 7)
							{
								GoodsData good = new GoodsData
								{
									Id = -1,
									GoodsID = Convert.ToInt32(strinfro[0]),
									Using = 0,
									Forge_level = Convert.ToInt32(strinfro[3]),
									Starttime = "1900-01-01 12:00:00",
									Endtime = "1900-01-01 12:00:00",
									Site = 0,
									Quality = 0,
									Props = "",
									GCount = Convert.ToInt32(strinfro[1]),
									Binding = Convert.ToInt32(strinfro[2]),
									Jewellist = "",
									BagIndex = 0,
									AddPropIndex = 0,
									BornIndex = 0,
									Lucky = Convert.ToInt32(strinfro[5]),
									Strong = 0,
									ExcellenceInfo = Convert.ToInt32(strinfro[6]),
									AppendPropLev = Convert.ToInt32(strinfro[4]),
									ChangeLifeLevForEquip = 0
								};
								goodsDataList.Add(good);
							}
						}
						if (!Global.CanAddGoodsNum(client, goodsDataList.Count))
						{
							foreach (GoodsData item in goodsDataList)
							{
								Global.UseMailGivePlayerAward(client, item, GLang.GetLang(100, new object[0]), GLang.GetLang(100, new object[0]), 1.0);
							}
						}
						else
						{
							foreach (GoodsData item in goodsDataList)
							{
								GoodsData goodsData = new GoodsData
								{
									Id = -1,
									GoodsID = item.GoodsID,
									Using = 0,
									Forge_level = item.Forge_level,
									Starttime = "1900-01-01 12:00:00",
									Endtime = "1900-01-01 12:00:00",
									Site = 0,
									Quality = item.Quality,
									Props = item.Props,
									GCount = item.GCount,
									Binding = item.Binding,
									Jewellist = item.Jewellist,
									BagIndex = 0,
									AddPropIndex = item.AddPropIndex,
									BornIndex = item.BornIndex,
									Lucky = item.Lucky,
									Strong = item.Strong,
									ExcellenceInfo = item.ExcellenceInfo,
									AppendPropLev = item.AppendPropLev,
									ChangeLifeLevForEquip = item.ChangeLifeLevForEquip
								};
								goodsData.Id = Global.AddGoodsDBCommand_Hook(Global._TCPManager.TcpOutPacketPool, client, goodsData.GoodsID, goodsData.GCount, goodsData.Quality, goodsData.Props, goodsData.Forge_level, goodsData.Binding, 0, goodsData.Jewellist, true, 1, "副本通关获取物品", false, goodsData.Endtime, goodsData.AddPropIndex, goodsData.BornIndex, goodsData.Lucky, goodsData.Strong, goodsData.ExcellenceInfo, goodsData.AppendPropLev, goodsData.ChangeLifeLevForEquip, true, null, null, "1900-01-01 12:00:00", 0, true);
							}
							client.ClientData.AddAwardRecord(RoleAwardMsg.DailyActive, goodsDataList, false);
							GameManager.ClientMgr.NotifyGetAwardMsg(client, RoleAwardMsg.DailyActive, "");
						}
					}
				}
				DailyActiveManager.UpdateDailyActiveAwardFlag(client, nid);
				result = 1;
			}
			return result;
		}

		
		public static bool IsFlagIsTrue(GameClient client, int DailyActiveID, bool forAward = false)
		{
			int index = DailyActiveManager.GetCompletedFlagIndex(DailyActiveID);
			bool result;
			if (index < 0)
			{
				result = false;
			}
			else
			{
				if (forAward)
				{
					index++;
				}
				List<ulong> lsLong = Global.GetRoleParamsUlongListFromDB(client, "DailyActiveFlag");
				if (lsLong.Count <= 0)
				{
					result = false;
				}
				else
				{
					int arrPosIndex = index / 64;
					if (arrPosIndex >= lsLong.Count)
					{
						result = false;
					}
					else
					{
						int subIndex = index % 64;
						ulong destLong = lsLong[arrPosIndex];
						ulong flag = 1UL << subIndex;
						bool bResult = (destLong & flag) > 0UL;
						result = bResult;
					}
				}
			}
			return result;
		}

		
		public static bool UpdateDailyActiveFlag(GameClient client, int DailyActiveID)
		{
			int index = DailyActiveManager.GetCompletedFlagIndex(DailyActiveID);
			bool result;
			if (index < 0)
			{
				result = false;
			}
			else
			{
				List<ulong> lsLong = Global.GetRoleParamsUlongListFromDB(client, "DailyActiveFlag");
				int arrPosIndex = index / 64;
				while (arrPosIndex > lsLong.Count - 1)
				{
					lsLong.Add(0UL);
				}
				int subIndex = index % 64;
				ulong destLong = lsLong[arrPosIndex];
				ulong flag = 1UL << subIndex;
				lsLong[arrPosIndex] = (destLong | flag);
				Global.SaveRoleParamsUlongListToDB(client, lsLong, "DailyActiveFlag", true);
				result = true;
			}
			return result;
		}

		
		public static void UpdateDailyActiveAwardFlag(GameClient client, int nID)
		{
			int nFlag = Global.GetRoleParamsInt32FromDB(client, "DailyActiveAwardFlag");
			int i = Global.SetIntSomeBit(nID, nFlag, true);
			Global.SaveRoleParamsInt32ValueToDB(client, "DailyActiveAwardFlag", i, true);
		}

		
		public static void ProcessDailyActiveKillMonster(GameClient killer, Monster victim)
		{
			if (DailyActiveManager.CheckLevCondition(killer, 1300) || DailyActiveManager.CheckLevCondition(killer, 1301) || DailyActiveManager.CheckLevCondition(killer, 1302))
			{
				killer.ClientData.DailyTotalKillMonsterNum += 1U;
				SafeClientData clientData = killer.ClientData;
				clientData.TimerKilledMonsterNum += 1;
				if (killer.ClientData.TimerKilledMonsterNum > 20)
				{
					killer.ClientData.TimerKilledMonsterNum = 0;
					DailyActiveManager.ModifyDailyActiveInfor(killer, killer.ClientData.DailyTotalKillMonsterNum, DailyActiveDataField1.DailyActiveTotalKilledMonsterNum, false);
				}
				DailyActiveManager.CheckDailyActiveKillMonster(killer);
				if (401 == victim.MonsterType)
				{
					for (int i = 0; i < Data.KillBossCountForChengJiu.Length; i++)
					{
						if (victim.MonsterInfo.ExtensionID == Data.KillBossCountForChengJiu[i])
						{
							DailyActiveManager.CheckDailyActiveKillBoss(killer);
						}
					}
				}
			}
		}

		
		public static void CheckDailyActiveKillMonster(GameClient client)
		{
			if (client.ClientData.DailyTotalKillMonsterNum >= client.ClientData.DailyNextKillMonsterNum && 2147483647U != client.ClientData.DailyNextKillMonsterNum)
			{
				bool bIsCompleted = false;
				uint nextNeedNum = DailyActiveManager.CheckSingleConditionForDailyActive(client, 1300, 1302, (long)((ulong)client.ClientData.DailyTotalKillMonsterNum), "KillMonster", out bIsCompleted);
				client.ClientData.DailyNextKillMonsterNum = nextNeedNum;
				if (DailyActiveManager.IsDailyActiveCompleted(client, 1302))
				{
					client.ClientData.DailyNextKillMonsterNum = 2147483647U;
				}
			}
		}

		
		public static void CheckDailyActiveKillBoss(GameClient client)
		{
			if (!DailyActiveManager.IsDailyActiveCompleted(client, 1400))
			{
				if (DailyActiveManager.CheckLevCondition(client, 1400))
				{
					bool bIsCompleted = false;
					client.ClientData.DailyTotalKillKillBossNum += 1U;
					DailyActiveManager.ModifyDailyActiveInfor(client, client.ClientData.DailyTotalKillKillBossNum, DailyActiveDataField1.DailyActiveTotalKilledBossNum, true);
					DailyActiveManager.CheckSingleConditionForDailyActive(client, 1400, 1400, (long)client.ClientData.MyRoleDailyData.TodayKillBoss, "KillBoss", out bIsCompleted);
				}
			}
		}

		
		protected static uint CheckSingleConditionForDailyActive(GameClient client, int DailyActiveMinID, int DailyActiveMaxID, long roleCurrentValue, string strCheckField, out bool bIsCompleted)
		{
			bIsCompleted = false;
			SystemXmlItem itemDailyActive = null;
			uint needMinValue = 0U;
			for (int DailyActiveID = DailyActiveMinID; DailyActiveID <= DailyActiveMaxID; DailyActiveID++)
			{
				if (DailyActiveManager.CheckLevCondition(client, DailyActiveID))
				{
					if (GameManager.systemDailyActiveInfo.SystemXmlItemDict.TryGetValue(DailyActiveID, out itemDailyActive))
					{
						if (null != itemDailyActive)
						{
							needMinValue = (uint)itemDailyActive.GetIntValue(strCheckField, -1);
							if (roleCurrentValue < (long)((ulong)needMinValue))
							{
								break;
							}
							if (!DailyActiveManager.IsDailyActiveCompleted(client, DailyActiveID))
							{
								DailyActiveManager.AddDailyActivePoints(client, DailyActiveID, itemDailyActive, true);
								DailyActiveManager.OnDailyActiveCompleted(client, DailyActiveID);
								string analysisLog = string.Format("huoyue server={0} account={1} player={2} zoneid={3} task_id={4}", new object[]
								{
									GameManager.ServerId,
									client.strUserID,
									client.ClientData.LocalRoleID,
									client.ClientData.ZoneID,
									DailyActiveID
								});
								LogManager.WriteLog(LogTypes.Analysis, analysisLog, null, true);
								bIsCompleted = true;
							}
						}
					}
				}
			}
			return needMinValue;
		}

		
		public static void ProcessOnlineForDailyActive(GameClient client)
		{
			bool bIsCompleted = false;
			if (!DailyActiveManager.IsDailyActiveCompleted(client, 200))
			{
				if (client.ClientData.DayOnlineSecond - client.ClientData.DailyOnlineTimeTmp > 0)
				{
					client.ClientData.DailyOnlineTimeTmp += 60;
					if (!DailyActiveManager.CheckLevCondition(client, 200))
					{
						bIsCompleted = false;
					}
					else
					{
						DailyActiveManager.CheckSingleConditionForDailyActive(client, 200, 200, (long)(client.ClientData.DayOnlineSecond / 60), "Online", out bIsCompleted);
					}
				}
			}
		}

		
		public static void ProcessLoginForDailyActive(GameClient client, out bool bIsCompleted)
		{
			bIsCompleted = false;
			if (!DailyActiveManager.IsDailyActiveCompleted(client, 100))
			{
				if (DailyActiveManager.CheckLevCondition(client, 100))
				{
					client.ClientData.DailyActiveDayLginCount += 1U;
					uint nvalue = DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveDayLoginNum);
					DailyActiveManager.ModifyDailyActiveInfor(client, client.ClientData.DailyActiveDayLginCount, DailyActiveDataField1.DailyActiveDayLoginNum, true);
					nvalue = DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveDayLoginNum);
					DailyActiveManager.CheckSingleConditionForDailyActive(client, 100, 100, (long)((ulong)client.ClientData.DailyActiveDayLginCount), "Login", out bIsCompleted);
					client.ClientData.DailyActiveDayLginSetFlag = true;
				}
			}
		}

		
		public static void ProcessBuyItemInMallForDailyActive(GameClient client, int nValue)
		{
			int xiaoFei = Global.GetRoleParamsInt32FromDB(client, "10175");
			xiaoFei += nValue;
			if (xiaoFei >= 100)
			{
				WebOldPlayerManager.getInstance().ChouJiangAddCheck(client.ClientData.RoleID, 2);
			}
			Global.SaveRoleParamsInt32ValueToDB(client, "10175", xiaoFei, true);
			if (!DailyActiveManager.IsDailyActiveCompleted(client, 300))
			{
				if (DailyActiveManager.CheckLevCondition(client, 300))
				{
					uint nSpend = DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveBuyItemInMall);
					client.ClientData.DailyActiveDayBuyItemInMall += (int)(nSpend + (uint)nValue);
					DailyActiveManager.ModifyDailyActiveInfor(client, (uint)client.ClientData.DailyActiveDayBuyItemInMall, DailyActiveDataField1.DailyActiveBuyItemInMall, true);
					bool bIsCompleted = false;
					DailyActiveManager.CheckSingleConditionForDailyActive(client, 300, 300, (long)client.ClientData.DailyActiveDayBuyItemInMall, "Consumption", out bIsCompleted);
				}
			}
		}

		
		public static void ProcessCompleteDailyTaskForDailyActive(GameClient client, int nValue)
		{
			if (!DailyActiveManager.IsDailyActiveCompleted(client, 400) || !DailyActiveManager.IsDailyActiveCompleted(client, 401))
			{
				if (DailyActiveManager.CheckLevCondition(client, 400) || DailyActiveManager.CheckLevCondition(client, 401))
				{
					client.ClientData.DailyCompleteDailyTaskCount = (uint)nValue;
					DailyActiveManager.ModifyDailyActiveInfor(client, client.ClientData.DailyCompleteDailyTaskCount, DailyActiveDataField1.DailyActiveCompleteDailyTask, true);
					bool bIsCompleted = false;
					DailyActiveManager.CheckSingleConditionForDailyActive(client, 400, 401, (long)((ulong)client.ClientData.DailyCompleteDailyTaskCount), "RiChang", out bIsCompleted);
				}
			}
		}

		
		public static void ProcessCompleteCopyMapForDailyActive(GameClient client, int nCopyMapLev, int count = 1)
		{
			if (!DailyActiveManager.IsDailyActiveCompleted(client, 500) || !DailyActiveManager.IsDailyActiveCompleted(client, 600) || !DailyActiveManager.IsDailyActiveCompleted(client, 700))
			{
				if (nCopyMapLev >= 0)
				{
					bool bIsCompleted = false;
					switch (nCopyMapLev)
					{
					case 1:
						if (DailyActiveManager.CheckLevCondition(client, 500))
						{
							int nNum = (int)DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveCompleteCopyMap1);
							nNum++;
							nNum *= count;
							DailyActiveManager.ModifyDailyActiveInfor(client, (uint)nNum, DailyActiveDataField1.DailyActiveCompleteCopyMap1, true);
							DailyActiveManager.CheckSingleConditionForDailyActive(client, 500, 500, (long)nNum, "KillRaid", out bIsCompleted);
						}
						break;
					case 2:
						if (DailyActiveManager.CheckLevCondition(client, 600))
						{
							int nNum = (int)DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveCompleteCopyMap2);
							nNum++;
							nNum *= count;
							DailyActiveManager.ModifyDailyActiveInfor(client, (uint)nNum, DailyActiveDataField1.DailyActiveCompleteCopyMap2, true);
							DailyActiveManager.CheckSingleConditionForDailyActive(client, 600, 600, (long)nNum, "KillRaid", out bIsCompleted);
						}
						break;
					case 3:
						if (DailyActiveManager.CheckLevCondition(client, 700))
						{
							int nNum = (int)DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveCompleteCopyMap3);
							nNum++;
							nNum *= count;
							DailyActiveManager.ModifyDailyActiveInfor(client, (uint)nNum, DailyActiveDataField1.DailyActiveCompleteCopyMap3, true);
							DailyActiveManager.CheckSingleConditionForDailyActive(client, 700, 700, (long)nNum, "KillRaid", out bIsCompleted);
						}
						break;
					}
				}
			}
		}

		
		public static void ProcessCompleteDailyActivityForDailyActive(GameClient client, int nType)
		{
			if (!DailyActiveManager.IsDailyActiveCompleted(client, 800) || !DailyActiveManager.IsDailyActiveCompleted(client, 900) || !DailyActiveManager.IsDailyActiveCompleted(client, 1000))
			{
				if (nType >= 0)
				{
					bool bIsCompleted = false;
					switch (nType)
					{
					case 1:
						if (DailyActiveManager.CheckLevCondition(client, 800))
						{
							int nNum = (int)DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveCompleteBloodCastle);
							nNum++;
							DailyActiveManager.ModifyDailyActiveInfor(client, (uint)nNum, DailyActiveDataField1.DailyActiveCompleteBloodCastle, true);
							DailyActiveManager.CheckSingleConditionForDailyActive(client, 800, 800, (long)nNum, "HuoDongLimit", out bIsCompleted);
						}
						break;
					case 2:
						if (DailyActiveManager.CheckLevCondition(client, 900))
						{
							int nNum = (int)DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveCompleteDaimonSquare);
							nNum++;
							DailyActiveManager.ModifyDailyActiveInfor(client, (uint)nNum, DailyActiveDataField1.DailyActiveCompleteDaimonSquare, true);
							DailyActiveManager.CheckSingleConditionForDailyActive(client, 900, 900, (long)nNum, "HuoDongLimit", out bIsCompleted);
						}
						break;
					case 3:
						if (DailyActiveManager.CheckLevCondition(client, 1000))
						{
							int nNum = (int)DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveCompleteBattle);
							nNum++;
							DailyActiveManager.ModifyDailyActiveInfor(client, (uint)nNum, DailyActiveDataField1.DailyActiveCompleteBattle, true);
							DailyActiveManager.CheckSingleConditionForDailyActive(client, 1000, 1000, (long)nNum, "HuoDongLimit", out bIsCompleted);
						}
						break;
					}
				}
			}
		}

		
		public static void ProcessDailyActiveEquipForge(GameClient client)
		{
			if (!DailyActiveManager.IsDailyActiveCompleted(client, 1100))
			{
				if (DailyActiveManager.CheckLevCondition(client, 1100))
				{
					int nNum = (int)DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveEquipForge);
					nNum++;
					DailyActiveManager.ModifyDailyActiveInfor(client, (uint)nNum, DailyActiveDataField1.DailyActiveEquipForge, true);
					bool bIsCompleted = false;
					DailyActiveManager.CheckSingleConditionForDailyActive(client, 1100, 1100, (long)nNum, "QiangHuaLimit", out bIsCompleted);
				}
			}
		}

		
		public static void ProcessDailyActiveEquipAppend(GameClient client)
		{
			if (!DailyActiveManager.IsDailyActiveCompleted(client, 1200))
			{
				if (DailyActiveManager.CheckLevCondition(client, 1200))
				{
					int nNum = (int)DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveEquipAppend);
					nNum++;
					DailyActiveManager.ModifyDailyActiveInfor(client, (uint)nNum, DailyActiveDataField1.DailyActiveEquipAppend, true);
					bool bIsCompleted = false;
					DailyActiveManager.CheckSingleConditionForDailyActive(client, 1200, 1200, (long)nNum, "ZhuiJiaLimit", out bIsCompleted);
				}
			}
		}

		
		public static void ProcessDailyActiveChangeLife(GameClient client)
		{
			if (!DailyActiveManager.IsDailyActiveCompleted(client, 1500))
			{
				if (DailyActiveManager.CheckLevCondition(client, 1500))
				{
					int nNum = (int)DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveChangeLife);
					nNum++;
					DailyActiveManager.ModifyDailyActiveInfor(client, (uint)nNum, DailyActiveDataField1.DailyActiveChangeLife, true);
					bool bIsCompleted = false;
					DailyActiveManager.CheckSingleConditionForDailyActive(client, 1500, 1500, (long)nNum, "ZhuanShengLimit", out bIsCompleted);
				}
			}
		}

		
		public static void ProcessDailyActiveMergeFruit(GameClient client)
		{
			if (!DailyActiveManager.IsDailyActiveCompleted(client, 1600))
			{
				if (DailyActiveManager.CheckLevCondition(client, 1600))
				{
					int nNum = (int)DailyActiveManager.GetDailyActiveDataByField(client, DailyActiveDataField1.DailyActiveMergeFruit);
					nNum++;
					DailyActiveManager.ModifyDailyActiveInfor(client, (uint)nNum, DailyActiveDataField1.DailyActiveMergeFruit, true);
					bool bIsCompleted = false;
					DailyActiveManager.CheckSingleConditionForDailyActive(client, 1600, 1600, (long)nNum, "HeChengLimit", out bIsCompleted);
				}
			}
		}

		
		public static bool CheckLevCondition(GameClient client, int daTpye)
		{
			SystemXmlItem itemDailyActive = null;
			GameManager.systemDailyActiveInfo.SystemXmlItemDict.TryGetValue(daTpye, out itemDailyActive);
			bool result;
			if (null == itemDailyActive)
			{
				result = false;
			}
			else
			{
				int MinZhuanshengleve = itemDailyActive.GetIntValue("MinZhuanshengleve", -1);
				if (client.ClientData.ChangeLifeCount < MinZhuanshengleve)
				{
					result = false;
				}
				else
				{
					if (client.ClientData.ChangeLifeCount == itemDailyActive.GetIntValue("MinZhuanshengleve", -1))
					{
						int nLev = itemDailyActive.GetIntValue("Minleve", -1);
						if (client.ClientData.Level < nLev)
						{
							return false;
						}
					}
					result = true;
				}
			}
			return result;
		}

		
		public static void CleanDailyActiveInfo(GameClient client)
		{
			List<ulong> lsLong = new List<ulong>();
			Global.SaveRoleParamsUlongListToDB(client, lsLong, "DailyActiveFlag", true);
			List<uint> lsUint = new List<uint>();
			Global.SaveRoleParamsUintListToDB(client, lsUint, "DailyActiveInfo1", true);
			int nToday = TimeUtil.NowDateTime().DayOfYear;
			Global.SaveRoleParamsInt32ValueToDB(client, "DailyActiveDayID", nToday, true);
			Global.SaveRoleParamsInt32ValueToDB(client, "DailyActiveAwardFlag", 0, true);
			client.ClientData.DailyActiveDayID = nToday;
			client.ClientData.DailyActiveValues = 0;
			client.ClientData.DailyTotalKillMonsterNum = 0U;
			client.ClientData.DailyCompleteDailyTaskCount = 0U;
			client.ClientData.DailyNextKillMonsterNum = 0U;
			client.ClientData.DailyActiveDayBuyItemInMall = 0;
			client.ClientData.DailyActiveDayLginCount = 0U;
		}

		
		private static Dictionary<int, int> m_DailyActiveInfo = new Dictionary<int, int>();

		
		public static int m_DailyActiveDayID = 0;
	}
}
