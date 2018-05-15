using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawnner : MonoBehaviour
{

    [SerializeField]
    private GameObject[] clouds;
    [SerializeField]
    private GameObject[] collectables;

    private float distanceBetweenClouds = 2f;
    private float xMin, xMax;
    private float lastCloudPositionY;
    private float controlX;
    private GameObject player;



    void Awake()
    {
        controlX = 0;
        player = GameObject.Find("Player");

        SetMinAndMax();
        CreateCloud();

        // Set the collectables to false at the beginning
        for (int i = 0; i < collectables.Length; i++)
        {
            collectables[i].SetActive(false);
        }
    }

    void Start()
    {
        PositionPlayerOnCluds();
    }

    void SetMinAndMax()
    {
        Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));

        xMax = bounds.x - 0.5f;
        xMin = -bounds.x + 0.5f;
    }

    void Shuffle(GameObject[] arrayClouds)
    {
        for (int i = 0; i < arrayClouds.Length; i++)
        {
            GameObject tempCloud = arrayClouds[i];
            int random = Random.Range(i, arrayClouds.Length);
            arrayClouds[i] = arrayClouds[random];
            arrayClouds[random] = tempCloud;
        }
    }

    void CreateCloud()
    {
        Shuffle(clouds);

        float positionY = 0f;

        for (int i = 0; i < clouds.Length; i++)
        {
            Vector3 temp = clouds[i].transform.position;
            temp.y = positionY;

            // Setting the x position           
            if (controlX == 0)
            {
                temp.x = Random.Range(0.0f, xMax);
                controlX = 1;
            }
            else if(controlX == 1)
            {
                temp.x = Random.Range(0.0f, xMin);
                controlX = 2;
            }
            else if (controlX == 2)
            {
                temp.x = Random.Range(-1.0f, xMax);
                controlX = 3;
            }
            else if (controlX == 3)
            {
                temp.x = Random.Range(1.0f, xMin);
                controlX = 0;
            }

            lastCloudPositionY = positionY;                      

            clouds[i].transform.position = temp;
            positionY -= distanceBetweenClouds;
        }
    }

    void PositionPlayerOnCluds()
    {
        GameObject[] darkClouds = GameObject.FindGameObjectsWithTag("Deadly");
        GameObject[] clouds = GameObject.FindGameObjectsWithTag("Cloud");

        for (int i = 0; i < darkClouds.Length; i++)
        {
           if(darkClouds[i].transform.position.y == 0)
            {
                Vector3 t = darkClouds[i].transform.position;

                darkClouds[i].transform.position = new Vector3(clouds[0].transform.position.x,
                                                                clouds[0].transform.position.y,
                                                                 clouds[0].transform.position.z);

                clouds[0].transform.position = t;
            }
        }

        Vector3 temp = clouds[0].transform.position;
        for (int i = 1; i < clouds.Length; i++)
        {
            if(temp.y < clouds[i].transform.position.y)
            {
                temp = clouds[i].transform.position;
            }
        }

        temp.y += 0.8f;
        player.transform.position = temp;
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Cloud" || target.tag == "Deadly")
        {
            if(target.transform.position.y == lastCloudPositionY)
            {
                Shuffle(clouds);
                Shuffle(collectables);

                Vector3 temp = target.transform.position;

                for (int i = 0; i < clouds.Length; i++)
                {
                    if(!clouds[i].activeInHierarchy)
                    {
                        // Setting the x position           
                        if (controlX == 0)
                        {
                            temp.x = Random.Range(0.0f, xMax);
                            controlX = 1;
                        }
                        else if (controlX == 1)
                        {
                            temp.x = Random.Range(0.0f, xMin);
                            controlX = 2;
                        }
                        else if (controlX == 2)
                        {
                            temp.x = Random.Range(-1.0f, xMax);
                            controlX = 3;
                        }
                        else if (controlX == 3)
                        {
                            temp.x = Random.Range(1.0f, xMin);
                            controlX = 0;
                        }

                        temp.y -= distanceBetweenClouds;

                        clouds[i].transform.position = temp;
                       
                        lastCloudPositionY = temp.y;
                        clouds[i].SetActive(true);

                        int random = Random.Range(0, collectables.Length);

                        if(clouds[i].tag != "Deadly")
                        {
                            if(!collectables[random].activeInHierarchy)
                            {
                                Vector3 temp2 = clouds[i].transform.position;
                                temp2.y += 0.7f;

                                if(collectables[random].tag == "Life")
                                {
                                    if(PlayerScore.lifeCount < 2)
                                    {
                                        collectables[random].transform.position = temp2;
                                        collectables[random].SetActive(true);
                                    }
                                }
                                else
                                {
                                    collectables[random].transform.position = temp2;
                                    collectables[random].SetActive(true);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
