// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.DanielTheRobot.ROB
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.DanielTheRobot
{
  public class ROB : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 7;
      Main.projPet[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.netImportant = true;
      ((Entity) this.Projectile).width = 40;
      ((Entity) this.Projectile).height = 60;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft *= 5;
      this.Projectile.aiStyle = 26;
    }

    public virtual bool PreAI()
    {
      Player player = Main.player[this.Projectile.owner];
      PatreonPlayer modPlayer = player.GetModPlayer<PatreonPlayer>();
      if (!((Entity) player).active || player.dead || player.ghost)
        modPlayer.ROB = false;
      if (modPlayer.ROB)
        this.Projectile.timeLeft = 2;
      this.HandleAnimation();
      return true;
    }

    private void HandleAnimation()
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = 5;
      if ((double) this.Projectile.ai[0] != 1.0)
        goto label_2;
label_1:
      num1 = 4;
      num2 = 6;
      goto label_7;
label_2:
      if ((double) ((Entity) this.Projectile).velocity.Y > 0.0)
        num1 = num2 = 1;
      else if ((double) ((Entity) this.Projectile).velocity.Y >= 0.0)
      {
        if ((double) ((Entity) this.Projectile).velocity.X != 0.0)
        {
          num1 = 2;
          num2 = 3;
        }
      }
      else
        goto label_1;
label_7:
      if (this.Projectile.frame < num1 || this.Projectile.frame > num2)
        this.Projectile.frame = num1;
      if (++this.Projectile.frameCounter <= num3)
        return;
      this.Projectile.frameCounter = 0;
      if (++this.Projectile.frame <= num2)
        return;
      this.Projectile.frame = num1;
    }

    private Asset<Texture2D> EyebrowAsset
    {
      get => ModContent.Request<Texture2D>(this.Texture + "_Eyebrows", (AssetRequestMode) 2);
    }

    private Asset<Texture2D> GlowAsset
    {
      get => ModContent.Request<Texture2D>(this.Texture + "Glow", (AssetRequestMode) 2);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Vector2 vector2_2 = Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitY, 10f), this.Projectile.scale);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 1 : (SpriteEffects) 0;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_2), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      if (Luminance.Common.Utilities.Utilities.AnyBosses())
        Main.EntitySpriteDraw(Asset<Texture2D>.op_Explicit(this.EyebrowAsset), Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_2), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      Main.EntitySpriteDraw(Asset<Texture2D>.op_Explicit(this.GlowAsset), Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_2), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), Color.White, this.Projectile.rotation, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
