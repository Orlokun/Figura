using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticGameManager
{
    private static PlayerProfileData pData;


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
            pData = ScriptableObject.CreateInstance<PlayerProfileData>();
            pData.StartData();
        }
    }

    private static void StartPlayerManager()
    {

    }


}
