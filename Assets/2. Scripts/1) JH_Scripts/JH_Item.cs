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
                //IAmFood();
                break;
            case WHO.item_Waii:
                //IAmWaii();
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

        JH_GameManager.instance.SavePanel.SetActive(true);
        Time.timeScale = 0;
        JH_GameManager.instance.sceneTime = 0;
        JH_GameManager.instance.savePanel_On = true;
    }

    private void IAmFood()
    {



    }


    private void IAmCoin()
    {


        if (scoreCount == CountScore.score1)
        {
            JH_ScoreManager.instance.COIN_SCORE++;
        }
        else if(scoreCount == CountScore.score5)
        {
            JH_ScoreManager.instance.COIN_SCORE += 5;
        }
        else if (scoreCount == CountScore.score10)
        {
            JH_ScoreManager.instance.COIN_SCORE = JH_ScoreManager.instance.COIN_SCORE+10;
        }
    }



    //트리거 사용할지 or 콜리전 사용할지

    void OnTriggerEnter(Collider col)
    {
        if (me == "Waii")
        {
            iAm = WHO.item_Waii;
        }

        if (me == "Food")
        {
            iAm = WHO.item_Food;
        }

        if (me == "Coin")
        {
            iAm = WHO.item_Coin;
            if (this.gameObject.name.Contains("Coin1"))
            {
                scoreCount = CountScore.score1;
            }
            if (this.gameObject.name.Contains("Coin5"))
            {
                scoreCount = CountScore.score5;
            }
            if (this.gameObject.name.Contains("Coin10"))
            {
                scoreCount = CountScore.score10;
            }
        }

        // 플레이어가 부딪히면
        if (col.gameObject.tag == "Player")
        {

            if (iAm == WHO.item_Coin)
            {
                IAmCoin();

                //동전 획득 애니메이션
                Destroy(gameObject);
            }

            if (iAm == WHO.item_Waii)
            {
                IAmWaii();
            }
            if (iAm == WHO.item_Food)
            {
                IAmFood();
            }

        }


    }


    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (me == "Waii")
    //    {
    //        iAm = WHO.item_Waii;
    //    }

    //    if (me == "Food")
    //    {
    //        iAm = WHO.item_Food;
    //    }

    //    if (me == "Coin")
    //    {
    //        iAm = WHO.item_Coin;
    //        if (this.gameObject.name.Contains("Coin1"))
    //        {
    //            scoreCount = CountScore.score1;
    //        }
    //        if (this.gameObject.name.Contains("Coin5"))
    //        {
    //            scoreCount = CountScore.score5;
    //        }
    //        if (this.gameObject.name.Contains("Coin10"))
    //        {
    //            scoreCount = CountScore.score10;
    //        }
    //    }

    //    // 플레이어가 부딪히면
    //    if (collision.gameObject.tag == "Player")
    //    {

    //        if (iAm == WHO.item_Coin)
    //        {
    //            //동전 획득 애니메이션
    //            Destroy(gameObject);

    //            IAmCoin();
    //        }

    //        if (iAm == WHO.item_Waii)
    //        {
    //            IAmWaii();
    //        }
    //        if (iAm == WHO.item_Food)
    //        {
    //            IAmFood();
    //        }
            
    //    }

        
    //}
   
}
