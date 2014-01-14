using UnityEngine;
using System.Collections;
using ZS.Engine.Math;
using ZS.Engine;

namespace ZS.Engine.Cam { 

    public class CameraController : MonoBehaviour {

    	public Camera mainCamera;
    	public float delta = 0.1f;
        public Transform target;
        public float trackingSpeed = 2.0f;
        private float shakeDuration = 0.05f;
        public float magnitude = 2.0f;

    	// Use this for initialization
    	void Start () {
            CameraManager.Instance.Follow(Registry.Instance.player.transform);
    	}
    	
    	public IEnumerator Shake() {
            
            float elapsed = 0.0f;
        
            Vector3 originalCamPos = mainCamera.transform.position;
            while (elapsed < shakeDuration) {
            
                elapsed += Time.deltaTime;          
                
                float percentComplete = elapsed / shakeDuration;         
                float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);
                
                // map noise to [-1, 1]
                float x = Random.value * 2.0f - 1.0f;
                float y = Random.value * 2.0f - 1.0f;
                x *= magnitude * damper;
                y *= magnitude * damper;
                
                var cPos = mainCamera.transform.position;
                mainCamera.transform.position = new Vector3(cPos.x + x, cPos.y + y, originalCamPos.z);
                    
                yield return null;
            }
            Camera.main.transform.position = originalCamPos;
        }

    	// Update is called once per frame
    	void Update () {
            CameraManager.Instance.UpdatePosition();
    	}
    }

}