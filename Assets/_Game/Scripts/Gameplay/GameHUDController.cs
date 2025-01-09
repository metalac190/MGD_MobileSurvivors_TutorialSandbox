using UnityEngine;
using UnityEngine.UIElements;

public class GameHUDController : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private PlayerCharacter _playerCharacter;

    private UIDocument _document;
    private VisualElement _loseMenuVisualTree;
    private Button _loseRetryButton;

    private VisualElement _playerHUDVisualTree;
    private VisualElement _expBarFill;
    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        // get menu sub objects
        _loseMenuVisualTree = _document.rootVisualElement
            .Q("LoseMenuVisualTree");
        _playerHUDVisualTree = _document.rootVisualElement
            .Q("PlayerHUDVisualTree");
        // only search the playerHUDvisual tree, to show we can
        _expBarFill = _playerHUDVisualTree.Q("ProgressBarFill");
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
        _playerCharacter.OnEXPGained += SetEXPBarPercent;
    }
    private void OnDisable()
    {
        // unregester callbacks to buttons
        _loseRetryButton.UnregisterCallback
            <ClickEvent>(OnLoseRetryButtonClick);

        _gameController.OnLose.RemoveListener(DisplayLoseMenu);
        _playerCharacter.OnEXPGained -= SetEXPBarPercent;
    }
    private void Start()
    {
        DisableAllDisplays();
        //SetEXPBarPercent(30,100);
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

    private void SetEXPBarPercent(float currentLevelEXP, float expForLevelUp)
    {
        // formula: 1/max * current is 0-1 value. *100 to convert to %
        float percentage = ((1/ expForLevelUp) * currentLevelEXP) * 100;
        // set width as %
        _expBarFill.style.width = Length.Percent(percentage);
    }
}
