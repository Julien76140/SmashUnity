using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageOnPlayer : MonoBehaviour
{
    public double damageTaken=0;
    public Text damageText;

    // Update is called once per frame
    void Update()
    {
        damageTaken = HitCollider.damageTaken;

        damageText.text = damageTaken.ToString();
        
    }
}
