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
using GivePony;

namespace Equestrian.Util
{
    // Token: 0x02000004 RID: 4
    public class ClassSelectionArrowButton : MonoBehaviour
    {
        public bool isRight;
        public bool isMain;
        public bool isActive;
        public GameUISelectableButton button;

        // Token: 0x06000004 RID: 4 RVA: 0x00002071 File Offset: 0x00000271
        private void Start()
        {
            this._classSelectionScreen = base.GetComponentInParent<ClassSelectionScreen>();
            this._arrowButton = base.GetComponent<GameUISelectableButton>();
            UISignals.GameUITriggered.AddListener(new Action<CoreInputControlMapping, IGameUIComponent>(this.OnGameUITriggered));
        }

        public void Init(bool isRight, bool isMain, bool isActive, GameUISelectableButton button) 
        {
            this.isRight = isRight;
            this.isMain = isMain;
            this.isActive = isActive;
            this.button = button;

            if (this.button != null) 
            {
                if (isActive) 
                {
                    button.SetState(GameUISelectableButton.State.Enabled);
                }
                else 
                {
                    button.SetState(GameUISelectableButton.State.Disabled);
                }
            }
        }

        // Token: 0x06000005 RID: 5 RVA: 0x000020A1 File Offset: 0x000002A1
        private void OnDestroy()
        {
            UISignals.GameUITriggered.RemoveListener(new Action<CoreInputControlMapping, IGameUIComponent>(this.OnGameUITriggered));
        }

        // Token: 0x06000006 RID: 6 RVA: 0x000020BC File Offset: 0x000002BC
        private void OnGameUITriggered(CoreInputControlMapping mapping, IGameUIComponent component)
        {
            if (component.IsGameUIComponent(this._arrowButton) && mapping.IsID(InputManager.Controls.Clicked) && this._coroutine == null)
            {
                Traverse.Create(this._classSelectionScreen).Field("soundManager").GetValue<SoundManager>().PlaySfx("UI_Click", AudioSfxPriority.Normal, false);
                this._coroutine = GlobalMonoBehavior.Inst.StartCoroutine(this.RefreshUICoroutine(this._classSelectionScreen));
            }
        }

        // Token: 0x06000007 RID: 7 RVA: 0x0000212F File Offset: 0x0000032F
        public IEnumerator RefreshUICoroutine(ClassSelectionScreen classSelectionScreen)
        {
            //Ponies.Log("You clicked on a button.");

            if (this.isMain && this.isRight) 
            {
                CustomClassSelectionUIManager.MainOffset += 1;
                CustomClassSelectionUIManager.RefreshUI(classSelectionScreen, true);
            }
            else if (this.isMain && !this.isRight) 
            {
                CustomClassSelectionUIManager.MainOffset -= 1;
                CustomClassSelectionUIManager.RefreshUI(classSelectionScreen, true);
            }
            else if (!this.isMain && this.isRight) 
            {
                CustomClassSelectionUIManager.SubOffset += 1;
                CustomClassSelectionUIManager.RefreshUI(classSelectionScreen, false);
            }
            else if (!this.isMain && !this.isRight) 
            {
                CustomClassSelectionUIManager.SubOffset -= 1;
                CustomClassSelectionUIManager.RefreshUI(classSelectionScreen, false);
            }

            yield break;
        }

        // Token: 0x06000008 RID: 8 RVA: 0x00002140 File Offset: 0x00000340
        public static GameUISelectableButton Create(ClassSelectionScreen classSelectionScreen, GameUISelectableButton toClone, string name, bool isRight, bool isMain, bool isActive)
        {
            ClassSelectionIconUI classSelectionUI;

            if (isMain)
            {
                classSelectionUI = AccessTools.Field(typeof(ClassSelectionScreen), "mainClassSelectionUI").GetValue(classSelectionScreen) as ClassSelectionIconUI;
            }
            else
            {
                classSelectionUI = AccessTools.Field(typeof(ClassSelectionScreen), "subClassSelectionUI").GetValue(classSelectionScreen) as ClassSelectionIconUI;
            }
            //if (Traverse.Create(Traverse.Create(battleHud).Field("combatManager").GetValue<CombatManager>()).Field("saveManager").GetValue<SaveManager>().IsBattleMode())
            //{
            //    return;
            //}
            //Component value = Traverse.Create(battleHud).Field("endTurnButton").GetValue<EndTurnUI>();
            //EnergyUI value2 = Traverse.Create(battleHud).Field("energyUI").GetValue<EnergyUI>();
            //CardPileCountUI value3 = Traverse.Create(battleHud).Field("deckUI").GetValue<CardPileCountUI>();
            GameUISelectableButton gameObject = UnityEngine.Object.Instantiate<GameUISelectableButton>(toClone, classSelectionUI.transform);
            //GameUISelectableButton gameObject = UnityEngine.Object.Instantiate<GameUISelectableButton>(toClone, toClone.GetComponentInParent<Transform>());
            gameObject.name = name;
            //ClassSelectionArrowButton.DeleteUnwanted(gameObject.gameObject, name);
            RectTransform rectTransform = gameObject.transform as RectTransform;
            RectTransform rectTransform2 = toClone.transform as RectTransform;
            rectTransform.anchorMin = rectTransform2.anchorMin;
            rectTransform.anchorMax = rectTransform2.anchorMax;
            //rectTransform.anchoredPosition = rectTransform2.anchoredPosition;
            rectTransform.sizeDelta = rectTransform2.sizeDelta;
            rectTransform.localScale = new Vector3(0.8f, 0.8f, 1.0f); //Shrink the button.
            RectTransform rectTransform3 = gameObject.GetComponent<Graphic2DInvisRaycastTarget>().transform as RectTransform;
            rectTransform3.sizeDelta = new Vector2(50.0f, 50.0f); //Scaling this component fixes the squashed button problem, including the selection area.

            if (!isRight) 
            {
                //It helps if the left arrow buttons are actually on the left side.
                gameObject.transform.SetSiblingIndex(0);
            }

            //gameObject.image = toClone.image;

            //Need to reposition to aviod the Restart Battle Button
            float MoveX = 0.0f;
            float MoveY = 0.0f;
            float MoveZ = 0.0f;

            //I have no idea what these values should be. Just try stuff and see what happens.
            //Values found through trial and error, mostly. The runtime Unity object allows you to change the numbers for immediate effect, making this much easier.
            //Never mind. Attaching the buttons to the ClassSelectionUI automatically positions them.
            //if (isMain && isRight)
            //{
            //    //Top right (main clan)
            //    MoveX = 592.0f;
            //    MoveY = 1025.0f;
            //    MoveZ = 0.0f;
            //}
            //else if (isMain && !isRight) 
            //{
            //    //Top left (main clan)
            //    MoveX = 34.0f;
            //    MoveY = 1025.0f;
            //    MoveZ = 0.0f;
            //}
            //else if (!isMain && isRight) 
            //{
            //    //Middle right (sub clan)
            //    MoveX = 592.0f;
            //    MoveY = 685.0f;
            //    MoveZ = 0.0f;
            //}
            //else if (!isMain && !isRight) 
            //{
            //    //Middle left (sub clan)
            //    MoveX = 34.0f;
            //    MoveY = 685.0f;
            //    MoveZ = 0.0f;
            //}

            //Default location.
            gameObject.transform.position = new Vector3(toClone.transform.position.x + MoveX, toClone.transform.position.y + MoveY, gameObject.transform.position.z + MoveZ);
            
            //Ponies.Log($"x, y, z: {Mathf.LerpUnclamped(value3.transform.position.x, value2.transform.position.x, 1.75f)}, {Mathf.LerpUnclamped(value3.transform.position.y, value2.transform.position.y, -0.1f)}, {gameObject.transform.position.z}");

            //gameObject.transform.localScale = new Vector3(0.6f, 0.6f, gameObject.transform.localScale.z);
            //GameUISelectableButton component = gameObject.GetComponent<GameUISelectableButton>();
            Traverse.Create(gameObject).Field("inputType").SetValue(0);
            ClassSelectionArrowButton arrowButton = gameObject.gameObject.AddComponent<ClassSelectionArrowButton>();
            arrowButton.Init(isRight, isMain, isActive, gameObject);

            return gameObject;
        }

        /*
        private static void DeleteUnwanted(GameObject objRoot, string name)
        {
            List<GameObject> list = new List<GameObject>();
            ClassSelectionArrowButton.DeleteUnwanted(objRoot, "", list, name);
            foreach (GameObject obj in list)
            {
                UnityEngine.Object.Destroy(obj);
            }
        }

        // Token: 0x0600000A RID: 10 RVA: 0x00002368 File Offset: 0x00000568
        private static void DeleteUnwanted(GameObject obj, string parentPath, List<GameObject> toDelete, string name)
        {
            string text = string.IsNullOrEmpty(parentPath) ? obj.name : (parentPath + "/" + obj.name);
            if (text == name)
            {
                ClassSelectionArrowButton.DeleteUnwantedComponents(obj, new Type[]
                {
                    typeof(GameUISelectableButton),
                    typeof(Animator)
                });
            }
            else if (!(text == name + "/Content") && !(text == name + "/Content/Bg"))
            {
                if (text == name + "/Content/Label")
                {
                    ClassSelectionArrowButton.DeleteUnwantedComponents(obj, new Type[]
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
                ClassSelectionArrowButton.DeleteUnwanted(((Transform)obj2).gameObject, text, toDelete, name);
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
        private ClassSelectionScreen _classSelectionScreen;

        // Token: 0x04000002 RID: 2
        private GameUISelectableButton _arrowButton;

        // Token: 0x04000003 RID: 3
        private Coroutine _coroutine;

        // Myvariable Token: 0x04000004 RID: 4
        //private static bool _hasRestartBattleButton = CollisionAvoider.HasRestartBattleButton();
    }
}