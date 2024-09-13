// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.MothDust
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class MothDust : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 12;
      ((Entity) this.Projectile).height = 12;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.timeLeft = 180;
      this.Projectile.scale = 0.5f;
    }

    public virtual void AI()
    {
      Projectile projectile = this.Projectile;
      ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 0.96f);
      if (Utils.NextBool(Main.rand))
      {
        int index = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 70, 0.0f, 0.0f, 0, new Color(), 1f);
        Main.dust[index].noGravity = true;
        Dust dust = Main.dust[index];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 2.5f);
      }
      Lighting.AddLight(((Entity) this.Projectile).position, 0.3f, 0.1f, 0.3f);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.deviBoss, ModContent.NPCType<FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss>()))
      {
        target.AddBuff(ModContent.BuffType<BerserkedBuff>(), 240, true, false);
        target.AddBuff(ModContent.BuffType<MutantNibbleBuff>(), 240, true, false);
        target.AddBuff(ModContent.BuffType<GuiltyBuff>(), 240, true, false);
        target.AddBuff(ModContent.BuffType<LovestruckBuff>(), 240, true, false);
        target.AddBuff(ModContent.BuffType<RottingBuff>(), 240, true, false);
      }
      else
      {
        for (int index1 = 0; index1 < 5; ++index1)
        {
          int index2 = Main.rand.Next(FargowiltasSouls.FargowiltasSouls.DebuffIDs.Count);
          target.AddBuff(FargowiltasSouls.FargowiltasSouls.DebuffIDs[index2], 240, true, false);
        }
      }
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      if ((double) ((Entity) this.Projectile).velocity.X != (double) oldVelocity.X)
        ((Entity) this.Projectile).velocity.X = -oldVelocity.X;
      if ((double) ((Entity) this.Projectile).velocity.Y != (double) oldVelocity.Y)
        ((Entity) this.Projectile).velocity.Y = -oldVelocity.Y;
      return false;
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = texture2D.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      SpriteEffects spriteEffects = (SpriteEffects) 0;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
