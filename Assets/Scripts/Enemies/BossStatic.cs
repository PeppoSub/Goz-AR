using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatic : MonoBehaviour
{
    private int energy;

    void Start()
    {
        //gameObject.GetComponent<Damaged>().maxHealth = GameStatus.bossEnergy;
        //Debug.Log("GameStatus.bossEnergy = " + GameStatus.bossEnergy);
    }

    void Update()
    {
        Transform target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        transform.LookAt(target);
    }

    void OnDestroy()
    {
        GameStatus.bossKill = true;
    }

}
