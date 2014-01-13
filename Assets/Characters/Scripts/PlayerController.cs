using UnityEngine;
using System.Collections;
using ZS.Engine.Cam;

namespace Characters {

	public class PlayerController : MonoBehaviour {

		public GameObject bullet;
		private Animator animator;
		private Rigidbody2D rigidBody2D;
		public float flSpeed = 0.0f;
		protected Vector3 _inputRotation;
		protected Vector3 _tempVector;
		protected Vector3 _tempVector2;


		private bool _isShooting;

		// Use this for initialization
		void Start () {
			animator = GetComponent<Animator>();
			rigidBody2D = GetComponent<Rigidbody2D>();
		}
		
		// void FixedUpdate() {
		// 	float moveX = Input.GetAxis("Horizontal");
		// 	float moveY = Input.GetAxis("Vertical");
			
		// 	Debug.Log(Mathf.Abs(Mathf.Max(moveX, moveY)));

		// 	rigidBody2D.velocity = new Vector2(moveX * flSpeed, moveY * flSpeed);
		// 	animator.SetBool("isRunning", moveX != 0 || moveY != 0);
		// 	animator.SetFloat("speed" , Mathf.Abs(Mathf.Max(moveX, moveY)));
		// }

		// Update is called once per frame
		// void Update () {
		// 	var dTime = Time.deltaTime;
		// 	float moveX = Input.GetAxis("Horizontal") * dTime;
		// 	float moveY = Input.GetAxis("Vertical") * dTime;
			
		// 	// Find the position where we're facing.
		// 	// _tempVector2 = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0); // the position of the middle of the screen
		// 	// _tempVector = Input.mousePosition; // find the position of the moue on screen
		// 	// _tempVector.z = _tempVector2.z = 0; // input mouse position gives us 2D coordinates, I am moving the Y coordinate to the Z coorindate in temp Vector and setting the Y coordinate to 0, so that the Vector will read the input along the X (left and right of screen) and Z (up and down screen) axis, and not the X and Y (in and out of screen) axis
		// 	// _inputRotation = _tempVector - _tempVector2; // the direction we want face/aim/shoot is from the middle of the screen to where the mouse is pointing	
		// 	var pos = Camera.main.WorldToScreenPoint(transform.position);
		// 	_tempVector2 = new Vector3(pos.x ,0, pos.y);
		// 	_tempVector = Input.mousePosition; // find the position of the moue on screen
		// 	_tempVector.z = _tempVector.y; // input mouse position gives us 2D coordinates, I am moving the Y coordinate to the Z coorindate in temp Vector and setting the Y coordinate to 0, so that the Vector will read the input along the X (left and right of screen) and Z (up and down screen) axis, and not the X and Y (in and out of screen) axis
		// 	_tempVector.y = 0;
		// 	_inputRotation = _tempVector - _tempVector2; // the direction we want face/aim/shoot is from the middle of the screen to where the mouse is pointing

		// 	rigidBody2D.velocity = new Vector2(moveX * flSpeed, moveY * flSpeed);
		// 	transform.rotation = Quaternion.LookRotation(_inputRotation);
		// 	transform.eulerAngles = new Vector3(0, 0, -transform.eulerAngles.y);
		// 	// transform.position = new Vector3(transform.position.x, transform.position.y, 0);

		// 	animator.SetBool("isRunning", moveX != 0 || moveY != 0);
		// 	animator.SetFloat("speed" , Mathf.Abs(Mathf.Max(moveX, moveY)));

		// 	_isShooting = Input.GetMouseButtonDown(0);
		// 	if(_isShooting)
		// 		HandleBullets();
		// }

	// void Update () {
	// 		var dTime = Time.deltaTime;
	// 		float moveX = Input.GetAxis("Horizontal") * dTime;
	// 		float moveY = Input.GetAxis("Vertical") * dTime;
			
	// 		var pos = Camera.main.WorldToScreenPoint(transform.position);
	// 		_tempVector2 = new Vector3(pos.x , pos.y , 0);
	// 		_tempVector = Input.mousePosition; // find the position of the moue on screen
	// 		_tempVector.z = 0;
	// 		_inputRotation = _tempVector - _tempVector2; // the direction we want face/aim/shoot is from the middle of the screen to where the mouse is pointing

	// 		rigidBody2D.velocity = new Vector2(moveX * flSpeed, moveY * flSpeed);
	// 		transform.rotation = Quaternion.LookRotation(_inputRotation);
	// 	//	transform.rotation = Quaternion.LookRotation(_inputRotation);
	// //		transform.eulerAngles = new Vector3(0, 0, 0);
	// 		// transform.position = new Vector3(transform.position.x, transform.position.y, 0);

	// 		animator.SetBool("isRunning", moveX != 0 || moveY != 0);
	// 		animator.SetFloat("speed" , Mathf.Abs(Mathf.Max(moveX, moveY)));

	// 		_isShooting = Input.GetMouseButtonDown(0);
	// 		if(_isShooting)
	// 			HandleBullets();
	// 	}

	Vector3 mouse_pos  ;
	 Vector3 object_pos ;
	float angle  ;


	Quaternion _rotation;

		void Update () {
			var dTime = Time.deltaTime;
			float moveX = Input.GetAxis("Horizontal") * dTime;
			float moveY = Input.GetAxis("Vertical") * dTime;

			rigidBody2D.velocity = new Vector2(moveX * flSpeed, moveY * flSpeed);
			_rotation = LookHelper.SmoothLookAtMouse(transform, 0.05f, -90);

			// var obj = GameObject.FindGameObjectsWithTag ("XCube")[0];
			// _rotation = LookHelper.SmoothLookAt(transform, obj.transform, 0.01f, -90);

			animator.SetBool("isRunning", moveX != 0 || moveY != 0);
			animator.SetFloat("speed" , Mathf.Abs(Mathf.Max(moveX, moveY)));

			_isShooting = Input.GetMouseButtonDown(0);
			if(_isShooting)
				HandleBullets();
		}

		protected void HandleBullets()
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
	}
}