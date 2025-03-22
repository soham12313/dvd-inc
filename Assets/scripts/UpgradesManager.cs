using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Upgrade
{
    public int count;
    public int maxCount;
    public float baseCost;
    public float currentCost;
    public Action<GameManager> effect;
    public Action<float> increaseCost;

    public Upgrade(int count, int maxCount, float baseCost, Action<GameManager> effect, Action<float> increaseCost)
    {
        this.count = count;
        this.maxCount = maxCount;
        this.baseCost = baseCost;
        this.currentCost = this.baseCost;
        this.effect = effect;
        this.increaseCost = increaseCost;
    }

    public void IncrementCount()
    {
        this.count++;
    }

    public void SetCost(double newCost)
    {
        this.currentCost = (float) Math.Round(newCost, 2);
    }

    public bool IsMax()
    {
        return this.count >= maxCount;
    }
}

public class UpgradesManager : MonoBehaviour
{
    public Dictionary<string, Upgrade> upgradeMap = new();

    void Start()
    {
        InitializeNormalUpgrades();
    }

    public void InitializeNormalUpgrades()
    {
        upgradeMap[Constants.SPEED_INCREASE_NAME] = new Upgrade(0, 10, Constants.SPEED_BASE_COST, (gameManager) => { gameManager.IncreaseSpeed(gameManager.GetDvdSpeed() * Constants.SPEED_INCREASE); }, (baseCost) => { upgradeMap[Constants.SPEED_INCREASE_NAME].SetCost(baseCost * Math.Pow(1.15, upgradeMap[Constants.SPEED_INCREASE_NAME].count)); });
        upgradeMap[Constants.POINTS_INCREASE_NAME] = new Upgrade(0, 5, Constants.POINTS_BASE_COST, (gameManager) => { gameManager.IncreasePointsEarner(gameManager.GetPointsEarner() * Constants.POINTS_INCREASE); }, (baseCost) => { upgradeMap[Constants.POINTS_INCREASE_NAME].SetCost(baseCost * Math.Pow(2, upgradeMap[Constants.POINTS_INCREASE_NAME].count)); });
        upgradeMap[Constants.DVD_COUNT_NAME] = new Upgrade(1, 3, Constants.DVD_BASE_COST, (gameManager) => { gameManager.SpawnDvd(); }, (baseCost) => { upgradeMap[Constants.DVD_COUNT_NAME].SetCost(baseCost * this.Factorial(upgradeMap[Constants.DVD_COUNT_NAME].count)); });
        upgradeMap[Constants.CORNER_SCALE_NAME] = new Upgrade(0, 10, Constants.CORNER_SCALE_BASE_COST, (gameManager) => { gameManager.IncreaseCornerScale(gameManager.GetCornerScale() * Constants.CORNER_SCALE_INCREASE); }, (baseCost) => { upgradeMap[Constants.CORNER_SCALE_NAME].SetCost(baseCost * Math.Pow(1.5, upgradeMap[Constants.CORNER_SCALE_NAME].count)); });
        upgradeMap[Constants.COMBO_MULTIPLIER_NAME] = new Upgrade(0, 5, Constants.COMBO_MULTIPLIER_BASE_COST, (GameManager) => { GameManager.IncreaseComboMultiplier(GameManager.GetComboMultiplier() * Constants.COMBO_MULTIPLIER_INCREASE); }, (baseCost) => { upgradeMap[Constants.COMBO_MULTIPLIER_NAME].SetCost(baseCost * Math.Pow(1.5, upgradeMap[Constants.COMBO_MULTIPLIER_NAME].count)); });
        upgradeMap[Constants.COMBO_DURATION_NAME] = new Upgrade(0, 10, Constants.COMBO_DURATION_BASE_COST, (GameManager) => { GameManager.IncreaseComboDuration(GameManager.GetComboDuration() * Constants.COMBO_DURATION_INCREASE); }, (baseCost) => { upgradeMap[Constants.COMBO_DURATION_NAME].SetCost(baseCost * Math.Pow(1.5, upgradeMap[Constants.COMBO_DURATION_NAME].count)); });
    }

    public int GetUpgradeCount(string upgrade)
    {
        return upgradeMap[upgrade].count;
    }

    public float GetUpgradeCost(string upgrade)
    {
        return upgradeMap[upgrade].currentCost;
    }

    public void RunUpgradeEffect(string upgrade, GameManager gm)
    {
        upgradeMap[upgrade].effect.Invoke(gm);
    }

    public long Factorial(int n)
    {
        long result = 1;
        for (int i = 2; i <= n; i++)
        {
            result *= i;
        }
        return result;
    }
}
