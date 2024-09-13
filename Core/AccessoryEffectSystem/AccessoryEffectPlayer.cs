// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.AccessoryEffectSystem.AccessoryEffectPlayer
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

#nullable disable
namespace FargowiltasSouls.Core.AccessoryEffectSystem
{
  public class AccessoryEffectPlayer : ModPlayer
  {
    public bool[] ActiveEffects = Array.Empty<bool>();
    public bool[] EquippedEffects = Array.Empty<bool>();
    public Item[] EffectItems = Array.Empty<Item>();
    private static readonly Dictionary<Expression<Func<AccessoryEffect, Delegate>>, List<AccessoryEffect>> Hooks = new Dictionary<Expression<Func<AccessoryEffect, Delegate>>, List<AccessoryEffect>>();
    private static List<AccessoryEffect> HookPreUpdate = AccessoryEffectPlayer.AddHook<Action<Player>>((Expression<Func<AccessoryEffect, Delegate>>) (p => (Action<Player>) ((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AccessoryEffect.PreUpdate))).CreateDelegate(typeof (Action<Player>), p)));
    private static List<AccessoryEffect> HookPostUpdateEquips = AccessoryEffectPlayer.AddHook<Action<Player>>((Expression<Func<AccessoryEffect, Delegate>>) (p => (Action<Player>) ((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AccessoryEffect.PostUpdateEquips))).CreateDelegate(typeof (Action<Player>), p)));
    private static List<AccessoryEffect> HookUpdateBadLifeRegen = AccessoryEffectPlayer.AddHook<Action<Player>>((Expression<Func<AccessoryEffect, Delegate>>) (p => (Action<Player>) ((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AccessoryEffect.UpdateBadLifeRegen))).CreateDelegate(typeof (Action<Player>), p)));
    private static List<AccessoryEffect> HookPostUpdate = AccessoryEffectPlayer.AddHook<Action<Player>>((Expression<Func<AccessoryEffect, Delegate>>) (p => (Action<Player>) ((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AccessoryEffect.PostUpdate))).CreateDelegate(typeof (Action<Player>), p)));
    private static List<AccessoryEffect> HookPostUpdateMiscEffects = AccessoryEffectPlayer.AddHook<Action<Player>>((Expression<Func<AccessoryEffect, Delegate>>) (p => (Action<Player>) ((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AccessoryEffect.PostUpdateMiscEffects))).CreateDelegate(typeof (Action<Player>), p)));
    private static List<AccessoryEffect> HookTryAdditionalAttacks = AccessoryEffectPlayer.AddHook<Action<Player, int, DamageClass>>((Expression<Func<AccessoryEffect, Delegate>>) (p => (Action<Player, int, DamageClass>) ((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AccessoryEffect.TryAdditionalAttacks))).CreateDelegate(typeof (Action<Player, int, DamageClass>), p)));
    private static List<AccessoryEffect> HookModifyHitNPCWithProj = AccessoryEffectPlayer.AddHook<AccessoryEffectPlayer.DelegateModifyHitNPCWithProj>((Expression<Func<AccessoryEffect, Delegate>>) (p => (\u003C\u003EA\u007B00000200\u007D<Player, Projectile, NPC, NPC.HitModifiers>) ((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AccessoryEffect.ModifyHitNPCWithProj))).CreateDelegate(typeof (\u003C\u003EA\u007B00000200\u007D<Player, Projectile, NPC, NPC.HitModifiers>), p)));
    private static List<AccessoryEffect> HookModifyHitNPCWithItem = AccessoryEffectPlayer.AddHook<AccessoryEffectPlayer.DelegateModifyHitNPCWithItem>((Expression<Func<AccessoryEffect, Delegate>>) (p => (\u003C\u003EA\u007B00000200\u007D<Player, Item, NPC, NPC.HitModifiers>) ((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AccessoryEffect.ModifyHitNPCWithItem))).CreateDelegate(typeof (\u003C\u003EA\u007B00000200\u007D<Player, Item, NPC, NPC.HitModifiers>), p)));
    private static List<AccessoryEffect> HookModifyHitNPCBoth = AccessoryEffectPlayer.AddHook<AccessoryEffectPlayer.DelegateHookModifyHitNPCBoth>((Expression<Func<AccessoryEffect, Delegate>>) (p => (\u003C\u003EA\u007B00000040\u007D<Player, NPC, NPC.HitModifiers, DamageClass>) ((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AccessoryEffect.ModifyHitNPCBoth))).CreateDelegate(typeof (\u003C\u003EA\u007B00000040\u007D<Player, NPC, NPC.HitModifiers, DamageClass>), p)));
    private static List<AccessoryEffect> HookModifyHitInfo = AccessoryEffectPlayer.AddHook<AccessoryEffectPlayer.DelegateHookModifyHitInfo>((Expression<Func<AccessoryEffect, Delegate>>) (p => (\u003C\u003EA\u007B00000040\u007D<Player, NPC, NPC.HitInfo, DamageClass>) ((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AccessoryEffect.ModifyHitInfo))).CreateDelegate(typeof (\u003C\u003EA\u007B00000040\u007D<Player, NPC, NPC.HitInfo, DamageClass>), p)));
    private static List<AccessoryEffect> HookOnHitNPCWithProj = AccessoryEffectPlayer.AddHook<Action<Player, Projectile, NPC, NPC.HitInfo, int>>((Expression<Func<AccessoryEffect, Delegate>>) (p => (Action<Player, Projectile, NPC, NPC.HitInfo, int>) ((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AccessoryEffect.OnHitNPCWithProj))).CreateDelegate(typeof (Action<Player, Projectile, NPC, NPC.HitInfo, int>), p)));
    private static List<AccessoryEffect> HookOnHitNPCWithItem = AccessoryEffectPlayer.AddHook<Action<Player, Item, NPC, NPC.HitInfo, int>>((Expression<Func<AccessoryEffect, Delegate>>) (p => (Action<Player, Item, NPC, NPC.HitInfo, int>) ((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AccessoryEffect.OnHitNPCWithItem))).CreateDelegate(typeof (Action<Player, Item, NPC, NPC.HitInfo, int>), p)));
    private static List<AccessoryEffect> HookOnHitNPCEither = AccessoryEffectPlayer.AddHook<Action<Player, NPC, NPC.HitInfo, DamageClass, int, Projectile, Item>>((Expression<Func<AccessoryEffect, Delegate>>) (p => (Action<Player, NPC, NPC.HitInfo, DamageClass, int, Projectile, Item>) ((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AccessoryEffect.OnHitNPCEither))).CreateDelegate(typeof (Action<Player, NPC, NPC.HitInfo, DamageClass, int, Projectile, Item>), p)));
    private static List<AccessoryEffect> HookMeleeEffects = AccessoryEffectPlayer.AddHook<Action<Player, Item, Rectangle>>((Expression<Func<AccessoryEffect, Delegate>>) (p => (Action<Player, Item, Rectangle>) ((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AccessoryEffect.MeleeEffects))).CreateDelegate(typeof (Action<Player, Item, Rectangle>), p)));
    private static List<AccessoryEffect> HookModifyUseSpeed = AccessoryEffectPlayer.AddHook<Func<Player, Item, float>>((Expression<Func<AccessoryEffect, Delegate>>) (p => (Func<Player, Item, float>) ((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AccessoryEffect.ModifyUseSpeed))).CreateDelegate(typeof (Func<Player, Item, float>), p)));
    private static List<AccessoryEffect> HookContactDamageDR = AccessoryEffectPlayer.AddHook<AccessoryEffectPlayer.DelegateContactDamageDR>((Expression<Func<AccessoryEffect, Delegate>>) (p => (\u003C\u003EF\u007B00000040\u007D<Player, NPC, Player.HurtModifiers, float>) ((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AccessoryEffect.ContactDamageDR))).CreateDelegate(typeof (\u003C\u003EF\u007B00000040\u007D<Player, NPC, Player.HurtModifiers, float>), p)));
    private static List<AccessoryEffect> HookProjectileDamageDR = AccessoryEffectPlayer.AddHook<AccessoryEffectPlayer.DelegateProjectileDamageDR>((Expression<Func<AccessoryEffect, Delegate>>) (p => (\u003C\u003EF\u007B00000040\u007D<Player, Projectile, Player.HurtModifiers, float>) ((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AccessoryEffect.ProjectileDamageDR))).CreateDelegate(typeof (\u003C\u003EF\u007B00000040\u007D<Player, Projectile, Player.HurtModifiers, float>), p)));
    private static List<AccessoryEffect> HookModifyHitByNPC = AccessoryEffectPlayer.AddHook<AccessoryEffectPlayer.DelegateModifyHitByNPC>((Expression<Func<AccessoryEffect, Delegate>>) (p => (\u003C\u003EA\u007B00000040\u007D<Player, NPC, Player.HurtModifiers>) ((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AccessoryEffect.ModifyHitByNPC))).CreateDelegate(typeof (\u003C\u003EA\u007B00000040\u007D<Player, NPC, Player.HurtModifiers>), p)));
    private static List<AccessoryEffect> HookModifyHitByProjectile = AccessoryEffectPlayer.AddHook<AccessoryEffectPlayer.DelegateModifyHitByProjectile>((Expression<Func<AccessoryEffect, Delegate>>) (p => (\u003C\u003EA\u007B00000040\u007D<Player, Projectile, Player.HurtModifiers>) ((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AccessoryEffect.ModifyHitByProjectile))).CreateDelegate(typeof (\u003C\u003EA\u007B00000040\u007D<Player, Projectile, Player.HurtModifiers>), p)));
    private static List<AccessoryEffect> HookOnHitByNPC = AccessoryEffectPlayer.AddHook<Action<Player, NPC, Player.HurtInfo>>((Expression<Func<AccessoryEffect, Delegate>>) (p => (Action<Player, NPC, Player.HurtInfo>) ((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AccessoryEffect.OnHitByNPC))).CreateDelegate(typeof (Action<Player, NPC, Player.HurtInfo>), p)));
    private static List<AccessoryEffect> HookOnHitByProjectile = AccessoryEffectPlayer.AddHook<Action<Player, Projectile, Player.HurtInfo>>((Expression<Func<AccessoryEffect, Delegate>>) (p => (Action<Player, Projectile, Player.HurtInfo>) ((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AccessoryEffect.OnHitByProjectile))).CreateDelegate(typeof (Action<Player, Projectile, Player.HurtInfo>), p)));
    private static List<AccessoryEffect> HookOnHitByEither = AccessoryEffectPlayer.AddHook<Action<Player, NPC, Projectile>>((Expression<Func<AccessoryEffect, Delegate>>) (p => (Action<Player, NPC, Projectile>) ((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AccessoryEffect.OnHitByEither))).CreateDelegate(typeof (Action<Player, NPC, Projectile>), p)));
    private static List<AccessoryEffect> HookCanBeHitByNPC = AccessoryEffectPlayer.AddHook<Func<Player, NPC, bool>>((Expression<Func<AccessoryEffect, Delegate>>) (p => (Func<Player, NPC, bool>) ((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AccessoryEffect.CanBeHitByNPC))).CreateDelegate(typeof (Func<Player, NPC, bool>), p)));
    private static List<AccessoryEffect> HookCanBeHitByProjectile = AccessoryEffectPlayer.AddHook<Func<Player, Projectile, bool>>((Expression<Func<AccessoryEffect, Delegate>>) (p => (Func<Player, Projectile, bool>) ((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AccessoryEffect.CanBeHitByProjectile))).CreateDelegate(typeof (Func<Player, Projectile, bool>), p)));
    private static List<AccessoryEffect> HookModifyHurt = AccessoryEffectPlayer.AddHook<AccessoryEffectPlayer.DelegateModifyHurt>((Expression<Func<AccessoryEffect, Delegate>>) (p => (\u003C\u003EA\u007B00000008\u007D<Player, Player.HurtModifiers>) ((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AccessoryEffect.ModifyHurt))).CreateDelegate(typeof (\u003C\u003EA\u007B00000008\u007D<Player, Player.HurtModifiers>), p)));
    private static List<AccessoryEffect> HookOnHurt = AccessoryEffectPlayer.AddHook<Action<Player, Player.HurtInfo>>((Expression<Func<AccessoryEffect, Delegate>>) (p => (Action<Player, Player.HurtInfo>) ((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AccessoryEffect.OnHurt))).CreateDelegate(typeof (Action<Player, Player.HurtInfo>), p)));
    private static List<AccessoryEffect> HookPreKill = AccessoryEffectPlayer.AddHook<AccessoryEffectPlayer.DelegatePreKill>((Expression<Func<AccessoryEffect, Delegate>>) (p => (\u003C\u003EF\u007B00049000\u007D<Player, double, int, bool, bool, bool, PlayerDeathReason, bool>) ((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AccessoryEffect.PreKill))).CreateDelegate(typeof (\u003C\u003EF\u007B00049000\u007D<Player, double, int, bool, bool, bool, PlayerDeathReason, bool>), p)));
    private static List<AccessoryEffect> HookDrawEffects = AccessoryEffectPlayer.AddHook<AccessoryEffectPlayer.DelegateDrawEffects>((Expression<Func<AccessoryEffect, Delegate>>) (p => (\u003C\u003EA\u007B00049240\u007D<Player, PlayerDrawSet, float, float, float, float, bool>) ((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AccessoryEffect.DrawEffects))).CreateDelegate(typeof (\u003C\u003EA\u007B00049240\u007D<Player, PlayerDrawSet, float, float, float, float, bool>), p)));

    public bool Active(AccessoryEffect effect) => this.ActiveEffects[effect.Index];

    public bool Equipped(AccessoryEffect effect) => this.EquippedEffects[effect.Index];

    public virtual void SetStaticDefaults()
    {
      foreach (KeyValuePair<Expression<Func<AccessoryEffect, Delegate>>, List<AccessoryEffect>> hook in AccessoryEffectPlayer.Hooks)
        hook.Value.AddRange(LoaderUtils.WhereMethodIsOverridden<AccessoryEffect>((IEnumerable<AccessoryEffect>) AccessoryEffectLoader.AccessoryEffects, hook.Key));
    }

    public virtual void Initialize()
    {
      int count = AccessoryEffectLoader.AccessoryEffects.Count;
      this.ActiveEffects = new bool[count];
      this.EquippedEffects = new bool[count];
      this.EffectItems = new Item[count];
    }

    private static List<AccessoryEffect> AddHook<F>(Expression<Func<AccessoryEffect, Delegate>> expr)
    {
      List<AccessoryEffect> accessoryEffectList = new List<AccessoryEffect>();
      AccessoryEffectPlayer.Hooks.Add(expr, accessoryEffectList);
      return accessoryEffectList;
    }

    public virtual void ResetEffects()
    {
      for (int index = 0; index < this.ActiveEffects.Length; ++index)
        this.ActiveEffects[index] = false;
      for (int index = 0; index < this.EquippedEffects.Length; ++index)
        this.EquippedEffects[index] = false;
      for (int index = 0; index < this.EffectItems.Length; ++index)
        this.EffectItems[index] = (Item) null;
    }

    public virtual void UpdateDead() => base.ResetEffects();

    public virtual void PreUpdate()
    {
      foreach (AccessoryEffect effect in AccessoryEffectPlayer.HookPreUpdate)
      {
        if (this.Active(effect))
          effect.PreUpdate(this.Player);
      }
    }

    public virtual void PostUpdateEquips()
    {
      foreach (AccessoryEffect hookPostUpdateEquip in AccessoryEffectPlayer.HookPostUpdateEquips)
      {
        if (this.Active(hookPostUpdateEquip))
          hookPostUpdateEquip.PostUpdateEquips(this.Player);
      }
    }

    public virtual void UpdateBadLifeRegen()
    {
      foreach (AccessoryEffect effect in AccessoryEffectPlayer.HookUpdateBadLifeRegen)
      {
        if (this.Active(effect))
          effect.UpdateBadLifeRegen(this.Player);
      }
    }

    public virtual void PostUpdate()
    {
      foreach (AccessoryEffect effect in AccessoryEffectPlayer.HookPostUpdate)
      {
        if (this.Active(effect))
          effect.PostUpdate(this.Player);
      }
    }

    public virtual void PostUpdateMiscEffects()
    {
      foreach (AccessoryEffect updateMiscEffect in AccessoryEffectPlayer.HookPostUpdateMiscEffects)
      {
        if (this.Active(updateMiscEffect))
          updateMiscEffect.PostUpdateMiscEffects(this.Player);
      }
    }

    public void TryAdditionalAttacks(int damage, DamageClass damageType)
    {
      foreach (AccessoryEffect additionalAttack in AccessoryEffectPlayer.HookTryAdditionalAttacks)
      {
        if (this.Active(additionalAttack))
          additionalAttack.TryAdditionalAttacks(this.Player, damage, damageType);
      }
    }

    public virtual void ModifyHitNPCWithProj(
      Projectile proj,
      NPC target,
      ref NPC.HitModifiers modifiers)
    {
      if (proj.hostile)
        return;
      foreach (AccessoryEffect effect in AccessoryEffectPlayer.HookModifyHitNPCWithProj)
      {
        if (this.Active(effect))
          effect.ModifyHitNPCWithProj(this.Player, proj, target, ref modifiers);
      }
      this.ModifyHitNPCBoth(target, ref modifiers, proj.DamageType);
    }

    public virtual void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
    {
      foreach (AccessoryEffect effect in AccessoryEffectPlayer.HookModifyHitNPCWithItem)
      {
        if (this.Active(effect))
          effect.ModifyHitNPCWithItem(this.Player, item, target, ref modifiers);
      }
      this.ModifyHitNPCBoth(target, ref modifiers, item.DamageType);
    }

    public void ModifyHitNPCBoth(
      NPC target,
      ref NPC.HitModifiers modifiers,
      DamageClass damageClass)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      AccessoryEffectPlayer.\u003C\u003Ec__DisplayClass31_0 cDisplayClass310 = new AccessoryEffectPlayer.\u003C\u003Ec__DisplayClass31_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass310.\u003C\u003E4__this = this;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass310.target = target;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass310.damageClass = damageClass;
      // ISSUE: method pointer
      ((NPC.HitModifiers) ref modifiers).ModifyHitInfo += new NPC.HitModifiers.HitInfoModifier((object) cDisplayClass310, __methodptr(\u003CModifyHitNPCBoth\u003Eb__0));
      foreach (AccessoryEffect effect in AccessoryEffectPlayer.HookModifyHitNPCBoth)
      {
        if (this.Active(effect))
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          effect.ModifyHitNPCBoth(this.Player, cDisplayClass310.target, ref modifiers, cDisplayClass310.damageClass);
        }
      }
    }

    public void ModifyHitInfo(NPC target, ref NPC.HitInfo hitInfo, DamageClass damageClass)
    {
      foreach (AccessoryEffect effect in AccessoryEffectPlayer.HookModifyHitInfo)
      {
        if (this.Active(effect))
          effect.ModifyHitInfo(this.Player, target, ref hitInfo, damageClass);
      }
    }

    public virtual void OnHitNPCWithProj(
      Projectile proj,
      NPC target,
      NPC.HitInfo hit,
      int damageDone)
    {
      if (target.type == 488 || target.friendly)
        return;
      foreach (AccessoryEffect effect in AccessoryEffectPlayer.HookOnHitNPCWithProj)
      {
        if (this.Active(effect))
          effect.OnHitNPCWithProj(this.Player, proj, target, hit, damageDone);
      }
      if (proj.minion)
        this.TryAdditionalAttacks(proj.damage, proj.DamageType);
      this.OnHitNPCEither(target, hit, proj.DamageType, proj);
    }

    public virtual void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
    {
      if (target.type == 488 || target.friendly)
        return;
      foreach (AccessoryEffect effect in AccessoryEffectPlayer.HookOnHitNPCWithItem)
      {
        if (this.Active(effect))
          effect.OnHitNPCWithItem(this.Player, item, target, hit, damageDone);
      }
      this.OnHitNPCEither(target, hit, item.DamageType, item: item);
    }

    private void OnHitNPCEither(
      NPC target,
      NPC.HitInfo hitInfo,
      DamageClass damageClass,
      Projectile projectile = null,
      Item item = null)
    {
      int baseDamage = GetBaseDamage();
      foreach (AccessoryEffect effect in AccessoryEffectPlayer.HookOnHitNPCEither)
      {
        if (this.Active(effect))
          effect.OnHitNPCEither(this.Player, target, hitInfo, damageClass, baseDamage, projectile, item);
      }

      int GetBaseDamage()
      {
        int baseDamage = ((NPC.HitInfo) ref hitInfo).SourceDamage;
        if (projectile != null)
          baseDamage = projectile.damage;
        else if (item != null)
          baseDamage = this.Player.GetWeaponDamage(item, false);
        return baseDamage;
      }
    }

    public virtual void MeleeEffects(Item item, Rectangle hitbox)
    {
      foreach (AccessoryEffect hookMeleeEffect in AccessoryEffectPlayer.HookMeleeEffects)
      {
        if (this.Active(hookMeleeEffect))
          hookMeleeEffect.MeleeEffects(this.Player, item, hitbox);
      }
    }

    public float ModifyUseSpeed(Item item)
    {
      float num = 0.0f;
      foreach (AccessoryEffect effect in AccessoryEffectPlayer.HookModifyUseSpeed)
      {
        if (this.Active(effect))
          num += effect.ModifyUseSpeed(this.Player, item);
      }
      return num;
    }

    public float ContactDamageDR(NPC npc, ref Player.HurtModifiers modifiers)
    {
      float num = 0.0f;
      foreach (AccessoryEffect effect in AccessoryEffectPlayer.HookContactDamageDR)
      {
        if (this.Active(effect))
          num += effect.ContactDamageDR(this.Player, npc, ref modifiers);
      }
      return num;
    }

    public float ProjectileDamageDR(Projectile projectile, ref Player.HurtModifiers modifiers)
    {
      float num = 0.0f;
      foreach (AccessoryEffect effect in AccessoryEffectPlayer.HookProjectileDamageDR)
      {
        if (this.Active(effect))
          num += effect.ProjectileDamageDR(this.Player, projectile, ref modifiers);
      }
      return num;
    }

    public virtual void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
    {
      foreach (AccessoryEffect effect in AccessoryEffectPlayer.HookModifyHitByNPC)
      {
        if (this.Active(effect))
          effect.ModifyHitByNPC(this.Player, npc, ref modifiers);
      }
    }

    public virtual void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
    {
      foreach (AccessoryEffect effect in AccessoryEffectPlayer.HookModifyHitByProjectile)
      {
        if (this.Active(effect))
          effect.ModifyHitByProjectile(this.Player, proj, ref modifiers);
      }
    }

    public virtual void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
    {
      foreach (AccessoryEffect effect in AccessoryEffectPlayer.HookOnHitByNPC)
      {
        if (this.Active(effect))
          effect.OnHitByNPC(this.Player, npc, hurtInfo);
      }
      this.OnHitByEither(npc, (Projectile) null);
    }

    public virtual void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
    {
      foreach (AccessoryEffect effect in AccessoryEffectPlayer.HookOnHitByProjectile)
      {
        if (this.Active(effect))
          effect.OnHitByProjectile(this.Player, proj, hurtInfo);
      }
      this.OnHitByEither((NPC) null, proj);
    }

    public void OnHitByEither(NPC npc, Projectile proj)
    {
      foreach (AccessoryEffect effect in AccessoryEffectPlayer.HookOnHitByEither)
      {
        if (this.Active(effect))
          effect.OnHitByEither(this.Player, npc, proj);
      }
    }

    public virtual bool CanBeHitByNPC(NPC npc, ref int CooldownSlot)
    {
      foreach (AccessoryEffect effect in AccessoryEffectPlayer.HookCanBeHitByNPC)
      {
        if (this.Active(effect) && !effect.CanBeHitByNPC(this.Player, npc))
          return false;
      }
      return true;
    }

    public virtual bool CanBeHitByProjectile(Projectile proj)
    {
      foreach (AccessoryEffect effect in AccessoryEffectPlayer.HookCanBeHitByProjectile)
      {
        if (this.Active(effect) && !effect.CanBeHitByProjectile(this.Player, proj))
          return false;
      }
      return true;
    }

    public virtual void ModifyHurt(ref Player.HurtModifiers modifiers)
    {
      foreach (AccessoryEffect effect in AccessoryEffectPlayer.HookModifyHurt)
      {
        if (this.Active(effect))
          effect.ModifyHurt(this.Player, ref modifiers);
      }
    }

    public virtual void OnHurt(Player.HurtInfo info)
    {
      foreach (AccessoryEffect effect in AccessoryEffectPlayer.HookOnHurt)
      {
        if (this.Active(effect))
          effect.OnHurt(this.Player, info);
      }
    }

    public virtual bool PreKill(
      double damage,
      int hitDirection,
      bool pvp,
      ref bool playSound,
      ref bool genGore,
      ref PlayerDeathReason damageSource)
    {
      bool flag = true;
      foreach (AccessoryEffect effect in AccessoryEffectPlayer.HookPreKill)
      {
        if (this.Active(effect))
          flag &= effect.PreKill(this.Player, damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
      }
      return flag;
    }

    public virtual void DrawEffects(
      PlayerDrawSet drawInfo,
      ref float r,
      ref float g,
      ref float b,
      ref float a,
      ref bool fullBright)
    {
      foreach (AccessoryEffect hookDrawEffect in AccessoryEffectPlayer.HookDrawEffects)
      {
        if (this.Active(hookDrawEffect))
          hookDrawEffect.DrawEffects(this.Player, drawInfo, ref r, ref g, ref b, ref a, ref fullBright);
      }
    }

    private delegate void DelegateModifyHitNPCWithProj(
      Player player,
      Projectile proj,
      NPC target,
      ref NPC.HitModifiers modifiers);

    private delegate void DelegateModifyHitNPCWithItem(
      Player player,
      Item item,
      NPC target,
      ref NPC.HitModifiers modifiers);

    private delegate void DelegateHookModifyHitNPCBoth(
      Player player,
      NPC target,
      ref NPC.HitModifiers modifiers,
      DamageClass damageClass);

    private delegate void DelegateHookModifyHitInfo(
      Player player,
      NPC target,
      ref NPC.HitInfo hitInfo,
      DamageClass damageClass);

    private delegate float DelegateContactDamageDR(
      Player player,
      NPC npc,
      ref Player.HurtModifiers modifiers);

    private delegate float DelegateProjectileDamageDR(
      Player player,
      Projectile projectile,
      ref Player.HurtModifiers modifiers);

    private delegate void DelegateModifyHitByNPC(
      Player player,
      NPC npc,
      ref Player.HurtModifiers modifiers);

    private delegate void DelegateModifyHitByProjectile(
      Player player,
      Projectile proj,
      ref Player.HurtModifiers modifiers);

    private delegate void DelegateModifyHurt(Player player, ref Player.HurtModifiers modifiers);

    private delegate bool DelegatePreKill(
      Player player,
      double damage,
      int hitDirection,
      bool pvp,
      ref bool playSound,
      ref bool genGore,
      ref PlayerDeathReason damageSource);

    private delegate void DelegateDrawEffects(
      Player player,
      PlayerDrawSet drawInfo,
      ref float r,
      ref float g,
      ref float b,
      ref float a,
      ref bool fullBright);
  }
}
