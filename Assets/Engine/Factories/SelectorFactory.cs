using UnityEngine;
using System.Collections.Generic;
using ZS.Characters;

namespace ZS.Engine.Factories {
	
	// Factory of selectors.
	public class SelectorFactory : Singleton<SelectorFactory>, IInitializable {

		private List<GameObject> _selectors;
		public GameObject selectorPrefab;
			
		public void Initialize() { 
			_selectors = new List<GameObject>();
		}

		// Get selector.
		public GameObject Get(Transform parent, SelectionType type) {
			if(_selectors.Count == 0) {
				var sel = InstantiateSelector();
				_selectors.Add(sel);
			}
			ResetSelector(parent, _selectors[0]);
			return _selectors[0];
		}

		public void Return(GameObject sel) {
			sel.transform.parent = null;
			sel.SetActive(false);
		}

		private GameObject InstantiateSelector() {
			return (GameObject) Instantiate(
					selectorPrefab, 
					Vector3.zero, 
					Quaternion.identity); 
		}

		private void ResetSelector(Transform parent, GameObject selector) {
			selector.transform.parent = parent;
			selector.transform.localPosition = Vector3.zero;
			selector.transform.localRotation = Quaternion.identity;
			selector.SetActive(true);
		}

		public void HideSelectors() {
			foreach(var s in _selectors) {
				s.SetActive(false);
			}
		}

	}
}
