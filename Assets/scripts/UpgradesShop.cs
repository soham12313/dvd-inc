using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UpgradesShop : MonoBehaviour
{
    private UpgradesManager upgradesManager;
    private GameManager gameManager;

    private void OnEnable()
    {
        upgradesManager = GameObject.FindObjectOfType<UpgradesManager>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        UpdateButtonColors();
        UpdateButtonsCost();
        UpdateUpgradesCountText();
    }

    public void PurchaseUpgrade(string upgradeName)
    {
        if (gameManager.GetPoints() < upgradesManager.upgradeMap[upgradeName].currentCost || upgradesManager.upgradeMap[upgradeName].IsMax())
            return;

        gameManager.RemovePoints(upgradesManager.upgradeMap[upgradeName].currentCost);
        upgradesManager.upgradeMap[upgradeName].IncrementCount();
        upgradesManager.RunUpgradeEffect(upgradeName, gameManager);
        upgradesManager.upgradeMap[upgradeName].increaseCost(upgradesManager.upgradeMap[upgradeName].baseCost);

        UpdateButtonColors();
        UpdateButtonsCost();
        UpdateUpgradesCountText();
    }

    public void UpdateButtonColors()
    {
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("UpgradeButton");

        for (int i = 0; i < buttons.Length; i++)
        {
            if (upgradesManager.upgradeMap[buttons[i].name].IsMax())
            {
                buttons[i].GetComponent<Image>().color = Color.gray;
            }
            else
            {
                buttons[i].GetComponent<Image>().color = upgradesManager.upgradeMap[buttons[i].name].currentCost > gameManager.GetPoints() ? Color.red : Color.green;
            }
        }
    }

    public void UpdateButtonsCost()
    {
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("UpgradeButton");
        GameObject[] costText = GameObject.FindGameObjectsWithTag("UpgradeCost");

        for (int i = 0; i < buttons.Length; i++)
        {
            costText[i].GetComponent<TextMeshProUGUI>().text = upgradesManager.upgradeMap[buttons[i].name].currentCost + " points";
        }
    }

    public void UpdateUpgradesCountText()
    {
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("UpgradeButton");
        GameObject[] countText = GameObject.FindGameObjectsWithTag("UpgradeCount");

        for (int i = 0; i < buttons.Length; i++)
        {
            countText[i].GetComponent<TextMeshProUGUI>().text = upgradesManager.upgradeMap[buttons[i].name].count + "/" + upgradesManager.upgradeMap[buttons[i].name].maxCount;
        }
    }
}
