// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Earth.EarthChainBlast
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
namespace FargowiltasSouls.Content.Bosses.Champions.Earth
{
  public class EarthChainBlast : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_687";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = Main.projFrames[645];
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 100;
      ((Entity) this.Projectile).height = 100;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
      this.Projectile.penetrate = -1;
      this.Projectile.scale = 1f;
      this.Projectile.alpha = 0;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
    }

    public virtual bool? CanDamage()
    {
      return new bool?(this.Projectile.frame == 3 || this.Projectile.frame == 4);
    }

    public virtual void AI()
    {
      if (Utils.HasNaNs(((Entity) this.Projectile).position))
      {
        this.Projectile.Kill();
      }
      else
      {
        if (++this.Projectile.frameCounter >= 2)
        {
          this.Projectile.frameCounter = 0;
          if (++this.Projectile.frame >= Main.projFrames[this.Projectile.type])
          {
            --this.Projectile.frame;
            this.Projectile.Kill();
            return;
          }
          if (this.Projectile.frame == 3)
            this.Projectile.FargoSouls().GrazeCD = 0;
        }
        if ((double) ++this.Projectile.localAI[1] == 8.0 && (double) this.Projectile.ai[1] > 0.0 && FargoSoulsUtil.HostCheck)
        {
          --this.Projectile.ai[1];
          Vector2 rotationVector2 = Utils.ToRotationVector2(this.Projectile.ai[0]);
          float radians = MathHelper.ToRadians(15f);
          if ((double) this.Projectile.ai[1] > 2.0)
          {
            for (int index = -1; index <= 1; ++index)
            {
              if (index != 0)
              {
                Vector2 vector2 = Vector2.op_Multiply((float) ((Entity) this.Projectile).width * 1.25f, Utils.RotatedBy(rotationVector2, (double) MathHelper.ToRadians(60f) * (double) index + (double) Utils.NextFloat(Main.rand, -radians, radians), new Vector2()));
                Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), Vector2.op_Addition(((Entity) this.Projectile).Center, vector2), Vector2.Zero, this.Projectile.type, this.Projectile.damage, 0.0f, this.Projectile.owner, this.Projectile.ai[0], this.Projectile.ai[1], 0.0f);
              }
            }
          }
          else
          {
            Vector2 vector2 = Vector2.op_Multiply((float) ((Entity) this.Projectile).width * 2.25f, Utils.RotatedBy(rotationVector2, (double) Utils.NextFloat(Main.rand, -radians, radians), new Vector2()));
            Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), Vector2.op_Addition(((Entity) this.Projectile).Center, vector2), Vector2.Zero, this.Projectile.type, this.Projectile.damage, 0.0f, this.Projectile.owner, this.Projectile.ai[0], this.Projectile.ai[1], 0.0f);
          }
        }
        if ((double) this.Projectile.localAI[0] != 0.0)
          return;
        this.Projectile.localAI[0] = 1f;
        SoundEngine.PlaySound(ref SoundID.Item88, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
        this.Projectile.scale = Utils.NextFloat(Main.rand, 1f, 3f);
        ((Entity) this.Projectile).width = (int) ((double) ((Entity) this.Projectile).width * (double) this.Projectile.scale);
        ((Entity) this.Projectile).height = (int) ((double) ((Entity) this.Projectile).height * (double) this.Projectile.scale);
        ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(24, 300, true, false);
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(67, 300, true, false);
      target.AddBuff(ModContent.BuffType<LethargicBuff>(), 300, true, false);
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index = 0; index < 2; ++index)
        Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
      if (!Utils.NextBool(Main.rand, 8))
        return;
      int index1 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).position, new Vector2((float) (((Entity) this.Projectile).width * Main.rand.Next(100)) / 100f, (float) (((Entity) this.Projectile).height * Main.rand.Next(100)) / 100f)), Vector2.op_Multiply(Vector2.One, 10f)), new Vector2(), Main.rand.Next(61, 64), 1f);
      Gore gore = Main.gore[index1];
      gore.velocity = Vector2.op_Multiply(gore.velocity, 0.3f);
      Main.gore[index1].velocity.X += (float) Main.rand.Next(-10, 11) * 0.05f;
      Main.gore[index1].velocity.Y += (float) Main.rand.Next(-10, 11) * 0.05f;
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply((double) this.Projectile.ai[1] <= 3.0 ? Color.Lerp(new Color((int) byte.MaxValue, 95, 46, 50), new Color(150, 35, 0, 100), (float) ((3.0 - (double) this.Projectile.ai[1]) / 3.0)) : Color.Lerp(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0), new Color((int) byte.MaxValue, 95, 46, 50), (float) ((7.0 - (double) this.Projectile.ai[1]) / 4.0)), this.Projectile.Opacity));
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
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
