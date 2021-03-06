﻿using System;
using System.Collections.Generic;
using KF.Contract.Data;
using Server.Data;
using Tmsk.Contract;

namespace GameServer.Logic
{
	
	public class TianTi5v5Scene
	{
		
		public void CleanAllInfo()
		{
			this.m_nMapCode = 0;
			this.m_lPrepareTime = 0L;
			this.m_lBeginTime = 0L;
			this.m_lEndTime = 0L;
			this.m_eStatus = GameSceneStatuses.STATUS_NULL;
			this.m_nPlarerCount = 0;
			this.m_bEndFlag = false;
		}

		
		public int m_nMapCode = 0;

		
		public int FuBenSeqId = 0;

		
		public int CopyMapId = 0;

		
		public long m_lPrepareTime = 0L;

		
		public long m_lBeginTime = 0L;

		
		public long m_lEndTime = 0L;

		
		public long m_lLeaveTime = 0L;

		
		public GameSceneStatuses m_eStatus = GameSceneStatuses.STATUS_NULL;

		
		public int m_nPlarerCount = 0;

		
		public int SuccessSide = 0;

		
		public bool m_bEndFlag = false;

		
		public int GameId;

		
		public CopyMap CopyMap;

		
		public Dictionary<int, GameClient> ClientDict = new Dictionary<int, GameClient>();

		
		public Dictionary<int, TianTi5v5RoleMiniData> RoleIdDuanWeiIdDict = new Dictionary<int, TianTi5v5RoleMiniData>();

		
		public List<Tuple<TianTi5v5ZhanDuiData, int>> ZhanDuiDataDict = new List<Tuple<TianTi5v5ZhanDuiData, int>>();

		
		public KuaFu5v5FuBenData FuBenData;

		
		public Dictionary<int, Tuple<int, bool>> RoleSideStateDict = new Dictionary<int, Tuple<int, bool>>();

		
		public int LastLeaveZhanDuiID = -1;

		
		public KuaFu5v5ScoreInfoData ScoreInfoData = new KuaFu5v5ScoreInfoData();

		
		public GameSceneStateTimeData StateTimeData = new GameSceneStateTimeData();
	}
}
