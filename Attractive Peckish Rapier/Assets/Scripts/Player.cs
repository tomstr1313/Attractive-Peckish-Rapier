using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public NPCTypes playerType;

    void Start()
    {
        playerType = NPCTypes.RAPIER;
    }
}
