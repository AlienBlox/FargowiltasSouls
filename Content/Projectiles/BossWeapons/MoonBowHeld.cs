// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.MoonBowHeld
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class MoonBowHeld : ModProjectile
  {
    private int syncTimer;
    private Vector2 mousePos;
    private const int theTime = 300;
    private const int window = 60;

    public virtual string Texture => "FargowiltasSouls/Content/Items/Weapons/BossDrops/MoonBow";

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 28;
      ((Entity) this.Projectile).height = 62;
      this.Projectile.aiStyle = -1;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.FargoSouls().CanSplit = false;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.hide = true;
      this.Projectile.netImportant = true;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.mousePos.X);
      writer.Write(this.mousePos.Y);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      Vector2 vector2;
      vector2.X = reader.ReadSingle();
      vector2.Y = reader.ReadSingle();
      if (this.Projectile.owner == Main.myPlayer)
        return;
      this.mousePos = vector2;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      if (player.dead || !((Entity) player).active || player.ghost || ((Entity) player).whoAmI == Main.myPlayer && !player.controlUseTile)
      {
        this.Projectile.Kill();
        if (!((Entity) player).active || ((Entity) player).whoAmI != Main.myPlayer || (double) this.Projectile.localAI[0] < 300.0 || (double) this.Projectile.localAI[0] > 360.0)
          return;
        player.AddBuff(ModContent.BuffType<MoonBowBuff>(), 600, true, false);
        SoundEngine.PlaySound(ref SoundID.NPCDeath6, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        for (int index1 = 0; index1 < 30; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 100, new Color(), 3f);
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
        }
        for (int index3 = 0; index3 < 20; ++index3)
        {
          int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
          Main.dust[index4].noGravity = true;
          Dust dust1 = Main.dust[index4];
          dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
          int index5 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
          Dust dust2 = Main.dust[index5];
          dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
        }
        for (int index6 = 0; index6 < 50; ++index6)
        {
          int index7 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 100, new Color(), 2f);
          Main.dust[index7].noGravity = true;
          Dust dust3 = Main.dust[index7];
          dust3.velocity = Vector2.op_Multiply(dust3.velocity, 21f * this.Projectile.scale);
          Main.dust[index7].noLight = true;
          int index8 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 100, new Color(), 1f);
          Dust dust4 = Main.dust[index8];
          dust4.velocity = Vector2.op_Multiply(dust4.velocity, 12f);
          Main.dust[index8].noGravity = true;
          Main.dust[index8].noLight = true;
        }
        for (int index9 = 0; index9 < 40; ++index9)
        {
          int index10 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 100, new Color(), Utils.NextFloat(Main.rand, 2f, 5f));
          if (Utils.NextBool(Main.rand, 3))
            Main.dust[index10].noGravity = true;
          Dust dust = Main.dust[index10];
          dust.velocity = Vector2.op_Multiply(dust.velocity, Utils.NextFloat(Main.rand, 12f, 18f));
          Main.dust[index10].position = ((Entity) this.Projectile).Center;
        }
      }
      else
      {
        this.Projectile.hide = false;
        Vector2 mountedCenter = player.MountedCenter;
        ((Entity) this.Projectile).velocity = Vector2.op_Multiply(22f, Vector2.Normalize(Vector2.op_Subtraction(this.mousePos, player.MountedCenter)));
        ((Entity) this.Projectile).Center = mountedCenter;
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
        float num1 = (double) ((Entity) this.Projectile).direction * (double) player.gravDir < 0.0 ? 3.14159274f : 0.0f;
        float num2 = ((Entity) this.Projectile).direction < 0 ? 3.14159274f : 0.0f;
        player.itemRotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + num2;
        player.itemRotation = MathHelper.WrapAngle(player.itemRotation);
        player.ChangeDir(((Entity) this.Projectile).direction);
        player.heldProj = ((Entity) this.Projectile).whoAmI;
        player.itemTime = 10;
        player.itemAnimation = 10;
        this.Projectile.spriteDirection = ((Entity) this.Projectile).direction * (int) player.gravDir;
        this.Projectile.rotation -= num1;
        if (this.Projectile.owner == Main.myPlayer)
        {
          this.mousePos = Main.MouseWorld;
          if (++this.syncTimer > 20)
          {
            this.syncTimer = 0;
            this.Projectile.netUpdate = true;
          }
        }
        if ((double) ++this.Projectile.localAI[0] == 300.0)
        {
          SoundStyle soundStyle = SoundID.Item29;
          ((SoundStyle) ref soundStyle).Volume = 1.5f;
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        }
        else if ((double) this.Projectile.localAI[0] == 360.0)
        {
          SoundStyle soundStyle = SoundID.Item8;
          ((SoundStyle) ref soundStyle).Volume = 1.5f;
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        }
        if ((double) this.Projectile.localAI[0] >= 360.0)
          return;
        if ((double) this.Projectile.localAI[0] > 300.0)
          this.Projectile.localAI[1] = MathHelper.Lerp(this.Projectile.localAI[1], 1f, 0.05f);
        float num3 = this.Projectile.localAI[0] / 360f;
        int index = Dust.NewDust(((Entity) player).Center, 0, 0, 56, 0.0f, 0.0f, 200, new Color(0, (int) byte.MaxValue, (int) byte.MaxValue, 100), 0.5f);
        Main.dust[index].noGravity = true;
        Dust dust5 = Main.dust[index];
        dust5.velocity = Vector2.op_Multiply(dust5.velocity, 0.75f);
        Main.dust[index].fadeIn = 1.3f;
        Vector2 vector2_1;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_1).\u002Ector((float) Main.rand.Next(-100, 101), (float) Main.rand.Next(-100, 101));
        ((Vector2) ref vector2_1).Normalize();
        Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, (float) Main.rand.Next(50, 100) * 0.04f);
        Main.dust[index].velocity = vector2_2;
        ((Vector2) ref vector2_2).Normalize();
        Vector2 vector2_3 = Vector2.op_Multiply(vector2_2, 34f);
        Main.dust[index].scale *= num3;
        Vector2 vector2_4 = Vector2.op_Multiply(vector2_3, num3 * 5f);
        Dust dust6 = Main.dust[index];
        dust6.velocity = Vector2.op_Multiply(dust6.velocity, num3 * 5f);
        Main.dust[index].position = Vector2.op_Subtraction(((Entity) this.Projectile).Center, vector2_4);
        Dust dust7 = Main.dust[index];
        dust7.velocity = Vector2.op_Addition(dust7.velocity, ((Entity) player).velocity);
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      Color color = lightColor;
      if ((double) this.Projectile.localAI[0] < 360.0)
      {
        float num = Math.Min(1f, this.Projectile.localAI[0] / 300f);
        ((Color) ref color).A = (byte) ((double) byte.MaxValue - 200.0 * (double) num);
      }
      return new Color?(color);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      FargoSoulsUtil.GenericProjectileDraw(this.Projectile, lightColor);
      if ((double) this.Projectile.localAI[0] >= 300.0 && (double) this.Projectile.localAI[0] <= 360.0)
      {
        Color color;
        // ISSUE: explicit constructor call
        ((Color) ref color).\u002Ector(51, (int) byte.MaxValue, 191);
        float num1 = this.Projectile.localAI[0] - 300f;
        float num2 = 6.28318548f * this.Projectile.localAI[1];
        float num3 = Math.Min(1f, (float) Math.Sin(Math.PI * (double) num1 / 60.0) * 2f);
        float num4 = Math.Min(1f, num3 * 2f);
        float num5 = (float) ((double) this.Projectile.scale * (double) num3 * (double) Main.cursorScale * 1.25);
        Texture2D texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/Effects/LifeStar", (AssetRequestMode) 1).Value;
        Rectangle rectangle;
        // ISSUE: explicit constructor call
        ((Rectangle) ref rectangle).\u002Ector(0, 0, texture2D.Width, texture2D.Height);
        Vector2 vector2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2).\u002Ector((float) (texture2D.Width / 2) + num5, (float) (texture2D.Height / 2) + num5);
        Vector2 center = ((Entity) this.Projectile).Center;
        Main.spriteBatch.End();
        Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
        Main.spriteBatch.Draw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), Color.op_Multiply(color, num4), num2, vector2, num5, (SpriteEffects) 0, 0.0f);
        Main.spriteBatch.Draw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), Color.op_Multiply(Color.op_Multiply(Color.White, num4), 0.75f), num2, vector2, num5, (SpriteEffects) 0, 0.0f);
        Main.spriteBatch.End();
        Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      }
      return false;
    }
  }
}
