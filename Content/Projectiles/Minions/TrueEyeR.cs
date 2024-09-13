// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.TrueEyeR
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
  public class TrueEyeR : ModProjectile
  {
    private float localAI0;
    private float localAI1;

    public virtual string Texture => "Terraria/Images/Projectile_650";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 4;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.netImportant = true;
      ((Entity) this.Projectile).width = 32;
      ((Entity) this.Projectile).height = 42;
      this.Projectile.timeLeft *= 5;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.minion = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 10;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      if (((Entity) player).whoAmI == Main.myPlayer && ((Entity) player).active && !player.dead && player.FargoSouls().TrueEyes)
      {
        this.Projectile.timeLeft = 2;
        this.Projectile.netUpdate = true;
      }
      if (this.Projectile.damage == 0)
        this.Projectile.damage = (int) (60.0 * (double) ((StatModifier) ref player.GetDamage(DamageClass.Summon)).Additive);
      DelegateMethods.v3_1 = Vector3.op_Multiply(new Vector3(0.5f, 0.9f, 1f), 1.5f);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      Utils.PlotTileLine(((Entity) this.Projectile).Center, Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 6f)), 20f, TrueEyeR.\u003C\u003EO.\u003C0\u003E__CastLightOpen ?? (TrueEyeR.\u003C\u003EO.\u003C0\u003E__CastLightOpen = new Utils.TileActionAttempt((object) null, __methodptr(CastLightOpen))));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      Utils.PlotTileLine(((Entity) this.Projectile).Left, ((Entity) this.Projectile).Right, 20f, TrueEyeR.\u003C\u003EO.\u003C0\u003E__CastLightOpen ?? (TrueEyeR.\u003C\u003EO.\u003C0\u003E__CastLightOpen = new Utils.TileActionAttempt((object) null, __methodptr(CastLightOpen))));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      Utils.PlotTileLine(((Entity) player).Center, Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(((Entity) player).velocity, 6f)), 40f, TrueEyeR.\u003C\u003EO.\u003C0\u003E__CastLightOpen ?? (TrueEyeR.\u003C\u003EO.\u003C0\u003E__CastLightOpen = new Utils.TileActionAttempt((object) null, __methodptr(CastLightOpen))));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      Utils.PlotTileLine(((Entity) player).Left, ((Entity) player).Right, 40f, TrueEyeR.\u003C\u003EO.\u003C0\u003E__CastLightOpen ?? (TrueEyeR.\u003C\u003EO.\u003C0\u003E__CastLightOpen = new Utils.TileActionAttempt((object) null, __methodptr(CastLightOpen))));
      if ((double) this.Projectile.ai[0] >= 0.0 && (double) this.Projectile.ai[0] < (double) Main.maxNPCs)
      {
        NPC minionAttackTargetNpc = this.Projectile.OwnerMinionAttackTargetNPC;
        if (minionAttackTargetNpc != null && (double) this.Projectile.ai[0] != (double) ((Entity) minionAttackTargetNpc).whoAmI && minionAttackTargetNpc.CanBeChasedBy((object) null, false))
          this.Projectile.ai[0] = (float) ((Entity) minionAttackTargetNpc).whoAmI;
        ++this.Projectile.localAI[0];
        NPC npc1 = Main.npc[(int) this.Projectile.ai[0]];
        if (npc1.CanBeChasedBy((object) null, false))
        {
          switch ((int) this.Projectile.ai[1])
          {
            case 0:
              Vector2 vector2_1 = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) npc1).Center, ((Entity) this.Projectile).Center), new Vector2(200f, -200f));
              if (Vector2.op_Inequality(vector2_1, Vector2.Zero))
              {
                ((Vector2) ref vector2_1).Normalize();
                vector2_1 = Vector2.op_Multiply(vector2_1, 24f);
                ((Entity) this.Projectile).velocity.X = (float) (((double) ((Entity) this.Projectile).velocity.X * 29.0 + (double) vector2_1.X) / 30.0);
                ((Entity) this.Projectile).velocity.Y = (float) (((double) ((Entity) this.Projectile).velocity.Y * 29.0 + (double) vector2_1.Y) / 30.0);
              }
              if ((double) ((Entity) this.Projectile).Distance(((Entity) npc1).Center) < 150.0)
              {
                if ((double) ((Entity) this.Projectile).Center.X < (double) ((Entity) npc1).Center.X)
                  ((Entity) this.Projectile).velocity.X -= 0.25f;
                else
                  ((Entity) this.Projectile).velocity.X += 0.25f;
                if ((double) ((Entity) this.Projectile).Center.Y < (double) ((Entity) npc1).Center.Y)
                  ((Entity) this.Projectile).velocity.Y -= 0.25f;
                else
                  ((Entity) this.Projectile).velocity.Y += 0.25f;
              }
              if ((double) this.Projectile.localAI[0] > 90.0)
              {
                if ((double) ((Entity) this.Projectile).Distance(((Entity) npc1).Center) > 1500.0)
                {
                  this.Projectile.ai[0] = (float) FargoSoulsUtil.FindClosestHostileNPCPrioritizingMinionFocus(this.Projectile, 1000f, center: new Vector2());
                  this.Projectile.ai[1] = 0.0f;
                  this.Projectile.netUpdate = true;
                }
                this.Projectile.localAI[0] = 0.0f;
                ++this.Projectile.ai[1];
              }
              if ((double) this.Projectile.rotation > 3.14159274101257)
                this.Projectile.rotation -= 6.283185f;
              this.Projectile.rotation = (double) this.Projectile.rotation <= -0.005 || (double) this.Projectile.rotation >= 0.005 ? this.Projectile.rotation * 0.96f : 0.0f;
              break;
            case 1:
              if ((double) this.Projectile.localAI[0] == 1.0)
              {
                for (int index = 0; index < 6; ++index)
                {
                  Vector2 vector2_2 = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.UnitY, 6f)), Utils.RotatedBy(new Vector2(50f, 0.0f), 1.0471975803375244 * (double) index, new Vector2()));
                  if (this.Projectile.owner == Main.myPlayer)
                    Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), vector2_2, Vector2.Zero, ModContent.ProjectileType<PhantasmalSphereTrueEye>(), this.Projectile.damage / 3 * 11, 10f, this.Projectile.owner, (float) this.Projectile.identity, (float) index, 0.0f);
                }
              }
              Projectile projectile1 = this.Projectile;
              ((Entity) projectile1).velocity = Vector2.op_Multiply(((Entity) projectile1).velocity, 0.95f);
              if ((double) this.Projectile.localAI[0] > 60.0)
              {
                ((Entity) this.Projectile).velocity = Vector2.Zero;
                this.Projectile.localAI[0] = 0.0f;
                ++this.Projectile.ai[1];
              }
              if ((double) this.Projectile.rotation > 3.14159274101257)
                this.Projectile.rotation -= 6.283185f;
              this.Projectile.rotation = (double) this.Projectile.rotation <= -0.005 || (double) this.Projectile.rotation >= 0.005 ? this.Projectile.rotation * 0.96f : 0.0f;
              break;
            case 2:
              if ((double) this.Projectile.localAI[0] == 1.0)
              {
                SoundStyle zombie102 = SoundID.Zombie102;
                ((SoundStyle) ref zombie102).Volume = 0.75f;
                ((SoundStyle) ref zombie102).Pitch = 0.0f;
                SoundEngine.PlaySound(ref zombie102, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
                ((Entity) this.Projectile).velocity = Vector2.op_Subtraction(((Entity) npc1).Center, ((Entity) this.Projectile).Center);
                if (Vector2.op_Inequality(((Entity) this.Projectile).velocity, Vector2.Zero))
                {
                  ((Vector2) ref ((Entity) this.Projectile).velocity).Normalize();
                  Projectile projectile2 = this.Projectile;
                  ((Entity) projectile2).velocity = Vector2.op_Multiply(((Entity) projectile2).velocity, 24f);
                }
              }
              else if ((double) this.Projectile.localAI[0] > 10.0)
              {
                this.Projectile.localAI[0] = 0.0f;
                ++this.Projectile.ai[1];
              }
              float num1 = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
              if ((double) Math.Abs(this.Projectile.rotation - num1) >= 3.14159274101257)
                this.Projectile.rotation = (double) num1 >= (double) this.Projectile.rotation ? this.Projectile.rotation + 6.283185f : this.Projectile.rotation - 6.283185f;
              float num2 = 12f;
              this.Projectile.rotation = (this.Projectile.rotation * (num2 - 1f) + num1) / num2;
              break;
            default:
              this.Projectile.ai[1] = 0.0f;
              goto case 0;
          }
        }
        else if ((double) this.Projectile.ai[1] == 0.0)
        {
          this.Projectile.ai[0] = (float) FargoSoulsUtil.FindClosestHostileNPCPrioritizingMinionFocus(this.Projectile, 1000f, center: new Vector2());
          this.Projectile.ai[1] = 0.0f;
          this.Projectile.localAI[0] = 0.0f;
          this.Projectile.localAI[1] = 0.0f;
        }
        else
        {
          float num3 = 1000f;
          int num4 = -1;
          for (int index = 0; index < 200; ++index)
          {
            NPC npc2 = Main.npc[index];
            if (npc2.CanBeChasedBy((object) null, false))
            {
              float num5 = ((Entity) this.Projectile).Distance(((Entity) npc2).Center);
              if ((double) num5 < (double) num3)
              {
                num3 = num5;
                num4 = index;
              }
            }
          }
          this.Projectile.ai[0] = (float) num4;
          if ((double) this.Projectile.ai[0] == -1.0)
          {
            this.Projectile.localAI[0] = 0.0f;
            this.Projectile.localAI[1] = 0.0f;
            this.Projectile.ai[1] = 0.0f;
            this.Projectile.netUpdate = true;
          }
        }
        if (++this.Projectile.frameCounter >= 4)
        {
          this.Projectile.frameCounter = 0;
          if (++this.Projectile.frame >= Main.projFrames[this.Projectile.type])
            this.Projectile.frame = 0;
        }
        this.UpdatePupil();
      }
      else
      {
        if ((double) this.Projectile.localAI[1]++ > 15.0)
        {
          this.Projectile.ai[0] = (float) FargoSoulsUtil.FindClosestHostileNPCPrioritizingMinionFocus(this.Projectile, 1000f, center: new Vector2());
          this.Projectile.ai[1] = 0.0f;
          this.Projectile.localAI[0] = 0.0f;
          this.Projectile.localAI[1] = 0.0f;
          this.Projectile.netUpdate = true;
        }
        Vector2 vector2_3;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_3).\u002Ector((float) (((Entity) player).direction * 100), 0.0f);
        Vector2 vector2_4 = Vector2.op_Addition(player.MountedCenter, vector2_3);
        double num6 = (double) Vector2.Distance(((Entity) this.Projectile).Center, vector2_4);
        if (num6 > 1500.0)
          ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) player).Center, vector2_3);
        Vector2 vector2_5 = Vector2.op_Subtraction(vector2_4, ((Entity) this.Projectile).Center);
        float num7 = 4f;
        if (num6 < (double) num7)
        {
          Projectile projectile = this.Projectile;
          ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 0.25f);
        }
        if (Vector2.op_Inequality(vector2_5, Vector2.Zero))
        {
          if ((double) ((Vector2) ref vector2_5).Length() < (double) num7)
            ((Entity) this.Projectile).velocity = vector2_5;
          else
            ((Entity) this.Projectile).velocity = Vector2.op_Multiply(vector2_5, 0.1f);
        }
        if ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() > 6.0)
        {
          float num8 = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
          if ((double) Math.Abs(this.Projectile.rotation - num8) >= 3.14159274101257)
            this.Projectile.rotation = (double) num8 >= (double) this.Projectile.rotation ? this.Projectile.rotation + 6.283185f : this.Projectile.rotation - 6.283185f;
          this.Projectile.rotation = (float) (((double) this.Projectile.rotation * 11.0 + (double) num8) / 12.0);
          if (++this.Projectile.frameCounter >= 4)
          {
            this.Projectile.frameCounter = 0;
            if (++this.Projectile.frame >= Main.projFrames[this.Projectile.type])
              this.Projectile.frame = 0;
          }
        }
        else
        {
          if ((double) this.Projectile.rotation > 3.14159274101257)
            this.Projectile.rotation -= 6.283185f;
          this.Projectile.rotation = (double) this.Projectile.rotation <= -0.005 || (double) this.Projectile.rotation >= 0.005 ? this.Projectile.rotation * 0.96f : 0.0f;
          if (++this.Projectile.frameCounter >= 6)
          {
            this.Projectile.frameCounter = 0;
            if (++this.Projectile.frame >= Main.projFrames[this.Projectile.type])
              this.Projectile.frame = 0;
          }
        }
        this.UpdatePupil();
      }
    }

    private void UpdatePupil()
    {
      Player player = Main.player[this.Projectile.owner];
      float num1 = (float) ((double) this.localAI0 % 6.28318548202515 - 3.14159274101257);
      float num2 = (float) Math.IEEERemainder((double) this.localAI1, 1.0);
      if ((double) num2 < 0.0)
        ++num2;
      float num3 = (float) Math.Floor((double) this.localAI1);
      float num4 = 0.999f;
      int num5 = 0;
      float num6 = 0.1f;
      float num7;
      float num8;
      float num9;
      if ((double) this.Projectile.ai[0] != -1.0)
      {
        num7 = ((Entity) this.Projectile).AngleTo(((Entity) Main.npc[(int) this.Projectile.ai[0]]).Center);
        num5 = 2;
        num8 = MathHelper.Clamp(num2 + 0.05f, 0.0f, num4);
        num9 = num3 + (float) Math.Sign(-12f - num3);
      }
      else if ((double) ((Vector2) ref ((Entity) player).velocity).Length() > 3.0)
      {
        num7 = ((Entity) this.Projectile).AngleTo(Vector2.op_Addition(((Entity) this.Projectile).Center, ((Entity) player).velocity));
        num5 = 1;
        num8 = MathHelper.Clamp(num2 + 0.05f, 0.0f, num4);
        num9 = num3 + (float) Math.Sign(-10f - num3);
      }
      else
      {
        num7 = ((Entity) player).direction == 1 ? 0.0f : 3.141603f;
        num8 = MathHelper.Clamp(num2 + (float) Math.Sign(0.75f - num2) * 0.05f, 0.0f, num4);
        num9 = num3 + (float) Math.Sign(0.0f - num3);
        num6 = 0.12f;
      }
      Vector2 rotationVector2 = Utils.ToRotationVector2(num7);
      this.localAI0 = (float) ((double) Utils.ToRotation(Vector2.Lerp(Utils.ToRotationVector2(num1), rotationVector2, num6)) + (double) num5 * 6.28318548202515 + 3.14159274101257);
      this.localAI1 = num9 + num8;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 360, false);
    }

    public virtual bool? CanCutTiles() => new bool?(false);

    public virtual Color? GetAlpha(Color lightColor)
    {
      int num1 = (int) ((Color) ref lightColor).R * 3 / 2;
      int num2 = (int) ((Color) ref lightColor).G * 3 / 2;
      int num3 = (int) ((Color) ref lightColor).B * 3 / 2;
      if (num1 > (int) byte.MaxValue)
        num1 = (int) byte.MaxValue;
      if (num2 > (int) byte.MaxValue)
        num2 = (int) byte.MaxValue;
      if (num3 > (int) byte.MaxValue)
        num3 = (int) byte.MaxValue;
      return new Color?(new Color(num1, num2, num3));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D1 = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D1.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      Texture2D texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/Minions/TrueEyePupil", (AssetRequestMode) 1).Value;
      Vector2 vector2_2 = Vector2.op_Addition(Utils.RotatedBy(new Vector2(this.localAI1 / 2f, 0.0f), (double) this.localAI0, new Vector2()), Utils.RotatedBy(new Vector2(0.0f, -6f), (double) this.Projectile.rotation, new Vector2()));
      Vector2 vector2_3 = Vector2.op_Division(Utils.Size(texture2D2), 2f);
      Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(vector2_2, ((Entity) this.Projectile).Center), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(texture2D2.Bounds), this.Projectile.GetAlpha(lightColor), 0.0f, vector2_3, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
