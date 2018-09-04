using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualChild : MonoBehaviour 
{
	public Vector3 pos;  
    public Vector3 scl = Vector3.one;  
    public Vector3 rot;  
  
    public Transform virtualParent;  
  
 	void Update () 
	 {  
        Matrix4x4 matLocal = Matrix4x4.TRS(pos, Quaternion.Euler(rot), scl);  
        Matrix4x4 invMine = transform.parent.worldToLocalMatrix;  
        Matrix4x4 matResult = invMine * virtualParent.localToWorldMatrix * matLocal;  
  
        transform.localPosition = matResult.GetColumn(3);  
  
        Vector3 forward = matResult.GetColumn(2);  
        Vector3 upwards = matResult.GetColumn(1);  
        transform.localRotation = Quaternion.LookRotation(forward, upwards);  
  
        Vector3 scale = new Vector3(  
            matResult.GetColumn(0).magnitude,  
            matResult.GetColumn(1).magnitude,  
            matResult.GetColumn(2).magnitude);  
        transform.localScale = scale;  
    }  
}  