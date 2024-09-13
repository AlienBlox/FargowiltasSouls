// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BaseArena
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles
{
  public abstract class BaseArena : ModProjectile
  {
    protected float rotationPerTick;
    protected readonly int npcType;
    protected readonly int dustType;
    protected readonly int increment;
    protected readonly int visualCount;
    protected float threshold;
    protected float targetPlayer;
    private float speed = 17f;

    protected BaseArena(
      float rotationPerTick,
      float threshold,
      int npcType,
      int dustType = 135,
      int increment = 2,
      int visualCount = 32)
    {
      this.rotationPerTick = rotationPerTick;
      this.threshold = threshold;
      this.npcType = npcType;
      this.dustType = dustType;
      this.increment = increment;
      this.visualCount = visualCount;
    }

    public virtual void SetStaticDefaults()
    {
      ((ModType) this).SetStaticDefaults();
      ProjectileID.Sets.DrawScreenCheckFluff[this.Projectile.type] = 2400;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 60;
      ((Entity) this.Projectile).height = 60;
      this.Projectile.hostile = true;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 600;
      this.Projectile.netImportant = true;
      this.CooldownSlot = 0;
      this.Projectile.FargoSouls().GrazeCheck = (Func<Projectile, bool>) (projectile =>
      {
        bool? nullable = base.CanDamage();
        bool flag = true;
        if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue) || (double) this.targetPlayer != (double) Main.myPlayer)
          return false;
        Vector2 vector2 = Vector2.op_Subtraction(((Entity) Main.LocalPlayer).Center, ((Entity) this.Projectile).Center);
        return (double) Math.Abs(((Vector2) ref vector2).Length() - this.threshold) < (double) (((Entity) this.Projectile).width / 2) * (double) this.Projectile.scale + 42.0 + (double) Main.LocalPlayer.FargoSouls().GrazeRadius;
      });
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 4;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
      this.Projectile.hide = true;
      this.Projectile.FargoSouls().DeletionImmuneRank = 3;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
    }

    public virtual bool? CanDamage() => new bool?(this.Projectile.alpha == 0);

    public virtual bool CanHitPlayer(Player target)
    {
      return (double) this.targetPlayer == (double) ((Entity) target).whoAmI && target.hurtCooldowns[this.CooldownSlot] == 0;
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      Vector2 vector2 = Vector2.op_Subtraction(Utils.ToVector2(((Rectangle) ref targetHitbox).Center), Utils.ToVector2(((Rectangle) ref projHitbox).Center));
      return new bool?((double) Math.Abs(((Vector2) ref vector2).Length() - this.threshold) < (double) (((Entity) this.Projectile).width / 2) * (double) this.Projectile.scale);
    }

    protected virtual void Movement(NPC npc)
    {
    }

    public virtual void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], this.npcType);
      if (npc != null)
      {
        this.Projectile.alpha -= this.increment;
        if (this.Projectile.alpha < 0)
          this.Projectile.alpha = 0;
        this.Movement(npc);
        this.targetPlayer = (float) npc.target;
        Player localPlayer = Main.LocalPlayer;
        if (((Entity) localPlayer).active && !localPlayer.dead && !localPlayer.ghost)
        {
          float num1 = ((Entity) localPlayer).Distance(((Entity) this.Projectile).Center);
          if ((double) num1 > (double) this.threshold && (double) num1 < (double) this.threshold * 4.0)
          {
            if ((double) num1 > (double) this.threshold * 2.0)
            {
              localPlayer.controlLeft = false;
              localPlayer.controlRight = false;
              localPlayer.controlUp = false;
              localPlayer.controlDown = false;
              localPlayer.controlUseItem = false;
              localPlayer.controlUseTile = false;
              localPlayer.controlJump = false;
              localPlayer.controlHook = false;
              if (localPlayer.grapCount > 0)
                localPlayer.RemoveAllGrapplingHooks();
              if (localPlayer.mount.Active)
                localPlayer.mount.Dismount(localPlayer);
              ((Entity) localPlayer).velocity.X = 0.0f;
              ((Entity) localPlayer).velocity.Y = -0.4f;
              localPlayer.FargoSouls().NoUsingItems = 2;
            }
            Vector2 vector2 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, ((Entity) localPlayer).Center);
            float num2 = ((Vector2) ref vector2).Length() - this.threshold;
            ((Vector2) ref vector2).Normalize();
            vector2 = Vector2.op_Multiply(vector2, (double) num2 < (double) this.speed ? num2 : this.speed);
            Player player = localPlayer;
            ((Entity) player).position = Vector2.op_Addition(((Entity) player).position, vector2);
            for (int index1 = 0; index1 < 20; ++index1)
            {
              int index2 = Dust.NewDust(((Entity) localPlayer).position, ((Entity) localPlayer).width, ((Entity) localPlayer).height, this.dustType, 0.0f, 0.0f, 0, new Color(), 2.5f);
              Main.dust[index2].noGravity = true;
              Dust dust = Main.dust[index2];
              dust.velocity = Vector2.op_Multiply(dust.velocity, 5f);
            }
            if ((double) this.speed <= 60.0)
              this.speed += 0.05f;
          }
          else
            this.speed = 17f;
        }
      }
      else
      {
        ((Entity) this.Projectile).velocity = Vector2.Zero;
        this.Projectile.alpha += this.increment;
        if (this.Projectile.alpha > (int) byte.MaxValue)
        {
          this.Projectile.Kill();
          return;
        }
      }
      this.Projectile.timeLeft = 2;
      this.Projectile.scale = (float) ((1.0 - (double) this.Projectile.alpha / (double) byte.MaxValue) * 2.0);
      this.Projectile.ai[0] += this.rotationPerTick;
      if ((double) this.Projectile.ai[0] > 3.1415927410125732)
      {
        this.Projectile.ai[0] -= 6.28318548f;
        this.Projectile.netUpdate = true;
      }
      else if ((double) this.Projectile.ai[0] < -3.1415927410125732)
      {
        this.Projectile.ai[0] += 6.28318548f;
        this.Projectile.netUpdate = true;
      }
      this.Projectile.localAI[0] = this.threshold;
    }

    public virtual void PostAI() => this.Projectile.hide = false;

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      ((Entity) target).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) target, ((Entity) this.Projectile).Center), 4f);
    }

    public virtual void OnKill(int timeLeft)
    {
      float num1 = (float) (((double) byte.MaxValue - (double) this.Projectile.alpha) / (double) byte.MaxValue);
      float num2 = this.threshold * num1;
      int num3 = (int) (300.0 * (double) num1);
      for (int index1 = 0; index1 < num3; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, this.dustType, 0.0f, 0.0f, 0, new Color(), 4f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 6f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(num2, Utils.RotatedByRandom(Vector2.UnitX, 2.0 * Math.PI)));
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.op_Multiply(Color.White, this.Projectile.Opacity), (double) this.targetPlayer == (double) Main.myPlayer ? 1f : 0.15f));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      Color alpha = this.Projectile.GetAlpha(lightColor);
      for (int index1 = 0; index1 < this.visualCount; ++index1)
      {
        int num2 = (this.Projectile.frame + index1) % Main.projFrames[this.Projectile.type];
        int num3 = num1 * num2;
        Rectangle rectangle;
        // ISSUE: explicit constructor call
        ((Rectangle) ref rectangle).\u002Ector(0, num3, texture2D.Width, num1);
        Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
        float num4 = 6.28318548f / (float) this.visualCount * (float) index1 + this.Projectile.ai[0];
        Vector2 vector2_2 = Utils.RotatedBy(Utils.RotatedBy(new Vector2((float) ((double) this.threshold * (double) this.Projectile.scale / 2.0), 0.0f), (double) this.Projectile.ai[0], new Vector2()), 6.2831854820251465 / (double) this.visualCount * (double) index1, new Vector2());
        for (int index2 = 0; index2 < 4; ++index2)
        {
          Color color = Color.op_Multiply(alpha, (float) (4 - index2) / 4f);
          Vector2 vector2_3 = Vector2.op_Addition(((Entity) this.Projectile).Center, Utils.RotatedBy(vector2_2, (double) this.rotationPerTick * (double) -index2, new Vector2()));
          float num5 = (float) ((double) num4 + (double) this.Projectile.rotation + 1.5707963705062866 * (double) index1);
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(vector2_3, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num5, vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
        }
        float num6 = (float) ((double) num4 + (double) this.Projectile.rotation + 1.5707963705062866 * (double) index1);
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_2), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, num6, vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      return false;
    }

    public virtual bool? CanCutTiles() => new bool?(false);
  }
}
