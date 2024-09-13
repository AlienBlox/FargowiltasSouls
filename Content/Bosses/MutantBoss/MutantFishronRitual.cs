// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantFishronRitual
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantFishronRitual : ModProjectile
  {
    private const int safeRange = 150;

    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/Masomode/FishronRitual";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 320;
      ((Entity) this.Projectile).height = 320;
      this.Projectile.hostile = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 600;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.penetrate = -1;
      this.CooldownSlot = -1;
      this.Projectile.FargoSouls().GrazeCheck = (Func<Projectile, bool>) (projectile =>
      {
        bool? nullable = base.CanDamage();
        bool flag = true;
        if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
          return false;
        Vector2 vector2 = Vector2.op_Subtraction(((Entity) Main.LocalPlayer).Center, ((Entity) this.Projectile).Center);
        return (double) Math.Abs(((Vector2) ref vector2).Length() - 150f) < 42.0 + (double) Main.LocalPlayer.FargoSouls().GrazeRadius;
      });
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public virtual bool? CanDamage()
    {
      return new bool?((double) this.Projectile.alpha == 0.0 && Main.player[this.Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<MutantBomb>()] > 0);
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      Vector2 vector2 = Vector2.op_Subtraction(Utils.ToVector2(((Rectangle) ref projHitbox).Center), Utils.ToVector2(((Rectangle) ref targetHitbox).Center));
      if ((double) ((Vector2) ref vector2).Length() < 150.0)
        return new bool?(false);
      int num1 = ((Rectangle) ref projHitbox).Center.X - ((Rectangle) ref targetHitbox).Center.X;
      int num2 = ((Rectangle) ref projHitbox).Center.Y - ((Rectangle) ref targetHitbox).Center.Y;
      if (Math.Abs(num1) > targetHitbox.Width / 2)
        num1 = targetHitbox.Width / 2 * Math.Sign(num1);
      if (Math.Abs(num2) > targetHitbox.Height / 2)
        num2 = targetHitbox.Height / 2 * Math.Sign(num2);
      int num3 = ((Rectangle) ref projHitbox).Center.X - ((Rectangle) ref targetHitbox).Center.X - num1;
      int num4 = ((Rectangle) ref projHitbox).Center.Y - ((Rectangle) ref targetHitbox).Center.Y - num2;
      return new bool?(Math.Sqrt((double) (num3 * num3 + num4 * num4)) <= 1200.0);
    }

    public virtual void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>());
      if (npc != null && (double) npc.ai[0] == 34.0)
      {
        this.Projectile.alpha -= 7;
        this.Projectile.timeLeft = 300;
        ((Entity) this.Projectile).Center = ((Entity) npc).Center;
        ((Entity) this.Projectile).position.Y -= 100f;
      }
      else
        this.Projectile.alpha += 17;
      if (this.Projectile.alpha < 0)
        this.Projectile.alpha = 0;
      if (this.Projectile.alpha > (int) byte.MaxValue)
      {
        this.Projectile.alpha = (int) byte.MaxValue;
        this.Projectile.Kill();
      }
      else
      {
        this.Projectile.scale = (float) (1.0 - (double) this.Projectile.alpha / (double) byte.MaxValue);
        this.Projectile.rotation += (float) Math.PI / 70f;
        Lighting.AddLight(((Entity) this.Projectile).Center, 0.4f, 0.9f, 1.1f);
        if (this.Projectile.FargoSouls().GrazeCD <= 10)
          return;
        this.Projectile.FargoSouls().GrazeCD = 10;
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (WorldSavingSystem.EternityMode)
      {
        target.FargoSouls().MaxLifeReduction += 100;
        target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400, true, false);
        target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180, true, false);
      }
      target.AddBuff(ModContent.BuffType<MutantNibbleBuff>(), 900, true, false);
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 900, true, false);
    }

    public virtual Color? GetAlpha(Color lightColor) => new Color?(Color.White);
  }
}
