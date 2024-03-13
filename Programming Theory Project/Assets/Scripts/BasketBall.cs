using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBall : Ball // INHERITANCE
{
    // NON UTILIZZARE Start e Update qui, non vengono eseguite quelle di FallingObject!
    // Uso Awake per impostare le proprietà
    void Awake()
    {
        Score = 1;
    }
}
