using UnityEngine;
using System.Collections;
using ZS.Engine.Cam;
using ZS.Engine.Peripherials;
using ZS.Engine;

namespace Characters {

	public class PlayerController : MonoBehaviour {

		public GameObject bullet;
		private Animator animator;
		private Rigidbody2D rigidBody2D;
		public float flSpeed = 0.0f;
		private float _moveX, _moveY;
		protected Vector3 _inputRotation;
		protected Vector3 _tempVector;
		protected Vector3 _tempVector2;
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

		private void TacticalLeftButtonDown() {
			 if(Registry.Instance.hudManager.PointInClientBounds(InputService.Instance.MousePosition)) {
			        GameObject hitObject = CameraManager.Instance.FindHitObject(InputService.Instance.MousePosition);
			        Debug.Log(hitObject != null);
			        // Vector3 hitPoint = FindHitPoint();
			        // if(hitObject && hitPoint != ResourceManager.InvalidPosition) {
			        //     if(player.SelectedObject) player.SelectedObject.MouseClick(hitObject, hitPoint, player);
			        //     else if(hitObject.name!="Ground") {
			        //         WorldObject worldObject = hitObject.transform.root.GetComponent< WorldObject >();
			        //         if(worldObject) {
			        //             //we already know the player has no selected object
			        //             player.SelectedObject = worldObject;
			        //             worldObject.SetSelection(true);
			        //         }
			        //     }
			        // }
			    }
		}



		private void TacticalRightButtonDown() {

		}
	}
}