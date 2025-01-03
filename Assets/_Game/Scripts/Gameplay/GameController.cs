using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField] private float _timeToWin = 30;
    public float ElapsedTime { get; private set; }
    public bool HasWon { get; private set; }

    public UnityEvent OnWin;
    public UnityEvent OnLose;

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

}
