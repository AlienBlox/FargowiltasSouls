// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.SkeletronGuardian
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class SkeletronGuardian : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_197";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 8;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 42;
      ((Entity) this.Projectile).height = 42;
      this.Projectile.penetrate = -1;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.aiStyle = -1;
      this.Projectile.timeLeft = 360;
      this.Projectile.hide = true;
      this.Projectile.light = 0.5f;
    }

    public virtual void OnSpawn(IEntitySource source)
    {
      if (!SkeletronBone.SourceIsSkeletron(source))
        return;
      this.Projectile.ai[2] = 1f;
      this.Projectile.netUpdate = true;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        this.Projectile.rotation = Utils.NextFloat(Main.rand, 0.0f, 6.28318548f);
        this.Projectile.hide = false;
        SoundEngine.PlaySound(ref SoundID.Item21, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        for (int index1 = 0; index1 < 50; ++index1)
        {
          int index2 = Dust.NewDust(new Vector2(((Entity) this.Projectile).Center.X + (float) Main.rand.Next(-20, 20), ((Entity) this.Projectile).Center.Y + (float) Main.rand.Next(-20, 20)), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 5, 0.0f, 0.0f, 100, new Color(), 2f);
          Main.dust[index2].noGravity = true;
        }
      }
      if ((double) this.Projectile.ai[0] == 0.0)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Subtraction(((Entity) projectile).velocity, Utils.RotatedBy(new Vector2(this.Projectile.ai[1], 0.0f), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2()));
        if ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() < 1.0)
        {
          int closest = (int) Player.FindClosest(((Entity) this.Projectile).Center, 0, 0);
          if (closest != -1)
          {
            ((Entity) this.Projectile).velocity = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) Main.player[closest]).Center);
            this.Projectile.ai[0] = 1f;
            this.Projectile.ai[1] = (float) closest;
            this.Projectile.netUpdate = true;
            SoundEngine.PlaySound(ref SoundID.Item1, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          }
          else
            this.Projectile.Kill();
        }
      }
      else
      {
        if ((double) ++this.Projectile.localAI[0] < 45.0)
        {
          Projectile projectile = this.Projectile;
          ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.08f);
        }
        if ((double) this.Projectile.localAI[0] < 65.0)
        {
          float rotation1 = Utils.ToRotation(((Entity) this.Projectile).velocity);
          float rotation2 = Utils.ToRotation(Vector2.op_Subtraction(((Entity) Main.player[(int) this.Projectile.ai[1]]).Center, ((Entity) this.Projectile).Center));
          ((Entity) this.Projectile).velocity = Utils.RotatedBy(new Vector2(((Vector2) ref ((Entity) this.Projectile).velocity).Length(), 0.0f), (double) Utils.AngleLerp(rotation1, rotation2, 0.065f), new Vector2());
        }
      }
      ((Entity) this.Projectile).direction = (double) ((Entity) this.Projectile).velocity.X < 0.0 ? -1 : 1;
      this.Projectile.rotation += (float) ((Entity) this.Projectile).direction * 0.3f;
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 50; ++index1)
      {
        int index2 = Dust.NewDust(new Vector2(((Entity) this.Projectile).Center.X + (float) Main.rand.Next(-20, 20), ((Entity) this.Projectile).Center.Y + (float) Main.rand.Next(-20, 20)), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 5, 0.0f, 0.0f, 100, new Color(), 2f);
        Main.dust[index2].noGravity = true;
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 300, true, false);
      target.AddBuff(ModContent.BuffType<LethargicBuff>(), 300, true, false);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture = ((double) this.Projectile.ai[2] != 1.0 || !SoulConfig.Instance.BossRecolors ? 0 : (WorldSavingSystem.EternityMode ? 1 : 0)) != 0 ? ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/DeviBoss/DeviGuardian_Recolor", (AssetRequestMode) 2).Value : TextureAssets.Projectile[this.Type].Value;
      FargoSoulsUtil.ProjectileWithTrailDraw(this.Projectile, Color.op_Multiply(new Color((int) byte.MaxValue, 200, (int) byte.MaxValue, 0), this.Projectile.Opacity), texture);
      FargoSoulsUtil.GenericProjectileDraw(this.Projectile, lightColor, texture);
      return false;
    }
  }
}
