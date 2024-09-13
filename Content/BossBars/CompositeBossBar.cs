// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.BossBars.CompositeBossBar
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.Champions.Shadow;
using FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.LunarEvents.Stardust;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.BossBars
{
  public class CompositeBossBar : ModBossBar
  {
    private int bossHeadIndex = -1;

    public virtual Asset<Texture2D> GetIconTexture(ref Rectangle? iconFrame)
    {
      return this.bossHeadIndex != -1 ? TextureAssets.NpcHeadBoss[this.bossHeadIndex] : base.GetIconTexture(ref iconFrame);
    }

    public virtual bool? ModifyInfo(
      ref BigProgressBarInfo info,
      ref float life,
      ref float lifeMax,
      ref float shield,
      ref float shieldMax)
    {
      NPC npc = FargoSoulsUtil.NPCExists(info.npcIndexToAimAt, Array.Empty<int>());
      if (npc == null || !((Entity) npc).active)
        return new bool?(false);
      this.bossHeadIndex = npc.GetBossHeadTextureIndex();
      bool flag = true;
      if (npc.ModNPC is FargowiltasSouls.Content.Bosses.TrojanSquirrel.TrojanSquirrel modNpc)
      {
        float num = 0.0f;
        if (modNpc.head != null)
          num += (float) modNpc.head.life;
        if (modNpc.arms != null)
          num += (float) modNpc.arms.life;
        life = (float) npc.life + num;
        lifeMax = (float) (npc.lifeMax + modNpc.lifeMaxHead + modNpc.lifeMaxArms);
      }
      else if (npc.type == ModContent.NPCType<ShadowChampion>())
      {
        float num1 = 0.0f;
        float num2 = 0.0f;
        if ((double) npc.ai[0] == -1.0)
        {
          shield = 1f;
          shieldMax = 1f;
        }
        else
        {
          foreach (NPC npc1 in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && n.type == ModContent.NPCType<ShadowOrbNPC>() && (double) n.ai[0] == (double) ((Entity) npc).whoAmI)))
          {
            ++num2;
            if (!npc1.dontTakeDamage)
              ++num1;
          }
          shield = num1;
          shieldMax = num2;
          if ((double) shield <= 0.0)
            return new bool?();
        }
      }
      else
      {
        if (npc.type == 493)
        {
          int num3 = 0;
          int num4 = 0;
          foreach (NPC npc2 in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && n.type == ModContent.NPCType<StardustMinion>() && (double) n.ai[2] == (double) ((Entity) npc).whoAmI && n.frame.Y == 0)))
          {
            num3 += npc2.life;
            num4 += npc2.lifeMax;
          }
          life = (float) (npc.life + num3);
          lifeMax = (float) (npc.lifeMax + num4);
          return new bool?(true);
        }
        flag = false;
      }
      return new bool?(flag);
    }
  }
}
