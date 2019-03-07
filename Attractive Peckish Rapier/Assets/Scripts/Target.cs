using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public NPCTypes npcType;
    public int id;

    public void SetValues(int id, NPCTypes type)
    {
        this.id = id;
        npcType = type;
    }

    public string ToString()
    {
        return "Target - id: " + id + " of type: " + npcType;
    }
}
