using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private float _expValue = 20;
    private void OnTriggerEnter2D(Collider2D otherCollision)
    {
        // test if the gameObject has a PlayerCharacter component
        if (otherCollision.TryGetComponent
            (out PlayerCharacter playerCharacter))
        {
            // play sounds
            // play particles
            Collect(playerCharacter);
            Destroy(this.gameObject);
        }
    }

    public void Collect(PlayerCharacter playerCharacter)
    {
        playerCharacter.IncreaseEXP(_expValue);
    }
}
