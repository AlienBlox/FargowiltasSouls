// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.FargoExtensionMethods
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls
{
  public static class FargoExtensionMethods
  {
    private static readonly FieldInfo _damageFieldHitInfo = typeof (NPC.HitInfo).GetField("_damage", (BindingFlags) 36);
    private static readonly FieldInfo _damageFieldHurtInfo = typeof (Player.HurtInfo).GetField("_damage", (BindingFlags) 36);

    public static TooltipLine ArticlePrefixAdjustment(
      this TooltipLine itemName,
      string[] localizationArticles)
    {
      List<string> list = ((IEnumerable<string>) itemName.Text.Split(' ', StringSplitOptions.None)).ToList<string>();
      for (int index = 0; index < localizationArticles.Length; ++index)
      {
        if (list.Remove(localizationArticles[index]))
        {
          list.Insert(0, localizationArticles[index]);
          break;
        }
      }
      itemName.Text = string.Join(" ", (IEnumerable<string>) list);
      return itemName;
    }

    public static bool TryFindTooltipLine(
      this List<TooltipLine> tooltips,
      string tooltipName,
      out TooltipLine tooltipLine)
    {
      tooltips.TryFindTooltipLine(tooltipName, "Terraria", out tooltipLine);
      return tooltipLine != null;
    }

    public static bool TryFindTooltipLine(
      this List<TooltipLine> tooltips,
      string tooltipName,
      string tooltipMod,
      out TooltipLine tooltipLine)
    {
      tooltipLine = tooltips.First<TooltipLine>((Func<TooltipLine, bool>) (line => line.Name == tooltipName && line.Mod == tooltipMod));
      return tooltipLine != null;
    }

    public static void Null(ref this NPC.HitInfo hitInfo)
    {
      object obj = (object) hitInfo;
      FargoExtensionMethods._damageFieldHitInfo.SetValue(obj, (object) 0);
      hitInfo = (NPC.HitInfo) obj;
      hitInfo.Knockback = 0.0f;
      hitInfo.Crit = false;
      hitInfo.InstantKill = false;
    }

    public static void Null(ref this NPC.HitModifiers hitModifiers)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      ((NPC.HitModifiers) ref hitModifiers).ModifyHitInfo += FargoExtensionMethods.\u003C\u003Ec.\u003C\u003E9__5_0 ?? (FargoExtensionMethods.\u003C\u003Ec.\u003C\u003E9__5_0 = new NPC.HitModifiers.HitInfoModifier((object) FargoExtensionMethods.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CNull\u003Eb__5_0)));
    }

    public static void Null(ref this Player.HurtInfo hurtInfo)
    {
      object obj = (object) hurtInfo;
      FargoExtensionMethods._damageFieldHurtInfo.SetValue(obj, (object) 0);
      hurtInfo = (Player.HurtInfo) obj;
      hurtInfo.Knockback = 0.0f;
    }

    public static void Null(ref this Player.HurtModifiers hurtModifiers)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      ((Player.HurtModifiers) ref hurtModifiers).ModifyHurtInfo += FargoExtensionMethods.\u003C\u003Ec.\u003C\u003E9__8_0 ?? (FargoExtensionMethods.\u003C\u003Ec.\u003C\u003E9__8_0 = new Player.HurtModifiers.HurtInfoModifier((object) FargoExtensionMethods.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CNull\u003Eb__8_0)));
    }

    public static void AddDebuffImmunities(this NPC npc, List<int> debuffs)
    {
      foreach (int debuff in debuffs)
        NPCID.Sets.SpecificDebuffImmunity[npc.type][debuff] = new bool?(true);
    }

    public static FargoSoulsGlobalNPC FargoSouls(this NPC npc)
    {
      return npc.GetGlobalNPC<FargoSoulsGlobalNPC>();
    }

    public static EModeGlobalNPC Eternity(this NPC npc) => npc.GetGlobalNPC<EModeGlobalNPC>();

    public static FargoSoulsGlobalProjectile FargoSouls(this Projectile projectile)
    {
      return projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>();
    }

    public static EModeGlobalProjectile Eternity(this Projectile projectile)
    {
      return projectile.GetGlobalProjectile<EModeGlobalProjectile>();
    }

    public static FargoSoulsPlayer FargoSouls(this Player player)
    {
      return player.GetModPlayer<FargoSoulsPlayer>();
    }

    public static EModePlayer Eternity(this Player player) => player.GetModPlayer<EModePlayer>();

    public static AccessoryEffectPlayer AccessoryEffects(this Player player)
    {
      return player.GetModPlayer<AccessoryEffectPlayer>();
    }

    public static bool ForceEffect<T>(this Player player) where T : AccessoryEffect
    {
      Item obj = player.EffectItem<T>();
      return obj != null && obj.ModItem != null && player.FargoSouls().ForceEffect(obj.ModItem);
    }

    public static bool Alive(this Player player)
    {
      return player != null && ((Entity) player).active && !player.dead && !player.ghost;
    }

    public static bool Alive(this Projectile projectile)
    {
      return projectile != null && ((Entity) projectile).active;
    }

    public static bool Alive(this NPC npc) => npc != null && ((Entity) npc).active;

    public static bool TypeAlive(this Projectile projectile, int type)
    {
      return projectile.Alive() && projectile.type == type;
    }

    public static bool TypeAlive<T>(this Projectile projectile) where T : ModProjectile
    {
      return projectile.Alive() && projectile.type == ModContent.ProjectileType<T>();
    }

    public static bool TypeAlive(this NPC npc, int type) => npc.Alive() && npc.type == type;

    public static bool TypeAlive<T>(this NPC npc) where T : ModNPC
    {
      return npc.Alive() && npc.type == ModContent.NPCType<T>();
    }

    public static NPC GetSourceNPC(this Projectile projectile)
    {
      return projectile.GetGlobalProjectile<A_SourceNPCGlobalProjectile>().sourceNPC;
    }

    public static void SetSourceNPC(this Projectile projectile, NPC npc)
    {
      projectile.GetGlobalProjectile<A_SourceNPCGlobalProjectile>().sourceNPC = npc;
    }

    public static float ActualClassDamage(this Player player, DamageClass damageClass)
    {
      StatModifier totalDamage = player.GetTotalDamage(damageClass);
      double additive = (double) ((StatModifier) ref totalDamage).Additive;
      totalDamage = player.GetTotalDamage(damageClass);
      double multiplicative = (double) ((StatModifier) ref totalDamage).Multiplicative;
      return (float) (additive * multiplicative);
    }

    public static bool IsWeapon(this Item item)
    {
      return item.damage > 0 && item.pick == 0 && item.axe == 0 && item.hammer == 0 || item.type == 905;
    }

    public static bool IsWeaponWithDamageClass(this Item item)
    {
      return item.damage > 0 && item.DamageType != DamageClass.Default && item.pick == 0 && item.axe == 0 && item.hammer == 0 || item.type == 905;
    }

    public static bool IsWithinBounds(this int index, int cap) => index >= 0 && index < cap;

    public static bool IsWithinBounds(this int index, int lowerBound, int higherBound)
    {
      return index >= lowerBound && index < higherBound;
    }

    public static Vector2 SetMagnitude(this Vector2 vector, float magnitude)
    {
      return Vector2.op_Multiply(Utils.SafeNormalize(vector, Vector2.UnitY), magnitude);
    }

    public static float ActualClassCrit(this Player player, DamageClass damageClass)
    {
      return damageClass != DamageClass.Summon || player.HasEffect<SpiderEffect>() ? player.GetTotalCritChance(damageClass) : 0.0f;
    }

    public static bool FeralGloveReuse(this Player player, Item item)
    {
      if (!player.autoReuseGlove)
        return false;
      return item.CountsAsClass(DamageClass.Melee) || item.CountsAsClass(DamageClass.SummonMeleeSpeed);
    }

    public static bool CountsAsClass(this DamageClass damageClass, DamageClass intendedClass)
    {
      return damageClass == intendedClass || damageClass.GetEffectInheritance(intendedClass);
    }

    public static DamageClass ProcessDamageTypeFromHeldItem(this Player player)
    {
      if (player.HeldItem.damage <= 0 || player.HeldItem.pick > 0 || player.HeldItem.axe > 0 || player.HeldItem.hammer > 0)
        return DamageClass.Summon;
      if (player.HeldItem.DamageType.CountsAsClass(DamageClass.Melee))
        return DamageClass.Melee;
      if (player.HeldItem.DamageType.CountsAsClass(DamageClass.Ranged))
        return DamageClass.Ranged;
      if (player.HeldItem.DamageType.CountsAsClass(DamageClass.Magic))
        return DamageClass.Magic;
      return player.HeldItem.DamageType.CountsAsClass(DamageClass.Summon) || player.HeldItem.DamageType == DamageClass.Generic || player.HeldItem.DamageType == DamageClass.Default ? DamageClass.Summon : player.HeldItem.DamageType;
    }

    public static void Animate(
      this Projectile proj,
      int ticksPerFrame,
      int startFrame = 0,
      int? frames = null)
    {
      frames.GetValueOrDefault();
      if (!frames.HasValue)
        frames = new int?(Main.projFrames[proj.type]);
      if (++proj.frameCounter < ticksPerFrame)
        return;
      int num1 = ++proj.frame;
      int num2 = startFrame;
      int? nullable1 = frames;
      int? nullable2 = nullable1.HasValue ? new int?(num2 + nullable1.GetValueOrDefault()) : new int?();
      int valueOrDefault = nullable2.GetValueOrDefault();
      if (num1 >= valueOrDefault & nullable2.HasValue)
        proj.frame = startFrame;
      proj.frameCounter = 0;
    }
  }
}
