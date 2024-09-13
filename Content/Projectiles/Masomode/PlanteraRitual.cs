// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.PlanteraRitual
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class PlanteraRitual : BaseArena
  {
    private const float realRotation = -0.0174532924f;

    public virtual string Texture => "Terraria/Images/Projectile_226";

    public PlanteraRitual()
      : base(-1f * (float) Math.PI / 180f, 1100f, 262, 44)
    {
    }

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      ProjectileID.Sets.DrawScreenCheckFluff[this.Projectile.type] = 10000;
    }

    protected override void Movement(NPC npc)
    {
      ((Entity) this.Projectile).velocity = Vector2.op_Subtraction(((Entity) npc).Center, ((Entity) this.Projectile).Center);
      Projectile projectile = this.Projectile;
      ((Entity) projectile).velocity = Vector2.op_Division(((Entity) projectile).velocity, 40f);
      this.rotationPerTick = -1f * (float) Math.PI / 180f;
    }

    public override void AI()
    {
      base.AI();
      --this.Projectile.rotation;
    }

    public override Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.op_Multiply(lightColor, this.Projectile.Opacity), (double) this.targetPlayer == (double) Main.myPlayer ? 1f : 0.15f));
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(ModContent.BuffType<IvyVenomBuff>(), 120, true, false);
    }

    public override bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      Color alpha = this.Projectile.GetAlpha(lightColor);
      for (int index1 = 0; index1 < 32; ++index1)
      {
        int num2 = (this.Projectile.frame + index1) % Main.projFrames[this.Projectile.type];
        int num3 = num1 * num2;
        Rectangle rectangle;
        // ISSUE: explicit constructor call
        ((Rectangle) ref rectangle).\u002Ector(0, num3, texture2D.Width, num1);
        Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
        Vector2 vector2_2 = Utils.RotatedBy(Utils.RotatedBy(new Vector2((float) ((double) this.threshold * (double) this.Projectile.scale / 2.0), 0.0f), (double) this.Projectile.ai[0], new Vector2()), 0.19634954631328583 * (double) index1, new Vector2());
        float num4 = Utils.ToRotation(vector2_2) + 1.57079637f;
        for (int index2 = 0; index2 < 4; ++index2)
        {
          Color color = Color.op_Multiply(alpha, (float) (4 - index2) / 4f);
          Vector2 vector2_3 = Vector2.op_Addition(((Entity) this.Projectile).Center, Utils.RotatedBy(vector2_2, (double) this.rotationPerTick * (double) -index2, new Vector2()));
          float num5 = num4;
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(vector2_3, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num5, vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
        }
        float num6 = num4;
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_2), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, num6, vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      return false;
    }
  }
}
