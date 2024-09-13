// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.EaterRocket
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class EaterRocket : ModProjectile
  {
    private bool sweetspot;

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 12;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 24;
      ((Entity) this.Projectile).height = 24;
      this.Projectile.aiStyle = 1;
      this.AIType = 14;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.timeLeft = 1200;
      this.Projectile.extraUpdates = 1;
    }

    public virtual void AI()
    {
      int index = Dust.NewDust(new Vector2(((Entity) this.Projectile).position.X, ((Entity) this.Projectile).position.Y + 2f), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height + 5, 70, ((Entity) this.Projectile).velocity.X * 0.2f, ((Entity) this.Projectile).velocity.Y * 0.2f, 100, new Color(), 1f);
      Main.dust[index].noGravity = true;
    }

    public virtual void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
      Player player = Main.player[this.Projectile.owner];
      int num1 = 300;
      int rockeaterDistance = player.FargoSouls().RockeaterDistance;
      int num2 = (num1 + rockeaterDistance) / 2;
      Vector2 desiredLocation = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) player, ((Entity) target).Center), (float) num2));
      float num3 = Vector2.Distance(FargoSoulsUtil.ClosestPointInHitbox(((Entity) target).Hitbox, desiredLocation), ((Entity) player).Center);
      if ((double) num3 <= (double) num1 || (double) num3 >= (double) rockeaterDistance)
        return;
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Multiply(local, 1.5f);
      this.sweetspot = true;
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).Center, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 70, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.20000000298023224), 100, new Color(), 1.5f);
        Main.dust[index2].noGravity = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 2f);
        int index3 = Dust.NewDust(((Entity) this.Projectile).Center, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 70, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.20000000298023224), 100, new Color(), 1f);
        Dust dust2 = Main.dust[index3];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 1.5f);
      }
      if (this.sweetspot)
      {
        for (int index4 = 0; index4 < 40; ++index4)
        {
          int index5 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 295, 0.0f, 0.0f, 100, new Color(), 2f);
          Main.dust[index5].noGravity = true;
          Main.dust[index5].noLight = true;
          Main.dust[index5].velocity = Vector2.op_Addition(Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), 9f), Utils.NextVector2Circular(Main.rand, 12f, 12f));
          Dust dust = Main.dust[index5];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 2.5f);
          Main.dust[index5].scale *= Utils.NextFloat(Main.rand, 1.5f, 3f);
        }
      }
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index6 = 0; index6 < 10; ++index6)
      {
        int index7 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 3f);
        Dust dust = Main.dust[index7];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
        Main.dust[index7].noGravity = true;
      }
      for (int index8 = 0; index8 < 5; ++index8)
      {
        int index9 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
        Main.dust[index9].noGravity = true;
        Dust dust3 = Main.dust[index9];
        dust3.velocity = Vector2.op_Multiply(dust3.velocity, 7f);
        int index10 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust4 = Main.dust[index10];
        dust4.velocity = Vector2.op_Multiply(dust4.velocity, 3f);
      }
      float num1 = 0.5f;
      for (int index11 = 0; index11 < 4; ++index11)
      {
        int index12 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore1 = Main.gore[index12];
        gore1.velocity = Vector2.op_Multiply(gore1.velocity, num1);
        Gore gore2 = Main.gore[index12];
        gore2.velocity = Vector2.op_Addition(gore2.velocity, Utils.RotatedBy(new Vector2(1f, 1f), 1.5707963705062866 * (double) index11, new Vector2()));
      }
      if (this.Projectile.owner != Main.myPlayer)
        return;
      int num2 = 2;
      for (int index13 = 0; index13 < num2; ++index13)
      {
        int index14 = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2((float) Main.rand.Next(-10, 10), (float) Main.rand.Next(-10, 10)), 307, this.Projectile.damage / 6, this.Projectile.knockBack / 6f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        if (index14 != Main.maxProjectiles)
          Main.projectile[index14].DamageType = DamageClass.Ranged;
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
      this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.White, this.Projectile.Opacity), 0.75f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
