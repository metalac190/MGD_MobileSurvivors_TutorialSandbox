using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCharacter : MonoBehaviour
{
    public event Action OnLeveledUp = delegate { };
    // OnEXPGained(currentEXP, EXPForNextLevelUp)
    public event Action<float ,float> OnEXPGained = delegate { };

    [SerializeField] private WeaponData _startingWeaponData;
    public List<IUpgrade> ActiveUpgrades { get; private set; } = new List<IUpgrade>();
    // NOTE: we're resetting EXP to 0 every level up
    public float EXP { get; private set; } = 0;
    public int LVL { get; private set; } = 1;

    private float _expForNextLevelUp = 100;
    // tracking TOTAL exp in case we need it later
    private float _expTotal = 0;

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
        Debug.Log("EXP Increase: " + EXP);
        OnEXPGained.Invoke(EXP, _expForNextLevelUp);

        // if we have enough EXP to level up, DING
        if (EXP >= _expForNextLevelUp)
            LevelUp();
    }

    public void LevelUp()
    {
        // increase level
        LVL++;
        Debug.Log("Level up! " + LVL);
        // store overflow exp into next level
        float overflowEXP = EXP - _expForNextLevelUp;
        // store current EXP in total exp then reset
        _expTotal += _expForNextLevelUp;
        // wipe exp to 0, but add any overflow
        EXP = overflowEXP;
        // increase amount needed for next level
        // continues to increase each level up
        // ex. 100exp, 150exp,  225exp, etc.
        _expForNextLevelUp *= 1.5f;

        OnEXPGained?.Invoke(EXP, _expForNextLevelUp);
        OnLeveledUp?.Invoke();

        GetUpgrades();
    }

    private void GetUpgrades()
    {
        // get a few options from our Upgrade pool
        List<IUpgrade> upgradeChoices = _gameController.GetUpgradeChoices(3);
        foreach (IUpgrade upgrade in upgradeChoices)
        {
            Debug.Log("Choice: " + upgrade.ToString());
        }
        // for simplicity choose one of the options and upgrade automatically
        // in an actual, larger game, we could let the player click a button
        int randomIndex = UnityEngine.Random.Range(0, upgradeChoices.Count);
        IUpgrade newUpgrade = upgradeChoices[randomIndex];
        Debug.Log("Chose: " + newUpgrade.ToString());
        // upgrade ourselves with the chosen upgrade
        newUpgrade.Upgrade(this);
    }
}

/*
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
*/
