using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JH_Item : MonoBehaviour
{
    public GameObject coin_Yellow;
    public GameObject coin_Green;
    public GameObject coin_Red;

    public enum WHO
    {
        item_Coin,
        item_Food,
        item_Waii,
        item_Box,
        item_Flower,
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

    public enum Box
    {
        Star,
        Wood1,
        Wood2,
        Iron1,
        Iron2,

    }
    public Box box;

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
            case WHO.item_Box:
                break;
            case WHO.item_Flower:
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

        switch (box)
        {
            case Box.Star:
                break;
            case Box.Wood1:
                break;
            case Box.Wood2:
                break;
            case Box.Iron1:
                break;
            case Box.Iron2:
                break;
        }

    }

    public bool flowerAnim = false;
    void IAmFlower()
    {
        flowerAnim = true;
        JH_ScoreManager.instance.COIN_SCORE++;

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


        // 플레이어 HP 회복
        PlayerHP.instance.HP += 2;

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

    private void IAmBox()
    {
        if(box == Box.Wood1)
        {
            // 1점 짜리 코인 드랍

            //테스트
            JH_ScoreManager.instance.COIN_SCORE++;
        }
        if (box == Box.Wood2)
        {
            // 5점 짜리 코인 드랍

            //테스트
            JH_ScoreManager.instance.COIN_SCORE += 5;
        }
        if (box == Box.Iron1)
        {
            // 5점 짜리 코인 드랍
        }
        if (box == Box.Iron2)
        {
            // 10점 짜리 코인 드랍


        }
        if (box == Box.Star)
        {
            // 빈박스

        }
    }



    //트리거 사용할지 or 콜리전 사용할지

    //void OnTriggerEnter(Collider col)
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
    //    if (me == "Box")
    //    {
    //        iAm = WHO.item_Box;
    //        if (this.gameObject.name.Contains("Wood1"))
    //        {
    //            box = Box.Wood1;
    //        }
    //        if (this.gameObject.name.Contains("Wood2"))
    //        {
    //            box = Box.Wood2;
    //        }
    //        if (this.gameObject.name.Contains("Iron1"))
    //        {
    //            box = Box.Iron1;
    //        }
    //        if (this.gameObject.name.Contains("Iron2"))
    //        {
    //            box = Box.Iron2;
    //        }
    //        if (this.gameObject.name.Contains("Star"))
    //        {
    //            box = Box.Star;
    //        }
            
    //    }
    //    if (me == "Flower")
    //    { 

    //    }

    //        // 플레이어가 부딪히면
    //        if (col.gameObject.tag == "Player")
    //    {

    //        if (iAm == WHO.item_Coin)
    //        {
    //            IAmCoin();

    //            //동전 획득 애니메이션
    //            Destroy(gameObject);
    //        }

    //        if (iAm == WHO.item_Waii)
    //        {
    //            IAmWaii();
    //        }
    //        if (iAm == WHO.item_Food)
    //        {
    //            IAmFood();
    //        }

    //        if(iAm == WHO.item_Box)
    //        {
    //            IAmBox();
    //        }
    //    }


    //}


    private void OnCollisionEnter(Collision col)
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
        if (me == "Box")
        {
            iAm = WHO.item_Box;
            if (this.gameObject.name.Contains("Wood1"))
            {
                box = Box.Wood1;
            }
            if (this.gameObject.name.Contains("Wood2"))
            {
                box = Box.Wood2;
            }
            if (this.gameObject.name.Contains("Iron1"))
            {
                box = Box.Iron1;
            }
            if (this.gameObject.name.Contains("Iron2"))
            {
                box = Box.Iron2;
            }
            else if (this.gameObject.name.Contains("Star"))
            {
                box = Box.Star;
            }

        }
        if (me == "Flower")
        {
            iAm = WHO.item_Flower;

        }

        // 플레이어가 부딪히면
        if (col.gameObject.tag == "Player" )
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
                Destroy(gameObject);
            }
            if (iAm == WHO.item_Food)
            {
                IAmFood();
                this.gameObject.transform.Translate(Vector3.up * 10 * Time.deltaTime) ;
                this.gameObject.GetComponent<Collider>().enabled = false;
                Destroy(gameObject,1);
            }

            if (iAm == WHO.item_Box)
            {
                //IAmBox();

            }

            if (iAm == WHO.item_Flower)
            {
                IAmFlower();
                this.gameObject.GetComponent<Collider>().enabled = false;
                Instantiate(coin_Yellow, transform.position, Quaternion.identity);
                //Destroy(this.gameObject);
            }
        }

        if(col.gameObject.tag == "bullet" || ((col.gameObject.layer == LayerMask.NameToLayer("Car")) && CarController.instance.carState == CarController.CarState.Dash) || col.gameObject.tag == "bullet2" || col.gameObject.tag == "bubble")
        {
            if (iAm == WHO.item_Box )
            {
                IAmBox();
                Destroy(this.gameObject);
            }
        }

    }

}
