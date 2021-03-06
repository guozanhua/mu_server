﻿using System;
using System.Collections.Generic;
using System.Xml.Linq;
using GameServer.Core.Executor;
using GameServer.Core.GameEvent;
using GameServer.Server;
using Server.Data;
using Server.Tools;

namespace GameServer.Logic
{
	
	internal class MarriageOtherLogic : ICmdProcessorEx, ICmdProcessor, IEventListener
	{
		
		public static MarriageOtherLogic getInstance()
		{
			return MarriageOtherLogic.instance;
		}

		
		public void init()
		{
			try
			{
				this.dNeedGam = GameManager.systemParamsList.GetParamValueDoubleArrayByName("XianHuaCost", ',');
			}
			catch (Exception ex)
			{
				DataHelper.WriteFormatExceptionLog(ex, "init marryotherlogic XianHuaCost", false, false);
			}
			try
			{
				this.dRingmodulus = GameManager.systemParamsList.GetParamValueDoubleByName("GoodWillXiShu", 0.0);
			}
			catch (Exception ex)
			{
				DataHelper.WriteFormatExceptionLog(ex, "init marryotherlogic GoodWillXiShu", false, false);
			}
			try
			{
				this.dOtherRingmodulus = GameManager.systemParamsList.GetParamValueDoubleByName("BanLvXiShu", 0.0);
			}
			catch (Exception ex)
			{
				DataHelper.WriteFormatExceptionLog(ex, "init marryotherlogic BanLvXiShu", false, false);
			}
			try
			{
				this.WeddingRingDic.LoadFromXMlFile("Config/WeddingRing.xml", "", "GoodsID", 0);
			}
			catch (Exception ex)
			{
				DataHelper.WriteFormatExceptionLog(ex, "init marryotherlogic WeddingRing.xml", false, false);
			}
			try
			{
				SystemXmlItems RoseXmlItems = new SystemXmlItems();
				RoseXmlItems.LoadFromXMlFile("Config/GiveRose.xml", "", "GoodsID", 0);
				foreach (KeyValuePair<int, SystemXmlItem> item in RoseXmlItems.SystemXmlItemDict)
				{
					MarriageRoseData rosedata = new MarriageRoseData();
					rosedata.nBaseAddGoodWill = item.Value.GetIntValue("GoodWill", -1);
					string[] strfiled = item.Value.GetStringValue("MultiplyingPower").Split(new char[]
					{
						'|'
					});
					int nAddRate = 0;
					for (int i = 0; i < strfiled.Length; i++)
					{
						string[] strfiled2 = strfiled[i].Split(new char[]
						{
							','
						});
						nAddRate += (int)(Convert.ToDouble(strfiled2[1]) * 100.0);
						rosedata.modulusList.Add(Convert.ToInt32(strfiled2[0]));
						rosedata.rateList.Add(nAddRate);
					}
					this.RoseDataDic.Add(Convert.ToInt32(item.Value.GetIntValue("GoodsID", -1)), rosedata);
				}
			}
			catch (Exception ex)
			{
				DataHelper.WriteFormatExceptionLog(ex, "init marryotherlogic GiveRose.xml", false, false);
			}
			try
			{
				SystemXmlItems XmlItems = new SystemXmlItems();
				XmlItems.LoadFromXMlFile("Config/GoodWill.xml", "", "Type", 0);
				sbyte tmpStar = 0;
				int nAddExp = 0;
				this.GoodwillAllExpList.Add(0);
				foreach (KeyValuePair<int, SystemXmlItem> item in XmlItems.SystemXmlItemDict)
				{
					tmpStar = 0;
					foreach (XElement xmlnode in item.Value.XMLNode.Descendants())
					{
						nAddExp += Convert.ToInt32(xmlnode.Attribute("NeedGoodWill").Value);
						this.GoodwillAllExpList.Add(nAddExp);
						tmpStar += 1;
					}
				}
				this.byMaxGoodwillLv = (sbyte)((this.GoodwillAllExpList.Count - 1) / (int)tmpStar);
				this.byMaxGoodwillStar = tmpStar;
			}
			catch (Exception ex)
			{
				DataHelper.WriteFormatExceptionLog(ex, "init marryotherlogic GoodWill.xml", false, false);
			}
			TCPCmdDispatcher.getInstance().registerProcessorEx(871, 1, 1, MarriageOtherLogic.getInstance(), TCPCmdFlags.IsStringArrayParams);
			TCPCmdDispatcher.getInstance().registerProcessorEx(872, 1, 1, MarriageOtherLogic.getInstance(), TCPCmdFlags.IsStringArrayParams);
			TCPCmdDispatcher.getInstance().registerProcessorEx(873, 1, 1, MarriageOtherLogic.getInstance(), TCPCmdFlags.IsStringArrayParams);
		}

		
		public void destroy()
		{
		}

		
		public bool processCmd(GameClient client, string[] cmdParams)
		{
			return false;
		}

		
		public bool processCmdEx(GameClient client, int nID, byte[] bytes, string[] cmdParams)
		{
			switch (nID)
			{
			case 871:
				if (cmdParams == null || cmdParams.Length != 1)
				{
					return false;
				}
				if (!MarryLogic.IsVersionSystemOpenOfMarriage())
				{
					client.sendCmd<int>(nID, 11, false);
				}
				else
				{
					try
					{
						int nGoodsDBId = Global.SafeConvertToInt32(cmdParams[0]);
						int iRet = (int)this.GiveRose(client, nGoodsDBId);
						client.sendCmd<int>(nID, iRet, false);
					}
					catch (Exception ex)
					{
						DataHelper.WriteFormatExceptionLog(ex, "CMD_SPR_MARRY_ROSE", false, false);
					}
				}
				break;
			case 872:
				if (cmdParams == null || cmdParams.Length != 1)
				{
					return false;
				}
				if (!MarryLogic.IsVersionSystemOpenOfMarriage())
				{
					client.sendCmd<int>(nID, 11, false);
				}
				else
				{
					try
					{
						int nRingId = Global.SafeConvertToInt32(cmdParams[0]);
						int iRet = (int)this.ChangeRing(client, nRingId);
						client.sendCmd<int>(nID, iRet, false);
					}
					catch (Exception ex)
					{
						DataHelper.WriteFormatExceptionLog(ex, "CMD_SPR_MARRY_RING", false, false);
					}
				}
				break;
			case 873:
				if (cmdParams == null || cmdParams.Length != 1)
				{
					return false;
				}
				if (!MarryLogic.IsVersionSystemOpenOfMarriage())
				{
					client.sendCmd<int>(nID, 11, false);
				}
				else
				{
					try
					{
						int iRet = (int)this.ChangeMarriageMessage(client, cmdParams[0]);
						client.sendCmd<int>(nID, iRet, false);
					}
					catch (Exception ex)
					{
						DataHelper.WriteFormatExceptionLog(ex, "CMD_SPR_MARRY_MESSAGE", false, false);
					}
				}
				break;
			}
			return true;
		}

		
		public void processEvent(EventObject eventObject)
		{
		}

		
		public void PlayGameAfterSend(GameClient client)
		{
			this.SendMarriageDataToClient(client, true);
			if (!client.ClientSocket.IsKuaFuLogin)
			{
				GameClient Spouseclient = GameManager.ClientMgr.FindClient(client.ClientData.MyMarriageData.nSpouseID);
				if (null != Spouseclient)
				{
					string SpouseOLTips = string.Format(GLang.GetLang(489, new object[0]), client.ClientData.RoleName);
					GameManager.ClientMgr.NotifyImportantMsg(Spouseclient, SpouseOLTips, GameInfoTypeIndexes.Hot, ShowGameInfoTypes.OnlySysHint, 0);
				}
			}
		}

		
		public MarryOtherResult ChangeMarriageMessage(GameClient client, string strMessage)
		{
			MarryOtherResult result;
			if (-1 == client.ClientData.MyMarriageData.byMarrytype)
			{
				result = MarryOtherResult.NotMarriaged;
			}
			else if (strMessage.Length >= 64)
			{
				result = MarryOtherResult.MessageLimit;
			}
			else
			{
				client.ClientData.MyMarriageData.strLovemessage = strMessage;
				MarryFuBenMgr.UpdateMarriageData2DB(client);
				this.SendMarriageDataToClient(client, true);
				GameClient Spouseclient = GameManager.ClientMgr.FindClient(client.ClientData.MyMarriageData.nSpouseID);
				if (null != Spouseclient)
				{
					Spouseclient.ClientData.MyMarriageData.strLovemessage = strMessage;
					MarryFuBenMgr.UpdateMarriageData2DB(Spouseclient);
					this.SendMarriageDataToClient(Spouseclient, true);
				}
				else
				{
					string tcpstring = string.Format("{0}", client.ClientData.MyMarriageData.nSpouseID);
					MarriageData SpouseMarriageData = Global.sendToDB<MarriageData, string>(10186, tcpstring, client.ServerId);
					if (SpouseMarriageData == null || -1 == SpouseMarriageData.byMarrytype)
					{
						return MarryOtherResult.Error;
					}
					MarryFuBenMgr.UpdateMarriageData2DB(client.ClientData.MyMarriageData.nSpouseID, SpouseMarriageData, client);
				}
				result = MarryOtherResult.Success;
			}
			return result;
		}

		
		public MarryOtherResult ChangeRing(GameClient client, int nRingID)
		{
			MarryOtherResult result;
			if (-1 == client.ClientData.MyMarriageData.byMarrytype)
			{
				result = MarryOtherResult.NotMarriaged;
			}
			else if (nRingID - client.ClientData.MyMarriageData.nRingID != 1)
			{
				result = MarryOtherResult.NotNexRise;
			}
			else
			{
				SystemXmlItem RingXmlItem = null;
				if (!this.WeddingRingDic.SystemXmlItemDict.TryGetValue(nRingID, out RingXmlItem) || null == RingXmlItem)
				{
					result = MarryOtherResult.NotRing;
				}
				else
				{
					SystemXmlItem NowRingXmlItem = null;
					if (!this.WeddingRingDic.SystemXmlItemDict.TryGetValue(client.ClientData.MyMarriageData.nRingID, out NowRingXmlItem) || null == NowRingXmlItem)
					{
						result = MarryOtherResult.NotRing;
					}
					else
					{
						string strCostList = "";
						int oldUserMoney = client.ClientData.UserMoney;
						int oldUserGlod = client.ClientData.Gold;
						int nCostGam = RingXmlItem.GetIntValue("NeedZuanShi", -1);
						int nBeforeCostGam = NowRingXmlItem.GetIntValue("NeedZuanShi", -1);
						int chajia = nCostGam - nBeforeCostGam;
						if (!GameManager.ClientMgr.SubUserMoney(Global._TCPManager.MySocketListener, Global._TCPManager.tcpClientPool, Global._TCPManager.TcpOutPacketPool, client, chajia, "更换婚戒扣除", true, true, false, DaiBiSySType.None))
						{
							result = MarryOtherResult.NeedGam;
						}
						else
						{
							strCostList += EventLogManager.NewResPropString(ResLogType.FristBindZuanShi, new object[]
							{
								-chajia,
								oldUserGlod,
								client.ClientData.Gold,
								oldUserMoney,
								client.ClientData.UserMoney
							});
							client.ClientData.MyMarriageData.nRingID = nRingID;
							EventLogManager.AddRingBuyEvent(client, 1, nRingID, (int)client.ClientData.MyMarriageData.byGoodwilllevel, (int)client.ClientData.MyMarriageData.byGoodwilllevel, (int)client.ClientData.MyMarriageData.byGoodwillstar, (int)client.ClientData.MyMarriageData.byGoodwillstar, strCostList);
							this.UpdateRingAttr(client, true, false);
							MarryFuBenMgr.UpdateMarriageData2DB(client);
							this.SendMarriageDataToClient(client, true);
							result = MarryOtherResult.Success;
						}
					}
				}
			}
			return result;
		}

		
		public void UpdateRingAttr(GameClient client, bool bNeedUpdateSpouse = false, bool bIsLogin = false)
		{
			if (MarryLogic.IsVersionSystemOpenOfMarriage())
			{
				if (-1 != client.ClientData.MyMarriageData.nRingID)
				{
					if (-1 != client.ClientData.MyMarriageData.byMarrytype && -1 != client.ClientData.MyMarriageData.nSpouseID)
					{
						GameClient Spouseclient = GameManager.ClientMgr.FindClient(client.ClientData.MyMarriageData.nSpouseID);
						MarriageData SpouseMarriageData;
						if (null != Spouseclient)
						{
							SpouseMarriageData = Spouseclient.ClientData.MyMarriageData;
						}
						else
						{
							string tcpstring = string.Format("{0}", client.ClientData.MyMarriageData.nSpouseID);
							SpouseMarriageData = Global.sendToDB<MarriageData, string>(10186, tcpstring, client.ServerId);
						}
						if (SpouseMarriageData != null && -1 != SpouseMarriageData.nRingID)
						{
							EquipPropItem myringitem = GameManager.EquipPropsMgr.FindEquipPropItem(client.ClientData.MyMarriageData.nRingID);
							EquipPropItem tmpmyringitem = new EquipPropItem();
							EquipPropItem spouseringitem = GameManager.EquipPropsMgr.FindEquipPropItem(SpouseMarriageData.nRingID);
							EquipPropItem tmpspouseringitem = new EquipPropItem();
							for (int i = 0; i < tmpmyringitem.ExtProps.Length; i++)
							{
								tmpmyringitem.ExtProps[i] = this.RingAttrJiSuan(client.ClientData.MyMarriageData.byGoodwilllevel, client.ClientData.MyMarriageData.byGoodwillstar, myringitem.ExtProps[i]);
								tmpspouseringitem.ExtProps[i] = this.RingAttrJiSuan(SpouseMarriageData.byGoodwilllevel, SpouseMarriageData.byGoodwillstar, spouseringitem.ExtProps[i]);
								tmpmyringitem.ExtProps[i] += tmpspouseringitem.ExtProps[i] * this.dOtherRingmodulus;
							}
							client.ClientData.PropsCacheManager.SetExtProps(new object[]
							{
								PropsSystemTypes.MarriageRing,
								tmpmyringitem.ExtProps
							});
							if (!bIsLogin)
							{
								GameManager.ClientMgr.NotifyUpdateEquipProps(Global._TCPManager.MySocketListener, Global._TCPManager.TcpOutPacketPool, client);
								GameManager.ClientMgr.NotifyOthersLifeChanged(Global._TCPManager.MySocketListener, Global._TCPManager.TcpOutPacketPool, client, true, false, 7);
							}
							if (bNeedUpdateSpouse)
							{
								if (null != Spouseclient)
								{
									this.UpdateRingAttr(Spouseclient, false, false);
								}
							}
						}
					}
				}
			}
		}

		
		private double RingAttrJiSuan(sbyte level, sbyte star, double ExpProp)
		{
			return ExpProp * ((double)(1 + (level - 1) * 2) + (double)star * this.dRingmodulus);
		}

		
		public void ResetRingAttr(GameClient client)
		{
			if (-1 != client.ClientData.MyMarriageData.nRingID)
			{
				EquipPropItem tmpnullprop = new EquipPropItem();
				client.ClientData.PropsCacheManager.SetExtProps(new object[]
				{
					PropsSystemTypes.MarriageRing,
					tmpnullprop.ExtProps
				});
				GameManager.ClientMgr.NotifyUpdateEquipProps(Global._TCPManager.MySocketListener, Global._TCPManager.TcpOutPacketPool, client);
				GameManager.ClientMgr.NotifyOthersLifeChanged(Global._TCPManager.MySocketListener, Global._TCPManager.TcpOutPacketPool, client, true, false, 7);
			}
		}

		
		public int GetMaxGoodwillStar()
		{
			return (int)this.byMaxGoodwillStar;
		}

		
		public MarryOtherResult GiveRose(GameClient client, int nGoodsDBId)
		{
			MarryOtherResult result;
			if (client.ClientData.MyMarriageData.byMarrytype == -1)
			{
				result = MarryOtherResult.NotMarriaged;
			}
			else if (client.ClientData.MyMarriageData.byGoodwilllevel == this.byMaxGoodwillLv && client.ClientData.MyMarriageData.byGoodwillstar == this.byMaxGoodwillStar)
			{
				result = MarryOtherResult.MaxLimit;
			}
			else
			{
				GoodsData goodsData = Global.GetGoodsByID(client, nGoodsDBId);
				if (null == goodsData)
				{
					result = MarryOtherResult.NotFindItem;
				}
				else
				{
					lock (this.RoseDataDic)
					{
						MarriageRoseData rosedata = null;
						if (!this.RoseDataDic.TryGetValue(goodsData.GoodsID, out rosedata))
						{
							return MarryOtherResult.ItemNotRose;
						}
						int ngamcost;
						if (client.ClientData.MyMarriageData.nGivenrose < this.dNeedGam.Length)
						{
							ngamcost = Convert.ToInt32(this.dNeedGam[client.ClientData.MyMarriageData.nGivenrose]);
						}
						else
						{
							ngamcost = Convert.ToInt32(this.dNeedGam[this.dNeedGam.Length - 1]);
						}
						if (ngamcost != 0 && !GameManager.ClientMgr.SubUserMoney(Global._TCPManager.MySocketListener, Global._TCPManager.tcpClientPool, Global._TCPManager.TcpOutPacketPool, client, ngamcost, "结婚献花", true, true, false, DaiBiSySType.None))
						{
							return MarryOtherResult.NeedGam;
						}
						if (!GameManager.ClientMgr.NotifyUseGoods(Global._TCPManager.MySocketListener, Global._TCPManager.tcpClientPool, Global._TCPManager.TcpOutPacketPool, client, goodsData, 1, false, false))
						{
							return MarryOtherResult.NeedRose;
						}
						client.ClientData.MyMarriageData.nGivenrose++;
						int nRank = Global.GetRandomNumber(0, 100);
						int nModulus = 1;
						for (int i = 0; i < rosedata.rateList.Count; i++)
						{
							if (nRank < rosedata.rateList[i])
							{
								nModulus = rosedata.modulusList[i];
								break;
							}
						}
						this.UpdateMarriageGoodWill(client, rosedata.nBaseAddGoodWill * nModulus);
						if (nModulus != 1)
						{
							return MarryOtherResult.CirEffect;
						}
					}
					result = MarryOtherResult.Success;
				}
			}
			return result;
		}

		
		public bool CanAddMarriageGoodWill(GameClient client)
		{
			bool result;
			if (!MarryLogic.IsVersionSystemOpenOfMarriage())
			{
				result = false;
			}
			else if (client.ClientData.MyMarriageData.byMarrytype == -1)
			{
				result = false;
			}
			else
			{
				sbyte tmpGoodwilllv = client.ClientData.MyMarriageData.byGoodwilllevel;
				sbyte tmpGoodwillstar = client.ClientData.MyMarriageData.byGoodwillstar;
				result = (tmpGoodwilllv != this.byMaxGoodwillLv || tmpGoodwillstar != this.byMaxGoodwillStar);
			}
			return result;
		}

		
		public void UpdateMarriageGoodWill(GameClient client, int addGoodwillValue)
		{
			if (MarryLogic.IsVersionSystemOpenOfMarriage())
			{
				if (client.ClientData.MyMarriageData.byMarrytype != -1)
				{
					if (addGoodwillValue != 0)
					{
						sbyte tmpGoodwilllv = client.ClientData.MyMarriageData.byGoodwilllevel;
						sbyte tmpGoodwillstar = client.ClientData.MyMarriageData.byGoodwillstar;
						if (tmpGoodwilllv != this.byMaxGoodwillLv || tmpGoodwillstar != this.byMaxGoodwillStar)
						{
							int oldLevel = (int)client.ClientData.MyMarriageData.byGoodwilllevel;
							int oldStart = (int)client.ClientData.MyMarriageData.byGoodwillstar;
							client.ClientData.MyMarriageData.nGoodwillexp += addGoodwillValue;
							int nNowLvAddExp = this.GoodwillAllExpList[(int)((tmpGoodwilllv - 1) * this.byMaxGoodwillStar + tmpGoodwillstar)];
							client.ClientData.MyMarriageData.nGoodwillexp += nNowLvAddExp;
							bool bUpdateLv = false;
							bool bUpdateStar = false;
							for (int i = 1; i < this.GoodwillAllExpList.Count; i++)
							{
								if (i == this.GoodwillAllExpList.Count - 1 && client.ClientData.MyMarriageData.nGoodwillexp >= this.GoodwillAllExpList[i])
								{
									client.ClientData.MyMarriageData.byGoodwilllevel = this.byMaxGoodwillLv;
									client.ClientData.MyMarriageData.byGoodwillstar = this.byMaxGoodwillStar;
									bUpdateStar = true;
									client.ClientData.MyMarriageData.nGoodwillexp = this.GoodwillAllExpList[i] - this.GoodwillAllExpList[i - 1];
								}
								else if (client.ClientData.MyMarriageData.nGoodwillexp < this.GoodwillAllExpList[i])
								{
									int nLv;
									int nStar;
									if (i <= (int)(this.byMaxGoodwillStar + 1))
									{
										nLv = 1;
										nStar = i - 1;
									}
									else
									{
										nLv = (i - 2) / (int)this.byMaxGoodwillStar + 1;
										nStar = (i - 1) % (int)this.byMaxGoodwillStar;
										if (nStar == 0)
										{
											nStar = 10;
										}
									}
									if (nLv != (int)tmpGoodwilllv)
									{
										bUpdateLv = true;
									}
									if (nStar != (int)tmpGoodwillstar)
									{
										bUpdateStar = true;
									}
									client.ClientData.MyMarriageData.byGoodwilllevel = (sbyte)nLv;
									client.ClientData.MyMarriageData.byGoodwillstar = (sbyte)nStar;
									client.ClientData.MyMarriageData.nGoodwillexp -= this.GoodwillAllExpList[i - 1];
									break;
								}
							}
							if (bUpdateLv || bUpdateStar)
							{
								client.ClientData.MyMarriageData.ChangTime = TimeUtil.NowDateTime().ToString("yyyy-MM-dd HH:mm:ss");
							}
							MarryFuBenMgr.UpdateMarriageData2DB(client);
							if (bUpdateLv || bUpdateStar)
							{
								this.UpdateRingAttr(client, true, false);
							}
							this.SendMarriageDataToClient(client, bUpdateLv || bUpdateStar);
							if (bUpdateLv)
							{
								if (client._IconStateMgr.CheckJieRiFanLi(client, ActivityTypes.JieriMarriage) || client._IconStateMgr.CheckSpecialActivity(client) || client._IconStateMgr.CheckEverydayActivity(client))
								{
									client._IconStateMgr.AddFlushIconState(14000, client._IconStateMgr.IsAnyJieRiTipActived());
									client._IconStateMgr.SendIconStateToClient(client);
								}
							}
							if (addGoodwillValue > 0)
							{
								string strHint = StringUtil.substitute(GLang.GetLang(490, new object[0]), new object[]
								{
									addGoodwillValue
								});
								GameManager.ClientMgr.NotifyImportantMsg(client, strHint, GameInfoTypeIndexes.Normal, ShowGameInfoTypes.PiaoFuZi, 0);
							}
							EventLogManager.AddRingStarSuitEvent(client, client.ClientData.MyMarriageData.nRingID, oldLevel, (int)client.ClientData.MyMarriageData.byGoodwilllevel, oldStart, (int)client.ClientData.MyMarriageData.byGoodwillstar, "");
						}
					}
				}
			}
		}

		
		public void ChangeDayUpdate(GameClient client, bool bIsFirstLogin = true)
		{
			if (bIsFirstLogin && client.ClientData.MyMarriageData.nGivenrose != 0)
			{
				client.ClientData.MyMarriageData.nGivenrose = 0;
				MarryFuBenMgr.UpdateMarriageData2DB(client);
				this.SendMarriageDataToClient(client, false);
			}
		}

		
		public void SendMarriageDataToClient(GameClient client, bool bSendSpouseData = true)
		{
			if (null != client.ClientData.MyMarriageData)
			{
				client.sendCmd<MarriageData>(895, client.ClientData.MyMarriageData, false);
				if (bSendSpouseData)
				{
					this.SendSpouseDataToClient(client);
				}
			}
		}

		
		public void SendSpouseDataToClient(GameClient client)
		{
			try
			{
				if (-1 != client.ClientData.MyMarriageData.nSpouseID)
				{
					MarriageData_EX myMarriageData_EX = new MarriageData_EX();
					GameClient Spouseclient = GameManager.ClientMgr.FindClient(client.ClientData.MyMarriageData.nSpouseID);
					if (null != Spouseclient)
					{
						myMarriageData_EX.myMarriageData = Spouseclient.ClientData.MyMarriageData;
						myMarriageData_EX.roleName = Spouseclient.ClientData.RoleName;
						myMarriageData_EX.Occupation = Spouseclient.ClientData.OccupationList[0];
						client.sendCmd<MarriageData_EX>(896, myMarriageData_EX, false);
					}
					else
					{
						RoleDataEx roleDataEx = MarryLogic.GetOfflineRoleData(client.ClientData.MyMarriageData.nSpouseID);
						if (roleDataEx != null)
						{
							myMarriageData_EX.roleName = roleDataEx.RoleName;
							myMarriageData_EX.Occupation = roleDataEx.OccupationList[0];
							myMarriageData_EX.myMarriageData = roleDataEx.MyMarriageData;
							client.sendCmd<MarriageData_EX>(896, myMarriageData_EX, false);
						}
					}
				}
			}
			catch (Exception ex)
			{
				DataHelper.WriteFormatExceptionLog(ex, "SendSpouseDataToClient", false, false);
			}
		}

		
		private static MarriageOtherLogic instance = new MarriageOtherLogic();

		
		private Dictionary<int, MarriageRoseData> RoseDataDic = new Dictionary<int, MarriageRoseData>();

		
		private Dictionary<sbyte, Dictionary<sbyte, int>> GoodwillLvDic = new Dictionary<sbyte, Dictionary<sbyte, int>>();

		
		private List<int> GoodwillAllExpList = new List<int>();

		
		public SystemXmlItems WeddingRingDic = new SystemXmlItems();

		
		private sbyte byMaxGoodwillStar = 0;

		
		private sbyte byMaxGoodwillLv = 0;

		
		private double[] dNeedGam;

		
		private double dRingmodulus = 0.0;

		
		private double dOtherRingmodulus = 0.0;
	}
}
