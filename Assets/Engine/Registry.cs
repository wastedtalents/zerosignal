using UnityEngine;
using System.Collections;
using ZS.HUD;

namespace ZS.Engine { 

	// Object registry.
	public class Registry : Singleton<Registry>, IInitializable {

		public Transform mainCameraTransform;
		public float cameraTrackingSpeed;
		public float cameraDrag = 40f;
		public float cameraZoom = 20f;

		public float cameraZoomMin = -18;
		public float cameraZoomMax = -2;
		public float cameraScrollOffset = 15f;
		public float cameraScrollSpeed = 25f;
	
		public int gui_ordersBarWidth = 150;
		public int gui_resourcesBarHeight = 40;

		[HideInInspector]
		public Camera mainCamera;
		
		[HideInInspector]
		public GameObject player;
		
		[HideInInspector]
		public GameObject hud;
		
		[HideInInspector]
		public HUDManager hudManager;


		public void Initialize() {
			mainCamera = Camera.main;
			player = GameObject.FindGameObjectsWithTag("Player")[0];
			hud = GameObject.FindGameObjectsWithTag("HUD")[0];
			hudManager = hud.GetComponent<HUDManager>();
		}
	}

}
