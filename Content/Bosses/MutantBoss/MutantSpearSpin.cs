// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantSpearSpin
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
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantSpearSpin : ModProjectile
  {
    private bool predictive;
    private int direction = 1;

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
      ((Entity) this.Projectile).width = 152;
      ((Entity) this.Projectile).height = 152;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.alpha = 0;
      this.CooldownSlot = 1;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[1] == 0.0)
      {
        this.Projectile.localAI[1] = Utils.NextBool(Main.rand) ? -1f : 1f;
        this.Projectile.timeLeft = (int) this.Projectile.ai[1];
      }
      NPC npc = Main.npc[(int) this.Projectile.ai[0]];
      if (((Entity) npc).active && npc.type == ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>())
      {
        ((Entity) this.Projectile).Center = ((Entity) npc).Center;
        this.direction = ((Entity) npc).direction;
        if ((double) npc.ai[0] == 4.0 || (double) npc.ai[0] == 13.0 || (double) npc.ai[0] == 21.0)
        {
          this.Projectile.rotation += 0.4586267f * this.Projectile.localAI[1];
          if ((double) ++this.Projectile.localAI[0] > 8.0)
          {
            this.Projectile.localAI[0] = 0.0f;
            if (FargoSoulsUtil.HostCheck && (double) ((Entity) this.Projectile).Distance(((Entity) Main.player[npc.target]).Center) > 360.0)
            {
              Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitY, Math.PI / 2.0), Utils.NextFloat(Main.rand, 6f, 9f));
              if ((double) ((Entity) npc).Center.Y < (double) ((Entity) Main.player[npc.target]).Center.Y)
                vector2 = Vector2.op_Multiply(vector2, -1f);
              float num = (float) (this.Projectile.timeLeft + Main.rand.Next(this.Projectile.timeLeft / 2));
              Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), Vector2.op_Addition(((Entity) this.Projectile).position, Utils.NextVector2Square(Main.rand, 0.0f, (float) ((Entity) this.Projectile).width)), vector2, ModContent.ProjectileType<MutantEyeHoming>(), this.Projectile.damage, 0.0f, this.Projectile.owner, (float) npc.target, num, 0.0f);
            }
          }
          if (this.Projectile.timeLeft % 20 == 0)
            SoundEngine.PlaySound(ref SoundID.Item1, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          if ((double) npc.ai[0] == 13.0)
            this.predictive = true;
          this.Projectile.alpha = 0;
        }
        else
          this.Projectile.alpha = (int) byte.MaxValue;
      }
      else
        this.Projectile.Kill();
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
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D1 = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D1.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.5f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      if ((double) this.Projectile.ai[1] > 0.0)
      {
        Texture2D texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/MutantBoss/MutantSpearAimGlow", (AssetRequestMode) 1).Value;
        float num4 = (float) this.Projectile.timeLeft / this.Projectile.ai[1];
        Color color1 = FargoSoulsUtil.AprilFools ? new Color((int) byte.MaxValue, 191, 51, 210) : new Color(51, (int) byte.MaxValue, 191, 210);
        if (this.predictive)
          color1 = FargoSoulsUtil.AprilFools ? new Color((int) byte.MaxValue, 0, 0, 210) : new Color(0, 0, (int) byte.MaxValue, 210);
        Color color2 = Color.op_Multiply(color1, 1f - num4);
        float num5 = this.Projectile.scale * 8f * num4;
        Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(texture2D2.Bounds), color2, 0.0f, Vector2.op_Division(Utils.Size(texture2D2.Bounds), 2f), num5, (SpriteEffects) 0, 0.0f);
      }
      return false;
    }
  }
}
