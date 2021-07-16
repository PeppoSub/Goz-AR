using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatic : MonoBehaviour
{
    public int energy;
    public int maxEnergy;

    void Start()
    {
        energy = this.gameObject.GetComponent<Damaged>().maxHealth;

        GameStatus.ReloadDifficultySettings();
        if (GameStatus.missionGoal > 0)
        {
            float multiplier = Mathf.Pow(3, GameStatus.missionGoal - 2);  // see GameStatus about multiplier values
            energy = (int)(energy * multiplier);
            this.gameObject.GetComponent<Damaged>().ResetHealth(energy);
        }
        maxEnergy = energy;

        Debug.Log("energy = " + energy);
    }

    void Update()
    {
        Transform target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        transform.LookAt(target);

        energy = this.gameObject.GetComponent<Damaged>().CurrentHealth();

        GameStatus.BossHealth(energy, maxEnergy);
    }

    void OnDestroy()
    {
        GameStatus.bossKill = true;
    }

}
