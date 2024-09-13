// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.Systems.WorldSavingSystem
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Core.Systems
{
  public class WorldSavingSystem : ModSystem
  {
    public const int MaxCountPreHM = 560;
    public const int MaxCountHM = 240;
    internal static bool swarmActive;
    internal static bool downedBetsy;
    internal static bool shouldBeEternityMode;
    internal static bool masochistModeReal;
    internal static bool canPlayMaso;
    internal static bool downedFishronEX;
    internal static bool downedDevi;
    internal static bool downedAbom;
    internal static bool downedMutant;
    internal static bool angryMutant;
    internal static bool haveForcedAbomFromGoblins;
    internal static int skipMutantP1;
    internal static bool receivedTerraStorage;
    internal static bool spawnedDevi;
    internal static bool downedAnyBoss;
    internal static bool[] downedBoss = new bool[Enum.GetValues(typeof (WorldSavingSystem.Downed)).Length];
    internal static bool wOFDroppedDeviGift2;
    public static bool PlacedMutantStatue;
    public static List<int> IronUsedList = new List<int>();

    public static bool EternityMode { get; set; }

    public static bool EternityVanillaBehaviour { get; set; }

    public static bool DownedAnyBoss
    {
      get => WorldSavingSystem.downedAnyBoss;
      set => WorldSavingSystem.downedAnyBoss = value;
    }

    public static int SkipMutantP1
    {
      get => WorldSavingSystem.skipMutantP1;
      set => WorldSavingSystem.skipMutantP1 = value;
    }

    public static bool WOFDroppedDeviGift2
    {
      get => WorldSavingSystem.wOFDroppedDeviGift2;
      set => WorldSavingSystem.wOFDroppedDeviGift2 = value;
    }

    public static bool[] DownedBoss
    {
      get => WorldSavingSystem.downedBoss;
      set => WorldSavingSystem.downedBoss = value;
    }

    public static bool ShouldBeEternityMode
    {
      get => WorldSavingSystem.shouldBeEternityMode;
      set => WorldSavingSystem.shouldBeEternityMode = value;
    }

    public static bool MasochistModeReal
    {
      get => WorldSavingSystem.masochistModeReal;
      set => WorldSavingSystem.masochistModeReal = value;
    }

    public static bool CanPlayMaso
    {
      get => WorldSavingSystem.canPlayMaso;
      set => WorldSavingSystem.canPlayMaso = value;
    }

    public static bool DownedFishronEX
    {
      get => WorldSavingSystem.downedFishronEX;
      set => WorldSavingSystem.downedFishronEX = value;
    }

    public static bool DownedDevi
    {
      get => WorldSavingSystem.downedDevi;
      set => WorldSavingSystem.downedDevi = value;
    }

    public static bool DownedAbom
    {
      get => WorldSavingSystem.downedAbom;
      set => WorldSavingSystem.downedAbom = value;
    }

    public static bool DownedMutant
    {
      get => WorldSavingSystem.downedMutant;
      set => WorldSavingSystem.downedMutant = value;
    }

    public static bool AngryMutant
    {
      get => WorldSavingSystem.angryMutant;
      set => WorldSavingSystem.angryMutant = value;
    }

    public static bool HaveForcedAbomFromGoblins
    {
      get => WorldSavingSystem.haveForcedAbomFromGoblins;
      set => WorldSavingSystem.haveForcedAbomFromGoblins = value;
    }

    public static bool ReceivedTerraStorage
    {
      get => WorldSavingSystem.receivedTerraStorage;
      set => WorldSavingSystem.receivedTerraStorage = value;
    }

    public static bool SpawnedDevi
    {
      get => WorldSavingSystem.spawnedDevi;
      set => WorldSavingSystem.spawnedDevi = value;
    }

    public static bool DownedBetsy
    {
      get => WorldSavingSystem.downedBetsy;
      set => WorldSavingSystem.downedBetsy = value;
    }

    public static bool SwarmActive
    {
      get => WorldSavingSystem.swarmActive;
      set => WorldSavingSystem.swarmActive = value;
    }

    public virtual void Unload() => WorldSavingSystem.DownedBoss = (bool[]) null;

    private static void ResetFlags()
    {
      WorldSavingSystem.SwarmActive = false;
      WorldSavingSystem.DownedBetsy = false;
      WorldSavingSystem.ShouldBeEternityMode = false;
      WorldSavingSystem.EternityMode = false;
      WorldSavingSystem.EternityVanillaBehaviour = true;
      WorldSavingSystem.CanPlayMaso = false;
      WorldSavingSystem.MasochistModeReal = false;
      WorldSavingSystem.DownedFishronEX = false;
      WorldSavingSystem.DownedDevi = false;
      WorldSavingSystem.DownedAbom = false;
      WorldSavingSystem.DownedMutant = false;
      WorldSavingSystem.AngryMutant = false;
      WorldSavingSystem.HaveForcedAbomFromGoblins = false;
      WorldSavingSystem.SkipMutantP1 = 0;
      WorldSavingSystem.ReceivedTerraStorage = false;
      WorldSavingSystem.SpawnedDevi = false;
      for (int index = 0; index < WorldSavingSystem.DownedBoss.Length; ++index)
        WorldSavingSystem.DownedBoss[index] = false;
      WorldSavingSystem.DownedAnyBoss = false;
      WorldSavingSystem.WOFDroppedDeviGift2 = false;
      WorldSavingSystem.PlacedMutantStatue = false;
    }

    public virtual void OnWorldLoad() => WorldSavingSystem.ResetFlags();

    public virtual void OnWorldUnload() => WorldSavingSystem.ResetFlags();

    public virtual void SaveWorldData(TagCompound tag)
    {
      List<string> stringList = new List<string>();
      if (WorldSavingSystem.DownedBetsy)
        stringList.Add("betsy");
      if (WorldSavingSystem.ShouldBeEternityMode)
        stringList.Add("shouldBeEternityMode");
      if (WorldSavingSystem.EternityMode)
        stringList.Add("eternity");
      if (WorldSavingSystem.CanPlayMaso)
        stringList.Add("CanPlayMaso");
      if (WorldSavingSystem.MasochistModeReal)
        stringList.Add("getReal");
      if (WorldSavingSystem.DownedFishronEX)
        stringList.Add("downedFishronEX");
      if (WorldSavingSystem.DownedDevi)
        stringList.Add("downedDevi");
      if (WorldSavingSystem.DownedAbom)
        stringList.Add("downedAbom");
      if (WorldSavingSystem.DownedMutant)
        stringList.Add("downedMutant");
      if (WorldSavingSystem.AngryMutant)
        stringList.Add("AngryMutant");
      if (WorldSavingSystem.HaveForcedAbomFromGoblins)
        stringList.Add("haveForcedAbomFromGoblins");
      if (WorldSavingSystem.ReceivedTerraStorage)
        stringList.Add("ReceivedTerraStorage");
      if (WorldSavingSystem.SpawnedDevi)
        stringList.Add("spawnedDevi");
      if (WorldSavingSystem.DownedAnyBoss)
        stringList.Add("downedAnyBoss");
      if (WorldSavingSystem.WOFDroppedDeviGift2)
        stringList.Add("WOFDroppedDeviGift2");
      if (WorldSavingSystem.PlacedMutantStatue)
        stringList.Add("PlacedMutantStatue");
      if (WorldSavingSystem.IronUsedList.Count > 0)
      {
        string str1 = "IronUsedList";
        foreach (int ironUsed in WorldSavingSystem.IronUsedList)
        {
          if (ironUsed >= (int) ItemID.Count)
          {
            str1 = str1 + "_" + ((ModType) ItemLoader.GetItem(ironUsed)).FullName;
          }
          else
          {
            string str2 = str1;
            DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
            interpolatedStringHandler.AppendLiteral("_");
            interpolatedStringHandler.AppendFormatted<int>(ironUsed);
            string stringAndClear = interpolatedStringHandler.ToStringAndClear();
            str1 = str2 + stringAndClear;
          }
        }
        stringList.Add(str1);
      }
      for (int index = 0; index < WorldSavingSystem.DownedBoss.Length; ++index)
      {
        if (WorldSavingSystem.DownedBoss[index])
          stringList.Add("downedBoss" + index.ToString());
      }
      tag.Add("downed", (object) stringList);
      tag.Add("mutantP1", (object) WorldSavingSystem.SkipMutantP1);
    }

    public virtual void LoadWorldData(TagCompound tag)
    {
      IList<string> list = tag.GetList<string>("downed");
      WorldSavingSystem.DownedBetsy = list.Contains("betsy");
      WorldSavingSystem.ShouldBeEternityMode = list.Contains("shouldBeEternityMode");
      WorldSavingSystem.EternityMode = list.Contains("eternity") || list.Contains("masochist");
      WorldSavingSystem.EternityVanillaBehaviour = true;
      WorldSavingSystem.CanPlayMaso = list.Contains("CanPlayMaso");
      WorldSavingSystem.MasochistModeReal = list.Contains("getReal");
      WorldSavingSystem.DownedFishronEX = list.Contains("downedFishronEX");
      WorldSavingSystem.DownedDevi = list.Contains("downedDevi");
      WorldSavingSystem.DownedAbom = list.Contains("downedAbom");
      WorldSavingSystem.DownedMutant = list.Contains("downedMutant");
      WorldSavingSystem.AngryMutant = list.Contains("AngryMutant");
      WorldSavingSystem.HaveForcedAbomFromGoblins = list.Contains("haveForcedAbomFromGoblins");
      WorldSavingSystem.ReceivedTerraStorage = list.Contains("ReceivedTerraStorage");
      WorldSavingSystem.SpawnedDevi = list.Contains("spawnedDevi");
      WorldSavingSystem.DownedAnyBoss = list.Contains("downedAnyBoss");
      WorldSavingSystem.WOFDroppedDeviGift2 = list.Contains("WOFDroppedDeviGift2");
      WorldSavingSystem.PlacedMutantStatue = list.Contains("PlacedMutantStatue");
      if (list.Contains("IronUsedList_"))
      {
        foreach (string s in list.First<string>((Func<string, bool>) (i => i.Contains("IronUsedList"))).Split("_", StringSplitOptions.None))
        {
          if (s != "IronUsedList")
          {
            int result;
            if (int.TryParse(s, out result))
            {
              WorldSavingSystem.IronUsedList.Add(result);
            }
            else
            {
              ModItem modItem = ModContent.Find<ModItem>(s);
              WorldSavingSystem.IronUsedList.Add(modItem.Type);
            }
          }
        }
      }
      for (int index1 = 0; index1 < WorldSavingSystem.DownedBoss.Length; ++index1)
      {
        bool[] downedBoss = WorldSavingSystem.DownedBoss;
        int index2 = index1;
        IList<string> stringList1 = list;
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 1);
        interpolatedStringHandler.AppendLiteral("downedBoss");
        interpolatedStringHandler.AppendFormatted<int>(index1);
        string stringAndClear1 = interpolatedStringHandler.ToStringAndClear();
        int num;
        if (!stringList1.Contains(stringAndClear1))
        {
          IList<string> stringList2 = list;
          interpolatedStringHandler = new DefaultInterpolatedStringHandler(14, 1);
          interpolatedStringHandler.AppendLiteral("downedChampion");
          interpolatedStringHandler.AppendFormatted<int>(index1);
          string stringAndClear2 = interpolatedStringHandler.ToStringAndClear();
          num = stringList2.Contains(stringAndClear2) ? 1 : 0;
        }
        else
          num = 1;
        downedBoss[index2] = num != 0;
      }
      if (!tag.ContainsKey("mutantP1"))
        return;
      WorldSavingSystem.SkipMutantP1 = tag.GetAsInt("mutantP1");
    }

    public virtual void NetReceive(BinaryReader reader)
    {
      WorldSavingSystem.SkipMutantP1 = reader.ReadInt32();
      BitsByte bitsByte1 = BitsByte.op_Implicit(reader.ReadByte());
      WorldSavingSystem.DownedBetsy = ((BitsByte) ref bitsByte1)[0];
      WorldSavingSystem.EternityMode = ((BitsByte) ref bitsByte1)[1];
      WorldSavingSystem.DownedFishronEX = ((BitsByte) ref bitsByte1)[2];
      WorldSavingSystem.DownedDevi = ((BitsByte) ref bitsByte1)[3];
      WorldSavingSystem.DownedAbom = ((BitsByte) ref bitsByte1)[4];
      WorldSavingSystem.DownedMutant = ((BitsByte) ref bitsByte1)[5];
      WorldSavingSystem.AngryMutant = ((BitsByte) ref bitsByte1)[6];
      WorldSavingSystem.HaveForcedAbomFromGoblins = ((BitsByte) ref bitsByte1)[7];
      BitsByte bitsByte2 = BitsByte.op_Implicit(reader.ReadByte());
      WorldSavingSystem.ReceivedTerraStorage = ((BitsByte) ref bitsByte2)[0];
      WorldSavingSystem.SpawnedDevi = ((BitsByte) ref bitsByte2)[1];
      WorldSavingSystem.MasochistModeReal = ((BitsByte) ref bitsByte2)[2];
      WorldSavingSystem.CanPlayMaso = ((BitsByte) ref bitsByte2)[3];
      WorldSavingSystem.ShouldBeEternityMode = ((BitsByte) ref bitsByte2)[4];
      WorldSavingSystem.DownedAnyBoss = ((BitsByte) ref bitsByte2)[5];
      WorldSavingSystem.WOFDroppedDeviGift2 = ((BitsByte) ref bitsByte2)[6];
      WorldSavingSystem.PlacedMutantStatue = ((BitsByte) ref bitsByte2)[7];
      for (int index = 0; index < WorldSavingSystem.DownedBoss.Length; ++index)
      {
        int num = index % 8;
        if (num == 0)
          bitsByte2 = BitsByte.op_Implicit(reader.ReadByte());
        WorldSavingSystem.DownedBoss[index] = ((BitsByte) ref bitsByte2)[num];
      }
    }

    public virtual void NetSend(BinaryWriter writer)
    {
      writer.Write(WorldSavingSystem.SkipMutantP1);
      BinaryWriter binaryWriter1 = writer;
      BitsByte bitsByte1 = new BitsByte();
      ((BitsByte) ref bitsByte1)[0] = WorldSavingSystem.DownedBetsy;
      ((BitsByte) ref bitsByte1)[1] = WorldSavingSystem.EternityMode;
      ((BitsByte) ref bitsByte1)[2] = WorldSavingSystem.DownedFishronEX;
      ((BitsByte) ref bitsByte1)[3] = WorldSavingSystem.DownedDevi;
      ((BitsByte) ref bitsByte1)[4] = WorldSavingSystem.DownedAbom;
      ((BitsByte) ref bitsByte1)[5] = WorldSavingSystem.DownedMutant;
      ((BitsByte) ref bitsByte1)[6] = WorldSavingSystem.AngryMutant;
      ((BitsByte) ref bitsByte1)[7] = WorldSavingSystem.HaveForcedAbomFromGoblins;
      int num1 = (int) BitsByte.op_Implicit(bitsByte1);
      binaryWriter1.Write((byte) num1);
      BinaryWriter binaryWriter2 = writer;
      BitsByte bitsByte2 = new BitsByte();
      ((BitsByte) ref bitsByte2)[0] = WorldSavingSystem.ReceivedTerraStorage;
      ((BitsByte) ref bitsByte2)[1] = WorldSavingSystem.SpawnedDevi;
      ((BitsByte) ref bitsByte2)[2] = WorldSavingSystem.MasochistModeReal;
      ((BitsByte) ref bitsByte2)[3] = WorldSavingSystem.CanPlayMaso;
      ((BitsByte) ref bitsByte2)[4] = WorldSavingSystem.ShouldBeEternityMode;
      ((BitsByte) ref bitsByte2)[5] = WorldSavingSystem.DownedAnyBoss;
      ((BitsByte) ref bitsByte2)[6] = WorldSavingSystem.WOFDroppedDeviGift2;
      ((BitsByte) ref bitsByte2)[7] = WorldSavingSystem.PlacedMutantStatue;
      int num2 = (int) BitsByte.op_Implicit(bitsByte2);
      binaryWriter2.Write((byte) num2);
      BitsByte bitsByte3 = new BitsByte();
      for (int index = 0; index < WorldSavingSystem.DownedBoss.Length; ++index)
      {
        int num3 = index % 8;
        if (num3 == 0 && index != 0)
        {
          writer.Write(BitsByte.op_Implicit(bitsByte3));
          bitsByte3 = new BitsByte();
        }
        ((BitsByte) ref bitsByte3)[num3] = WorldSavingSystem.DownedBoss[index];
      }
      writer.Write(BitsByte.op_Implicit(bitsByte3));
    }

    internal enum Downed
    {
      TimberChampion,
      TerraChampion,
      EarthChampion,
      NatureChampion,
      LifeChampion,
      ShadowChampion,
      SpiritChampion,
      WillChampion,
      CosmosChampion,
      TrojanSquirrel,
      Lifelight,
      CursedCoffin,
      BanishedBaron,
      Magmaw,
    }
  }
}
