using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager inst = null;
    public string research;
    public int specialization;

    public GameObject researchContainer;

    //singleton
    void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
        else if (inst != this)
        {
            Destroy(gameObject);
        }
    }

    public void UnlockResearch()
    {
        Instantiate(researchContainer, Upgrade.newUpgradesParent);
    }
}
