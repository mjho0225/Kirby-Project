using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JH_Item : MonoBehaviour
{
    public enum WHO
    {
        item_Coin,
        item_Food,
        item_Waii,

    }

    public WHO iAm;
    string me;

    public enum CountScore
    {
        score1,
        score5,
        score10,
    }
    public CountScore scoreCount;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        me = this.gameObject.tag;

    }

    // Update is called once per frame
    void Update()
    {
        switch (iAm)
        {
            case WHO.item_Coin:
                //IAmCoin();
                break;
            case WHO.item_Food:
                IAmFood();
                break;
            case WHO.item_Waii:
                IAmWaii();
                break;
        }

        switch (scoreCount)
        {
            case CountScore.score1:
                break;
            case CountScore.score5:
                break;
            case CountScore.score10:
                break;
        }



    }

    private void IAmWaii()
    {



    }

    private void IAmFood()
    {



    }


    private void IAmCoin()
    {
        if(scoreCount == CountScore.score1)
        {
            JH_ScoreManager.instance.COIN_SCORE++;
        }
        else if(scoreCount == CountScore.score5)
        {
            JH_ScoreManager.instance.COIN_SCORE += 5;
        }
        else if (scoreCount == CountScore.score10)
        {
            JH_ScoreManager.instance.COIN_SCORE += 10 ;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (me == "Waii")
        {
            iAm = WHO.item_Waii;
        }
        if (me == "Food")
        {
            iAm = WHO.item_Food;
        }
        if (me == "Finish")
        {
            iAm = WHO.item_Coin;
            if (this.gameObject.name.Contains("CoinO"))
            {
                scoreCount = CountScore.score1;
            }
            if (this.gameObject.name.Contains("CoinF"))
            {
                scoreCount = CountScore.score5;
            }
            if (this.gameObject.name.Contains("CoinT"))
            {
                scoreCount = CountScore.score10;
            }
        }

        // 플레이어가 부딪히면
        if (collision.gameObject.tag == "Player")
        {

            if (iAm == WHO.item_Coin)
            {
                IAmCoin();
            }

            if (iAm == WHO.item_Waii)
            {

            }
            if (iAm == WHO.item_Food)
            {

            }
            
        }

    }

}
