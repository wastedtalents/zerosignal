using UnityEngine;
using System.Collections;

namespace ZS.Engine { 

	// Object registry.
	public class Registry : Singleton<Registry> {
		public Transform mainCameraTransform;
		public float cameraTrackingSpeed;
		public float cameraDrag = 40f;
		public float cameraZoom = 20f;

		public readonly float cameraZoomMin = -18;
		public readonly float cameraZoomMax = -2;
		public readonly float cameraScrollOffset = 15f;
		public readonly float cameraScrollSpeed = 25f;
	}

}
