using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : Singleton<CollectibleManager>
{
    private int collectibleCount;
    // Start is called before the first frame update
    void Start()
    {
        collectibleCount = 0;
    }

    public void Add()
    {
        collectibleCount++;
        Debug.Log(collectibleCount);
    }

}
