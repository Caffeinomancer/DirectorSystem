using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    private static float SCREEN_WIDTH_MIN = -15;
    private static float SCREEN_WIDTH_MAX = 15;
    private static float SCREEN_HEIGHT_MIN = -9;
    private static float SCREEN_HEIGHT_MAX = 9;
    public float KillDistance = 0.0f;
    public float BulletSpeed;

    public Vector3 startingPos;
    public Quaternion startingRot;

    private Rigidbody2D rb;

    private Vector2 destination;
    GameObject player;

    private MenuScript checkMenu;
    private bool isMenu = false;

	// Use this for initialization
	void Start () 
    {
        //rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        //transform.position = player.transform.position;
        //transform.rotation = player.transform.rotation;
        checkMenu = GameObject.Find("Main Camera").GetComponent<MenuScript>();
        if(checkMenu != null)
        {
            isMenu = true;
        }

        gameObject.tag = "PlayersBullet";
	}
	
	// Update is called once per frame
	void Update () 
    {
        MoveBullet();
        CheckOutOfBounds();
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "WanderEnemy")
        {
            if(!isMenu)
             Kill();
        }

        if (coll.gameObject.tag == "SeekerEnemy")
        {
            if (!isMenu)
                Kill();
        }

        if (coll.gameObject.tag == "MenuEnemy")
        {
            Kill();
        }
    }

    private void MoveBullet()
    {
        transform.Translate(Vector3.up * 10.0f * Time.smoothDeltaTime);
    }
    



    private void CheckOutOfBounds()
    {
        if(transform.position.x < SCREEN_WIDTH_MIN - KillDistance)
        {
            Kill();
        }

        else if(transform.position.x > SCREEN_WIDTH_MAX + KillDistance)
        {
            Kill();
        }

        else if(transform.position.y < SCREEN_HEIGHT_MIN - KillDistance)
        {
            Kill();
        }

        else if(transform.position.y > SCREEN_HEIGHT_MAX + KillDistance)
        {
            Kill();
        }

        else
        {
            //Alive
        }
    }

    private void Kill()
    {
        Destroy(gameObject);
    }

    public void MakeMoveable()
    {
        rb.isKinematic = false;
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
    }
}
