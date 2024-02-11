using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Trainworks.Builders;
using Trainworks.Managers;
using Trainworks.Enums;
using Trainworks.Constants;
using Equestrian.Init;
using Equestrian;
using Equestrian.CardPools;
using Equestrian.Enhancers;

namespace CustomEffects 
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    // Token: 0x0200009E RID: 158
    public sealed class CustomEffectKablooie : CardEffectBase
    {
        // Token: 0x06000727 RID: 1831 RVA: 0x00021B68 File Offset: 0x0001FD68
        public override void Setup(CardEffectState cardEffectState)
        {
            base.Setup(cardEffectState);
            this.damageMultiplier = 1;
            this.explosionDamage = cardEffectState.GetParamInt();
            this.chanceToExplode = cardEffectState.GetParamMinInt();
            lastTestedCharacter = null;

            string paramStr = cardEffectState.GetParamStr();
            foreach (SubtypeData subtypeData in SubtypeManager.AllData)
            {
                if (subtypeData.Key == paramStr)
                {
                    this.ignoreSubtype = subtypeData;
                    break;
                }
            }
        }

        // Token: 0x06000728 RID: 1832 RVA: 0x00021BE4 File Offset: 0x0001FDE4
        private void CollectSourceTargets(CardEffectParams cardEffectParams)
        {
            this.toProcessCharacters.Clear();
            this.toProcessCharacters.Add(cardEffectParams.selfTarget);
            for (int i = this.toProcessCharacters.Count - 1; i >= 0; i--)
            {
                CharacterState characterState = this.toProcessCharacters[i];
                if (characterState.GetTeamType() != Team.Type.Monsters)
                {
                    this.toProcessCharacters.RemoveAt(i);
                }
                else if (characterState.GetCharacterManager().DoesCharacterPassSubtypeCheck(characterState, this.ignoreSubtype))
                {
                    this.toProcessCharacters.RemoveAt(i);
                }
                else if (characterState.IsChampion() || characterState.IsMiniboss() || characterState.IsOuterTrainBoss() || characterState.IsPyreHeart())
                {
                    this.toProcessCharacters.RemoveAt(i);
                }
            }
        }

        // Token: 0x06000729 RID: 1833 RVA: 0x00021C98 File Offset: 0x0001FE98
        private void CollectTargetTargets(CardEffectParams cardEffectParams)
        {
            this.toProcessCharacters.Clear();
            if (this.boomRoom == null) { return; }
            ProviderManager.CombatManager.GetAllCharactersInRoom(toProcessCharacters, this.boomRoom);

            //this.toProcessCharacters.AddRange(cardEffectParams.targets);
        }

        // Token: 0x0600072A RID: 1834 RVA: 0x00021CF9 File Offset: 0x0001FEF9
        public override bool TestEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
        {
            //if (this.chanceToExplode < 100) { return false; }

            this.CollectSourceTargets(cardEffectParams);
            return this.toProcessCharacters.Count > 0;
        }

        // Token: 0x0600072B RID: 1835 RVA: 0x00021D10 File Offset: 0x0001FF10
        private int GetDamageAmount(CardEffectState cardEffectState, CharacterState selfTarget)
        {
            int num = explosionDamage;
            if (cardEffectState.GetUseStatusEffectStackMultiplier())
            {
                int statusEffectStacks = selfTarget.GetStatusEffectStacks(cardEffectState.GetStatusEffectStackMultiplier());
                this.damageMultiplier = statusEffectStacks;
                num *= statusEffectStacks;
            }
            return num;
        }

        // Token: 0x0600072C RID: 1836 RVA: 0x00021D3E File Offset: 0x0001FF3E
        public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
        {
            if (ProviderManager.SaveManager.PreviewMode) 
            {
                //Ponies.Log("No boom in preview mode.");
                yield break;
            }

            this.CollectSourceTargets(cardEffectParams);

            if (toProcessCharacters.Count == 0) 
            {
                //Ponies.Log("Champion: Did not explode.");
                yield break;
            }

            if (lastTestedCharacter == this.toProcessCharacters[0]) 
            {
                //Ponies.Log("Skipping duplicate unit.");
                //Upgrade filter seems to be working. The effect isn't being erroneously duplicated anymore. Less lag too.
                yield break;
            }

            lastTestedCharacter = this.toProcessCharacters[0];

            int num = (this.chanceToExplode < 100) ? RandomManager.Range(0, 100, RngId.Battle) : 0;
            //Ponies.Log("Random number: " + num + "  Chance to explode: " + this.chanceToExplode);
            if (num > this.chanceToExplode)
            {
                Ponies.Log("RNG: Did not explode!");
                yield break;
            }

            //Ponies.Log("KA-BOOM!");
            int damage = this.toProcessCharacters.Count * this.damageMultiplier * this.explosionDamage;
            foreach (CharacterState characterState in this.toProcessCharacters)
            {
                if (Ponies.HallowedHallsInEffect)
                {
                    yield return cardEffectParams.relicManager.ApplyOnDiscardRelicEffects(new CardManager.DiscardCardParams
                    {
                        discardCard = characterState.GetSpawnerCard(),
                        wasPlayed = true,
                        characterSummoned = characterState
                    });
                    cardEffectParams.monsterManager.SetOverrideIgnoreCapacity(false);
                    CardManager cardManager = cardEffectParams.cardManager;
                    cardManager.MoveToStandByPile(characterState.GetSpawnerCard(), false, false, new RemoveFromStandByCondition(() => cardManager.CheckMonsterRemoveFromStandByCondition(characterState), null), null, HandUI.DiscardEffect.None);
                    Ponies.Log("Fixing iteration for " + characterState.GetName());
                    //CS$<> 8__locals2 = null;
                    //spawnPoint = null;
                    //card = null;
                }

                boomRoom = null;
                boomRoom = characterState.GetSpawnPoint(false).GetRoomOwner();
                yield return characterState.Sacrifice(null, false, false);
            }
            //List<CharacterState>.Enumerator enumerator = default(List<CharacterState>.Enumerator);
            yield return CoreUtil.WaitForSecondsOrBreak(cardEffectParams.saveManager.GetActiveTiming().CharacterTriggerPostFire);
            this.CollectTargetTargets(cardEffectParams);
            foreach (CharacterState target in this.toProcessCharacters)
            {
                yield return cardEffectParams.combatManager.ApplyDamageToTarget(damage, target, new CombatManager.ApplyDamageToTargetParameters
                {
                    vfxAtLoc = cardEffectState.GetAppliedVFX(),
                    showDamageVfx = cardEffectParams.allowPlayingDamageVfx
                });
            }
            //enumerator = default(List<CharacterState>.Enumerator);
            yield break;
            //yield break;
        }

        public int chanceToExplode;

        public int explosionDamage;

        public int damageMultiplier;

        public SubtypeData ignoreSubtype;

        public RoomState boomRoom;

        public static CharacterState lastTestedCharacter;

        // Token: 0x04000448 RID: 1096
        private List<CharacterState> toProcessCharacters = new List<CharacterState>();
    }

}