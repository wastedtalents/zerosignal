using UnityEngine;
using System.Collections;

namespace ZS.Characters {

	// Type of selection to be used.
	public enum SelectionType {
		NotSelected = 0,
		Command,
		Target
	}

	// Base entity.
	public abstract class Entity : MonoBehaviour {
		public string displayName; // display name.
		public EntityParams parameters;	
		
		protected string[] _actions = {};
		protected SelectionType _currentSelection;

		// Set this object as selected.
		public void SetSelection(SelectionType selection) {
 		   _currentSelection = selection;

 		   if(_currentSelection == SelectionType.Command)
 		   	Debug.Log("SELECTED");
 		   else if(_currentSelection == SelectionType.NotSelected)
 		   	Debug.Log("DESELECTED");

		}

		// Gets available actions.
		public string[] GetActions() {
   			return _actions;
		}

		// Peform a given action.
		public virtual void PerformAction(string actionToPerform) {
		}

		// Mouse clicked at point.
		public virtual void ActionInitiated(GameObject hitObject, Entity entity, Vector3 hitPoint) {
			Debug.Log("ACTION CLICKED");
			 // if(_currentSelection == SelectionType.Command && hitObject != null 
			 // 	&& hitObject.name != Registry.GROUND_NAME) {
			 // 	var entity = hitObject.transform.root.GetComponent< Entity >();
	   //      	// This logic might be moved up BUT 
	   //      	if(entity != null) 
	   //      		ChangeSelection(worldObject, controller);
			 // }
		}
	}

}