using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region ========���ʹ� ���� ���==========

// 0. ��    �� -> e_Mush
// 1. ��    �� -> e_Ghost
// 2. ��    �� -> e_Fox
// 3. �� ��ź1 -> e_Ranger
// 4. �� ��ź2 -> e_MovingRanger
// 5. ��    ź -> e_Bomb
// 6. ����ġ -> e_Gosum
// 7.    ��    -> e_Bird

// ���� -> �� HP, �ǰ� ������
// ���� -> �÷��̾ �ε����� ��, �˹�

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
