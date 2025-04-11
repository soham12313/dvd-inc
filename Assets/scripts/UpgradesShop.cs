using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UpgradesShop : MonoBehaviour
{
    [SerializeField] private GameObject toolTip;
    [SerializeField] private UpgradesManager upgradesManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private bool isRebirth;

    private void OnEnable()
    {
        this.UpdateButtonColors();
        this.UpdateButtonsCost();
    }

    public void PurchaseUpgrade(string upgradeName)
    {
        float points = this.isRebirth ? this.gameManager.GetRebirthPoints() : this.gameManager.GetPoints();
        if (points < this.upgradesManager.upgradeMap[upgradeName].currentCost || this.upgradesManager.upgradeMap[upgradeName].IsMax())
            return;

        if (this.isRebirth)
        {
            gameManager.RemoveRebirthPoints(this.upgradesManager.upgradeMap[upgradeName].currentCost);
        }
        else
        {
            gameManager.RemovePoints(this.upgradesManager.upgradeMap[upgradeName].currentCost);
        }

        this.upgradesManager.upgradeMap[upgradeName].IncrementCount();
        this.upgradesManager.RunUpgradeEffect(upgradeName, this.gameManager);
        this.upgradesManager.upgradeMap[upgradeName].increaseCost(this.upgradesManager.upgradeMap[upgradeName].baseCost);

        this.UpdateButtonColors();
        this.UpdateButtonsCost();

        if (this.toolTip.activeSelf)
        {
            this.toolTip.GetComponent<ToolTipController>().SetCountText(this.upgradesManager.upgradeMap[upgradeName].count + "/" + this.upgradesManager.upgradeMap[upgradeName].maxCount);
        }
    }

    public void UpdateButtonColors()
    {
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("UpgradeButton");
        float points = this.isRebirth ? this.gameManager.GetRebirthPoints() : this.gameManager.GetPoints();

        for (int i = 0; i < buttons.Length; i++)
        {
            if (this.upgradesManager.upgradeMap[buttons[i].name].IsMax())
            {
                buttons[i].GetComponent<Image>().color = Color.gray;
            }
            else
            {
                buttons[i].GetComponent<Image>().color = this.upgradesManager.upgradeMap[buttons[i].name].currentCost > points ? Color.red : Color.green;
            }
        }
    }

    public void UpdateButtonsCost()
    {
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("UpgradeButton");
        GameObject[] costText = GameObject.FindGameObjectsWithTag("UpgradeCost");
        string pointsType = this.isRebirth ? " RP" : " Points";

        for (int i = 0; i < buttons.Length; i++)
        {
            costText[i].GetComponent<TextMeshProUGUI>().text = this.upgradesManager.upgradeMap[buttons[i].name].currentCost + pointsType;
        }
    }

    public void OnUpgradeHover(string upgradeName)
    {
        this.toolTip.SetActive(true);
        this.toolTip.GetComponent<ToolTipController>().SetTitleText(upgradeName);
        this.toolTip.GetComponent<ToolTipController>().SetDescriptionText(Constants.UPGRADES_DESCRIPTIONS[upgradeName]);
        this.toolTip.GetComponent<ToolTipController>().SetCountText(this.upgradesManager.upgradeMap[upgradeName].count + "/" + this.upgradesManager.upgradeMap[upgradeName].maxCount);
    }

    public void OnUpgradeExit()
    {
        this.toolTip.SetActive(false);
    }

    public void onRebirthComplete()
    {
        this.gameObject.SetActive(false);
    }
}
