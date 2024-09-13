// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BeeDash
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Souls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles
{
  public class BeeDash : ModProjectile
  {
    public virtual string Texture => "FargowiltasSouls/Assets/ExtraTextures/Resprites/NPC_222";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = Main.npcFrameCount[222];
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 10;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 42;
      ((Entity) this.Projectile).height = 42;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 15;
      this.Projectile.penetrate = -1;
      this.Projectile.FargoSouls().CanSplit = false;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public virtual void AI()
    {
      Player player1 = Main.player[this.Projectile.owner];
      if (!((Entity) player1).active || player1.dead)
        this.Projectile.Kill();
      else if (player1.HasBuff(ModContent.BuffType<TimeFrozenBuff>()))
      {
        this.Projectile.Kill();
      }
      else
      {
        player1.dashDelay = 5;
        player1.FargoSouls().IsDashingTimer = 0;
        ((Entity) player1).Center = ((Entity) this.Projectile).Center;
        if (this.Projectile.timeLeft > 1)
        {
          Player player2 = player1;
          ((Entity) player2).position = Vector2.op_Addition(((Entity) player2).position, ((Entity) this.Projectile).velocity);
        }
        ((Entity) player1).velocity = Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.5f);
        if ((double) ((Entity) this.Projectile).velocity.X != 0.0)
          ((Entity) player1).direction = (double) ((Entity) this.Projectile).velocity.X > 0.0 ? 1 : -1;
        player1.controlUseItem = false;
        player1.controlUseTile = false;
        player1.controlHook = false;
        player1.controlMount = false;
        if (player1.mount.Active)
          player1.mount.Dismount(player1);
        if (Vector2.op_Inequality(((Entity) this.Projectile).velocity, Vector2.Zero))
          this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
        if ((double) this.Projectile.localAI[0] != 0.0)
          return;
        this.Projectile.localAI[0] = 1f;
        SoundEngine.PlaySound(ref SoundID.Item97, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        for (int index1 = 0; index1 < 30; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 87, 0.0f, 0.0f, 0, new Color(), 2.5f);
          Main.dust[index2].noGravity = true;
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
        }
      }
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(70, 600, false);
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item97, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 30; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 87, 0.0f, 0.0f, 0, new Color(), 2.5f);
        Main.dust[index2].noGravity = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
      }
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity) => false;

    public virtual bool PreDraw(ref Color lightColor)
    {
      if ((double) this.Projectile.localAI[0] != 0.0)
      {
        Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
        int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
        int num2 = num1 * this.Projectile.frame;
        Rectangle rectangle;
        // ISSUE: explicit constructor call
        ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
        Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
        Color alpha = this.Projectile.GetAlpha(lightColor);
        SpriteEffects spriteEffects = ((Entity) this.Projectile).direction < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
        float num3 = ((Entity) this.Projectile).direction < 0 ? 3.14159274f : 0.0f;
        float num4 = this.Projectile.scale * 0.5f;
        Vector2 vector2_2 = Vector2.Zero;
        if (Vector2.op_Inequality(((Entity) this.Projectile).velocity, Vector2.Zero))
          vector2_2 = Vector2.op_Multiply(16f, Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.Zero));
        for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
        {
          Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
          Vector2 oldPo = this.Projectile.oldPos[index];
          float num5 = this.Projectile.oldRot[index] + num3;
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(Vector2.op_Addition(vector2_2, oldPo), Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num5, vector2_1, num4, spriteEffects, 0.0f);
        }
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(vector2_2, ((Entity) this.Projectile).Center), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation + num3, vector2_1, num4, spriteEffects, 0.0f);
      }
      return false;
    }
  }
}
