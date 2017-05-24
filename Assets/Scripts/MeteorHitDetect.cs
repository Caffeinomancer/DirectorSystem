using UnityEngine;
using System.Collections;

public class MeteorHitDetect : MonoBehaviour 
{

    private MenuScript menuCheck;
    private bool isMenu = false;
	// Use this for initialization
	void Start () 
    {
        menuCheck = GameObject.Find("Main Camera").GetComponent<MenuScript>();
        if (menuCheck != null)
            isMenu = true;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
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
            if(!isMenu)
                Kill();
        }
    }

    private void Kill()
    {
        Destroy(gameObject);
    }
}
