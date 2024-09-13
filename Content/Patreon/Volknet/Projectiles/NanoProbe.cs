// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Volknet.Projectiles.NanoProbe
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Volknet.Projectiles
{
  internal class NanoProbe : ModProjectile
  {
    public bool OldChannel;

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 20;
      ((Entity) this.Projectile).height = 20;
      this.Projectile.scale = 0.7f;
      this.Projectile.friendly = true;
      this.Projectile.hostile = false;
      this.Projectile.timeLeft = 2;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.penetrate = -1;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
      this.Projectile.FargoSouls().CanSplit = false;
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D1 = TextureAssets.Projectile[this.Projectile.type].Value;
      Texture2D texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Patreon/Volknet/Projectiles/NanoProbeGlow", (AssetRequestMode) 1).Value;
      Color alpha = this.Projectile.GetAlpha(lightColor);
      for (float index1 = 0.0f; (double) index1 < (double) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index1 += 0.2f)
      {
        Color color = Color.op_Multiply(Color.LimeGreen, this.Projectile.Opacity);
        if ((double) index1 > 1.0)
          color = Color.op_Multiply(color, 0.3f);
        ((Color) ref color).A = (byte) 0;
        float num1 = ((float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index1) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type];
        if ((double) index1 > 1.0)
          color = Color.op_Multiply(color, num1 * num1);
        int index2 = (int) index1 - 1;
        if (index2 >= 0)
        {
          float num2 = this.Projectile.oldRot[index2];
          Vector2 vector2 = Vector2.op_Addition(Vector2.Lerp(this.Projectile.oldPos[(int) index1], this.Projectile.oldPos[index2], (float) (1.0 - (double) index1 % 1.0)), Vector2.op_Division(((Entity) this.Projectile).Size, 2f));
          Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(vector2, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(), color, num2, Vector2.op_Division(Utils.Size(texture2D1), 2f), this.Projectile.scale * Utils.NextFloat(Main.rand, 1f, 1.6f), (SpriteEffects) 0, 0.0f);
        }
      }
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(), alpha, this.Projectile.rotation, Vector2.op_Division(Utils.Size(texture2D1), 2f), this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(), Color.White, this.Projectile.rotation, Vector2.op_Division(Utils.Size(texture2D1), 2f), this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      if (((Entity) Main.player[this.Projectile.owner]).active)
      {
        Player player = Main.player[this.Projectile.owner];
        if (!player.dead && player.HeldItem.type == ModContent.ItemType<NanoCore>())
        {
          this.Projectile.timeLeft = 2;
          if ((double) ((Entity) this.Projectile).Distance(((Entity) player).Center) > 1200.0)
            ((Entity) this.Projectile).Center = ((Entity) player).Center;
          if (player.GetModPlayer<NanoPlayer>().NanoCoreMode == 0)
          {
            Vector2 vector2 = Vector2.op_Subtraction(Main.MouseWorld, ((Entity) player).Center);
            if (Vector2.op_Equality(vector2, Vector2.Zero))
            {
              // ISSUE: explicit constructor call
              ((Vector2) ref vector2).\u002Ector(0.0f, -1f);
            }
            ((Vector2) ref vector2).Normalize();
            if ((double) this.Projectile.ai[0] < 5.0)
            {
              Vector2 targetPos = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(Vector2.op_Multiply(vector2, 30f), this.Projectile.ai[0] + 1f));
              if ((double) this.Projectile.ai[1] == 0.0)
              {
                this.Movement(targetPos, 0.5f);
                this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
                if ((double) ((Entity) this.Projectile).Distance(targetPos) >= 30.0)
                  return;
                this.Projectile.ai[1] = 1f;
              }
              else
              {
                ((Entity) this.Projectile).velocity = Vector2.Zero;
                ((Entity) this.Projectile).Center = targetPos;
                this.Projectile.rotation = Utils.ToRotation(vector2);
              }
            }
            else
            {
              int num = Math.Sign((double) this.Projectile.ai[0] - 5.5);
              Vector2 targetPos = Vector2.op_Addition(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(Utils.ToRotationVector2(Utils.ToRotation(vector2) + 1.57079637f * (float) num), 20f)), Vector2.op_Multiply(vector2, 30f));
              if ((double) this.Projectile.ai[1] == 0.0)
              {
                this.Movement(targetPos, 0.5f);
                this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
                if ((double) ((Entity) this.Projectile).Distance(targetPos) >= 30.0)
                  return;
                this.Projectile.ai[1] = 1f;
              }
              else
              {
                ((Entity) this.Projectile).velocity = Vector2.Zero;
                ((Entity) this.Projectile).Center = targetPos;
                this.Projectile.rotation = Utils.ToRotation(vector2) + 1.57079637f * (float) num;
              }
            }
          }
          else if (player.GetModPlayer<NanoPlayer>().NanoCoreMode == 1)
          {
            Vector2 vector2 = Vector2.op_Subtraction(Main.MouseWorld, ((Entity) player).Center);
            if (Vector2.op_Equality(vector2, Vector2.Zero))
            {
              // ISSUE: explicit constructor call
              ((Vector2) ref vector2).\u002Ector(0.0f, -1f);
            }
            ((Vector2) ref vector2).Normalize();
            Vector2 targetPos = Vector2.Zero;
            float num1 = 0.0f;
            float num2 = this.Projectile.ai[0];
            if ((double) num2 != 0.0)
            {
              if ((double) num2 != 1.0)
              {
                if ((double) num2 != 2.0)
                {
                  if ((double) num2 != 3.0)
                  {
                    if ((double) num2 != 4.0)
                    {
                      if ((double) num2 != 5.0)
                      {
                        if ((double) num2 == 6.0)
                        {
                          targetPos = Vector2.op_Addition(Vector2.op_Addition(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(vector2, 40f)), Vector2.op_Multiply(Utils.ToRotationVector2(Utils.ToRotation(vector2) - 1.57079637f), 30f)), Vector2.op_Multiply(Utils.ToRotationVector2(Utils.ToRotation(vector2) - 2.3561945f), 60f));
                          num1 = Utils.ToRotation(vector2) - 2.3561945f;
                        }
                      }
                      else
                      {
                        targetPos = Vector2.op_Addition(Vector2.op_Addition(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(vector2, 40f)), Vector2.op_Multiply(Utils.ToRotationVector2(Utils.ToRotation(vector2) + 1.57079637f), 30f)), Vector2.op_Multiply(Utils.ToRotationVector2(Utils.ToRotation(vector2) + 2.3561945f), 60f));
                        num1 = Utils.ToRotation(vector2) + 2.3561945f;
                      }
                    }
                    else
                    {
                      targetPos = Vector2.op_Addition(Vector2.op_Addition(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(vector2, 40f)), Vector2.op_Multiply(Utils.ToRotationVector2(Utils.ToRotation(vector2) - 1.57079637f), 30f)), Vector2.op_Multiply(Utils.ToRotationVector2(Utils.ToRotation(vector2) - 2.3561945f), 30f));
                      num1 = Utils.ToRotation(vector2) - 2.3561945f;
                    }
                  }
                  else
                  {
                    targetPos = Vector2.op_Addition(Vector2.op_Addition(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(vector2, 40f)), Vector2.op_Multiply(Utils.ToRotationVector2(Utils.ToRotation(vector2) + 1.57079637f), 30f)), Vector2.op_Multiply(Utils.ToRotationVector2(Utils.ToRotation(vector2) + 2.3561945f), 30f));
                    num1 = Utils.ToRotation(vector2) + 2.3561945f;
                  }
                }
                else
                {
                  targetPos = Vector2.op_Addition(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(vector2, 40f)), Vector2.op_Multiply(Utils.ToRotationVector2(Utils.ToRotation(vector2) - 1.57079637f), 30f));
                  num1 = Utils.ToRotation(vector2) - 1.57079637f;
                }
              }
              else
              {
                targetPos = Vector2.op_Addition(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(vector2, 40f)), Vector2.op_Multiply(Utils.ToRotationVector2(Utils.ToRotation(vector2) + 1.57079637f), 30f));
                num1 = Utils.ToRotation(vector2) + 1.57079637f;
              }
            }
            else
            {
              targetPos = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(vector2, 40f));
              num1 = Utils.ToRotation(vector2);
            }
            if ((double) this.Projectile.ai[1] == 0.0)
            {
              this.Movement(targetPos, 0.5f);
              this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
              if ((double) ((Entity) this.Projectile).Distance(targetPos) >= 30.0)
                return;
              this.Projectile.ai[1] = 1f;
            }
            else
            {
              ((Entity) this.Projectile).velocity = Vector2.Zero;
              ((Entity) this.Projectile).Center = targetPos;
              this.Projectile.rotation = num1;
            }
          }
          else if (player.GetModPlayer<NanoPlayer>().NanoCoreMode == 2)
          {
            Vector2 vector2 = Vector2.op_Subtraction(Main.MouseWorld, ((Entity) player).Center);
            if (Vector2.op_Equality(vector2, Vector2.Zero))
            {
              // ISSUE: explicit constructor call
              ((Vector2) ref vector2).\u002Ector(0.0f, -1f);
            }
            ((Vector2) ref vector2).Normalize();
            Vector2 targetPos = Vector2.Zero;
            float num3 = 0.0f;
            float num4 = this.Projectile.ai[0];
            if ((double) num4 != 0.0)
            {
              if ((double) num4 != 1.0)
              {
                if ((double) num4 != 2.0)
                {
                  if ((double) num4 != 3.0)
                  {
                    if ((double) num4 != 4.0)
                    {
                      if ((double) num4 != 5.0)
                      {
                        if ((double) num4 == 6.0)
                        {
                          targetPos = Vector2.op_Addition(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(vector2, 115f)), Vector2.op_Multiply(Utils.ToRotationVector2(Utils.ToRotation(vector2) - 1.57079637f), 15f));
                          num3 = Utils.ToRotation(vector2);
                        }
                      }
                      else
                      {
                        targetPos = Vector2.op_Addition(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(vector2, 115f)), Vector2.op_Multiply(Utils.ToRotationVector2(Utils.ToRotation(vector2) + 1.57079637f), 15f));
                        num3 = Utils.ToRotation(vector2);
                      }
                    }
                    else
                    {
                      targetPos = Vector2.op_Addition(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(vector2, 85f)), Vector2.op_Multiply(Utils.ToRotationVector2(Utils.ToRotation(vector2) - 1.57079637f), 15f));
                      num3 = Utils.ToRotation(vector2);
                    }
                  }
                  else
                  {
                    targetPos = Vector2.op_Addition(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(vector2, 85f)), Vector2.op_Multiply(Utils.ToRotationVector2(Utils.ToRotation(vector2) + 1.57079637f), 15f));
                    num3 = Utils.ToRotation(vector2);
                  }
                }
                else
                {
                  targetPos = Vector2.op_Addition(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(vector2, 55f)), Vector2.op_Multiply(Utils.ToRotationVector2(Utils.ToRotation(vector2) - 1.57079637f), 15f));
                  num3 = Utils.ToRotation(vector2);
                }
              }
              else
              {
                targetPos = Vector2.op_Addition(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(vector2, 55f)), Vector2.op_Multiply(Utils.ToRotationVector2(Utils.ToRotation(vector2) + 1.57079637f), 15f));
                num3 = Utils.ToRotation(vector2);
              }
            }
            else
            {
              targetPos = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(vector2, 40f));
              num3 = Utils.ToRotation(vector2);
            }
            if ((double) this.Projectile.ai[1] == 0.0)
            {
              this.Movement(targetPos, 0.5f);
              this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
              if ((double) ((Entity) this.Projectile).Distance(targetPos) >= 30.0)
                return;
              this.Projectile.ai[1] = 1f;
            }
            else
            {
              ((Entity) this.Projectile).velocity = Vector2.Zero;
              ((Entity) this.Projectile).Center = targetPos;
              this.Projectile.rotation = num3;
            }
          }
          else
          {
            if (player.GetModPlayer<NanoPlayer>().NanoCoreMode != 3)
              return;
            Vector2 vector2 = Vector2.op_Subtraction(Main.MouseWorld, ((Entity) player).Center);
            if (Vector2.op_Equality(vector2, Vector2.Zero))
            {
              // ISSUE: explicit constructor call
              ((Vector2) ref vector2).\u002Ector(0.0f, -1f);
            }
            ((Vector2) ref vector2).Normalize();
            Vector2 targetPos = Vector2.Zero;
            float rotation = Utils.ToRotation(Vector2.Normalize(Vector2.op_Subtraction(Main.MouseWorld, ((Entity) this.Projectile).Center)));
            if (!player.channel)
            {
              targetPos = ((Entity) player).direction > 0 ? Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(Utils.ToRotationVector2((float) (1.5707963705062866 + 0.39269909262657166 * ((double) this.Projectile.ai[0] + 1.0))), 60f)) : Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(Utils.ToRotationVector2((float) (0.39269909262657166 * ((double) this.Projectile.ai[0] + 1.0) - 1.5707963705062866)), 60f));
              rotation = Utils.ToRotation(Vector2.op_Subtraction(((Entity) this.Projectile).Center, ((Entity) player).Center));
            }
            else
            {
              float num = this.Projectile.ai[0];
              if ((double) num != 0.0)
              {
                if ((double) num != 1.0)
                {
                  if ((double) num != 2.0)
                  {
                    if ((double) num != 3.0)
                    {
                      if ((double) num != 4.0)
                      {
                        if ((double) num != 5.0)
                        {
                          if ((double) num == 6.0)
                            targetPos = Vector2.op_Addition(((Entity) player).Center, new Vector2((float) (-30 * ((Entity) player).direction), -40f));
                        }
                        else
                          targetPos = Vector2.op_Addition(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(vector2, 450f)), Vector2.op_Multiply(Utils.ToRotationVector2(Utils.ToRotation(vector2) - 1.57079637f), 200f));
                      }
                      else
                        targetPos = Vector2.op_Addition(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(vector2, 450f)), Vector2.op_Multiply(Utils.ToRotationVector2(Utils.ToRotation(vector2) + 1.57079637f), 200f));
                    }
                    else
                      targetPos = Vector2.op_Addition(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(vector2, 200f)), Vector2.op_Multiply(Utils.ToRotationVector2(Utils.ToRotation(vector2) - 1.57079637f), 250f));
                  }
                  else
                    targetPos = Vector2.op_Addition(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(vector2, 200f)), Vector2.op_Multiply(Utils.ToRotationVector2(Utils.ToRotation(vector2) + 1.57079637f), 250f));
                }
                else
                  targetPos = Vector2.op_Addition(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(vector2, -50f)), Vector2.op_Multiply(Utils.ToRotationVector2(Utils.ToRotation(vector2) - 1.57079637f), 300f));
              }
              else
                targetPos = Vector2.op_Addition(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(vector2, -50f)), Vector2.op_Multiply(Utils.ToRotationVector2(Utils.ToRotation(vector2) + 1.57079637f), 300f));
            }
            if (this.OldChannel && !player.channel)
            {
              this.Projectile.ai[1] = 0.0f;
              this.OldChannel = false;
            }
            if (!this.OldChannel && player.channel)
            {
              this.Projectile.ai[1] = 0.0f;
              this.OldChannel = true;
            }
            if ((double) this.Projectile.ai[1] == 0.0)
            {
              this.Movement(targetPos, 0.5f);
              this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
              if ((double) ((Entity) this.Projectile).Distance(targetPos) >= 30.0)
                return;
              this.Projectile.ai[1] = 1f;
            }
            else
            {
              ((Entity) this.Projectile).velocity = Vector2.Zero;
              ((Entity) this.Projectile).Center = targetPos;
              this.Projectile.rotation = rotation;
            }
          }
        }
        else
          this.Projectile.Kill();
      }
      else
        this.Projectile.Kill();
    }

    public void Movement(Vector2 targetPos, float speedModifier, float Limit = 24f)
    {
      Projectile projectile = this.Projectile;
      ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, Vector2.op_Multiply(Vector2.op_Subtraction(((Entity) Main.player[this.Projectile.owner]).position, ((Entity) Main.player[this.Projectile.owner]).oldPosition), 0.8f));
      if ((double) ((Entity) this.Projectile).Center.X < (double) targetPos.X)
      {
        ((Entity) this.Projectile).velocity.X += speedModifier;
        if ((double) ((Entity) this.Projectile).velocity.X < 0.0)
          ((Entity) this.Projectile).velocity.X += speedModifier * 2f;
      }
      else
      {
        ((Entity) this.Projectile).velocity.X -= speedModifier;
        if ((double) ((Entity) this.Projectile).velocity.X > 0.0)
          ((Entity) this.Projectile).velocity.X -= speedModifier * 2f;
      }
      if ((double) ((Entity) this.Projectile).Center.Y < (double) targetPos.Y)
      {
        ((Entity) this.Projectile).velocity.Y += speedModifier;
        if ((double) ((Entity) this.Projectile).velocity.Y < 0.0)
          ((Entity) this.Projectile).velocity.Y += speedModifier * 2f;
      }
      else
      {
        ((Entity) this.Projectile).velocity.Y -= speedModifier;
        if ((double) ((Entity) this.Projectile).velocity.Y > 0.0)
          ((Entity) this.Projectile).velocity.Y -= speedModifier * 2f;
      }
      if ((double) Math.Abs(((Entity) this.Projectile).velocity.X) > (double) Limit)
        ((Entity) this.Projectile).velocity.X = Limit * (float) Math.Sign(((Entity) this.Projectile).velocity.X);
      if ((double) Math.Abs(((Entity) this.Projectile).velocity.Y) <= (double) Limit)
        return;
      ((Entity) this.Projectile).velocity.Y = Limit * (float) Math.Sign(((Entity) this.Projectile).velocity.Y);
    }
  }
}
