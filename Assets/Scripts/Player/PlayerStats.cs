using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    public static bool isInitialized { get; set; }
    public static bool canDoubleJump { get; set; }
    public static bool canDash { get; set; }
    public static bool canDropBomb { get; set; }
    public static bool canBecomeBall { get; set; }
    public static int currentHealth { get; set; }
    public static int maxHealth { get; set; }
}
