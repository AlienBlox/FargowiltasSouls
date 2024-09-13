// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantPillar
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantPillar : ModProjectile
  {
    private int target = -1;

    public virtual string Texture
    {
      get
      {
        return !FargoSoulsUtil.AprilFools ? "FargowiltasSouls/Content/Projectiles/Masomode/CelestialPillar" : "FargowiltasSouls/Content/Bosses/MutantBoss/MutantPillar_April";
      }
    }

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 4;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 10;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 120;
      ((Entity) this.Projectile).height = 120;
      this.Projectile.aiStyle = -1;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 600;
      this.CooldownSlot = 1;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
    }

    public virtual void SendExtraAI(BinaryWriter writer) => writer.Write(this.target);

    public virtual void ReceiveExtraAI(BinaryReader reader) => this.target = reader.ReadInt32();

    public virtual bool? CanDamage() => new bool?(this.Projectile.alpha == 0);

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
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
        for (int index = 0; index < 50; ++index)
        {
          Dust dust1 = Main.dust[Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, num2, 0.0f, 0.0f, 0, new Color(), 1f)];
          Dust dust2 = dust1;
          dust2.velocity = Vector2.op_Multiply(dust2.velocity, 10f);
          dust1.fadeIn = 1f;
          dust1.scale = (float) (1.0 + (double) Utils.NextFloat(Main.rand) + (double) Main.rand.Next(4) * 0.30000001192092896);
          if (!Utils.NextBool(Main.rand, 3))
          {
            dust1.noGravity = true;
            Dust dust3 = dust1;
            dust3.velocity = Vector2.op_Multiply(dust3.velocity, 3f);
            dust1.scale *= 2f;
          }
        }
      }
      if (this.Projectile.alpha > 0)
      {
        ((Entity) this.Projectile).velocity.Y += 0.0416666679f;
        this.Projectile.rotation += (float) ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() / 20.0 * 2.0);
        this.Projectile.localAI[1] += ((Entity) this.Projectile).velocity.Y;
        this.Projectile.alpha -= 2;
        if (this.Projectile.alpha <= 0)
        {
          this.Projectile.alpha = 0;
          if (this.target != -1)
          {
            SoundEngine.PlaySound(ref SoundID.Item89, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
            ((Entity) this.Projectile).velocity = Vector2.op_Subtraction(((Entity) Main.player[this.target]).Center, ((Entity) this.Projectile).Center);
            float num = ((Vector2) ref ((Entity) this.Projectile).velocity).Length();
            ((Vector2) ref ((Entity) this.Projectile).velocity).Normalize();
            Projectile projectile = this.Projectile;
            ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 32f);
            this.Projectile.timeLeft = (int) ((double) num / 32.0);
            this.Projectile.netUpdate = true;
            return;
          }
          this.Projectile.Kill();
        }
        else
        {
          NPC npc = Main.npc[(int) this.Projectile.ai[1]];
          this.target = npc.target;
          ((Entity) this.Projectile).Center = ((Entity) npc).Center;
          ((Entity) this.Projectile).position.Y += this.Projectile.localAI[1];
        }
        if (this.target >= 0 && ((Entity) Main.player[this.target]).active && !Main.player[this.target].dead)
        {
          if (this.Projectile.alpha < 100)
            this.Projectile.rotation = Utils.AngleLerp(this.Projectile.rotation, Utils.ToRotation(Vector2.op_Subtraction(((Entity) Main.player[this.target]).Center, ((Entity) this.Projectile).Center)), (float) ((double) ((int) byte.MaxValue - this.Projectile.alpha) / (double) byte.MaxValue * 0.079999998211860657));
        }
        else
        {
          int closest = (int) Player.FindClosest(((Entity) this.Projectile).Center, 0, 0);
          if (closest != -1)
          {
            this.target = closest;
            this.Projectile.netUpdate = true;
          }
        }
      }
      else
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
      this.Projectile.frame = (int) this.Projectile.ai[0];
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (target.mount.Active)
        target.mount.Dismount(target);
      ((Entity) target).velocity.X = (double) ((Entity) this.Projectile).velocity.X < 0.0 ? -15f : 15f;
      ((Entity) target).velocity.Y = -10f;
      target.AddBuff(ModContent.BuffType<StunnedBuff>(), 60, true, false);
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 600, true, false);
      if (WorldSavingSystem.EternityMode)
      {
        target.AddBuff(ModContent.BuffType<MarkedforDeathBuff>(), 240, true, false);
        target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180, true, false);
      }
      switch ((int) this.Projectile.ai[0])
      {
        case 0:
          target.AddBuff(ModContent.BuffType<ReverseManaFlowBuff>(), 360, true, false);
          break;
        case 1:
          target.AddBuff(ModContent.BuffType<AtrophiedBuff>(), 360, true, false);
          break;
        case 2:
          target.AddBuff(ModContent.BuffType<JammedBuff>(), 360, true, false);
          break;
        default:
          target.AddBuff(ModContent.BuffType<AntisocialBuff>(), 360, true, false);
          break;
      }
      this.Projectile.timeLeft = 0;
    }

    public virtual void OnKill(int timeLeft)
    {
      if (((Entity) Main.LocalPlayer).active && !Main.dedServ)
        ScreenShakeSystem.StartShake(10f, 6.28318548f, new Vector2?(), 0.333333343f);
      SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
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
      for (int index = 0; index < 80; ++index)
      {
        Dust dust1 = Main.dust[Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, num2, 0.0f, 0.0f, 0, new Color(), 1f)];
        Dust dust2 = dust1;
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 10f);
        dust1.fadeIn = 1f;
        dust1.scale = (float) (1.0 + (double) Utils.NextFloat(Main.rand) + (double) Main.rand.Next(4) * 0.30000001192092896);
        if (!Utils.NextBool(Main.rand, 3))
        {
          dust1.noGravity = true;
          Dust dust3 = dust1;
          dust3.velocity = Vector2.op_Multiply(dust3.velocity, 3f);
          dust1.scale *= 2f;
        }
      }
      if (!FargoSoulsUtil.HostCheck)
        return;
      int num3 = 240;
      if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>()) && (double) Main.npc[EModeGlobalNPC.mutantBoss].ai[0] == 19.0)
        num3 = (int) Main.npc[EModeGlobalNPC.mutantBoss].localAI[0];
      float num4 = WorldSavingSystem.MasochistModeReal ? 5.5f : 5f;
      for (int index1 = 0; index1 < 4; ++index1)
      {
        Vector2 vector2 = Utils.RotatedBy(new Vector2(0.0f, num4 * ((float) index1 + 0.5f)), (double) this.Projectile.rotation, new Vector2());
        for (int index2 = 0; index2 < 24; ++index2)
        {
          int index3 = Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Utils.RotatedBy(vector2, 0.2617993950843811 * (double) index2, new Vector2()), ModContent.ProjectileType<MutantFragment>(), this.Projectile.damage / 2, 0.0f, Main.myPlayer, this.Projectile.ai[0], 0.0f, 0.0f);
          if (index3 != Main.maxProjectiles)
            Main.projectile[index3].timeLeft = num3;
        }
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue - this.Projectile.alpha));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index += 3)
      {
        Color color = Color.op_Multiply(alpha, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
