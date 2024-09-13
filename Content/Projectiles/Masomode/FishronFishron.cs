// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.FishronFishron
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class FishronFishron : MutantFishron
  {
    private bool firstTick;

    public override string Texture => FargoSoulsUtil.VanillaTextureNPC(370);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.scale *= 0.75f;
      this.CooldownSlot = -1;
    }

    public override bool CanHitPlayer(Player target) => true;

    public override bool PreAI()
    {
      if (!this.firstTick)
      {
        this.Projectile.timeLeft = 150 + Main.rand.Next(10);
        this.Projectile.netUpdate = true;
        this.firstTick = true;
      }
      if ((double) this.Projectile.localAI[0] > 85.0)
      {
        int num = 7;
        for (int index1 = 0; index1 < num; ++index1)
        {
          Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), new Vector2((float) (((Entity) this.Projectile).width + 50) / 2f, (float) ((Entity) this.Projectile).height)), 0.75f), (double) (index1 - (num / 2 - 1)) * Math.PI / (double) num, new Vector2()), ((Entity) this.Projectile).Center);
          Vector2 vector2_2 = Vector2.op_Multiply(Utils.ToRotationVector2((float) (Main.rand.NextDouble() * 3.14159274101257) - 1.57079637f), (float) Main.rand.Next(3, 8));
          Vector2 vector2_3 = vector2_2;
          int index2 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_3), 0, 0, 172, vector2_2.X * 2f, vector2_2.Y * 2f, 100, new Color(), 1.4f);
          Main.dust[index2].noGravity = true;
          Main.dust[index2].noLight = true;
          Dust dust1 = Main.dust[index2];
          dust1.velocity = Vector2.op_Division(dust1.velocity, 4f);
          Dust dust2 = Main.dust[index2];
          dust2.velocity = Vector2.op_Subtraction(dust2.velocity, ((Entity) this.Projectile).velocity);
        }
      }
      return true;
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 600, true, false);
      target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 1200, true, false);
      target.FargoSouls().MaxLifeReduction += FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.fishBossEX, 370) ? 100 : 25;
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index = 0; index < 150; ++index)
        Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 5, (float) (2 * ((Entity) this.Projectile).direction), -2f, 0, new Color(), 1f);
      SoundStyle soundStyle = Utils.NextBool(Main.rand) ? SoundID.NPCDeath17 : SoundID.NPCDeath30;
      ((SoundStyle) ref soundStyle).Volume = 0.75f;
      ((SoundStyle) ref soundStyle).Pitch = 0.2f;
      SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      if (!Main.dedServ)
        Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, 20f), (float) ((Entity) this.Projectile).direction)), ((Entity) this.Projectile).velocity, 576, this.Projectile.scale);
      if (!Main.dedServ)
        Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.UnitY, 30f)), ((Entity) this.Projectile).velocity, 574, this.Projectile.scale);
      if (!Main.dedServ)
        Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, ((Entity) this.Projectile).velocity, 575, this.Projectile.scale);
      if (!Main.dedServ)
        Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, 20f), (float) ((Entity) this.Projectile).direction)), ((Entity) this.Projectile).velocity, 573, this.Projectile.scale);
      if (!Main.dedServ)
        Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.UnitY, 30f)), ((Entity) this.Projectile).velocity, 574, this.Projectile.scale);
      if (Main.dedServ)
        return;
      Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, ((Entity) this.Projectile).velocity, 575, this.Projectile.scale);
    }

    public override bool PreDraw(ref Color lightColor)
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
      if ((double) this.Projectile.localAI[0] > 85.0)
      {
        for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index += 2)
        {
          Color color = Color.op_Multiply(Color.Lerp(alpha, Color.Blue, 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
          Vector2 oldPo = this.Projectile.oldPos[index];
          float num3 = this.Projectile.oldRot[index];
          if (this.Projectile.spriteDirection < 0)
            num3 += 3.14159274f;
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
        }
      }
      float rotation = this.Projectile.rotation;
      if (this.Projectile.spriteDirection < 0)
        rotation += 3.14159274f;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }

    public override Color? GetAlpha(Color lightColor)
    {
      float num1 = (float) ((int) byte.MaxValue - this.Projectile.alpha) / (float) byte.MaxValue;
      float num2 = MathHelper.Lerp(num1, 1f, 0.25f);
      if ((double) num2 > 1.0)
        num2 = 1f;
      return new Color?(new Color((int) ((double) ((Color) ref lightColor).R * (double) num1), (int) ((double) ((Color) ref lightColor).G * (double) num1), (int) ((double) ((Color) ref lightColor).B * (double) num2), (int) ((double) ((Color) ref lightColor).A * (double) num1)));
    }
  }
}
