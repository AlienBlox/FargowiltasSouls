// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.BanishedBaron.BaronEyeFlash
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.BanishedBaron
{
  public class BaronEyeFlash : ModProjectile
  {
    public virtual string Texture => FargoSoulsUtil.EmptyTexture;

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 10;
      ((Entity) this.Projectile).height = 10;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = false;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.scale = 1f;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 1.0)
      {
        this.Projectile.ai[1] = Utils.NextBool(Main.rand) ? 1f : -1f;
        SoundEngine.PlaySound(ref SoundID.MaxMana, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      this.Projectile.rotation += (float) ((double) this.Projectile.ai[1] * 6.2831854820251465 / 90.0);
      if ((double) ++this.Projectile.localAI[0] > 20.0)
      {
        this.Projectile.alpha += 10;
        this.Projectile.scale -= 0.05f;
      }
      NPC npc = Main.npc[(int) this.Projectile.ai[0]];
      if (npc.TypeAlive<FargowiltasSouls.Content.Bosses.BanishedBaron.BanishedBaron>())
      {
        ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(Vector2.op_Multiply(Utils.ToRotationVector2(npc.rotation), (float) ((Entity) npc).width), 0.35f));
        ((Entity) this.Projectile).velocity = ((Entity) npc).velocity;
      }
      if ((double) this.Projectile.scale > 0.05)
        return;
      this.Projectile.Kill();
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 5; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 8, 0.0f, 0.0f, 0, new Color(), 1.5f);
        Main.dust[index2].noGravity = true;
      }
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      Texture2D texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/Effects/LifeStar", (AssetRequestMode) 1).Value;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, 0, texture2D.Width, texture2D.Height);
      float num = this.Projectile.scale * Utils.NextFloat(Main.rand, 1.5f, 3f);
      Vector2 vector2;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2).\u002Ector((float) (texture2D.Width / 2) + num, (float) (texture2D.Height / 2) + num);
      Main.spriteBatch.Draw(texture2D, Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Rectangle?(rectangle), Color.op_Multiply(Color.HotPink, this.Projectile.Opacity), 0.0f, vector2, num, (SpriteEffects) 0, 0.0f);
      DrawData drawData;
      // ISSUE: explicit constructor call
      ((DrawData) ref drawData).\u002Ector(texture2D, Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Rectangle?(rectangle), Color.op_Multiply(Color.HotPink, this.Projectile.Opacity), 0.0f, vector2, num, (SpriteEffects) 0, 0.0f);
      GameShaders.Misc["LCWingShader"].UseColor(Color.op_Multiply(Color.HotPink, this.Projectile.Opacity)).UseSecondaryColor(Color.op_Multiply(Color.HotPink, this.Projectile.Opacity));
      GameShaders.Misc["LCWingShader"].Apply(new DrawData?());
      ((DrawData) ref drawData).Draw(Main.spriteBatch);
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      return false;
    }
  }
}
