using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusLevel : MonoBehaviour
{

      public float HalfXBounds=40;
      public float HalfYBounds=10;
      public float HalfZBounds=10;
      public Bounds FocusBounds;
      

    // Update is called once per frame
    void Update()
    {
        Vector3 position=gameObject.transform.position;
        Bounds bounds= new Bounds();
        bounds.Encapsulate(new Vector3(position.x-HalfXBounds,position.y-HalfYBounds,position.z-HalfZBounds));
        bounds.Encapsulate(new Vector3(position.x+HalfXBounds,position.y+HalfYBounds,position.z+HalfZBounds));
        FocusBounds=bounds;
    }
}
