// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Purified.PrimeMinionProj
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Purified
{
  public class PrimeMinionProj : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 7;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      ProjectileID.Sets.MinionTargettingFeature[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 34;
      ((Entity) this.Projectile).height = 38;
      this.Projectile.aiStyle = -1;
      this.Projectile.penetrate = -1;
      this.Projectile.friendly = true;
      this.Projectile.minion = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.netImportant = true;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 18000;
      this.Projectile.FargoSouls().CanSplit = false;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      PatreonPlayer modPlayer = player.GetModPlayer<PatreonPlayer>();
      if (player.dead)
        modPlayer.PrimeMinion = false;
      if (modPlayer.PrimeMinion)
        this.Projectile.timeLeft = 2;
      bool flag1 = false;
      int[] source = new int[4]
      {
        ModContent.ProjectileType<PrimeMinionCannon>(),
        ModContent.ProjectileType<PrimeMinionLaserGun>(),
        ModContent.ProjectileType<PrimeMinionSaw>(),
        ModContent.ProjectileType<PrimeMinionVice>()
      };
      for (int index = 0; index < Main.projectile.Length; ++index)
      {
        if (((Entity) Main.projectile[index]).active && Main.projectile[index].owner == this.Projectile.owner && ((IEnumerable<int>) source).Contains<int>(Main.projectile[index].type))
        {
          flag1 = true;
          break;
        }
      }
      if (!flag1 && this.Projectile.owner == Main.myPlayer)
        this.Projectile.Kill();
      ++this.Projectile.frameCounter;
      if (this.Projectile.frameCounter >= 6)
      {
        this.Projectile.frameCounter = 0;
        this.Projectile.frame = (this.Projectile.frame + 1) % 6;
      }
      bool flag2 = false;
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[1]);
      Vector2 vector2;
      if (npc == null)
      {
        vector2 = Vector2.op_Subtraction(((Entity) player).Top, Vector2.op_Multiply(32f, Vector2.UnitY));
        ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = ((Entity) player).direction;
        if ((double) ((Entity) this.Projectile).Distance(vector2) > 1200.0)
          ((Entity) this.Projectile).Center = ((Entity) player).Center;
        if ((double) ++this.Projectile.localAI[0] > 10.0)
        {
          this.Projectile.localAI[0] = 0.0f;
          this.Projectile.ai[1] = (float) FargoSoulsUtil.FindClosestHostileNPCPrioritizingMinionFocus(this.Projectile, 800f, true, ((Entity) player).Center);
          this.Projectile.netUpdate = true;
        }
      }
      else
      {
        if ((double) ++this.Projectile.ai[0] > 360.0)
          flag2 = true;
        if ((double) this.Projectile.ai[0] > 540.0)
        {
          this.Projectile.ai[0] = 0.0f;
          this.Projectile.netUpdate = true;
        }
        vector2 = flag2 ? ((Entity) npc).Center : Vector2.op_Subtraction(((Entity) npc).Top, Vector2.op_Multiply(32f, Vector2.UnitY));
        if (!flag2)
          ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = Math.Sign(((Entity) npc).Center.X - ((Entity) this.Projectile).Center.X);
        NPC minionAttackTargetNpc = this.Projectile.OwnerMinionAttackTargetNPC;
        if (minionAttackTargetNpc != null && (double) this.Projectile.ai[1] != (double) ((Entity) minionAttackTargetNpc).whoAmI && minionAttackTargetNpc.CanBeChasedBy((object) null, false))
        {
          this.Projectile.ai[1] = (float) ((Entity) minionAttackTargetNpc).whoAmI;
          this.Projectile.netUpdate = true;
        }
        if (!npc.CanBeChasedBy((object) null, false) || (double) ((Entity) player).Distance(((Entity) npc).Center) > 1200.0)
        {
          this.Projectile.ai[1] = -1f;
          this.Projectile.netUpdate = true;
        }
      }
      if ((double) ((Entity) this.Projectile).Distance(vector2) > 16.0 | flag2)
      {
        float num1 = npc == null ? 12f : 16f;
        float num2 = 0.03f + 0.03f * Math.Min(1f, this.Projectile.localAI[1] / 300f);
        if (flag2)
        {
          num1 *= 1.5f;
          num2 *= 2f;
        }
        ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, vector2), num1), num2);
      }
      else
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 0.99f);
        this.Projectile.localAI[1] = 0.0f;
      }
      if (flag2)
      {
        this.Projectile.rotation += 0.314159274f;
        ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = 1;
      }
      else
        this.Projectile.rotation = 0.0f;
    }
  }
}
