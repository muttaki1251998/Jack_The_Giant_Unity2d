using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounds : MonoBehaviour {

    private float xMin, xMax;

    // Use this for initialization
    void Start () {
        SetMinMax();
	}
	
	// Update is called once per frame
	void Update () {
        SetPlayerBounds();
	}

    void SetMinMax()
    {
        Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        xMax = bounds.x;
        xMin = -bounds.x;
    }

    void SetPlayerBounds()
    {
        if(transform.position.x > xMax)
        {
            Vector2 temp = transform.position;
            temp.x = xMax;
            transform.position = temp;
        }

        if (transform.position.x < xMin)
        {
            Vector2 temp = transform.position;
            temp.x = xMin;
            transform.position = temp;
        }
    }
}
