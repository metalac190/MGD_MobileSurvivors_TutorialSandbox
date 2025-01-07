public class CooldownUpgrade : IUpgrade
{
    private float _cooldownReductionAmount = 0.2f;
    public void Upgrade(PlayerCharacter playerCharacter)
    {
        WeaponSystem[] weaponSystems = 
            playerCharacter.GetComponents<WeaponSystem>();
        if (weaponSystems != null && weaponSystems.Length > 0)
        {
            foreach (WeaponSystem weapon in weaponSystems)
            {
                weapon.DecreaseCooldown(_cooldownReductionAmount);
            }
        }
    }
}

/*

 */
