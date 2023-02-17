namespace ET
{
	public static partial class InnerOpcode
	{
		 public const ushort ObjectQueryRequest = 20002;
		 public const ushort M2A_Reload = 20003;
		 public const ushort A2M_Reload = 20004;
		 public const ushort G2G_LockRequest = 20005;
		 public const ushort G2G_LockResponse = 20006;
		 public const ushort G2G_LockReleaseRequest = 20007;
		 public const ushort G2G_LockReleaseResponse = 20008;
		 public const ushort ObjectAddRequest = 20009;
		 public const ushort ObjectAddResponse = 20010;
		 public const ushort ObjectLockRequest = 20011;
		 public const ushort ObjectLockResponse = 20012;
		 public const ushort ObjectUnLockRequest = 20013;
		 public const ushort ObjectUnLockResponse = 20014;
		 public const ushort ObjectRemoveRequest = 20015;
		 public const ushort ObjectRemoveResponse = 20016;
		 public const ushort ObjectGetRequest = 20017;
		 public const ushort ObjectGetResponse = 20018;
		 public const ushort R2G_GetLoginKey = 20019;
		 public const ushort G2R_GetLoginKey = 20020;
		 public const ushort M2M_UnitTransferResponse = 20021;
		 public const ushort G2M_SessionDisconnect = 20022;
		 public const ushort A2L_LoginAccountRequest = 20023;
		 public const ushort L2A_LoginAccountResponse = 20024;
		 public const ushort L2G_DisconnectGateUnitRequest = 20025;
		 public const ushort G2L_DisconnectGateUnitResponse = 20026;
		 public const ushort A2R_GetRealmKeyRequest = 20027;
		 public const ushort R2A_GetRealmKeyResponse = 20028;
		 public const ushort R2G_GetLoginGateKeyRequest = 20029;
		 public const ushort G2R_GetLoginGateKeyResponse = 20030;
		 public const ushort G2L_AddLoginRecordRequest = 20031;
		 public const ushort L2G_AddLoginRecordResponse = 20032;
		 public const ushort G2M_GetEnterGameStateRequest = 20033;
		 public const ushort M2G_GetEnterGameStateResponse = 20034;
		 public const ushort G2M_ExitGameRequest = 20035;
		 public const ushort M2G_ExitGameResponse = 20036;
		 public const ushort G2L_RemoveLoginRecordRequest = 20037;
		 public const ushort L2G_RemoveLoginRecordResponse = 20038;
		 public const ushort Other2UnitCache_AddOrUpdateUnitRequest = 20039;
		 public const ushort UnitCache2Other_AddOrUpdateUnitResponse = 20040;
		 public const ushort Other2UnitCache_GetUnitRequest = 20041;
		 public const ushort Other2UnitCache_DeleteUnitRequest = 20042;
		 public const ushort UnitCache2Other_DeleteUnitResponse = 20043;
	}
}
