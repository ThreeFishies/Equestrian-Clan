using System;
using System.Collections;
using System.Collections.Generic;
using HarmonyLib;
using ShinyShoe;
using ShinyShoe.Audio;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Trainworks.Managers;
using Trainworks.Constants;
using Equestrian.MonsterCards;
using Equestrian.HarmonyPatches;
using Equestrian.Init;
using Equestrian.CardPools;
using Equestrian.Enhancers;

namespace GivePony
{
	// Token: 0x02000004 RID: 4
	public class GivePonyButton : MonoBehaviour
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002071 File Offset: 0x00000271
		private void Start()
		{
			this._battleHud = base.GetComponentInParent<BattleHud>();
			this._givePonyButton = base.GetComponent<GameUISelectableButton>();
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
			if (component.IsGameUIComponent(this._givePonyButton) && mapping.IsID(InputManager.Controls.Clicked) && this._coroutine == null)
			{
				Traverse.Create(this._battleHud).Field("soundManager").GetValue<SoundManager>().PlaySfx("UI_EndTurn", AudioSfxPriority.Normal, false);
				this._coroutine = GlobalMonoBehavior.Inst.StartCoroutine(GivePonyButton.GivePonyCoroutine(this._battleHud));
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000212F File Offset: 0x0000032F
		public static IEnumerator GivePonyCoroutine(BattleHud battleHud)
		{
			//if (Trainworks.Managers.ProviderManager.SaveManager.IsBattleMode())
			//{
				Ponies.Log("Give me a pony.");

			//Thay spawn with multistrike for now. Will replace with a custom upgrade later.
			CardData randomPony = MyCardPools.GivePonyCardPool.GetRandomChoice(RngId.Battle);
			CardManager.AddCardUpgradingInfo addCardUpgradingInfo = new CardManager.AddCardUpgradingInfo();
			//addCardUpgradingInfo.upgradeDatas.Add(ProviderManager.SaveManager.GetAllGameData().FindEnhancerData("0016b165-11a9-4a26-8837-3b2895bc39f8").GetEffects()[0].GetParamCardUpgradeData());
			addCardUpgradingInfo.upgradeDatas.Add(Playstone.PlaystoneData.GetEffects()[0].GetParamCardUpgradeData());
			addCardUpgradingInfo.upgradeDatas.Add(VanillaUnitEnhancers.GetRandomEnhancer(RngId.Battle));

			CardManager value4 = Traverse.Create(battleHud).Field("cardManager").GetValue<CardManager>();
				value4.AddCard(randomPony, CardPile.HandPile, 1, 1,false, false, addCardUpgradingInfo);
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
		public static void Create(BattleHud battleHud)
		{
			if (Traverse.Create(Traverse.Create(battleHud).Field("combatManager").GetValue<CombatManager>()).Field("saveManager").GetValue<SaveManager>().IsBattleMode())
			{
				return;
			}
			Component value = Traverse.Create(battleHud).Field("endTurnButton").GetValue<EndTurnUI>();
			EnergyUI value2 = Traverse.Create(battleHud).Field("energyUI").GetValue<EnergyUI>();
			CardPileCountUI value3 = Traverse.Create(battleHud).Field("deckUI").GetValue<CardPileCountUI>();
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(value.gameObject, value2.transform.parent);
			gameObject.name = "GivePonyButton";
			GivePonyButton.DeleteUnwanted(gameObject);
			RectTransform rectTransform = gameObject.transform as RectTransform;
			RectTransform rectTransform2 = value2.transform as RectTransform;
			rectTransform.anchorMin = rectTransform2.anchorMin;
			rectTransform.anchorMax = rectTransform2.anchorMax;
			rectTransform.anchoredPosition = rectTransform2.anchoredPosition;
			rectTransform.sizeDelta = rectTransform2.sizeDelta;

			//Need to reposition to aviod the Restart Battle Button
			float MoveX = 0.0f;
			float MoveY = 0.0f;
			float MoveZ = 0.0f;

			//I have no idea what these values should be. Just try stuff and see what happens.
			if (_hasRestartBattleButton) 
			{
				//This seems pretty decent
				MoveX = 10.0f;
				MoveY = -5.0f;
				MoveZ = 0.0f;
			}

			gameObject.transform.position = new Vector3(Mathf.LerpUnclamped(value3.transform.position.x, value2.transform.position.x, 1.75f)+MoveX, Mathf.LerpUnclamped(value3.transform.position.y, value2.transform.position.y, -0.1f)+MoveY, gameObject.transform.position.z+MoveZ);

			//Ponies.Log($"x, y, z: {Mathf.LerpUnclamped(value3.transform.position.x, value2.transform.position.x, 1.75f)}, {Mathf.LerpUnclamped(value3.transform.position.y, value2.transform.position.y, -0.1f)}, {gameObject.transform.position.z}");

			gameObject.transform.localScale = new Vector3(0.6f, 0.6f, gameObject.transform.localScale.z);
			GameUISelectableButton component = gameObject.GetComponent<GameUISelectableButton>();
			Traverse.Create(component).Field("inputType").SetValue(0);
			gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Give Pony";
			component.gameObject.AddComponent<GivePonyButton>();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002308 File Offset: 0x00000508
		private static void DeleteUnwanted(GameObject objRoot)
		{
			List<GameObject> list = new List<GameObject>();
			GivePonyButton.DeleteUnwanted(objRoot, "", list);
			foreach (GameObject obj in list)
			{
				UnityEngine.Object.Destroy(obj);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002368 File Offset: 0x00000568
		private static void DeleteUnwanted(GameObject obj, string parentPath, List<GameObject> toDelete)
		{
			string text = string.IsNullOrEmpty(parentPath) ? obj.name : (parentPath + "/" + obj.name);
			if (text == "GivePonyButton")
			{
				GivePonyButton.DeleteUnwantedComponents(obj, new Type[]
				{
					typeof(GameUISelectableButton),
					typeof(Animator)
				});
			}
			else if (!(text == "GivePonyButton/Content") && !(text == "GivePonyButton/Content/Bg"))
			{
				if (text == "GivePonyButton/Content/Label")
				{
					GivePonyButton.DeleteUnwantedComponents(obj, new Type[]
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
				GivePonyButton.DeleteUnwanted(((Transform)obj2).gameObject, text, toDelete);
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

		// Token: 0x04000001 RID: 1
		private BattleHud _battleHud;

		// Token: 0x04000002 RID: 2
		private GameUISelectableButton _givePonyButton;

		// Token: 0x04000003 RID: 3
		private Coroutine _coroutine;

		// Myvariable Token: 0x04000004 RID: 4
		private static bool _hasRestartBattleButton = CollisionAvoider.HasRestartBattleButton();
	}
}
