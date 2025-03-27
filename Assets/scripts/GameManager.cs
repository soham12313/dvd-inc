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
    public float comboMultiplier;
    public float comboDuration;
    public float comboCountdownTimer;
    private bool isComboActive;

    // Start is called before the first frame update
    void Start()
    {
        upgradesManager.InitializeNormalUpgrades();

        this.dvdSpeed = Constants.BASE_DVD_SPEED;
        this.points = Constants.BASE_POINTS;
        this.pointEarner = Constants.BASE_POINT_GAIN;
        this.cornerScale = Constants.BASE_CORNER_SCALE;
        this.comboMultiplier = Constants.BASE_COMBO_MULTIPLER;
        this.comboDuration = Constants.BASE_COMBO_DURATION;

        for (int i = 0; i < upgradesManager.GetUpgradeCount(Constants.DVD_COUNT_NAME); i++)
        {
            this.SpawnDvd();
        }
    }

    private void Update()
    {
        if (this.isComboActive)
        {
            if (this.comboCountdownTimer < this.comboDuration)
            {
                this.comboCountdownTimer += Time.deltaTime;
                this.comboSlider.value = this.comboDuration - this.comboCountdownTimer;
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
        this.points += this.pointEarner;
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
        if (this.isComboActive)
        {
            this.pointEarnerWithoutCombo += amount;
            this.pointEarner = this.pointEarnerWithoutCombo * this.comboMultiplier;
        }
        else
        {
            this.pointEarner += amount;
        }
    }

    public float GetPointsEarner()
    {
        return this.isComboActive ? this.pointEarnerWithoutCombo : this.pointEarner;
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

    public void IncreaseComboMultiplier(float comboMulti)
    {
        this.comboMultiplier += (float) Math.Round(this.comboMultiplier * comboMulti, 2);

        if (this.isComboActive)
        {
            this.pointEarner = this.pointEarnerWithoutCombo * this.comboMultiplier;
            this.comboText.text = this.comboMultiplier + "x";
        }
    }

    public float GetComboMultiplier()
    {
        return this.comboMultiplier;
    }

    public void IncreaseComboDuration(float duration)
    {
        this.comboDuration += duration;
        this.comboSlider.maxValue = this.comboDuration;
    }

    public float GetComboDuration()
    {
        return this.comboDuration;
    }

    public void StartCombo()
    {
        if (this.upgradesManager.GetUpgradeCount(Constants.COMBO_MULTIPLIER_NAME) > 0)
        {
            this.comboCountdownTimer = 0;

            if (!this.isComboActive)
            {
                this.isComboActive = true;
                this.pointEarnerWithoutCombo = this.pointEarner;
                this.pointEarner = this.pointEarnerWithoutCombo * this.comboMultiplier;
                this.comboSlider.maxValue = this.comboDuration;
                this.comboText.text = this.comboMultiplier + "x";
            }
        }
    }

    public void EndCombo()
    {
        this.isComboActive = false;
        this.pointEarner = this.pointEarnerWithoutCombo;
        this.comboText.text = "";
    }
}
