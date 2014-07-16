using UnityEngine;
using System.Collections;

public class MoveController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    bool isOnGround = false;
    int playerSpeed = 10;
	void Update () {
	   if(Input.GetKey(KeyCode.A))
       {
           gameObject.rigidbody2D.velocity = new Vector2(-playerSpeed, gameObject.rigidbody2D.velocity.y);
       }
       else if(Input.GetKey(KeyCode.D))
       {
           gameObject.rigidbody2D.velocity = new Vector2(playerSpeed, gameObject.rigidbody2D.velocity.y);
       }
       else
       {
           gameObject.rigidbody2D.velocity = new Vector2(0, gameObject.rigidbody2D.velocity.y);
       }
       if(isOnGround && Input.GetKeyDown(KeyCode.W))
       {
           gameObject.rigidbody2D.velocity = new Vector2(gameObject.rigidbody2D.velocity.x, playerSpeed);
       }
	}

    void OnCollisionEnter2D()
    {
        Debug.Log("enter");
        isOnGround = true;
    }

    void OnCollisionExit2D()
    {
        Debug.Log("exit");
        isOnGround = false;
    }

    void OnCollisionStay2D()
    {
        //Debug.Log("stay");
    }
}
