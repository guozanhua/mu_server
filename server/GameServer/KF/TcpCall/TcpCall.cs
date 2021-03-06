﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AutoCSer.Net;
using AutoCSer.Net.TcpInternalServer;
using AutoCSer.Net.TcpServer;
using KF.Contract;
using KF.Contract.Data;
using KF.Remoting;
using KF.Remoting.HuanYingSiYuan.TcpStaticClient;
using Server.Data;
using Tmsk.Contract.KuaFuData;

namespace KF.TcpCall
{
	
	public static class TcpCall
	{
		
		public class EscapeBattle_K
		{
			
			public static ReturnValue<int> GameResult(int gameId, List<int> zhanDuiScoreList)
			{
				AutoWaitReturnValue<KfCall._p6> _wait_ = AutoWaitReturnValue<KfCall._p6>.Pop();
				try
				{
					AutoCSer.Net.TcpInternalServer.ClientSocketSender _socket_ = KfCall.TcpClient.Sender;
					if (_socket_ != null)
					{
						KfCall._p5 _inputParameter_ = new KfCall._p5
						{
							p1 = gameId,
							p0 = zhanDuiScoreList
						};
						KfCall._p6 _outputParameter_ = default(KfCall._p6);
						ReturnType _returnType_ = _socket_.WaitGet<KfCall._p5, KfCall._p6>(TcpCall.EscapeBattle_K._c3, ref _wait_, ref _inputParameter_, ref _outputParameter_);
						return new ReturnValue<int>
						{
							Type = _returnType_,
							Value = _outputParameter_.Return
						};
					}
				}
				finally
				{
					if (_wait_ != null)
					{
						AutoWaitReturnValue<KfCall._p6>.PushNotNull(_wait_);
					}
				}
				return new ReturnValue<int>
				{
					Type = ReturnType.ClientException
				};
			}

			
			public static ReturnValue<int> GameState(int gameId, int state)
			{
				AutoWaitReturnValue<KfCall._p6> _wait_ = AutoWaitReturnValue<KfCall._p6>.Pop();
				try
				{
					AutoCSer.Net.TcpInternalServer.ClientSocketSender _socket_ = KfCall.TcpClient.Sender;
					if (_socket_ != null)
					{
						KfCall._p27 _inputParameter_ = new KfCall._p27
						{
							p0 = gameId,
							p1 = state
						};
						KfCall._p6 _outputParameter_ = default(KfCall._p6);
						ReturnType _returnType_ = _socket_.WaitGet<KfCall._p27, KfCall._p6>(TcpCall.EscapeBattle_K._c21, ref _wait_, ref _inputParameter_, ref _outputParameter_);
						return new ReturnValue<int>
						{
							Type = _returnType_,
							Value = _outputParameter_.Return
						};
					}
				}
				finally
				{
					if (_wait_ != null)
					{
						AutoWaitReturnValue<KfCall._p6>.PushNotNull(_wait_);
					}
				}
				return new ReturnValue<int>
				{
					Type = ReturnType.ClientException
				};
			}

			
			public static ReturnValue<int> GetZhanDuiState(int zhanDuiID)
			{
				AutoWaitReturnValue<KfCall._p6> _wait_ = AutoWaitReturnValue<KfCall._p6>.Pop();
				try
				{
					AutoCSer.Net.TcpInternalServer.ClientSocketSender _socket_ = KfCall.TcpClient.Sender;
					if (_socket_ != null)
					{
						KfCall._p12 _inputParameter_ = new KfCall._p12
						{
							p0 = zhanDuiID
						};
						KfCall._p6 _outputParameter_ = default(KfCall._p6);
						ReturnType _returnType_ = _socket_.WaitGet<KfCall._p12, KfCall._p6>(TcpCall.EscapeBattle_K._c22, ref _wait_, ref _inputParameter_, ref _outputParameter_);
						return new ReturnValue<int>
						{
							Type = _returnType_,
							Value = _outputParameter_.Return
						};
					}
				}
				finally
				{
					if (_wait_ != null)
					{
						AutoWaitReturnValue<KfCall._p6>.PushNotNull(_wait_);
					}
				}
				return new ReturnValue<int>
				{
					Type = ReturnType.ClientException
				};
			}

			
			public static ReturnValue<EscapeBattleSyncData> SyncZhengBaData(EscapeBattleSyncData lastSyncData)
			{
				AutoWaitReturnValue<KfCall._p2> _wait_ = AutoWaitReturnValue<KfCall._p2>.Pop();
				try
				{
					AutoCSer.Net.TcpInternalServer.ClientSocketSender _socket_ = KfCall.TcpClient.Sender;
					if (_socket_ != null)
					{
						KfCall._p1 _inputParameter_ = new KfCall._p1
						{
							p0 = lastSyncData
						};
						KfCall._p2 _outputParameter_ = default(KfCall._p2);
						ReturnType _returnType_ = _socket_.WaitGet<KfCall._p1, KfCall._p2>(TcpCall.EscapeBattle_K._c1, ref _wait_, ref _inputParameter_, ref _outputParameter_);
						return new ReturnValue<EscapeBattleSyncData>
						{
							Type = _returnType_,
							Value = _outputParameter_.Return
						};
					}
				}
				finally
				{
					if (_wait_ != null)
					{
						AutoWaitReturnValue<KfCall._p2>.PushNotNull(_wait_);
					}
				}
				return new ReturnValue<EscapeBattleSyncData>
				{
					Type = ReturnType.ClientException
				};
			}

			
			public static ReturnValue<int> ZhanDuiJoin(int zhanDuiID, int jiFen, int readyNum)
			{
				AutoWaitReturnValue<KfCall._p6> _wait_ = AutoWaitReturnValue<KfCall._p6>.Pop();
				try
				{
					AutoCSer.Net.TcpInternalServer.ClientSocketSender _socket_ = KfCall.TcpClient.Sender;
					if (_socket_ != null)
					{
						KfCall._p29 _inputParameter_ = new KfCall._p29
						{
							p0 = zhanDuiID,
							p1 = jiFen,
							p2 = readyNum
						};
						KfCall._p6 _outputParameter_ = default(KfCall._p6);
						ReturnType _returnType_ = _socket_.WaitGet<KfCall._p29, KfCall._p6>(TcpCall.EscapeBattle_K._c20, ref _wait_, ref _inputParameter_, ref _outputParameter_);
						return new ReturnValue<int>
						{
							Type = _returnType_,
							Value = _outputParameter_.Return
						};
					}
				}
				finally
				{
					if (_wait_ != null)
					{
						AutoWaitReturnValue<KfCall._p6>.PushNotNull(_wait_);
					}
				}
				return new ReturnValue<int>
				{
					Type = ReturnType.ClientException
				};
			}

			
			public static ReturnValue<int> ZhengBaKuaFuLogin(int zhanDuiID, int gameId, int srcServerID, out EscapeBattleFuBenData copyData)
			{
				AutoWaitReturnValue<KfCall._p4> _wait_ = AutoWaitReturnValue<KfCall._p4>.Pop();
				try
				{
					AutoCSer.Net.TcpInternalServer.ClientSocketSender _socket_ = KfCall.TcpClient.Sender;
					if (_socket_ != null)
					{
						KfCall._p3 _inputParameter_ = new KfCall._p3
						{
							p1 = zhanDuiID,
							p2 = gameId,
							p3 = srcServerID
						};
						KfCall._p4 _outputParameter_ = default(KfCall._p4);
						ReturnType _returnType_ = _socket_.WaitGet<KfCall._p3, KfCall._p4>(TcpCall.EscapeBattle_K._c2, ref _wait_, ref _inputParameter_, ref _outputParameter_);
						copyData = _outputParameter_.p0;
						return new ReturnValue<int>
						{
							Type = _returnType_,
							Value = _outputParameter_.Return
						};
					}
				}
				finally
				{
					if (_wait_ != null)
					{
						AutoWaitReturnValue<KfCall._p4>.PushNotNull(_wait_);
					}
				}
				copyData = null;
				return new ReturnValue<int>
				{
					Type = ReturnType.ClientException
				};
			}

			
			public static ReturnValue<int> ZhengBaRequestEnter(int zhanDuiID, out int gameId, out int kuaFuServerID, out string[] ips, out int[] ports)
			{
				AutoWaitReturnValue<KfCall._p8> _wait_ = AutoWaitReturnValue<KfCall._p8>.Pop();
				try
				{
					AutoCSer.Net.TcpInternalServer.ClientSocketSender _socket_ = KfCall.TcpClient.Sender;
					if (_socket_ != null)
					{
						KfCall._p7 _inputParameter_ = new KfCall._p7
						{
							p0 = zhanDuiID
						};
						KfCall._p8 _outputParameter_ = default(KfCall._p8);
						ReturnType _returnType_ = _socket_.WaitGet<KfCall._p7, KfCall._p8>(TcpCall.EscapeBattle_K._c4, ref _wait_, ref _inputParameter_, ref _outputParameter_);
						gameId = _outputParameter_.p0;
						kuaFuServerID = _outputParameter_.p1;
						ips = _outputParameter_.p3;
						ports = _outputParameter_.p2;
						return new ReturnValue<int>
						{
							Type = _returnType_,
							Value = _outputParameter_.Return
						};
					}
				}
				finally
				{
					if (_wait_ != null)
					{
						AutoWaitReturnValue<KfCall._p8>.PushNotNull(_wait_);
					}
				}
				gameId = 0;
				kuaFuServerID = 0;
				ips = null;
				ports = null;
				return new ReturnValue<int>
				{
					Type = ReturnType.ClientException
				};
			}

			
			private static readonly CommandInfo _c3 = new CommandInfo
			{
				Command = 130,
				InputParameterIndex = 5,
				TaskType = ClientTaskType.Synchronous,
				IsSimpleSerializeOutputParamter = true
			};

			
			private static readonly CommandInfo _c21 = new CommandInfo
			{
				Command = 148,
				InputParameterIndex = 27,
				TaskType = ClientTaskType.Synchronous,
				IsSimpleSerializeInputParamter = true,
				IsSimpleSerializeOutputParamter = true
			};

			
			private static readonly CommandInfo _c22 = new CommandInfo
			{
				Command = 149,
				InputParameterIndex = 12,
				TaskType = ClientTaskType.Synchronous,
				IsSimpleSerializeInputParamter = true,
				IsSimpleSerializeOutputParamter = true
			};

			
			private static readonly CommandInfo _c1 = new CommandInfo
			{
				Command = 128,
				InputParameterIndex = 1,
				TaskType = ClientTaskType.Synchronous
			};

			
			private static readonly CommandInfo _c20 = new CommandInfo
			{
				Command = 147,
				InputParameterIndex = 29,
				TaskType = ClientTaskType.Synchronous,
				IsSimpleSerializeInputParamter = true,
				IsSimpleSerializeOutputParamter = true
			};

			
			private static readonly CommandInfo _c2 = new CommandInfo
			{
				Command = 129,
				InputParameterIndex = 3,
				TaskType = ClientTaskType.Synchronous
			};

			
			private static readonly CommandInfo _c4 = new CommandInfo
			{
				Command = 131,
				InputParameterIndex = 7,
				TaskType = ClientTaskType.Synchronous
			};
		}

		
		public class KFBoCaiManager
		{
			
			public static ReturnValue<bool> BoCaiBuyItem(KFBoCaiShopDB Item, int maxNum)
			{
				AutoWaitReturnValue<KfCall._p10> _wait_ = AutoWaitReturnValue<KfCall._p10>.Pop();
				try
				{
					AutoCSer.Net.TcpInternalServer.ClientSocketSender _socket_ = KfCall.TcpClient.Sender;
					if (_socket_ != null)
					{
						KfCall._p9 _inputParameter_ = new KfCall._p9
						{
							p0 = Item,
							p1 = maxNum
						};
						KfCall._p10 _outputParameter_ = default(KfCall._p10);
						ReturnType _returnType_ = _socket_.WaitGet<KfCall._p9, KfCall._p10>(TcpCall.KFBoCaiManager._c5, ref _wait_, ref _inputParameter_, ref _outputParameter_);
						return new ReturnValue<bool>
						{
							Type = _returnType_,
							Value = _outputParameter_.Return
						};
					}
				}
				finally
				{
					if (_wait_ != null)
					{
						AutoWaitReturnValue<KfCall._p10>.PushNotNull(_wait_);
					}
				}
				return new ReturnValue<bool>
				{
					Type = ReturnType.ClientException
				};
			}

			
			public static ReturnValue<bool> BuyBoCai(KFBuyBocaiData data)
			{
				AutoWaitReturnValue<KfCall._p10> _wait_ = AutoWaitReturnValue<KfCall._p10>.Pop();
				try
				{
					AutoCSer.Net.TcpInternalServer.ClientSocketSender _socket_ = KfCall.TcpClient.Sender;
					if (_socket_ != null)
					{
						KfCall._p11 _inputParameter_ = new KfCall._p11
						{
							p0 = data
						};
						KfCall._p10 _outputParameter_ = default(KfCall._p10);
						ReturnType _returnType_ = _socket_.WaitGet<KfCall._p11, KfCall._p10>(TcpCall.KFBoCaiManager._c6, ref _wait_, ref _inputParameter_, ref _outputParameter_);
						return new ReturnValue<bool>
						{
							Type = _returnType_,
							Value = _outputParameter_.Return
						};
					}
				}
				finally
				{
					if (_wait_ != null)
					{
						AutoWaitReturnValue<KfCall._p10>.PushNotNull(_wait_);
					}
				}
				return new ReturnValue<bool>
				{
					Type = ReturnType.ClientException
				};
			}

			
			public static ReturnValue<KFStageData> GetKFStageData(int type)
			{
				AutoWaitReturnValue<KfCall._p13> _wait_ = AutoWaitReturnValue<KfCall._p13>.Pop();
				try
				{
					AutoCSer.Net.TcpInternalServer.ClientSocketSender _socket_ = KfCall.TcpClient.Sender;
					if (_socket_ != null)
					{
						KfCall._p12 _inputParameter_ = new KfCall._p12
						{
							p0 = type
						};
						KfCall._p13 _outputParameter_ = default(KfCall._p13);
						ReturnType _returnType_ = _socket_.WaitGet<KfCall._p12, KfCall._p13>(TcpCall.KFBoCaiManager._c7, ref _wait_, ref _inputParameter_, ref _outputParameter_);
						return new ReturnValue<KFStageData>
						{
							Type = _returnType_,
							Value = _outputParameter_.Return
						};
					}
				}
				finally
				{
					if (_wait_ != null)
					{
						AutoWaitReturnValue<KfCall._p13>.PushNotNull(_wait_);
					}
				}
				return new ReturnValue<KFStageData>
				{
					Type = ReturnType.ClientException
				};
			}

			
			public static ReturnValue<OpenLottery> GetOpenLottery(int type)
			{
				AutoWaitReturnValue<KfCall._p14> _wait_ = AutoWaitReturnValue<KfCall._p14>.Pop();
				try
				{
					AutoCSer.Net.TcpInternalServer.ClientSocketSender _socket_ = KfCall.TcpClient.Sender;
					if (_socket_ != null)
					{
						KfCall._p12 _inputParameter_ = new KfCall._p12
						{
							p0 = type
						};
						KfCall._p14 _outputParameter_ = default(KfCall._p14);
						ReturnType _returnType_ = _socket_.WaitGet<KfCall._p12, KfCall._p14>(TcpCall.KFBoCaiManager._c8, ref _wait_, ref _inputParameter_, ref _outputParameter_);
						return new ReturnValue<OpenLottery>
						{
							Type = _returnType_,
							Value = _outputParameter_.Return
						};
					}
				}
				finally
				{
					if (_wait_ != null)
					{
						AutoWaitReturnValue<KfCall._p14>.PushNotNull(_wait_);
					}
				}
				return new ReturnValue<OpenLottery>
				{
					Type = ReturnType.ClientException
				};
			}

			
			public static ReturnValue<List<OpenLottery>> GetOpenLottery(int type, long DataPeriods, bool getOne)
			{
				AutoWaitReturnValue<KfCall._p16> _wait_ = AutoWaitReturnValue<KfCall._p16>.Pop();
				try
				{
					AutoCSer.Net.TcpInternalServer.ClientSocketSender _socket_ = KfCall.TcpClient.Sender;
					if (_socket_ != null)
					{
						KfCall._p15 _inputParameter_ = new KfCall._p15
						{
							p1 = type,
							p2 = DataPeriods,
							p0 = getOne
						};
						KfCall._p16 _outputParameter_ = default(KfCall._p16);
						ReturnType _returnType_ = _socket_.WaitGet<KfCall._p15, KfCall._p16>(TcpCall.KFBoCaiManager._c9, ref _wait_, ref _inputParameter_, ref _outputParameter_);
						return new ReturnValue<List<OpenLottery>>
						{
							Type = _returnType_,
							Value = _outputParameter_.Return
						};
					}
				}
				finally
				{
					if (_wait_ != null)
					{
						AutoWaitReturnValue<KfCall._p16>.PushNotNull(_wait_);
					}
				}
				return new ReturnValue<List<OpenLottery>>
				{
					Type = ReturnType.ClientException
				};
			}

			
			public static ReturnValue<List<KFBoCaoHistoryData>> GetWinHistory(int type)
			{
				AutoWaitReturnValue<KfCall._p17> _wait_ = AutoWaitReturnValue<KfCall._p17>.Pop();
				try
				{
					AutoCSer.Net.TcpInternalServer.ClientSocketSender _socket_ = KfCall.TcpClient.Sender;
					if (_socket_ != null)
					{
						KfCall._p12 _inputParameter_ = new KfCall._p12
						{
							p0 = type
						};
						KfCall._p17 _outputParameter_ = default(KfCall._p17);
						ReturnType _returnType_ = _socket_.WaitGet<KfCall._p12, KfCall._p17>(TcpCall.KFBoCaiManager._c10, ref _wait_, ref _inputParameter_, ref _outputParameter_);
						return new ReturnValue<List<KFBoCaoHistoryData>>
						{
							Type = _returnType_,
							Value = _outputParameter_.Return
						};
					}
				}
				finally
				{
					if (_wait_ != null)
					{
						AutoWaitReturnValue<KfCall._p17>.PushNotNull(_wait_);
					}
				}
				return new ReturnValue<List<KFBoCaoHistoryData>>
				{
					Type = ReturnType.ClientException
				};
			}

			
			public static ReturnValue<bool> IsCanBuy(int type, string buyValue, int buyNum, long DataPeriods)
			{
				AutoWaitReturnValue<KfCall._p10> _wait_ = AutoWaitReturnValue<KfCall._p10>.Pop();
				try
				{
					AutoCSer.Net.TcpInternalServer.ClientSocketSender _socket_ = KfCall.TcpClient.Sender;
					if (_socket_ != null)
					{
						KfCall._p18 _inputParameter_ = new KfCall._p18
						{
							p0 = type,
							p3 = buyValue,
							p1 = buyNum,
							p2 = DataPeriods
						};
						KfCall._p10 _outputParameter_ = default(KfCall._p10);
						ReturnType _returnType_ = _socket_.WaitGet<KfCall._p18, KfCall._p10>(TcpCall.KFBoCaiManager._c11, ref _wait_, ref _inputParameter_, ref _outputParameter_);
						return new ReturnValue<bool>
						{
							Type = _returnType_,
							Value = _outputParameter_.Return
						};
					}
				}
				finally
				{
					if (_wait_ != null)
					{
						AutoWaitReturnValue<KfCall._p10>.PushNotNull(_wait_);
					}
				}
				return new ReturnValue<bool>
				{
					Type = ReturnType.ClientException
				};
			}

			
			private static readonly CommandInfo _c5 = new CommandInfo
			{
				Command = 132,
				InputParameterIndex = 9,
				TaskType = ClientTaskType.Synchronous,
				IsSimpleSerializeOutputParamter = true
			};

			
			private static readonly CommandInfo _c6 = new CommandInfo
			{
				Command = 133,
				InputParameterIndex = 11,
				TaskType = ClientTaskType.Synchronous,
				IsSimpleSerializeOutputParamter = true
			};

			
			private static readonly CommandInfo _c7 = new CommandInfo
			{
				Command = 134,
				InputParameterIndex = 12,
				TaskType = ClientTaskType.Synchronous,
				IsSimpleSerializeInputParamter = true
			};

			
			private static readonly CommandInfo _c8 = new CommandInfo
			{
				Command = 135,
				InputParameterIndex = 12,
				TaskType = ClientTaskType.Synchronous,
				IsSimpleSerializeInputParamter = true
			};

			
			private static readonly CommandInfo _c9 = new CommandInfo
			{
				Command = 136,
				InputParameterIndex = 15,
				TaskType = ClientTaskType.Synchronous,
				IsSimpleSerializeInputParamter = true
			};

			
			private static readonly CommandInfo _c10 = new CommandInfo
			{
				Command = 137,
				InputParameterIndex = 12,
				TaskType = ClientTaskType.Synchronous,
				IsSimpleSerializeInputParamter = true
			};

			
			private static readonly CommandInfo _c11 = new CommandInfo
			{
				Command = 138,
				InputParameterIndex = 18,
				TaskType = ClientTaskType.Synchronous,
				IsSimpleSerializeInputParamter = true,
				IsSimpleSerializeOutputParamter = true
			};
		}

		
		public class KFServiceBase
		{
			
			public static ReturnValue<int> ExecuteCommand(string[] args)
			{
				AutoWaitReturnValue<KfCall._p6> _wait_ = AutoWaitReturnValue<KfCall._p6>.Pop();
				try
				{
					AutoCSer.Net.TcpInternalServer.ClientSocketSender _socket_ = KfCall.TcpClient.Sender;
					if (_socket_ != null)
					{
						KfCall._p19 _inputParameter_ = new KfCall._p19
						{
							p0 = args
						};
						KfCall._p6 _outputParameter_ = default(KfCall._p6);
						ReturnType _returnType_ = _socket_.WaitGet<KfCall._p19, KfCall._p6>(TcpCall.KFServiceBase._c12, ref _wait_, ref _inputParameter_, ref _outputParameter_);
						return new ReturnValue<int>
						{
							Type = _returnType_,
							Value = _outputParameter_.Return
						};
					}
				}
				finally
				{
					if (_wait_ != null)
					{
						AutoWaitReturnValue<KfCall._p6>.PushNotNull(_wait_);
					}
				}
				return new ReturnValue<int>
				{
					Type = ReturnType.ClientException
				};
			}

			
			public static ReturnValue<bool> InitializeClient(AutoCSer.Net.TcpInternalServer.ClientSocketSender _sender_, KuaFuClientContext clientInfo)
			{
				AutoWaitReturnValue<KfCall._p10> _wait_ = AutoWaitReturnValue<KfCall._p10>.Pop();
				try
				{
					if (_sender_ != null)
					{
						KfCall._p20 _inputParameter_ = new KfCall._p20
						{
							p0 = clientInfo
						};
						KfCall._p10 _outputParameter_ = default(KfCall._p10);
						ReturnType _returnType_ = _sender_.WaitGet<KfCall._p20, KfCall._p10>(TcpCall.KFServiceBase._c13, ref _wait_, ref _inputParameter_, ref _outputParameter_);
						return new ReturnValue<bool>
						{
							Type = _returnType_,
							Value = _outputParameter_.Return
						};
					}
				}
				finally
				{
					if (_wait_ != null)
					{
						AutoWaitReturnValue<KfCall._p10>.PushNotNull(_wait_);
					}
				}
				return new ReturnValue<bool>
				{
					Type = ReturnType.ClientException
				};
			}

			
			public static KeepCallback KeepGetMessage(Action<ReturnValue<KFCallMsg>> _onReturn_)
			{
				Callback<ReturnValue<KfCall._p21>> _onOutput_ = KfCall.TcpClient.GetCallback<KFCallMsg, KfCall._p21>(_onReturn_);
				try
				{
					AutoCSer.Net.TcpInternalServer.ClientSocketSender _socket_ = KfCall.TcpClient.Sender;
					if (_socket_ != null)
					{
						return _socket_.GetKeep<KfCall._p21>(TcpCall.KFServiceBase._ac14, ref _onOutput_);
					}
				}
				finally
				{
					if (_onOutput_ != null)
					{
						ReturnValue<KfCall._p21> _outputParameter_ = new ReturnValue<KfCall._p21>
						{
							Type = ReturnType.ClientException
						};
						_onOutput_.Call(ref _outputParameter_);
					}
				}
				return null;
			}

			
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static void UpdateKuaFuMapClientCount(int serverId, Dictionary<int, int> mapClientCountDict)
			{
				KfCall._p22 _inputParameter_ = new KfCall._p22
				{
					p1 = serverId,
					p0 = mapClientCountDict
				};
				KfCall.TcpClient.Sender.CallOnly<KfCall._p22>(TcpCall.KFServiceBase._c15, ref _inputParameter_);
			}

			
			private static readonly CommandInfo _c12 = new CommandInfo
			{
				Command = 139,
				InputParameterIndex = 19,
				TaskType = ClientTaskType.Synchronous,
				IsSimpleSerializeOutputParamter = true
			};

			
			private static readonly CommandInfo _c13 = new CommandInfo
			{
				Command = 140,
				InputParameterIndex = 20,
				TaskType = ClientTaskType.Synchronous,
				IsVerifyMethod = true,
				IsSimpleSerializeOutputParamter = true
			};

			
			private static readonly CommandInfo _ac14 = new CommandInfo
			{
				Command = 141,
				InputParameterIndex = 0,
				TaskType = ClientTaskType.Timeout,
				IsKeepCallback = 1
			};

			
			private static readonly CommandInfo _c15 = new CommandInfo
			{
				Command = 142,
				InputParameterIndex = 22,
				IsSendOnly = 1,
				TaskType = ClientTaskType.Synchronous
			};
		}

		
		public class TestS2KFCommunication
		{
			
			public static ReturnValue<string> SendData(int strLen, bool flag)
			{
				AutoWaitReturnValue<KfCall._p31> _wait_ = AutoWaitReturnValue<KfCall._p31>.Pop();
				try
				{
					AutoCSer.Net.TcpInternalServer.ClientSocketSender _socket_ = KfCall.TcpClient.Sender;
					if (_socket_ != null)
					{
						KfCall._p30 _inputParameter_ = new KfCall._p30
						{
							p1 = strLen,
							p0 = flag
						};
						KfCall._p31 _outputParameter_ = default(KfCall._p31);
						ReturnType _returnType_ = _socket_.WaitGet<KfCall._p30, KfCall._p31>(TcpCall.TestS2KFCommunication._c23, ref _wait_, ref _inputParameter_, ref _outputParameter_);
						return new ReturnValue<string>
						{
							Type = _returnType_,
							Value = _outputParameter_.Return
						};
					}
				}
				finally
				{
					if (_wait_ != null)
					{
						AutoWaitReturnValue<KfCall._p31>.PushNotNull(_wait_);
					}
				}
				return new ReturnValue<string>
				{
					Type = ReturnType.ClientException
				};
			}

			
			private static readonly CommandInfo _c23 = new CommandInfo
			{
				Command = 150,
				InputParameterIndex = 30,
				TaskType = ClientTaskType.Synchronous,
				IsSimpleSerializeInputParamter = true,
				IsSimpleSerializeOutputParamter = true
			};
		}

		
		public class ZhanDuiZhengBa_K
		{
			
			public static ReturnValue<ZhanDuiZhengBaSyncData> SyncZhengBaData(ZhanDuiZhengBaSyncData lastSyncData)
			{
				AutoWaitReturnValue<KfCall._p24> _wait_ = AutoWaitReturnValue<KfCall._p24>.Pop();
				try
				{
					AutoCSer.Net.TcpInternalServer.ClientSocketSender _socket_ = KfCall.TcpClient.Sender;
					if (_socket_ != null)
					{
						KfCall._p23 _inputParameter_ = new KfCall._p23
						{
							p0 = lastSyncData
						};
						KfCall._p24 _outputParameter_ = default(KfCall._p24);
						ReturnType _returnType_ = _socket_.WaitGet<KfCall._p23, KfCall._p24>(TcpCall.ZhanDuiZhengBa_K._c16, ref _wait_, ref _inputParameter_, ref _outputParameter_);
						return new ReturnValue<ZhanDuiZhengBaSyncData>
						{
							Type = _returnType_,
							Value = _outputParameter_.Return
						};
					}
				}
				finally
				{
					if (_wait_ != null)
					{
						AutoWaitReturnValue<KfCall._p24>.PushNotNull(_wait_);
					}
				}
				return new ReturnValue<ZhanDuiZhengBaSyncData>
				{
					Type = ReturnType.ClientException
				};
			}

			
			public static ReturnValue<int> ZhengBaKuaFuLogin(int zhanDuiID, int gameId, int srcServerID, out ZhanDuiZhengBaFuBenData copyData)
			{
				AutoWaitReturnValue<KfCall._p26> _wait_ = AutoWaitReturnValue<KfCall._p26>.Pop();
				try
				{
					AutoCSer.Net.TcpInternalServer.ClientSocketSender _socket_ = KfCall.TcpClient.Sender;
					if (_socket_ != null)
					{
						KfCall._p25 _inputParameter_ = new KfCall._p25
						{
							p1 = zhanDuiID,
							p2 = gameId,
							p3 = srcServerID
						};
						KfCall._p26 _outputParameter_ = default(KfCall._p26);
						ReturnType _returnType_ = _socket_.WaitGet<KfCall._p25, KfCall._p26>(TcpCall.ZhanDuiZhengBa_K._c17, ref _wait_, ref _inputParameter_, ref _outputParameter_);
						copyData = _outputParameter_.p0;
						return new ReturnValue<int>
						{
							Type = _returnType_,
							Value = _outputParameter_.Return
						};
					}
				}
				finally
				{
					if (_wait_ != null)
					{
						AutoWaitReturnValue<KfCall._p26>.PushNotNull(_wait_);
					}
				}
				copyData = null;
				return new ReturnValue<int>
				{
					Type = ReturnType.ClientException
				};
			}

			
			public static ReturnValue<List<ZhanDuiZhengBaNtfPkResultData>> ZhengBaPkResult(int gameId, int winner1)
			{
				AutoWaitReturnValue<KfCall._p28> _wait_ = AutoWaitReturnValue<KfCall._p28>.Pop();
				try
				{
					AutoCSer.Net.TcpInternalServer.ClientSocketSender _socket_ = KfCall.TcpClient.Sender;
					if (_socket_ != null)
					{
						KfCall._p27 _inputParameter_ = new KfCall._p27
						{
							p0 = gameId,
							p1 = winner1
						};
						KfCall._p28 _outputParameter_ = default(KfCall._p28);
						ReturnType _returnType_ = _socket_.WaitGet<KfCall._p27, KfCall._p28>(TcpCall.ZhanDuiZhengBa_K._c18, ref _wait_, ref _inputParameter_, ref _outputParameter_);
						return new ReturnValue<List<ZhanDuiZhengBaNtfPkResultData>>
						{
							Type = _returnType_,
							Value = _outputParameter_.Return
						};
					}
				}
				finally
				{
					if (_wait_ != null)
					{
						AutoWaitReturnValue<KfCall._p28>.PushNotNull(_wait_);
					}
				}
				return new ReturnValue<List<ZhanDuiZhengBaNtfPkResultData>>
				{
					Type = ReturnType.ClientException
				};
			}

			
			public static ReturnValue<int> ZhengBaRequestEnter(int zhanDuiID, out int gameId, out int kuaFuServerID, out string[] ips, out int[] ports)
			{
				AutoWaitReturnValue<KfCall._p8> _wait_ = AutoWaitReturnValue<KfCall._p8>.Pop();
				try
				{
					AutoCSer.Net.TcpInternalServer.ClientSocketSender _socket_ = KfCall.TcpClient.Sender;
					if (_socket_ != null)
					{
						KfCall._p7 _inputParameter_ = new KfCall._p7
						{
							p0 = zhanDuiID
						};
						KfCall._p8 _outputParameter_ = default(KfCall._p8);
						ReturnType _returnType_ = _socket_.WaitGet<KfCall._p7, KfCall._p8>(TcpCall.ZhanDuiZhengBa_K._c19, ref _wait_, ref _inputParameter_, ref _outputParameter_);
						gameId = _outputParameter_.p0;
						kuaFuServerID = _outputParameter_.p1;
						ips = _outputParameter_.p3;
						ports = _outputParameter_.p2;
						return new ReturnValue<int>
						{
							Type = _returnType_,
							Value = _outputParameter_.Return
						};
					}
				}
				finally
				{
					if (_wait_ != null)
					{
						AutoWaitReturnValue<KfCall._p8>.PushNotNull(_wait_);
					}
				}
				gameId = 0;
				kuaFuServerID = 0;
				ips = null;
				ports = null;
				return new ReturnValue<int>
				{
					Type = ReturnType.ClientException
				};
			}

			
			private static readonly CommandInfo _c16 = new CommandInfo
			{
				Command = 143,
				InputParameterIndex = 23,
				TaskType = ClientTaskType.Synchronous
			};

			
			private static readonly CommandInfo _c17 = new CommandInfo
			{
				Command = 144,
				InputParameterIndex = 25,
				TaskType = ClientTaskType.Synchronous
			};

			
			private static readonly CommandInfo _c18 = new CommandInfo
			{
				Command = 145,
				InputParameterIndex = 27,
				TaskType = ClientTaskType.Synchronous,
				IsSimpleSerializeInputParamter = true
			};

			
			private static readonly CommandInfo _c19 = new CommandInfo
			{
				Command = 146,
				InputParameterIndex = 7,
				TaskType = ClientTaskType.Synchronous
			};
		}
	}
}
