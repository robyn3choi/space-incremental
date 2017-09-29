using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager inst = null;
    public int specialization;

    public GameObject foundLifeContainer;

    public enum Life { Alliance, Slave, Ignore };
    public Life life;

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

    public void UnlockFoundLife()
    {
        Instantiate(foundLifeContainer, Upgrade.newUpgradesParent);
    }
}
