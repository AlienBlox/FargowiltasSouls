// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Purified.PrimeMinionCannon
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Purified
{
  public class PrimeMinionCannon : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.MinionSacrificable[this.Projectile.type] = true;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      ProjectileID.Sets.MinionTargettingFeature[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 34;
      ((Entity) this.Projectile).height = 38;
      this.Projectile.netImportant = true;
      this.Projectile.friendly = true;
      this.Projectile.minionSlots = 1f;
      this.Projectile.timeLeft = 18000;
      this.Projectile.penetrate = -1;
      this.Projectile.minion = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.tileCollide = false;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      PatreonPlayer modPlayer = player.GetModPlayer<PatreonPlayer>();
      if (player.dead)
        modPlayer.PrimeMinion = false;
      if (modPlayer.PrimeMinion)
        this.Projectile.timeLeft = 2;
      int index1 = -1;
      for (int index2 = 0; index2 < Main.projectile.Length; ++index2)
      {
        if (Main.projectile[index2].type == ModContent.ProjectileType<PrimeMinionProj>() && ((Entity) Main.projectile[index2]).active && Main.projectile[index2].owner == this.Projectile.owner)
          index1 = index2;
      }
      if (index1 == -1)
      {
        if (this.Projectile.owner != Main.myPlayer)
          return;
        this.Projectile.Kill();
      }
      else
      {
        for (int index3 = 0; index3 < 1000; ++index3)
        {
          if (index3 != ((Entity) this.Projectile).whoAmI && ((Entity) Main.projectile[index3]).active && Main.projectile[index3].owner == this.Projectile.owner && Main.projectile[index3].type == this.Projectile.type && (double) Math.Abs(((Entity) this.Projectile).position.X - ((Entity) Main.projectile[index3]).position.X) + (double) Math.Abs(((Entity) this.Projectile).position.Y - ((Entity) Main.projectile[index3]).position.Y) < (double) ((Entity) this.Projectile).width)
          {
            if ((double) ((Entity) this.Projectile).position.X < (double) ((Entity) Main.projectile[index3]).position.X)
              ((Entity) this.Projectile).velocity.X -= 0.2f;
            else
              ((Entity) this.Projectile).velocity.X += 0.2f;
            if ((double) ((Entity) this.Projectile).position.Y < (double) ((Entity) Main.projectile[index3]).position.Y)
              ((Entity) this.Projectile).velocity.Y -= 0.2f;
            else
              ((Entity) this.Projectile).velocity.Y += 0.2f;
          }
        }
        bool flag = false;
        NPC npc = (NPC) null;
        NPC minionAttackTargetNpc = this.Projectile.OwnerMinionAttackTargetNPC;
        if (minionAttackTargetNpc != null && minionAttackTargetNpc.CanBeChasedBy((object) this, false))
        {
          Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) minionAttackTargetNpc).Center, ((Entity) this.Projectile).Center);
          Vector2 vector2_2 = Vector2.op_Subtraction(((Entity) minionAttackTargetNpc).Center, ((Entity) Main.projectile[index1]).Center);
          if ((double) ((Vector2) ref vector2_1).Length() < 1000.0 && (double) ((Vector2) ref vector2_2).Length() < 400.0)
          {
            npc = minionAttackTargetNpc;
            flag = true;
          }
        }
        else if (!flag)
        {
          float num = 1000f;
          for (int index4 = 0; index4 < 200; ++index4)
          {
            if (Main.npc[index4].CanBeChasedBy((object) this, false))
            {
              Vector2 vector2_3 = Vector2.op_Subtraction(((Entity) Main.npc[index4]).Center, ((Entity) this.Projectile).Center);
              Vector2 vector2_4 = Vector2.op_Subtraction(((Entity) Main.npc[index4]).Center, ((Entity) Main.projectile[index1]).Center);
              if ((double) ((Vector2) ref vector2_3).Length() < (double) num && (double) ((Vector2) ref vector2_4).Length() < 400.0)
              {
                num = ((Vector2) ref vector2_3).Length();
                npc = Main.npc[index4];
                flag = true;
              }
            }
          }
        }
        float num1 = Math.Max(((Entity) this.Projectile).Distance(((Entity) Main.projectile[index1]).Center) / 40f, 14f);
        if ((double) ((Entity) this.Projectile).Distance(((Entity) Main.projectile[index1]).Center) > 64.0)
          ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) Main.projectile[index1]).Center), num1), 0.04f);
        this.Projectile.rotation = 0.0f;
        ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = Main.projectile[index1].spriteDirection;
        if (flag)
        {
          this.Projectile.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) npc).Center));
          ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = 1;
          if ((double) ++this.Projectile.localAI[0] > 60.0)
          {
            this.Projectile.localAI[0] = (float) -Main.rand.Next(20);
            if (this.Projectile.owner == Main.myPlayer)
            {
              int index5 = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.op_Multiply(16f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) npc).Center)), 162, this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
              if (index5 != Main.maxProjectiles)
              {
                Main.projectile[index5].DamageType = DamageClass.Summon;
                Main.projectile[index5].usesIDStaticNPCImmunity = false;
                Main.projectile[index5].usesLocalNPCImmunity = false;
              }
            }
          }
        }
        Projectile projectile = this.Projectile;
        ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, Vector2.op_Multiply(((Entity) Main.projectile[index1]).velocity, 0.8f));
      }
    }
  }
}
