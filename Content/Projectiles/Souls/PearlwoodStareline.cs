// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Souls.PearlwoodStareline
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Souls
{
  public class PearlwoodStareline : GlobalProjectile
  {
    public bool Pearlwood;

    public virtual bool AppliesToEntity(Projectile entity, bool lateInstantiation)
    {
      return entity.type == 931;
    }

    public virtual bool InstancePerEntity => true;

    public virtual void OnSpawn(Projectile projectile, IEntitySource source)
    {
      Player player = Main.player[projectile.owner];
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (player == null || !((Entity) player).active || player.dead || !fargoSoulsPlayer.PearlwoodStar || !player.HasEffect<PearlwoodEffect>())
        return;
      int[] source1 = new int[3]
      {
        ModContent.ItemType<PearlwoodEnchant>(),
        ModContent.ItemType<TimberForce>(),
        ModContent.ItemType<TerrariaSoul>()
      };
      if (!(source is EntitySource_ItemUse entitySourceItemUse) || !((IEnumerable<int>) source1).Contains<int>(entitySourceItemUse.Item.type))
        return;
      SoundEngine.PlaySound(ref SoundID.Item84, new Vector2?(((Entity) projectile).Center), (SoundUpdateCallback) null);
      this.Pearlwood = true;
      projectile.hostile = true;
      projectile.friendly = false;
      projectile.penetrate = 1;
      projectile.timeLeft = 22;
      projectile.tileCollide = false;
    }

    public virtual void AI(Projectile projectile)
    {
      if (!this.Pearlwood)
      {
        base.AI(projectile);
      }
      else
      {
        base.AI(projectile);
        Player player = Main.player[projectile.owner];
        if (player == null || !((Entity) player).active || player.dead)
        {
          projectile.Kill();
        }
        else
        {
          FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
          if (!fargoSoulsPlayer.PearlwoodStar || !player.HasEffect<PearlwoodEffect>())
          {
            projectile.Kill();
          }
          else
          {
            bool flag = fargoSoulsPlayer.ForceEffect<PearlwoodEnchant>();
            projectile.friendly = flag;
            projectile.timeLeft = 22;
            projectile.damage = 1000;
            ((Entity) projectile).velocity = Vector2.op_Subtraction(fargoSoulsPlayer.PStarelinePos, ((Entity) projectile).Center);
          }
        }
      }
    }

    public virtual bool? CanHitNPC(Projectile projectile, NPC target)
    {
      return this.Pearlwood && target.type == 488 ? new bool?(false) : base.CanHitNPC(projectile, target);
    }

    public virtual void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
    {
      if (!this.Pearlwood)
      {
        base.OnHitPlayer(projectile, target, info);
      }
      else
      {
        SoundEngine.PlaySound(ref SoundID.Item25, new Vector2?(((Entity) target).Center), (SoundUpdateCallback) null);
        projectile.Kill();
      }
    }

    public virtual void ModifyHitPlayer(
      Projectile projectile,
      Player target,
      ref Player.HurtModifiers modifiers)
    {
      if (!this.Pearlwood)
      {
        base.ModifyHitPlayer(projectile, target, ref modifiers);
      }
      else
      {
        ((Player.HurtModifiers) ref modifiers).SetMaxDamage((int) ((double) target.statLifeMax2 * 0.2));
        ref AddableFloat local = ref modifiers.ScalingArmorPenetration;
        local = AddableFloat.op_Addition(local, 1f);
      }
    }

    public virtual void OnKill(Projectile projectile, int timeLeft)
    {
      if (!this.Pearlwood)
      {
        base.OnKill(projectile, timeLeft);
      }
      else
      {
        FargoSoulsPlayer fargoSoulsPlayer = Main.player[projectile.owner].FargoSouls();
        for (int index = 0; index < 20; ++index)
          Dust.NewDust(fargoSoulsPlayer.PStarelinePos, 22, 22, 228, 0.0f, 0.0f, 175, new Color(), 1.75f);
      }
    }
  }
}
