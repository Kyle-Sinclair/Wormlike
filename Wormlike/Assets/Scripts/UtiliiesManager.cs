using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public static class UtiliiesManager
{
    private const int characterLayerMask = 1 << 3;
    //static Collider[] buffer = new Collider[100];

    public static int BufferedCount { get; private set; }

    public static bool FillBuffer (Vector3 position, float range, ref Collider[] buffer) {
        BufferedCount = Physics.OverlapSphereNonAlloc(
            position, range, buffer, characterLayerMask
        );
        return BufferedCount > 0;
    }
    
    
}
