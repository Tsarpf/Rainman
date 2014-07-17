using UnityEngine;
using System.Collections;

public class Droplet : MonoBehaviour {

    float fullSize = 0.75F;
    float steps;
    Object splash;
    
    // Use this for initialization
	void Start () {
        steps = 0.0F;
        splash = Resources.Load("SplashPrefab");
	}
	
	// Update is called once per frame
	void Update () {
        if (steps < fullSize)
        {
            steps += 0.01F;
        }
        gameObject.transform.localScale = new Vector3(steps, steps, 1);
	}


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "DropletPrefab(Clone)")
            return;
        else if(other.name == "Player")
        {
            Destroy(other.gameObject);
            gameOver();
        }
        else if (other.name == "Floor")
        {
            GameObject newSplash = Instantiate(splash) as GameObject;
            newSplash.transform.position = gameObject.transform.position;  
        }


        Destroy(gameObject);
    }

    void gameOver()
    {
        
        Object box = Resources.Load("TextBox");
        WorldScript.GameOver();
        GameObject tryAgainBox = Instantiate(box) as GameObject;
        tryAgainBox.transform.position = new Vector3(0.5F, 0, 0);

    }

}
