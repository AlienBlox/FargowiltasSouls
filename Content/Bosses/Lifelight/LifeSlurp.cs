// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Lifelight.LifeSlurp
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Lifelight
{
  public class LifeSlurp : ModProjectile
  {
    private int rGem = 1;
    public bool home = true;
    public bool homingonPlayer;
    public bool chosenDirection;
    public bool First = true;
    public NPC lifelight;
    private int RotDirect = 1;

    public virtual string Texture
    {
      get => "FargowiltasSouls/Assets/ExtraTextures/LifelightParts/ShardGem1";
    }

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 24;
      ((Entity) this.Projectile).height = 24;
      this.Projectile.aiStyle = 0;
      this.Projectile.hostile = true;
      this.AIType = 14;
      this.Projectile.penetrate = 1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.light = 0.5f;
      this.Projectile.scale = 1f;
    }

    public virtual bool? CanDamage() => new bool?(this.Projectile.alpha <= 0);

    public virtual void AI()
    {
      if ((double) this.Projectile.ai[0] == 0.0)
      {
        this.Projectile.rotation = (float) Main.rand.Next(100);
        this.RotDirect = Utils.NextBool(Main.rand, 2) ? -1 : 1;
        this.rGem = Main.rand.Next(1, 9);
      }
      this.Projectile.rotation += 0.2f * (float) this.RotDirect;
      if (Utils.NextBool(Main.rand, 6))
      {
        int index = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 70, ((Entity) this.Projectile).velocity.X * 0.2f, ((Entity) this.Projectile).velocity.Y * 0.2f, 100, new Color(), 2.5f);
        Main.dust[index].noGravity = true;
        Main.dust[index].velocity.X *= 0.5f;
        Main.dust[index].velocity.Y *= 0.5f;
      }
      if (this.Projectile.alpha > 0)
        this.Projectile.alpha -= 4;
      if ((double) this.Projectile.ai[0] > 30.0)
      {
        if (this.First)
        {
          this.lifelight = Main.npc[(int) this.Projectile.ai[1]];
          this.Projectile.ai[1] = 0.0f;
          this.First = false;
        }
        Player player = Main.player[this.lifelight.target];
        Vector2 vector2 = ((Entity) this.Projectile).Center;
        float num1 = 8f;
        float num2 = 5f;
        if ((double) this.Projectile.ai[1] <= 90.0)
        {
          vector2 = Vector2.op_Subtraction(((Entity) this.lifelight).Center, ((Entity) this.Projectile).Center);
          num1 = 8f;
        }
        if ((double) this.Projectile.ai[1] > 90.0)
        {
          vector2 = Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.Projectile).Center);
          num1 = 12f;
          this.homingonPlayer = true;
          this.home = false;
        }
        double num3 = (double) ((Vector2) ref vector2).Length();
        if (num3 < 200.0 && this.homingonPlayer)
          this.home = false;
        if (num3 < 20.0)
          ++this.Projectile.ai[1];
        if (num3 > 20.0 && this.home)
        {
          ((Vector2) ref vector2).Normalize();
          vector2 = Vector2.op_Multiply(vector2, num1);
          ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, num2 - 1f), vector2), num2);
        }
        else if (Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        {
          ((Entity) this.Projectile).velocity.X = -0.15f;
          ((Entity) this.Projectile).velocity.Y = -0.05f;
        }
        if (!this.home && this.homingonPlayer && !this.chosenDirection)
        {
          if (!WorldSavingSystem.EternityMode)
            this.Projectile.Kill();
          double radians = (double) MathHelper.ToRadians((float) Main.rand.Next(-100, 100));
          ((Vector2) ref vector2).Normalize();
          vector2 = Vector2.op_Multiply(Utils.RotatedBy(vector2, radians, new Vector2()), num1);
          ((Entity) this.Projectile).velocity = vector2;
          this.chosenDirection = true;
        }
      }
      if ((double) this.Projectile.ai[0] > 600.0)
        this.Projectile.Kill();
      ++this.Projectile.ai[0];
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<SmiteBuff>(), 180, true, false);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(61, 1);
      interpolatedStringHandler.AppendLiteral("FargowiltasSouls/Assets/ExtraTextures/LifelightParts/ShardGem");
      interpolatedStringHandler.AppendFormatted<int>(this.rGem);
      Texture2D texture2D = ModContent.Request<Texture2D>(interpolatedStringHandler.ToStringAndClear(), (AssetRequestMode) 1).Value;
      int num1 = texture2D.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.Purple, this.Projectile.Opacity), 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
