using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using HarmonyLib;
using ShinyShoe;
using ShinyShoe.Audio;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Trainworks.Managers;
using Trainworks.Constants;
using Equestrian.MonsterCards;
using Equestrian.HarmonyPatches;
using Equestrian.Init;
using Equestrian.CardPools;
using Equestrian.Enhancers;
using Equestrian.Metagame;

namespace GivePony
{
	// Token: 0x02000004 RID: 4
	public class GivePonyToggle : MonoBehaviour
	{
		public static bool isGivePonyButtonEnabled;
		public static Image toggleIMG;
		public static Sprite toggleOn;
		public static Sprite toggleOff;

		// Token: 0x06000004 RID: 4 RVA: 0x00002071 File Offset: 0x00000271
		private void Start()
		{
			//this._mainMenu = base.GetComponentInParent<MainMenuScreen>();
			//this._givePonyButton = base.GetComponent<GameUISelectableCheckbox>();
			UISignals.GameUITriggered.AddListener(new Action<CoreInputControlMapping, IGameUIComponent>(this.OnGameUITriggered));
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020A1 File Offset: 0x000002A1
		private void OnDestroy()
		{
			UISignals.GameUITriggered.RemoveListener(new Action<CoreInputControlMapping, IGameUIComponent>(this.OnGameUITriggered));
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020BC File Offset: 0x000002BC
		private void OnGameUITriggered(CoreInputControlMapping mapping, IGameUIComponent component)
		{
			if (component.IsGameUIComponent(_givePonyButton) && mapping.IsID(InputManager.Controls.Clicked) && this._coroutine == null)
			{
				Traverse.Create(_mainMenu).Field("soundManager").GetValue<SoundManager>().PlaySfx("UI_EndTurn", AudioSfxPriority.Normal, false);
				this._coroutine = GlobalMonoBehavior.Inst.StartCoroutine(GivePonyToggle.GivePonyToggleCoroutine());
				_givePonyButton.Toggle();
				_givePonyButton.RefreshChecked();
				GivePonyToggle.RefreshSprite();
			}
		}

		public static void RefreshSprite() 
		{
			if (toggleOn == null || toggleOn.GetInstanceID() == 0)
			{
				toggleOn = null;
				toggleOff = null;

				string localPath = Path.GetDirectoryName(Ponies.Instance.Info.Location);

				toggleOn = CustomAssetManager.LoadSpriteFromPath(Path.Combine(localPath, "ClanAssets/toggleOn.png"));
				toggleOff = CustomAssetManager.LoadSpriteFromPath(Path.Combine(localPath, "ClanAssets/toggleOff.png"));
			}

			if (toggleIMG == null) { return; }

			if (isGivePonyButtonEnabled)
			{
				toggleIMG.sprite = toggleOn;
			}
            else 
			{
				toggleIMG.sprite = toggleOff;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000212F File Offset: 0x0000032F
		public static IEnumerator GivePonyToggleCoroutine()
		{
			isGivePonyButtonEnabled = !isGivePonyButtonEnabled;

			GivePonyToggle.RefreshSprite();

			PonyMetagame.dirty = true;
			PonyMetagame.SavePonyMetaFile();

			//Don't give a pony
			//if (Trainworks.Managers.ProviderManager.SaveManager.IsBattleMode())
			//{
			//	Ponies.Log("Give me a pony.");

			//Thay spawn with multistrike for now. Will replace with a custom upgrade later.
			//CardData randomPony = MyCardPools.GivePonyCardPool.GetRandomChoice(RngId.Battle);
			//CardManager.AddCardUpgradingInfo addCardUpgradingInfo = new CardManager.AddCardUpgradingInfo();
			//addCardUpgradingInfo.upgradeDatas.Add(ProviderManager.SaveManager.GetAllGameData().FindEnhancerData("0016b165-11a9-4a26-8837-3b2895bc39f8").GetEffects()[0].GetParamCardUpgradeData());
			//addCardUpgradingInfo.upgradeDatas.Add(Playstone.PlaystoneData.GetEffects()[0].GetParamCardUpgradeData());
			//addCardUpgradingInfo.upgradeDatas.Add(VanillaUnitEnhancers.GetRandomEnhancer(RngId.Battle));

			//CardManager value4 = Traverse.Create(battleHud).Field("cardManager").GetValue<CardManager>();
			//	value4.AddCard(randomPony, CardPile.HandPile, 1, 1,false, false, addCardUpgradingInfo);
			//}
			
			//Don't restart the battle
			/*CombatManager value = Traverse.Create(battleHud).Field("combatManager").GetValue<CombatManager>();
			GameStateManager gameStateManager = Traverse.Create(value).Field("gameStateManager").GetValue<GameStateManager>();
			SaveManager value2 = Traverse.Create(value).Field("saveManager").GetValue<SaveManager>();
			ScreenManager value3 = Traverse.Create(battleHud).Field("screenManager").GetValue<ScreenManager>();
			CardStatistics value4 = Traverse.Create(Traverse.Create(battleHud).Field("cardManager").GetValue<CardManager>()).Field("cardStatistics").GetValue<CardStatistics>();
			RunType runType = Traverse.Create(value2).Field("activeRunType").GetValue<RunType>();
			string sharecode = Traverse.Create(value2).Field("activeSharecode").GetValue<string>();
			gameStateManager.LeaveGame();
			value3.ReturnToMainMenu();
			value4.ResetAllStats();
			yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "main_menu" && SceneManager.GetActiveScene().isLoaded);
			yield return null;
			yield return null;
			yield return null;
			gameStateManager.ContinueRun(runType, sharecode, null);*/
			yield break;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002140 File Offset: 0x00000340
		public static void Create(MainMenuScreen mainMenuScreen)
		{
			if (!Ponies.EquestrianClanIsInit) { return; }

			_mainMenu = mainMenuScreen;

			MainMenuButton quitButton = AccessTools.Field(typeof(MainMenuScreen), "quitButton").GetValue(mainMenuScreen) as MainMenuButton;
			TextMeshProUGUI betterLabel = AccessTools.Field(typeof(MainMenuButton), "label").GetValue(quitButton) as TextMeshProUGUI;

			//Ponies.Log("Line 96");
			DLCToggle valueX = Traverse.Create(mainMenuScreen).Field("hellforgedDlcToggle").GetValue<DLCToggle>();
			Component value = Traverse.Create(valueX).Field("checkboxButton").GetValue<GameUISelectableCheckbox>();

			//Ponies.Log("Line 98");
			//EnergyUI value2 = Traverse.Create(battleHud).Field("energyUI").GetValue<EnergyUI>();
			//CardPileCountUI value3 = Traverse.Create(battleHud).Field("deckUI").GetValue<CardPileCountUI>();
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(value.gameObject, mainMenuScreen.transform.parent);
			//Ponies.Log("Line 102");
			gameObject.name = "GivePonyToggleButton";
			//Ponies.Log("Line 104");

			//Delete unwanted components
			ContentSizeFitter sizeFitter = gameObject.GetComponent<ContentSizeFitter>();
			Destroy(sizeFitter);
			HorizontalLayoutGroup layoutGroup = gameObject.GetComponent<HorizontalLayoutGroup>();
			Destroy(layoutGroup);
			ForceTooltipOnSelect forceTooltip = gameObject.GetComponent<ForceTooltipOnSelect>();
			Destroy(forceTooltip);

			//foreach (Component componentii in gameObject.GetComponents<Component>())
			//{
			//	Ponies.Log(componentii.GetType().ToString());
			//}
			//Output:
			//UnityEngine.RectTransform
			//UnityEngine.UI.HorizontalLayoutGroup
			//UnityEngine.UI.ContentSizeFitter
			//ShinyShoe.GameUISelectableCheckbox
			//ForceTooltipOnSelect

			//GivePonyToggle.DeleteUnwanted(gameObject);
			//Ponies.Log("Line 106");
			RectTransform rectTransform = gameObject.transform as RectTransform;
			//Ponies.Log("Line 108");
			RectTransform rectTransform2 = value.transform as RectTransform;
			//Ponies.Log("Line 110");
			rectTransform.anchorMin = rectTransform2.anchorMin;
			rectTransform.anchorMax = rectTransform2.anchorMax;
			rectTransform.anchoredPosition = rectTransform2.anchoredPosition;
			rectTransform.sizeDelta = rectTransform2.sizeDelta;
			rectTransform.localScale = rectTransform2.localScale;
			//Ponies.Log("Line 115");

			gameObject.transform.position = new Vector3(1140.0f, -454.0f, gameObject.transform.position.z);
			//Ponies.Log("Line 123");

			//Ponies.Log($"x, y, z: {Mathf.LerpUnclamped(value3.transform.position.x, value2.transform.position.x, 1.75f)}, {Mathf.LerpUnclamped(value3.transform.position.y, value2.transform.position.y, -0.1f)}, {gameObject.transform.position.z}");

			gameObject.transform.localScale = new Vector3(1.0f, 1.0f, value.transform.localScale.z);

			//Ponies.Log("Line 128");
			GameUISelectableCheckbox component = gameObject.GetComponent<GameUISelectableCheckbox>();

			_givePonyButton = component;

			//Ponies.Log("Line 130");
			//Traverse.Create(component).Field("inputType").SetValue(0);
			component.isChecked = isGivePonyButtonEnabled;
			//Ponies.Log("Line 133");

			//TextMeshProUGUI textMeshPro = gameObject.AddComponent<TextMeshProUGUI>();
			TextMeshProUGUI textMeshPro = UnityEngine.Object.Instantiate<TextMeshProUGUI>(betterLabel, mainMenuScreen.transform.parent);

			textMeshPro.name = "GivePonyToggleLabel";
			textMeshPro.text = "Give_Pony_Button_Toggle_Text".Localize();
			textMeshPro.transform.position = new Vector3(960.0f, 50.0f, gameObject.transform.position.z);
			textMeshPro.rectTransform.sizeDelta = new Vector2(350.0f, 55.0f);

			_label = textMeshPro;

			// -2293, -1760
			/*
			textMeshPro.margin = new Vector4()
			{
				x = -200.0f,
				y = 0.0f,
				z = 0.0f,
				w = 0.0f
			};
			textMeshPro.characterSpacing = -3.0f;
			textMeshPro.outlineWidth = 0.2f;
			textMeshPro.alignment = TextAlignmentOptions.Left;
			textMeshPro.outlineColor = Color.black;
			textMeshPro.color = Color.white;
			textMeshPro.transform.localScale = new Vector3(1.0f, 1.0f, value.transform.localScale.z);
			*/

			//Ponies.Log("Line 135");


			Image checkmark = Traverse.Create(component).Field("checkMark").GetValue<Image>();

			//I think the secret to making this appear was to attach it to a parent object already present in the scene.
			toggleIMG = Image.Instantiate(checkmark, mainMenuScreen.transform.parent);
			toggleIMG.name = "GivePonyToggleIMG";
			toggleIMG.transform.SetPosition(990f, 230.0f, 0.0f);
			toggleIMG.transform.localScale = new Vector3() { x = 10.0f, y = 10.0f, z = 0.0f };
			
			GivePonyToggle.RefreshSprite(); 
			
			toggleIMG.gameObject.SetActive(true);

			_image = toggleIMG;

			component.gameObject.AddComponent<GivePonyToggle>();
			//Ponies.Log("Line 137");
			component.SetState(GameUISelectableButton.State.Enabled);


			//toggleIMG.sortingLayerID = SortingLayers.CharacterForeground.LayerID();
		}

		public static void SetActive(bool isActive) 
		{
			if (_givePonyButton != null)
			{
				_givePonyButton.gameObject.SetActive(isActive);
			}
			if (_label != null)
			{
				_label.gameObject.SetActive(isActive);
			}
			if (_image != null)
			{
				_image.gameObject.SetActive(isActive);
			}
		}

		/*
		// Token: 0x06000009 RID: 9 RVA: 0x00002308 File Offset: 0x00000508
		private static void DeleteUnwanted(GameObject objRoot)
		{
			List<GameObject> list = new List<GameObject>();
			GivePonyToggle.DeleteUnwanted(objRoot, "", list);
			foreach (GameObject obj in list)
			{
				UnityEngine.Object.Destroy(obj);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002368 File Offset: 0x00000568
		private static void DeleteUnwanted(GameObject obj, string parentPath, List<GameObject> toDelete)
		{
			string text = string.IsNullOrEmpty(parentPath) ? obj.name : (parentPath + "/" + obj.name);
			if (text == "GivePonyToggleButton")
			{
				GivePonyToggle.DeleteUnwantedComponents(obj, new Type[]
				{
					typeof(GameUISelectableCheckbox),
					typeof(Animator)
				});
			}
			else if (!(text == "GivePonyButton/Content") && !(text == "GivePonyButton/Content/Bg"))
			{
				if (text == "GivePonyButton/Content/Label")
				{
					GivePonyToggle.DeleteUnwantedComponents(obj, new Type[]
					{
						typeof(TextMeshProUGUI)
					});
				}
				else
				{
					toDelete.Add(obj);
				}
			}
			foreach (object obj2 in obj.transform)
			{
				GivePonyToggle.DeleteUnwanted(((Transform)obj2).gameObject, text, toDelete);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002468 File Offset: 0x00000668
		private static void DeleteUnwantedComponents(GameObject obj, params Type[] compsWanted)
		{
			foreach (Component component in obj.GetComponents<Component>())
			{
				if (!(component.GetType() == typeof(RectTransform)) && !(component.GetType() == typeof(CanvasRenderer)) && Array.IndexOf<Type>(compsWanted, component.GetType()) < 0)
				{
					UnityEngine.Object.Destroy(component);
				}
			}
		}
		*/

		// Token: 0x04000001 RID: 1
		public static MainMenuScreen _mainMenu;

		// Token: 0x04000002 RID: 2
		public static GameUISelectableCheckbox _givePonyButton;

		public static TextMeshProUGUI _label;

		public static Image _image;

		// Token: 0x04000003 RID: 3
		public Coroutine _coroutine;

		// Myvariable Token: 0x04000004 RID: 4
		//private static bool _hasRestartBattleButton = CollisionAvoider.HasRestartBattleButton();
	}

    [HarmonyPatch(typeof(MainMenuScreen),"SelectButton")]
	public static class HideToggleUI 
	{ 
		public static void Prefix() 
		{
			GivePonyToggle.SetActive(false);
		}
	}

	[HarmonyPatch(typeof(MainMenuScreen), "ResetButtons")]
	public static class ShowToggleUI
	{
		public static void Prefix()
		{
			GivePonyToggle.SetActive(true);
		}
	}
}
