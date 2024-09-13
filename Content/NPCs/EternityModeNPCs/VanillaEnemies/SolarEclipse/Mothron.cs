// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.SolarEclipse.Mothron
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.SolarEclipse
{
  public class Mothron : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(477);

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.knockBackResist *= 0.1f;
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(100f, Vector2.UnitX)), 479, velocity: new Vector2());
      FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), Vector2.op_Subtraction(((Entity) npc).Center, Vector2.op_Multiply(100f, Vector2.UnitX)), 479, velocity: new Vector2());
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      npc.defense = npc.defDefense;
      if ((double) npc.ai[0] == 0.0)
        npc.ai[1] += 10f;
      else if ((double) npc.ai[0] == 1.0)
      {
        NPC npc1 = npc;
        ((Entity) npc1).position = Vector2.op_Addition(((Entity) npc1).position, Vector2.op_Division(((Entity) npc).velocity, 2f));
      }
      else if ((double) npc.ai[0] == 2.0)
        ++npc.ai[1];
      else if ((double) npc.ai[0] == 3.0999999046325684 && (double) npc.ai[1] < 1.0)
      {
        for (int index = -5; index <= 5; ++index)
        {
          if (index != 0)
          {
            int num = Math.Sign(((Entity) Main.player[npc.target]).Center.X - ((Entity) npc).Center.X);
            Vector2 center = ((Entity) npc).Center;
            center.X += (float) (100.0 * (double) Math.Abs(index) - 250.0) * (float) -num;
            center.Y += 160f * (float) index;
            Vector2 vector2 = Vector2.op_Division(Vector2.op_Multiply(2f, Vector2.op_Subtraction(center, ((Entity) npc).Center)), 100f);
            if (FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, ModContent.ProjectileType<MothronZenith>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, -1f, (float) num, 0.0f);
          }
        }
      }
      else if (((double) npc.ai[0] == 3.0 || (double) npc.ai[0] == 4.0) && (double) npc.ai[1] == 0.0)
      {
        if (FargoSoulsUtil.HostCheck)
        {
          int num = ModContent.ProjectileType<MothronZenith>();
          for (int index = 0; index < Main.maxProjectiles; ++index)
          {
            if (((Entity) Main.projectile[index]).active && Main.projectile[index].type == num && (double) Main.projectile[index].ai[0] == (double) ((Entity) npc).whoAmI)
              Main.projectile[index].Kill();
          }
        }
        for (int index = -6; index <= 6; ++index)
        {
          if ((index >= 0 || (double) npc.ai[0] != 3.0) && index != 0)
          {
            float num = 1.04719758f * (float) index;
            if (FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<MothronZenith>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, num, 0.0f);
          }
        }
      }
      else
      {
        if ((double) npc.ai[0] != 4.0999999046325684 && (double) npc.ai[0] != 4.1999998092651367)
          return;
        NPC npc2 = npc;
        ((Entity) npc2).position = Vector2.op_Addition(((Entity) npc2).position, Vector2.op_Division(((Entity) npc).velocity, 2f));
        npc.defDefense *= 2;
      }
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(148, 3600, true, false);
      target.FargoSouls().AddBuffNoStack(ModContent.BuffType<StunnedBuff>(), 60);
    }
  }
}
