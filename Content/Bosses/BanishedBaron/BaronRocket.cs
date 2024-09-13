// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.BanishedBaron.BaronRocket
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Graphics.Particles;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.BanishedBaron
{
  public class BaronRocket : ModProjectile
  {
    public bool home = true;
    public bool BeenOutside;
    private Vector2 HomePos = Vector2.Zero;

    public bool Rocket
    {
      get => (double) this.Projectile.ai[0] != 3.0 && (double) this.Projectile.ai[0] != 4.0;
    }

    public int MaxFrames => !this.Rocket ? 4 : 2;

    public virtual void SetStaticDefaults() => Main.projFrames[this.Type] = 4;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 16;
      ((Entity) this.Projectile).height = 16;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = 1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.scale = 1f;
      this.Projectile.light = 1f;
    }

    public virtual void AI()
    {
      Vector2 worldPosition = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), 54f), this.Projectile.scale), 2f)), Utils.NextVector2Circular(Main.rand, 10f, 10f));
      if (Utils.NextBool(Main.rand, 6) && (double) ((Vector2) ref ((Entity) this.Projectile).velocity).LengthSquared() > 9.0)
      {
        if (this.Rocket)
        {
          Dust.NewDust(worldPosition, 2, 2, 6, -((Entity) this.Projectile).velocity.X, -((Entity) this.Projectile).velocity.Y, 0, new Color(), 1f);
        }
        else
        {
          if (Main.netMode != 2)
            new Bubble(worldPosition, Vector2.op_Division(Vector2.op_Multiply(Vector2.op_UnaryNegation(Utils.RotatedByRandom(((Entity) this.Projectile).velocity, 0.37699112296104431)), Utils.NextFloat(Main.rand, 0.6f, 1f)), 2f), 1f, 30, rotation: Utils.NextFloat(Main.rand, 6.28318548f)).Spawn();
          Dust.NewDust(worldPosition, 2, 2, 33, -((Entity) this.Projectile).velocity.X, -((Entity) this.Projectile).velocity.Y, 0, new Color(), 1f);
        }
      }
      this.Projectile.rotation = Utils.ToRotation(Utils.RotatedBy(((Entity) this.Projectile).velocity, 3.1415927410125732, new Vector2()));
      if ((double) ++this.Projectile.localAI[0] > 600.0)
        this.Projectile.Kill();
      if (!this.Projectile.tileCollide && (double) this.Projectile.localAI[0] > (double) (60 * this.Projectile.MaxUpdates))
      {
        Tile tileSafely = Framing.GetTileSafely(((Entity) this.Projectile).Center);
        if (!((Tile) ref tileSafely).HasUnactuatedTile || !Main.tileSolid[(int) ((Tile) ref tileSafely).TileType] || Main.tileSolidTop[(int) ((Tile) ref tileSafely).TileType])
        {
          ++this.Projectile.localAI[1];
          if ((double) this.Projectile.localAI[1] > 15.0)
            this.Projectile.tileCollide = true;
        }
      }
      if (++this.Projectile.frameCounter > 8)
      {
        if (++this.Projectile.frame >= this.MaxFrames)
          this.Projectile.frame = 0;
        this.Projectile.frameCounter = 0;
      }
      if (Vector2.op_Equality(this.HomePos, Vector2.Zero))
      {
        Player player = Main.player[(int) this.Projectile.ai[1]];
        if (player != null && ((Entity) player).active && !player.ghost)
          this.HomePos = ((Entity) player).Center;
      }
      if ((double) this.Projectile.ai[0] == 2.0)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.05f);
      }
      if ((double) this.Projectile.ai[0] == 4.0)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.05f);
      }
      if ((double) this.Projectile.ai[0] != 3.0 && (double) this.Projectile.ai[0] != 1.0)
        return;
      Player player1 = Main.player[(int) this.Projectile.ai[1]];
      bool flag = (double) this.Projectile.ai[0] == 3.0 && (double) this.Projectile.localAI[0] > 10.0 || (double) this.Projectile.ai[0] == 1.0 && (double) this.Projectile.localAI[0] > 40.0;
      if (!flag)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 0.96f);
      }
      if (((player1 == null || !((Entity) player1).active ? 0 : (!player1.ghost ? 1 : 0)) & (flag ? 1 : 0)) == 0)
        return;
      Vector2 vector2 = Vector2.op_Subtraction(this.LerpWithoutClamp(this.HomePos, ((Entity) player1).Center, this.Projectile.ai[2]), ((Entity) this.Projectile).Center);
      float num1 = (double) this.Projectile.ai[0] == 1.0 ? 24f : 18f;
      float num2 = (double) this.Projectile.ai[0] == 1.0 ? 24f : 20f;
      float num3 = WorldSavingSystem.MasochistModeReal ? 150f : 180f;
      double num4 = (double) ((Vector2) ref vector2).Length();
      if (num4 > (double) num3 && this.home)
      {
        ((Vector2) ref vector2).Normalize();
        vector2 = Vector2.op_Multiply(vector2, num1);
        ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, num2 - 1f), vector2), num2);
      }
      if (Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
      {
        ((Entity) this.Projectile).velocity.X = -0.15f;
        ((Entity) this.Projectile).velocity.Y = -0.05f;
      }
      if (num4 > (double) num3)
        this.BeenOutside = true;
      if (num4 >= (double) num3 || !this.BeenOutside)
        return;
      this.home = false;
    }

    private Vector2 LerpWithoutClamp(Vector2 A, Vector2 B, float t)
    {
      return Vector2.op_Addition(A, Vector2.op_Multiply(Vector2.op_Subtraction(B, A), t));
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(24, 600, true, false);
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(323, 600, true, false);
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 3f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
      }
      for (int index3 = 0; index3 < 15; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
        Main.dust[index4].noGravity = true;
        Dust dust1 = Main.dust[index4];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
        int index5 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust2 = Main.dust[index5];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
      }
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = this.Rocket ? TextureAssets.Projectile[this.Projectile.type].Value : ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/BanishedBaron/BaronRocketTorp", (AssetRequestMode) 1).Value;
      int num1 = texture2D.Height / this.MaxFrames;
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Vector2 vector2_2 = Vector2.op_Division(Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), (float) (texture2D.Width - ((Entity) this.Projectile).width)), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.75f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(Vector2.op_Addition(oldPo, vector2_2), Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_2), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
