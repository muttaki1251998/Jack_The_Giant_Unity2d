using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesSpawn : MonoBehaviour
{

     void OnEnable()
    {
        Invoke("DestroyCollectables", 6.0f);
    }

    void DestroyCollectables()
    {
        gameObject.SetActive(false);
    }

}
