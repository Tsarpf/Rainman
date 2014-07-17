using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveController : MonoBehaviour {

    Texture2D boxTexture;
    GUIStyle style = new GUIStyle();
    public static List<string> playerParts;
	void Start () {
        playerParts = new List<string>();
        foreach (Transform child in transform)
        {
            playerParts.Add(child.name);
        }
        boxTexture = new Texture2D(1, 1);	
        for(int y = 0; y < boxTexture.height; y++)
        {
            for(int x = 0; x < boxTexture.width; x++)
            {
                boxTexture.SetPixel(x, y, new Color(1, 1, 1, 1));
            }
        }

        style.normal.background = boxTexture;
	}


    bool goLeft = false;
    bool goRight = false;
    bool jump = false;
    void getInput()
    {

        if (!Input.GetMouseButton(0))
            return;

        float x = Input.mousePosition.x;
        float y = Input.mousePosition.y;

        //jump
        if(y < 400 && y > 100)
        {
            jump = true;
        }

        int totalWidth = 300;
        int xOffset = Screen.width / 2 - totalWidth / 2;
        //reft
        if(x < 100 + xOffset && y < 400)
        {
            goLeft = true;
        }

        //light
        else if(x > 200 + xOffset && y < 400)
        {
            goRight = true;
        }
    }

    bool isOnGround = false;
    int playerSpeed = 10;
	void Update () {
        getInput();

        if(jump && isOnGround)
            gameObject.rigidbody2D.velocity = new Vector2(gameObject.rigidbody2D.velocity.x, playerSpeed);

        if(goLeft)
            gameObject.rigidbody2D.velocity = new Vector2(-playerSpeed, gameObject.rigidbody2D.velocity.y);
        else if(goRight)
            gameObject.rigidbody2D.velocity = new Vector2(playerSpeed, gameObject.rigidbody2D.velocity.y);
        else
            gameObject.rigidbody2D.velocity = new Vector2(0, gameObject.rigidbody2D.velocity.y);

        jump = false;
        goLeft = false;
        goRight = false;


        transform.rotation = new Quaternion();
	}


    void OnGUI()
    {

        int totalWidth = 300;
        int xOffset = Screen.width / 2 - totalWidth / 2;
        //Jump
        GUI.color = Color.blue;
        GUI.Box(new Rect(0 + xOffset, Screen.height - 200, 300, 100), "This is a title", style);
        //Left
        GUI.color = Color.red;
        GUI.Box(new Rect(0 + xOffset, Screen.height - 200, 100, 200), "This is a title", style);
        GUI.color = Color.green;
        GUI.Box(new Rect(200 + xOffset, Screen.height - 200, 100, 200), "This is a title", style);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.collider.name == "DropletPrefab(Clone)")
        //{
        //    Debug.Log("you died");
        //    Destroy(gameObject);
        //}
        //Debug.Log("enter");
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
