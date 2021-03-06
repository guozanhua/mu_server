﻿using System;
using GameServer.Server;
using Server.Data;
using Server.Protocol;
using Server.Tools;

namespace GameServer.Logic.MUWings
{
	
	public static class MUWingsManager
	{
		
		
		public static int MaxWingID
		{
			get
			{
				return GameManager.SystemWingsUp.MaxKey;
			}
		}

		
		public static void InitFirstWing(GameClient client)
		{
			if (null == client.ClientData.MyWingData)
			{
				WingData wingData = MUWingsManager.AddWingDBCommand(TCPOutPacketPool.getInstance(), client.ClientData.RoleID, 1, client.ServerId);
				client.ClientData.MyWingData = wingData;
			}
		}

		
		public static WingData AddWingDBCommand(TCPOutPacketPool pool, int roleID, int WingID, int serverId)
		{
			TCPOutPacket tcpOutPacket = null;
			string strcmd = string.Format("{0}:{1}", roleID, WingID);
			TCPProcessCmdResults dbRequestResult = Global.RequestToDBServer2(Global._TCPManager.tcpClientPool, pool, 10153, strcmd, out tcpOutPacket, serverId);
			WingData result;
			if (dbRequestResult == TCPProcessCmdResults.RESULT_FAILED)
			{
				result = null;
			}
			else if (null == tcpOutPacket)
			{
				result = null;
			}
			else
			{
				WingData wingData = DataHelper.BytesToObject<WingData>(tcpOutPacket.GetPacketBytes(), 6, tcpOutPacket.PacketDataSize - 6);
				Global.PushBackTcpOutPacket(tcpOutPacket);
				result = wingData;
			}
			return result;
		}

		
		public static int WingOnOffDBCommand(GameClient client, int dbID, int isUsing)
		{
			string strcmd = string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}:{7}:{8}", new object[]
			{
				client.ClientData.RoleID,
				dbID,
				isUsing,
				"*",
				"*",
				"*",
				"*",
				"*",
				"*"
			});
			string[] fields = Global.ExecuteDBCmd(10154, strcmd, client.ServerId);
			int result;
			if (fields == null || fields.Length != 2)
			{
				result = -1;
			}
			else
			{
				result = Convert.ToInt32(fields[1]);
			}
			return result;
		}

		
		public static int WingUpStarDBCommand(GameClient client, int dbID, int nStarLevel, int nStarExp)
		{
			string strcmd = string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}:{7}:{8}", new object[]
			{
				client.ClientData.RoleID,
				dbID,
				"*",
				"*",
				nStarLevel,
				"*",
				nStarExp,
				"*",
				"*"
			});
			string[] fields = Global.ExecuteDBCmd(10154, strcmd, client.ServerId);
			int result;
			if (fields == null || fields.Length != 2)
			{
				result = -1;
			}
			else
			{
				result = Convert.ToInt32(fields[1]);
			}
			return result;
		}

		
		public static int WingUpDBCommand(GameClient client, int dbID, int nWingLevel, int nFailNum, int nStarLevel, int nStarExp, int nZhuLingNum, int nZhuHunNum)
		{
			string strcmd = string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}:{7}:{8}", new object[]
			{
				client.ClientData.RoleID,
				dbID,
				"*",
				nWingLevel,
				nStarLevel,
				nFailNum,
				nStarExp,
				nZhuLingNum,
				nZhuHunNum
			});
			string[] fields = Global.ExecuteDBCmd(10154, strcmd, client.ServerId);
			int result;
			if (fields == null || fields.Length != 2)
			{
				result = -1;
			}
			else
			{
				result = Convert.ToInt32(fields[1]);
			}
			return result;
		}

		
		public static bool UpdateWingDataProps(GameClient client, bool toAdd = true)
		{
			bool result;
			if (null == client.ClientData.MyWingData)
			{
				result = false;
			}
			else if (client.ClientData.MyWingData.WingID <= 0)
			{
				result = false;
			}
			else
			{
				SystemXmlItem baseXmlNode = WingPropsCacheManager.GetWingPropsCacheItem(Global.CalcOriginalOccupationID(client), client.ClientData.MyWingData.WingID);
				if (null == baseXmlNode)
				{
					result = false;
				}
				else
				{
					MUWingsManager.ChangeWingDataProps(client, baseXmlNode, toAdd);
					baseXmlNode = WingStarCacheManager.GetWingStarCacheItem(Global.CalcOriginalOccupationID(client), client.ClientData.MyWingData.WingID, client.ClientData.MyWingData.ForgeLevel);
					if (null == baseXmlNode)
					{
						result = false;
					}
					else
					{
						MUWingsManager.ChangeWingDataProps(client, baseXmlNode, toAdd);
						result = true;
					}
				}
			}
			return result;
		}

		
		public static bool ChangeWingDataProps(GameClient client, SystemXmlItem baseXmlNode, bool toAdd = true)
		{
			bool result;
			if (null == client.ClientData.MyWingData)
			{
				result = false;
			}
			else if (client.ClientData.MyWingData.WingID <= 0)
			{
				result = false;
			}
			else
			{
				double minAttackV = baseXmlNode.GetDoubleValue("MinAttackV");
				if (!toAdd)
				{
					minAttackV = 0.0 - minAttackV;
				}
				double maxAttackV = baseXmlNode.GetDoubleValue("MaxAttackV");
				if (!toAdd)
				{
					maxAttackV = 0.0 - maxAttackV;
				}
				client.ClientData.EquipProp.ExtProps[7] += minAttackV;
				client.ClientData.EquipProp.ExtProps[8] += maxAttackV;
				double minMAttackV = baseXmlNode.GetDoubleValue("MinMAttackV");
				if (!toAdd)
				{
					minMAttackV = 0.0 - minMAttackV;
				}
				double maxMAttackV = baseXmlNode.GetDoubleValue("MaxMAttackV");
				if (!toAdd)
				{
					maxMAttackV = 0.0 - maxMAttackV;
				}
				client.ClientData.EquipProp.ExtProps[9] += minMAttackV;
				client.ClientData.EquipProp.ExtProps[10] += maxMAttackV;
				double minDefenseV = baseXmlNode.GetDoubleValue("MinDefenseV");
				if (!toAdd)
				{
					minDefenseV = 0.0 - minDefenseV;
				}
				double maxDefenseV = baseXmlNode.GetDoubleValue("MaxDefenseV");
				if (!toAdd)
				{
					maxDefenseV = 0.0 - maxDefenseV;
				}
				client.ClientData.EquipProp.ExtProps[3] += minDefenseV;
				client.ClientData.EquipProp.ExtProps[4] += maxDefenseV;
				double minMDefenseV = baseXmlNode.GetDoubleValue("MinMDefenseV");
				if (!toAdd)
				{
					minMDefenseV = 0.0 - minMDefenseV;
				}
				double maxMDefenseV = baseXmlNode.GetDoubleValue("MaxMDefenseV");
				if (!toAdd)
				{
					maxMDefenseV = 0.0 - maxMDefenseV;
				}
				client.ClientData.EquipProp.ExtProps[5] += minMDefenseV;
				client.ClientData.EquipProp.ExtProps[6] += maxMDefenseV;
				double maxLifeV = baseXmlNode.GetDoubleValue("MaxLifeV");
				if (!toAdd)
				{
					maxLifeV = 0.0 - maxLifeV;
				}
				client.ClientData.EquipProp.ExtProps[13] += maxLifeV;
				double subAttackInjurePercent = baseXmlNode.GetDoubleValue("SubAttackInjurePercent");
				if (!toAdd)
				{
					subAttackInjurePercent = 0.0 - subAttackInjurePercent;
				}
				client.ClientData.EquipProp.ExtProps[24] += subAttackInjurePercent;
				double addAttackInjurePercent = baseXmlNode.GetDoubleValue("AddAttackInjurePercent");
				if (!toAdd)
				{
					addAttackInjurePercent = 0.0 - addAttackInjurePercent;
				}
				client.ClientData.EquipProp.ExtProps[26] += addAttackInjurePercent;
				double Dodge = baseXmlNode.GetDoubleValue("Dodge");
				if (!toAdd)
				{
					Dodge = 0.0 - Dodge;
				}
				client.ClientData.EquipProp.ExtProps[19] += Dodge;
				double HitV = baseXmlNode.GetDoubleValue("HitV");
				if (!toAdd)
				{
					HitV = 0.0 - HitV;
				}
				client.ClientData.EquipProp.ExtProps[18] += HitV;
				result = true;
			}
			return result;
		}

		
		public static SystemXmlItem GetWingUPCacheItem(int nLevel)
		{
			SystemXmlItem systemWingPropsCacheItem = null;
			SystemXmlItem result;
			if (!GameManager.SystemWingsUp.SystemXmlItemDict.TryGetValue(nLevel, out systemWingPropsCacheItem))
			{
				result = null;
			}
			else
			{
				result = systemWingPropsCacheItem;
			}
			return result;
		}

		
		public static bool IfWingPerfect(GameClient client)
		{
			return null != client.ClientData.MyWingData && client.ClientData.MyWingData.WingID >= MUWingsManager.MaxWingID && client.ClientData.MyWingData.ForgeLevel >= MUWingsManager.MaxWingEnchanceLevel;
		}

		
		public static int MaxWingEnchanceLevel = 10;
	}
}
