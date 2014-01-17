using UnityEngine;
using System.Collections;
using ZS.Engine;
using ZS.Engine.GUI;
using ZS.Engine.Factories;
using ZS.Engine.Utilities;

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
		protected GameObject _selection;

		public Entity(string name) { 
			displayName = name;
		}

		public Entity() {
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
			_selection = SelectorFactory.Instance.Get(gameObject.transform, _currentSelection);
		} 

		protected void HideSelection() {
			SelectorFactory.Instance.Return(_selection);
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