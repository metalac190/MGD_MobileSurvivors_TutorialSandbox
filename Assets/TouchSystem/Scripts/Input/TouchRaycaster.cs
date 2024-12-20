using UnityEngine;

public class TouchRaycaster : MonoBehaviour
{
    [SerializeField] private TouchInput _touchInput;
    [SerializeField] private GameObject _touchVisual;
    private void Awake()
    {
        // disable by default
        _touchVisual.SetActive(false);
    }
    private void OnEnable()
    {
        _touchInput.Tapped += OnTapped;
        _touchInput.Released += OnReleased;
    }
    private void OnDisable()
    {
        _touchInput.Tapped -= OnTapped;
        _touchInput.Released -= OnReleased;
    }
    private void Update()
    {
        if (_touchInput.TapHeld)
        {
            RepositionVisual(_touchInput.CurrentTouchPosition);
        }
    }
    private void OnTapped(Vector2 position)
    {
        DetectWorldCollider(position);
        RepositionVisual(_touchInput.CurrentTouchPosition);
    }
    private void OnReleased(Vector2 position)
    {
        _touchVisual.SetActive(false);
    }

    private void DetectWorldCollider(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        // if our Ray hits a collider
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            // search the collider hit for the Touchable component
            Touchable touchable =
                hitInfo.collider.gameObject.GetComponent<Touchable>();
            // if so, click it
            if (touchable != null)
            {
                touchable.Touch();
            }
        }
    }

    private void RepositionVisual(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        // if our Ray hits a collider
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            // move visual
            Debug.Log("Touched: " + hitInfo.transform.gameObject.name);
            _touchVisual.transform.position = hitInfo.point;
            _touchVisual.SetActive(true);
        }
    }

    /*
     *             

    */
}
