// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Common.ÉternityInfoUIManager
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Luminance.Core.MenuInfoUI;
using System;
using System.Collections.Generic;
using Terraria.IO;
using Terraria.ModLoader.IO;

#nullable enable
namespace FargowiltasSouls.Common
{
  public class ÉternityInfoUIManager : InfoUIManager
  {
    public static bool EternityWorld(
    #nullable disable
    TagCompound tag)
    {
      return tag.ContainsKey(nameof (EternityWorld)) && tag.GetBool(nameof (EternityWorld));
    }

    public static bool MasoWorld(TagCompound tag)
    {
      return tag.ContainsKey("MasochistWorld") && tag.GetBool("MasochistWorld");
    }

    public virtual IEnumerable<WorldInfoIcon> GetWorldInfoIcons()
    {
      TagCompound tag1;
      yield return new WorldInfoIcon(ÉternityInfoUIManager.EternityIconPath, "Mods.FargowiltasSouls.UI.EternityEnabled", (Func<WorldFileData, bool>) (worldFileData => worldFileData.TryGetHeaderData<EternityWorldIconManager>(ref tag1) && ÉternityInfoUIManager.EternityWorld(tag1) && !ÉternityInfoUIManager.MasoWorld(tag1)), (byte) 0);
      TagCompound tag2;
      yield return new WorldInfoIcon(ÉternityInfoUIManager.MasochistIconPath, "Mods.FargowiltasSouls.UI.MasochistEnabled", (Func<WorldFileData, bool>) (worldFileData => worldFileData.TryGetHeaderData<EternityWorldIconManager>(ref tag2) && ÉternityInfoUIManager.MasoWorld(tag2)), (byte) 0);
    }

    internal static string EternityIconPath => "FargowiltasSouls/Assets/UI/OncomingMutant";

    internal static string MasochistIconPath => "FargowiltasSouls/Assets/UI/OncomingMutantWithAura";
  }
}
