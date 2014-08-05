using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Droplet : MonoBehaviour {

    float fullSize = 0.75F;
    float steps;
    static UnityEngine.Object splash;
    
    // Use this for initialization
	void Start () {
        steps = 0.0F;
        if(splash == null)
            splash = Resources.Load("SplashPrefab");
	}
	
	// Update is called once per frame
	void Update () {
        if (steps < fullSize)
        {
            steps += 0.01F;
        }
        gameObject.transform.localScale = new Vector3(steps, steps, 1);

        if (transform.position.y < 0)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "DropletPrefab(Clone)")
            return;

		//if (other.name == "Player")
		//	Destroy(other.gameObject);
        //if(MoveController.parts.Contains(other.name))
        //{
        //    Destroy(GameObject.Find("Player"));
        //}

        Destroy(gameObject);
    }

    void gameOver()
    {
        
        //Object box = Resources.Load("TextBox");
        //WorldScript.GameOver();
        //GameObject tryAgainBox = Instantiate(box) as GameObject;
        //tryAgainBox.transform.position = new Vector3(0.5F, 0, 0);

    }

}
