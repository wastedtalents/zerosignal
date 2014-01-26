using UnityEngine;
using System.Collections;
using ZS.Engine;
using ZS.Engine.GUI;
using ZS.Engine.Utilities;

namespace ZS.Characters {

	// Base entity.
	//<description>Describes a basic entity that is ANIMATE.</description>
	public abstract class Entity : MonoBehaviour {

		public string displayName; // display name.
		public EntityParams parameters;	
		public Player _owner; // Actual owner of this object if any.
		
		protected string[] _actions = {};
		protected SelectionType _currentSelection;
		protected GameObject _selection;		

		public Player Owner { 
			get { return _owner; }
		}

		public Entity(string name) { 
			displayName = name;
		}

		public Entity() {
		}

		// While this object was selected, someone hovered over "go".
		public void SetHoverState(GameObject go) {

		}

		// Set this object as selected.
		public void SetSelection(SelectionType selection) {
		   _currentSelection = selection;
		   if(_currentSelection != SelectionType.NotSelected)
 		   	 ShowSelection();
 		   else if(_selection != null)
 		   	 HideSelection();
		}

		protected void ShowSelection() {
			_selection = GUIService.Instance.GetSelection(gameObject.transform, _currentSelection);
		} 

		protected void HideSelection() {
			GUIService.Instance.ReturnSelection(_selection);
			_selection = null;
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