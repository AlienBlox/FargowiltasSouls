// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.Systems.WorldUpdatingSystem
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Core.Systems
{
  public class WorldUpdatingSystem : ModSystem
  {
    public virtual void PreUpdateNPCs()
    {
      Mod mod;
      int num;
      if (Terraria.ModLoader.ModLoader.TryGetMod("Fargowiltas", ref mod))
        num = (bool) mod.Call(new object[1]
        {
          (object) "SwarmActive"
        }) ? 1 : 0;
      else
        num = 0;
      WorldSavingSystem.SwarmActive = num != 0;
    }

    public virtual void PostUpdateWorld()
    {
      if (!WorldSavingSystem.PlacedMutantStatue && (Main.zenithWorld || Main.remixWorld))
      {
        int spawnTileX = Main.spawnTileX;
        int spawnTileY = Main.spawnTileY;
        int num1 = -30;
        int num2 = 10;
        bool flag = false;
        for (int index1 = -50; index1 <= 50; ++index1)
        {
          for (int index2 = num1; index2 <= num2; ++index2)
          {
            if (WorldGenSystem.TryPlacingStatue(spawnTileX + index1, spawnTileY + index2))
            {
              flag = true;
              WorldSavingSystem.PlacedMutantStatue = true;
              break;
            }
          }
          if (flag)
            break;
        }
      }
      DefaultInterpolatedStringHandler interpolatedStringHandler;
      if (WorldSavingSystem.ShouldBeEternityMode)
      {
        if (WorldSavingSystem.EternityMode && !FargoSoulsUtil.WorldIsExpertOrHarder())
        {
          WorldSavingSystem.EternityMode = false;
          interpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 2);
          interpolatedStringHandler.AppendLiteral("Mods.");
          interpolatedStringHandler.AppendFormatted(((ModType) this).Mod.Name);
          interpolatedStringHandler.AppendLiteral(".Message.");
          interpolatedStringHandler.AppendFormatted(((ModType) this).Name);
          interpolatedStringHandler.AppendLiteral(".EternityWrongDifficulty");
          FargoSoulsUtil.PrintLocalization(interpolatedStringHandler.ToStringAndClear(), new Color(175, 75, (int) byte.MaxValue));
          if (Main.netMode == 2)
            NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          if (!Main.dedServ)
            SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) Main.LocalPlayer).Center), (SoundUpdateCallback) null);
        }
        else if (!WorldSavingSystem.EternityMode && FargoSoulsUtil.WorldIsExpertOrHarder() && !Luminance.Common.Utilities.Utilities.AnyBosses())
        {
          WorldSavingSystem.EternityMode = true;
          interpolatedStringHandler = new DefaultInterpolatedStringHandler(25, 2);
          interpolatedStringHandler.AppendLiteral("Mods.");
          interpolatedStringHandler.AppendFormatted(((ModType) this).Mod.Name);
          interpolatedStringHandler.AppendLiteral(".Message.");
          interpolatedStringHandler.AppendFormatted(((ModType) this).Name);
          interpolatedStringHandler.AppendLiteral(".EternityOn");
          FargoSoulsUtil.PrintLocalization(interpolatedStringHandler.ToStringAndClear(), new Color(175, 75, (int) byte.MaxValue));
          if (Main.masterMode && !WorldSavingSystem.CanPlayMaso)
          {
            interpolatedStringHandler = new DefaultInterpolatedStringHandler(36, 2);
            interpolatedStringHandler.AppendLiteral("Mods.");
            interpolatedStringHandler.AppendFormatted(((ModType) this).Mod.Name);
            interpolatedStringHandler.AppendLiteral(".Message.");
            interpolatedStringHandler.AppendFormatted(((ModType) this).Name);
            interpolatedStringHandler.AppendLiteral(".EternityMasterWarning");
            FargoSoulsUtil.PrintLocalization(interpolatedStringHandler.ToStringAndClear(), new Color((int) byte.MaxValue, (int) byte.MaxValue, 0));
          }
          if (Main.netMode == 2)
            NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          if (!Main.dedServ)
            SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) Main.LocalPlayer).Center), (SoundUpdateCallback) null);
        }
      }
      else if (WorldSavingSystem.EternityMode)
      {
        WorldSavingSystem.EternityMode = false;
        interpolatedStringHandler = new DefaultInterpolatedStringHandler(26, 2);
        interpolatedStringHandler.AppendLiteral("Mods.");
        interpolatedStringHandler.AppendFormatted(((ModType) this).Mod.Name);
        interpolatedStringHandler.AppendLiteral(".Message.");
        interpolatedStringHandler.AppendFormatted(((ModType) this).Name);
        interpolatedStringHandler.AppendLiteral(".EternityOff");
        FargoSoulsUtil.PrintLocalization(interpolatedStringHandler.ToStringAndClear(), new Color(175, 75, (int) byte.MaxValue));
        if (Main.netMode == 2)
          NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        if (!Main.dedServ)
          SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) Main.LocalPlayer).Center), (SoundUpdateCallback) null);
      }
      if (WorldSavingSystem.EternityMode)
      {
        ModNPC modNpc;
        if (Main.bloodMoon && !WorldSavingSystem.HaveForcedAbomFromGoblins && !WorldSavingSystem.DownedAnyBoss && ModContent.TryFind<ModNPC>("Fargowiltas", "Abominationn", ref modNpc) && !NPC.AnyNPCs(modNpc.Type))
        {
          interpolatedStringHandler = new DefaultInterpolatedStringHandler(30, 2);
          interpolatedStringHandler.AppendLiteral("Mods.");
          interpolatedStringHandler.AppendFormatted(((ModType) this).Mod.Name);
          interpolatedStringHandler.AppendLiteral(".Message.");
          interpolatedStringHandler.AppendFormatted(((ModType) this).Name);
          interpolatedStringHandler.AppendLiteral(".BloodMoonCancel");
          FargoSoulsUtil.PrintLocalization(interpolatedStringHandler.ToStringAndClear(), new Color(175, 75, (int) byte.MaxValue));
          Main.bloodMoon = false;
          if (Main.netMode == 2)
            NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        }
        if (!WorldSavingSystem.MasochistModeReal && WorldSavingSystem.EternityMode && (FargoSoulsUtil.WorldIsMaster() && WorldSavingSystem.CanPlayMaso || Main.zenithWorld) && !Luminance.Common.Utilities.Utilities.AnyBosses())
        {
          WorldSavingSystem.MasochistModeReal = true;
          interpolatedStringHandler = new DefaultInterpolatedStringHandler(26, 3);
          interpolatedStringHandler.AppendLiteral("Mods.");
          interpolatedStringHandler.AppendFormatted(((ModType) this).Mod.Name);
          interpolatedStringHandler.AppendLiteral(".Message.");
          interpolatedStringHandler.AppendFormatted(((ModType) this).Name);
          interpolatedStringHandler.AppendLiteral(".MasochistOn");
          interpolatedStringHandler.AppendFormatted(Main.zenithWorld ? "Zenith" : "");
          FargoSoulsUtil.PrintLocalization(interpolatedStringHandler.ToStringAndClear(), new Color(51, (int) byte.MaxValue, 191, 0));
          if (Main.getGoodWorld)
          {
            interpolatedStringHandler = new DefaultInterpolatedStringHandler(34, 2);
            interpolatedStringHandler.AppendLiteral("Mods.");
            interpolatedStringHandler.AppendFormatted(((ModType) this).Mod.Name);
            interpolatedStringHandler.AppendLiteral(".Message.");
            interpolatedStringHandler.AppendFormatted(((ModType) this).Name);
            interpolatedStringHandler.AppendLiteral(".MasochistFTWWarning");
            FargoSoulsUtil.PrintLocalization(interpolatedStringHandler.ToStringAndClear(), new Color(51, (int) byte.MaxValue, 191, 0));
          }
          if (Main.netMode == 2)
            NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          if (!Main.dedServ)
            SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) Main.LocalPlayer).Center), (SoundUpdateCallback) null);
        }
      }
      if (!WorldSavingSystem.MasochistModeReal || WorldSavingSystem.EternityMode && (FargoSoulsUtil.WorldIsMaster() && WorldSavingSystem.CanPlayMaso || Main.zenithWorld))
        return;
      WorldSavingSystem.MasochistModeReal = false;
      interpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 2);
      interpolatedStringHandler.AppendLiteral("Mods.");
      interpolatedStringHandler.AppendFormatted(((ModType) this).Mod.Name);
      interpolatedStringHandler.AppendLiteral(".Message.");
      interpolatedStringHandler.AppendFormatted(((ModType) this).Name);
      interpolatedStringHandler.AppendLiteral(".MasochistOff");
      FargoSoulsUtil.PrintLocalization(interpolatedStringHandler.ToStringAndClear(), new Color(51, (int) byte.MaxValue, 191, 0));
      if (Main.netMode == 2)
        NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      if (Main.dedServ)
        return;
      SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) Main.LocalPlayer).Center), (SoundUpdateCallback) null);
    }
  }
}
