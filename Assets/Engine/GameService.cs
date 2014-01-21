using UnityEngine;
using System.Collections;
using ZS.Engine.Cam;
using ZS.Characters;
using ZS.Engine.GUI;

namespace ZS.Engine {

	// Service for all the in-game logic.
	public class GameService : Singleton<GameService> {

		// Currently selected object.
		// TODO - add more than one.
		[HideInInspector]
		public Entity selectedObject;

		private bool _isTactical;

		// Is in tactical mode.
		public bool IsTactical {
			get { return _isTactical; }
		}

		void Start() {
			SetOpsMode(false);
		}

		public void SetOpsMode(bool isTactical) {
			if(_isTactical == isTactical)
				return;
			_isTactical = isTactical;
			if(_isTactical)  {
				CameraManager.Instance.Detach();
				Registry.Instance.hudManager.SetTacticalMode();
			} 
			else  {
				// Hide selectors.
				Registry.Instance.hudManager.SetArcadeMode();
				GUIService.Instance.HideSelectors();
				CameraManager.Instance.Follow(Registry.Instance.player.transform);
			}
		}

		public void ToggleOpsMode() {
			SetOpsMode(!_isTactical);
		}

	}
}