// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.PhantasmalBolt2
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
  public class PhantasmalBolt2 : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_462";

    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 5;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 8;
      ((Entity) this.Projectile).height = 8;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.aiStyle = 1;
      this.AIType = 462;
      this.Projectile.penetrate = -1;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.hostile = true;
      this.Projectile.extraUpdates = 3;
      this.Projectile.timeLeft = 1200;
      this.CooldownSlot = 1;
    }

    public virtual void AI()
    {
      int index = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, 229, 0.0f, 0.0f, 100, new Color(), 1f);
      Main.dust[index].noLight = true;
      Main.dust[index].noGravity = true;
      Main.dust[index].velocity = ((Entity) this.Projectile).velocity;
      Dust dust = Main.dust[index];
      dust.position = Vector2.op_Subtraction(dust.position, Vector2.op_Multiply(Vector2.One, 4f));
      Main.dust[index].scale = 0.8f;
      if (++this.Projectile.frameCounter >= 24)
      {
        this.Projectile.frameCounter = 0;
        if (++this.Projectile.frame >= 5)
          this.Projectile.frame = 0;
      }
      if (this.Projectile.timeLeft % this.Projectile.MaxUpdates != 0)
        return;
      this.Projectile.alpha -= 2;
      if (this.Projectile.alpha < 0)
        this.Projectile.alpha = 0;
      if ((double) ++this.Projectile.localAI[1] <= 30.0)
        return;
      if ((double) this.Projectile.localAI[1] < 120.0)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.025f);
      }
      if ((double) this.Projectile.localAI[1] >= 120.0 || !FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.moonBoss, 398) || !Main.npc[EModeGlobalNPC.moonBoss].HasValidTarget)
        return;
      float rotation1 = Utils.ToRotation(((Entity) this.Projectile).velocity);
      float rotation2 = Utils.ToRotation(Vector2.op_Subtraction(((Entity) Main.player[Main.npc[EModeGlobalNPC.moonBoss].target]).Center, ((Entity) this.Projectile).Center));
      ((Entity) this.Projectile).velocity = Utils.RotatedBy(new Vector2(((Vector2) ref ((Entity) this.Projectile).velocity).Length(), 0.0f), (double) Utils.AngleLerp(rotation1, rotation2, 0.012f), new Vector2());
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 360, true, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 128), (float) (1.0 - (double) this.Projectile.alpha / (double) byte.MaxValue)));
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
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
