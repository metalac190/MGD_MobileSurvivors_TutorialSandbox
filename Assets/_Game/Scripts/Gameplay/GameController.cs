using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField] private float _timeToWin = 30;

    [Header("Upgrade Dependencies")]
    [SerializeField] WeaponData _magicWandData;
    public float ElapsedTime { get; private set; }
    public bool HasWon { get; private set; }
    public List<IUpgrade> PossibleUpgrades 
    { get; private set; } = new List<IUpgrade>();

    public UnityEvent OnWin;
    public UnityEvent OnLose;

    private void Awake()
    {
        SetupUpgrades();
    }

    private void Start()
    {
        // reset our elapsed time at the start of level
        ElapsedTime = 0;
        HasWon = false;
    }

    private void Update()
    {
        // increased elapsed time per second
        ElapsedTime += Time.deltaTime;

        // check win condition
        if(ElapsedTime >= _timeToWin
            && HasWon == false)
        {
            EnterWinState();
        }
    }
    public void EnterWinState()
    {
        HasWon = true;
        Debug.Log("Win!");
        OnWin?.Invoke();
    }
    public void EnterLoseState()
    {
        Debug.Log("Lose!");
        OnLose?.Invoke();
    }
    private void SetupUpgrades()
    {
        // add all possible upgrades at the beginning
        PossibleUpgrades.Add(new HealthUpgrade());
        PossibleUpgrades.Add(new CooldownUpgrade());
        PossibleUpgrades.Add(new MagicWandUpgrade(_magicWandData));
        // longterm we could let designer define this with
        // scriptable objects
    }
    public List<IUpgrade> GetUpgradeChoices(int numberOfChoices)
    {
        // create a temporary list that we can remove upgrades
        // from so we don't offer the same choice multiple times
        // temporary list should start the same as PossibleUpgrades
        List<IUpgrade> tempPossibleUpgrades 
            = new List<IUpgrade>(PossibleUpgrades);
        // prep our list of upgrade choices to use when we find one
        List<IUpgrade> upgradeChoices = new List<IUpgrade>();
        // pull our upgrades from possible
        for (int i = 0; i < numberOfChoices; i++)
        {
            // if we've run out of possible upgrade choices, exit loop
            if (tempPossibleUpgrades.Count == 0)
                break;
            // choose random index in the list
            int randomIndex = Random.Range(0, tempPossibleUpgrades.Count);
            // grab the upgrade in that index and store it
            IUpgrade upgradeOption = tempPossibleUpgrades[randomIndex];
            upgradeChoices.Add(upgradeOption);
            // remove it so it's not chosen next time we search random
            tempPossibleUpgrades.RemoveAt(randomIndex);
        }

        return upgradeChoices;
    }
}
