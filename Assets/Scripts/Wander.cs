using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class Wander : MonoBehaviour 
{

    AudioSource audio;

    public AudioClip explosion;

    public GameObject deathAnim;
    private Vector2 destination;
    private bool newDestination;
    private bool isInGameField = false;
    public float speed = 5.0f;

    ScoreSystem scoreSystem;

    public float xRangeMin;
    public float xRangeMax;
    public float yRangeMin;
    public float yRangeMax;
    PlayerScript player;

    private bool isMenu = false;

	// Use this for initialization
	void Start () 
    {
        newDestination = false;
        destination = new Vector2(Random.Range(xRangeMin, xRangeMax), Random.Range(yRangeMin, yRangeMax));
        gameObject.tag = "WanderEnemy";

        if(SceneManager.GetActiveScene().name != "Menu")
            scoreSystem = GameObject.Find("Score").GetComponent<ScoreSystem>();

        audio = GetComponent<AudioSource>();
        player = GameObject.Find("Player").GetComponent<PlayerScript>();

    }



    void OnTriggerEnter2D(Collider2D coll)
    {
        if(isInGameField)
        {
            if (coll.gameObject.tag == "MenuEnemy")
            {
                Kill();
            }

            if(coll.gameObject.tag == "PlayersBullet")
            {
                if(!isMenu)
                    scoreSystem.WandererKill();

                Kill();
            }

            if (coll.gameObject.tag == "Meteor")
            {
                if(!isMenu)
                    scoreSystem.WandererKill();

                Kill();
            }
        }
    }

	// Update is called once per frame
	void Update () 
    {

        if (scoreSystem == null)
            isMenu = true;

        if(!isInGameField)
        {
            if((transform.position.x >= xRangeMin && transform.position.x <= xRangeMax) &&
                (transform.position.y >= yRangeMin && transform.position.y <= yRangeMax))
            {
                isInGameField = true;
            }
        }

        if(newDestination)
        {
            destination = new Vector2(Random.Range(-18, 18), Random.Range(-9.5f, 9.5f));
            newDestination = false; 
        }


        transform.Rotate(Vector3.forward * 2);

        PathToPoint();
	}

    private void PathToPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, Time.deltaTime * speed);
        if(transform.position.x == destination.x && transform.position.y == destination.y)
        {
            newDestination = true;
        }
    }

    private void Kill()
    {
        //player.PlayExplosion();
        GameObject anim = Instantiate(deathAnim) as GameObject;
        anim.transform.position = transform.position;

        


        Destroy(gameObject);
    }
}
