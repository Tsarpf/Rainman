using UnityEngine;
using System.Collections;

public class Droplet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "DropletPrefab(Clone)")
            return;
        else if(other.name == "Player")
        {
            Destroy(other.gameObject);
        }


        Destroy(gameObject);
    }
}
