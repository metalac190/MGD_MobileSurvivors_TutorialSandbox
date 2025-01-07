using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private WeaponData _startingWeaponData;
    public List<IUpgrade> ActiveUpgrades { get; private set; } = new List<IUpgrade>();

    public float EXP { get; private set; } = 0;
    public int LVL { get; private set; } = 1;
    // total exp needed for next level up
    private float _totalEXPNeededForLevelUp = 100;
    // increase in amount of exp needed once level is gained
    private float _expNeededIncreaseAmount = 100;

    private GameController _gameController;
    private void Awake()
    {
        _gameController = FindAnyObjectByType<GameController>();
    }
    private void Start()
    {
        CreateWeapon(_startingWeaponData);
    }
    public void CreateWeapon(WeaponData weaponData)
    {
        // add WeaponSystem component
        WeaponSystem newWeaponSystem = 
            gameObject.AddComponent<WeaponSystem>();
        // customize it
        newWeaponSystem.SetupWeapon(weaponData);
    }

    public void IncreaseEXP(float amount)
    {
        EXP += amount;
        // if we have enough EXP to level up, DING
        if (EXP >= _totalEXPNeededForLevelUp)
            LevelUp();
    }

    public void LevelUp()
    {
        // increase level
        LVL++;
        Debug.Log("Level up! " + LVL);
        // increase amount needed for next level
        _expNeededIncreaseAmount *= 1.5f;
        // calculate total EXP needed for next level
        // ex. 100exp, 100 + 150 = 250exp, 250 + 225 = 475exp, etc.
        _totalEXPNeededForLevelUp += _expNeededIncreaseAmount;
        // in the future this could be a more complex formula

        // get a few options from our Upgrade pool
        List<IUpgrade> upgradeChoices = _gameController.GetUpgradeChoices(3);
        Debug.Log("upgradeChoices count: " + upgradeChoices.Count);
        foreach (IUpgrade upgrade in upgradeChoices)
        {
            Debug.Log("Choice: " + upgrade.ToString());
        }
        // for simplicity choose one of the options and upgrade automatically
        // in an actual, larger game, we could let the player click a button
        int randomIndex = Random.Range(0, upgradeChoices.Count);
        IUpgrade newUpgrade = upgradeChoices[randomIndex];
        Debug.Log("Chose: " + newUpgrade.ToString());
        // upgrade ourselves with the chosen upgrade
        newUpgrade.Upgrade(this);
    }
}
