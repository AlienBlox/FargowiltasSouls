// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantSphereRing
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
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
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantSphereRing : ModProjectile
  {
    protected bool DieOutsideArena;
    private int ritualID = -1;
    private float originalSpeed;
    private bool spawned;

    public virtual string Texture
    {
      get
      {
        return !FargoSoulsUtil.AprilFools ? "Terraria/Images/Projectile_454" : "FargowiltasSouls/Content/Bosses/MutantBoss/MutantSphere_April";
      }
    }

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 2;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 40;
      ((Entity) this.Projectile).height = 40;
      this.Projectile.hostile = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 480;
      this.Projectile.alpha = 200;
      this.CooldownSlot = 1;
      if (this.Projectile.type != ModContent.ProjectileType<MutantSphereRing>())
        return;
      this.DieOutsideArena = true;
      this.Projectile.FargoSouls().TimeFreezeImmune = WorldSavingSystem.MasochistModeReal && FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>()) && (double) Main.npc[EModeGlobalNPC.mutantBoss].ai[0] == -5.0;
    }

    public virtual bool CanHitPlayer(Player target)
    {
      return target.hurtCooldowns[1] == 0 || WorldSavingSystem.MasochistModeReal;
    }

    public virtual void AI()
    {
      if (!this.spawned)
      {
        this.spawned = true;
        this.originalSpeed = ((Vector2) ref ((Entity) this.Projectile).velocity).Length();
      }
      ((Entity) this.Projectile).velocity = Vector2.op_Multiply(this.originalSpeed, Utils.RotatedBy(Vector2.Normalize(((Entity) this.Projectile).velocity), (double) this.Projectile.ai[1] / (2.0 * Math.PI * (double) this.Projectile.ai[0] * (double) ++this.Projectile.localAI[0]), new Vector2()));
      if (this.Projectile.alpha > 0)
      {
        this.Projectile.alpha -= 20;
        if (this.Projectile.alpha < 0)
          this.Projectile.alpha = 0;
      }
      this.Projectile.scale = (float) (1.0 - (double) this.Projectile.alpha / (double) byte.MaxValue);
      if (++this.Projectile.frameCounter >= 6)
      {
        this.Projectile.frameCounter = 0;
        if (++this.Projectile.frame > 1)
          this.Projectile.frame = 0;
      }
      if (this.DieOutsideArena)
      {
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
        Projectile projectile = FargoSoulsUtil.ProjectileExists(this.ritualID, new int[1]
        {
          ModContent.ProjectileType<MutantRitual>()
        });
        if (projectile != null && (double) ((Entity) this.Projectile).Distance(((Entity) projectile).Center) > 1200.0)
          this.Projectile.timeLeft = 0;
      }
      this.TryTimeStop();
    }

    private void TryTimeStop()
    {
      if (!this.Projectile.hostile || this.Projectile.friendly || !((Entity) Main.LocalPlayer).active || Main.LocalPlayer.dead || Main.LocalPlayer.ghost || !FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>()) || !WorldSavingSystem.MasochistModeReal || (double) Main.npc[EModeGlobalNPC.mutantBoss].ai[0] != -5.0 || !this.Projectile.Colliding(((Entity) this.Projectile).Hitbox, Main.LocalPlayer.FargoSouls().GetPrecisionHurtbox()))
        return;
      if (!Main.LocalPlayer.HasBuff(ModContent.BuffType<TimeFrozenBuff>()))
      {
        SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/ZaWarudo", (SoundType) 0);
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) Main.LocalPlayer).Center), (SoundUpdateCallback) null);
      }
      Main.LocalPlayer.AddBuff(ModContent.BuffType<TimeFrozenBuff>(), 300, true, false);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>()) && WorldSavingSystem.EternityMode)
      {
        target.FargoSouls().MaxLifeReduction += 100;
        target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400, true, false);
        target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180, true, false);
      }
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 360, true, false);
      this.TryTimeStop();
    }

    public virtual void OnKill(int timeleft)
    {
      if (Utils.NextBool(Main.rand, Main.player[this.Projectile.owner].ownedProjectileCounts[this.Projectile.type] / 10 + 1))
        SoundEngine.PlaySound(ref SoundID.NPCDeath6, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
      ((Entity) this.Projectile).width = ((Entity) this.Projectile).height = 208;
      ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
      for (int index1 = 0; index1 < 2; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Main.dust[index2].position = Vector2.op_Addition(Vector2.op_Multiply(Utils.RotatedBy(new Vector2((float) (((Entity) this.Projectile).width / 2), 0.0f), 6.28318548202515 * Main.rand.NextDouble(), new Vector2()), (float) Main.rand.NextDouble()), ((Entity) this.Projectile).Center);
      }
      for (int index3 = 0; index3 < 4; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 0, new Color(), 2.5f);
        Main.dust[index4].position = Vector2.op_Addition(Vector2.op_Multiply(Utils.RotatedBy(new Vector2((float) (((Entity) this.Projectile).width / 2), 0.0f), 6.28318548202515 * Main.rand.NextDouble(), new Vector2()), (float) Main.rand.NextDouble()), ((Entity) this.Projectile).Center);
        Main.dust[index4].noGravity = true;
        Dust dust1 = Main.dust[index4];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 1f);
        int index5 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 229, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Main.dust[index5].position = Vector2.op_Addition(Vector2.op_Multiply(Utils.RotatedBy(new Vector2((float) (((Entity) this.Projectile).width / 2), 0.0f), 6.28318548202515 * Main.rand.NextDouble(), new Vector2()), (float) Main.rand.NextDouble()), ((Entity) this.Projectile).Center);
        Dust dust2 = Main.dust[index5];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 1f);
        Main.dust[index5].noGravity = true;
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/MutantBoss/MutantSphereGlow", (AssetRequestMode) 1).Value;
      int height = texture2D.Height;
      int num1 = 0;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num1, texture2D.Width, height);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color color1 = Color.op_Multiply(Color.Lerp(FargoSoulsUtil.AprilFools ? Color.Red : new Color(196, 247, (int) byte.MaxValue, 0), Color.Transparent, 0.9f), this.Projectile.Opacity);
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color2 = Color.op_Multiply(color1, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        float num2 = this.Projectile.scale * (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type];
        Vector2 vector2_2 = Vector2.op_Subtraction(this.Projectile.oldPos[index], Vector2.op_Multiply(Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), (float) index), 6f));
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(vector2_2, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color2, Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f, vector2_1, num2 * 1.5f, (SpriteEffects) 0, 0.0f);
      }
      Color color3 = Color.Lerp(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0), Color.Transparent, 0.85f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).position, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color3, Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f, vector2_1, this.Projectile.scale * 1.5f, (SpriteEffects) 0, 0.0f);
      return false;
    }

    public virtual void PostDraw(Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
    }
  }
}
