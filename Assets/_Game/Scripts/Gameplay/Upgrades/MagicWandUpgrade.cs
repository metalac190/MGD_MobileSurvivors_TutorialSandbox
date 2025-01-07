using UnityEngine;

public class MagicWandUpgrade : IUpgrade
{
    private string _magicWandName = "MagicWand";
    private int _damageIncreaseValue = 5;
    private WeaponData _weaponData;
    // constructors don't have a return value
    public MagicWandUpgrade(WeaponData weaponData)
    {
        _weaponData = weaponData;
    }
    public void Upgrade(PlayerCharacter playerCharacter)
    {
        // search for existing magic wand
        WeaponSystem[] weaponSystems 
            = playerCharacter.GetComponents<WeaponSystem>();
        foreach(WeaponSystem weaponSystem in weaponSystems)
        {
            // if it already exists, upgrade it
            if(weaponSystem.Name == _magicWandName)
            {
                weaponSystem.IncreaseDamage(_damageIncreaseValue);
                return;
            }
        }
        // if not found, add one
        playerCharacter.CreateWeapon(_weaponData);
    }
}
