// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern.RuneWizard
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern
{
  public class RuneWizard : Teleporters
  {
    public int AttackTimer;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(172);

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.lavaImmune = true;
      npc.lifeMax *= 4;
      npc.damage /= 2;
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[24] = true;
    }

    public override void AI(NPC npc)
    {
      base.AI(npc);
      if (++this.AttackTimer > 300)
      {
        this.AttackTimer = 0;
        if (FargoSoulsUtil.HostCheck && npc.HasPlayerTarget)
        {
          Vector2 vector2 = Vector2.op_Multiply(((Entity) npc).DirectionFrom(((Entity) Main.player[npc.target]).Center), 8f);
          for (int index1 = 0; index1 < 5; ++index1)
          {
            int index2 = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Utils.RotatedBy(vector2, 2.0 * Math.PI / 5.0 * (double) index1, new Vector2()), 129, 30, 0.0f, Main.myPlayer, 1f, 0.0f, 0.0f);
            if (index2 != Main.maxProjectiles)
              Main.projectile[index2].timeLeft = 300;
          }
        }
      }
      EModeGlobalNPC.Aura(npc, 450f, true, 74, Color.GreenYellow, ModContent.BuffType<HexedBuff>());
      EModeGlobalNPC.Aura(npc, 150f, false, 73, new Color(), ModContent.BuffType<HexedBuff>(), 68);
    }
  }
}
