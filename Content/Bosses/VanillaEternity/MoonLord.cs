// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.MoonLord
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public abstract class MoonLord : EModeNPCBehaviour
  {
    public abstract int GetVulnerabilityState(NPC npc);

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[ModContent.BuffType<ClippedWingsBuff>()] = true;
      npc.buffImmune[ModContent.BuffType<LethargicBuff>()] = true;
      npc.buffImmune[68] = true;
    }

    public virtual bool CanHitPlayer(NPC npc, Player target, ref int CooldownSlot) => false;

    public virtual bool? CanBeHitByItem(NPC npc, Player player, Item item)
    {
      int vulnerabilityState = this.GetVulnerabilityState(npc);
      return item.CountsAsClass(DamageClass.Melee) && vulnerabilityState > 0 && vulnerabilityState < 4 && !player.buffImmune[ModContent.BuffType<NullificationCurseBuff>()] && !WorldSavingSystem.SwarmActive ? new bool?(false) : base.CanBeHitByItem(npc, player, item);
    }

    public virtual bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
    {
      if (!Main.player[projectile.owner].buffImmune[ModContent.BuffType<NullificationCurseBuff>()] && !WorldSavingSystem.SwarmActive)
      {
        switch (this.GetVulnerabilityState(npc))
        {
          case 0:
            if (!projectile.CountsAsClass(DamageClass.Melee))
              return new bool?(false);
            break;
          case 1:
            if (!projectile.CountsAsClass(DamageClass.Ranged))
              return new bool?(false);
            break;
          case 2:
            if (!projectile.CountsAsClass(DamageClass.Magic))
              return new bool?(false);
            break;
          case 3:
            if (!FargoSoulsUtil.IsSummonDamage(projectile))
              return new bool?(false);
            break;
        }
      }
      return base.CanBeHitByProjectile(npc, projectile);
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadNPCSprite(recolor, npc.type);
    }
  }
}
