// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.DeerclopsHand
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class DeerclopsHand : ModProjectile
  {
    private bool spawned;
    private int oldMash;

    public virtual string Texture => "Terraria/Images/Projectile_965";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = Main.projFrames[965];
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = ProjectileID.Sets.TrailCacheLength[965];
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = ProjectileID.Sets.TrailingMode[965];
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(965);
      this.Projectile.aiStyle = -1;
      this.Projectile.timeLeft = 900;
    }

    public virtual void AI()
    {
      if (!this.spawned)
      {
        this.spawned = true;
        SoundEngine.PlaySound(ref SoundID.DD2_GhastlyGlaiveImpactGhost, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      if ((double) this.Projectile.ai[1] == 0.0)
      {
        Player player = FargoSoulsUtil.PlayerExists(this.Projectile.ai[0]);
        if (player != null && player.FargoSouls().MashCounter < 20 && this.Projectile.timeLeft > 60)
        {
          if (((Entity) player).active && !player.dead && !player.ghost && this.Projectile.Colliding(((Entity) this.Projectile).Hitbox, ((Entity) player).Hitbox))
          {
            player.frozen = true;
            if (WorldSavingSystem.MasochistModeReal)
              player.AddBuff(ModContent.BuffType<MarkedforDeathBuff>(), 2, true, false);
            player.AddBuff(ModContent.BuffType<GrabbedBuff>(), 2, true, false);
            if (this.oldMash < player.FargoSouls().MashCounter)
            {
              this.oldMash = player.FargoSouls().MashCounter;
              if (Utils.NextBool(Main.rand, Main.player[this.Projectile.owner].ownedProjectileCounts[this.Projectile.type] / 2 + 1))
              {
                SoundEngine.PlaySound(ref SoundID.Item27, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
                for (int index = 0; index < 10; ++index)
                  Dust.NewDust(((Entity) player).position, ((Entity) player).width, ((Entity) player).height, 67, 0.0f, 0.0f, 0, new Color(), 1f);
              }
            }
          }
          ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.Projectile).Center), 10f);
          Projectile projectile1 = this.Projectile;
          ((Entity) projectile1).position = Vector2.op_Addition(((Entity) projectile1).position, Vector2.op_Subtraction(((Entity) player).position, ((Entity) player).oldPosition));
          ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = Math.Sign(((Entity) player).Center.X - ((Entity) this.Projectile).Center.X);
          this.Projectile.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) player).Center));
          foreach (Projectile projectile2 in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && p.type == this.Projectile.type && ((Entity) p).whoAmI != ((Entity) this.Projectile).whoAmI && (double) ((Entity) p).Distance(((Entity) this.Projectile).Center) < (double) ((Entity) this.Projectile).width)))
          {
            ((Entity) projectile2).position.X += (float) (2.0 * ((double) ((Entity) projectile2).position.X < (double) ((Entity) this.Projectile).position.X ? -1.0 : 1.0));
            ((Entity) projectile2).position.Y += (float) (2.0 * ((double) ((Entity) projectile2).position.Y < (double) ((Entity) this.Projectile).position.Y ? -1.0 : 1.0));
          }
        }
        else
        {
          if (Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
            ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Utils.NextVector2CircularEdge(Main.rand, 1f, 1f), 16f);
          Projectile projectile = this.Projectile;
          ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, -1f);
          SoundEngine.PlaySound(ref SoundID.DD2_GhastlyGlaiveImpactGhost, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          this.Projectile.ai[1] = 1f;
          this.Projectile.netUpdate = true;
        }
        this.Projectile.alpha -= 25;
        if (this.Projectile.alpha < 50)
          this.Projectile.alpha = 50;
      }
      else
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 0.98f);
        this.Projectile.alpha += 25;
        if (this.Projectile.alpha > (int) byte.MaxValue)
        {
          this.Projectile.alpha = (int) byte.MaxValue;
          this.Projectile.Kill();
          return;
        }
        ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = Math.Sign(((Entity) this.Projectile).velocity.X);
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
      }
      if (this.Projectile.spriteDirection >= 0)
        return;
      this.Projectile.rotation += 3.14159274f;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(44, 90, true, false);
      if (WorldSavingSystem.MasochistModeReal)
        target.AddBuff(ModContent.BuffType<MarkedforDeathBuff>(), 900, true, false);
      target.AddBuff(ModContent.BuffType<HypothermiaBuff>(), 1200, true, false);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color color1 = lightColor;
      Color color2 = Color.op_Multiply(Color.Black, this.Projectile.Opacity);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color3 = Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.LightBlue, this.Projectile.Opacity), 0.75f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color3, num3, vector2, this.Projectile.scale * 1.1f, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color2, this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
