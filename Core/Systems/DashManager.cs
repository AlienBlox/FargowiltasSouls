// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.Systems.DashManager
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Consumables;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Core.Systems
{
  public class DashManager : ModSystem
  {
    public static void AddDashes(Player player)
    {
      if (((Entity) player).whoAmI != Main.myPlayer)
        return;
      if (player.HasEffect<JungleDashEffect>())
        JungleDashEffect.AddDash(player);
      if (player.HasEffect<MonkDashEffect>())
        MonkDashEffect.AddDash(player);
      if (player.HasEffect<SolarEffect>())
        SolarEffect.AddDash(player);
      if (!player.HasEffect<DeerSinewEffect>())
        return;
      DeerSinewEffect.AddDash(player);
    }

    public static void ManageDashes(Player Player)
    {
      if (((Entity) Player).whoAmI != Main.myPlayer)
        return;
      FargoSoulsPlayer fargoSoulsPlayer = Player.FargoSouls();
      if (fargoSoulsPlayer.FargoDash == DashManager.DashType.None)
        return;
      Player.dashType = 22;
      if (Player.dashDelay != 0 || Player.mount.Active)
        return;
      bool dashing;
      int dir;
      DashManager.HandleDash(out dashing, out dir);
      if (!dashing || dir == 0)
        return;
      switch (fargoSoulsPlayer.FargoDash)
      {
        case DashManager.DashType.Monk:
          MonkDashEffect.MonkDash(Player, dir);
          break;
        case DashManager.DashType.Jungle:
          JungleDashEffect.JungleDash(Player, dir);
          break;
        case DashManager.DashType.DeerSinew:
          fargoSoulsPlayer.DeerSinewDash(dir);
          break;
        default:
          Main.NewText("Fargo dash manager: dash not registered", byte.MaxValue, byte.MaxValue, byte.MaxValue);
          break;
      }
    }

    public static MethodInfo DashHandleMethod { get; set; }

    public virtual void Load()
    {
      DashManager.DashHandleMethod = typeof (Player).GetMethod("DoCommonDashHandle", Luminance.Common.Utilities.Utilities.UniversalBindingFlags);
    }

    public static void HandleDash(out bool dashing, out int dir)
    {
      dir = 1;
      dashing = true;
      Player localPlayer = Main.LocalPlayer;
      Player.DashStartAction dashStartAction = (Player.DashStartAction) null;
      object[] objArray = new object[3]
      {
        (object) dir,
        (object) dashing,
        (object) dashStartAction
      };
      ((MethodBase) DashManager.DashHandleMethod).Invoke((object) localPlayer, objArray);
      dir = (int) objArray[0];
      dashing = (bool) objArray[1];
    }

    public enum DashType
    {
      None,
      Monk,
      Jungle,
      DeerSinew,
    }
  }
}
