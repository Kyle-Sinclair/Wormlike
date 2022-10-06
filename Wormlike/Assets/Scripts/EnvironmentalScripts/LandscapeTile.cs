using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LandscapeTile : MonoBehaviour
{
    public void GrowOnEdge(Vector2 offset)
    {
 
        Vector3 localScale = transform.localScale;
        Vector3 actualOffset = transform.localRotation * new Vector3(offset.x * localScale.x, 0, offset.y * localScale.z);
        Vector3 newPosition  = 5f *  actualOffset;
        GameObject newTarget = Instantiate(gameObject, transform.position + newPosition, transform.rotation, transform.parent);
        Selection.objects = new Object[1]{newTarget};
    }

   
}
