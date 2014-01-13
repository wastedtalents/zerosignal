using UnityEngine;
using System.Collections;

namespace ZS.Engine.Math {

	public static class Maths  {

		private static GameObject _go = new GameObject();

		public static Transform PointToTransform(Vector3 point) {
			_go.transform.position = point;
			return _go.transform;
		}
	}
}