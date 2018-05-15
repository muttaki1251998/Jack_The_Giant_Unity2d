using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSpawn : MonoBehaviour {

    private GameObject[] background;
    private float lastY;

    void Start()
    {
        MakeBackground();
    }


    void MakeBackground()
    {
        background = GameObject.FindGameObjectsWithTag("Background");

        // Set the last Y position
        lastY = background[0].transform.position.y;
        for (int i = 1; i < background.Length; i++)
        {
            if(lastY > background[i].transform.position.y)
            {
                lastY = background[i].transform.position.y;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Background")
        {
            if(target.transform.position.y == lastY)
            {
                Vector3 temp = target.transform.position;
                float height = ((BoxCollider2D)target).size.y;

                for (int i = 0; i < background.Length; i++)
                {
                    if(!background[i].activeInHierarchy)
                    {
                        temp.y -= height;
                        lastY = temp.y;
                        background[i].transform.position = temp;
                        background[i].gameObject.SetActive(true);
                    }
                }
            }
        }
    }




}
