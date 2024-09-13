// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.TrojanSquirrel.TrojanSquirrelProj
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.TrojanSquirrel
{
  public class TrojanSquirrelProj : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 16;
      ((Entity) this.Projectile).height = 16;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
    }

    public virtual void AI()
    {
      ((Entity) this.Projectile).velocity.Y += this.Projectile.ai[0];
      if ((double) --this.Projectile.ai[1] <= 0.0 && !this.Projectile.tileCollide && !Collision.SolidCollision(((Entity) this.Projectile).Center, 0, 0))
        this.Projectile.tileCollide = true;
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
      if ((double) this.Projectile.localAI[0] != 0.0)
        return;
      this.Projectile.localAI[0] = Utils.NextBool(Main.rand) ? 1f : -1f;
    }

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      fallThrough = false;
      return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<GuiltyBuff>(), 120, true, false);
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundStyle npcDeath1 = SoundID.NPCDeath1;
      ((SoundStyle) ref npcDeath1).Volume = 0.5f;
      SoundEngine.PlaySound(ref npcDeath1, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index = 0; index < 20; ++index)
        Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 5, 0.0f, -1f, 0, new Color(), 1f);
      if (Utils.NextBool(Main.rand, 5) && !Main.dedServ)
      {
        int index = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.op_Multiply(-0.1f, Utils.RotatedByRandom(((Entity) this.Projectile).oldVelocity, 0.78539818525314331)), ModContent.Find<ModGore>(((ModType) this).Mod.Name, Utils.NextBool(Main.rand) ? "TrojanSquirrelGore2" : "TrojanSquirrelGore2_2").Type, this.Projectile.scale);
        Main.gore[index].rotation = Utils.NextFloat(Main.rand, 6.28318548f);
      }
      SoundStyle soundStyle = SoundID.Item14;
      ((SoundStyle) ref soundStyle).Volume = 0.5f;
      SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 10; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
      }
      for (int index3 = 0; index3 < 5; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 2.5f);
        Main.dust[index4].noGravity = true;
        Dust dust1 = Main.dust[index4];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 5f);
        int index5 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 1f);
        Dust dust2 = Main.dust[index5];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
      }
      for (int index6 = 0; index6 < 2; ++index6)
      {
        int index7 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore1 = Main.gore[index7];
        gore1.velocity = Vector2.op_Multiply(gore1.velocity, 0.4f);
        Gore gore2 = Main.gore[index7];
        gore2.velocity = Vector2.op_Addition(gore2.velocity, Utils.RotatedBy(new Vector2(1f, 1f), 3.1415927410125732 * (double) index6, new Vector2()));
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
      Color alpha = this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(alpha, this.Projectile.Opacity), 0.75f), 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
