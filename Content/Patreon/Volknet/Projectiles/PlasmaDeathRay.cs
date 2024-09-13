// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Volknet.Projectiles.PlasmaDeathRay
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Volknet.Projectiles
{
  public class PlasmaDeathRay : ModProjectile
  {
    public float LaserWidth = 20f;
    public float LaserHeight = 30f;
    public float LaserLen = 5000f;
    public int LaserTime = 30;

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 2;
      ((Entity) this.Projectile).height = 2;
      this.Projectile.scale = 1f;
      this.Projectile.friendly = true;
      this.Projectile.timeLeft = this.LaserTime;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.penetrate = -1;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 0;
      this.Projectile.DamageType = DamageClass.Magic;
      this.Projectile.hide = true;
      this.Projectile.light = 1.1f;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
    }

    public virtual void DrawBehind(
      int index,
      List<int> behindNPCsAndTiles,
      List<int> behindNPCs,
      List<int> behindProjectiles,
      List<int> overPlayers,
      List<int> overWiresUI)
    {
      behindProjectiles.Add(index);
    }

    public virtual void AI()
    {
      this.CastLights();
      if (!Main.dedServ)
        FargoSoulsUtil.ScreenshakeRumble(6f);
      if ((double) this.Projectile.ai[1] < (double) this.LaserWidth / 2.0)
        ++this.Projectile.ai[1];
      if ((double) this.Projectile.timeLeft < (double) this.LaserWidth)
        this.Projectile.ai[1] = (float) this.Projectile.timeLeft / 2f;
      if (((Entity) Main.player[this.Projectile.owner]).active)
      {
        Player player = Main.player[this.Projectile.owner];
        if (!player.dead && player.HeldItem.type == ModContent.ItemType<NanoCore>())
        {
          if (player.GetModPlayer<NanoPlayer>().NanoCoreMode == 2 && player.channel)
          {
            ((Entity) this.Projectile).Center = ((Entity) player).Center;
            this.Projectile.rotation = Utils.ToRotation(Vector2.op_Subtraction(Main.MouseWorld, ((Entity) player).Center));
            for (int index = 0; index < 9; ++index)
            {
              Dust dust = Dust.NewDustDirect(((Entity) player).Center, 0, 0, 157, 0.0f, 0.0f, 0, new Color(), 1.5f);
              dust.position = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), (float) (50 + Main.rand.Next(80))));
              dust.velocity = Vector2.op_Multiply(Vector2.op_Multiply(Utils.ToRotationVector2(Utils.NextFloat(Main.rand) * 6.28318548f), Utils.NextFloat(Main.rand)), 3f);
              dust.fadeIn = 0.9f;
              dust.noGravity = true;
              dust.customData = (object) player;
            }
          }
          else
            this.Projectile.Kill();
        }
        else
          this.Projectile.Kill();
      }
      else
        this.Projectile.Kill();
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 150), 0.8f));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector((int) ((double) this.LaserWidth / 2.0 - (double) this.Projectile.ai[1]), 0, (int) ((double) this.Projectile.ai[1] * 2.0), (int) this.LaserHeight);
      float laserLen = this.LaserLen;
      float laserHeight = this.LaserHeight;
      Vector2 rotationVector2 = Utils.ToRotationVector2(this.Projectile.rotation);
      float rotation = this.Projectile.rotation;
      for (float num = 0.0f; (double) num <= (double) laserLen; num += laserHeight)
        Main.EntitySpriteDraw(TextureAssets.Projectile[this.Projectile.type].Value, Vector2.op_Subtraction(Vector2.op_Addition(Vector2.op_Addition(new Vector2(0.0f, this.Projectile.gfxOffY), ((Entity) this.Projectile).Center), Vector2.op_Multiply(rotationVector2, num + 45f)), Main.screenPosition), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), rotation - 1.57079637f, Vector2.op_Multiply(Utils.Size(rectangle), 0.5f), 1f, (SpriteEffects) 0, 0.0f);
      return false;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(195, 1800, false);
      target.AddBuff(44, 1800, false);
      target.AddBuff(39, 1800, false);
      target.AddBuff(69, 1800, false);
      target.AddBuff(153, 1800, false);
      for (int index1 = 0; index1 < 6; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) target).position, ((Entity) target).width, ((Entity) target).height, 157, 0.0f, 0.0f, 100, new Color(), 4f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].noLight = true;
        Main.dust[index2].velocity = Vector2.op_Addition(Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) target).Center), 9f), Utils.NextVector2Circular(Main.rand, 12f, 12f));
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 2f);
      }
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      Vector2 rotationVector2 = Utils.ToRotationVector2(this.Projectile.rotation);
      float num = 0.0f;
      return new bool?(Collision.CheckAABBvLineCollision(Utils.TopLeft(targetHitbox), Utils.Size(targetHitbox), ((Entity) this.Projectile).Center, Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(rotationVector2, this.LaserLen)), this.LaserWidth, ref num));
    }

    public virtual bool? CanCutTiles() => new bool?(true);

    public virtual void CutTiles()
    {
      DelegateMethods.tilecut_0 = (TileCuttingContext) 2;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      Utils.PlotTileLine(((Entity) this.Projectile).Center, Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), this.LaserLen)), (this.LaserWidth + 16f) * this.Projectile.scale, PlasmaDeathRay.\u003C\u003EO.\u003C0\u003E__CutTiles ?? (PlasmaDeathRay.\u003C\u003EO.\u003C0\u003E__CutTiles = new Utils.TileActionAttempt((object) null, __methodptr(CutTiles))));
    }

    private void CastLights()
    {
      DelegateMethods.v3_1 = new Vector3(0.6f, 1f, 0.6f);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      Utils.PlotTileLine(((Entity) this.Projectile).Center, Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), this.LaserLen)), this.LaserWidth, PlasmaDeathRay.\u003C\u003EO.\u003C1\u003E__CastLight ?? (PlasmaDeathRay.\u003C\u003EO.\u003C1\u003E__CastLight = new Utils.TileActionAttempt((object) null, __methodptr(CastLight))));
    }
  }
}
