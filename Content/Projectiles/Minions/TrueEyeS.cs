// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.TrueEyeS
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
  public class TrueEyeS : ModProjectile
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
      Utils.PlotTileLine(((Entity) this.Projectile).Center, Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 6f)), 20f, TrueEyeS.\u003C\u003EO.\u003C0\u003E__CastLightOpen ?? (TrueEyeS.\u003C\u003EO.\u003C0\u003E__CastLightOpen = new Utils.TileActionAttempt((object) null, __methodptr(CastLightOpen))));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      Utils.PlotTileLine(((Entity) this.Projectile).Left, ((Entity) this.Projectile).Right, 20f, TrueEyeS.\u003C\u003EO.\u003C0\u003E__CastLightOpen ?? (TrueEyeS.\u003C\u003EO.\u003C0\u003E__CastLightOpen = new Utils.TileActionAttempt((object) null, __methodptr(CastLightOpen))));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      Utils.PlotTileLine(((Entity) player).Center, Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(((Entity) player).velocity, 6f)), 40f, TrueEyeS.\u003C\u003EO.\u003C0\u003E__CastLightOpen ?? (TrueEyeS.\u003C\u003EO.\u003C0\u003E__CastLightOpen = new Utils.TileActionAttempt((object) null, __methodptr(CastLightOpen))));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      Utils.PlotTileLine(((Entity) player).Left, ((Entity) player).Right, 40f, TrueEyeS.\u003C\u003EO.\u003C0\u003E__CastLightOpen ?? (TrueEyeS.\u003C\u003EO.\u003C0\u003E__CastLightOpen = new Utils.TileActionAttempt((object) null, __methodptr(CastLightOpen))));
      if ((double) this.Projectile.ai[0] >= 0.0 && (double) this.Projectile.ai[0] < (double) Main.maxNPCs)
      {
        NPC minionAttackTargetNpc = this.Projectile.OwnerMinionAttackTargetNPC;
        if (minionAttackTargetNpc != null && (double) this.Projectile.ai[0] != (double) ((Entity) minionAttackTargetNpc).whoAmI && minionAttackTargetNpc.CanBeChasedBy((object) null, false))
          this.Projectile.ai[0] = (float) ((Entity) minionAttackTargetNpc).whoAmI;
        ++this.Projectile.localAI[0];
        NPC npc = Main.npc[(int) this.Projectile.ai[0]];
        if (npc.CanBeChasedBy((object) null, false))
        {
          switch ((int) this.Projectile.ai[1])
          {
            case 0:
              Vector2 vector2 = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) npc).Center, ((Entity) this.Projectile).Center), new Vector2(-200f, -200f));
              if (Vector2.op_Inequality(vector2, Vector2.Zero))
              {
                ((Vector2) ref vector2).Normalize();
                vector2 = Vector2.op_Multiply(vector2, 24f);
                ((Entity) this.Projectile).velocity.X = (float) (((double) ((Entity) this.Projectile).velocity.X * 29.0 + (double) vector2.X) / 30.0);
                ((Entity) this.Projectile).velocity.Y = (float) (((double) ((Entity) this.Projectile).velocity.Y * 29.0 + (double) vector2.Y) / 30.0);
              }
              if ((double) ((Entity) this.Projectile).Distance(((Entity) npc).Center) < 150.0)
              {
                if ((double) ((Entity) this.Projectile).Center.X < (double) ((Entity) npc).Center.X)
                  ((Entity) this.Projectile).velocity.X -= 0.25f;
                else
                  ((Entity) this.Projectile).velocity.X += 0.25f;
                if ((double) ((Entity) this.Projectile).Center.Y < (double) ((Entity) npc).Center.Y)
                  ((Entity) this.Projectile).velocity.Y -= 0.25f;
                else
                  ((Entity) this.Projectile).velocity.Y += 0.25f;
              }
              if ((double) this.Projectile.localAI[0] > 60.0)
              {
                if ((double) ((Entity) this.Projectile).Distance(((Entity) npc).Center) > 1500.0)
                {
                  this.Projectile.ai[0] = (float) FargoSoulsUtil.FindClosestHostileNPCPrioritizingMinionFocus(this.Projectile, 1000f, center: new Vector2());
                  this.Projectile.ai[1] = 0.0f;
                  this.Projectile.localAI[1] = 0.0f;
                }
                this.Projectile.localAI[0] = 0.0f;
                ++this.Projectile.ai[1];
                break;
              }
              break;
            case 1:
              Projectile projectile = this.Projectile;
              ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 0.9f);
              if ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() < 1.0)
              {
                ((Entity) this.Projectile).velocity = Vector2.Zero;
                this.Projectile.localAI[0] = 0.0f;
                ++this.Projectile.ai[1];
                break;
              }
              break;
            case 2:
              if ((double) this.Projectile.localAI[0] == 7.0)
              {
                SoundStyle npcDeath6 = SoundID.NPCDeath6;
                ((SoundStyle) ref npcDeath6).Volume = 0.75f;
                ((SoundStyle) ref npcDeath6).Pitch = 0.0f;
                SoundEngine.PlaySound(ref npcDeath6, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
                this.ShootBolts(npc);
                break;
              }
              if ((double) this.Projectile.localAI[0] == 14.0)
              {
                this.ShootBolts(npc);
                break;
              }
              if ((double) this.Projectile.localAI[0] > 21.0)
              {
                this.Projectile.localAI[0] = 0.0f;
                ++this.Projectile.ai[1];
                break;
              }
              break;
            default:
              this.Projectile.ai[1] = 0.0f;
              goto case 0;
          }
        }
        else
        {
          this.Projectile.ai[0] = (float) FargoSoulsUtil.FindClosestHostileNPCPrioritizingMinionFocus(this.Projectile, 1000f, center: new Vector2());
          this.Projectile.ai[1] = 0.0f;
          this.Projectile.localAI[0] = 0.0f;
          this.Projectile.localAI[1] = 0.0f;
        }
        if ((double) this.Projectile.rotation > 3.14159274101257)
          this.Projectile.rotation -= 6.283185f;
        this.Projectile.rotation = (double) this.Projectile.rotation <= -0.005 || (double) this.Projectile.rotation >= 0.005 ? this.Projectile.rotation * 0.96f : 0.0f;
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
        }
        Vector2 vector2_1;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_1).\u002Ector((float) ((Entity) player).direction * -100f, 0.0f);
        Vector2 vector2_2 = Vector2.op_Addition(player.MountedCenter, vector2_1);
        double num1 = (double) Vector2.Distance(((Entity) this.Projectile).Center, vector2_2);
        if (num1 > 1500.0)
          ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) player).Center, vector2_1);
        Vector2 vector2_3 = Vector2.op_Subtraction(vector2_2, ((Entity) this.Projectile).Center);
        float num2 = 4f;
        if (num1 < (double) num2)
        {
          Projectile projectile = this.Projectile;
          ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 0.25f);
        }
        if (Vector2.op_Inequality(vector2_3, Vector2.Zero))
        {
          if ((double) ((Vector2) ref vector2_3).Length() < (double) num2)
            ((Entity) this.Projectile).velocity = vector2_3;
          else
            ((Entity) this.Projectile).velocity = Vector2.op_Multiply(vector2_3, 0.1f);
        }
        if ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() > 6.0)
        {
          float num3 = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
          if ((double) Math.Abs(this.Projectile.rotation - num3) >= 3.14159274101257)
            this.Projectile.rotation = (double) num3 >= (double) this.Projectile.rotation ? this.Projectile.rotation + 6.283185f : this.Projectile.rotation - 6.283185f;
          this.Projectile.rotation = (float) (((double) this.Projectile.rotation * 11.0 + (double) num3) / 12.0);
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

    private void ShootBolts(NPC npc)
    {
      Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.UnitY, 6f));
      Vector2 vector2_2 = Vector2.op_Subtraction(Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(((Entity) npc).velocity, 15f)), vector2_1);
      if (!Vector2.op_Inequality(vector2_2, Vector2.Zero))
        return;
      ((Vector2) ref vector2_2).Normalize();
      vector2_2 = Vector2.op_Multiply(vector2_2, 8f);
      for (int index = -1; index <= 1; ++index)
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), vector2_1, Utils.RotatedBy(vector2_2, Math.PI / 24.0 * (double) index, new Vector2()), ModContent.ProjectileType<PhantasmalBoltTrueEye>(), this.Projectile.damage / 3 * 7, 6f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
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
