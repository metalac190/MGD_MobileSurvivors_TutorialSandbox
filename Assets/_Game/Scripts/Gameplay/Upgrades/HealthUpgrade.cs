public class HealthUpgrade : IUpgrade
{
    private int _maxHealthIncreaseAmount = 100;
    public void Upgrade(PlayerCharacter playerCharacter)
    {
        if (playerCharacter.TryGetComponent(out Health health))
        {
            health.IncreaseMaxHealth(_maxHealthIncreaseAmount);
        }
    }
}

