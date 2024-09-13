// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.CursedCoffin.CoffinHand
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.CursedCoffin
{
  public class CoffinHand : ModProjectile
  {
    private int CaughtPlayer = -1;
    private static readonly Color GlowColor = new Color(224, 196, 252, 0);

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Type] = 10;
      ProjectileID.Sets.TrailingMode[this.Type] = 2;
      Main.projFrames[this.Type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 76;
      ((Entity) this.Projectile).height = 76;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.scale = 1f;
      this.Projectile.light = 1f;
      this.Projectile.timeLeft = 360;
      this.Projectile.Opacity = 0.2f;
    }

    public ref float Timer => ref this.Projectile.localAI[0];

    public ref float State => ref this.Projectile.ai[1];

    public ref float RotDir => ref this.Projectile.ai[2];

    public virtual void SendExtraAI(BinaryWriter writer) => writer.Write(this.Timer);

    public virtual void ReceiveExtraAI(BinaryReader reader) => this.Timer = reader.ReadSingle();

    public virtual void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
    {
      if (target.HasBuff<GrabbedBuff>())
        return;
      target.buffImmune[ModContent.BuffType<CoffinTossBuff>()] = true;
      this.Projectile.frame = 1;
      this.Timer = 0.0f;
      this.State = 100f;
      ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Vector2.op_UnaryNegation(Vector2.UnitY), 5f);
      this.Projectile.damage = 0;
      this.CaughtPlayer = ((Entity) target).whoAmI;
      modifiers.Null();
    }

    public virtual bool CanHitPlayer(Player target)
    {
      return (double) this.State != 100.0 && (double) this.State != 101.0 && (double) this.State != 1.0 && base.CanHitPlayer(target);
    }

    public virtual void OnKill(int timeLeft)
    {
      if (!this.CaughtPlayer.IsWithinBounds((int) byte.MaxValue))
        return;
      Player player = Main.player[this.CaughtPlayer];
      if (!player.Alive())
        return;
      player.fullRotation = 0.0f;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.scale = 0.2f;
        this.Projectile.localAI[0] = 1f;
      }
      if ((double) this.State != 1.0 && (double) this.State != 2.0)
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
      this.Projectile.Opacity = (float) Utils.Lerp((double) this.Projectile.Opacity, 1.0, 0.10000000149011612);
      this.Projectile.scale = (float) Utils.Lerp((double) this.Projectile.scale, 1.0, 0.10000000149011612);
      NPC npc = Main.npc[(int) this.Projectile.ai[0]];
      if (!npc.TypeAlive<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin>())
      {
        this.Projectile.Kill();
      }
      else
      {
        FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin cursedCoffin = Luminance.Common.Utilities.Utilities.As<FargowiltasSouls.Content.Bosses.CursedCoffin.CursedCoffin>(npc);
        Player player1 = Main.player[npc.target];
        if (!player1.Alive())
          return;
        float num1 = this.State;
        if ((double) num1 != 1.0)
        {
          if ((double) num1 != 2.0)
          {
            if ((double) num1 != 22.0)
            {
              if ((double) num1 != 100.0)
              {
                if ((double) num1 != 101.0)
                  return;
                ((Entity) this.Projectile).velocity = Vector2.Zero;
                this.Projectile.scale -= 0.05f;
                this.Projectile.Opacity -= 0.1f;
                if ((double) this.Projectile.Opacity >= 0.10000000149011612)
                  return;
                this.Projectile.Kill();
              }
              else if (!this.CaughtPlayer.IsWithinBounds((int) byte.MaxValue))
              {
                this.State = 101f;
              }
              else
              {
                Player player2 = Main.player[this.CaughtPlayer];
                if (!player2.Alive())
                {
                  this.State = 101f;
                }
                else
                {
                  player2.buffImmune[ModContent.BuffType<StunnedBuff>()] = true;
                  if ((double) this.Timer >= 60.0)
                  {
                    player2.AddBuff(ModContent.BuffType<CoffinTossBuff>(), 100, true, false);
                    ((Entity) player2).velocity = Vector2.op_Multiply(((Entity) this.Projectile).DirectionFrom(((Entity) npc).Center), 30f);
                    cursedCoffin.MashTimer = 15;
                    npc.netUpdate = true;
                    this.State = 101f;
                  }
                  else
                  {
                    int mashTimer = cursedCoffin.MashTimer;
                    if (WorldSavingSystem.MasochistModeReal)
                      mashTimer += 666;
                    if (player2.Alive() && ((double) ((Entity) this.Projectile).Distance(((Entity) player2).Center) < 160.0 || ((Entity) player2).whoAmI != Main.myPlayer) && player2.FargoSouls().MashCounter < mashTimer)
                    {
                      player2.AddBuff(ModContent.BuffType<GrabbedBuff>(), 2, true, false);
                      Projectile projectile = this.Projectile;
                      ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 0.96f);
                      ((Entity) player2).Center = ((Entity) this.Projectile).Center;
                      player2.fullRotation = Utils.ToRotation(((Entity) this.Projectile).DirectionFrom(((Entity) npc).Center)) + 1.57079637f;
                      player2.fullRotationOrigin = Vector2.op_Subtraction(((Entity) player2).Center, ((Entity) player2).position);
                    }
                    else
                    {
                      this.CaughtPlayer = -1;
                      this.State = 101f;
                      player2.fullRotation = 0.0f;
                      this.Projectile.netUpdate = true;
                      cursedCoffin.MashTimer += 7;
                      npc.netUpdate = true;
                      if (Main.netMode == 2)
                        NetMessage.SendData(27, -1, -1, (NetworkText) null, ((Entity) this.Projectile).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                    }
                    ++this.Timer;
                  }
                }
              }
            }
            else
            {
              Vector2 vector2 = Vector2.op_Subtraction(((Entity) player1).Center, ((Entity) this.Projectile).Center);
              float num2 = 28f;
              float num3 = 10f;
              ((Vector2) ref vector2).Normalize();
              vector2 = Vector2.op_Multiply(vector2, num2);
              ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, num3 - 1f), vector2), num3);
              if (!Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
                return;
              ((Entity) this.Projectile).velocity.X = -0.15f;
              ((Entity) this.Projectile).velocity.Y = -0.05f;
            }
          }
          else
          {
            float num4 = (this.Timer - 25f) / (WorldSavingSystem.MasochistModeReal ? 2f : 3f);
            if ((double) num4 > 24.0)
              num4 = 24f;
            ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation - 1.57079637f), num4);
            if ((double) this.Timer > 120.0)
              this.Projectile.Kill();
            ++this.Timer;
          }
        }
        else
        {
          Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) player1, ((Entity) this.Projectile).Center), (double) this.RotDir * 0.031415928155183792, new Vector2()), 350f);
          this.Movement(Vector2.op_Addition(((Entity) player1).Center, vector2), 0.2f, 30f, decel: 0.2f, slowdown: 15f);
          this.Projectile.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) player1).Center)) + 1.57079637f;
        }
      }
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
      Vector2 vector2_2 = Vector2.op_Division(Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), (float) (texture2D.Width - ((Entity) this.Projectile).width)), 2f);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(this.Projectile.GetAlpha(CoffinHand.GlowColor), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(Vector2.op_Addition(oldPo, vector2_2), Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_2), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }

    private void Movement(
      Vector2 pos,
      float accel = 0.03f,
      float maxSpeed = 20f,
      float lowspeed = 5f,
      float decel = 0.03f,
      float slowdown = 30f)
    {
      if ((double) ((Entity) this.Projectile).Distance(pos) > (double) slowdown)
        ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.op_Multiply(Utils.SafeNormalize(Vector2.op_Subtraction(pos, ((Entity) this.Projectile).Center), Vector2.Zero), maxSpeed), accel);
      else
        ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.op_Multiply(Utils.SafeNormalize(Vector2.op_Subtraction(pos, ((Entity) this.Projectile).Center), Vector2.Zero), lowspeed), decel);
    }
  }
}
