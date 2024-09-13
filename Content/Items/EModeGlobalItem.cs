// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.EModeGlobalItem
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items
{
  public class EModeGlobalItem : GlobalItem
  {
    public virtual void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
      base.ModifyTooltips(item, tooltips);
      if (!WorldSavingSystem.EternityMode)
        return;
      EmodeItemBalance.BalanceTooltips(item, ref tooltips);
      if (item.prefix < 62 || item.prefix > 65)
        return;
      int num = 5;
      foreach (TooltipLine tooltip in tooltips)
      {
        if (tooltip.Name == "PrefixAccDefense")
        {
          if (!Main.hardMode)
          {
            List<char> list = tooltip.Text.ToList<char>();
            --list[1];
            tooltip.Text = new string(list.ToArray());
          }
          TooltipLine tooltipLine = tooltip;
          tooltipLine.Text = tooltipLine.Text + "\n" + Language.GetTextValue("Mods.FargowiltasSouls.Items.Extra.DefensePrefixMaxLife", (object) num);
        }
      }
    }

    public virtual void PickAmmo(
      Item weapon,
      Item ammo,
      Player player,
      ref int type,
      ref float speed,
      ref StatModifier damage,
      ref float knockback)
    {
      int num = WorldSavingSystem.EternityMode ? 1 : 0;
    }

    public virtual void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
      base.UpdateAccessory(item, player, hideVisual);
      if (!WorldSavingSystem.EternityMode)
        return;
      if (item.prefix >= 62 && item.prefix <= 65)
      {
        if (!Main.hardMode)
        {
          Player player1 = player;
          player1.statDefense = Player.DefenseStat.op_Subtraction(player1.statDefense, 1);
        }
        player.statLifeMax2 += 5;
      }
      if (item.type != 208)
        return;
      player.FargoSouls().HasJungleRose = true;
    }

    public virtual void HoldItem(Item item, Player player)
    {
      if (!WorldSavingSystem.EternityMode)
      {
        base.HoldItem(item, player);
      }
      else
      {
        EModePlayer emodePlayer = player.Eternity();
        if (item.type == 390 || item.type == 484)
        {
          if (!player.ItemAnimationActive && emodePlayer.MythrilHalberdTimer < 121)
            ++emodePlayer.MythrilHalberdTimer;
          if (player.itemAnimation == 1)
            emodePlayer.MythrilHalberdTimer = 0;
          if (emodePlayer.MythrilHalberdTimer == 120 && ((Entity) player).whoAmI == Main.myPlayer)
          {
            SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/ChargeSound", (SoundType) 0);
            SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
          }
        }
        else
          emodePlayer.MythrilHalberdTimer = 0;
        base.HoldItem(item, player);
      }
    }

    public virtual bool CanUseItem(Item item, Player player)
    {
      if (!WorldSavingSystem.EternityMode)
      {
        if (item.type == 1192)
        {
          Item obj = new Item(1192, 1, 0);
          item.shoot = obj.shoot;
          item.shootSpeed = obj.shootSpeed;
          ((Entity) obj).active = false;
        }
        return base.CanUseItem(item, player);
      }
      EModePlayer emodePlayer = player.Eternity();
      if (item.damage <= 0 && (item.type == 1326 || item.type == 3620 || item.type == 3611 || item.type == 510 || item.type == 509 || item.type == 850 || item.type == 851 || item.type == 3625 || item.type == 3612 || item.type == 849))
      {
        if (!player.FargoSouls().LihzahrdCurse)
        {
          Tile tileSafely = Framing.GetTileSafely(Main.MouseWorld);
          if (((Tile) ref tileSafely).WallType != (ushort) 87 || player.buffImmune[ModContent.BuffType<LihzahrdCurseBuff>()])
            goto label_8;
        }
        return false;
      }
label_8:
      if (item.type == 1326 && Luminance.Common.Utilities.Utilities.AnyBosses())
        player.chaosState = true;
      if (item.type == 1192)
      {
        item.shoot = 221;
        item.shootSpeed = 5f;
      }
      if (item.type == 483)
        emodePlayer.CobaltHitCounter = 0;
      if (item.type == 5335 && Luminance.Common.Utilities.Utilities.AnyBosses())
      {
        player.hurtCooldowns[0] = 0;
        Player.DefenseStat statDefense = player.statDefense;
        float endurance = player.endurance;
        ref MultipliableFloat local = ref player.statDefense.FinalMultiplier;
        local = MultipliableFloat.op_Multiply(local, 0.0f);
        player.endurance = 0.0f;
        player.Hurt(PlayerDeathReason.ByCustomReason(Language.GetTextValue("Mods.FargowiltasSouls.DeathMessage.RodOfHarmony", (object) player.name)), player.statLifeMax2 / 7, 0, false, false, 0, false, 0.0f, 0.0f, 4.5f);
        player.statDefense = statDefense;
        player.endurance = endurance;
      }
      return base.CanUseItem(item, player);
    }

    public virtual bool? UseItem(Item item, Player player)
    {
      if (!WorldSavingSystem.EternityMode)
        return base.UseItem(item, player);
      player.Eternity();
      if (item.type == 5334 && Main.zenithWorld)
        Main.time = 18000.0;
      return base.UseItem(item, player);
    }

    public virtual void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
    {
      if (player.Eternity().MythrilHalberdTimer < 120 || item.type != 484)
        return;
      damage = StatModifier.op_Multiply(damage, 8f * player.FargoSouls().AttackSpeed);
    }

    public virtual bool Shoot(
      Item item,
      Player player,
      EntitySource_ItemUse_WithAmmo source,
      Vector2 position,
      Vector2 velocity,
      int type,
      int damage,
      float knockback)
    {
      if (!WorldSavingSystem.EternityMode)
        return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
      if (item.type != 1192)
        return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
      for (int index = 0; index < 2; ++index)
        Projectile.NewProjectile(player.GetSource_ItemUse(item, (string) null), position, Utils.RotatedByRandom(velocity, 0.22439947724342346), type, (int) ((double) damage * 0.75), knockback / 2f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      return false;
    }

    public virtual void OnHitNPC(
      Item item,
      Player player,
      NPC target,
      NPC.HitInfo hit,
      int damageDone)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      EModePlayer emodePlayer = player.Eternity();
      switch (item.type)
      {
        case 483:
          if (emodePlayer.CobaltHitCounter >= 2)
            break;
          Projectile projectile = FargoSoulsUtil.NewProjectileDirectSafe(((Entity) player).GetSource_OnHit((Entity) target, (string) null), Vector2.op_Addition(Vector2.op_Addition(((Entity) target).position, Vector2.op_Multiply(Vector2.UnitX, (float) Main.rand.Next(((Entity) target).width))), Vector2.op_Multiply(Vector2.UnitY, (float) Main.rand.Next(((Entity) target).height))), Vector2.Zero, ModContent.ProjectileType<CobaltExplosion>(), (int) ((double) ((NPC.HitInfo) ref hit).Damage * 0.40000000596046448), 0.0f, Main.myPlayer);
          if (projectile != null)
            projectile.FargoSouls().CanSplit = false;
          ++emodePlayer.CobaltHitCounter;
          break;
        case 1185:
          if (target.type == 488 || target.friendly)
            break;
          player.AddBuff(58, 300, true, false);
          break;
      }
    }

    public virtual void ModifyShootStats(
      Item item,
      Player player,
      ref Vector2 position,
      ref Vector2 velocity,
      ref int type,
      ref int damage,
      ref float knockback)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      if (!NPC.downedBoss3 && item.type == 165)
      {
        type = 358;
        damage = 0;
      }
      if (!NPC.downedBoss2 && item.type == (int) sbyte.MaxValue)
      {
        type = 178;
        damage = 0;
      }
      if (player.Eternity().MythrilHalberdTimer < 120 || item.type != 390)
        return;
      damage = (int) ((double) (damage * 8) * (double) player.FargoSouls().AttackSpeed);
      player.Eternity().MythrilHalberdTimer = 0;
    }
  }
}
