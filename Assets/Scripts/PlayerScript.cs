using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

	// Use this for initialization

    public float invulnTime = 3.0f;

    private Vector3 startingLocation;
    private Quaternion startingRot;

    private bool isInvuln = false;
    private bool isHit = false;
    private bool permaInvuln = false;

    private int timesHit = 0;

    private int livesLeft = 3;

    private Renderer damRen1;
    private Renderer damRen2;
    private Renderer damRen3;

    private Renderer lifeRen1;
    private Renderer lifeRen2;
    private Renderer lifeRen3;

    private Renderer playerRen;
    private Renderer shieldRen;


    DirectorSystem director;
    AudioSource audio;

    public AudioClip explosion;




    void Start () 
    {
        playerRen = this.gameObject.GetComponent<Renderer>();
        damRen1 = this.gameObject.transform.GetChild(0).GetComponent<Renderer>();
        damRen2 = this.gameObject.transform.GetChild(1).GetComponent<Renderer>();
        damRen3 = this.gameObject.transform.GetChild(2).GetComponent<Renderer>();
        lifeRen1 = GameObject.Find("Lives/Life1").GetComponent<Renderer>();
        lifeRen2 = GameObject.Find("Lives/Life2").GetComponent<Renderer>();
        lifeRen3 = GameObject.Find("Lives/Life3").GetComponent<Renderer>();
        shieldRen = this.gameObject.transform.GetChild(3).GetComponent<Renderer>();

        audio = GetComponent<AudioSource>();

        director = GameObject.Find("Director").GetComponent<DirectorSystem>();

        startingLocation = transform.position;
        startingRot = transform.rotation;

        lifeRen1.enabled = true;
        lifeRen2.enabled = true;
        lifeRen3.enabled = true;
        shieldRen.enabled = false;
        gameObject.tag = "PlayersShip";
	}

    public void PlayExplosion()
    {
        audio.PlayOneShot(explosion, 0.2F);

    }

    public void Reset()
    {
        invulnTime = 3.0f;
        isInvuln = false;
        isHit = false;
        permaInvuln = false;

        timesHit = 0;

        if(livesLeft == 0)
        {
            livesLeft = 3;
            lifeRen1.enabled = true;
            lifeRen2.enabled = true;
            lifeRen3.enabled = true;
        }

        damRen1.enabled = false;
        damRen2.enabled = false;
        damRen3.enabled = false;

        transform.rotation = startingRot;
        transform.position = startingLocation;

        GameObject[] toDel;
        toDel = GameObject.FindGameObjectsWithTag("PlayersBullet");

        for(int i = 0; i < toDel.Length; i++)
        {
            Destroy(toDel[i]);
        }
    }

    public int GetLivesLeft()
    {
        return livesLeft;
    }

    void ShowDamage()
    {
        if(timesHit == 1)
        {
            if(!damRen1.enabled)
                damRen1.enabled = true;
        }

        else if( timesHit == 2)
        {
            if(!damRen2.enabled)
            {
                damRen1.enabled = false;
                damRen2.enabled = true;
            }
        }

        else
        {
            if(!damRen3.enabled)
            {
                damRen2.enabled = false;
                damRen3.enabled = true;
            }
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
        DebugControls();

	    if(isHit)
        {
            isInvuln = true;
            isHit = false;
            timesHit++;
            director.AdjustStress(StressAdjustment.DECREASE_STRESS_A_MODERATE);

            if(timesHit > 3)
            {
                isHit = false;
                isInvuln = false;
                //director.PlayerDeath();
                director.AdjustStress(StressAdjustment.DECREASE_STRESS_A_LOT);

                Kill();
            }

            else
            {
                //director.PlayerHit();
                ShowDamage();
            }
        }

        if(isInvuln)
        {
            if (!shieldRen.enabled)
                shieldRen.enabled = true;

            invulnTime -= Time.deltaTime;
        }

        if(invulnTime <= 0)
        {
            invulnTime = 3.0f;
            isHit = false;
            isInvuln = false;

            if (shieldRen)
                shieldRen.enabled = false;
                
        }
	}

    void Kill()
    {
        bool shouldRespawn = true;

        audio.PlayOneShot(explosion, 0.2F);

        livesLeft--;

        switch(livesLeft)
        {
            case 2:
                lifeRen3.enabled = false;
                playerRen.enabled = false;
                break;
            case 1:
                lifeRen2.enabled = false;
                playerRen.enabled = false;
                break;
            case 0:
                lifeRen1.enabled = false;
                shouldRespawn = false;
                Destroy(gameObject);
                SceneManager.LoadScene(0);
                break;

            default:
                break;

        }

        if(shouldRespawn)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = startingLocation;
        transform.rotation = startingRot;
        damRen1.enabled = false;
        damRen2.enabled = false;
        damRen3.enabled = false;
        timesHit = 0;
        invulnTime = 3.0f;
        isHit = false;
        isInvuln = true;
        playerRen.enabled = true;

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(!permaInvuln)
        {
            if (coll.gameObject.tag == "WanderEnemy" || coll.gameObject.tag == "SeekerEnemy")
            {
                if (!isInvuln)
                    isHit = true;
            }
        }
        
    }

    void DebugControls()
    {
        if (Input.GetKey(KeyCode.R))
        {
            if (!permaInvuln)
            {
                permaInvuln = true;
                shieldRen.enabled = true;
            }

            else
            {
                permaInvuln = false;
                shieldRen.enabled = true;
            }
        }
    }
}
