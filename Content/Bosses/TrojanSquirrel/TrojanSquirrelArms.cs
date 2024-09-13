// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.TrojanSquirrel.TrojanSquirrelArms
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.TrojanSquirrel
{
  public class TrojanSquirrelArms : TrojanSquirrelLimb
  {
    public override void SetDefaults()
    {
      base.SetDefaults();
      this.NPC.lifeMax = 450;
      ((Entity) this.NPC).width = this.baseWidth = 114;
      ((Entity) this.NPC).height = this.baseHeight = 64;
    }

    public virtual void AI()
    {
      base.AI();
      if (this.body == null)
        return;
      ((Entity) this.NPC).velocity = Vector2.Zero;
      this.NPC.target = this.body.target;
      ((Entity) this.NPC).direction = this.NPC.spriteDirection = ((Entity) this.body).direction;
      ((Entity) this.NPC).Center = Vector2.op_Addition(((Entity) this.body).Bottom, Vector2.op_Multiply(new Vector2(18f * (float) ((Entity) this.NPC).direction, -105f), this.body.scale));
      switch ((int) this.NPC.ai[0])
      {
        case 0:
          if ((double) this.body.ai[0] != 0.0 || (double) this.body.localAI[0] > 0.0)
            break;
          this.NPC.ai[1] += WorldSavingSystem.EternityMode ? 1.5f : 1f;
          if (this.body.dontTakeDamage)
            ++this.NPC.ai[1];
          int num1 = 360;
          int num2 = num1 - 30;
          if ((double) this.NPC.ai[1] > (double) num2)
          {
            FargowiltasSouls.Content.Bosses.TrojanSquirrel.TrojanSquirrel trojanSquirrel = Luminance.Common.Utilities.Utilities.As<FargowiltasSouls.Content.Bosses.TrojanSquirrel.TrojanSquirrel>(this.body);
            if (trojanSquirrel.head != null && (double) trojanSquirrel.head.ai[0] != 0.0)
              this.NPC.ai[1] = (float) num2;
          }
          if ((double) this.NPC.ai[1] <= (double) num1 || (double) Math.Abs(((Entity) this.body).velocity.Y) >= 0.05000000074505806)
            break;
          float num3 = ((Entity) this.NPC).direction > 0 ? 0.0f : 3.14159274f;
          if ((double) Math.Abs(MathHelper.WrapAngle(Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) Main.player[this.NPC.target]).Center)) - num3)) > 0.78539818525314331)
          {
            this.NPC.ai[1] = (float) num2;
            break;
          }
          this.NPC.ai[0] = 1f + this.NPC.ai[2];
          this.NPC.ai[1] = 0.0f;
          if (Main.expertMode)
            this.NPC.ai[2] = (float) ((double) this.NPC.ai[2] == 0.0 ? 1 : 0);
          this.NPC.netUpdate = true;
          this.body.localAI[3] = (float) Math.Sign(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.body, ((Entity) Main.player[this.body.target]).Center).X);
          this.body.netUpdate = true;
          break;
        case 1:
          int num4 = 90;
          if (WorldSavingSystem.EternityMode)
            num4 -= 30;
          if (WorldSavingSystem.MasochistModeReal)
            num4 -= 30;
          int num5 = 310;
          int num6 = num4 / (WorldSavingSystem.MasochistModeReal ? 3 : 2);
          if ((double) this.NPC.ai[1] < (double) num4)
          {
            ((Entity) this.body).velocity.X *= 0.9f;
            if ((double) this.NPC.ai[1] % (double) num6 == 0.0)
              SoundEngine.PlaySound(ref SoundID.NPCHit41, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          }
          ++this.NPC.ai[1];
          this.NPC.ai[3] = (double) this.NPC.ai[1] >= (double) num4 || (double) this.NPC.ai[1] % (double) num6 >= (double) (num6 / 2) ? 0.0f : 1f;
          if ((double) this.NPC.ai[1] > (double) num4 && (double) this.NPC.ai[1] < (double) num5 && (double) this.NPC.ai[1] % (this.body.dontTakeDamage || WorldSavingSystem.MasochistModeReal ? 40.0 : 70.0) == 0.0)
          {
            Vector2 shootPos = this.GetShootPos();
            float num7 = ((Entity) this.NPC).direction > 0 ? 0.0f : 3.14159274f;
            float num8 = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) Main.player[this.NPC.target]).Center));
            if ((double) Math.Abs(MathHelper.WrapAngle(num8 - num7)) > 1.5707963705062866)
              num8 = 1.57079637f * (float) Math.Sign(num8);
            if (FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), shootPos, Vector2.op_Multiply(8f, Utils.ToRotationVector2(num8)), ModContent.ProjectileType<TrojanHook>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
          if ((double) this.NPC.ai[1] <= 300.0 || !FargoSoulsUtil.HostCheck || Main.LocalPlayer.ownedProjectileCounts[ModContent.ProjectileType<TrojanHook>()] > 0)
            break;
          this.NPC.ai[0] = 0.0f;
          this.NPC.ai[1] = 0.0f;
          this.NPC.netUpdate = true;
          this.body.localAI[3] = 0.0f;
          this.body.netUpdate = true;
          break;
        case 2:
          ++this.NPC.ai[1];
          int num9 = 70;
          int num10 = 340;
          if (WorldSavingSystem.EternityMode)
          {
            num9 -= 30;
            num10 -= 30;
          }
          if (WorldSavingSystem.MasochistModeReal)
            num10 -= 60;
          ((Entity) this.body).velocity.X *= 0.98f;
          if ((double) this.NPC.ai[1] == 10.0)
          {
            for (int index1 = 0; index1 < 2; ++index1)
            {
              Vector2 shootPos = this.GetShootPos();
              SoundEngine.PlaySound(ref SoundID.Item11, new Vector2?(shootPos), (SoundUpdateCallback) null);
              for (int index2 = 0; index2 < 20; ++index2)
              {
                int index3 = Dust.NewDust(shootPos, 0, 0, 51, 0.0f, 0.0f, 0, new Color(), 3f);
                Main.dust[index3].noGravity = true;
                Dust dust = Main.dust[index3];
                dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
                Main.dust[index3].velocity.X += (float) ((Entity) this.NPC).direction * Utils.NextFloat(Main.rand, 6f, 24f);
              }
            }
          }
          if ((double) this.NPC.ai[1] > (double) num9 && (double) this.NPC.ai[1] % 4.0 == 0.0)
          {
            if ((double) this.NPC.ai[1] % 8.0 == 0.0)
            {
              Vector2 shootPos = this.GetShootPos();
              SoundEngine.PlaySound(ref SoundID.Item11, new Vector2?(shootPos), (SoundUpdateCallback) null);
              float num11 = (this.NPC.ai[1] - (float) num9) / (float) (num10 - num9);
              Vector2 vector2_1 = ((Entity) this.NPC).Center;
              vector2_1.X += (float) Math.Sign(((Entity) this.NPC).direction) * (WorldSavingSystem.EternityMode ? 1800f : 1200f) * num11;
              vector2_1 = Vector2.op_Addition(vector2_1, Utils.NextVector2Circular(Main.rand, 16f, 16f));
              float num12 = 45f;
              Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, shootPos);
              vector2_2.X /= num12;
              vector2_2.Y = (float) ((double) vector2_2.Y / (double) num12 - 0.25 * (double) num12);
              if (FargoSoulsUtil.HostCheck)
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), shootPos, vector2_2, ModContent.ProjectileType<TrojanSnowball>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.5f, 0.0f, 0.0f);
            }
            this.NPC.ai[1] += (double) this.NPC.ai[1] > (double) (num10 / 3) ? ((double) this.NPC.ai[1] > 0.0 ? 3f : 1f) : 0.0f;
          }
          if ((double) this.NPC.ai[1] <= (double) num10)
            break;
          this.NPC.ai[0] = 0.0f;
          this.NPC.ai[1] = 0.0f;
          this.NPC.netUpdate = true;
          this.body.localAI[3] = 0.0f;
          this.body.netUpdate = true;
          break;
      }
    }

    private Vector2 GetShootPos()
    {
      this.NPC.localAI[0] = (float) ((double) this.NPC.localAI[0] == 0.0 ? 1 : 0);
      Vector2 bottom = ((Entity) this.NPC).Bottom;
      bottom.X += (float) ((Entity) this.NPC).width / 2f * (float) ((Entity) this.NPC).direction;
      bottom.Y -= 16f * this.NPC.scale;
      bottom.X -= (float) (((double) this.NPC.localAI[0] == 0.0 ? 10 : 48) * ((Entity) this.NPC).direction) * this.NPC.scale;
      return bottom;
    }

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      if (this.NPC.life > 0)
        return;
      for (int index = 8; index <= 10; ++index)
      {
        Vector2 vector2_1 = Utils.NextVector2FromRectangle(Main.rand, ((Entity) this.NPC).Hitbox);
        if (!Main.dedServ)
        {
          IEntitySource sourceFromThis = ((Entity) this.NPC).GetSource_FromThis((string) null);
          Vector2 vector2_2 = vector2_1;
          Vector2 velocity = ((Entity) this.NPC).velocity;
          string name = ((ModType) this).Mod.Name;
          DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(18, 1);
          interpolatedStringHandler.AppendLiteral("TrojanSquirrelGore");
          interpolatedStringHandler.AppendFormatted<int>(index);
          string stringAndClear = interpolatedStringHandler.ToStringAndClear();
          int type = ModContent.Find<ModGore>(name, stringAndClear).Type;
          double scale = (double) this.NPC.scale;
          Gore.NewGore(sourceFromThis, vector2_2, velocity, type, (float) scale);
        }
      }
    }
  }
}
