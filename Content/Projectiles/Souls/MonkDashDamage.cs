// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Souls.MonkDashDamage
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Souls
{
  public class MonkDashDamage : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      ((ModType) this).SetStaticDefaults();
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 2;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 0;
    }

    public virtual void SetDefaults()
    {
      Player player = Main.player[this.Projectile.owner];
      this.Projectile.DamageType = DamageClass.Generic;
      this.Projectile.friendly = true;
      ((Entity) this.Projectile).width = ((Entity) player).width;
      ((Entity) this.Projectile).height = ((Entity) player).height;
      this.Projectile.usesIDStaticNPCImmunity = true;
      this.Projectile.idStaticNPCHitCooldown = 30;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 30;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      if (player == null || !((Entity) player).active || player.dead)
        this.Projectile.Kill();
      ((Entity) this.Projectile).Center = ((Entity) player).Center;
    }

    public virtual string Texture => FargoSoulsUtil.EmptyTexture;

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      this.Projectile.damage = (int) Math.Round((double) this.Projectile.damage * 0.8);
      SoundEngine.PlaySound(ref SoundID.DD2_MonkStaffSwing, new Vector2?(((Entity) target).Center), (SoundUpdateCallback) null);
      NPC npc = target;
      Rectangle hitbox = ((Entity) npc).Hitbox;
      ((Rectangle) ref hitbox).Inflate(30, 16);
      hitbox.Y -= 8;
      Vector2 vector2_1 = Utils.NextVector2FromRectangle(Main.rand, hitbox);
      Vector2 vector2_2 = Utils.ToVector2(((Rectangle) ref hitbox).Center);
      Vector2 vector2_3 = Vector2.op_Multiply(Utils.SafeNormalize(Vector2.op_Subtraction(vector2_2, vector2_1), new Vector2((float) hit.HitDirection, 0.5f)), 8f);
      double num1 = (double) Utils.NextFloat(Main.rand);
      float num2 = (float) (Main.rand.Next(2) * 2 - 1) * (float) (0.62831854820251465 + 2.5132741928100586 * (double) Utils.NextFloat(Main.rand)) * 0.5f;
      Vector2 vector2_4 = Utils.RotatedBy(vector2_3, 0.78539818525314331, new Vector2());
      int num3 = 3;
      int num4 = 10 * num3;
      int num5 = 5;
      int num6 = num5 * num3;
      Vector2 vector2_5 = vector2_2;
      for (int index = 0; index < num6; ++index)
      {
        vector2_5 = Vector2.op_Subtraction(vector2_5, vector2_4);
        vector2_4 = Utils.RotatedBy(vector2_4, (0.0 - (double) num2) / (double) num4, new Vector2());
      }
      Vector2 vector2_6 = Vector2.op_Addition(vector2_5, Vector2.op_Multiply(((Entity) npc).velocity, (float) num5));
      Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), vector2_6, vector2_4, ModContent.ProjectileType<MonkDashSlash>(), (int) ((double) this.Projectile.damage * 0.5), 0.0f, Main.myPlayer, num2, 0.0f, 0.0f);
    }
  }
}
