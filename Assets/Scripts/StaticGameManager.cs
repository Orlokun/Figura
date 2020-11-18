using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticGameManager
{
    public static PlayerProfileData pData;
    public static ServerMessageHandler sMessageHandler;

    public static PlayerProfileData GetPlayerData()
    {
        return pData;
    }

    public static void SetNewPlayerData(PlayerProfileData _pData)
    {
        pData = _pData;
    }

    public static void CreatePlayerDataIfNeeded()
    {
        if (pData != null)
        {
            Debug.Log("Already have playerData");
        }
        else
        {
            Debug.Log("Creating PDATA!");
            pData = new PlayerProfileData();
            pData.StartData();
        }
    }

    public static void CreateServerInstance()
    {
        if (sMessageHandler != null)
        {
            Debug.Log("Already have ServerMessageHandler");
        }
        else
        {
            sMessageHandler = new ServerMessageHandler();
            Debug.Log("Creating MHandler!");
        }
    }

}
