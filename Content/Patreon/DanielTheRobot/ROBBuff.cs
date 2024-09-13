// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.DanielTheRobot.ROBBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.DanielTheRobot
{
  public class ROBBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.buffNoTimeDisplay[this.Type] = true;
      Main.vanityPet[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      player.buffTime[buffIndex] = 18000;
      player.GetModPlayer<PatreonPlayer>().ROB = true;
      if (((Entity) player).whoAmI != Main.myPlayer || player.ownedProjectileCounts[ModContent.ProjectileType<ROB>()] >= 1)
        return;
      Projectile.NewProjectile(player.GetSource_Buff(buffIndex), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<ROB>(), 0, 0.0f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
    }

    public virtual void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
    {
      if (Main.LocalPlayer.name.Contains("Daniel"))
        tip = Language.GetTextValue("Mods.FargowiltasSouls.Buffs.ROBBuff.DescPatreon");
      else
        tip = Language.GetTextValue("Mods.FargowiltasSouls.Buffs.ROBBuff.DescNonPatreon");
    }
  }
}
