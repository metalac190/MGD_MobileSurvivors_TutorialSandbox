using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    protected abstract void Collect(PlayerCharacter playerCharacter);

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
}
