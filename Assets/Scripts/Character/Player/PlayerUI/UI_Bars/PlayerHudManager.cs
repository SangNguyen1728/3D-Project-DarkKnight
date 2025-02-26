using Unity.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHudManager : MonoBehaviour
{
    [SerializeField] UI_StatBar staminaBar;

    public void SetNewStaminaValue(float oldValue, float newValue)
    {
        staminaBar.SetStat(Mathf.RoundToInt(newValue));
    }
    public void SetMaxStaminaValue(int maxStamina)
    {
        staminaBar.SetMaxStat(maxStamina);
    }
}
