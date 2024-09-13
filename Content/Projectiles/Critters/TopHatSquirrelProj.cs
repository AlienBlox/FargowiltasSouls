// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Critters.TopHatSquirrelProj
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Critters
{
  internal class TopHatSquirrelProj : ModProjectile
  {
    public int Counter = 1;

    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Items/Weapons/Misc/TophatSquirrelWeapon";
    }

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 15;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 20;
      ((Entity) this.Projectile).height = 20;
      this.Projectile.friendly = true;
      this.Projectile.aiStyle = -1;
      this.Projectile.DamageType = DamageClass.Magic;
      this.Projectile.scale = 0.5f;
      this.Projectile.timeLeft = 100;
    }

    public virtual void AI()
    {
      this.Projectile.spriteDirection = Math.Sign(((Entity) this.Projectile).velocity.X);
      this.Projectile.rotation += 0.2f * (float) this.Projectile.spriteDirection;
      this.Projectile.scale += 0.02f;
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      ((Entity) this.Projectile).velocity = oldVelocity;
      return true;
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.NPCDeath52, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      if (this.Projectile.owner != Main.myPlayer)
        return;
      int type = ModContent.ProjectileType<TopHatSquirrelLaser>();
      FargoSoulsUtil.XWay(16, ((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, type, ((Vector2) ref ((Entity) this.Projectile).velocity).Length() * 2f, this.Projectile.damage * 4, this.Projectile.knockBack);
      int num = Main.player[this.Projectile.owner].ownedProjectileCounts[type] >= 50 ? 25 : 50;
      for (int index = 0; index < num; ++index)
      {
        Vector2 vector2 = Vector2.op_Addition(Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), Utils.NextFloat(Main.rand, 600f, 1800f))), Vector2.op_Multiply(Vector2.Normalize(Utils.RotatedBy(((Entity) this.Projectile).velocity, 1.5707963705062866, new Vector2())), Utils.NextFloat(Main.rand, -900f, 900f)));
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), vector2, Vector2.op_Multiply(Vector2.op_UnaryNegation(((Entity) this.Projectile).velocity), Utils.NextFloat(Main.rand, 2f, 3f)), type, this.Projectile.damage * 4, this.Projectile.knockBack, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      }
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
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 1 : (SpriteEffects) 0;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index += 3)
      {
        Color color;
        // ISSUE: explicit constructor call
        ((Color) ref color).\u002Ector(93, (int) byte.MaxValue, 241, 0);
        color = Color.op_Multiply(color, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale * 1.1f, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
