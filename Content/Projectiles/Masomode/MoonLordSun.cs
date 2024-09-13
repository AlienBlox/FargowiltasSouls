// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.MoonLordSun
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class MoonLordSun : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 7;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 410;
      ((Entity) this.Projectile).height = 410;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.extraUpdates = 0;
      this.CooldownSlot = 1;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
      this.Projectile.penetrate = -1;
      this.Projectile.scale = 0.75f;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
    }

    public virtual bool? CanDamage() => new bool?((double) this.Projectile.localAI[0] > 120.0);

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      int num1 = ((Rectangle) ref projHitbox).Center.X - ((Rectangle) ref targetHitbox).Center.X;
      int num2 = ((Rectangle) ref projHitbox).Center.Y - ((Rectangle) ref targetHitbox).Center.Y;
      if (Math.Abs(num1) > targetHitbox.Width / 2)
        num1 = targetHitbox.Width / 2 * Math.Sign(num1);
      if (Math.Abs(num2) > targetHitbox.Height / 2)
        num2 = targetHitbox.Height / 2 * Math.Sign(num2);
      int num3 = ((Rectangle) ref projHitbox).Center.X - ((Rectangle) ref targetHitbox).Center.X - num1;
      int num4 = ((Rectangle) ref projHitbox).Center.Y - ((Rectangle) ref targetHitbox).Center.Y - num2;
      return new bool?(Math.Sqrt((double) (num3 * num3 + num4 * num4)) <= (double) (((Entity) this.Projectile).width / 2));
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.Projectile.localAI[0]);
      writer.Write(this.Projectile.localAI[1]);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.Projectile.localAI[0] = reader.ReadSingle();
      this.Projectile.localAI[1] = reader.ReadSingle();
    }

    public virtual void AI()
    {
      NPC npc1 = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], 398);
      NPC npc2 = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], 397);
      if (npc2 == null || npc1 == null || (double) npc2.ai[3] != (double) ((Entity) npc1).whoAmI || (double) npc1.ai[0] == 2.0)
      {
        this.Projectile.Kill();
      }
      else
      {
        if ((double) ++this.Projectile.localAI[0] < 120.0)
        {
          Vector2 center = ((Entity) npc1).Center;
          center.X += (float) (576 * Math.Sign(((Entity) npc2).Center.X - ((Entity) npc1).Center.X));
          center.Y -= 384f;
          ((Entity) npc2).Center = Vector2.Lerp(((Entity) npc2).Center, center, 0.03f);
          ((Entity) this.Projectile).Center = ((Entity) npc2).Center;
          ((Entity) this.Projectile).position.Y -= 250f * Math.Min(1f, this.Projectile.localAI[0] / 85f);
          if ((double) this.Projectile.localAI[0] < 60.0)
          {
            Vector2 vector2 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(10f, Vector2.op_Subtraction(((Entity) npc2).position, ((Entity) npc2).oldPosition)));
            for (int index = 0; index < 10; ++index)
            {
              if (!Utils.NextBool(Main.rand, 4))
              {
                float num1 = Utils.NextFloat(Main.rand) * 6.28318548f;
                float num2 = Utils.NextFloat(Main.rand);
                Dust dust = Dust.NewDustPerfect(Vector2.op_Addition(vector2, Vector2.op_Multiply(Utils.ToRotationVector2(num1), (float) (440.0 + 110.0 * (double) num2 * 8.0))), 262, new Vector2?(Vector2.op_Multiply(Utils.ToRotationVector2(num1 - 3.14159274f), (float) (40.0 + 8.0 * (double) num2 * 8.0))), 0, new Color(), 1f);
                dust.scale = 3.2f;
                dust.noGravity = true;
              }
            }
          }
          this.Projectile.alpha -= 3;
          if (this.Projectile.alpha < 0)
            this.Projectile.alpha = 0;
        }
        else if ((double) this.Projectile.localAI[0] == 120.0)
        {
          ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Multiply(Vector2.op_Subtraction(((Entity) Main.player[npc1.target]).Center, ((Entity) this.Projectile).Center), 2f), 90f);
          this.Projectile.localAI[1] = ((Vector2) ref ((Entity) this.Projectile).velocity).Length() / 90f;
          this.Projectile.timeLeft = 91;
          this.Projectile.netUpdate = true;
          SoundEngine.PlaySound(ref SoundID.Item62, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        }
        else
        {
          ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), ((Vector2) ref ((Entity) this.Projectile).velocity).Length() - this.Projectile.localAI[1]);
          this.Projectile.alpha = 0;
        }
        this.Projectile.rotation = Utils.NextFloat(Main.rand, 6.28318548f);
        this.Projectile.scale = Utils.NextFloat(Main.rand, 0.95f, 1.05f);
      }
    }

    public virtual void OnKill(int timeLeft)
    {
      NPC npc1 = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], 398);
      NPC npc2 = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], 397);
      if (npc2 == null || npc1 == null || (double) npc2.ai[3] != (double) ((Entity) npc1).whoAmI || (double) npc1.ai[0] == 2.0)
      {
        if (!FargoSoulsUtil.HostCheck)
          return;
        Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<MoonLordSunBlast>(), 0, 0.0f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
      }
      else
      {
        if (!Main.dedServ && ((Entity) Main.LocalPlayer).active)
          ScreenShakeSystem.StartShake(10f, 6.28318548f, new Vector2?(), 0.333333343f);
        if (!FargoSoulsUtil.HostCheck)
          return;
        for (int index = 0; index < 4; ++index)
        {
          Vector2 vector2 = Vector2.op_Multiply((float) (((Entity) this.Projectile).width / 2), Utils.RotatedBy(Vector2.UnitX, Math.PI / 2.0 * (double) index, new Vector2()));
          Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), Vector2.op_Addition(((Entity) this.Projectile).Center, Utils.NextVector2Circular(Main.rand, (float) (((Entity) this.Projectile).width / 2), (float) (((Entity) this.Projectile).height / 2))), Vector2.Zero, ModContent.ProjectileType<MoonLordSunBlast>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, MathHelper.WrapAngle(Utils.ToRotation(vector2)), 32f, 0.0f);
        }
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(67, 120, true, false);
      target.AddBuff(24, 300, true, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      Color lightYellow = Color.LightYellow;
      ((Color) ref lightYellow).A = (byte) 0;
      return new Color?(lightYellow);
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
      Main.spriteBatch.Draw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), Color.op_Multiply(alpha, this.Projectile.Opacity), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      Main.spriteBatch.Draw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), Color.op_Multiply(alpha, 0.35f), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
