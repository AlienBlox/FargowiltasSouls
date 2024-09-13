// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.CreeperMinion
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
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class CreeperMinion : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.MinionSacrificable[this.Projectile.type] = true;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      ProjectileID.Sets.MinionTargettingFeature[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 20;
      ((Entity) this.Projectile).height = 20;
      this.Projectile.netImportant = true;
      this.Projectile.friendly = true;
      this.Projectile.minionSlots = 1f;
      this.Projectile.timeLeft = 18000;
      this.Projectile.penetrate = -1;
      this.Projectile.minion = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.tileCollide = false;
    }

    public virtual bool? CanCutTiles() => new bool?(false);

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (player.dead)
        fargoSoulsPlayer.BrainMinion = false;
      if (fargoSoulsPlayer.BrainMinion)
        this.Projectile.timeLeft = 2;
      int index1 = -1;
      for (int index2 = 0; index2 < Main.projectile.Length; ++index2)
      {
        if (Main.projectile[index2].type == ModContent.ProjectileType<BrainMinion>() && ((Entity) Main.projectile[index2]).active && Main.projectile[index2].owner == this.Projectile.owner)
          index1 = index2;
      }
      if (index1 == -1)
      {
        this.Projectile.Kill();
      }
      else
      {
        for (int index3 = 0; index3 < Main.maxProjectiles; ++index3)
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
        NPC npc = FargoSoulsUtil.NPCExists(FargoSoulsUtil.FindClosestHostileNPCPrioritizingMinionFocus(this.Projectile, 1000f, true, new Vector2()), Array.Empty<int>());
        int num1 = npc != null ? 1 : 0;
        if (num1 == 0 || (double) this.Projectile.ai[0] > 0.0)
        {
          float num2 = Math.Max(((Entity) this.Projectile).Distance(((Entity) Main.projectile[index1]).Center) / 40f, 10f);
          ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) Main.projectile[index1]).Center), num2), 0.04f);
          Rectangle hitbox = ((Entity) this.Projectile).Hitbox;
          if (((Rectangle) ref hitbox).Intersects(((Entity) Main.projectile[index1]).Hitbox))
            this.Projectile.ai[0] = 0.0f;
        }
        if (num1 == 0 || (double) this.Projectile.ai[0] != 0.0)
          return;
        float num3 = Math.Max(((Entity) this.Projectile).Distance(((Entity) npc).Center) / 40f, 14f);
        ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) npc).Center), num3), 0.05f);
      }
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      ++this.Projectile.ai[0];
    }
  }
}
