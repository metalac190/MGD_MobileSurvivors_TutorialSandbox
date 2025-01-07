using UnityEngine;
using UnityEngine.UIElements;

public class GameHUDController : MonoBehaviour
{
    [SerializeField] private GameController _gameController;

    private UIDocument _document;
    private VisualElement _loseMenuVisualTree;
    private Button _loseRetryButton;
    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        // get menu sub objects
        _loseMenuVisualTree = _document.rootVisualElement
            .Q("LoseMenuVisualTree");
        // assign button callbacks
        _loseRetryButton = _loseMenuVisualTree
            .Q("RetryButton") as Button;
    }
    private void OnEnable()
    {
        // register callbacks to buttons
        _loseRetryButton.RegisterCallback
            <ClickEvent>(OnLoseRetryButtonClick);

        _gameController.OnLose.AddListener(DisplayLoseMenu);
    }
    private void OnDisable()
    {
        // unregester callbacks to buttons
        _loseRetryButton.UnregisterCallback
            <ClickEvent>(OnLoseRetryButtonClick);

        _gameController.OnLose.RemoveListener(DisplayLoseMenu);
    }
    private void Start()
    {
        DisableAllDisplays();
    }
    private void DisableAllDisplays()
    {
        _loseMenuVisualTree.style.display = DisplayStyle.None;
    }
    public void DisplayLoseMenu()
    {
        _loseMenuVisualTree.style.display = DisplayStyle.Flex;
    }
    private void OnLoseRetryButtonClick(ClickEvent evt)
    {
        _loseMenuVisualTree.style.display = DisplayStyle.None;
        _gameController.ReloadLevel();
    }
}
