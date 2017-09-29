using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundLife : MonoBehaviour {

    public GameObject options;
    int price = 25000;

    public void OpenOptions()
    {
        options.SetActive(true);
    }

    public void CloseOptions()
    {
        options.SetActive(false);
    }

    public void SelectOption(int choice)
    {
        CloseOptions();
        transform.SetParent(Upgrade.purchasedUpgradesParent, false);
        CoinManager.inst.SubtractCoins(price);

        if (choice == 1)
        {
            GameManager.inst.life = GameManager.Life.Alliance;
        }
        else if (choice == 2)
        {
            GameManager.inst.life = GameManager.Life.Slave;
        }
        else if (choice == 3)
        {
            GameManager.inst.life = GameManager.Life.Ignore;
        }

        Building newBuilding = Instantiate(Building.buildingPrefab, Building.buildingParent).GetComponent<Building>();
        newBuilding.index = 4;
        newBuilding.InitVariables();
        newBuilding.Unlock();
    }
}
