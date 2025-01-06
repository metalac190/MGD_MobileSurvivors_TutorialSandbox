using UnityEngine;

public class HealthPickup : Pickup
{
    [SerializeField] private int _healthAmount = 20;
    protected override void Collect(PlayerCharacter playerCharacter)
    {
        // NOTE: could optimize by saving Heatlh reference on
        // PlayerCharacter class. This is fine for now
        if (playerCharacter.TryGetComponent(out Health health))
        {
            health.IncreaseHealth(_healthAmount);
        }
    }
}
