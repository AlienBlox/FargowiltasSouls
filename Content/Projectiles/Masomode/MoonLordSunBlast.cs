// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.MoonLordSunBlast
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.Champions.Cosmos;
using FargowiltasSouls.Content.Bosses.Champions.Earth;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class MoonLordSunBlast : EarthChainBlast
  {
    public override string Texture => "Terraria/Images/Projectile_687";

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override void SetDefaults()
    {
      base.SetDefaults();
      ((Entity) this.Projectile).width = 70;
      ((Entity) this.Projectile).height = 70;
      this.CooldownSlot = 1;
    }

    public override bool? CanDamage()
    {
      return new bool?(this.Projectile.frame == 3 || this.Projectile.frame == 4);
    }

    public override void AI()
    {
      if (Utils.HasNaNs(((Entity) this.Projectile).position))
      {
        this.Projectile.Kill();
      }
      else
      {
        if (++this.Projectile.frameCounter >= 2)
        {
          this.Projectile.frameCounter = 0;
          if (++this.Projectile.frame >= Main.projFrames[this.Projectile.type])
          {
            --this.Projectile.frame;
            this.Projectile.Kill();
            return;
          }
          if (this.Projectile.frame == 3)
            this.Projectile.FargoSouls().GrazeCD = 0;
        }
        if ((double) this.Projectile.localAI[1] == 0.0)
        {
          SoundEngine.PlaySound(ref SoundID.Item88, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
          this.Projectile.scale = Utils.NextFloat(Main.rand, 1.5f, 4f);
          this.Projectile.rotation = Utils.NextFloat(Main.rand, 6.28318548f);
          ((Entity) this.Projectile).width = (int) ((double) ((Entity) this.Projectile).width * (double) this.Projectile.scale);
          ((Entity) this.Projectile).height = (int) ((double) ((Entity) this.Projectile).height * (double) this.Projectile.scale);
          ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
        }
        if ((double) ++this.Projectile.localAI[1] != 6.0 || (double) this.Projectile.ai[1] <= 0.0 || !FargoSoulsUtil.HostCheck)
          return;
        --this.Projectile.ai[1];
        Vector2 rotationVector2 = Utils.ToRotationVector2(this.Projectile.ai[0]);
        float radians = MathHelper.ToRadians(15f);
        if ((double) this.Projectile.localAI[0] != 2.0)
        {
          float num = Math.Min(5f, this.Projectile.ai[1]);
          int index = Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), Vector2.op_Addition(((Entity) this.Projectile).Center, Utils.NextVector2Circular(Main.rand, 20f, 20f)), Vector2.Zero, this.Projectile.type, this.Projectile.damage, 0.0f, this.Projectile.owner, this.Projectile.ai[0], num, 0.0f);
          if (index != Main.maxProjectiles)
            Main.projectile[index].localAI[0] = 1f;
        }
        if ((double) this.Projectile.localAI[0] == 1.0)
          return;
        Vector2 vector2 = Vector2.op_Multiply((float) ((double) ((Entity) this.Projectile).width / (double) this.Projectile.scale * 10.0 / 7.0), Utils.RotatedBy(rotationVector2, (double) Utils.NextFloat(Main.rand, -radians, radians), new Vector2()));
        int index1 = Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), Vector2.op_Addition(((Entity) this.Projectile).Center, vector2), Vector2.Zero, this.Projectile.type, this.Projectile.damage, 0.0f, this.Projectile.owner, this.Projectile.ai[0], this.Projectile.ai[1], 0.0f);
        if (index1 == Main.maxProjectiles)
          return;
        Main.projectile[index1].localAI[0] = this.Projectile.localAI[0];
      }
    }

    public virtual void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
    {
      if (!NPC.AnyNPCs(ModContent.NPCType<CosmosChampion>()))
        return;
      ref AddableFloat local = ref modifiers.ScalingArmorPenetration;
      local = AddableFloat.op_Addition(local, 0.25f);
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(67, 120, true, false);
      target.AddBuff(24, 300, true, false);
      if (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>()))
        return;
      target.FargoSouls().MaxLifeReduction += 100;
      target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400, true, false);
      target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180, true, false);
    }

    public override Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 100), this.Projectile.Opacity));
    }
  }
}
