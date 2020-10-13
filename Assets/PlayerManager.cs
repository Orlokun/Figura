using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    PlayerGeneralStats gStats;

    private void Awake()
    {
        //Instance from Json
        gStats = (PlayerGeneralStats)PlayerGeneralStats.CreateInstance(typeof(PlayerGeneralStats));

    }
}
