using UnityEngine;
using System.Collections;

public class PlayerControllerExt : MonoBehaviour {

protected Vector3 _inputRotation;
	protected Vector3 _tempVector;
	protected Vector3 _tempVector2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mousePos = Input.mousePosition;
       mousePos.z = 10.0f;
       Vector3 lookPos = Camera.mainCamera.ScreenToWorldPoint(mousePos);
 
       Quaternion targetRotation = Quaternion.LookRotation(lookPos - transform.position, Vector3.forward);
       transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20f);
	}
}
