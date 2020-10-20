using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerManager", menuName ="PlayerManager")]
public class PlayerManager : ScriptableObject
{
    [SerializeField]
    public PlayerProfileData pStats;


    public void GetPlayerDataFromFiles()
    {
        //Get Json depending on user
        pStats = (PlayerProfileData)PlayerProfileData.CreateInstance(typeof(PlayerProfileData));
    }

    public PlayerProfileData GetPlayerData()
    {
        return pStats;
    }
}
