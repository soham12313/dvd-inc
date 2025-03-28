using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UpgradesShop : MonoBehaviour
{
    [SerializeField] private GameObject toolTip;
    private UpgradesManager upgradesManager;
    private GameManager gameManager;

    private void OnEnable()
    {
        upgradesManager = GameObject.FindObjectOfType<UpgradesManager>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        UpdateButtonColors();
        UpdateButtonsCost();
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

        if (this.toolTip.activeSelf)
        {
            this.toolTip.GetComponent<ToolTipController>().SetCountText(upgradesManager.upgradeMap[upgradeName].count + "/" + upgradesManager.upgradeMap[upgradeName].maxCount);
        }
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

    public void OnUpgradeHover(string upgradeName)
    {
        this.toolTip.SetActive(true);
        this.toolTip.GetComponent<ToolTipController>().SetTitleText(upgradeName);
        this.toolTip.GetComponent<ToolTipController>().SetDescriptionText(Constants.UPGRADES_DESCRIPTIONS[upgradeName]);
        this.toolTip.GetComponent<ToolTipController>().SetCountText(upgradesManager.upgradeMap[upgradeName].count + "/" + upgradesManager.upgradeMap[upgradeName].maxCount);
    }

    public void OnUpgradeExit()
    {
        this.toolTip.SetActive(false);
    }
}
