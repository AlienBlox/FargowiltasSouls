// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.CrystalLeafShot
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class CrystalLeafShot : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_227";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 10;
      ((Entity) this.Projectile).height = 10;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 900;
      this.Projectile.aiStyle = -1;
      this.AIType = 227;
      this.Projectile.penetrate = -1;
    }

    public void VanillaAIStyleCrystalLeafShot()
    {
      this.Projectile.rotation = (float) Math.Atan2((double) ((Entity) this.Projectile).velocity.Y, (double) ((Entity) this.Projectile).velocity.X) + 3.14f;
      float num = (float) ((((1.0 - (double) this.Projectile.timeLeft / 180.0) * -6.0 * 0.85000002384185791 + 0.33000001311302185) % 1.0 + 1.0) % 1.0);
      bool flag = SoulConfig.Instance.BossRecolors && WorldSavingSystem.EternityMode;
      Color color1 = flag ? Color.Blue : Color.LimeGreen;
      Color color2 = Color.Lerp(flag ? color1 : Main.hslToRgb(num, 1f, 0.5f, byte.MaxValue), flag ? Color.DarkSlateGray : Color.Red, Utils.Remap(num, 0.33f, 0.7f, 0.0f, 1f, true));
      Color color3 = Color.Lerp(color2, Color.Lerp(color1, Color.Gold, 0.3f), (float) ((double) ((Color) ref color2).R / (double) byte.MaxValue * 1.0));
      if (this.Projectile.frameCounter++ >= 1)
      {
        this.Projectile.frameCounter = 0;
        ParticleOrchestrator.RequestParticleSpawn(true, (ParticleOrchestraType) 17, new ParticleOrchestraSettings()
        {
          PositionInWorld = ((Entity) this.Projectile).Center,
          MovementVector = ((Entity) this.Projectile).velocity,
          UniqueInfoPiece = (int) (byte) ((double) Main.rgbToHsl(color3).X * (double) byte.MaxValue)
        }, new int?());
      }
      Lighting.AddLight(((Entity) this.Projectile).Center, Vector3.op_Multiply(new Vector3(0.05f, 0.2f, 0.1f), 1.5f));
      if (!Utils.NextBool(Main.rand, 5))
        return;
      Dust dust = Dust.NewDustDirect(new Vector2(((Entity) this.Projectile).position.X, ((Entity) this.Projectile).position.Y), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 63, 0.0f, 0.0f, 0, new Color(), 1f);
      dust.noGravity = true;
      dust.velocity = Vector2.op_Multiply(dust.velocity, 0.1f);
      dust.scale = 1.5f;
      dust.velocity = Vector2.op_Addition(dust.velocity, Vector2.op_Multiply(((Entity) this.Projectile).velocity, Utils.NextFloat(Main.rand)));
      dust.color = color3;
      ref Color local = ref dust.color;
      ((Color) ref local).A = (byte) ((uint) ((Color) ref local).A / 4U);
      dust.alpha = 100;
      dust.noLight = true;
    }

    public virtual void AI()
    {
      if (!Collision.SolidCollision(Vector2.op_Addition(((Entity) this.Projectile).position, ((Entity) this.Projectile).velocity), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height))
      {
        if ((!SoulConfig.Instance.BossRecolors ? 0 : (WorldSavingSystem.EternityMode ? 1 : 0)) != 0)
          Lighting.AddLight(((Entity) this.Projectile).Center, 0.09803922f, 0.184313729f, 0.2509804f);
        else
          Lighting.AddLight(Vector2.op_Addition(((Entity) this.Projectile).Center, ((Entity) this.Projectile).velocity), 0.1f, 0.4f, 0.2f);
      }
      if (this.Projectile.timeLeft < 780)
        this.Projectile.tileCollide = true;
      if ((double) this.Projectile.ai[1] > 0.0)
      {
        if ((double) this.Projectile.ai[1] > 10.0)
        {
          Player player = Main.player[(int) this.Projectile.ai[2]];
          if (player.Alive())
          {
            float num = FargoSoulsUtil.RotationDifference(((Entity) this.Projectile).velocity, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) player).Center));
            ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, (double) Math.Sign(num) * (double) Math.Min(Math.Abs(num), (float) Math.PI / 70f), new Vector2());
            this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
          }
        }
        Projectile projectile = this.Projectile;
        ((Entity) projectile).position = Vector2.op_Subtraction(((Entity) projectile).position, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.9f));
        ++this.Projectile.ai[1];
        if ((double) this.Projectile.ai[1] > 70.0)
        {
          this.Projectile.ai[1] = 0.0f;
          this.Projectile.netUpdate = true;
        }
      }
      this.VanillaAIStyleCrystalLeafShot();
    }

    public virtual void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
    {
      if (target.hurtCooldowns[1] != 0)
        return;
      target.AddBuff(ModContent.BuffType<IvyVenomBuff>(), 240, true, false);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = (!SoulConfig.Instance.BossRecolors ? 0 : (WorldSavingSystem.EternityMode ? 1 : 0)) != 0 ? ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/Masomode/CrystalLeafShot", (AssetRequestMode) 2).Value : TextureAssets.Projectile[this.Type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Color alpha = this.Projectile.GetAlpha(lightColor);
      float num3 = this.Projectile.scale * 1.5f;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2, num3, spriteEffects, 0.0f);
      ((Color) ref alpha).A = (byte) 150;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2, num3, spriteEffects, 0.0f);
      return false;
    }
  }
}
