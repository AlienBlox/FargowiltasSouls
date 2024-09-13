// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.PhantasmalSphereTrueEye
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class PhantasmalSphereTrueEye : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_454";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 2;
      ProjectileID.Sets.MinionShot[this.Projectile.type] = true;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 46;
      ((Entity) this.Projectile).height = 46;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.aiStyle = -1;
      this.Projectile.penetrate = -1;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.timeLeft = 300;
      this.Projectile.usesIDStaticNPCImmunity = true;
      this.Projectile.idStaticNPCHitCooldown = 20;
      this.Projectile.FargoSouls().noInteractionWithNPCImmunityFrames = true;
    }

    public virtual void AI()
    {
      Projectile projectile1 = FargoSoulsUtil.ProjectileExists(FargoSoulsUtil.GetProjectileByIdentity(this.Projectile.owner, this.Projectile.ai[0], ModContent.ProjectileType<TrueEyeR>()), Array.Empty<int>());
      if (projectile1 != null)
      {
        if ((double) this.Projectile.localAI[0] == 0.0 && (double) this.Projectile.localAI[1] == 0.0)
        {
          Vector2 vector2 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, ((Entity) projectile1).Center);
          this.Projectile.localAI[0] = vector2.X;
          this.Projectile.localAI[1] = vector2.Y;
        }
        if ((double) projectile1.ai[1] == 0.0)
        {
          this.Projectile.ai[0] = -1f;
          ((Entity) this.Projectile).velocity = Vector2.Zero;
          this.Projectile.netUpdate = true;
        }
        else
          ((Entity) this.Projectile).velocity = ((Entity) projectile1).velocity;
        if ((double) projectile1.ai[1] == 2.0 && (double) projectile1.localAI[0] > 1.0)
        {
          this.Projectile.ai[0] = -1f;
        }
        else
        {
          ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) projectile1).Center, new Vector2(this.Projectile.localAI[0], this.Projectile.localAI[1]));
          Projectile projectile2 = this.Projectile;
          ((Entity) projectile2).position = Vector2.op_Subtraction(((Entity) projectile2).position, ((Entity) this.Projectile).velocity);
        }
      }
      else if (this.Projectile.timeLeft > 60)
        this.Projectile.timeLeft = 60;
      Projectile projectile3 = this.Projectile;
      ((Entity) projectile3).velocity = Vector2.op_Multiply(((Entity) projectile3).velocity, 1.01f);
      if (this.Projectile.alpha > 200)
        this.Projectile.alpha = 200;
      this.Projectile.alpha -= 5;
      if (this.Projectile.alpha < 0)
        this.Projectile.alpha = 0;
      this.Projectile.scale = (float) (0.64999997615814209 - 0.64999997615814209 * (double) this.Projectile.alpha / (double) byte.MaxValue);
      ++this.Projectile.frameCounter;
      if (this.Projectile.frameCounter < 6)
        return;
      this.Projectile.frameCounter = 0;
      ++this.Projectile.frame;
      if (this.Projectile.frame <= 1)
        return;
      this.Projectile.frame = 0;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 360, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), (float) (1.0 - (double) this.Projectile.alpha / (double) byte.MaxValue)), 0.5f));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/MutantBoss/MutantSphereGlow", (AssetRequestMode) 1).Value;
      int height = texture2D.Height;
      int num1 = 0;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num1, texture2D.Width, height);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color color1 = Color.Lerp(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0), Color.Transparent, 0.92f);
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color2 = Color.op_Multiply(color1, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        float num2 = this.Projectile.scale * (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type];
        Vector2 oldPo = this.Projectile.oldPos[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color2, Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f, vector2, num2 * 1.5f, (SpriteEffects) 0, 0.0f);
      }
      Color color3 = Color.Lerp(new Color(196, 247, (int) byte.MaxValue, 0), Color.Transparent, 0.8f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).position, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color3, Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f, vector2, this.Projectile.scale * 1.5f, (SpriteEffects) 0, 0.0f);
      return false;
    }

    public virtual void PostDraw(Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
    }

    public virtual void OnKill(int timeleft)
    {
      SoundStyle npcDeath6 = SoundID.NPCDeath6;
      ((SoundStyle) ref npcDeath6).Volume = 0.5f;
      ((SoundStyle) ref npcDeath6).Pitch = 0.0f;
      SoundEngine.PlaySound(ref npcDeath6, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
      ((Entity) this.Projectile).width = ((Entity) this.Projectile).height = 208;
      ((Entity) this.Projectile).position.X -= (float) (((Entity) this.Projectile).width / 2);
      ((Entity) this.Projectile).position.Y -= (float) (((Entity) this.Projectile).height / 2);
      for (int index1 = 0; index1 < 3; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Main.dust[index2].position = Vector2.op_Addition(Vector2.op_Multiply(Utils.RotatedBy(new Vector2((float) (((Entity) this.Projectile).width / 2), 0.0f), 6.28318548202515 * Main.rand.NextDouble(), new Vector2()), (float) Main.rand.NextDouble()), ((Entity) this.Projectile).Center);
      }
      for (int index3 = 0; index3 < 30; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 0, new Color(), 2.5f);
        Main.dust[index4].position = Vector2.op_Addition(Vector2.op_Multiply(Utils.RotatedBy(new Vector2((float) (((Entity) this.Projectile).width / 2), 0.0f), 6.28318548202515 * Main.rand.NextDouble(), new Vector2()), (float) Main.rand.NextDouble()), ((Entity) this.Projectile).Center);
        Main.dust[index4].noGravity = true;
        Dust dust1 = Main.dust[index4];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 1f);
        int index5 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Main.dust[index5].position = Vector2.op_Addition(Vector2.op_Multiply(Utils.RotatedBy(new Vector2((float) (((Entity) this.Projectile).width / 2), 0.0f), 6.28318548202515 * Main.rand.NextDouble(), new Vector2()), (float) Main.rand.NextDouble()), ((Entity) this.Projectile).Center);
        Dust dust2 = Main.dust[index5];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 1f);
        Main.dust[index5].noGravity = true;
      }
    }
  }
}
