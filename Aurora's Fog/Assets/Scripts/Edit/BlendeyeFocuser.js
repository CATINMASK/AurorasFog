#pragma strict

var blendshapeEyeMesh : SkinnedMeshRenderer;
var positiveXAxis : int;
var negativeXAxis : int;
var positiveYAxis : int;
var negativeYAxis : int;
var blinkerEyes : int;

var target : Transform;
var targetMultiplier : float = 500.0;
//var _2DTarget : Vector2;
@Range(-1.0,1.0)
var _2DTargetX : float;
@Range(-1.0,1.0)
var _2DTargetY : float;

var randomEyes = false;
var randomCompressor = 10.0;
var blinkable = false;
var blinkInterval = 3.0;
var blinkSpeed = 0.3;

var isDebugging = false;
var gizmoSize = 0.02;

var pxName : String;
var nxName : String;
var pyName : String;
var nyName : String;
var blinkerName : String = "eCTRLEyesClosed";

private var twoDeePos : Vector4;
private var triDeePos : Vector4;
private var randomizerTarget : GameObject;
private var isBlinking = false;
private var isBlinkedFull = false;
private var isBlinkinged = false;
private var minz = -1.0;

/*
vx = px
vy = py
vz = nx
vw = ny
*/

function Awake(){
	if(!blendshapeEyeMesh){
		Debug.LogWarning("Dude! You should set your head mesh that has blendshape first!");
		enabled = false;
	}
}

function Update () {
	if(!isDebugging){
		if(target){
			
			var newTgt = new Vector3(0,0,0);
			newTgt = target.transform.InverseTransformPoint(blendshapeEyeMesh.transform.position);
			
			triDeePos.x = newTgt.x*targetMultiplier;
			triDeePos.y = -newTgt.y*targetMultiplier;
			triDeePos.z = -newTgt.x*targetMultiplier;
			triDeePos.w = newTgt.y*targetMultiplier;
			
			//blendshapeEyeMesh.SetBlendShapeWeight(positiveXAxis,triDeePos.x); // +X
			//blendshapeEyeMesh.SetBlendShapeWeight(positiveYAxis,triDeePos.y); // +Y
			//blendshapeEyeMesh.SetBlendShapeWeight(negativeXAxis,triDeePos.z); // -X
			//blendshapeEyeMesh.SetBlendShapeWeight(negativeYAxis,triDeePos.w); // -Y
			
			//twoDeeTarget = Vector4.zero;
		}else{ //incase if you don't have 3D Target
			if(_2DTargetX> 0 || _2DTargetY > 0){ //Positive Converter
				twoDeePos.x = _2DTargetX;
				twoDeePos.y = _2DTargetY;
			}
			
			if(_2DTargetX < 0 || _2DTargetY < 0){ //Negative Converter
				twoDeePos.z = -_2DTargetX;
				twoDeePos.w = -_2DTargetY;
			}
			
			//blendshapeEyeMesh.SetBlendShapeWeight(positiveXAxis,twoDeePos.x*100); // +X
			//blendshapeEyeMesh.SetBlendShapeWeight(positiveYAxis,twoDeePos.y*100); // +Y
			//blendshapeEyeMesh.SetBlendShapeWeight(negativeXAxis,twoDeePos.z*100); // -X
			//blendshapeEyeMesh.SetBlendShapeWeight(negativeYAxis,twoDeePos.w*100); // -Y
		}
		
		if(randomEyes){
			if(!randomizerTarget){
				randomizerTarget = new GameObject("RNDTRG");
				randomizerTarget.transform.position = blendshapeEyeMesh.transform.position;
				target = randomizerTarget.transform;
			}else{
				target.position += Random.onUnitSphere * Time.fixedDeltaTime/randomCompressor;
			}
		}
		
		if(blinkable){
			var blinkVal = blendshapeEyeMesh.GetBlendShapeWeight(blinkerEyes);
		
			if(Time.time - blinkInterval > minz && !isBlinking && !isBlinkedFull && !isBlinkinged){
				minz = Time.time - Time.deltaTime;
				isBlinking = true;
			}
			
			if(isBlinking){				
				if(!isBlinkinged){
					if(blinkVal < 99 && !isBlinkedFull){
						blinkVal = Mathf.Lerp(blinkVal,100,blinkSpeed);
						if(blinkVal >= 99) isBlinkedFull = true;
					}
					
					if (isBlinkedFull){ 
						blinkVal = Mathf.Lerp(blinkVal,0.0,blinkSpeed);
						if(blinkVal <= 2.0) isBlinkinged = true;
					}
				}
				
				blendshapeEyeMesh.SetBlendShapeWeight(blinkerEyes, blinkVal);
				
				if(isBlinkinged){
					blendshapeEyeMesh.SetBlendShapeWeight(blinkerEyes, 0);
					isBlinkinged = false;
					isBlinking = false;
					isBlinkedFull = false;
				}
			}
		}
	}
}

function OnDrawGizmos(){
	if(target && blendshapeEyeMesh){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(target.position, gizmoSize);
		Gizmos.DrawLine(blendshapeEyeMesh.transform.position, target.position);
	}
}

function LateUpdate(){
	if(isDebugging){
		print(blendshapeEyeMesh.sharedMesh.blendShapeCount);
		//pxName = blendshapeEyeMesh.sharedMesh.GetBlendShapeName(positiveXAxis);
		//nxName = blendshapeEyeMesh.sharedMesh.GetBlendShapeName(negativeXAxis);
		//pyName = blendshapeEyeMesh.sharedMesh.GetBlendShapeName(positiveYAxis);
		//nyName = blendshapeEyeMesh.sharedMesh.GetBlendShapeName(negativeXAxis);
        //blinkerName = blendshapeEyeMesh.sharedMesh.GetBlendShapeName(blinkerEyes);
	}
}

@script ExecuteInEditMode() //DEBUG use only, or ANIMATOR use only, delete this line if you finished debugging.