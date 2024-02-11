using System;
using System.Collections;
using System.Collections.Generic;
using Equestrian.Init;
using ShinyShoe;
using static CharacterState;

// Token: 0x020000A6 RID: 166
namespace CustomEffects
{

    public sealed class CustomCardEffectEnchantMareALeeCoach : CardEffectBase, ICardEnchantEffect
    {
        // Token: 0x1700006D RID: 109
        // (get) Token: 0x06000752 RID: 1874 RVA: 0x0002205C File Offset: 0x0002025C
        private Dictionary<CharacterState, CustomCardEffectEnchantMareALeeCoach.EnchantedState> EnchantedTargets
        {
            get
            {
                if (!(this.saveManager != null))
                {
                    return this._primaryEnchantedTargets;
                }
                if (this.saveManager.PreviewMode)
                {
                    return this._previewEnchantedTargets;
                }
                return this._primaryEnchantedTargets;
            }
        }

        // Token: 0x06000753 RID: 1875 RVA: 0x0002208D File Offset: 0x0002028D
        public override void Setup(CardEffectState cardEffectState)
        {
            this._primaryEnchantedTargets = new Dictionary<CharacterState, CustomCardEffectEnchantMareALeeCoach.EnchantedState>();
            this._previewEnchantedTargets = new Dictionary<CharacterState, CustomCardEffectEnchantMareALeeCoach.EnchantedState>();
            this.statusEffects = cardEffectState.GetParamStatusEffectStackData(); // .GetSourceCardEffectData().GetParamStatusEffects(); //CardEffectAddStatusEffect.GetStatusEffectStack(cardEffectState.GetSourceCardEffectData(), cardEffectState, null, false);
            this.cachedState = cardEffectState;
            cardEffectState.SetShouldOverrideTriggerUI(true);

            //Ponies.Log("Debug data for CustomCardEffectEnchantMareALeeCoach:");
            //Ponies.Log($"Status Effect Count: {this.statusEffects.Length}");
            //for (int ii = 0; ii < this.statusEffects.Length; ii++) 
            //{
            //    Ponies.Log($"Status[{ii}]: {this.statusEffects[ii].statusId} {this.statusEffects[ii].count}.");
            //}
        }

        // Token: 0x06000754 RID: 1876 RVA: 0x000220C7 File Offset: 0x000202C7
        public void SetEnchanterCharacter(CharacterState enchanterCharacter, SaveManager saveManager, MonsterManager monsterManager, HeroManager heroManager)
        {
            this.enchanterCharacter = enchanterCharacter;
            this.saveManager = saveManager;
            this.monsterManager = monsterManager;
            this.heroManager = heroManager;
            this.enchanterCharacter.AddCharacterRemovedSignal(new CoroutineSignal.SignalCallback(this.OnEnchanterDeath), true);
        }

        // Token: 0x06000755 RID: 1877 RVA: 0x00022100 File Offset: 0x00020300
        private void ApplyEffectInternal()
        {
            if (this.enchanterCharacter == null)
            {
                return;
            }
            CardEffectParams cardEffectParams = new CardEffectParams
            {
                selfTarget = this.enchanterCharacter,
                monsterManager = this.monsterManager,
                heroManager = this.heroManager,
                selectedRoom = this.enchanterCharacter.GetCurrentRoomIndex()
            };
            TargetHelper.CollectTargets(this.cachedState, cardEffectParams, this.saveManager.PreviewMode);
            if (this.saveManager.PreviewMode)
            {
                this._previewEnchantedTargets.Clear();
                foreach (KeyValuePair<CharacterState, CustomCardEffectEnchantMareALeeCoach.EnchantedState> keyValuePair in this._primaryEnchantedTargets)
                {
                    this._previewEnchantedTargets.Add(keyValuePair.Key, new CustomCardEffectEnchantMareALeeCoach.EnchantedState(keyValuePair.Value));
                }
            }
            foreach (CharacterState characterState in cardEffectParams.targets)
            {
                if (!this.EnchantedTargets.ContainsKey(characterState) && characterState != this.enchanterCharacter)
                {
                    this.EnchantedTargets.Add(characterState, new CustomCardEffectEnchantMareALeeCoach.EnchantedState());
                }
            }
            bool flag = false;
            foreach (KeyValuePair<CharacterState, CustomCardEffectEnchantMareALeeCoach.EnchantedState> keyValuePair2 in this.EnchantedTargets)
            {
                CharacterState key = keyValuePair2.Key;
                keyValuePair2.Value.nextStateAction = (this.IsEnchantmentValidForTarget(key) ? CustomCardEffectEnchantMareALeeCoach.EnchanterStateNextAction.AddStatusEffect : CustomCardEffectEnchantMareALeeCoach.EnchanterStateNextAction.RemoveStatusEffect);
                if (keyValuePair2.Value.nextStateAction == CustomCardEffectEnchantMareALeeCoach.EnchanterStateNextAction.AddStatusEffect && !keyValuePair2.Value.isEnchanted)
                {
                    flag = true;
                }
            }
            BalanceData.TimingData activeTiming = this.saveManager.GetActiveTiming();
            if (flag && !this.saveManager.PreviewMode)
            {
                this.enchanterCharacter.DoMovementAttacking(activeTiming.UnitAttackWindUpDuration, activeTiming.UnitAttackDuration, null, CharacterUI.Anim.Attack);
            }
            foreach (KeyValuePair<CharacterState, CustomCardEffectEnchantMareALeeCoach.EnchantedState> keyValuePair3 in this.EnchantedTargets)
            {
                CharacterState key2 = keyValuePair3.Key;
                CustomCardEffectEnchantMareALeeCoach.EnchantedState value = keyValuePair3.Value;
                if (!key2.IsDead && !key2.IsDestroyed)
                {
                    if (value.nextStateAction == CustomCardEffectEnchantMareALeeCoach.EnchanterStateNextAction.AddStatusEffect && !value.isEnchanted)
                    {
                        CharacterState.AddStatusEffectParams addStatusEffectParams = new CharacterState.AddStatusEffectParams
                        {
                            sourceRelicState = cardEffectParams.sourceRelic,
                            fromEffectType = typeof(CustomCardEffectEnchantMareALeeCoach),
                            sourceIsHero = (key2.GetTeamType() == Team.Type.Heroes)
                        };

                        if (this.statusEffects.Length > 0)
                        {
                            for (int ii = 0; ii < this.statusEffects.Length; ii++)
                            {
                                key2.AddStatusEffect(this.statusEffects[ii].statusId, this.statusEffects[ii].count, addStatusEffectParams);
                            }
                        }
                        value.isEnchanted = true;
                    }
                    else if (value.nextStateAction == CustomCardEffectEnchantMareALeeCoach.EnchanterStateNextAction.RemoveStatusEffect && value.isEnchanted)
                    {
                        if (this.statusEffects.Length > 0)
                        {
                            for (int ii = 0; ii < this.statusEffects.Length; ii++)
                            {
                                key2.RemoveStatusEffect(this.statusEffects[ii].statusId, false, this.statusEffects[ii].count, !this.saveManager.PreviewMode, cardEffectParams.sourceRelic, typeof(CustomCardEffectEnchantMareALeeCoach));
                            }
                        }
                        value.isEnchanted = false;
                    }
                    value.nextStateAction = CustomCardEffectEnchantMareALeeCoach.EnchanterStateNextAction.NoAction;
                }
            }
        }

        // Token: 0x06000756 RID: 1878 RVA: 0x0002247C File Offset: 0x0002067C
        private bool IsEnchantmentValidForTarget(CharacterState target)
        {
            CharacterState characterState = this.enchanterCharacter;
            if (characterState == null || !characterState.IsDestroyed)
            {
                CharacterState characterState2 = this.enchanterCharacter;
                if (characterState2 != null && characterState2.IsAlive && (target == null || !target.IsDestroyed) && target != null && target.IsAlive && this.enchanterCharacter != target)
                {
                    return this.enchanterCharacter.GetCurrentRoomIndex() == target.GetCurrentRoomIndex();
                }
            }
            return false;
        }

        // Token: 0x06000757 RID: 1879 RVA: 0x000224EA File Offset: 0x000206EA
        private IEnumerator OnEnchanterDeath()
        {
            this.ApplyEffectInternal();
            yield break;
        }

        // Token: 0x06000758 RID: 1880 RVA: 0x000224F9 File Offset: 0x000206F9
        public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
        {
            this.ApplyEffectInternal();
            yield break;
        }

        // Token: 0x06000759 RID: 1881 RVA: 0x00022508 File Offset: 0x00020708
        public void OnSpawnPointsChanged()
        {
            this.ApplyEffectInternal();
        }

        // Token: 0x0600075A RID: 1882 RVA: 0x00022510 File Offset: 0x00020710
        public override void GetTooltipsStatusList(CardEffectState cardEffectState, ref List<string> outStatusIdList)
        {
            //Ponies.Log("Generating statusID list for tooltips (CustomCardEffectEnchantMareALeeCoach).");

            foreach (StatusEffectStackData statusEffectStackData in cardEffectState.GetSourceCardEffectData().GetParamStatusEffectStackData())
            {
                //Ponies.Log($"Adding: {statusEffectStackData.statusId}");
                outStatusIdList.Add(statusEffectStackData.statusId);
            }
        }

        // Token: 0x0600075B RID: 1883 RVA: 0x00022520 File Offset: 0x00020720
        public static void GetTooltipsStatusList(CardEffectData cardEffectData, ref List<string> outStatusIdList)
        {
            foreach (StatusEffectStackData statusEffectStackData in cardEffectData.GetParamStatusEffects())
            {
                outStatusIdList.Add(statusEffectStackData.statusId);
            }
        }


        // Token: 0x0400044F RID: 1103
        private Dictionary<CharacterState, CustomCardEffectEnchantMareALeeCoach.EnchantedState> _primaryEnchantedTargets;

        // Token: 0x04000450 RID: 1104
        private Dictionary<CharacterState, CustomCardEffectEnchantMareALeeCoach.EnchantedState> _previewEnchantedTargets;

        // Token: 0x04000451 RID: 1105
        private CardEffectState cachedState;

        // Token: 0x04000452 RID: 1106
        private CharacterState enchanterCharacter;

        // Token: 0x04000453 RID: 1107
        private StatusEffectStackData[] statusEffects;

        // Token: 0x04000454 RID: 1108
        private SaveManager saveManager;

        // Token: 0x04000455 RID: 1109
        private MonsterManager monsterManager;

        // Token: 0x04000456 RID: 1110
        private HeroManager heroManager;

        // Token: 0x02000C01 RID: 3073
        private enum EnchanterStateNextAction
        {
            // Token: 0x04003FA2 RID: 16290
            NoAction,
            // Token: 0x04003FA3 RID: 16291
            AddStatusEffect,
            // Token: 0x04003FA4 RID: 16292
            RemoveStatusEffect
        }

        // Token: 0x02000C02 RID: 3074
        private class EnchantedState
        {
            // Token: 0x06006468 RID: 25704 RVA: 0x00002B1B File Offset: 0x00000D1B
            public EnchantedState()
            {
            }

            // Token: 0x06006469 RID: 25705 RVA: 0x001720AF File Offset: 0x001702AF
            public EnchantedState(CustomCardEffectEnchantMareALeeCoach.EnchantedState other)
            {
                this.isEnchanted = other.isEnchanted;
                this.nextStateAction = other.nextStateAction;
            }

            // Token: 0x0600646A RID: 25706 RVA: 0x001720CF File Offset: 0x001702CF
            public override string ToString()
            {
                return string.Format("(Applied? {0} Action? {1})", this.isEnchanted, this.nextStateAction);
            }

            // Token: 0x04003FA5 RID: 16293
            public bool isEnchanted;

            // Token: 0x04003FA6 RID: 16294
            public CustomCardEffectEnchantMareALeeCoach.EnchanterStateNextAction nextStateAction;
        }
    }
}