using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class blastZone : MonoBehaviour
{

          public VideoPlayer Vp;
          private int nbVie;
          public AudioSource blastSe;
          public Rigidbody MarioBody;
          Shader ShaderBlaszone;
          Shader ShaderTranparent;
          Renderer rendu;
          GameObject blastObject;
          GameObject MarioObject;
          GameObject TotoObject;
          float sensible;

        
    // Start is called before the first frame update
    void Start()
    {
       sensible=1f;
 
          rendu=GetComponent<Renderer>();
          ShaderBlaszone=Shader.Find("Custom/ChromaKeyShader");
          ShaderTranparent=Shader.Find("Legacy Shaders/Transparent/Diffuse");
          MarioObject=GameObject.Find("Mario");     


     MarioBody=MarioObject.GetComponent<marioController>().Mario;    
     rendu.material.shader =ShaderTranparent;
     blastObject=GameObject.Find("blasterAnimation");
            rendu.material.shader =ShaderBlaszone;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
         blastObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Sensitivity",sensible);

      SpawnAfterDeathAndDeathOfPlayer();
      if( MarioObject.transform.position.y<1){

   blastObject.transform.rotation = Quaternion.Euler(90.0f, 270.0f, 90.0f); 
   blastObject.transform.position=new Vector3(MarioObject.transform.position.x,MarioObject.transform.position.y+15,blastObject.transform.position.z);
       

       }
       else if( MarioObject.transform.position.y>29 ){

   blastObject.transform.rotation = Quaternion.Euler(-90f, 270.0f, 90.0f); 
   blastObject.transform.position=new Vector3(MarioObject.transform.position.x,MarioObject.transform.position.y-15,blastObject.transform.position.z);
       

       }
       else if( MarioObject.transform.position.x<-14){

   blastObject.transform.rotation = Quaternion.Euler(0f, 270.0f, 90.0f); 
   blastObject.transform.position=new Vector3(MarioObject.transform.position.x+15,MarioObject.transform.position.y,blastObject.transform.position.z);
       

       }
              else if( MarioObject.transform.position.x>19){

   blastObject.transform.rotation = Quaternion.Euler(180.0f, 270.0f, 90.0f); 
   blastObject.transform.position=new Vector3(MarioObject.transform.position.x-10,MarioObject.transform.position.y,blastObject.transform.position.z);
       

       }

    }

 void SpawnAfterDeathAndDeathOfPlayer(){
 
  if(MarioObject.transform.position.x>20 || MarioObject.transform.position.x<-15 || MarioObject.transform.position.y>30 || MarioObject.transform.position.y<0 ){

     MarioBody.transform.position=new Vector3(0,20,-1);
   MarioBody.constraints = RigidbodyConstraints.FreezePositionY;
      rendu.material.shader=ShaderBlaszone;

         Vp.Play();
         blastSe.Play();
 sensible=0f;

         
 }






 }



}
