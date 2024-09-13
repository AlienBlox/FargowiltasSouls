// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.BrainIllusionProj
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class BrainIllusionProj : ModProjectile
  {
    private const int attackDelay = 120;

    public virtual string Texture => "Terraria/Images/NPC_266";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = Main.npcFrameCount[266];
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 120;
      ((Entity) this.Projectile).height = 80;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 240;
      this.Projectile.penetrate = -1;
      this.Projectile.scale += 0.25f;
    }

    public virtual bool? CanDamage() => new bool?((double) this.Projectile.ai[1] == 2.0);

    public virtual void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], 266);
      if (npc == null)
      {
        this.Projectile.Kill();
      }
      else
      {
        if (++this.Projectile.frameCounter > 6)
        {
          this.Projectile.frameCounter = 0;
          ++this.Projectile.frame;
        }
        if (this.Projectile.frame < 4 || this.Projectile.frame > 7)
          this.Projectile.frame = 4;
        if ((double) this.Projectile.ai[1] == 0.0)
          this.Projectile.alpha = (int) ((double) byte.MaxValue * (double) npc.life / (double) npc.lifeMax);
        else if ((double) this.Projectile.ai[1] == 1.0)
        {
          this.Projectile.alpha = (int) MathHelper.Lerp((float) this.Projectile.alpha, 0.0f, 0.02f);
          Projectile projectile = this.Projectile;
          ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, Vector2.op_Multiply(Vector2.op_Multiply(0.5f, Vector2.op_Subtraction(((Entity) Main.player[npc.target]).position, ((Entity) Main.player[npc.target]).oldPosition)), (float) (1.0 - (double) this.Projectile.localAI[0] / 120.0)));
          ((Entity) this.Projectile).velocity = Vector2.Zero;
          this.Projectile.timeLeft = 180;
          if ((double) ++this.Projectile.localAI[0] <= 120.0)
            return;
          this.Projectile.ai[1] = 2f;
          ((Entity) this.Projectile).velocity = Vector2.op_Multiply(18f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) Main.player[npc.target]).Center));
          this.Projectile.netUpdate = true;
          this.Projectile.localAI[0] = ((Entity) Main.player[npc.target]).Center.X;
          this.Projectile.localAI[1] = ((Entity) Main.player[npc.target]).Center.Y;
        }
        else
        {
          this.Projectile.alpha = 0;
          Projectile projectile = this.Projectile;
          ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.015f);
          if ((double) ((Entity) this.Projectile).Distance(new Vector2(this.Projectile.localAI[0], this.Projectile.localAI[1])) >= (double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() + 1.0)
            return;
          this.Projectile.Kill();
        }
      }
    }

    public virtual void OnKill(int timeLeft)
    {
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      if (!TextureAssets.Npc[266].IsLoaded)
        return false;
      Texture2D texture2D = TextureAssets.Npc[266].Value;
      int num1 = texture2D.Height / Main.npcFrameCount[266];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      bool? nullable = base.CanDamage();
      bool flag = true;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
        {
          Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
          Vector2 oldPo = this.Projectile.oldPos[index];
          float num3 = this.Projectile.oldRot[index];
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
        }
      }
      Vector2 vector2_2;
      if ((double) this.Projectile.ai[1] == 1.0)
      {
        float num4 = (float) (16.0 * (double) this.Projectile.localAI[0] / 120.0);
        vector2_2 = Utils.NextVector2Circular(Main.rand, num4, num4);
      }
      else
        vector2_2 = Vector2.Zero;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_2), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
