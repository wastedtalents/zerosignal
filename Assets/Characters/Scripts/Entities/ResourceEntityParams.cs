using UnityEngine;
using System.Collections;

namespace ZS.Characters {

	// Collection of parameters for an entity that is a resource - building, etc.
	public sealed class ResourceEntityParams : MonoBehaviour {
		public int cost, 
				   sellValue;
	}

}