using UnityEngine;
using System.Collections;

// Represents a simple node for the dialogue system.
public class DialogNode  {
	#region Members.

	private bool _isExecuting;

	#endregion

	// Characters animations to fire
	// Text Resource Name
	// Camera transition (LERP) - target localization
	// Node is considered complete wthen it ends executing animations etc. 

	// Is this node currently executing or not.
	public bool IsExecuting {
		get { return _isExecuting; }
		private set { _isExecuting = value; }
	}

	// Execute the node.
	public void Execute() {
	}
}
