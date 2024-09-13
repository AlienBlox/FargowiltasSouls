// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.Systems.SetBonusManager
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Armor;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Core.Systems
{
  public class SetBonusManager : ModSystem
  {
    public virtual void Load()
    {
      // ISSUE: method pointer
      On_Player.KeyDoubleTap += new On_Player.hook_KeyDoubleTap((object) this, __methodptr(SetBonusKeyEffects));
    }

    public virtual void Unload()
    {
      // ISSUE: method pointer
      On_Player.KeyDoubleTap -= new On_Player.hook_KeyDoubleTap((object) this, __methodptr(SetBonusKeyEffects));
    }

    public void SetBonusKeyEffects(On_Player.orig_KeyDoubleTap orig, Player player, int keyDir)
    {
      orig.Invoke(player, keyDir);
      if (keyDir != (Main.ReversedUpDownArmorSetBonuses ? 1 : 0))
        return;
      GladiatorBanner.ActivateGladiatorBanner(player);
      PalmwoodEffect.ActivatePalmwoodSentry(player);
      EridanusHat.EridanusSetBonusKey(player);
      GaiaHelmet.GaiaSetBonusKey(player);
      NekomiHood.NekomiSetBonusKey(player);
      StyxCrown.StyxSetBonusKey(player);
      ForbiddenEffect.ActivateForbiddenStorm(player);
      VortexEnchant.ActivateVortex(player);
    }
  }
}
