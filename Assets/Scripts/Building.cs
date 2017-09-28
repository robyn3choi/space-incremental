using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour {

    public bool canAfford;
    public int price;
    public int basePrice;
    public int type;
    public int cps;
    public int numOwned;
    public bool locked = true;
    public float percentageOfCps;

    public GameObject unlockedText;
    public GameObject lockedText;
    public Text typeText;
    public Text priceText;
    public Text cpsText;
    public Text numOwnedText;
    Button button;

    float multiplier = 1.1f;

    public GameObject _buildingPrefab;
    public GameObject _upgradePrefab;
    public static GameObject buildingPrefab;
    static GameObject upgradePrefab;
    static int[] buildingBasePrices = { 10, 50, 300, 1000, 5000, 24000, 100000, 1000000 };
    static int[] buildingCpss = { 1, 3, 10, 50, 150, 500, 2000, 10000 };
    public Building nextBuilding;
    public static Transform buildingParent;
    int lastNonResearchBuilding = 3;

    // Use this for initialization
    void Start () {
        InitVariables();
        if (type == 1)
        {
            buildingParent = transform.parent;
            Unlock();
        }
    }

    public void InitVariables()
    {
        if (buildingPrefab == null)
        {
            buildingPrefab = _buildingPrefab;
        }
        if (upgradePrefab == null)
        {
            upgradePrefab = _upgradePrefab;
        }
        gameObject.name = "building" + type;
        basePrice = buildingBasePrices[type - 1];
        price = basePrice;
        cps = buildingCpss[type - 1];
        cpsText.text = "CPS: " + cps.ToString();
        priceText.text = "Price: " + price.ToString();
        button = GetComponent<Button>();
        if (type > lastNonResearchBuilding) // research building
        {
            typeText.text = "Building" + type.ToString() + GameManager.inst.research;
        }
        else
        {
            typeText.text = "Building" + type.ToString();
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (CoinManager.inst.coins < price)
        {
          //  canAfford = false;
            button.interactable = false;
        }
        else
        {
          //  canAfford = true;
            button.interactable = true;
        }
	}

    public void Buy()
    {
        print(type);
        CoinManager.inst.SubtractCoins(price);
        CoinManager.inst.AddCps(cps);
        numOwned++;
        numOwnedText.text = "# owned: " + numOwned.ToString();
        // Price = BaseCost×Multiplier ^ (#Owned)
        price = (int) Mathf.Round(basePrice * Mathf.Pow(multiplier, numOwned));
        priceText.text = "Price: " + price.ToString();
        int upgradeMultiplier = DetermineUpgradeMultiplier();
        if (upgradeMultiplier != 0) { UnlockUpgrade(upgradeMultiplier); }
        
        // if this is building3, don't unlock next building because it doesn't exist
        // 25x of building3 unlocks research
        if (type == lastNonResearchBuilding)
        {
            if (numOwned == 25)
            {
                GameManager.inst.UnlockResearch();
            }
            return;
        }
        if (nextBuilding.locked) { nextBuilding.Unlock(); }
    }

    public void Unlock()
    {
        locked = false;
        lockedText.SetActive(false);
        unlockedText.SetActive(true);
        if (type != buildingBasePrices.Length && type != lastNonResearchBuilding)
        {
            RevealNextBuilding();
        }
    }

    public void Lock()
    {
        locked = true;
        lockedText.SetActive(true);
        unlockedText.SetActive(false);
    }

    public void RevealNextBuilding()
    {
        GameObject nextBuildingObject = Instantiate(buildingPrefab, buildingParent);
        nextBuilding = nextBuildingObject.GetComponent<Building>();
        nextBuilding.type = type + 1;
        nextBuilding.InitVariables();
        nextBuilding.Lock();
    }

    void UnlockUpgrade(int upgradeMultiplier)
    {
        GameObject unlockedUpgrade = Instantiate(upgradePrefab);
        Upgrade upgrade = unlockedUpgrade.GetComponent<Upgrade>();
        upgrade.building = this;
        upgrade.multiplier = upgradeMultiplier;
        upgrade.InitVariables();
    }

    public void ApplyUpgrade(int multiplier)
    {
        CoinManager.inst.AddCps(-cps * numOwned);
        cps *= multiplier;
        CoinManager.inst.AddCps(cps * numOwned);
        cpsText.text = "CPS: " + cps;
    }

    int DetermineUpgradeMultiplier()
    {
        if (numOwned < 100)
        {
            if (numOwned == 10 || numOwned == 25 || numOwned == 50)
            {
                return 2;
            }
        }
        else if (numOwned < 1000)
        {
            if (numOwned % 100 == 0)
            {
                return 3;
            }
        }
        else if (numOwned < 2000)
        {
            if (numOwned % 250 == 0)
            {
                return 4;
            }
        }
        return 0;
    }
}
