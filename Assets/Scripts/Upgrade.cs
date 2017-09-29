using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour {

    bool purchased = false;
    public bool canAfford;
    public int price = 100; // temporary
    public Building building; // the building that this upgrade affects
    public int multiplier;
    string description;
    Button button;
    public Text priceText;
    public Text descriptionText;

    public static Transform newUpgradesParent;
    public static Transform purchasedUpgradesParent;

    public void InitVariables()
    {
        if (newUpgradesParent == null)
        {
            newUpgradesParent = GameObject.Find("NewUpgradesContent").transform;
        }
        if (purchasedUpgradesParent == null)
        {
            purchasedUpgradesParent = GameObject.Find("PurchasedUpgradesContent").transform;
        }
        transform.SetParent(newUpgradesParent, false);
        button = GetComponent<Button>();
        print(building + "button");
        description = multiplier + "X production for Building" + building.index;
        descriptionText.text = description;
        priceText.text = price.ToString();
    }

    // Update is called once per frame
    void Update () {
        if (purchased) { return; }
        if (CoinManager.inst.coins < price)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }

    public void Buy()
    {
        transform.SetParent(purchasedUpgradesParent, false);
        building.ApplyUpgrade(multiplier);
        purchased = true;
        button.interactable = false;
    }
}
