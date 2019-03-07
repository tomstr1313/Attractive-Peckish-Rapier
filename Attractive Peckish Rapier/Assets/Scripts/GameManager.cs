using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Target> targets;
    public GameObject targetPrefab;
    public GameObject targetsParent;

    // Start is called before the first frame update
    void Start()
    {
        // Spawning four targets randomly
        SpawnTargets(4);
    }

    // Spawn a target of type and id as a child of the Targets Game Object
    private void CreateTarget (int id, NPCTypes type)
    {
        GameObject go = Instantiate(targetPrefab);
        go.transform.position = new Vector3(Random.Range(-30, 30), Random.Range(-12, 12), 0);
        go.transform.parent = targetsParent.transform;

        if (type == NPCTypes.FARMER)
        {
            go.GetComponent<Target>().SetValues(id, NPCTypes.FARMER);
            go.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            go.GetComponent<Target>().SetValues(id, NPCTypes.ACCOUNTANT);
            go.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    // Spawn x targets
    public void SpawnTargets(int numTargets)
    {
        for (int i = 0; i < numTargets; i++)
        {
            int type = Random.Range(0, 2);
            if (type == 0)
                CreateTarget(i, NPCTypes.FARMER);
            else if (type == 1)
                CreateTarget(i, NPCTypes.ACCOUNTANT);
        }

        for (int i = 0; i < targetsParent.transform.childCount; i++)
        {
            targets.Add(targetsParent.transform.GetChild(i).GetComponent<Target>());
        }

        for (int i = 0; i < targets.Count; i++)
        {
            Debug.Log(targets[i].ToString());
        }
    }
}
