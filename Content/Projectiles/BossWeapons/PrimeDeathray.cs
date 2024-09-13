// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.PrimeDeathray
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Deathrays;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class PrimeDeathray : BaseDeathray
  {
    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Projectiles/Deathrays/PhantasmalDeathray";
    }

    public PrimeDeathray()
      : base(90f)
    {
    }

    public float Spinup => this.Projectile.localAI[0] / 2.25f;

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.CooldownSlot = -1;
      this.Projectile.hostile = false;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Magic;
      this.Projectile.FargoSouls().CanSplit = false;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
      this.Projectile.hide = true;
    }

    public virtual void DrawBehind(
      int index,
      List<int> behindNPCsAndTiles,
      List<int> behindNPCs,
      List<int> behindProjectiles,
      List<int> overPlayers,
      List<int> overWiresUI)
    {
      behindProjectiles.Add(index);
    }

    public virtual void AI()
    {
      Vector2? nullable = new Vector2?();
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      if (FargoSoulsUtil.GetProjectileByIdentity(this.Projectile.owner, (int) this.Projectile.ai[1], new int[1]
      {
        ModContent.ProjectileType<RefractorBlaster2Held>()
      }) != -1)
      {
        Player player = Main.player[this.Projectile.owner];
        this.Projectile.damage = player.GetWeaponDamage(player.HeldItem, false);
        this.Projectile.CritChance = player.GetWeaponCrit(player.HeldItem);
        this.Projectile.knockBack = player.GetWeaponKnockback(player.HeldItem, player.HeldItem.knockBack);
        ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(player.itemRotation + (((Entity) player).direction < 0 ? 3.14159274f : 0.0f));
        ((Entity) this.Projectile).Center = Vector2.op_Addition(player.MountedCenter, Vector2.op_Multiply(87f, ((Entity) this.Projectile).velocity));
        ++this.Projectile.timeLeft;
        float num1 = (double) this.Projectile.ai[0] > 0.0 ? 1f : -1f;
        Vector2 vector2 = Utils.RotatedBy(((Entity) this.Projectile).velocity, (double) num1 * 3.1415927410125732 / 6.0, new Vector2());
        float num2 = Math.Min(1f, this.Spinup);
        float num3 = (float) ((double) num2 * 1.5 * 6.0);
        ((Entity) this.Projectile).velocity = Utils.RotatedBy(vector2, Math.Sin((double) this.Projectile.localAI[0] * (double) num3 + (double) Math.Abs(this.Projectile.ai[0]) / 6.0 * 6.2831854820251465) * (double) num1 * 3.1415927410125732 / 14.0 * (double) num2, new Vector2());
      }
      else if ((double) this.Projectile.localAI[0] > 0.05000000074505806)
      {
        this.Projectile.Kill();
        return;
      }
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      float num4 = 0.2f;
      this.Projectile.localAI[0] += 0.01f;
      this.Projectile.scale = Math.Min(this.Projectile.localAI[0], num4);
      float length = 3f;
      float width = (float) ((Entity) this.Projectile).width;
      Vector2 center = ((Entity) this.Projectile).Center;
      if (nullable.HasValue)
        center = nullable.Value;
      float[] numArray = new float[(int) length];
      Collision.LaserScan(center, ((Entity) this.Projectile).velocity, width * this.Projectile.scale, 2000f, numArray);
      float num5 = 0.0f;
      for (int index = 0; index < numArray.Length; ++index)
        num5 += numArray[index];
      this.Projectile.localAI[1] = MathHelper.Lerp(this.Projectile.localAI[1], num5 / length, 0.5f);
      Projectile projectile = this.Projectile;
      ((Entity) projectile).position = Vector2.op_Subtraction(((Entity) projectile).position, ((Entity) this.Projectile).velocity);
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) - 1.57079637f;
      DelegateMethods.v3_1 = new Vector3(0.8f, 0.0f, 0.0f);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      Utils.PlotTileLine(((Entity) this.Projectile).Center, Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, this.Projectile.localAI[1])), 10f, PrimeDeathray.\u003C\u003EO.\u003C0\u003E__CastLight ?? (PrimeDeathray.\u003C\u003EO.\u003C0\u003E__CastLight = new Utils.TileActionAttempt((object) null, __methodptr(CastLight))));
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.immune[this.Projectile.owner] = 6;
      target.AddBuff(24, 600, false);
    }

    public virtual void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Multiply(local, Math.Min(1f, (float) (0.10000000149011612 + 0.89999997615814209 * (double) this.Spinup)));
    }

    public virtual void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
    {
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Multiply(local, Math.Min(1f, (float) (0.10000000149011612 + 0.89999997615814209 * (double) this.Spinup)));
    }

    public override Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.Lerp(Color.Transparent, Color.op_Multiply(new Color((int) byte.MaxValue, 0, 0, 0), 0.95f), Math.Min(1f, this.Spinup)));
    }
  }
}
