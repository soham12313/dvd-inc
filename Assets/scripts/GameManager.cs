using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI pointsText;
    [SerializeField]
    private GameObject dvd;
    [SerializeField]
    private UpgradesManager upgradesManager;
    [SerializeField]
    private UpgradesShop upgradesShop;
    [SerializeField]
    private Slider comboSlider;
    [SerializeField]
    private TextMeshProUGUI comboText;

    public float dvdSpeed;
    public float points;
    public float pointEarnerWithoutCombo;
    public float pointEarner;
    public float cornerScale;
    public float currentPerfectStreak;
    public float perfectStreakMultiplier;
    public float timedComboMultiplier;
    public float timedComboDuration;
    public float timedComboCountdownTimer;
    private bool isTimedComboActive;

    // Start is called before the first frame update
    void Start()
    {
        upgradesManager.InitializeNormalUpgrades();

        this.dvdSpeed = Constants.BASE_DVD_SPEED;
        this.points = Constants.BASE_POINTS;
        this.pointEarner = Constants.BASE_POINT_GAIN;
        this.cornerScale = Constants.BASE_CORNER_SCALE;
        this.ResetCurrentPerfectStreak();
        this.perfectStreakMultiplier = Constants.BASE_PERFECT_STREAK_MULTIPLIER;
        this.timedComboMultiplier = Constants.BASE_TIMED_COMBO_MULTIPLER;
        this.timedComboDuration = Constants.BASE_TIMED_COMBO_DURATION;

        for (int i = 0; i < upgradesManager.GetUpgradeCount(Constants.DVD_COUNT_NAME); i++)
        {
            this.SpawnDvd();
        }
    }

    private void Update()
    {
        if (this.isTimedComboActive)
        {
            if (this.timedComboCountdownTimer < this.timedComboDuration)
            {
                this.timedComboCountdownTimer += Time.deltaTime;
                this.comboSlider.value = this.timedComboDuration - this.timedComboCountdownTimer;
            }
            else
            {
                this.EndCombo();
            }
        }
    }

    public float GetPoints()
    {
        return this.points;
    }

    public void AddPoints()
    {
        this.points += this.currentPerfectStreak >= Constants.PERFECT_STREAK_NEEDED ? this.pointEarner  * this.perfectStreakMultiplier : this.pointEarner;
        this.pointsText.text = this.points + "";

        if (this.upgradesShop.gameObject.activeSelf)
        {
            this.upgradesShop.UpdateButtonColors();
        }
    }

    public void RemovePoints(float removed)
    {
        this.points -= removed;
        this.pointsText.text = this.points + "";

        if (this.upgradesShop.gameObject.activeSelf)
        {
            this.upgradesShop.UpdateButtonColors();
        }
    }

    public void IncreaseSpeed(float amount)
    {
        DvdController[] dvds;
        
        this.dvdSpeed += amount;
        dvds = GameObject.FindObjectsOfType<DvdController>();

        for (int i = 0; i < dvds.Length; i++)
        {
            dvds[i].SetMovementSpeed(dvdSpeed);
        }
    }

    public float GetDvdSpeed()
    {
        return this.dvdSpeed;
    }

    public void IncreasePointsEarner(float amount)
    {
        if (this.isTimedComboActive)
        {
            this.pointEarnerWithoutCombo += amount;
            this.pointEarner = this.pointEarnerWithoutCombo * this.timedComboMultiplier;
        }
        else
        {
            this.pointEarner += amount;
        }
    }

    public float GetPointsEarner()
    {
        return this.isTimedComboActive ? this.pointEarnerWithoutCombo : this.pointEarner;
    }

    public void SpawnDvd()
    {
        Instantiate(this.dvd).transform.position = new Vector2(0, 0);
    }

    public void IncreaseCornerScale(float scale)
    {
        GameObject[] collidePoints;

        this.cornerScale += scale;
        collidePoints = GameObject.FindGameObjectsWithTag("CollidePoint");
        for (int i = 0; i < collidePoints.Length; i++)
        {
            collidePoints[i].transform.localScale = new Vector3((float)this.cornerScale, (float)this.cornerScale);
        }
    }

    public float GetCornerScale()
    {
        return this.cornerScale;
    }
    
    public void ResetCurrentPerfectStreak()
    {
        this.currentPerfectStreak = 0;
    }

    public void IncrementCurrentPerfectStreak()
    {
        this.currentPerfectStreak++;
    }

    public void IncreasePerfectStreakMultiplier(float perfectMulti)
    {
        this.perfectStreakMultiplier += (float) Math.Round(this.perfectStreakMultiplier * perfectMulti, 2);
    }

    public float GetPerfectStreakMultiplier()
    {
        return this.perfectStreakMultiplier;
    }

    public void IncreaseComboMultiplier(float comboMulti)
    {
        this.timedComboMultiplier += (float) Math.Round(this.timedComboMultiplier * comboMulti, 2);

        if (this.isTimedComboActive)
        {
            this.pointEarner = this.pointEarnerWithoutCombo * this.timedComboMultiplier;
            this.comboText.text = this.timedComboMultiplier + "x";
        }
    }

    public float GetComboMultiplier()
    {
        return this.timedComboMultiplier;
    }

    public void IncreaseComboDuration(float duration)
    {
        this.timedComboDuration += duration;
        this.comboSlider.maxValue = this.timedComboDuration;
    }

    public float GetComboDuration()
    {
        return this.timedComboDuration;
    }

    public void StartCombo()
    {
        if (this.upgradesManager.GetUpgradeCount(Constants.TIMED_COMBO_MULTIPLIER_NAME) > 0)
        {
            this.timedComboCountdownTimer = 0;

            if (!this.isTimedComboActive)
            {
                this.isTimedComboActive = true;
                this.pointEarnerWithoutCombo = this.pointEarner;
                this.pointEarner = this.pointEarnerWithoutCombo * this.timedComboMultiplier;
                this.comboSlider.maxValue = this.timedComboDuration;
                this.comboText.text = this.timedComboMultiplier + "x";
            }
        }
    }

    public void EndCombo()
    {
        this.isTimedComboActive = false;
        this.pointEarner = this.pointEarnerWithoutCombo;
        this.comboText.text = "";
    }
}
