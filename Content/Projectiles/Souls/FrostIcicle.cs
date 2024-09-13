// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Souls.FrostIcicle
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Souls
{
  public class FrostIcicle : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.MinionSacrificable[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.netImportant = true;
      ((Entity) this.Projectile).width = 24;
      ((Entity) this.Projectile).height = 24;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = -1;
      this.Projectile.aiStyle = -1;
      this.Projectile.timeLeft = 2;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.FargoSouls().CanSplit = false;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      player.FargoSouls();
      ++this.Projectile.timeLeft;
      this.Projectile.netUpdate = true;
      if (((Entity) player).whoAmI == Main.myPlayer && !player.HasEffect<SnowEffect>())
      {
        this.Projectile.Kill();
      }
      else
      {
        this.Projectile.localAI[2] = (float) (!player.HasEffect<FrostEffect>() ? 1 : 0);
        if (this.Projectile.owner == Main.myPlayer)
        {
          float num = 32f;
          ((Entity) this.Projectile).position = Vector2.op_Addition(((Entity) player).Center, Utils.RotatedBy(new Vector2(num, 0.0f), (double) this.Projectile.ai[1], new Vector2()));
          ((Entity) this.Projectile).position.X -= (float) (((Entity) this.Projectile).width / 2);
          ((Entity) this.Projectile).position.Y -= (float) (((Entity) this.Projectile).height / 2);
          this.Projectile.ai[1] += (float) Math.PI / 60f;
          if ((double) this.Projectile.ai[1] > 3.1415927410125732)
          {
            this.Projectile.ai[1] -= 6.28318548f;
            this.Projectile.netUpdate = true;
          }
          this.Projectile.rotation = Utils.ToRotation(Vector2.op_Subtraction(Main.MouseWorld, ((Entity) this.Projectile).Center)) - 5f;
        }
        if (Main.netMode != 2)
          return;
        this.Projectile.netUpdate = true;
      }
    }

    public virtual void OnKill(int timeLeft)
    {
      --Main.player[this.Projectile.owner].FargoSouls().IcicleCount;
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture = TextureAssets.Projectile[(double) this.Projectile.localAI[2] == 0.0 ? this.Projectile.type : 166].Value;
      FargoSoulsUtil.GenericProjectileDraw(this.Projectile, lightColor, texture);
      return false;
    }
  }
}
