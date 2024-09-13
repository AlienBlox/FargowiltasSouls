// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.DeviBoss.DeviHammerHeld
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.DeviBoss
{
  public class DeviHammerHeld : ModProjectile
  {
    private const int maxTime = 60;

    public virtual string Texture => "Terraria/Images/Projectile_300";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 30;
      ((Entity) this.Projectile).height = 30;
      this.Projectile.scale = 1.5f;
      this.Projectile.hostile = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 60;
      this.Projectile.aiStyle = -1;
      this.Projectile.penetrate = -1;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
      this.Projectile.hide = true;
    }

    public virtual void AI()
    {
      this.Projectile.hide = false;
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], ModContent.NPCType<FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss>());
      if (npc != null)
      {
        if ((double) this.Projectile.localAI[0] == 0.0)
        {
          this.Projectile.localAI[0] = 1f;
          this.Projectile.localAI[1] = this.Projectile.ai[1] / 60f;
        }
        ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, (double) this.Projectile.ai[1], new Vector2());
        this.Projectile.ai[1] -= this.Projectile.localAI[1];
        ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(Utils.RotatedBy(new Vector2(20f, 20f), (double) Utils.ToRotation(((Entity) this.Projectile).velocity) - 0.78539818525314331, new Vector2()), this.Projectile.scale));
        ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = Math.Sign(this.Projectile.ai[1]);
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + MathHelper.ToRadians(((Entity) this.Projectile).direction < 0 ? 135f : 45f);
      }
      else
        this.Projectile.Kill();
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 10; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 246, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Main.dust[index2].noGravity = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 3.5f);
        Main.dust[index2].noLight = true;
        int index3 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 246, 0.0f, 0.0f, 100, new Color(), 1f);
        Dust dust2 = Main.dust[index3];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 2f);
        Main.dust[index3].noGravity = true;
        Main.dust[index3].noLight = true;
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (target.HasBuff(ModContent.BuffType<StunnedBuff>()))
        return;
      target.AddBuff(ModContent.BuffType<StunnedBuff>(), 60, true, false);
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
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 1 : (SpriteEffects) 0;
      Color alpha = this.Projectile.GetAlpha(lightColor);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
