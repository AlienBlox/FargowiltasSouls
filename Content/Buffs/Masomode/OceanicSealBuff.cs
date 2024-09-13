// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Masomode.OceanicSealBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Masomode
{
  public class OceanicSealBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      Main.debuff[this.Type] = true;
      Main.buffNoSave[this.Type] = true;
      Main.buffNoTimeDisplay[this.Type] = true;
      BuffID.Sets.NurseCannotRemoveDebuff[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      player.FargoSouls().OceanicMaul = true;
      player.FargoSouls().TinEternityDamage = 0.0f;
      player.FargoSouls().MutantPresence = true;
      player.FargoSouls().noDodge = true;
      player.FargoSouls().noSupersonic = true;
      player.moonLeech = true;
      if (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.fishBoss, 370))
        return;
      player.buffTime[buffIndex] = 2;
      if (((Entity) player).whoAmI != Main.npc[EModeGlobalNPC.fishBoss].target || ((Entity) player).whoAmI != Main.myPlayer || player.ownedProjectileCounts[ModContent.ProjectileType<FishronRitual2>()] >= 1)
        return;
      Projectile.NewProjectile(((Entity) Main.npc[EModeGlobalNPC.fishBoss]).GetSource_FromThis((string) null), ((Entity) Main.npc[EModeGlobalNPC.fishBoss]).Center, Vector2.Zero, ModContent.ProjectileType<FishronRitual2>(), 0, 0.0f, ((Entity) player).whoAmI, 0.0f, (float) EModeGlobalNPC.fishBoss, 0.0f);
    }
  }
}
