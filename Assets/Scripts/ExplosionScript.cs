using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour
{
    public Sprite[] sprites;

    private bool animActive;

    public float animTime = 1.0f;
    private float timePerSprite;
    private float currAnimTime;

    private int currSpriteIter = 0;

	// Use this for initialization
	void Start ()
    {
        animActive = true;

        timePerSprite = animTime / sprites.Length;
        currAnimTime = timePerSprite;
	}
	
	// Update is called once per frame
	void Update ()
    {
        currAnimTime -= Time.deltaTime;

        if(currAnimTime <= 0)
        {
            currSpriteIter++;

            if (currSpriteIter != sprites.Length)
            {
                transform.GetComponent<SpriteRenderer>().sprite = sprites[currSpriteIter];

                currAnimTime = timePerSprite;
            }
            
            else
            {
                Destroy(gameObject);
            }
        }
	}
}
