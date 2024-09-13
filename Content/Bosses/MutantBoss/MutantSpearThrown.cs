// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantSpearThrown
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantSpearThrown : MutantSpearAttack
  {
    protected float scaletimer;

    public virtual string Texture
    {
      get
      {
        return !FargoSoulsUtil.AprilFools ? "FargowiltasSouls/Content/Projectiles/BossWeapons/HentaiSpear" : "FargowiltasSouls/Content/Bosses/MutantBoss/MutantSpear_April";
      }
    }

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 10;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 16;
      ((Entity) this.Projectile).height = 16;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 180;
      this.Projectile.extraUpdates = 1;
      this.Projectile.alpha = 0;
      this.CooldownSlot = 1;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      if (((Rectangle) ref projHitbox).Intersects(targetHitbox))
        return new bool?(true);
      float num = 0.0f;
      Vector2 vector2_1 = Vector2.op_Multiply((float) (200.0 / 2.0) * this.Projectile.scale, Utils.ToRotationVector2(this.Projectile.rotation - MathHelper.ToRadians(135f)));
      Vector2 vector2_2 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, vector2_1);
      Vector2 vector2_3 = Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_1);
      return Collision.CheckAABBvLineCollision(Utils.TopLeft(targetHitbox), Utils.Size(targetHitbox), vector2_2, vector2_3, 8f * this.Projectile.scale, ref num) ? new bool?(true) : new bool?(false);
    }

    public virtual void OnSpawn(IEntitySource source)
    {
      if (!(source is EntitySource_Parent entitySourceParent) || !(entitySourceParent.Entity is NPC entity))
        return;
      this.npc = entity;
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write7BitEncodedInt(this.npc != null ? ((Entity) this.npc).whoAmI : -1);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.npc = FargoSoulsUtil.NPCExists(reader.Read7BitEncodedInt(), Array.Empty<int>());
    }

    public virtual void AI()
    {
      if ((double) --this.Projectile.localAI[0] < 0.0)
      {
        if (WorldSavingSystem.MasochistModeReal)
        {
          this.Projectile.localAI[0] = 3f;
          for (int index1 = -1; index1 <= 1; index1 += 2)
          {
            if (FargoSoulsUtil.HostCheck)
            {
              int index2 = Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.op_Multiply(8f, Utils.RotatedBy(Vector2.Normalize(((Entity) this.Projectile).velocity), 1.5707963705062866 * (double) index1, new Vector2())), ModContent.ProjectileType<MutantSphereSmall>(), this.Projectile.damage, 0.0f, this.Projectile.owner, -1f, 0.0f, 0.0f);
              if (index2 != Main.maxProjectiles)
                Main.projectile[index2].timeLeft = 15;
            }
          }
        }
        else
        {
          this.Projectile.localAI[0] = 4f;
          if ((double) this.Projectile.ai[1] == 0.0 && FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<MutantSphereSmall>(), this.Projectile.damage, 0.0f, this.Projectile.owner, this.Projectile.ai[0], 0.0f, 0.0f);
        }
      }
      if ((double) this.Projectile.localAI[1] == 0.0)
      {
        this.Projectile.localAI[1] = 1f;
        SoundEngine.PlaySound(ref SoundID.Item1, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + MathHelper.ToRadians(135f);
      ++this.scaletimer;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), Vector2.op_Addition(((Entity) target).Center, Utils.NextVector2Circular(Main.rand, 100f, 100f)), Vector2.Zero, ModContent.ProjectileType<MutantBombSmall>(), 0, 0.0f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
      if (WorldSavingSystem.EternityMode)
      {
        target.FargoSouls().MaxLifeReduction += 100;
        target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400, true, false);
        target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180, true, false);
      }
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 600, true, false);
      this.TryLifeSteal(((Entity) target).Center, ((Entity) target).whoAmI);
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      if (!Main.zenithWorld)
        return;
      this.TryLifeSteal(((Entity) target).Center, Main.myPlayer);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/MutantBoss/MutantEye_Glow", (AssetRequestMode) 1).Value;
      int num1 = texture2D.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color color1 = Color.Lerp(Color.Lerp(FargoSoulsUtil.AprilFools ? new Color((int) byte.MaxValue, 191, 51, 0) : new Color(51, (int) byte.MaxValue, 191, 0), Color.Transparent, 0.82f), Color.Lerp(FargoSoulsUtil.AprilFools ? new Color((int) byte.MaxValue, 242, 194, 0) : new Color(194, (int) byte.MaxValue, 242, 0), Color.Transparent, 0.6f), (float) (0.5 + Math.Sin((double) this.scaletimer / 7.0) / 2.0));
      Vector2 vector2_2 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitX), 28f));
      for (int index = 0; index < 3; ++index)
      {
        Vector2 vector2_3 = Vector2.op_Subtraction(Vector2.op_Addition(vector2_2, Utils.RotatedBy(Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitX), 20f), 0.62831854820251465 - (double) index * 3.1415927410125732 / 5.0, new Vector2())), Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitX), 20f));
        float num3 = this.Projectile.scale + (float) Math.Sin((double) this.scaletimer / 7.0) / 7f;
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(vector2_3, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color1, Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f, vector2_1, num3 * 1.25f, (SpriteEffects) 0, 0.0f);
      }
      for (int index = ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - 1; index > 0; --index)
      {
        Color color2 = Color.op_Multiply(color1, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        float num4 = this.Projectile.scale * (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] + (float) Math.Sin((double) this.scaletimer / 7.0) / 7f;
        Vector2 vector2_4 = Vector2.op_Subtraction(this.Projectile.oldPos[index], Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitX), 14f));
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(vector2_4, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color2, Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f, vector2_1, num4 * 1.25f, (SpriteEffects) 0, 0.0f);
      }
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
