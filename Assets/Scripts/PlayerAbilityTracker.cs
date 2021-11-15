using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityTracker : MonoBehaviour
{
    [SerializeField] bool canDoubleJump;
    [SerializeField] bool canDash;
    [SerializeField] bool canBecomeBall;
    [SerializeField] bool canDropBomb;

    public bool GetCanDoubleJump()
    {
        return canDoubleJump;
    }

    public void SetCanDoubleJump(bool value)
    {
        canDoubleJump = value;
    }

    public bool GetCanDash()
    {
        return canDash;
    }

    public void SetCanDash(bool value)
    {
        canDash = value;
    }

    public bool GetCanBecomeBall()
    {
        return canBecomeBall;
    }
    public void SetCanBecomeBall(bool value)
    {
        canBecomeBall = value;
    }

    public bool GetCanDropBomb()
    {
        return canDropBomb;
    }

    public void SetCanDropBomb(bool value)
    {
        canDropBomb = value;
    }
}
