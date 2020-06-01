using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subCam : MonoBehaviour
{

        public FocusLevel FocusLevel;
        public List<GameObject> Players;
        public float DepthUpdateSpeed=10;
        public float AngleUpdateSpeed=12;
        public float PositionUpdateSpeed=10;
        public float DepthMax=-10;
        public float DepthMin=-8;
        public float AngleMax=10;
        public float AngleMin=8;
        private float CameraEulerX;
        private Vector3 CameraPosition;


    // Start is called before the first frame update
    void Start()
    {
        Players.Add(FocusLevel.gameObject);
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CalculateCameraLocation();
        MoveCamera();
    }

private void MoveCamera(){

Vector3 position=gameObject.transform.position;
if(position !=CameraPosition){
Vector3 targetPosition=Vector3.zero;


if(CameraPosition.x>=3){
targetPosition.x=Mathf.MoveTowards(position.x,3,PositionUpdateSpeed*Time.deltaTime);
targetPosition.y=Mathf.MoveTowards(position.y,CameraPosition.y+4,PositionUpdateSpeed*Time.deltaTime);
targetPosition.z=Mathf.MoveTowards(position.z,-10,DepthUpdateSpeed*Time.deltaTime);

}else{


targetPosition.x=Mathf.MoveTowards(position.x,CameraPosition.x,PositionUpdateSpeed*Time.deltaTime);
targetPosition.y=Mathf.MoveTowards(position.y,CameraPosition.y+4,PositionUpdateSpeed*Time.deltaTime);
targetPosition.z=Mathf.MoveTowards(position.z,CameraPosition.z-2,DepthUpdateSpeed*Time.deltaTime);
}

gameObject.transform.position=targetPosition;
}
Vector3 localEulerAngle=gameObject.transform.localEulerAngles;

if(localEulerAngle.x!=CameraEulerX){
Vector3 targetEulerAngles= new Vector3(CameraEulerX,localEulerAngle.y,localEulerAngle.z);
gameObject.transform.localEulerAngles=Vector3.MoveTowards(localEulerAngle,targetEulerAngles,AngleUpdateSpeed*Time.deltaTime);



}

}

private void CalculateCameraLocation(){

Vector3 averageCenter= Vector3.zero;
Vector3 totalPosition=Vector3.zero;
Bounds playerBounds=new Bounds();
for(int i=0;i<Players.Count;i++){
Vector3 playerPosition=Players[i].transform.position;

if(!FocusLevel.FocusBounds.Contains(playerPosition))
{
float playerX=Mathf.Clamp(playerPosition.x,FocusLevel.FocusBounds.min.x,FocusLevel.FocusBounds.max.x);
float playerY=Mathf.Clamp(playerPosition.y,FocusLevel.FocusBounds.min.y,FocusLevel.FocusBounds.max.y);
float playerZ=Mathf.Clamp(playerPosition.z,FocusLevel.FocusBounds.min.z,FocusLevel.FocusBounds.max.z);
playerPosition= new Vector3(playerX,playerY,playerZ);

}
totalPosition+=playerPosition;
playerBounds.Encapsulate(playerPosition);


}
averageCenter=(totalPosition/Players.Count);
float extents=(playerBounds.extents.x+playerBounds.extents.y);
float lerpPercent=Mathf.InverseLerp(0,(FocusLevel.HalfXBounds+FocusLevel.HalfYBounds)/2,extents);
float depth=Mathf.Lerp(DepthMax,DepthMin,lerpPercent);
float angle=Mathf.Lerp(AngleMax,AngleMin,lerpPercent);
CameraEulerX=angle;
CameraPosition=new Vector3 (averageCenter.x,averageCenter.y,depth);

}







}
