using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTime : PowerUp // INHERITANCE
{
    private float timeToFreeze = 5.0f;

    public override void ReleasePower() // POLYMORPHISM
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().FreezeTimeFor(timeToFreeze); // ABSTRACTION
    }
}
