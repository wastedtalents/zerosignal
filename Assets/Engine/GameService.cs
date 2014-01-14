using UnityEngine;
using System.Collections;
using ZS.Engine.Cam;

namespace ZS.Engine {

	// Service for all the in-game logic.
	public class GameService : Singleton<GameService> {

		private bool _isTactical;
		
		// Is in tactical mode.
		public bool IsTactical {
			get { return _isTactical; }
		}

		public void SetOpsMode(bool isTactical) {
			if(_isTactical == isTactical)
				return;
			_isTactical = isTactical;
			if(_isTactical) 
				CameraManager.Instance.Detach();
			else 
				CameraManager.Instance.Follow(Registry.Instance.player.transform);
		}

		public void ToggleOpsMode() {
			SetOpsMode(!_isTactical);
		}
	}
}