// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Souls.ForbiddenTornado
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
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
namespace FargowiltasSouls.Content.Projectiles.Souls
{
  public class ForbiddenTornado : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 10;
      ((Entity) this.Projectile).height = 10;
      this.Projectile.friendly = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.alpha = 0;
      this.Projectile.penetrate = -1;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 10;
      this.Projectile.timeLeft = 1200;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
      this.Projectile.DamageType = DamageClass.Magic;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      player.FargoSouls();
      if (player.HasEffect<ForbiddenEffect>())
      {
        foreach (Projectile projectile in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && p.friendly && !p.hostile && p.owner == this.Projectile.owner && p.type != this.Projectile.type && p.Colliding(((Entity) p).Hitbox, ((Entity) this.Projectile).Hitbox))))
          projectile.FargoSouls().stormTimer = 240;
      }
      if (((Entity) Main.LocalPlayer).active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost && this.Projectile.Colliding(((Entity) this.Projectile).Hitbox, ((Entity) Main.LocalPlayer).Hitbox))
        Main.LocalPlayer.wingTime = (float) Main.LocalPlayer.wingTimeMax;
      ((Entity) this.Projectile).velocity = Vector2.UnitY;
      Projectile projectile1 = this.Projectile;
      ((Entity) projectile1).position = Vector2.op_Subtraction(((Entity) projectile1).position, ((Entity) this.Projectile).velocity);
      float num1 = 900f;
      if (this.Projectile.soundDelay == 0)
      {
        this.Projectile.soundDelay = -1;
        SoundEngine.PlaySound(ref SoundID.Item82, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      ++this.Projectile.ai[0];
      if ((double) this.Projectile.ai[0] >= (double) num1)
        this.Projectile.Kill();
      if ((double) this.Projectile.localAI[0] >= 30.0)
      {
        this.Projectile.damage = 0;
        if ((double) this.Projectile.ai[0] < (double) num1 - 120.0)
        {
          float num2 = this.Projectile.ai[0] % 60f;
          this.Projectile.ai[0] = num1 - 120f + num2;
          this.Projectile.netUpdate = true;
        }
      }
      float num3 = 15f;
      float num4 = 15f;
      Point tileCoordinates = Utils.ToTileCoordinates(((Entity) this.Projectile).Center);
      int num5;
      int num6;
      Collision.ExpandVertically(tileCoordinates.X, tileCoordinates.Y, ref num5, ref num6, (int) num3, (int) num4);
      int num7 = num5 + 1;
      int num8 = num6 - 1;
      Vector2 vector2_1 = Vector2.op_Addition(Vector2.op_Multiply(new Vector2((float) tileCoordinates.X, (float) num7), 16f), new Vector2(8f));
      Vector2 vector2_2 = Vector2.op_Addition(Vector2.op_Multiply(new Vector2((float) tileCoordinates.X, (float) num8), 16f), new Vector2(8f));
      Vector2 vector2_3 = Vector2.Lerp(vector2_1, vector2_2, 0.5f);
      Vector2 vector2_4;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_4).\u002Ector(0.0f, vector2_2.Y - vector2_1.Y);
      vector2_4.X = vector2_4.Y * 0.2f;
      ((Entity) this.Projectile).width = (int) ((double) vector2_4.X * 0.64999997615814209);
      ((Entity) this.Projectile).height = (int) vector2_4.Y;
      ((Entity) this.Projectile).Center = vector2_3;
      if (this.Projectile.owner == Main.myPlayer)
      {
        bool flag = false;
        Vector2 center = ((Entity) Main.player[this.Projectile.owner]).Center;
        Vector2 top = ((Entity) Main.player[this.Projectile.owner]).Top;
        for (float num9 = 0.0f; (double) num9 < 1.0; num9 += 0.05f)
        {
          Vector2 vector2_5 = Vector2.Lerp(vector2_1, vector2_2, num9);
          if (Collision.CanHitLine(vector2_5, 0, 0, center, 0, 0) || Collision.CanHitLine(vector2_5, 0, 0, top, 0, 0))
          {
            flag = true;
            break;
          }
        }
        if (!flag && (double) this.Projectile.ai[0] < (double) num1 - 120.0)
        {
          float num10 = this.Projectile.ai[0] % 60f;
          this.Projectile.ai[0] = num1 - 120f + num10;
          this.Projectile.netUpdate = true;
        }
      }
      if ((double) this.Projectile.ai[0] >= (double) num1 - 120.0)
        return;
      for (int index = 0; index < 1; ++index)
      {
        float num11 = -0.5f;
        float num12 = 0.9f;
        float num13 = Utils.NextFloat(Main.rand);
        Vector2 vector2_6;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_6).\u002Ector(MathHelper.Lerp(0.1f, 1f, Utils.NextFloat(Main.rand)), MathHelper.Lerp(num11, num12, num13));
        vector2_6.X *= MathHelper.Lerp(2.2f, 0.6f, num13);
        vector2_6.X *= -1f;
        Vector2 vector2_7;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_7).\u002Ector(6f, 10f);
        Vector2 vector2_8 = Vector2.op_Addition(Vector2.op_Addition(vector2_3, Vector2.op_Multiply(Vector2.op_Multiply(vector2_4, vector2_6), 0.5f)), vector2_7);
        Dust dust = Main.dust[Dust.NewDust(vector2_8, 0, 0, 269, 0.0f, 0.0f, 0, new Color(), 1f)];
        dust.position = vector2_8;
        dust.customData = (object) Vector2.op_Addition(vector2_3, vector2_7);
        dust.fadeIn = 1f;
        dust.scale = 0.3f;
        if ((double) vector2_6.X > -1.2000000476837158)
          dust.velocity.X = 1f + Utils.NextFloat(Main.rand);
        dust.velocity.Y = (float) ((double) Utils.NextFloat(Main.rand) * -0.5 - 1.0);
      }
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      float num1 = 220f;
      float num2 = 50f;
      for (float num3 = 0.0f; (double) num3 < (double) (int) num2; ++num3)
      {
        Color color;
        // ISSUE: explicit constructor call
        ((Color) ref color).\u002Ector(212, 192, 100);
        ref Color local = ref color;
        ((Color) ref local).A = (byte) ((uint) ((Color) ref local).A / 2U);
        float num4 = (double) Math.Abs(num2 / 2f - num3) > (double) num2 / 2.0 * 0.60000002384185791 ? Math.Abs(num2 / 2f - num3) / (num2 / 2f) : 0.0f;
        color = Color.Lerp(color, Color.Transparent, num4);
        Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
        Vector2 vector2 = Vector2.SmoothStep(Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), num1)), Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), num1)), num3 / num2);
        float num5 = MathHelper.Lerp(this.Projectile.scale * 0.8f, this.Projectile.scale * 2.5f, num3 / num2);
        Main.EntitySpriteDraw(texture2D, Vector2.op_Subtraction(vector2, Main.screenPosition), new Rectangle?(new Rectangle(0, 0, texture2D.Width, texture2D.Height)), this.Projectile.GetAlpha(color), (float) ((double) num3 / 6.0 - (double) Main.GlobalTimeWrappedHourly * 5.0) + this.Projectile.rotation, Vector2.op_Division(Utils.Size(texture2D), 2f), num5, (SpriteEffects) 0, 0.0f);
      }
      return false;
    }
  }
}
