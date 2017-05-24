using UnityEngine;
using System.Collections;

public class PowerUpScript : MonoBehaviour 
{

    Sprite p1;
    Sprite p2;
    Sprite p3;

    public int type;

    public float lifeSpan = 10.0f;

    WeaponTypeScript weaponTypeScript;


	void Start () 
    {
        weaponTypeScript = GameObject.Find("WeaponTypeBoxes").GetComponent<WeaponTypeScript>();
	
	}
	
	void Update () 
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - .05f, transform.position.z);

        lifeSpan -= Time.deltaTime;

        if(lifeSpan <= 0)
        {
            Destroy(gameObject);
        }
	}

   public void SetPowerUp(int powerUpType)
    {
       type = powerUpType;
      
       if(type == 1)
       {
           gameObject.GetComponent<SpriteRenderer>().sprite = p1;
       }

       else if(type == 2)
       {
           gameObject.GetComponent<SpriteRenderer>().sprite = p2;          
       }

       else if(type == 3)
       {
           gameObject.GetComponent<SpriteRenderer>().sprite = p3;
       }
    }

   void OnCollisionEnter2D(Collision2D coll)
   {
       if (coll.gameObject.tag == "PlayersShip")
       {
           weaponTypeScript.AddPowerUp("MeteorShield");
           Destroy(gameObject);
       }

       
   }
}

