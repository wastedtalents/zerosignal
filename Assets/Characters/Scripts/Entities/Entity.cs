using UnityEngine;
using System.Collections;
using ZS.Engine;

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

		public Entity(string name) { 
			displayName = name;
		}

		public Entity() {
		}

		// Set this object as selected.
		public void SetSelection(SelectionType selection) {
 		   _currentSelection = selection;
		}

		// Gets available actions.
		public string[] GetActions() {
   			return _actions;
		}

		// Peform a given action.
		public virtual void PerformAction(GameObject targetObject, Entity targetEntity, string actionToPerform) {
			if(actionToPerform == Registry.ACTION_ATTACK) {
				Debug.Log(targetEntity.displayName + " : " + actionToPerform);
			}
		}

		// Mouse clicked at point.
		public virtual void ActionInitiated(GameObject hitObject, Entity entity, Vector3 hitPoint) {
			 // If were currently SELECTED as command mode, we can do stuff!
			 if(_currentSelection == SelectionType.Command && hitObject != null 
			 	&& hitObject.name != Registry.GROUND_NAME) {
	        	// This logic might be moved up BUT 
	        	if(entity != null) { 
	        		// GetActions
	        		// Perform action
	        		entity.PerformAction(hitObject, entity, Registry.ACTION_ATTACK);
	        	}
			 }
		}
	}

}