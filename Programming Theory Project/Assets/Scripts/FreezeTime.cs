using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTime : PowerUp
{
    private float timeToFreeze = 5.0f;

    public override void ReleasePower()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().FreezeTimeFor(timeToFreeze);
    }
}
