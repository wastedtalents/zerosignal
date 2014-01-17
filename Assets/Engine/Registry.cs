using UnityEngine;
using System.Collections;
using ZS.HUD;

namespace ZS.Engine { 

	// Object registry.
	public class Registry : Singleton<Registry>, IInitializable {

		public const string GROUND_NAME = "Background";
		public readonly KeyCode TOGGLE_OPS_KEY = KeyCode.T;

		// ACTIONS.
		public const string ACTION_ATTACK = "A_Attack";

		public Transform mainCameraTransform;
		public float cameraTrackingSpeed;
		public float cameraDrag = 40f;
		public float cameraZoom = 20f;

		public float cameraZoomMin = -18;
		public float cameraZoomMax = -2;
		public float cameraScrollOffset = 15f;
		public float cameraScrollSpeed = 25f;

		[HideInInspector]
		public Camera mainCamera;
		
		[HideInInspector]
		public GameObject player;
		
		[HideInInspector]
		public GameObject hud;
		
		[HideInInspector]
		public HUDManager hudManager;

		[HideInInspector]
		public Vector3 invalidHitPoint;

		[HideInInspector]
		public Bounds invalidBounds;

		public void Initialize() {
			mainCamera = Camera.main;
			player = GameObject.FindGameObjectsWithTag("Player")[0];
			hud = GameObject.FindGameObjectsWithTag("HUD")[0];
			hudManager = hud.GetComponent<HUDManager>();
			invalidHitPoint = Vector3.forward;
			invalidBounds = new Bounds(new Vector3(-99999, -99999, -99999), new Vector3(0, 0, 0));
		}
	}

}
