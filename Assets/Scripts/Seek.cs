using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour
{

    public GameObject deathAnim;


    private Quaternion startingRot;
    public float speed = 4.0f;

    private Transform target;
   
    private bool isInGameField = false;

    ScoreSystem scoreSystem;

    public float xRangeMin;
    public float xRangeMax;
    public float yRangeMin;
    public float yRangeMax;

    AudioSource audio;
    PlayerScript player;

    public AudioClip explosion;

    // Use this for initialization
    void Start ()
    {
        scoreSystem = GameObject.Find("Score").GetComponent<ScoreSystem>();

        startingRot = transform.rotation;
        target = GameObject.Find("Player").transform;

        audio = GetComponent<AudioSource>();

        player = GameObject.Find("Player").GetComponent<PlayerScript>();

    }

    // Update is called once per frame
    void Update ()
    {
        if (!isInGameField)
        {
            if ((transform.position.x >= xRangeMin && transform.position.x <= xRangeMax) &&
                (transform.position.y >= yRangeMin && transform.position.y <= yRangeMax))
            {
                isInGameField = true;
            }
        }

        transform.Rotate(Vector3.forward * 2);

        if (Vector3.Distance(transform.position, target.position) > 1.0f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
        }
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(isInGameField)
        {
            if (coll.gameObject.tag == "PlayersBullet")
            {
                scoreSystem.SeekerKill();
                Kill();
            }

            if (coll.gameObject.tag == "Meteor")
            {
                scoreSystem.WandererKill();
                Kill();
            }
        }
    }

    private void Kill()
    {
        player.PlayExplosion();

        GameObject anim = Instantiate(deathAnim) as GameObject;
        anim.transform.position = transform.position;



        Destroy(gameObject);
    }
}
