// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantFragment
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantFragment : ModProjectile
  {
    private int ritualID = -1;

    public virtual string Texture
    {
      get
      {
        return !FargoSoulsUtil.AprilFools ? "FargowiltasSouls/Content/Projectiles/Masomode/CelestialFragment" : "FargowiltasSouls/Content/Bosses/MutantBoss/MutantFragment_April";
      }
    }

    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 4;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 8;
      ((Entity) this.Projectile).height = 8;
      this.Projectile.aiStyle = -1;
      this.Projectile.scale = 1.25f;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 600;
      this.CooldownSlot = 1;
    }

    public virtual void AI()
    {
      Projectile projectile1 = this.Projectile;
      ((Entity) projectile1).velocity = Vector2.op_Multiply(((Entity) projectile1).velocity, 0.985f);
      this.Projectile.rotation += ((Entity) this.Projectile).velocity.X / 30f;
      this.Projectile.frame = (int) this.Projectile.ai[0];
      if (Utils.NextBool(Main.rand, 15))
      {
        int num1;
        switch ((int) this.Projectile.ai[0])
        {
          case 0:
            num1 = 242;
            break;
          case 1:
            num1 = (int) sbyte.MaxValue;
            break;
          case 2:
            num1 = 229;
            break;
          default:
            num1 = 135;
            break;
        }
        int num2 = num1;
        Dust dust = Main.dust[Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, num2, 0.0f, 0.0f, 0, new Color(), 1f)];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
        dust.fadeIn = 1f;
        dust.scale = (float) (1.0 + (double) Utils.NextFloat(Main.rand) + (double) Main.rand.Next(4) * 0.30000001192092896);
        dust.noGravity = true;
      }
      if (this.ritualID == -1)
      {
        this.ritualID = -2;
        for (int index = 0; index < Main.maxProjectiles; ++index)
        {
          if (((Entity) Main.projectile[index]).active && Main.projectile[index].type == ModContent.ProjectileType<MutantRitual>())
          {
            this.ritualID = index;
            break;
          }
        }
      }
      Projectile projectile2 = FargoSoulsUtil.ProjectileExists(this.ritualID, new int[1]
      {
        ModContent.ProjectileType<MutantRitual>()
      });
      if (projectile2 == null || (double) ((Entity) this.Projectile).Distance(((Entity) projectile2).Center) <= 1200.0)
        return;
      this.Projectile.timeLeft = 0;
    }

    public virtual void OnKill(int timeLeft)
    {
      int num1;
      switch ((int) this.Projectile.ai[0])
      {
        case 0:
          num1 = 242;
          break;
        case 1:
          num1 = (int) sbyte.MaxValue;
          break;
        case 2:
          num1 = 229;
          break;
        default:
          num1 = 135;
          break;
      }
      int num2 = num1;
      for (int index = 0; index < 20; ++index)
      {
        Dust dust = Main.dust[Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, num2, 0.0f, 0.0f, 0, new Color(), 1f)];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 6f);
        dust.fadeIn = 1f;
        dust.scale = (float) (1.0 + (double) Utils.NextFloat(Main.rand) + (double) Main.rand.Next(4) * 0.30000001192092896);
        dust.noGravity = true;
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(ModContent.BuffType<HexedBuff>(), 120, true, false);
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 360, true, false);
      if (WorldSavingSystem.EternityMode)
        target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180, true, false);
      switch ((int) this.Projectile.ai[0])
      {
        case 0:
          target.AddBuff(ModContent.BuffType<ReverseManaFlowBuff>(), 180, true, false);
          break;
        case 1:
          target.AddBuff(ModContent.BuffType<AtrophiedBuff>(), 180, true, false);
          break;
        case 2:
          target.AddBuff(ModContent.BuffType<JammedBuff>(), 180, true, false);
          break;
        default:
          target.AddBuff(ModContent.BuffType<AntisocialBuff>(), 180, true, false);
          break;
      }
    }

    public virtual Color? GetAlpha(Color lightColor) => new Color?(Color.White);

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition);
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2_2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      Main.EntitySpriteDraw(texture2D, vector2_1, new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2_2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
