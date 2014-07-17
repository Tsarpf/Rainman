using UnityEngine;
using System.Collections;

public class MoveController : MonoBehaviour {

    Texture2D boxTexture;
    GUIStyle style = new GUIStyle();
	void Start () {
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

        //Debug.Log("mouse putton 0: " + Input.GetMouseButton(0));
        if (!Input.GetMouseButton(0))
            return;

        float x = Input.mousePosition.x;
        float y = Input.mousePosition.y;
        //Debug.Log("x: " + x + " y: " + y);

        //jump
        //if(x < 300 && y > Screen.height - 200 && y < Screen.height - 100)
        if(x < 300 && y < 200 && y > 100)
        {
            jump = true;
        }

        //reft
        if(x < 100 && y < 200)
        {
            goLeft = true;
        }

        //right
        else if(x > 200 && y < 200)
        {
            goRight = true;
        }
    }

    bool isOnGround = false;
    int playerSpeed = 10;
	void Update () {



        getInput();

        if(goLeft)
            gameObject.rigidbody2D.velocity = new Vector2(-playerSpeed, gameObject.rigidbody2D.velocity.y);
        else if(goRight)
            gameObject.rigidbody2D.velocity = new Vector2(playerSpeed, gameObject.rigidbody2D.velocity.y);
        else
            gameObject.rigidbody2D.velocity = new Vector2(0, gameObject.rigidbody2D.velocity.y);

        if(jump && isOnGround)
            gameObject.rigidbody2D.velocity = new Vector2(gameObject.rigidbody2D.velocity.x, playerSpeed);
            
            

        //if(Input.GetKey(KeyCode.A))
        //{
        //    gameObject.rigidbody2D.velocity = new Vector2(-playerSpeed, gameObject.rigidbody2D.velocity.y);
        //}
        //else if(Input.GetKey(KeyCode.D))
        //{
        //    gameObject.rigidbody2D.velocity = new Vector2(playerSpeed, gameObject.rigidbody2D.velocity.y);
        //}
        //else
        //{
        //    gameObject.rigidbody2D.velocity = new Vector2(0, gameObject.rigidbody2D.velocity.y);
        //}
        //if(isOnGround && Input.GetKeyDown(KeyCode.W))
        //{
        //    gameObject.rigidbody2D.velocity = new Vector2(gameObject.rigidbody2D.velocity.x, playerSpeed);
        //}

        jump = false;
        goLeft = false;
        goRight = false;
	}


    void OnGUI()
    {
        //Jump
        GUI.color = Color.blue;
        GUI.Box(new Rect(0, Screen.height - 200, 300, 100), "This is a title", style);
        //Left
        GUI.color = Color.red;
        GUI.Box(new Rect(0, Screen.height - 200, 100, 200), "This is a title", style);
        //Still
        GUI.color = Color.gray;
        GUI.Box(new Rect(100, Screen.height - 200, 100, 200), "This is a title", style);
        //Right
        GUI.color = Color.green;
        GUI.Box(new Rect(200, Screen.height - 200, 100, 200), "This is a title", style);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.name == "DropletPrefab(Clone)")
        {
            Debug.Log("you died");
            Destroy(gameObject);
        }
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
