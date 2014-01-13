using UnityEngine;
using System.Collections;

namespace ZS.Engine.Peripherials {

	public class InputService : Singleton<InputService> {

		public float GetZoom() {
			return Input.GetAxis("Mouse ScrollWheel");
		}

		public float MouseDX {
			get { return Input.GetAxis("Mouse X"); }
		}

		public float MouseDY {
			get { return Input.GetAxis("Mouse X"); }
		}

		public Vector3 MousePosition {
			get { return Input.mousePosition; }
		}

		public bool HasMouseMovedX {
			get { return MouseDX != 0; }
		}

		public bool HasMouseMovedY {
			get { return MouseDY != 0; }
		}		
	}

}