// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.Sharkron
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class Sharkron : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchTypeRange(372, 373);

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.lifeMax *= 5;
      npc.lavaImmune = true;
      if (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.fishBossEX, 370))
        return;
      npc.lifeMax *= 5000;
    }

    public virtual void OnSpawn(NPC npc, IEntitySource source)
    {
      base.OnSpawn(npc, source);
      if (!(source is EntitySource_Parent entitySourceParent) || !(entitySourceParent.Entity is Projectile entity) || entity.type != 386 || !entity.Eternity().altBehaviour)
        return;
      npc.type = 0;
      ((Entity) npc).active = false;
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.fishBossEX, 370))
      {
        npc.buffImmune[ModContent.BuffType<FlamesoftheUniverseBuff>()] = true;
        npc.buffImmune[ModContent.BuffType<LightningRodBuff>()] = true;
      }
      npc.buffImmune[24] = true;
      npc.buffImmune[68] = true;
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<MutantNibbleBuff>(), 300, true, false);
      target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 1200, true, false);
      target.FargoSouls().MaxLifeReduction += FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.fishBossEX, 370) ? 100 : 25;
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadNPCSprite(recolor, npc.type);
    }
  }
}
