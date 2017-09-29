using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject buildings;
    public GameObject upgrades;
    public GameObject newUpgrades;
    public GameObject purchasedUpgrades;

    public void OpenBuildings()
    {
        buildings.SetActive(true);
    }

    public void CloseBuildings()
    {
        buildings.SetActive(false);
    }

    public void OpenUpgrades()
    {
        upgrades.transform.localPosition = new Vector3(0, upgrades.transform.localPosition.y, 0);
        OpenNewUpgrades();
    }

    public void CloseUpgrades()
    {
        upgrades.transform.localPosition = new Vector3(999, upgrades.transform.localPosition.y, 0);
    }

    public void OpenNewUpgrades()
    {
        newUpgrades.transform.localPosition = new Vector3(0, 0, 0);
        purchasedUpgrades.transform.localPosition = new Vector3(999, 0, 0);
    }

    public void OpenPurchasedUpgrades()
    {
        newUpgrades.transform.localPosition = new Vector3(999, 0, 0);
        purchasedUpgrades.transform.localPosition = new Vector3(0, 0, 0);
    }
}
