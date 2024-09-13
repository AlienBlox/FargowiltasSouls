// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Cosmos.CosmosNebulaBlaze
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Cosmos
{
  public class CosmosNebulaBlaze : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_634";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 4;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 12;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 20;
      ((Entity) this.Projectile).height = 20;
      this.Projectile.aiStyle = -1;
      this.Projectile.timeLeft = 360;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
      this.Projectile.extraUpdates = 2;
      this.Projectile.scale = 1.5f;
      this.CooldownSlot = 1;
      if (!WorldSavingSystem.MasochistModeReal)
        return;
      this.Projectile.FargoSouls().GrazeCheck = (Func<Projectile, bool>) (Projectile => false);
    }

    public virtual void AI()
    {
      if ((double) ++this.Projectile.localAI[1] < 135.0 && FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.championBoss, ModContent.NPCType<CosmosChampion>()) && Main.npc[EModeGlobalNPC.championBoss].HasValidTarget)
      {
        float rotation1 = Utils.ToRotation(((Entity) this.Projectile).velocity);
        float rotation2 = Utils.ToRotation(Vector2.op_Subtraction(((Entity) Main.player[Main.npc[EModeGlobalNPC.championBoss].target]).Center, ((Entity) this.Projectile).Center));
        ((Entity) this.Projectile).velocity = Utils.RotatedBy(new Vector2(((Vector2) ref ((Entity) this.Projectile).velocity).Length(), 0.0f), (double) Utils.AngleLerp(rotation1, rotation2, this.Projectile.ai[0]), new Vector2());
      }
      float num1 = 5f;
      Vector2 vector2_1;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_1).\u002Ector(8f, 10f);
      float num2 = 1.2f;
      Vector3 vector3 = new Vector3(0.7f, 0.1f, 0.5f);
      int num3 = 4 * this.Projectile.MaxUpdates;
      int num4 = Utils.SelectRandom<int>(Main.rand, new int[5]
      {
        242,
        73,
        72,
        71,
        (int) byte.MaxValue
      });
      int maxValue = (int) byte.MaxValue;
      if ((double) this.Projectile.ai[1] == 0.0)
      {
        this.Projectile.ai[1] = 1f;
        this.Projectile.localAI[0] = (float) -Main.rand.Next(48);
        SoundEngine.PlaySound(ref SoundID.Item34, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      }
      if ((double) this.Projectile.ai[1] >= 1.0 && (double) this.Projectile.ai[1] < (double) num1)
      {
        ++this.Projectile.ai[1];
        if ((double) this.Projectile.ai[1] == (double) num1)
          this.Projectile.ai[1] = 1f;
      }
      this.Projectile.alpha -= 40;
      if (this.Projectile.alpha < 0)
        this.Projectile.alpha = 0;
      this.Projectile.spriteDirection = ((Entity) this.Projectile).direction;
      ++this.Projectile.frameCounter;
      if (this.Projectile.frameCounter >= num3)
      {
        ++this.Projectile.frame;
        this.Projectile.frameCounter = 0;
        if (this.Projectile.frame >= 4)
          this.Projectile.frame = 0;
      }
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
      ++this.Projectile.localAI[0];
      if ((double) this.Projectile.localAI[0] == 48.0)
        this.Projectile.localAI[0] = 0.0f;
      else if (this.Projectile.alpha == 0 && Utils.NextBool(Main.rand, 3))
      {
        Vector2.op_Multiply(Vector2.UnitX, -30f);
        Vector2 vector2_2 = Vector2.op_Subtraction(Vector2.op_Multiply(Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, (double) this.Projectile.localAI[0] * 0.130899697542191 + 3.14159274101257, new Vector2())), vector2_1), Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), 10f));
        int index = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, maxValue, 0.0f, 0.0f, 160, new Color(), 1f);
        Main.dust[index].scale = num2;
        Main.dust[index].noGravity = true;
        Main.dust[index].position = Vector2.op_Addition(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_2), Vector2.op_Multiply(((Entity) this.Projectile).velocity, 2f));
        Main.dust[index].velocity = Vector2.op_Addition(Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 2f), 8f)), Main.dust[index].position)), 2f), Vector2.op_Multiply(((Entity) this.Projectile).velocity, 2f));
        Dust dust = Main.dust[index];
        dust.velocity = Vector2.op_Multiply(dust.velocity, (float) (this.Projectile.MaxUpdates / 3));
      }
      if (!Utils.NextBool(Main.rand, 12))
        return;
      Vector2 vector2_3 = Vector2.op_UnaryNegation(Utils.RotatedBy(Utils.RotatedByRandom(Vector2.UnitX, 0.785398185253143), (double) Utils.ToRotation(((Entity) this.Projectile).velocity), new Vector2()));
      int index1 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, num4, 0.0f, 0.0f, 0, new Color(), 1.2f);
      Dust dust1 = Main.dust[index1];
      dust1.velocity = Vector2.op_Multiply(dust1.velocity, 0.3f);
      Dust dust2 = Main.dust[index1];
      dust2.velocity = Vector2.op_Multiply(dust2.velocity, (float) (this.Projectile.MaxUpdates / 3));
      Main.dust[index1].noGravity = true;
      Main.dust[index1].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(vector2_3, (float) ((Entity) this.Projectile).width), 2f));
      if (!Utils.NextBool(Main.rand))
        return;
      Main.dust[index1].fadeIn = 1.4f;
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
    }

    public virtual void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
    {
      if (!NPC.AnyNPCs(ModContent.NPCType<CosmosChampion>()))
        return;
      ref AddableFloat local = ref modifiers.ScalingArmorPenetration;
      local = AddableFloat.op_Addition(local, 0.25f);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<BerserkedBuff>(), 300, true, false);
      target.AddBuff(ModContent.BuffType<LethargicBuff>(), 300, true, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
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
      this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index += 2)
      {
        Color color = Color.op_Multiply(Color.HotPink, this.Projectile.Opacity);
        ((Color) ref color).A = (byte) 0;
        color = Color.op_Multiply(color, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
