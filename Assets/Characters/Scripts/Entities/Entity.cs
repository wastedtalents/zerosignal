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
		}

		// Gets available actions.
		public string[] GetActions() {
   			return _actions;
		}

		// Peform a given action.
		public virtual void PerformAction(string actionToPerform) {
		}
	}

}