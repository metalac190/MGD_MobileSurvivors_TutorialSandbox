using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Attach this component to a gameObject and the Unity Event will
/// fire appropriately. NOTE: This is using a component instead of 
/// an interface, since it is primarily intended to be used for gameObjects
/// in the world.
/// </summary>
public class Touchable : MonoBehaviour
{
    [SerializeField] private ParticleSystem _touchParticlePrefab;
    [SerializeField] private AudioClip _touchSound;

    public UnityEvent Touched;
    public void Touch()
    {
        // touch sound
        if(_touchSound != null)
        {
            // play a 3D sound at main camera's position
            AudioSource.PlayClipAtPoint(_touchSound, 
                Camera.main.transform.position);
        }
        // spawn particle. It should destroy itself
        if (_touchParticlePrefab != null)
        {
            Instantiate(_touchParticlePrefab,
                transform.position, Quaternion.identity);
        }

        Touched?.Invoke();
    }
}
 