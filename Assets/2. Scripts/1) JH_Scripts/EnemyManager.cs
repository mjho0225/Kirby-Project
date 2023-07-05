using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region ========¿¡³Ê¹Ì °ü¸® ¸ñ·Ï==========

// 0. ¹ö    ¼¸ -> e_Mush
// 1. À¯    ·É -> e_Ghost
// 2. ¿©    ¿ì -> e_Fox
// 3. ÃÑ Æ÷Åº1 -> e_Ranger
// 4. ÃÑ Æ÷Åº2 -> e_MovingRanger
// 5. Æø    Åº -> e_Bomb
// 6. °í½¿µµÄ¡ -> e_Gosum
// 7.    »õ    -> e_Bird

// °³º° -> ÃÑ HP, ÇÇ°Ý µ¥¹ÌÁö
// °øÅë -> ÇÃ·¹ÀÌ¾î¿¡ ºÎµúÇûÀ» ¶§, ³Ë¹é

#endregion


public class EnemyManager : MonoBehaviour
{

    enum Enemy
    {
        e_Mush,
        e_Ghost,
        e_Fox,
        e_Ranger,
        e_MovingRanger,
        e_Bomb,
        e_Gosum,
        e_Bird,
    }

    Enemy enemy;


    void Start()
    {
        
    }


    void Update()
    {
        switch (enemy)
        {
            case Enemy.e_Mush:          break;
            case Enemy.e_Ghost:         break;
            case Enemy.e_Fox:           break;
            case Enemy.e_Ranger:        break;
            case Enemy.e_MovingRanger:  break;
            case Enemy.e_Bomb:          break;
            case Enemy.e_Gosum:         break;
            case Enemy.e_Bird:          break;
        }






    }






}
