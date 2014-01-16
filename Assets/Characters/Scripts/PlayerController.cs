using UnityEngine;
using System.Collections;
using ZS.Engine.Cam;
using ZS.Engine.Peripherials;
using ZS.Engine;
using ZS.Characters;

namespace Characters {

	public class PlayerController : MonoBehaviour {

		public GameObject bullet;
		private Animator animator;
		private Rigidbody2D rigidBody2D;
		public float flSpeed = 0.0f;
		private float _moveX, _moveY;
		protected Vector3 _inputRotation;
		protected Vector3 _tempVector;
		private Quaternion _rotation;
		private bool _isShooting;
		private Vector3 mouse_pos  ;

		// Use this for initialization
		void Start () {
			animator = GetComponent<Animator>();
			rigidBody2D = GetComponent<Rigidbody2D>();
		}
		
		void Update () {
			// Activity related to base input.
			InputActivity();

			// Move the player.
			MovePlayer();

			// Parse mouse activity.
			MouseActivity();
		}

		private void MovePlayer() {
			rigidBody2D.velocity = new Vector2(_moveX * flSpeed, _moveY * flSpeed);
			_rotation = LookHelper.SmoothLookAtMouse(transform, 0.05f, -90);
			// var obj = GameObject.FindGameObjectsWithTag ("XCube")[0];
			// _rotation = LookHelper.SmoothLookAt(transform, obj.transform, 0.01f, -90);

			animator.SetBool("isRunning", _moveX != 0 || _moveY != 0);
			animator.SetFloat("speed" , Mathf.Abs(Mathf.Max(_moveX, _moveY)));
		}

		private void InputActivity() {
			if(GameService.Instance.IsTactical)
				return;

			if(Input.GetKeyUp(KeyCode.T)) {
				GameService.Instance.ToggleOpsMode();
			}
			else {
				_moveX = InputService.Instance.MoveDX * Time.deltaTime;
				_moveY = InputService.Instance.MoveDY * Time.deltaTime;
			}
		}

		private void HandleBullets()
		{
			_tempVector = Quaternion.AngleAxis(8f, Vector3.up) * mouse_pos;
			_tempVector = (transform.position + (_tempVector.normalized * 0.3f));
			GameObject objCreatedBullet = 
				(GameObject) Instantiate(
				bullet, 
				_tempVector, 
				_rotation ); // create a bullet, and rotate it based on the vector inputRotation
		//	Physics.IgnoreCollision(objCreatedBullet.collider, collider);
			var comp = Camera.main.GetComponent<CameraController>();
			StartCoroutine(comp.Shake());
		}

		private void MouseActivity() {
			if(GameService.Instance.IsTactical) {
				if(Input.GetMouseButtonDown(0)) 
					TacticalLeftButtonDown();
	    		else if(Input.GetMouseButtonDown(1)) 
	    			TacticalRightButtonDown();
	    	} else {
				_isShooting = Input.GetMouseButtonDown(0);
				if(_isShooting && !GameService.Instance.IsTactical)
					HandleBullets();
	    	}
		}

		static int it = 0;

		// Left button was pressed in tactical mode.
		private void TacticalLeftButtonDown() {
			 if(Registry.Instance.hudManager.PointInClientBounds(InputService.Instance.MousePosition)) {
			        GameObject hitObject = CameraManager.Instance.FindHitObject(InputService.Instance.MousePosition, out _tempVector);			        
			        // If nothing was hit or an illegal point was hit, do nothing.
			        if(_tempVector == Registry.Instance.invalidHitPoint) { // we dont care. 
						if(GameService.Instance.selectedObject != null) {
							GameService.Instance.selectedObject.SetSelection(SelectionType.NotSelected);
							GameService.Instance.selectedObject = null;
						}
			        	return;
			        }

			        var hitEntity = hitObject.transform.root.GetComponent< Entity >();
			        // Object hit was interactive.
			        if(hitEntity != null) { 
				        // If this entity is not already selected.
				        if(GameService.Instance.selectedObject != hitEntity) {
							hitEntity.SetSelection(SelectionType.Command);
							if (GameService.Instance.selectedObject != null) { // Deselect if selected.
								GameService.Instance.selectedObject.SetSelection(SelectionType.NotSelected);
			       			} 
				            GameService.Instance.selectedObject = hitEntity;
			        	}
			        }
			        else if (GameService.Instance.selectedObject != null) { // Deselect if selected.
						GameService.Instance.selectedObject.SetSelection(SelectionType.NotSelected);
						GameService.Instance.selectedObject = null;
			        }
			    }
		}

		// Right button was clicked in selection mode.
		private void TacticalRightButtonDown() {
			// For now, right click doesnt do anything if nothing is selected.
			if(GameService.Instance.selectedObject == null) {
				TacticalLeftButtonDown(); // treat it like a lbutton.
			}
			if(Registry.Instance.hudManager.PointInClientBounds(InputService.Instance.MousePosition)) {
			    GameObject hitObject = CameraManager.Instance.FindHitObject(InputService.Instance.MousePosition, out _tempVector);
				var hitEntity = hitObject == null ? null : hitObject.transform.root.GetComponent< Entity >();
			    if(hitEntity == null)
			       	return;

			    // Action mouse clicked.
			    hitEntity.ActionInitiated(GameService.Instance.selectedObject.gameObject,
			    GameService.Instance.selectedObject,
			    _tempVector);
			}
		}
	}
}