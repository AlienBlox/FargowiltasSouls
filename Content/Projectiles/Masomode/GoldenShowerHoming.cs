// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.GoldenShowerHoming
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class GoldenShowerHoming : ModProjectile
  {
    private bool hitATile;

    public virtual string Texture => "Terraria/Images/Projectile_288";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 10;
      ((Entity) this.Projectile).height = 10;
      this.Projectile.aiStyle = -1;
      this.Projectile.alpha = 0;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 900;
      this.Projectile.hostile = true;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[1] == 0.0)
      {
        this.Projectile.localAI[1] = 1f;
        SoundEngine.PlaySound(ref SoundID.Item17, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      if ((double) this.Projectile.ai[1] == 0.0)
      {
        Player player = FargoSoulsUtil.PlayerExists(this.Projectile.ai[0]);
        if (player == null)
        {
          this.Projectile.ai[1] = 1f;
          this.Projectile.netUpdate = true;
          this.Projectile.timeLeft = 180;
          ((Vector2) ref ((Entity) this.Projectile).velocity).Normalize();
        }
        else
        {
          float rotation1 = Utils.ToRotation(((Entity) this.Projectile).velocity);
          Vector2 vector2 = Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.Projectile).Center);
          float rotation2 = Utils.ToRotation(vector2);
          ((Entity) this.Projectile).velocity = Utils.RotatedBy(new Vector2(((Vector2) ref ((Entity) this.Projectile).velocity).Length(), 0.0f), (double) Utils.AngleLerp(rotation1, rotation2, this.Projectile.localAI[0]), new Vector2());
          if ((double) this.Projectile.localAI[0] < 0.5)
            this.Projectile.localAI[0] += 0.00033333333f;
          if ((double) ((Vector2) ref vector2).Length() < 250.0 || !((Entity) player).active || player.dead || player.ghost)
          {
            this.Projectile.ai[1] = 1f;
            this.Projectile.netUpdate = true;
            this.Projectile.timeLeft = 180;
            ((Vector2) ref ((Entity) this.Projectile).velocity).Normalize();
          }
        }
      }
      else if ((double) this.Projectile.ai[1] > 0.0)
      {
        if ((double) ++this.Projectile.ai[1] < 120.0)
        {
          Projectile projectile = this.Projectile;
          ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.03f);
          float rotation3 = Utils.ToRotation(((Entity) this.Projectile).velocity);
          float rotation4 = Utils.ToRotation(Vector2.op_Subtraction(((Entity) Main.player[(int) this.Projectile.ai[0]]).Center, ((Entity) this.Projectile).Center));
          ((Entity) this.Projectile).velocity = Utils.RotatedBy(new Vector2(((Vector2) ref ((Entity) this.Projectile).velocity).Length(), 0.0f), (double) Utils.AngleLerp(rotation3, rotation4, 0.025f), new Vector2());
        }
        if (WorldSavingSystem.MasochistModeReal && !this.hitATile && Collision.SolidTiles(((Entity) this.Projectile).Center, 0, 0))
        {
          this.hitATile = true;
          if (FargoSoulsUtil.HostCheck)
          {
            for (int index = 0; index < 8; ++index)
              Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitY, Math.PI / 4.0 * (double) index, new Vector2()), 2f), 149, 0, 0.0f, Main.myPlayer, 8f, 0.0f, 0.0f);
          }
        }
      }
      else
        ++this.Projectile.ai[1];
      for (int index1 = 0; index1 < 3; ++index1)
      {
        for (int index2 = 0; index2 < 3; ++index2)
        {
          int index3 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 170, 0.0f, 0.0f, 100, new Color(), 1f);
          Main.dust[index3].noGravity = true;
          Dust dust1 = Main.dust[index3];
          dust1.velocity = Vector2.op_Multiply(dust1.velocity, 0.1f);
          Dust dust2 = Main.dust[index3];
          dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.5f));
          Dust dust3 = Main.dust[index3];
          dust3.position = Vector2.op_Subtraction(dust3.position, Vector2.op_Multiply(Vector2.op_Division(((Entity) this.Projectile).velocity, 3f), (float) index2));
        }
        if (Utils.NextBool(Main.rand, 8))
        {
          int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 170, 0.0f, 0.0f, 100, new Color(), 0.5f);
          Dust dust4 = Main.dust[index4];
          dust4.velocity = Vector2.op_Multiply(dust4.velocity, 0.25f);
          Dust dust5 = Main.dust[index4];
          dust5.velocity = Vector2.op_Addition(dust5.velocity, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.5f));
        }
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(69, 900, true, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }
  }
}
