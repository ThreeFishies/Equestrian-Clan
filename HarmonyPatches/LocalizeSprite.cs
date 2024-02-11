/*
using System;
using UnityEngine;
using UnityEngine.UI;

namespace I2.Loc
{
    // Token: 0x02000ACB RID: 2763
    public class LocalizeTarget_UnityUI_Sprite : LocalizeTarget<Sprite>
    {
        // Token: 0x06005E0D RID: 24077 RVA: 0x0015846D File Offset: 0x0015666D
        static LocalizeTarget_UnityUI_Sprite()
        {
            LocalizeTarget_UnityUI_Sprite.AutoRegister();
        }

        // Token: 0x06005E0E RID: 24078 RVA: 0x00158474 File Offset: 0x00156674
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void AutoRegister()
        {
            LocalizationManager.RegisterTarget(new LocalizeTargetDesc_Type<Sprite, LocalizeTarget_UnityUI_Sprite>
            {
                Name = "Sprite",
                Priority = 100
            });
        }

        // Token: 0x06005E0F RID: 24079 RVA: 0x0000C623 File Offset: 0x0000A823
        public override bool CanUseSecondaryTerm()
        {
            return false;
        }

        // Token: 0x06005E10 RID: 24080 RVA: 0x0000C623 File Offset: 0x0000A823
        public override bool AllowMainTermToBeRTL()
        {
            return false;
        }

        // Token: 0x06005E11 RID: 24081 RVA: 0x0000C623 File Offset: 0x0000A823
        public override bool AllowSecondTermToBeRTL()
        {
            return false;
        }

        // Token: 0x06005E12 RID: 24082 RVA: 0x00158493 File Offset: 0x00156693
        public override eTermType GetPrimaryTermType(Localize cmp)
        {
            if (!(this.mTarget == null))
            {
                return eTermType.Sprite;
            }
            return eTermType.Texture;
        }

        // Token: 0x06005E13 RID: 24083 RVA: 0x0000C623 File Offset: 0x0000A823
        public override eTermType GetSecondaryTermType(Localize cmp)
        {
            return eTermType.Text;
        }

        // Token: 0x06005E14 RID: 24084 RVA: 0x001584AC File Offset: 0x001566AC
        public override void GetFinalTerms(Localize cmp, string Main, string Secondary, out string primaryTerm, out string secondaryTerm)
        {
            primaryTerm = (this.mTarget ? this.mTarget.name : "");
            if (this.mTarget != null && this.mTarget.name != primaryTerm)
            {
                primaryTerm = primaryTerm + "." + this.mTarget.name;
            }
            secondaryTerm = null;
        }

        // Token: 0x06005E15 RID: 24085 RVA: 0x00158538 File Offset: 0x00156738
        public override void DoLocalize(Localize cmp, string mainTranslation, string secondaryTranslation)
        {
            Sprite sprite = this.mTarget;
            if (sprite == null || sprite.name != mainTranslation)
            {
                this.mTarget = cmp.FindTranslatedObject<Sprite>(mainTranslation);
            }
        }
    }
}
*/