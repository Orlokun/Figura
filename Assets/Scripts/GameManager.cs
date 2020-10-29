using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    public static PlayerProfileData pData;


    public static PlayerProfileData GetPlayerManager()
    {
        return pData;
    }

    public static void SetPlayerManager(PlayerProfileData _pData)
    {
        pData = _pData;
    }

    private static void StartPlayerManager()
    {

    }
}
