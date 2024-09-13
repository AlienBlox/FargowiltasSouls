// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.AccessoryEffectSystem.AccessoryEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Core.AccessoryEffectSystem
{
  public abstract class AccessoryEffect : ModType
  {
    public int Index;

    public string ToggleDescription
    {
      get
      {
        string textValue = Language.GetTextValue("Mods." + this.Mod.Name + ".Toggler." + this.Name);
        if (this.ToggleItemType <= 0)
          return textValue;
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
        interpolatedStringHandler.AppendLiteral("[i:");
        interpolatedStringHandler.AppendFormatted<int>(this.ToggleItemType);
        interpolatedStringHandler.AppendLiteral("]");
        return interpolatedStringHandler.ToStringAndClear() + " " + textValue;
      }
    }

    public abstract Header ToggleHeader { get; }

    public virtual int ToggleItemType => -1;

    public bool HasToggle => this.ToggleHeader != null;

    public virtual bool MinionEffect => false;

    public virtual bool ExtraAttackEffect => false;

    public virtual bool IgnoresMutantPresence => false;

    public virtual bool DefaultToggle => true;

    protected virtual void Register()
    {
      AccessoryEffectLoader.Register(this);
      ModTypeLookup<AccessoryEffect>.Register(this);
    }

    public Item EffectItem(Player player) => player.AccessoryEffects().EffectItems[this.Index];

    public IEntitySource GetSource_EffectItem(Player player)
    {
      return player.GetSource_Accessory(this.EffectItem(player), (string) null);
    }

    public virtual void PreUpdate(Player player)
    {
    }

    public virtual void PostUpdateEquips(Player player)
    {
    }

    public virtual void UpdateBadLifeRegen(Player player)
    {
    }

    public virtual void PostUpdate(Player player)
    {
    }

    public virtual void PostUpdateMiscEffects(Player player)
    {
    }

    public virtual void TryAdditionalAttacks(Player player, int damage, DamageClass damageType)
    {
    }

    public virtual void ModifyHitNPCWithProj(
      Player player,
      Projectile proj,
      NPC target,
      ref NPC.HitModifiers modifiers)
    {
    }

    public virtual void ModifyHitNPCWithItem(
      Player player,
      Item item,
      NPC target,
      ref NPC.HitModifiers modifiers)
    {
    }

    public virtual void ModifyHitNPCBoth(
      Player player,
      NPC target,
      ref NPC.HitModifiers modifiers,
      DamageClass damageClass)
    {
    }

    public virtual void ModifyHitInfo(
      Player player,
      NPC target,
      ref NPC.HitInfo hitInfo,
      DamageClass damageClass)
    {
    }

    public virtual void OnHitNPCWithProj(
      Player player,
      Projectile proj,
      NPC target,
      NPC.HitInfo hit,
      int damageDone)
    {
    }

    public virtual void OnHitNPCWithItem(
      Player player,
      Item item,
      NPC target,
      NPC.HitInfo hit,
      int damageDone)
    {
    }

    public virtual void OnHitNPCEither(
      Player player,
      NPC target,
      NPC.HitInfo hitInfo,
      DamageClass damageClass,
      int baseDamage,
      Projectile projectile,
      Item item)
    {
    }

    public virtual void MeleeEffects(Player player, Item item, Rectangle hitbox)
    {
    }

    public virtual float ModifyUseSpeed(Player player, Item item) => 0.0f;

    public virtual float ContactDamageDR(
      Player player,
      NPC npc,
      ref Player.HurtModifiers modifiers)
    {
      return 0.0f;
    }

    public virtual float ProjectileDamageDR(
      Player player,
      Projectile projectile,
      ref Player.HurtModifiers modifiers)
    {
      return 0.0f;
    }

    public virtual void ModifyHitByNPC(Player player, NPC npc, ref Player.HurtModifiers modifiers)
    {
    }

    public virtual void ModifyHitByProjectile(
      Player player,
      Projectile projectile,
      ref Player.HurtModifiers modifiers)
    {
    }

    public virtual void OnHitByNPC(Player player, NPC npc, Player.HurtInfo hurtInfo)
    {
    }

    public virtual void OnHitByProjectile(Player player, Projectile proj, Player.HurtInfo hurtInfo)
    {
    }

    public virtual void OnHitByEither(Player player, NPC npc, Projectile proj)
    {
    }

    public virtual bool CanBeHitByNPC(Player player, NPC npc) => true;

    public virtual bool CanBeHitByProjectile(Player player, Projectile projectile) => true;

    public virtual void ModifyHurt(Player player, ref Player.HurtModifiers modifiers)
    {
    }

    public virtual void OnHurt(Player player, Player.HurtInfo info)
    {
    }

    public virtual bool PreKill(
      Player player,
      double damage,
      int hitDirection,
      bool pvp,
      ref bool playSound,
      ref bool genGore,
      ref PlayerDeathReason damageSource)
    {
      return true;
    }

    public virtual void DrawEffects(
      Player player,
      PlayerDrawSet drawInfo,
      ref float r,
      ref float g,
      ref float b,
      ref float a,
      ref bool fullBright)
    {
    }
  }
}
