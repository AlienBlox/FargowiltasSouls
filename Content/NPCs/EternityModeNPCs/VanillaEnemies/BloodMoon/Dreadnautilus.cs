// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.BloodMoon.Dreadnautilus
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.BloodMoon
{
  public class Dreadnautilus : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(618);

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.boss = true;
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[ModContent.BuffType<ClippedWingsBuff>()] = true;
    }

    public virtual bool CanHitPlayer(NPC npc, Player target, ref int CooldownSlot)
    {
      return (double) npc.ai[0] != 1.0 && base.CanHitPlayer(npc, target, ref CooldownSlot);
    }

    public override bool SafePreAI(NPC npc)
    {
      if (!npc.HasValidTarget)
        --((Entity) npc).velocity.Y;
      switch ((int) npc.ai[0])
      {
        case 0:
          if (npc.FargoSouls().BloodDrinker)
          {
            if (npc.HasValidTarget)
            {
              float num = (double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) > 900.0 ? 0.3f : 0.1f;
              NPC npc1 = npc;
              ((Entity) npc1).velocity = Vector2.op_Addition(((Entity) npc1).velocity, Vector2.op_Multiply(num, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center)));
            }
            NPC npc2 = npc;
            ((Entity) npc2).position = Vector2.op_Addition(((Entity) npc2).position, ((Entity) npc).velocity);
            break;
          }
          break;
        case 1:
          if ((double) npc.ai[1] <= 90.0)
          {
            if (npc.FargoSouls().BloodDrinker)
            {
              ++npc.ai[1];
              break;
            }
            break;
          }
          if (npc.HasValidTarget)
          {
            NPC npc3 = npc;
            ((Entity) npc3).position = Vector2.op_Addition(((Entity) npc3).position, Vector2.op_Subtraction(((Entity) Main.player[npc.target]).position, ((Entity) Main.player[npc.target]).oldPosition));
          }
          if ((double) npc.ai[1] % 2.0 == 0.0 && (double) ((Entity) npc).Distance(FargoSoulsUtil.ClosestPointInHitbox((Entity) Main.player[npc.target], ((Entity) npc).Center)) > 30.0)
          {
            float rotation = Utils.ToRotation(((Entity) npc).velocity);
            float num = MathHelper.WrapAngle(rotation - Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center)));
            Vector2 vector2_1 = Utils.RotatedBy(Vector2.UnitX, (double) (rotation + 1.57079637f * (float) Math.Sign(num)), new Vector2());
            Vector2 vector2_2 = Vector2.op_Multiply(100f, vector2_1);
            if (FargoSoulsUtil.HostCheck)
            {
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) npc).Center, vector2_2), vector2_1, ModContent.ProjectileType<BloodThornMissile>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              break;
            }
            break;
          }
          break;
        case 2:
          if ((double) npc.ai[1] < 90.0 && npc.FargoSouls().BloodDrinker)
            ++npc.ai[1];
          npc.ai[1] += 0.5f;
          break;
        case 3:
          Checks();
          if (npc.FargoSouls().BloodDrinker && (double) npc.ai[1] % 10.0 != 0.0)
          {
            ++npc.ai[1];
            Checks();
            break;
          }
          break;
      }
      foreach (NPC npc4 in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && !n.boss && n.lifeMax <= 1000 && (double) ((Entity) npc).Distance(((Entity) n).Center) < 666.0 && Collision.CanHitLine(((Entity) n).Center, 0, 0, ((Entity) npc).Center, 0, 0))))
      {
        if ((double) ((Entity) npc).Distance(((Entity) npc4).Center) < (double) (((Entity) npc).width / 4))
        {
          npc.AddBuff(ModContent.BuffType<BloodDrinkerBuff>(), 360, false);
          CombatText.NewText(((Entity) npc4).Hitbox, Color.Red, npc4.life, true, false);
          npc4.life = 0;
          npc4.HitEffect(0, 10.0, new bool?());
          npc4.checkDead();
          ((Entity) npc4).active = false;
        }
        else
        {
          NPC npc5 = npc4;
          ((Entity) npc5).position = Vector2.op_Subtraction(((Entity) npc5).position, ((Entity) npc4).velocity);
          NPC npc6 = npc4;
          ((Entity) npc6).position = Vector2.op_Addition(((Entity) npc6).position, Vector2.op_Division(((Entity) npc).velocity, 2f));
          NPC npc7 = npc4;
          ((Entity) npc7).position = Vector2.op_Addition(((Entity) npc7).position, Vector2.op_Multiply(Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc4, ((Entity) npc).Center), ((Vector2) ref ((Entity) npc4).velocity).Length()), 2f));
        }
      }
      return base.SafePreAI(npc);

      void Checks()
      {
        if ((double) npc.ai[1] == 60.0)
          SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
        if ((double) npc.ai[1] < 120.0 || (double) npc.ai[1] % 15.0 != 5.0)
          return;
        for (int index1 = 0; index1 < 10; ++index1)
        {
          Vector2 vector2_1 = Vector2.op_Addition(((Entity) Main.player[npc.target]).Center, Utils.NextVector2Circular(Main.rand, 64f, 16f));
          Vector2 vector2_2 = Vector2.op_Addition(((Entity) Main.player[npc.target]).Bottom, new Vector2(Utils.NextFloat(Main.rand, -256f, 256f), Utils.NextFloat(Main.rand, 64f)));
          vector2_2.Y += 250f;
          for (int index2 = 0; index2 < 40; ++index2)
          {
            Tile tileSafely = Framing.GetTileSafely(vector2_2);
            if (!((Tile) ref tileSafely).HasUnactuatedTile || !Main.tileSolid[(int) ((Tile) ref tileSafely).TileType] && !Main.tileSolidTop[(int) ((Tile) ref tileSafely).TileType])
              vector2_2.Y += 16f;
            else
              break;
          }
          Vector2 vector2_3 = Vector2.Normalize(Vector2.op_Subtraction(vector2_1, vector2_2));
          if (FargoSoulsUtil.HostCheck)
          {
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(vector2_2, Vector2.op_Multiply(vector2_3, 50f)), Vector2.op_Multiply(0.6f, vector2_3), ModContent.ProjectileType<BloodThornMissile>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2_2, Vector2.op_Multiply(16f, Utils.RotatedByRandom(vector2_3, (double) MathHelper.ToRadians(10f))), 756, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, Utils.NextFloat(Main.rand, 0.5f, 1f), 0.0f);
          }
        }
      }
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<AnticoagulationBuff>(), 600, true, false);
    }

    public virtual bool PreKill(NPC npc)
    {
      npc.boss = false;
      return base.PreKill(npc);
    }
  }
}
