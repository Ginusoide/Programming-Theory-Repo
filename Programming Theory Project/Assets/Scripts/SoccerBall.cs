using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerBall : Ball
{
    // NON UTILIZZARE Start e Update qui, non vengono eseguite quelle di Ball!
    // Uso Awake per impostare le proprietà
    void Awake()
    {
        Score = -1;
    }
}
