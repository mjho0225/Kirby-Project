using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    int hp;
    public int maxHP = 10;
    public Slider sliderHP;

    // Start is called before the first frame update
    void Start()
    {
        sliderHP.maxValue = maxHP;
        HP = maxHP;
    }

    public int HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            sliderHP.value = hp;
        }
    }


    public void SetHP(int value)
    {
        hp = value;
        sliderHP.value = hp;
    }

    public int GetHP()
    {
        return hp;
    }

}
