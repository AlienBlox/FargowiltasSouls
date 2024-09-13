// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.TrojanSquirrel.TrojanSquirrelHead
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.TrojanSquirrel
{
  public class TrojanSquirrelHead : TrojanSquirrelLimb
  {
    public override void SetDefaults()
    {
      base.SetDefaults();
      this.NPC.lifeMax = 600;
      ((Entity) this.NPC).width = this.baseWidth = 80;
      ((Entity) this.NPC).height = this.baseHeight = 76;
    }

    public virtual void AI()
    {
      base.AI();
      if (this.body == null)
        return;
      ((Entity) this.NPC).velocity = Vector2.Zero;
      this.NPC.target = this.body.target;
      ((Entity) this.NPC).direction = this.NPC.spriteDirection = ((Entity) this.body).direction;
      ((Entity) this.NPC).Center = Vector2.op_Addition(((Entity) this.body).Bottom, Vector2.op_Multiply(new Vector2(42f * (float) ((Entity) this.NPC).direction, -153f), this.body.scale));
      switch ((int) this.NPC.ai[0])
      {
        case 0:
          if ((double) this.body.ai[0] != 0.0 || (double) this.body.localAI[0] > 0.0)
            break;
          this.NPC.ai[1] += WorldSavingSystem.EternityMode ? 1.5f : 1f;
          if (this.body.dontTakeDamage)
            ++this.NPC.ai[1];
          int num1 = 240;
          int num2 = num1 - 30;
          if ((double) this.NPC.ai[1] > (double) num2)
          {
            FargowiltasSouls.Content.Bosses.TrojanSquirrel.TrojanSquirrel trojanSquirrel = Luminance.Common.Utilities.Utilities.As<FargowiltasSouls.Content.Bosses.TrojanSquirrel.TrojanSquirrel>(this.body);
            if (trojanSquirrel.arms != null && (double) trojanSquirrel.arms.ai[0] != 0.0)
              this.NPC.ai[1] = (float) num2;
          }
          if ((double) this.NPC.ai[1] <= (double) num1 || (double) Math.Abs(((Entity) this.body).velocity.Y) >= 0.05000000074505806)
            break;
          this.NPC.ai[0] = 1f + this.NPC.ai[2];
          this.NPC.ai[1] = 0.0f;
          if (Main.expertMode)
            this.NPC.ai[2] = (float) ((double) this.NPC.ai[2] == 0.0 ? 1 : 0);
          this.NPC.netUpdate = true;
          break;
        case 1:
          if ((double) this.NPC.ai[1] == 0.0 && !WorldSavingSystem.MasochistModeReal)
          {
            SoundEngine.PlaySound(ref SoundID.Item11, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            Vector2 center = ((Entity) this.NPC).Center;
            center.X += (float) (22 * ((Entity) this.NPC).direction);
            center.Y += 22f;
            for (int index1 = 0; index1 < 20; ++index1)
            {
              int index2 = Dust.NewDust(center, 0, 0, 3, 0.0f, 0.0f, 0, new Color(), 3f);
              Main.dust[index2].noGravity = true;
              Dust dust = Main.dust[index2];
              dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
              Main.dust[index2].velocity.X += (float) ((Entity) this.NPC).direction * Utils.NextFloat(Main.rand, 6f, 18f);
            }
          }
          if ((double) ++this.NPC.ai[1] % (this.body.dontTakeDamage || WorldSavingSystem.MasochistModeReal ? 30.0 : 45.0) == 0.0)
          {
            bool flag = true;
            if (!WorldSavingSystem.MasochistModeReal && (double) this.NPC.localAI[1] == 0.0)
            {
              this.NPC.localAI[1] = 1f;
              flag = false;
            }
            if (flag)
            {
              Vector2 center = ((Entity) this.NPC).Center;
              center.X += (float) (22 * ((Entity) this.NPC).direction);
              center.Y += 22f;
              float num3 = 80f;
              if (this.body.dontTakeDamage)
                num3 = 60f;
              if (WorldSavingSystem.MasochistModeReal)
                num3 = 45f;
              Vector2 vector2 = Vector2.op_Subtraction(((Entity) Main.player[this.NPC.target]).Center, center);
              vector2.X /= num3;
              vector2.Y = (float) ((double) vector2.Y / (double) num3 - 0.10000000149011612 * (double) num3);
              for (int index = 0; index < 10; ++index)
              {
                if (FargoSoulsUtil.HostCheck)
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), center, Vector2.op_Addition(vector2, Utils.NextVector2Square(Main.rand, -0.5f, 0.5f)), ModContent.ProjectileType<TrojanAcorn>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              }
            }
          }
          if ((double) this.NPC.ai[1] <= 210.0)
            break;
          this.NPC.ai[0] = 0.0f;
          this.NPC.ai[1] = 0.0f;
          this.NPC.netUpdate = true;
          break;
        case 2:
          if (WorldSavingSystem.MasochistModeReal && (double) this.NPC.ai[1] == 90.0)
          {
            NPC arms = (this.body.ModNPC as FargowiltasSouls.Content.Bosses.TrojanSquirrel.TrojanSquirrel).arms;
            if (arms != null && (double) arms.ai[0] != 2.0)
            {
              arms.ai[0] = 2f;
              arms.ai[1] = 0.0f;
              arms.netUpdate = true;
            }
          }
          ++this.NPC.ai[1];
          int num4 = 90;
          int num5 = 270;
          if (WorldSavingSystem.MasochistModeReal)
          {
            num4 = num4;
            num5 -= 60;
          }
          ((Entity) this.body).velocity.X *= 0.99f;
          if ((double) this.NPC.ai[1] % 4.0 == 0.0)
          {
            this.ShootSquirrelAt(Vector2.op_Addition(((Entity) this.body).Center, Utils.NextVector2Circular(Main.rand, 200f, 200f)));
            if ((double) this.NPC.ai[1] > (double) num4)
            {
              float num6 = (this.NPC.ai[1] - (float) num4) / (float) (num5 - num4);
              Vector2 target;
              // ISSUE: explicit constructor call
              ((Vector2) ref target).\u002Ector(((Entity) this.NPC).Center.X, ((Entity) Main.player[this.NPC.target]).Center.Y);
              target.X += (float) Math.Sign(((Entity) this.NPC).direction) * (float) (550.0 + (WorldSavingSystem.EternityMode ? 1800.0 : 1200.0) * (1.0 - (double) num6));
              this.ShootSquirrelAt(target);
            }
          }
          if ((double) this.NPC.ai[1] <= (double) num5)
            break;
          this.NPC.ai[0] = 0.0f;
          this.NPC.ai[1] = 0.0f;
          this.NPC.netUpdate = true;
          break;
      }
    }

    private void ShootSquirrelAt(Vector2 target)
    {
      float num1 = 0.6f;
      float num2 = 75f;
      if (this.body.dontTakeDamage)
        num2 -= 15f;
      if (WorldSavingSystem.MasochistModeReal)
        num2 -= 15f;
      float num3 = num1 * (75f / num2);
      Vector2 vector2 = Vector2.op_Subtraction(target, ((Entity) this.NPC).Center);
      vector2.X += Utils.NextFloat(Main.rand, (float) sbyte.MinValue, 128f);
      vector2.X /= num2;
      vector2.Y = (float) ((double) vector2.Y / (double) num2 - 0.5 * (double) num3 * (double) num2);
      vector2.X += Math.Min(4f, Math.Abs(((Entity) this.NPC).velocity.X)) * (float) Math.Sign(((Entity) this.NPC).velocity.X);
      SoundEngine.PlaySound(ref SoundID.Item1, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
      if (!FargoSoulsUtil.HostCheck)
        return;
      float num4 = (float) ((double) num2 + (double) Main.rand.Next(-10, 11) - 1.0);
      Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<TrojanSquirrelProj>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, num3, num4, 0.0f);
    }

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      if (this.NPC.life > 0)
        return;
      Vector2 center = ((Entity) this.NPC).Center;
      if (Main.dedServ)
        return;
      Gore.NewGore(((Entity) this.NPC).GetSource_FromThis((string) null), center, ((Entity) this.NPC).velocity, ModContent.Find<ModGore>(((ModType) this).Mod.Name, "TrojanSquirrelGore1").Type, this.NPC.scale);
    }
  }
}
