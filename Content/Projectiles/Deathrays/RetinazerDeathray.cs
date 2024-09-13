// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Deathrays.RetinazerDeathray
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.VanillaEternity;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Deathrays
{
  public class RetinazerDeathray : BaseDeathray
  {
    public RetinazerDeathray()
      : base(240f, sheeting: BaseDeathray.TextureSheeting.Vertical)
    {
    }

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      Main.projFrames[this.Projectile.type] = 4;
    }

    public virtual void AI()
    {
      Vector2? nullable = new Vector2?();
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], 125);
      if (npc != null)
      {
        Vector2 vector2_1 = Utils.RotatedBy(new Vector2((float) (((Entity) npc).width - 24), 0.0f), (double) npc.rotation + 1.57079633, new Vector2());
        ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) npc).Center, vector2_1);
        if (npc.GetGlobalNPC<Retinazer>().DeathrayState >= 3 && (double) this.Projectile.localAI[0] < (double) this.maxTime - 30.0)
          this.Projectile.localAI[0] = this.maxTime - 30f;
        if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
          ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
        if ((double) this.Projectile.localAI[0] == 0.0 && !Main.dedServ)
        {
          SoundStyle soundStyle;
          // ISSUE: explicit constructor call
          ((SoundStyle) ref soundStyle).\u002Ector("FargowiltasSouls/Assets/Sounds/RetinazerDeathray", (SoundType) 0);
          ((SoundStyle) ref soundStyle).Volume = 1.5f;
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        }
        float num1 = 1f;
        if (WorldSavingSystem.MasochistModeReal)
        {
          num1 = Utils.NextFloat(Main.rand, 2.5f, 5f);
          FargoSoulsUtil.ScreenshakeRumble(6f);
        }
        ++this.Projectile.localAI[0];
        if ((double) this.Projectile.localAI[0] >= (double) this.maxTime)
        {
          this.Projectile.Kill();
        }
        else
        {
          this.Projectile.scale = (float) Math.Sin((double) this.Projectile.localAI[0] * 3.1415927410125732 / (double) this.maxTime) * 10f * num1;
          if ((double) this.Projectile.scale > (double) num1)
            this.Projectile.scale = num1;
          float rotation = npc.rotation;
          this.Projectile.rotation = rotation;
          ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(rotation + 1.57079637f);
          float length = 3f;
          float width = (float) ((Entity) this.Projectile).width;
          Vector2 center = ((Entity) this.Projectile).Center;
          if (nullable.HasValue)
            center = nullable.Value;
          float[] numArray = new float[(int) length];
          Collision.LaserScan(center, ((Entity) this.Projectile).velocity, width * this.Projectile.scale, 2400f, numArray);
          float num2 = 0.0f;
          for (int index = 0; index < numArray.Length; ++index)
            num2 += numArray[index];
          this.Projectile.localAI[1] = MathHelper.Lerp(this.Projectile.localAI[1], num2 / length, 0.5f);
          Vector2 vector2_2 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, this.Projectile.localAI[1] - 14f));
          for (int index1 = 0; index1 < 2; ++index1)
          {
            float num3 = Utils.ToRotation(((Entity) this.Projectile).velocity) + (float) ((Utils.NextBool(Main.rand, 2) ? -1.0 : 1.0) * 1.5707963705062866);
            float num4 = (float) (Main.rand.NextDouble() * 2.0 + 2.0);
            Vector2 vector2_3;
            // ISSUE: explicit constructor call
            ((Vector2) ref vector2_3).\u002Ector((float) Math.Cos((double) num3) * num4, (float) Math.Sin((double) num3) * num4);
            int index2 = Dust.NewDust(vector2_2, 0, 0, 244, vector2_3.X, vector2_3.Y, 0, new Color(), 1f);
            Main.dust[index2].noGravity = true;
            Main.dust[index2].scale = 1.7f;
          }
          if (Utils.NextBool(Main.rand, 5))
          {
            Vector2 vector2_4 = Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(((Entity) this.Projectile).velocity, 1.5707963705062866, new Vector2()), (float) Main.rand.NextDouble() - 0.5f), (float) ((Entity) this.Projectile).width);
            int index = Dust.NewDust(Vector2.op_Subtraction(Vector2.op_Addition(vector2_2, vector2_4), Vector2.op_Multiply(Vector2.One, 4f)), 8, 8, 244, 0.0f, 0.0f, 100, new Color(), 1.5f);
            Dust dust = Main.dust[index];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 0.5f);
            Main.dust[index].velocity.Y = -Math.Abs(Main.dust[index].velocity.Y);
          }
          if (++this.Projectile.frameCounter <= 2)
            return;
          this.Projectile.frameCounter = 0;
          if (++this.Projectile.frame < Main.projFrames[this.Projectile.type])
            return;
          this.Projectile.frame = 0;
        }
      }
      else
        this.Projectile.Kill();
    }

    public override Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, 100, 100, 100), 0.95f));
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(67, 300, true, false);
      target.AddBuff(24, 300, true, false);
      target.AddBuff(69, 300, true, false);
    }
  }
}
