// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.EridanusMinion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class EridanusMinion : ModProjectile
  {
    public int drawTrailOffset;

    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Bosses/Champions/Cosmos/CosmosChampion";
    }

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 9;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 7;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 75;
      ((Entity) this.Projectile).height = 100;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.friendly = true;
      this.Projectile.minion = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.alpha = 0;
      this.Projectile.minionSlots = 0.0f;
      this.Projectile.penetrate = -1;
      this.Projectile.netImportant = true;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 10;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
    }

    public virtual void AI()
    {
      if (((Entity) Main.player[this.Projectile.owner]).active && !Main.player[this.Projectile.owner].dead && Main.player[this.Projectile.owner].FargoSouls().EridanusSet && (this.Projectile.owner != Main.myPlayer || Main.player[this.Projectile.owner].FargoSouls().EridanusEmpower))
      {
        this.Projectile.timeLeft = 2;
        Player player = Main.player[this.Projectile.owner];
        NPC minionAttackTargetNpc = this.Projectile.OwnerMinionAttackTargetNPC;
        if (minionAttackTargetNpc != null && (double) this.Projectile.ai[0] != (double) ((Entity) minionAttackTargetNpc).whoAmI && minionAttackTargetNpc.CanBeChasedBy((object) null, false))
        {
          this.Projectile.ai[0] = (float) ((Entity) minionAttackTargetNpc).whoAmI;
          this.Projectile.netUpdate = true;
        }
        if (++this.Projectile.frameCounter > 6)
        {
          this.Projectile.frameCounter = 0;
          ++this.Projectile.frame;
        }
        if (this.Projectile.frame > 4)
          this.Projectile.frame = 0;
        this.Projectile.rotation = 0.0f;
        if ((double) this.Projectile.ai[0] >= 0.0 && (double) this.Projectile.ai[0] < (double) Main.maxNPCs)
        {
          NPC npc = Main.npc[(int) this.Projectile.ai[0]];
          if (npc.CanBeChasedBy((object) null, false) && (double) ((Entity) player).Distance(((Entity) this.Projectile).Center) < 2500.0 && (double) ((Entity) this.Projectile).Distance(((Entity) npc).Center) < 2500.0)
          {
            ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = (double) ((Entity) this.Projectile).Center.X < (double) ((Entity) npc).Center.X ? 1 : -1;
            switch (player.FargoSouls().EridanusTimer / 600)
            {
              case 0:
                float num1 = ((Entity) player).Distance(((Entity) npc).Center) - 300f;
                if ((double) num1 > 300.0)
                  num1 = 300f;
                ((Entity) this.Projectile).Center = Vector2.Lerp(((Entity) this.Projectile).Center, Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) player, ((Entity) npc).Center), num1)), 0.15f);
                Projectile projectile1 = this.Projectile;
                ((Entity) projectile1).velocity = Vector2.op_Multiply(((Entity) projectile1).velocity, 0.8f);
                if ((double) ++this.Projectile.localAI[0] > 5.0)
                {
                  this.Projectile.localAI[0] = 0.0f;
                  if (Main.myPlayer == this.Projectile.owner && player.HeldItem.CountsAsClass(DamageClass.Melee))
                  {
                    int index = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) npc).Center), 40f)), Vector2.op_Multiply(16f, Utils.RotatedByRandom(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) npc).Center), (double) MathHelper.ToRadians(15f))), ModContent.ProjectileType<EridanusFist>(), (int) ((double) this.Projectile.originalDamage * (double) ((StatModifier) ref Main.player[this.Projectile.owner].GetDamage(DamageClass.Melee)).Additive / 3.0), this.Projectile.knockBack / 2f, Main.myPlayer, 700f, 0.0f, 0.0f);
                    if (index != Main.maxProjectiles)
                      Main.projectile[index].CritChance = (int) player.ActualClassCrit(DamageClass.Melee);
                  }
                }
                this.Projectile.frame = player.HeldItem.CountsAsClass(DamageClass.Melee) ? 6 : 5;
                this.Projectile.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) npc).Center));
                if (this.Projectile.spriteDirection < 0)
                {
                  this.Projectile.rotation += 3.14159274f;
                  break;
                }
                break;
              case 1:
                Vector2 center1 = ((Entity) player).Center;
                center1.X -= (float) (50 * ((Entity) player).direction);
                center1.Y -= 40f;
                ((Entity) this.Projectile).Center = Vector2.Lerp(((Entity) this.Projectile).Center, center1, 0.15f);
                Projectile projectile2 = this.Projectile;
                ((Entity) projectile2).velocity = Vector2.op_Multiply(((Entity) projectile2).velocity, 0.8f);
                if ((double) ++this.Projectile.localAI[0] > 65.0)
                {
                  this.Projectile.localAI[0] = 0.0f;
                  if (Main.myPlayer == this.Projectile.owner && player.HeldItem.CountsAsClass(DamageClass.Ranged))
                  {
                    int index = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.op_Multiply(12f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) npc).Center)), ModContent.ProjectileType<EridanusBullet>(), (int) ((double) this.Projectile.originalDamage * (double) ((StatModifier) ref Main.player[this.Projectile.owner].GetDamage(DamageClass.Ranged)).Additive * 1.5), this.Projectile.knockBack * 2f, Main.myPlayer, (float) ((Entity) npc).whoAmI, 0.0f, 0.0f);
                    if (index != Main.maxProjectiles)
                      Main.projectile[index].CritChance = (int) player.ActualClassCrit(DamageClass.Ranged);
                  }
                }
                if (player.HeldItem.CountsAsClass(DamageClass.Ranged))
                {
                  if ((double) this.Projectile.localAI[0] < 15.0)
                  {
                    this.Projectile.frame = 8;
                    break;
                  }
                  if ((double) this.Projectile.localAI[0] > 50.0)
                  {
                    this.Projectile.frame = 7;
                    break;
                  }
                  break;
                }
                break;
              case 2:
                ((Entity) this.Projectile).Center = Vector2.Lerp(((Entity) this.Projectile).Center, Vector2.op_Addition(((Entity) player).Center, Vector2.op_Division(Vector2.op_Subtraction(((Entity) npc).Center, ((Entity) player).Center), 3f)), 0.15f);
                Projectile projectile3 = this.Projectile;
                ((Entity) projectile3).velocity = Vector2.op_Multiply(((Entity) projectile3).velocity, 0.8f);
                if (player.HeldItem.CountsAsClass(DamageClass.Magic) && (double) this.Projectile.localAI[0] > 45.0)
                  this.Projectile.frame = 7;
                if ((double) ++this.Projectile.localAI[0] > 60.0)
                {
                  if ((double) this.Projectile.localAI[0] > 90.0)
                    this.Projectile.localAI[0] = 0.0f;
                  if (player.HeldItem.CountsAsClass(DamageClass.Magic))
                    this.Projectile.frame = 8;
                  if ((double) this.Projectile.localAI[0] % 5.0 == 0.0 && player.HeldItem.CountsAsClass(DamageClass.Magic))
                  {
                    SoundEngine.PlaySound(ref SoundID.Item88, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
                    if (Main.myPlayer == this.Projectile.owner)
                    {
                      Vector2 center2 = ((Entity) this.Projectile).Center;
                      center2.X += Utils.NextFloat(Main.rand, -250f, 250f);
                      center2.Y -= 600f;
                      Vector2 vector2_1 = Vector2.op_Multiply(10f, ((Entity) npc).DirectionFrom(center2));
                      Vector2 vector2_2 = Vector2.op_Addition(center2, Vector2.op_Multiply(((Entity) npc).velocity, Utils.NextFloat(Main.rand, 10f)));
                      int index = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), vector2_2, vector2_1, 645, (int) ((double) this.Projectile.originalDamage * (double) ((StatModifier) ref player.GetDamage(DamageClass.Magic)).Additive / 2.0), this.Projectile.knockBack / 2f, Main.myPlayer, 0.0f, ((Entity) npc).Center.Y, 0.0f);
                      if (index != Main.maxProjectiles)
                      {
                        Main.projectile[index].CritChance = (int) player.ActualClassCrit(DamageClass.Magic);
                        break;
                      }
                      break;
                    }
                    break;
                  }
                  break;
                }
                break;
              default:
                Vector2 center3 = ((Entity) npc).Center;
                center3.X += (float) (350 * Math.Sign(((Entity) player).Center.X - ((Entity) npc).Center.X));
                if ((double) ((Entity) this.Projectile).Distance(center3) > 50.0)
                  this.Movement(center3, 0.8f, 32f);
                this.Projectile.frame = 5;
                bool flag = player.controlUseItem && (player.HeldItem.CountsAsClass(DamageClass.Melee) || player.HeldItem.CountsAsClass(DamageClass.Ranged) || player.HeldItem.CountsAsClass(DamageClass.Magic) || player.HeldItem.CountsAsClass(DamageClass.Throwing)) && player.HeldItem.pick == 0 && player.HeldItem.axe == 0 && player.HeldItem.hammer == 0;
                if ((double) ++this.Projectile.localAI[0] > 15.0)
                {
                  this.Projectile.localAI[0] = 0.0f;
                  if (Main.myPlayer == this.Projectile.owner && !flag)
                  {
                    int num2 = Math.Sign(((Entity) this.Projectile).Center.Y - ((Entity) npc).Center.Y);
                    Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.op_Multiply(3000f, ((Entity) this.Projectile).DirectionFrom(((Entity) npc).Center)), (float) num2)), Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) npc).Center), (float) num2), ModContent.ProjectileType<EridanusDeathray>(), this.Projectile.damage, this.Projectile.knockBack / 4f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                  }
                }
                if (!flag && (double) this.Projectile.localAI[0] < 7.0)
                  this.Projectile.frame = 6;
                this.Projectile.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) npc).Center));
                if (this.Projectile.spriteDirection < 0)
                {
                  this.Projectile.rotation += 3.14159274f;
                  break;
                }
                break;
            }
          }
          else
          {
            this.Projectile.ai[0] = -1f;
            this.Projectile.localAI[0] = 0.0f;
            this.Projectile.netUpdate = true;
          }
        }
        else
        {
          this.Projectile.localAI[0] = 0.0f;
          Vector2 center = ((Entity) player).Center;
          center.X -= (float) (50 * ((Entity) player).direction);
          center.Y -= 40f;
          ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = ((Entity) player).direction;
          if ((double) ((Entity) this.Projectile).Distance(center) > 2000.0)
          {
            ((Entity) this.Projectile).Center = ((Entity) player).Center;
            ((Entity) this.Projectile).velocity = Vector2.Zero;
          }
          else
          {
            ((Entity) this.Projectile).Center = Vector2.Lerp(((Entity) this.Projectile).Center, center, 0.25f);
            Projectile projectile = this.Projectile;
            ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 0.8f);
          }
          if ((double) ++this.Projectile.localAI[1] > 6.0)
          {
            this.Projectile.localAI[1] = 0.0f;
            this.Projectile.ai[0] = (float) FargoSoulsUtil.FindClosestHostileNPC(((Entity) this.Projectile).Center, 1500f);
            this.Projectile.netUpdate = true;
          }
        }
        if (++this.drawTrailOffset <= 2)
          return;
        this.drawTrailOffset = 0;
      }
      else
        this.Projectile.Kill();
    }

    private void Movement(Vector2 targetPos, float speedModifier, float cap = 12f, bool fastY = false)
    {
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
        ((Entity) this.Projectile).velocity.Y += fastY ? speedModifier * 2f : speedModifier;
        if ((double) ((Entity) this.Projectile).velocity.Y < 0.0)
          ((Entity) this.Projectile).velocity.Y += speedModifier * 2f;
      }
      else
      {
        ((Entity) this.Projectile).velocity.Y -= fastY ? speedModifier * 2f : speedModifier;
        if ((double) ((Entity) this.Projectile).velocity.Y > 0.0)
          ((Entity) this.Projectile).velocity.Y -= speedModifier * 2f;
      }
      if ((double) Math.Abs(((Entity) this.Projectile).velocity.X) > (double) cap)
        ((Entity) this.Projectile).velocity.X = cap * (float) Math.Sign(((Entity) this.Projectile).velocity.X);
      if ((double) Math.Abs(((Entity) this.Projectile).velocity.Y) <= (double) cap)
        return;
      ((Entity) this.Projectile).velocity.Y = cap * (float) Math.Sign(((Entity) this.Projectile).velocity.Y);
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 360, false);
    }

    public virtual bool? CanCutTiles() => new bool?(false);

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D1 = TextureAssets.Projectile[this.Projectile.type].Value;
      Texture2D texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/Champions/Cosmos/CosmosChampion_Glow", (AssetRequestMode) 1).Value;
      Texture2D texture2D3 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/Champions/Cosmos/CosmosChampion_Glow2", (AssetRequestMode) 1).Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D1.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 1 : (SpriteEffects) 0;
      Color alpha = this.Projectile.GetAlpha(lightColor);
      int num3 = 150;
      Color color;
      // ISSUE: explicit constructor call
      ((Color) ref color).\u002Ector(num3 + Main.DiscoR / 3, num3 + Main.DiscoG / 3, num3 + Main.DiscoB / 3);
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        if (index % 2 != (this.drawTrailOffset > 1 ? 1 : 0))
        {
          Vector2 oldPo = this.Projectile.oldPos[index];
          float num4 = this.Projectile.oldRot[index];
          Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), Color.op_Multiply(color, 0.5f), num4, vector2, this.Projectile.scale, spriteEffects, 0.0f);
        }
      }
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.NonPremultiplied, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      Main.EntitySpriteDraw(texture2D3, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      Main.EntitySpriteDraw(texture2D3, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      return false;
    }
  }
}
