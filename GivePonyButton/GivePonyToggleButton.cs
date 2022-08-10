
/*
using System;
using ShinyShoe;
using UnityEngine;
using Equestrian.Metagame;
using Equestrian.Init;

namespace GivePony
{
	// Token: 0x0200044B RID: 1099
	public class GivePonyToggle : MonoBehaviour
	{
		// Token: 0x060026D6 RID: 9942 RVA: 0x00095561 File Offset: 0x00093761
		private void Awake()
		{
			//AppManager.PlatformServices.DlcOwnershipChanged += this.Refresh;
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x00095579 File Offset: 0x00093779
		private void OnDestroy()
		{
			//AppManager.PlatformServices.DlcOwnershipChanged -= this.Refresh;
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x00095591 File Offset: 0x00093791
		public void Initialize()
		{
			//this.saveManager = saveManager;
			//this.checkboxButton = new GameUISelectableCheckbox() 
			//{ 
			//	spriteState = new UnityEngine.UI.SpriteState() 
			//	{ 
			//		
			//	}
			//};



			this.Refresh();
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x000955A0 File Offset: 0x000937A0
		public void SetVisible(bool visible)
		{
			this.visible = visible;
			this.Refresh();
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x000955B0 File Offset: 0x000937B0
		private void Refresh()
		{
			//bool flag = AppManager.PlatformServices.IsDlcInstalled(this.dlc);

			bool flag = true;

			base.gameObject.SetActive(flag && this.visible);
			if (flag)
			{
				GameUISelectableCheckbox gameUISelectableCheckbox = this.checkboxButton;
				//SaveManager saveManager = this.saveManager;
				//bool? flag2;
				//if (saveManager == null)
				//{
				//	flag2 = null;
				//}
				//else
				//{
				//	MetagameSaveData metagameSave = saveManager.GetMetagameSave();
				//	flag2 = ((metagameSave != null) ? new bool?(metagameSave.IsDlcToggledOn(this.dlc)) : null);
				//}
				//gameUISelectableCheckbox.isChecked = (flag2 ?? false);

				gameUISelectableCheckbox.isChecked = isGivePonyButtonEnabled;
				this.RefreshState();
			}
		}

		// Token: 0x060026DB RID: 9947 RVA: 0x00095648 File Offset: 0x00093848
		public bool ApplyScreenInput(IGameUIComponent triggeredUI, InputManager.Controls triggeredMappingID)
		{
			if (this.checkboxButton.TryTrigger(triggeredUI, triggeredMappingID))
			{
				this.checkboxButton.Toggle();
				isGivePonyButtonEnabled = this.checkboxButton.isChecked;
				this.RefreshState();
				//this.saveManager.SetDLCEnabled(this.dlc, this.checkboxButton.isChecked);
				PonyMetagame.dirty = true;
				PonyMetagame.SavePonyMetaFile();

				return true;
			}
			return false;
		}

		// Token: 0x060026DC RID: 9948 RVA: 0x00095695 File Offset: 0x00093895
		public void SetInteractable(bool interactable)
		{
			this.checkboxButton.interactable = interactable;
		}

		// Token: 0x060026DD RID: 9949 RVA: 0x000956A3 File Offset: 0x000938A3
		private void RefreshState()
		{
			if (this.disabledCanvasGroup)
			{
				this.disabledCanvasGroup.alpha = (this.checkboxButton.isChecked ? 1f : 0.2f);
			}
		}

		// Token: 0x040015F8 RID: 5624
		//[SerializeField]
		//private DLC dlc;

		public static bool isGivePonyButtonEnabled;

		// Token: 0x040015F9 RID: 5625
		[SerializeField]
		private GameUISelectableCheckbox checkboxButton;

		// Token: 0x040015FA RID: 5626
		[SerializeField]
		private CanvasGroup disabledCanvasGroup;

		// Token: 0x040015FB RID: 5627
		//private SaveManager saveManager;

		// Token: 0x040015FC RID: 5628
		private bool visible = true;
	}
}
*/