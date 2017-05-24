using UnityEngine;
using System.Collections;

public class ScoreSystem : MonoBehaviour
{
    DirectorSystem directorRef;

    private int totalScore = 0;

    public int seekerScore = 25;
    public int wandererScore = 40;

    float currentMod;

    SpriteRenderer ones;
    SpriteRenderer tens;
    SpriteRenderer hundreds;
    SpriteRenderer thousands;
    SpriteRenderer tenThousands;
    SpriteRenderer hundredThousands;
    SpriteRenderer millions;

    SpriteRenderer[] digitRenList;
    public int[] digits;

    public Sprite s0;
    public Sprite s1;
    public Sprite s2;
    public Sprite s3;
    public Sprite s4;
    public Sprite s5;
    public Sprite s6;
    public Sprite s7;
    public Sprite s8;
    public Sprite s9;

    public int currentRound = 1;
    public int scoreToRound2 = 200;
    public int scoreToRound3 = 800;


    // Use this for initialization
    void Start ()
    {
        directorRef = GameObject.Find("Director").GetComponent<DirectorSystem>();

        digits = new int[7];
        digitRenList = new SpriteRenderer[7];

        ones = transform.FindChild("Ones").GetComponent<SpriteRenderer>();
        tens = transform.FindChild("Tens").GetComponent<SpriteRenderer>();
        hundreds = transform.FindChild("Hundreds").GetComponent<SpriteRenderer>();
        thousands = transform.FindChild("Thousands").GetComponent<SpriteRenderer>();
        tenThousands = transform.FindChild("TenThousands").GetComponent<SpriteRenderer>();
        hundredThousands = transform.FindChild("HundredThousands").GetComponent<SpriteRenderer>();
        millions = transform.FindChild("Millions").GetComponent<SpriteRenderer>();

        digitRenList[6] = (ones);
        digitRenList[5] = (tens);
        digitRenList[4] = (hundreds);
        digitRenList[3] = (thousands);
        digitRenList[2] = (tenThousands);
        digitRenList[1] = (hundredThousands);
        digitRenList[0] = (millions);

        for (int i = 0; i < 7; i++)
        {
            digits[i] = 0;
        }

    }

    // Update is called once per frame
    void Update ()
    {
        currentMod = directorRef.GetPointModifier();

        if(currentRound == 1 && totalScore >= scoreToRound2)
        {
            currentRound++;
            directorRef.Reset();
        }

        else if(currentRound == 2 && totalScore >= scoreToRound3)
        {
            currentRound++;
            directorRef.Reset();
        }

        else
        {
            //defualt
        }
	}

    public void WandererKill()
    {
        int scoreToAdd = (int)((float)wandererScore * currentMod);

        totalScore += scoreToAdd;

        if (totalScore >= 9999999)
            totalScore = 9999999;
               
        UpdateScoreVisual();
    }

    public void SeekerKill()
    {
        int scoreToAdd = (int)((float)seekerScore * currentMod);

        totalScore += scoreToAdd;

        if (totalScore >= 9999999)
            totalScore = 9999999;

        UpdateScoreVisual();
    }

    private void UpdateScoreVisual()
    {
        string scoreString = totalScore.ToString();

        int renIterSize = 6;
        int renIter;

        //Debug.Log(totalScore);
        //Debug.Log(scoreString.Length);

        for (int i = 0; i < scoreString.Length; i++)
        {
            renIter = (i + 1) + (renIterSize - scoreString.Length);

            if (ToInt(scoreString[i]) == 0)
            {
                digitRenList[renIter].sprite = s0;
            }

            if (ToInt(scoreString[i]) == 1)
            {
                digitRenList[renIter].sprite = s1;
            }

            if (ToInt(scoreString[i]) == 2)
            {
                digitRenList[renIter].sprite = s2;
            }

            if (ToInt(scoreString[i]) == 3)
            {
                digitRenList[renIter].sprite = s3;
            }

            if (ToInt(scoreString[i]) == 4)
            {
                digitRenList[renIter].sprite = s4;
            }

            if (ToInt(scoreString[i]) == 5)
            {
                digitRenList[renIter].sprite = s5;
            }

            if (ToInt(scoreString[i]) == 6)
            {
                digitRenList[renIter].sprite = s6;
            }

            if (ToInt(scoreString[i]) == 7)
            {
                digitRenList[renIter].sprite = s7;
            }

            if (ToInt(scoreString[i]) == 8)
            {
                digitRenList[renIter].sprite = s8;
            }

            if (ToInt(scoreString[i]) == 9)
            {
                digitRenList[renIter].sprite = s8;
            }

        }

    }

    private int ToInt(char input)
    {
        int ret;

        ret = int.Parse(input.ToString());

        return ret;
    }
}
