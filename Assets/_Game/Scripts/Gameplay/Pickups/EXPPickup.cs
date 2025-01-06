using UnityEngine;

public class EXPPickup : Pickup
{
    [SerializeField] private float _expValue = 20;
    protected override void Collect(PlayerCharacter playerCharacter)
    {
        playerCharacter.IncreaseEXP(_expValue);
    }
}
