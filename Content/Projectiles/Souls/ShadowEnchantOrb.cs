// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Souls.ShadowEnchantOrb
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Souls
{
  public class ShadowEnchantOrb : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_18";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.MinionSacrificable[this.Projectile.type] = true;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 8;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 0;
      Main.projPet[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.netImportant = true;
      ((Entity) this.Projectile).width = 32;
      ((Entity) this.Projectile).height = 32;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 18000;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      this.Projectile.netUpdate = true;
      if (((Entity) player).whoAmI == Main.myPlayer && (player.dead || !player.HasEffect<ShadowBalls>()))
      {
        this.Projectile.Kill();
      }
      else
      {
        if ((double) this.Projectile.ai[0] > 0.0)
        {
          --this.Projectile.ai[0];
          if ((double) this.Projectile.ai[0] == 0.0)
          {
            for (int index1 = 0; index1 < 18; ++index1)
            {
              Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitX, (double) this.Projectile.rotation, new Vector2()), 6f), (double) (index1 - 8) * 6.2831854820251465 / 18.0, new Vector2()), ((Entity) this.Projectile).Center);
              Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) this.Projectile).Center);
              int index2 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_2), 0, 0, 27, 0.0f, 0.0f, 0, new Color(), 2f);
              Main.dust[index2].noGravity = true;
              Main.dust[index2].velocity = vector2_2;
            }
          }
        }
        this.Projectile.scale = (float) ((double) Main.mouseTextColor / 200.0 - 0.34999999403953552) * 0.2f + 0.95f;
        if (this.Projectile.owner != Main.myPlayer)
          return;
        float num1 = 250f;
        Lighting.AddLight(((Entity) this.Projectile).Center, 0.1f, 0.4f, 0.2f);
        ((Entity) this.Projectile).position = Vector2.op_Addition(((Entity) player).Center, Utils.RotatedBy(new Vector2(num1, 0.0f), (double) this.Projectile.ai[1], new Vector2()));
        ((Entity) this.Projectile).position.X -= (float) (((Entity) this.Projectile).width / 2);
        ((Entity) this.Projectile).position.Y -= (float) (((Entity) this.Projectile).height / 2);
        this.Projectile.ai[1] -= (float) Math.PI / 120f;
        if ((double) this.Projectile.ai[1] > 3.1415927410125732)
        {
          this.Projectile.ai[1] -= 6.28318548f;
          this.Projectile.netUpdate = true;
        }
        this.Projectile.rotation = this.Projectile.ai[1] + 1.57079637f;
        if ((double) this.Projectile.ai[0] != 0.0)
          return;
        using (IEnumerator<Projectile> enumerator = ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (proj => ((Entity) proj).active && proj.friendly && !proj.hostile && proj.owner == this.Projectile.owner && proj.damage > 0 && !FargoSoulsUtil.IsSummonDamage(proj, false) && proj.type != ModContent.ProjectileType<ShadowBall>() && proj.Colliding(((Entity) proj).Hitbox, ((Entity) this.Projectile).Hitbox))).GetEnumerator())
        {
          if (!enumerator.MoveNext())
            return;
          Projectile current = enumerator.Current;
          int num2 = 5;
          int dmg = 25;
          if (fargoSoulsPlayer.AncientShadowEnchantActive)
          {
            num2 = 10;
            dmg = 50;
          }
          int damage = FargoSoulsUtil.HighestDamageTypeScaling(player, dmg);
          foreach (Projectile projectile in FargoSoulsUtil.XWay(num2, ((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, ModContent.ProjectileType<ShadowBall>(), 6f, damage, 0.0f))
            projectile.originalDamage = damage;
          if (FargoSoulsUtil.CanDeleteProjectile(current))
            current.Kill();
          this.Projectile.ai[0] = 300f;
        }
      }
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      if ((double) this.Projectile.ai[0] > 0.0)
        return false;
      Vector2 vector2_1;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_1).\u002Ector((float) TextureAssets.Projectile[this.Projectile.type].Value.Width * 0.5f, (float) ((Entity) this.Projectile).height * 0.5f);
      for (int index = 0; index < this.Projectile.oldPos.Length; ++index)
      {
        Vector2 vector2_2 = Vector2.op_Addition(Vector2.op_Addition(Vector2.op_Subtraction(this.Projectile.oldPos[index], Main.screenPosition), vector2_1), new Vector2(0.0f, this.Projectile.gfxOffY));
        Color color = Color.op_Multiply(this.Projectile.GetAlpha(lightColor), (float) (this.Projectile.oldPos.Length - index) / (float) this.Projectile.oldPos.Length);
        Main.spriteBatch.Draw(TextureAssets.Projectile[this.Projectile.type].Value, vector2_2, new Rectangle?(), color, this.Projectile.rotation, vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      return true;
    }
  }
}
