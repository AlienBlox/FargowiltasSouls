// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantRitual2
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantRitual2 : ModProjectile
  {
    private const float PI = 3.14159274f;
    private const float rotationPerTick = 0.06283186f;
    private const float threshold = 700f;

    public virtual string Texture
    {
      get
      {
        return !FargoSoulsUtil.AprilFools ? "Terraria/Images/Projectile_454" : "FargowiltasSouls/Content/Bosses/MutantBoss/MutantSphere_April";
      }
    }

    public virtual void SetStaticDefaults()
    {
      ((ModType) this).SetStaticDefaults();
      Main.projFrames[this.Projectile.type] = 2;
      ProjectileID.Sets.DrawScreenCheckFluff[this.Projectile.type] = 2400;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 46;
      ((Entity) this.Projectile).height = 46;
      this.Projectile.scale *= 2f;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
    }

    public virtual void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>());
      if (npc != null && (double) npc.ai[0] == 10.0)
      {
        this.Projectile.alpha -= 17;
        if (this.Projectile.alpha < 0)
          this.Projectile.alpha = 0;
        ((Entity) this.Projectile).Center = ((Entity) npc).Center;
      }
      else
      {
        if (npc != null)
          ((Entity) this.Projectile).Center = ((Entity) npc).Center;
        ((Entity) this.Projectile).velocity = Vector2.Zero;
        this.Projectile.alpha += 9;
        if (this.Projectile.alpha > (int) byte.MaxValue)
        {
          this.Projectile.Kill();
          return;
        }
      }
      this.Projectile.timeLeft = 2;
      this.Projectile.scale = (float) (1.0 - (double) this.Projectile.alpha / (double) byte.MaxValue);
      this.Projectile.ai[0] -= 0.06283186f;
      if ((double) this.Projectile.ai[0] < 3.1415927410125732)
      {
        this.Projectile.ai[0] += 6.28318548f;
        this.Projectile.netUpdate = true;
      }
      ++this.Projectile.frameCounter;
      if (this.Projectile.frameCounter < 6)
        return;
      this.Projectile.frameCounter = 0;
      ++this.Projectile.frame;
      if (this.Projectile.frame <= 1)
        return;
      this.Projectile.frame = 0;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D1 = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle1;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle1).\u002Ector(0, num2, texture2D1.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle1), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      Texture2D texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/MutantBoss/MutantSphereGlow", (AssetRequestMode) 1).Value;
      int height = texture2D2.Height;
      int num3 = 0;
      Rectangle rectangle2;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle2).\u002Ector(0, num3, texture2D2.Width, height);
      Vector2 vector2_2 = Vector2.op_Division(Utils.Size(rectangle2), 2f);
      Color color1 = Color.Lerp(FargoSoulsUtil.AprilFools ? Color.Red : new Color(196, 247, (int) byte.MaxValue, 0), Color.Transparent, 0.8f);
      for (int index1 = 0; index1 < 7; ++index1)
      {
        Vector2 vector2_3 = Utils.RotatedBy(Utils.RotatedBy(new Vector2((float) (700.0 * (double) this.Projectile.scale / 2.0), 0.0f), (double) this.Projectile.ai[0], new Vector2()), 0.89759790897369385 * (double) index1, new Vector2());
        for (int index2 = 0; index2 < 4; ++index2)
        {
          Color color2 = Color.op_Multiply(alpha, (float) (4 - index2) / 4f);
          Vector2 vector2_4 = Vector2.op_Addition(((Entity) this.Projectile).Center, Utils.RotatedBy(vector2_3, 0.062831856310367584 * (double) index2, new Vector2()));
          float rotation = this.Projectile.rotation;
          Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(vector2_4, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle1), color2, rotation, vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
          Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(vector2_4, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle2), Color.op_Multiply(color1, (float) (4 - index2) / 4f), this.Projectile.rotation, vector2_2, this.Projectile.scale * 1.4f, (SpriteEffects) 0, 0.0f);
        }
        Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_3), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle1), alpha, this.Projectile.rotation, vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
        Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_3), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle2), color1, this.Projectile.rotation, vector2_2, this.Projectile.scale * 1.3f, (SpriteEffects) 0, 0.0f);
      }
      return false;
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }
  }
}
