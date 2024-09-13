// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Common.EternityWorldIconManager
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Common
{
  public class EternityWorldIconManager : ModSystem
  {
    public virtual void SaveWorldHeader(TagCompound tag)
    {
      tag["EternityWorld"] = (object) WorldSavingSystem.EternityMode;
      tag["MasochistWorld"] = (object) WorldSavingSystem.MasochistModeReal;
    }
  }
}
