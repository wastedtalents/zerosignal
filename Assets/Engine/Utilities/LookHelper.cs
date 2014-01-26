using UnityEngine;
using System.Collections;

namespace ZS.Engine.Utilities {

	// All the utilities regarding look transformations.
	public static class LookHelper {

		#region Members.

		private static Vector3 _tempVector;
		private static Vector3 _mousePos;
	 	private static Vector3 _objectPos;
		private static float _angle;
		private static Quaternion _rotation;

		#endregion

		static LookHelper() {
			_tempVector = new Vector3(0, 0, 0);		
		}

		// Turn to rotation.
		private static Quaternion CalcRotation(Vector3 source, Vector3 target) {
			_tempVector.z = 0;
	 		_tempVector.x = target.x - source.x;
			_tempVector.y = target.y - source.y;
			_angle = Mathf.Atan2(_tempVector.y, _tempVector.x) * Mathf.Rad2Deg;

			_tempVector.x = 0;
			_tempVector.y = 0;
			_tempVector.z = _angle;

			return Quaternion.Euler(_tempVector);
		}

		public static Quaternion LookAtMouse(Transform source, float offset = 0) {
			_mousePos = Input.mousePosition;
	 		_objectPos = Camera.main.WorldToScreenPoint(source.position);

			var quaternion = CalcRotation(_objectPos, _mousePos);
			source.rotation = quaternion;
			if(offset != 0)
	 	  		source.eulerAngles = new Vector3(0,0,quaternion.eulerAngles.z - 90);
	 	   	return quaternion;
		}

		// Look into the direction of mouse smoothly.
		 public static Quaternion SmoothLookAtMouse(Transform source, float speed, float offset = 0) {
			_mousePos = Input.mousePosition;
	 		_objectPos = Camera.main.WorldToScreenPoint(source.position);

			var quaternion = CalcRotation(_objectPos, _mousePos);
		    var q = Quaternion.Euler(0,0, quaternion.eulerAngles.z + offset);
			source.rotation = Quaternion.Slerp(source.rotation, q, speed);
			return quaternion;
		 }

		 // Look into the direction of target.
		 public static Quaternion LookAt(Transform source, Transform target, float offset = 0) {
		 	var quaternion = CalcRotation(source.position, target.position);
		 	source.rotation = quaternion;
			if(offset != 0)
	 	  		source.eulerAngles = new Vector3(0,0,quaternion.eulerAngles.z - 90);
	 	   	return quaternion;	
		 }

		 // Look smoothly into the direction of target.
		 public static Quaternion SmoothLookAt(Transform source, Transform target, float speed, float offset = 0) {
		 	var quaternion = CalcRotation(source.position, target.position);
		 	var q = Quaternion.Euler(0,0, quaternion.eulerAngles.z + offset);
			source.rotation = Quaternion.Slerp(source.rotation, q, speed);
			return quaternion;	
		 }
	}

}