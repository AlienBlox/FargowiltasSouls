// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern.CrimsonMimic
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Cavern
{
  public class CrimsonMimic : BiomeMimics
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(474);

    public override void AI(NPC npc)
    {
      if (this.CanDoAttack)
      {
        NPC npc1 = npc;
        ((Entity) npc1).position = Vector2.op_Subtraction(((Entity) npc1).position, Vector2.op_Multiply(((Entity) npc).velocity, !npc.HasValidTarget || !Main.player[npc.target].ZoneRockLayerHeight ? 0.5f : 0.8f));
        if (this.IndividualAttackTimer > 10)
        {
          this.IndividualAttackTimer = 0;
          if (npc.HasValidTarget)
          {
            for (int index1 = 0; index1 < 8; ++index1)
            {
              Vector2 center = ((Entity) npc).Center;
              center.X += (float) ((double) Math.Sign(((Entity) npc).direction) * 600.0 * (double) (this.AttackCycleTimer + 60) / 360.0);
              center.X += Utils.NextFloat(Main.rand, -100f, 100f);
              center.Y += Utils.NextFloat(Main.rand, -450f, 450f);
              float num = 60f;
              Vector2 vector2 = Vector2.op_Subtraction(center, ((Entity) npc).Center);
              vector2.X /= num;
              vector2.Y = (float) ((double) vector2.Y / (double) num - 0.25 * (double) num);
              if (FargoSoulsUtil.HostCheck)
              {
                int index2 = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, ModContent.ProjectileType<GoldenShowerWOF>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.8f), 0.0f, Main.myPlayer, num, 0.0f, 0.0f);
                if (index2 != Main.maxProjectiles)
                  Main.projectile[index2].timeLeft = Main.rand.Next(60, 75) * 3;
              }
            }
          }
        }
      }
      base.AI(npc);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(69, 180, true, false);
    }
  }
}
