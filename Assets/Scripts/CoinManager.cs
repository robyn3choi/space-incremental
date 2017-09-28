using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour {
    public static CoinManager inst = null;
    public int cps;
    public int cpsWithClicks;
    public float coins;
    public Text cpsText;
    public Text coinsText;

    float counter = 0;
    float updateTime = 0.3f;

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

    // Use this for initialization
    void Start () {
        coinsText.text = "coins: " + Mathf.Floor(coins).ToString();
        cpsText.text = "cps: " + cps.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		if (counter < updateTime)
        {
            counter += Time.deltaTime;
        }
        else
        {
            coins += (float) cps * updateTime;
            coinsText.text = "coins: " + Mathf.Floor(coins).ToString();
            counter = 0;
        }
	}

    public void CoinTap()
    {
        coins++;
        coinsText.text = "coins: " + Mathf.Floor(coins).ToString();
    }

    public void AddCps(int add)
    {
        cps += add;
        cpsText.text = "cps: " + cps.ToString();
    }

    public void SubtractCoins(int cost)
    {
        coins -= cost;
        coinsText.text = "coins: " + Mathf.Floor(coins).ToString();
    }
}
