using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public float EXP { get; private set; } = 0;
    public int LVL { get; private set; } = 1;
    // total exp needed for next level up
    private float _totalEXPNeededForLevelUp = 100;
    // increase in amount of exp needed once level is gained
    private float _expNeededIncreaseAmount = 100;

    public void IncreaseEXP(float amount)
    {
        EXP += amount;
        // if we have enough EXP to level up, DING
        if (EXP >= _totalEXPNeededForLevelUp)
        {
            // increase level
            LVL++;
            Debug.Log("Level up! " + LVL);
            // increase amount needed for next level
            _expNeededIncreaseAmount *= 1.5f;
            // calculate total EXP needed for next level
            // ex. 100exp, 100 + 150 = 250exp, 250 + 225 = 475exp, etc.
            _totalEXPNeededForLevelUp += _expNeededIncreaseAmount;
            // in the future this could be a more complex formula
        }
    }
}
