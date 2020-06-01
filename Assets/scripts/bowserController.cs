using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowserController : MonoBehaviour
{
    public static Rigidbody BowserBody;
    public static GameObject BowserObject;
    public static Animator BowserAnimator;


    // Start is called before the first frame update
    void Start()
    {
        BowserObject=GetComponent<GameObject>();
        BowserBody=GetComponent<Rigidbody>();
        BowserAnimator=GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
    }
}
