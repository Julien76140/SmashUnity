using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour
{
      private Animator hurtAnimator;
    public static bool Jab1;
    public static bool Jab2;
    public static bool Jab3;
      public static float ejection=0f;
    public static float damageTaken=0f;
    public GameObject RightHandHitbox;
    public GameObject LeftHandHitbox;
    public GameObject RightLegHitbox;
    public GameObject LeftLegHitbox;
    public GameObject HeadHitbox;
    public static bool collision;

 

    Vector3 startPosition;
    Vector3 endPosition;
    float speedEjection=1f;
    Rigidbody BowserBody;
    GameObject BowserObject;

    void Start(){
            BowserBody=bowserController.BowserBody;
            BowserObject=bowserController.BowserObject;


    }

    void Update(){
      
        Jab1 = marioController.Jab1IsTrigger;
        Jab2 = marioController.Jab2IsTrigger;
        Jab3 = marioController.Jab3IsTrigger;

    }

   public void OnTriggerEnter(Collider Enemy)
    {

      if(Enemy.gameObject.name=="Bowser"){

      
           if(Jab1){

               damageTaken += 2.2f;
                ejection = 1.0f;
            


           }else{
damageTaken+=0f;

           }
               if(Jab2){
    damageTaken += 1.7f;
                ejection = 1.0f;

           }else{
damageTaken+=0f;

           }
               if(Jab3){
   damageTaken += 4.0f;
                ejection = 1.3f;

           }else{
damageTaken+=0f;

           }


 
      }

        }


}




