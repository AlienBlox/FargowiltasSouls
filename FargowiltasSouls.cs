// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.FargowiltasSouls
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.Champions.Cosmos;
using FargowiltasSouls.Content.Bosses.Champions.Earth;
using FargowiltasSouls.Content.Bosses.Champions.Life;
using FargowiltasSouls.Content.Bosses.Champions.Nature;
using FargowiltasSouls.Content.Bosses.Champions.Shadow;
using FargowiltasSouls.Content.Bosses.Champions.Spirit;
using FargowiltasSouls.Content.Bosses.Champions.Terra;
using FargowiltasSouls.Content.Bosses.Champions.Timber;
using FargowiltasSouls.Content.Bosses.Champions.Will;
using FargowiltasSouls.Content.Bosses.Lifelight;
using FargowiltasSouls.Content.Bosses.VanillaEternity;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Dyes;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.Items.Misc;
using FargowiltasSouls.Content.Items.Pets;
using FargowiltasSouls.Content.Items.Placables.MusicBoxes;
using FargowiltasSouls.Content.Items.Placables.Trophies;
using FargowiltasSouls.Content.Items.Summons;
using FargowiltasSouls.Content.Items.Weapons.Challengers;
using FargowiltasSouls.Content.NPCs.EternityModeNPCs;
using FargowiltasSouls.Content.Patreon.Volknet;
using FargowiltasSouls.Content.Sky;
using FargowiltasSouls.Content.Tiles;
using FargowiltasSouls.Content.UI;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Systems;
using FargowiltasSouls.Core.Toggler;
using Luminance.Core.ModCalls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

#nullable disable
namespace FargowiltasSouls
{
  public class FargowiltasSouls : Mod
  {
    internal static ModKeybind FreezeKey;
    internal static ModKeybind GoldKey;
    internal static ModKeybind SmokeBombKey;
    internal static ModKeybind SpecialDashKey;
    internal static ModKeybind BombKey;
    internal static ModKeybind SoulToggleKey;
    internal static ModKeybind PrecisionSealKey;
    internal static ModKeybind MagicalBulbKey;
    internal static ModKeybind FrigidSpellKey;
    internal static ModKeybind DebuffInstallKey;
    internal static ModKeybind AmmoCycleKey;
    internal static List<int> DebuffIDs;
    internal static FargowiltasSouls.FargowiltasSouls Instance;
    internal bool LoadedNewSprites;
    internal static float OldMusicFade;
    public UserInterface CustomResources;
    internal static Dictionary<int, int> ModProjDict = new Dictionary<int, int>();
    private static float ColorTimer;
    public Dictionary<string, float> BossChecklistValues = new Dictionary<string, float>()
    {
      {
        "DeviBoss",
        6.9f
      },
      {
        "AbomBoss",
        20f
      },
      {
        "MutantBoss",
        23f
      },
      {
        "TimberChampion",
        18.1f
      },
      {
        "TerraChampion",
        18.15f
      },
      {
        "EarthChampion",
        18.2f
      },
      {
        "NatureChampion",
        18.25f
      },
      {
        "LifeChampion",
        18.3f
      },
      {
        "ShadowChampion",
        18.35f
      },
      {
        "SpiritChampion",
        18.4f
      },
      {
        "WillChampion",
        18.45f
      },
      {
        "CosmosChampion",
        18.5f
      },
      {
        "TrojanSquirrel",
        0.5f
      },
      {
        "LifeChallenger",
        11.49f
      },
      {
        "BanishedBaron",
        8.7f
      },
      {
        "CursedCoffin",
        2.1f
      }
    };

    public virtual void Load()
    {
      FargowiltasSouls.FargowiltasSouls.Instance = this;
      ((EffectManager<CustomSky>) SkyManager.Instance)["FargowiltasSouls:AbomBoss"] = (CustomSky) new AbomSky();
      ((EffectManager<CustomSky>) SkyManager.Instance)["FargowiltasSouls:MutantBoss"] = (CustomSky) new MutantSky();
      ((EffectManager<CustomSky>) SkyManager.Instance)["FargowiltasSouls:MutantBoss2"] = (CustomSky) new MutantSky2();
      ((EffectManager<CustomSky>) SkyManager.Instance)["FargowiltasSouls:MoonLordSky"] = (CustomSky) new MoonLordSky();
      FargowiltasSouls.FargowiltasSouls.FreezeKey = KeybindLoader.RegisterKeybind((Mod) this, "Freeze", "P");
      FargowiltasSouls.FargowiltasSouls.GoldKey = KeybindLoader.RegisterKeybind((Mod) this, "Gold", "O");
      FargowiltasSouls.FargowiltasSouls.SmokeBombKey = KeybindLoader.RegisterKeybind((Mod) this, "SmokeBomb", "I");
      FargowiltasSouls.FargowiltasSouls.SpecialDashKey = KeybindLoader.RegisterKeybind((Mod) this, "SpecialDash", "C");
      FargowiltasSouls.FargowiltasSouls.BombKey = KeybindLoader.RegisterKeybind((Mod) this, "Bomb", "Z");
      FargowiltasSouls.FargowiltasSouls.SoulToggleKey = KeybindLoader.RegisterKeybind((Mod) this, "EffectToggle", ".");
      FargowiltasSouls.FargowiltasSouls.PrecisionSealKey = KeybindLoader.RegisterKeybind((Mod) this, "PrecisionSeal", "LeftShift");
      FargowiltasSouls.FargowiltasSouls.MagicalBulbKey = KeybindLoader.RegisterKeybind((Mod) this, "MagicalBulb", "N");
      FargowiltasSouls.FargowiltasSouls.FrigidSpellKey = KeybindLoader.RegisterKeybind((Mod) this, "FrigidSpell", "U");
      FargowiltasSouls.FargowiltasSouls.DebuffInstallKey = KeybindLoader.RegisterKeybind((Mod) this, "DebuffInstall", "Y");
      FargowiltasSouls.FargowiltasSouls.AmmoCycleKey = KeybindLoader.RegisterKeybind((Mod) this, "AmmoCycle", "L");
      ToggleLoader.Load();
      FargoUIManager.LoadUI();
      if (Main.netMode != 2)
      {
        Ref<Effect> ref1 = new Ref<Effect>(this.Assets.Request<Effect>("Assets/Effects/Armor/LifeChampionShader", (AssetRequestMode) 1).Value);
        Ref<Effect> ref2 = new Ref<Effect>(this.Assets.Request<Effect>("Assets/Effects/Armor/WillChampionShader", (AssetRequestMode) 1).Value);
        Ref<Effect> ref3 = new Ref<Effect>(this.Assets.Request<Effect>("Assets/Effects/Armor/GaiaShader", (AssetRequestMode) 1).Value);
        GameShaders.Misc["LCWingShader"] = new MiscShaderData(ref1, "LCWings");
        GameShaders.Armor.BindShader<ArmorShaderData>(ModContent.ItemType<LifeDye>(), new ArmorShaderData(ref1, "LCArmor").UseColor(new Color(1f, 0.647f, 0.839f)).UseSecondaryColor(Color.Goldenrod));
        GameShaders.Misc["WCWingShader"] = new MiscShaderData(ref2, "WCWings");
        GameShaders.Armor.BindShader<ArmorShaderData>(ModContent.ItemType<WillDye>(), new ArmorShaderData(ref2, "WCArmor").UseColor(Color.DarkOrchid).UseSecondaryColor(Color.LightPink).UseImage("Images/Misc/noise"));
        GameShaders.Misc["GaiaShader"] = new MiscShaderData(ref3, "GaiaGlow");
        GameShaders.Armor.BindShader<ArmorShaderData>(ModContent.ItemType<GaiaDye>(), new ArmorShaderData(ref3, "GaiaArmor").UseColor(new Color(0.44f, 1f, 0.09f)).UseSecondaryColor(new Color(0.5f, 1f, 0.9f)));
        ((EffectManager<Filter>) Filters.Scene)["FargowiltasSouls:Solar"] = new Filter(((EffectManager<Filter>) Filters.Scene)["MonolithSolar"].GetShader(), (EffectPriority) 2);
        ((EffectManager<Filter>) Filters.Scene)["FargowiltasSouls:Vortex"] = new Filter(((EffectManager<Filter>) Filters.Scene)["MonolithVortex"].GetShader(), (EffectPriority) 2);
        ((EffectManager<Filter>) Filters.Scene)["FargowiltasSouls:Nebula"] = new Filter(((EffectManager<Filter>) Filters.Scene)["MonolithNebula"].GetShader(), (EffectPriority) 2);
        ((EffectManager<Filter>) Filters.Scene)["FargowiltasSouls:Stardust"] = new Filter(((EffectManager<Filter>) Filters.Scene)["MonolithStardust"].GetShader(), (EffectPriority) 2);
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      On_Player.CheckSpawn_Internal += FargowiltasSouls.FargowiltasSouls.\u003C\u003EO.\u003C0\u003E__LifeRevitalizer_CheckSpawn_Internal ?? (FargowiltasSouls.FargowiltasSouls.\u003C\u003EO.\u003C0\u003E__LifeRevitalizer_CheckSpawn_Internal = new On_Player.hook_CheckSpawn_Internal((object) null, __methodptr(LifeRevitalizer_CheckSpawn_Internal)));
      // ISSUE: method pointer
      On_Player.AddBuff += new On_Player.hook_AddBuff((object) this, __methodptr(AddBuff));
    }

    private static bool LifeRevitalizer_CheckSpawn_Internal(
      On_Player.orig_CheckSpawn_Internal orig,
      int x,
      int y)
    {
      if (orig.Invoke(x, y))
        return true;
      int num1 = ModContent.TileType<LifeRevitalizerPlaced>();
      for (int index1 = -1; index1 <= 1; ++index1)
      {
        for (int index2 = -3; index2 <= -1; ++index2)
        {
          int num2 = x + index1;
          int num3 = y + index2;
          if (!WorldGen.InWorld(num2, num3, 0))
            return false;
          Tile tileSafely = Framing.GetTileSafely(num2, num3);
          if ((int) ((Tile) ref tileSafely).TileType != num1)
            return false;
        }
      }
      return true;
    }

    private void AddBuff(
      On_Player.orig_AddBuff orig,
      Player self,
      int type,
      int timeToAdd,
      bool quiet,
      bool foodHack)
    {
      FargoSoulsPlayer fargoSoulsPlayer = self.FargoSouls();
      if (Main.debuff[type] && timeToAdd > 3 && !Main.buffNoTimeDisplay[type] && !BuffID.Sets.NurseCannotRemoveDebuff[type] && (fargoSoulsPlayer.ParryDebuffImmuneTime > 0 || fargoSoulsPlayer.BetsyDashing || fargoSoulsPlayer.GoldShell || fargoSoulsPlayer.ShellHide || fargoSoulsPlayer.MonkDashing > 0 || fargoSoulsPlayer.CobaltImmuneTimer > 0 || fargoSoulsPlayer.TitaniumDRBuff) && FargowiltasSouls.FargowiltasSouls.DebuffIDs.Contains(type))
        return;
      orig.Invoke(self, type, timeToAdd, quiet, foodHack);
    }

    public virtual void Unload()
    {
      RestoreSprites(FargowiltasSouls.FargowiltasSouls.TextureBuffer.NPC, TextureAssets.Npc);
      RestoreSprites(FargowiltasSouls.FargowiltasSouls.TextureBuffer.NPCHeadBoss, TextureAssets.NpcHeadBoss);
      RestoreSprites(FargowiltasSouls.FargowiltasSouls.TextureBuffer.Gore, TextureAssets.Gore);
      RestoreSprites(FargowiltasSouls.FargowiltasSouls.TextureBuffer.Golem, TextureAssets.Golem);
      RestoreSprites(FargowiltasSouls.FargowiltasSouls.TextureBuffer.Dest, TextureAssets.Dest);
      RestoreSprites(FargowiltasSouls.FargowiltasSouls.TextureBuffer.GlowMask, TextureAssets.GlowMask);
      RestoreSprites(FargowiltasSouls.FargowiltasSouls.TextureBuffer.Extra, TextureAssets.Extra);
      RestoreSprites(FargowiltasSouls.FargowiltasSouls.TextureBuffer.Projectile, TextureAssets.Projectile);
      if (FargowiltasSouls.FargowiltasSouls.TextureBuffer.Ninja != null)
        TextureAssets.Ninja = FargowiltasSouls.FargowiltasSouls.TextureBuffer.Ninja;
      if (FargowiltasSouls.FargowiltasSouls.TextureBuffer.Probe != null)
        TextureAssets.Probe = FargowiltasSouls.FargowiltasSouls.TextureBuffer.Probe;
      if (FargowiltasSouls.FargowiltasSouls.TextureBuffer.BoneArm != null)
        TextureAssets.BoneArm = FargowiltasSouls.FargowiltasSouls.TextureBuffer.BoneArm;
      if (FargowiltasSouls.FargowiltasSouls.TextureBuffer.BoneArm2 != null)
        TextureAssets.BoneArm2 = FargowiltasSouls.FargowiltasSouls.TextureBuffer.BoneArm2;
      if (FargowiltasSouls.FargowiltasSouls.TextureBuffer.BoneLaser != null)
        TextureAssets.BoneLaser = FargowiltasSouls.FargowiltasSouls.TextureBuffer.BoneLaser;
      if (FargowiltasSouls.FargowiltasSouls.TextureBuffer.BoneEyes != null)
        TextureAssets.BoneEyes = FargowiltasSouls.FargowiltasSouls.TextureBuffer.BoneEyes;
      if (FargowiltasSouls.FargowiltasSouls.TextureBuffer.Chain12 != null)
        TextureAssets.Chain12 = FargowiltasSouls.FargowiltasSouls.TextureBuffer.Chain12;
      if (FargowiltasSouls.FargowiltasSouls.TextureBuffer.Chain26 != null)
        TextureAssets.Chain26 = FargowiltasSouls.FargowiltasSouls.TextureBuffer.Chain26;
      if (FargowiltasSouls.FargowiltasSouls.TextureBuffer.Chain27 != null)
        TextureAssets.Chain27 = FargowiltasSouls.FargowiltasSouls.TextureBuffer.Chain27;
      if (FargowiltasSouls.FargowiltasSouls.TextureBuffer.Wof != null)
        TextureAssets.Wof = FargowiltasSouls.FargowiltasSouls.TextureBuffer.Wof;
      ToggleLoader.Unload();
      FargowiltasSouls.FargowiltasSouls.FreezeKey = (ModKeybind) null;
      FargowiltasSouls.FargowiltasSouls.GoldKey = (ModKeybind) null;
      FargowiltasSouls.FargowiltasSouls.SmokeBombKey = (ModKeybind) null;
      FargowiltasSouls.FargowiltasSouls.SpecialDashKey = (ModKeybind) null;
      FargowiltasSouls.FargowiltasSouls.BombKey = (ModKeybind) null;
      FargowiltasSouls.FargowiltasSouls.SoulToggleKey = (ModKeybind) null;
      FargowiltasSouls.FargowiltasSouls.PrecisionSealKey = (ModKeybind) null;
      FargowiltasSouls.FargowiltasSouls.MagicalBulbKey = (ModKeybind) null;
      FargowiltasSouls.FargowiltasSouls.FrigidSpellKey = (ModKeybind) null;
      FargowiltasSouls.FargowiltasSouls.DebuffInstallKey = (ModKeybind) null;
      FargowiltasSouls.FargowiltasSouls.AmmoCycleKey = (ModKeybind) null;
      FargowiltasSouls.FargowiltasSouls.DebuffIDs?.Clear();
      FargowiltasSouls.FargowiltasSouls.ModProjDict.Clear();
      FargowiltasSouls.FargowiltasSouls.Instance = (FargowiltasSouls.FargowiltasSouls) null;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      On_Player.CheckSpawn_Internal -= FargowiltasSouls.FargowiltasSouls.\u003C\u003EO.\u003C0\u003E__LifeRevitalizer_CheckSpawn_Internal ?? (FargowiltasSouls.FargowiltasSouls.\u003C\u003EO.\u003C0\u003E__LifeRevitalizer_CheckSpawn_Internal = new On_Player.hook_CheckSpawn_Internal((object) null, __methodptr(LifeRevitalizer_CheckSpawn_Internal)));
      // ISSUE: method pointer
      On_Player.AddBuff -= new On_Player.hook_AddBuff((object) this, __methodptr(AddBuff));

      static void RestoreSprites(
        Dictionary<int, Asset<Texture2D>> buffer,
        Asset<Texture2D>[] original)
      {
        foreach (KeyValuePair<int, Asset<Texture2D>> keyValuePair in buffer)
          original[keyValuePair.Key] = keyValuePair.Value;
        buffer.Clear();
      }
    }

    public virtual object Call(params object[] args)
    {
      return ModCallManager.ProcessAllModCalls((Mod) this, args);
    }

    public static void DropDevianttsGift(Player player)
    {
      Item.NewItem((IEntitySource) null, ((Entity) player).Center, 3515, 1, false, 0, false, false);
      Item.NewItem((IEntitySource) null, ((Entity) player).Center, 3512, 1, false, 0, false, false);
      Item.NewItem((IEntitySource) null, ((Entity) player).Center, 3511, 1, false, 0, false, false);
      Item.NewItem((IEntitySource) null, ((Entity) player).Center, 148, 1, false, 0, false, false);
      Item.NewItem((IEntitySource) null, ((Entity) player).Center, 8, 200, false, 0, false, false);
      Item.NewItem((IEntitySource) null, ((Entity) player).Center, 28, 15, false, 0, false, false);
      Item.NewItem((IEntitySource) null, ((Entity) player).Center, 2350, 15, false, 0, false, false);
      if (Main.netMode != 0)
        Item.NewItem((IEntitySource) null, ((Entity) player).Center, 2997, 15, false, 0, false, false);
      Item.NewItem((IEntitySource) null, ((Entity) player).Center, ModContent.ItemType<EternityAdvisor>(), 1, false, 0, false, false);
      GiveItem("Fargowiltas", "AutoHouse", 2);
      GiveItem("Fargowiltas", "MiniInstaBridge", 2);
      Item.NewItem((IEntitySource) null, ((Entity) player).Center, ModContent.ItemType<EurusSock>(), 1, false, 0, false, false);
      Item.NewItem((IEntitySource) null, ((Entity) player).Center, ModContent.ItemType<PuffInABottle>(), 1, false, 0, false, false);
      Item.NewItem((IEntitySource) null, ((Entity) player).Center, 1991, 1, false, 0, false, false);
      Item.NewItem((IEntitySource) null, ((Entity) player).Center, 2018, 1, false, 0, false, false);
      if (Main.zenithWorld || Main.remixWorld)
        Item.NewItem((IEntitySource) null, ((Entity) player).Center, 288, 5, false, 0, false, false);
      bool flag = player.name.ToLower().Contains("terry");
      if (flag)
      {
        GiveItem("Fargowiltas", "HalfInstavator");
        GiveItem("Fargowiltas", "RegalStatue");
        Item.NewItem((IEntitySource) null, ((Entity) player).Center, 74, 1, false, 0, false, false);
        Item.NewItem((IEntitySource) null, ((Entity) player).Center, 84, 1, false, 0, false, false);
        Item.NewItem((IEntitySource) null, ((Entity) player).Center, 29, 4, false, 0, false, false);
        Item.NewItem((IEntitySource) null, ((Entity) player).Center, 109, 2, false, 0, false, false);
        Item.NewItem((IEntitySource) null, ((Entity) player).Center, ModContent.ItemType<SandsofTime>(), 1, false, 0, false, false);
      }
      if (WorldSavingSystem.ReceivedTerraStorage)
        return;
      int amount = flag ? 16 : 4;
      Mod mod;
      if (Terraria.ModLoader.ModLoader.TryGetMod("MagicStorage", ref mod))
      {
        GiveItem("MagicStorage", "StorageHeart");
        GiveItem("MagicStorage", "CraftingAccess");
        GiveItem("MagicStorage", "StorageUnit", amount);
        WorldSavingSystem.ReceivedTerraStorage = true;
        if (Main.netMode == 0)
          return;
        NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }
      else
      {
        if (!Terraria.ModLoader.ModLoader.TryGetMod("MagicStorageExtra", ref mod))
          return;
        GiveItem("MagicStorageExtra", "StorageHeart");
        GiveItem("MagicStorageExtra", "CraftingAccess");
        GiveItem("MagicStorageExtra", "StorageUnit", amount);
        WorldSavingSystem.ReceivedTerraStorage = true;
        if (Main.netMode == 0)
          return;
        NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }

      void GiveItem(string modName, string itemName, int amount = 1)
      {
        ModItem modItem;
        if (!ModContent.TryFind<ModItem>(modName, itemName, ref modItem))
          return;
        Item.NewItem((IEntitySource) null, ((Entity) player).Center, modItem.Type, amount, false, 0, false, false);
      }
    }

    public virtual void PostSetupContent()
    {
      try
      {
        Mod mod1;
        if (Terraria.ModLoader.ModLoader.TryGetMod("Wikithis", ref mod1) && !Main.dedServ)
          mod1.Call(new object[3]
          {
            (object) "AddModURL",
            (object) this,
            (object) "https://fargosmods.wiki.gg/wiki/{}"
          });
        List<int> intList = new List<int>();
        CollectionsMarshal.SetCount<int>(intList, 87);
        Span<int> span = CollectionsMarshal.AsSpan<int>(intList);
        int num1 = 0;
        span[num1] = 30;
        int num2 = num1 + 1;
        span[num2] = 24;
        int num3 = num2 + 1;
        span[num3] = 148;
        int num4 = num3 + 1;
        span[num4] = 31;
        int num5 = num4 + 1;
        span[num5] = 33;
        int num6 = num5 + 1;
        span[num6] = 36;
        int num7 = num6 + 1;
        span[num7] = 22;
        int num8 = num7 + 1;
        span[num8] = 32;
        int num9 = num8 + 1;
        span[num9] = 23;
        int num10 = num9 + 1;
        span[num10] = 20;
        int num11 = num10 + 1;
        span[num11] = 35;
        int num12 = num11 + 1;
        span[num12] = 39;
        int num13 = num12 + 1;
        span[num13] = 44;
        int num14 = num13 + 1;
        span[num14] = 46;
        int num15 = num14 + 1;
        span[num15] = 47;
        int num16 = num15 + 1;
        span[num16] = 67;
        int num17 = num16 + 1;
        span[num17] = 68;
        int num18 = num17 + 1;
        span[num18] = 69;
        int num19 = num18 + 1;
        span[num19] = 70;
        int num20 = num19 + 1;
        span[num20] = 80;
        int num21 = num20 + 1;
        span[num21] = 88;
        int num22 = num21 + 1;
        span[num22] = 103;
        int num23 = num22 + 1;
        span[num23] = 137;
        int num24 = num23 + 1;
        span[num24] = 144;
        int num25 = num24 + 1;
        span[num25] = 145;
        int num26 = num25 + 1;
        span[num26] = 149;
        int num27 = num26 + 1;
        span[num27] = 156;
        int num28 = num27 + 1;
        span[num28] = 160;
        int num29 = num28 + 1;
        span[num29] = 163;
        int num30 = num29 + 1;
        span[num30] = 164;
        int num31 = num30 + 1;
        span[num31] = 195;
        int num32 = num31 + 1;
        span[num32] = 196;
        int num33 = num32 + 1;
        span[num33] = 197;
        int num34 = num33 + 1;
        span[num34] = 199;
        int num35 = num34 + 1;
        span[num35] = ModContent.BuffType<AnticoagulationBuff>();
        int num36 = num35 + 1;
        span[num36] = ModContent.BuffType<AntisocialBuff>();
        int num37 = num36 + 1;
        span[num37] = ModContent.BuffType<AtrophiedBuff>();
        int num38 = num37 + 1;
        span[num38] = ModContent.BuffType<BerserkedBuff>();
        int num39 = num38 + 1;
        span[num39] = ModContent.BuffType<BloodthirstyBuff>();
        int num40 = num39 + 1;
        span[num40] = ModContent.BuffType<ClippedWingsBuff>();
        int num41 = num40 + 1;
        span[num41] = ModContent.BuffType<CrippledBuff>();
        int num42 = num41 + 1;
        span[num42] = ModContent.BuffType<CurseoftheMoonBuff>();
        int num43 = num42 + 1;
        span[num43] = ModContent.BuffType<DefenselessBuff>();
        int num44 = num43 + 1;
        span[num44] = ModContent.BuffType<FlamesoftheUniverseBuff>();
        int num45 = num44 + 1;
        span[num45] = ModContent.BuffType<FlippedBuff>();
        int num46 = num45 + 1;
        span[num46] = ModContent.BuffType<FlippedHallowBuff>();
        int num47 = num46 + 1;
        span[num47] = ModContent.BuffType<FusedBuff>();
        int num48 = num47 + 1;
        span[num48] = ModContent.BuffType<GodEaterBuff>();
        int num49 = num48 + 1;
        span[num49] = ModContent.BuffType<GuiltyBuff>();
        int num50 = num49 + 1;
        span[num50] = ModContent.BuffType<HexedBuff>();
        int num51 = num50 + 1;
        span[num51] = ModContent.BuffType<HolyPriceBuff>();
        int num52 = num51 + 1;
        span[num52] = ModContent.BuffType<HypothermiaBuff>();
        int num53 = num52 + 1;
        span[num53] = ModContent.BuffType<InfestedBuff>();
        int num54 = num53 + 1;
        span[num54] = ModContent.BuffType<NeurotoxinBuff>();
        int num55 = num54 + 1;
        span[num55] = ModContent.BuffType<IvyVenomBuff>();
        int num56 = num55 + 1;
        span[num56] = ModContent.BuffType<JammedBuff>();
        int num57 = num56 + 1;
        span[num57] = ModContent.BuffType<LethargicBuff>();
        int num58 = num57 + 1;
        span[num58] = ModContent.BuffType<LightningRodBuff>();
        int num59 = num58 + 1;
        span[num59] = ModContent.BuffType<LihzahrdCurseBuff>();
        int num60 = num59 + 1;
        span[num60] = ModContent.BuffType<LivingWastelandBuff>();
        int num61 = num60 + 1;
        span[num61] = ModContent.BuffType<LovestruckBuff>();
        int num62 = num61 + 1;
        span[num62] = ModContent.BuffType<LowGroundBuff>();
        int num63 = num62 + 1;
        span[num63] = ModContent.BuffType<MarkedforDeathBuff>();
        int num64 = num63 + 1;
        span[num64] = ModContent.BuffType<MidasBuff>();
        int num65 = num64 + 1;
        span[num65] = ModContent.BuffType<MutantNibbleBuff>();
        int num66 = num65 + 1;
        span[num66] = ModContent.BuffType<NanoInjectionBuff>();
        int num67 = num66 + 1;
        span[num67] = ModContent.BuffType<NullificationCurseBuff>();
        int num68 = num67 + 1;
        span[num68] = ModContent.BuffType<OceanicMaulBuff>();
        int num69 = num68 + 1;
        span[num69] = ModContent.BuffType<OceanicSealBuff>();
        int num70 = num69 + 1;
        span[num70] = ModContent.BuffType<OiledBuff>();
        int num71 = num70 + 1;
        span[num71] = ModContent.BuffType<PurgedBuff>();
        int num72 = num71 + 1;
        span[num72] = ModContent.BuffType<PurifiedBuff>();
        int num73 = num72 + 1;
        span[num73] = ModContent.BuffType<RushJobBuff>();
        int num74 = num73 + 1;
        span[num74] = ModContent.BuffType<ReverseManaFlowBuff>();
        int num75 = num74 + 1;
        span[num75] = ModContent.BuffType<RottingBuff>();
        int num76 = num75 + 1;
        span[num76] = ModContent.BuffType<ShadowflameBuff>();
        int num77 = num76 + 1;
        span[num77] = ModContent.BuffType<SmiteBuff>();
        int num78 = num77 + 1;
        span[num78] = ModContent.BuffType<SqueakyToyBuff>();
        int num79 = num78 + 1;
        span[num79] = ModContent.BuffType<StunnedBuff>();
        int num80 = num79 + 1;
        span[num80] = ModContent.BuffType<SwarmingBuff>();
        int num81 = num80 + 1;
        span[num81] = ModContent.BuffType<UnstableBuff>();
        int num82 = num81 + 1;
        span[num82] = ModContent.BuffType<AbomFangBuff>();
        int num83 = num82 + 1;
        span[num83] = ModContent.BuffType<AbomPresenceBuff>();
        int num84 = num83 + 1;
        span[num84] = ModContent.BuffType<MutantFangBuff>();
        int num85 = num84 + 1;
        span[num85] = ModContent.BuffType<MutantPresenceBuff>();
        int num86 = num85 + 1;
        span[num86] = ModContent.BuffType<AbomRebirthBuff>();
        int num87 = num86 + 1;
        span[num87] = ModContent.BuffType<TimeFrozenBuff>();
        int num88 = num87 + 1;
        FargowiltasSouls.FargowiltasSouls.DebuffIDs = intList;
        this.BossChecklistCompatibility();
        Mod mod2 = Terraria.ModLoader.ModLoader.GetMod("Fargowiltas");
        mod2.Call(new object[6]
        {
          (object) "AddSummon",
          (object) 0.5f,
          (object) nameof (FargowiltasSouls),
          (object) "SquirrelCoatofArms",
          (object) (Func<bool>) (() => WorldSavingSystem.DownedBoss[9]),
          (object) Item.buyPrice(0, 4, 0, 0)
        });
        mod2.Call(new object[6]
        {
          (object) "AddSummon",
          (object) 6.9f,
          (object) nameof (FargowiltasSouls),
          (object) "DevisCurse",
          (object) (Func<bool>) (() => WorldSavingSystem.DownedDevi),
          (object) Item.buyPrice(0, 17, 50, 0)
        });
        mod2.Call(new object[6]
        {
          (object) "AddSummon",
          (object) 8.7f,
          (object) nameof (FargowiltasSouls),
          (object) "MechLure",
          (object) (Func<bool>) (() => WorldSavingSystem.DownedBoss[12]),
          (object) Item.buyPrice(0, 22, 0, 0)
        });
        mod2.Call(new object[6]
        {
          (object) "AddSummon",
          (object) 11.49f,
          (object) nameof (FargowiltasSouls),
          (object) "FragilePixieLamp",
          (object) (Func<bool>) (() => WorldSavingSystem.DownedBoss[10]),
          (object) Item.buyPrice(0, 45, 0, 0)
        });
        mod2.Call(new object[6]
        {
          (object) "AddSummon",
          (object) 18.009f,
          (object) nameof (FargowiltasSouls),
          (object) "ChampionySigil",
          (object) (Func<bool>) (() => WorldSavingSystem.DownedBoss[8]),
          (object) Item.buyPrice(5, 0, 0, 0)
        });
        mod2.Call(new object[6]
        {
          (object) "AddSummon",
          (object) (4883f * (float) Math.E / 737f),
          (object) nameof (FargowiltasSouls),
          (object) "AbomsCurse",
          (object) (Func<bool>) (() => WorldSavingSystem.DownedAbom),
          (object) Item.buyPrice(10, 0, 0, 0)
        });
        mod2.Call(new object[6]
        {
          (object) "AddSummon",
          (object) 18.02f,
          (object) nameof (FargowiltasSouls),
          (object) "MutantsCurse",
          (object) (Func<bool>) (() => WorldSavingSystem.DownedMutant),
          (object) Item.buyPrice(20, 0, 0, 0)
        });
      }
      catch (Exception ex)
      {
        this.Logger.Warn((object) ("FargowiltasSouls PostSetupContent Error: " + ex.StackTrace + ex.Message));
      }
    }

    public static void ManageMusicTimestop(bool playMusicAgain)
    {
      if (Main.dedServ)
        return;
      if (playMusicAgain)
      {
        if ((double) FargowiltasSouls.FargowiltasSouls.OldMusicFade <= 0.0)
          return;
        Main.musicFade[Main.curMusic] = FargowiltasSouls.FargowiltasSouls.OldMusicFade;
        FargowiltasSouls.FargowiltasSouls.OldMusicFade = 0.0f;
      }
      else if ((double) FargowiltasSouls.FargowiltasSouls.OldMusicFade == 0.0)
      {
        FargowiltasSouls.FargowiltasSouls.OldMusicFade = Main.musicFade[Main.curMusic];
      }
      else
      {
        for (int index = 0; index < Main.musicFade.Length; ++index)
          Main.musicFade[index] = 0.0f;
      }
    }

    public static Color EModeColor()
    {
      Color color1;
      // ISSUE: explicit constructor call
      ((Color) ref color1).\u002Ector(28, 222, 152);
      Color color2;
      // ISSUE: explicit constructor call
      ((Color) ref color2).\u002Ector((int) byte.MaxValue, 224, 53);
      Color color3;
      // ISSUE: explicit constructor call
      ((Color) ref color3).\u002Ector((int) byte.MaxValue, 51, 153);
      FargowiltasSouls.FargowiltasSouls.ColorTimer += 0.5f;
      if ((double) FargowiltasSouls.FargowiltasSouls.ColorTimer >= 300.0)
        FargowiltasSouls.FargowiltasSouls.ColorTimer = 0.0f;
      if ((double) FargowiltasSouls.FargowiltasSouls.ColorTimer < 100.0)
        return Color.Lerp(color1, color2, FargowiltasSouls.FargowiltasSouls.ColorTimer / 100f);
      return (double) FargowiltasSouls.FargowiltasSouls.ColorTimer < 200.0 ? Color.Lerp(color2, color3, (float) (((double) FargowiltasSouls.FargowiltasSouls.ColorTimer - 100.0) / 100.0)) : Color.Lerp(color3, color1, (float) (((double) FargowiltasSouls.FargowiltasSouls.ColorTimer - 200.0) / 100.0));
    }

    public virtual void HandlePacket(BinaryReader reader, int whoAmI)
    {
      byte num1 = reader.ReadByte();
      if (!Enum.IsDefined(typeof (FargowiltasSouls.FargowiltasSouls.PacketID), (object) num1))
        return;
      switch (num1)
      {
        case 0:
          if (Main.netMode != 2)
            break;
          byte index1 = reader.ReadByte();
          int num2 = (int) reader.ReadByte();
          int index2 = NPC.NewNPC(NPC.GetBossSpawnSource((int) index1), (int) ((Entity) Main.player[(int) index1]).Center.X, (int) ((Entity) Main.player[(int) index1]).Center.Y, ModContent.NPCType<CreeperGutted>(), 0, (float) index1, 0.0f, (float) num2, 0.0f, (int) byte.MaxValue);
          if (index2 == Main.maxNPCs)
            break;
          ((Entity) Main.npc[index2]).velocity = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitX, 2.0 * Math.PI), 8f);
          NetMessage.SendData(23, -1, -1, (NetworkText) null, index2, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          break;
        case 1:
          if (Main.netMode != 2)
            break;
          int index3 = (int) reader.ReadByte();
          int index4 = (int) reader.ReadByte();
          Item.NewItem(((Entity) Main.player[index3]).GetSource_OnHit((Entity) Main.npc[index4], (string) null), ((Entity) Main.npc[index4]).Hitbox, 58, 1, false, 0, false, false);
          break;
        case 2:
          if (Main.netMode != 2)
            break;
          int index5 = (int) reader.ReadByte();
          LunaticCultist globalNpc = Main.npc[index5].GetGlobalNPC<LunaticCultist>();
          globalNpc.MeleeDamageCounter += reader.ReadInt32();
          globalNpc.RangedDamageCounter += reader.ReadInt32();
          globalNpc.MagicDamageCounter += reader.ReadInt32();
          globalNpc.MinionDamageCounter += reader.ReadInt32();
          break;
        case 3:
          if (Main.netMode == 0)
            break;
          byte num3 = reader.ReadByte();
          NPC npc1 = Main.npc[(int) reader.ReadByte()];
          if (!((Entity) npc1).active || npc1.type != ModContent.NPCType<CreeperGutted>() || (double) npc1.ai[0] != (double) num3)
            break;
          int num4 = npc1.lifeMax - npc1.life;
          npc1.life = npc1.lifeMax;
          if (num4 > 0)
            CombatText.NewText(((Entity) npc1).Hitbox, CombatText.HealLife, num4, false, false);
          if (Main.netMode != 2)
            break;
          npc1.netUpdate = true;
          break;
        case 4:
          if (Main.netMode != 2)
            break;
          FargowiltasSouls.FargowiltasSouls.DropDevianttsGift(Main.player[(int) reader.ReadByte()]);
          break;
        case 5:
          if (Main.netMode != 2)
            break;
          byte num5 = reader.ReadByte();
          int num6 = reader.ReadInt32();
          int num7 = reader.ReadInt32();
          EModeGlobalNPC.spawnFishronEX = true;
          NPC.NewNPC(NPC.GetBossSpawnSource((int) num5), num6, num7, 370, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) num5);
          EModeGlobalNPC.spawnFishronEX = false;
          ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasAwoken", new object[1]
          {
            (object) Language.GetTextValue("Mods.FargowiltasSouls.NPCs.DukeFishronEX.DisplayName")
          }), new Color(50, 100, (int) byte.MaxValue), -1);
          break;
        case 6:
          int index6 = reader.ReadInt32();
          Main.npc[index6].lifeMax = reader.ReadInt32();
          break;
        case 7:
          FargoSoulsPlayer fargoSoulsPlayer1 = Main.player[(int) reader.ReadByte()].FargoSouls();
          byte num8 = reader.ReadByte();
          List<AccessoryEffect> list = ToggleLoader.LoadedToggles.Keys.ToList<AccessoryEffect>();
          for (int index7 = 0; index7 < (int) num8; ++index7)
            fargoSoulsPlayer1.Toggler.Toggles[list[index7]].ToggleBool = reader.ReadBoolean();
          break;
        case 8:
          Main.player[(int) reader.ReadByte()].SetToggleValue(AccessoryEffectLoader.EffectType(reader.ReadString()), reader.ReadBoolean());
          break;
        case 9:
          FargoSoulsPlayer fargoSoulsPlayer2 = Main.player[(int) reader.ReadByte()].FargoSouls();
          fargoSoulsPlayer2.Toggler_ExtraAttacksDisabled = reader.ReadBoolean();
          fargoSoulsPlayer2.Toggler_MinionsDisabled = reader.ReadBoolean();
          break;
        case 10:
          if (Main.netMode != 2)
            break;
          WorldSavingSystem.CanPlayMaso = reader.ReadBoolean();
          break;
        case 11:
          Main.player[(int) reader.ReadByte()].GetModPlayer<NanoPlayer>().NanoCoreMode = reader.Read7BitEncodedInt();
          break;
        case 12:
          NPC npc2 = FargoSoulsUtil.NPCExists((int) reader.ReadByte(), Array.Empty<int>());
          int num9 = reader.ReadInt32();
          if (npc2 == null)
            break;
          npc2.life += num9;
          if (npc2.life > npc2.lifeMax)
            npc2.life = npc2.lifeMax;
          npc2.HealEffect(num9, true);
          npc2.netUpdate = true;
          break;
      }
    }

    public static bool NoInvasion(NPCSpawnInfo spawnInfo)
    {
      if (spawnInfo.Invasion || (Main.pumpkinMoon || Main.snowMoon) && (double) spawnInfo.SpawnTileY <= Main.worldSurface && !Main.dayTime)
        return false;
      return !Main.eclipse || (double) spawnInfo.SpawnTileY > Main.worldSurface || !Main.dayTime;
    }

    public static bool NoBiome(NPCSpawnInfo spawnInfo)
    {
      Player player = spawnInfo.Player;
      return !player.ZoneJungle && !player.ZoneDungeon && !player.ZoneCorrupt && !player.ZoneCrimson && !player.ZoneHallow && !player.ZoneSnow && !player.ZoneUndergroundDesert;
    }

    public static bool NoZoneAllowWater(NPCSpawnInfo spawnInfo)
    {
      return !spawnInfo.Sky && !spawnInfo.Player.ZoneMeteor && !spawnInfo.SpiderCave;
    }

    public static bool NoZone(NPCSpawnInfo spawnInfo)
    {
      return FargowiltasSouls.FargowiltasSouls.NoZoneAllowWater(spawnInfo) && !spawnInfo.Water;
    }

    public static bool NormalSpawn(NPCSpawnInfo spawnInfo)
    {
      return !spawnInfo.PlayerInTown && FargowiltasSouls.FargowiltasSouls.NoInvasion(spawnInfo);
    }

    public static bool NoZoneNormalSpawn(NPCSpawnInfo spawnInfo)
    {
      return FargowiltasSouls.FargowiltasSouls.NormalSpawn(spawnInfo) && FargowiltasSouls.FargowiltasSouls.NoZone(spawnInfo);
    }

    public static bool NoZoneNormalSpawnAllowWater(NPCSpawnInfo spawnInfo)
    {
      return FargowiltasSouls.FargowiltasSouls.NormalSpawn(spawnInfo) && FargowiltasSouls.FargowiltasSouls.NoZoneAllowWater(spawnInfo);
    }

    public static bool NoBiomeNormalSpawn(NPCSpawnInfo spawnInfo)
    {
      return FargowiltasSouls.FargowiltasSouls.NormalSpawn(spawnInfo) && FargowiltasSouls.FargowiltasSouls.NoBiome(spawnInfo) && FargowiltasSouls.FargowiltasSouls.NoZone(spawnInfo);
    }

    private void BossChecklistCompatibility()
    {
      Mod bossChecklist;
      if (!Terraria.ModLoader.ModLoader.TryGetMod("BossChecklist", ref bossChecklist))
        return;
      Terraria.ModLoader.ModLoader.HasMod("CalamityMod");
      List<int> npcIDs1 = new List<int>();
      CollectionsMarshal.SetCount<int>(npcIDs1, 1);
      Span<int> span = CollectionsMarshal.AsSpan<int>(npcIDs1);
      int num1 = 0;
      span[num1] = ModContent.NPCType<FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss>();
      int num2 = num1 + 1;
      List<int> collectibles1 = new List<int>();
      CollectionsMarshal.SetCount<int>(collectibles1, 5);
      span = CollectionsMarshal.AsSpan<int>(collectibles1);
      int num3 = 0;
      span[num3] = ModContent.ItemType<DeviMusicBox>();
      int num4 = num3 + 1;
      span[num4] = ModContent.ItemType<DeviatingEnergy>();
      int num5 = num4 + 1;
      span[num5] = ModContent.ItemType<DeviTrophy>();
      int num6 = num5 + 1;
      span[num6] = ModContent.ItemType<ChibiHat>();
      int num7 = num6 + 1;
      span[num7] = ModContent.ItemType<BrokenBlade>();
      num2 = num7 + 1;
      List<int> spawnItems1 = new List<int>();
      CollectionsMarshal.SetCount<int>(spawnItems1, 1);
      span = CollectionsMarshal.AsSpan<int>(spawnItems1);
      int num8 = 0;
      span[num8] = ModContent.ItemType<DevisCurse>();
      num2 = num8 + 1;
      Add("Boss", "DeviBoss", npcIDs1, (Func<bool>) (() => WorldSavingSystem.DownedDevi), (Func<bool>) (() => true), collectibles1, spawnItems1, true);
      List<int> npcIDs2 = new List<int>();
      CollectionsMarshal.SetCount<int>(npcIDs2, 1);
      span = CollectionsMarshal.AsSpan<int>(npcIDs2);
      int num9 = 0;
      span[num9] = ModContent.NPCType<FargowiltasSouls.Content.Bosses.AbomBoss.AbomBoss>();
      num2 = num9 + 1;
      List<int> collectibles2 = new List<int>();
      CollectionsMarshal.SetCount<int>(collectibles2, 5);
      span = CollectionsMarshal.AsSpan<int>(collectibles2);
      int num10 = 0;
      span[num10] = ModContent.ItemType<AbomMusicBox>();
      int num11 = num10 + 1;
      span[num11] = ModContent.ItemType<AbomEnergy>();
      int num12 = num11 + 1;
      span[num12] = ModContent.ItemType<AbomTrophy>();
      int num13 = num12 + 1;
      span[num13] = ModContent.ItemType<BabyScythe>();
      int num14 = num13 + 1;
      span[num14] = ModContent.ItemType<BrokenHilt>();
      num2 = num14 + 1;
      List<int> spawnItems2 = new List<int>();
      CollectionsMarshal.SetCount<int>(spawnItems2, 1);
      span = CollectionsMarshal.AsSpan<int>(spawnItems2);
      int num15 = 0;
      span[num15] = ModContent.ItemType<AbomsCurse>();
      num2 = num15 + 1;
      Add("Boss", "AbomBoss", npcIDs2, (Func<bool>) (() => WorldSavingSystem.DownedAbom), (Func<bool>) (() => true), collectibles2, spawnItems2, true);
      List<int> npcIDs3 = new List<int>();
      CollectionsMarshal.SetCount<int>(npcIDs3, 1);
      span = CollectionsMarshal.AsSpan<int>(npcIDs3);
      int num16 = 0;
      span[num16] = ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>();
      num2 = num16 + 1;
      List<int> collectibles3 = new List<int>();
      CollectionsMarshal.SetCount<int>(collectibles3, 5);
      span = CollectionsMarshal.AsSpan<int>(collectibles3);
      int num17 = 0;
      span[num17] = ModContent.ItemType<MutantMusicBox>();
      int num18 = num17 + 1;
      span[num18] = ModContent.ItemType<EternalEnergy>();
      int num19 = num18 + 1;
      span[num19] = ModContent.ItemType<MutantTrophy>();
      int num20 = num19 + 1;
      span[num20] = ModContent.ItemType<SpawnSack>();
      int num21 = num20 + 1;
      span[num21] = ModContent.ItemType<PhantasmalEnergy>();
      num2 = num21 + 1;
      List<int> spawnItems3 = new List<int>();
      CollectionsMarshal.SetCount<int>(spawnItems3, 1);
      span = CollectionsMarshal.AsSpan<int>(spawnItems3);
      int num22 = 0;
      span[num22] = ModContent.ItemType<AbominationnVoodooDoll>();
      num2 = num22 + 1;
      Add("Boss", "MutantBoss", npcIDs3, (Func<bool>) (() => WorldSavingSystem.DownedMutant), (Func<bool>) (() => true), collectibles3, spawnItems3, true);
      List<int> npcIDs4 = new List<int>();
      CollectionsMarshal.SetCount<int>(npcIDs4, 1);
      span = CollectionsMarshal.AsSpan<int>(npcIDs4);
      int num23 = 0;
      span[num23] = ModContent.NPCType<TimberChampion>();
      num2 = num23 + 1;
      List<int> list1 = new List<int>((IEnumerable<int>) BaseForce.EnchantsIn<TimberForce>()).Append<int>(ModContent.ItemType<ChampionMusicBox>()).ToList<int>();
      List<int> spawnItems4 = new List<int>();
      CollectionsMarshal.SetCount<int>(spawnItems4, 1);
      span = CollectionsMarshal.AsSpan<int>(spawnItems4);
      int num24 = 0;
      span[num24] = ModContent.ItemType<SigilOfChampions>();
      num2 = num24 + 1;
      Add("MiniBoss", "TimberChampion", npcIDs4, (Func<bool>) (() => WorldSavingSystem.DownedBoss[0]), (Func<bool>) (() => true), list1, spawnItems4, false);
      List<int> npcIDs5 = new List<int>();
      CollectionsMarshal.SetCount<int>(npcIDs5, 3);
      span = CollectionsMarshal.AsSpan<int>(npcIDs5);
      int num25 = 0;
      span[num25] = ModContent.NPCType<TerraChampion>();
      int num26 = num25 + 1;
      span[num26] = ModContent.NPCType<TerraChampionBody>();
      int num27 = num26 + 1;
      span[num27] = ModContent.NPCType<TerraChampionTail>();
      num2 = num27 + 1;
      List<int> list2 = new List<int>((IEnumerable<int>) BaseForce.EnchantsIn<TerraForce>()).Append<int>(ModContent.ItemType<ChampionMusicBox>()).ToList<int>();
      List<int> spawnItems5 = new List<int>();
      CollectionsMarshal.SetCount<int>(spawnItems5, 1);
      span = CollectionsMarshal.AsSpan<int>(spawnItems5);
      int num28 = 0;
      span[num28] = ModContent.ItemType<SigilOfChampions>();
      num2 = num28 + 1;
      Add("MiniBoss", "TerraChampion", npcIDs5, (Func<bool>) (() => WorldSavingSystem.DownedBoss[1]), (Func<bool>) (() => true), list2, spawnItems5, false, "Content/Bosses/Champions/Terra/TerraChampion_Still");
      List<int> npcIDs6 = new List<int>();
      CollectionsMarshal.SetCount<int>(npcIDs6, 2);
      span = CollectionsMarshal.AsSpan<int>(npcIDs6);
      int num29 = 0;
      span[num29] = ModContent.NPCType<EarthChampion>();
      int num30 = num29 + 1;
      span[num30] = ModContent.NPCType<EarthChampionHand>();
      num2 = num30 + 1;
      List<int> list3 = new List<int>((IEnumerable<int>) BaseForce.EnchantsIn<EarthForce>()).Append<int>(ModContent.ItemType<ChampionMusicBox>()).ToList<int>();
      List<int> spawnItems6 = new List<int>();
      CollectionsMarshal.SetCount<int>(spawnItems6, 1);
      span = CollectionsMarshal.AsSpan<int>(spawnItems6);
      int num31 = 0;
      span[num31] = ModContent.ItemType<SigilOfChampions>();
      num2 = num31 + 1;
      Add("MiniBoss", "EarthChampion", npcIDs6, (Func<bool>) (() => WorldSavingSystem.DownedBoss[2]), (Func<bool>) (() => true), list3, spawnItems6, false, "Content/Bosses/Champions/Earth/EarthChampion_Still");
      List<int> npcIDs7 = new List<int>();
      CollectionsMarshal.SetCount<int>(npcIDs7, 2);
      span = CollectionsMarshal.AsSpan<int>(npcIDs7);
      int num32 = 0;
      span[num32] = ModContent.NPCType<NatureChampion>();
      int num33 = num32 + 1;
      span[num33] = ModContent.NPCType<NatureChampionHead>();
      num2 = num33 + 1;
      List<int> list4 = new List<int>((IEnumerable<int>) BaseForce.EnchantsIn<NatureForce>()).Append<int>(ModContent.ItemType<ChampionMusicBox>()).ToList<int>();
      List<int> spawnItems7 = new List<int>();
      CollectionsMarshal.SetCount<int>(spawnItems7, 1);
      span = CollectionsMarshal.AsSpan<int>(spawnItems7);
      int num34 = 0;
      span[num34] = ModContent.ItemType<SigilOfChampions>();
      num2 = num34 + 1;
      Add("MiniBoss", "NatureChampion", npcIDs7, (Func<bool>) (() => WorldSavingSystem.DownedBoss[3]), (Func<bool>) (() => true), list4, spawnItems7, false, "Content/Bosses/Champions/Nature/NatureChampion_Still");
      List<int> npcIDs8 = new List<int>();
      CollectionsMarshal.SetCount<int>(npcIDs8, 1);
      span = CollectionsMarshal.AsSpan<int>(npcIDs8);
      int num35 = 0;
      span[num35] = ModContent.NPCType<LifeChampion>();
      num2 = num35 + 1;
      List<int> list5 = new List<int>((IEnumerable<int>) BaseForce.EnchantsIn<LifeForce>()).Append<int>(ModContent.ItemType<ChampionMusicBox>()).ToList<int>();
      List<int> spawnItems8 = new List<int>();
      CollectionsMarshal.SetCount<int>(spawnItems8, 1);
      span = CollectionsMarshal.AsSpan<int>(spawnItems8);
      int num36 = 0;
      span[num36] = ModContent.ItemType<SigilOfChampions>();
      num2 = num36 + 1;
      Add("MiniBoss", "LifeChampion", npcIDs8, (Func<bool>) (() => WorldSavingSystem.DownedBoss[4]), (Func<bool>) (() => true), list5, spawnItems8, false, "Content/Bosses/Champions/Life/LifeChampion_Still");
      List<int> npcIDs9 = new List<int>();
      CollectionsMarshal.SetCount<int>(npcIDs9, 1);
      span = CollectionsMarshal.AsSpan<int>(npcIDs9);
      int num37 = 0;
      span[num37] = ModContent.NPCType<ShadowChampion>();
      num2 = num37 + 1;
      List<int> list6 = new List<int>((IEnumerable<int>) BaseForce.EnchantsIn<ShadowForce>()).Append<int>(ModContent.ItemType<ChampionMusicBox>()).ToList<int>();
      List<int> spawnItems9 = new List<int>();
      CollectionsMarshal.SetCount<int>(spawnItems9, 1);
      span = CollectionsMarshal.AsSpan<int>(spawnItems9);
      int num38 = 0;
      span[num38] = ModContent.ItemType<SigilOfChampions>();
      num2 = num38 + 1;
      Add("MiniBoss", "ShadowChampion", npcIDs9, (Func<bool>) (() => WorldSavingSystem.DownedBoss[5]), (Func<bool>) (() => true), list6, spawnItems9, false);
      List<int> npcIDs10 = new List<int>();
      CollectionsMarshal.SetCount<int>(npcIDs10, 2);
      span = CollectionsMarshal.AsSpan<int>(npcIDs10);
      int num39 = 0;
      span[num39] = ModContent.NPCType<SpiritChampion>();
      int num40 = num39 + 1;
      span[num40] = ModContent.NPCType<SpiritChampionHand>();
      num2 = num40 + 1;
      List<int> list7 = new List<int>((IEnumerable<int>) BaseForce.EnchantsIn<SpiritForce>()).Append<int>(ModContent.ItemType<ChampionMusicBox>()).ToList<int>();
      List<int> spawnItems10 = new List<int>();
      CollectionsMarshal.SetCount<int>(spawnItems10, 1);
      span = CollectionsMarshal.AsSpan<int>(spawnItems10);
      int num41 = 0;
      span[num41] = ModContent.ItemType<SigilOfChampions>();
      num2 = num41 + 1;
      Add("MiniBoss", "SpiritChampion", npcIDs10, (Func<bool>) (() => WorldSavingSystem.DownedBoss[6]), (Func<bool>) (() => true), list7, spawnItems10, false, "Content/Bosses/Champions/Spirit/SpiritChampion_Still");
      List<int> npcIDs11 = new List<int>();
      CollectionsMarshal.SetCount<int>(npcIDs11, 1);
      span = CollectionsMarshal.AsSpan<int>(npcIDs11);
      int num42 = 0;
      span[num42] = ModContent.NPCType<WillChampion>();
      num2 = num42 + 1;
      List<int> list8 = new List<int>((IEnumerable<int>) BaseForce.EnchantsIn<WillForce>()).Append<int>(ModContent.ItemType<ChampionMusicBox>()).ToList<int>();
      List<int> spawnItems11 = new List<int>();
      CollectionsMarshal.SetCount<int>(spawnItems11, 1);
      span = CollectionsMarshal.AsSpan<int>(spawnItems11);
      int num43 = 0;
      span[num43] = ModContent.ItemType<SigilOfChampions>();
      num2 = num43 + 1;
      Add("MiniBoss", "WillChampion", npcIDs11, (Func<bool>) (() => WorldSavingSystem.DownedBoss[7]), (Func<bool>) (() => true), list8, spawnItems11, false);
      List<int> npcIDs12 = new List<int>();
      CollectionsMarshal.SetCount<int>(npcIDs12, 1);
      span = CollectionsMarshal.AsSpan<int>(npcIDs12);
      int num44 = 0;
      span[num44] = ModContent.NPCType<CosmosChampion>();
      num2 = num44 + 1;
      List<int> list9 = new List<int>((IEnumerable<int>) BaseForce.EnchantsIn<CosmoForce>()).Append<int>(ModContent.ItemType<ChampionMusicBox>()).ToList<int>();
      List<int> spawnItems12 = new List<int>();
      CollectionsMarshal.SetCount<int>(spawnItems12, 1);
      span = CollectionsMarshal.AsSpan<int>(spawnItems12);
      int num45 = 0;
      span[num45] = ModContent.ItemType<SigilOfChampions>();
      num2 = num45 + 1;
      Add("Boss", "CosmosChampion", npcIDs12, (Func<bool>) (() => WorldSavingSystem.DownedBoss[8]), (Func<bool>) (() => true), list9, spawnItems12, true);
      List<int> npcIDs13 = new List<int>();
      CollectionsMarshal.SetCount<int>(npcIDs13, 1);
      span = CollectionsMarshal.AsSpan<int>(npcIDs13);
      int num46 = 0;
      span[num46] = ModContent.NPCType<FargowiltasSouls.Content.Bosses.TrojanSquirrel.TrojanSquirrel>();
      num2 = num46 + 1;
      List<int> collectibles4 = new List<int>();
      CollectionsMarshal.SetCount<int>(collectibles4, 5);
      span = CollectionsMarshal.AsSpan<int>(collectibles4);
      int num47 = 0;
      span[num47] = ModContent.ItemType<TrojanSquirrelTrophy>();
      int num48 = num47 + 1;
      span[num48] = ModContent.ItemType<TreeSword>();
      int num49 = num48 + 1;
      span[num49] = ModContent.ItemType<MountedAcornGun>();
      int num50 = num49 + 1;
      span[num50] = ModContent.ItemType<SnowballStaff>();
      int num51 = num50 + 1;
      span[num51] = ModContent.ItemType<KamikazeSquirrelStaff>();
      num2 = num51 + 1;
      List<int> spawnItems13 = new List<int>();
      CollectionsMarshal.SetCount<int>(spawnItems13, 1);
      span = CollectionsMarshal.AsSpan<int>(spawnItems13);
      int num52 = 0;
      span[num52] = ModContent.ItemType<SquirrelCoatofArms>();
      num2 = num52 + 1;
      Add("Boss", "TrojanSquirrel", npcIDs13, (Func<bool>) (() => WorldSavingSystem.DownedBoss[9]), (Func<bool>) (() => true), collectibles4, spawnItems13, false, "Content/Bosses/TrojanSquirrel/TrojanSquirrel_Still");
      List<int> npcIDs14 = new List<int>();
      CollectionsMarshal.SetCount<int>(npcIDs14, 1);
      span = CollectionsMarshal.AsSpan<int>(npcIDs14);
      int num53 = 0;
      span[num53] = ModContent.NPCType<LifeChallenger>();
      num2 = num53 + 1;
      List<int> collectibles5 = new List<int>();
      CollectionsMarshal.SetCount<int>(collectibles5, 6);
      span = CollectionsMarshal.AsSpan<int>(collectibles5);
      int num54 = 0;
      span[num54] = ModContent.ItemType<LifelightTrophy>();
      int num55 = num54 + 1;
      span[num55] = ModContent.ItemType<EnchantedLifeblade>();
      int num56 = num55 + 1;
      span[num56] = ModContent.ItemType<Lightslinger>();
      int num57 = num56 + 1;
      span[num57] = ModContent.ItemType<CrystallineCongregation>();
      int num58 = num57 + 1;
      span[num58] = ModContent.ItemType<KamikazePixieStaff>();
      int num59 = num58 + 1;
      span[num59] = ModContent.ItemType<LifelightMasterPet>();
      num2 = num59 + 1;
      List<int> spawnItems14 = new List<int>();
      CollectionsMarshal.SetCount<int>(spawnItems14, 1);
      span = CollectionsMarshal.AsSpan<int>(spawnItems14);
      int num60 = 0;
      span[num60] = ModContent.ItemType<FragilePixieLamp>();
      num2 = num60 + 1;
      Add("Boss", "LifeChallenger", npcIDs14, (Func<bool>) (() => WorldSavingSystem.DownedBoss[10]), (Func<bool>) (() => true), collectibles5, spawnItems14, false, "Content/Bosses/Lifelight/LifeChallenger");
      List<int> npcIDs15 = new List<int>();
      CollectionsMarshal.SetCount<int>(npcIDs15, 1);
      span = CollectionsMarshal.AsSpan<int>(npcIDs15);
      int num61 = 0;
      span[num61] = ModContent.NPCType<FargowiltasSouls.Content.Bosses.BanishedBaron.BanishedBaron>();
      num2 = num61 + 1;
      List<int> collectibles6 = new List<int>();
      CollectionsMarshal.SetCount<int>(collectibles6, 5);
      span = CollectionsMarshal.AsSpan<int>(collectibles6);
      int num62 = 0;
      span[num62] = ModContent.ItemType<BaronTrophy>();
      int num63 = num62 + 1;
      span[num63] = ModContent.ItemType<TheBaronsTusk>();
      int num64 = num63 + 1;
      span[num64] = ModContent.ItemType<RoseTintedVisor>();
      int num65 = num64 + 1;
      span[num65] = ModContent.ItemType<NavalRustrifle>();
      int num66 = num65 + 1;
      span[num66] = ModContent.ItemType<DecrepitAirstrikeRemote>();
      num2 = num66 + 1;
      List<int> spawnItems15 = new List<int>();
      CollectionsMarshal.SetCount<int>(spawnItems15, 1);
      span = CollectionsMarshal.AsSpan<int>(spawnItems15);
      int num67 = 0;
      span[num67] = ModContent.ItemType<MechLure>();
      num2 = num67 + 1;
      Add("Boss", "BanishedBaron", npcIDs15, (Func<bool>) (() => WorldSavingSystem.downedBoss[12]), (Func<bool>) (() => true), collectibles6, spawnItems15, false);

      void Add(
        string type,
        string bossName,
        List<int> npcIDs,
        Func<bool> downed,
        Func<bool> available,
        List<int> collectibles,
        List<int> spawnItems,
        bool hasKilledAllMessage,
        string portrait = null)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        FargowiltasSouls.FargowiltasSouls.\u003C\u003Ec__DisplayClass39_1 cDisplayClass391 = new FargowiltasSouls.FargowiltasSouls.\u003C\u003Ec__DisplayClass39_1();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass391.CS\u0024\u003C\u003E8__locals1 = this;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass391.bossName = bossName;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass391.portrait = portrait;
        Mod mod = bossChecklist;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object[] objArray = new object[7]
        {
          (object) ("Log" + type),
          (object) this,
          (object) cDisplayClass391.bossName,
          (object) this.BossChecklistValues[cDisplayClass391.bossName],
          (object) downed,
          (object) npcIDs,
          null
        };
        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        dictionary.Add(nameof (spawnItems), (object) spawnItems);
        dictionary.Add("availability", (object) available);
        object obj;
        if (!hasKilledAllMessage)
        {
          DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(51, 2);
          interpolatedStringHandler.AppendLiteral("Mods.");
          interpolatedStringHandler.AppendFormatted(this.Name);
          interpolatedStringHandler.AppendLiteral(".NPCs.");
          // ISSUE: reference to a compiler-generated field
          interpolatedStringHandler.AppendFormatted(cDisplayClass391.bossName);
          interpolatedStringHandler.AppendLiteral(".BossChecklistIntegration.DespawnMessage");
          obj = (object) Language.GetText(interpolatedStringHandler.ToStringAndClear());
        }
        else
        {
          // ISSUE: method pointer
          obj = (object) new Func<NPC, LocalizedText>((object) cDisplayClass391, __methodptr(\u003CBossChecklistCompatibility\u003Eb__35));
        }
        dictionary.Add("despawnMessage", obj);
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated method
        dictionary.Add("customPortrait", cDisplayClass391.portrait == null ? (object) (Action<SpriteBatch, Rectangle, Color>) null : (object) new Action<SpriteBatch, Rectangle, Color>(cDisplayClass391.\u003CBossChecklistCompatibility\u003Eb__36));
        objArray[6] = (object) dictionary;
        mod.Call(objArray);
      }
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct TextureBuffer
    {
      public static readonly Dictionary<int, Asset<Texture2D>> NPC = new Dictionary<int, Asset<Texture2D>>();
      public static readonly Dictionary<int, Asset<Texture2D>> NPCHeadBoss = new Dictionary<int, Asset<Texture2D>>();
      public static readonly Dictionary<int, Asset<Texture2D>> Gore = new Dictionary<int, Asset<Texture2D>>();
      public static readonly Dictionary<int, Asset<Texture2D>> Golem = new Dictionary<int, Asset<Texture2D>>();
      public static readonly Dictionary<int, Asset<Texture2D>> Dest = new Dictionary<int, Asset<Texture2D>>();
      public static readonly Dictionary<int, Asset<Texture2D>> GlowMask = new Dictionary<int, Asset<Texture2D>>();
      public static readonly Dictionary<int, Asset<Texture2D>> Extra = new Dictionary<int, Asset<Texture2D>>();
      public static readonly Dictionary<int, Asset<Texture2D>> Projectile = new Dictionary<int, Asset<Texture2D>>();
      public static Asset<Texture2D> Ninja = (Asset<Texture2D>) null;
      public static Asset<Texture2D> Probe = (Asset<Texture2D>) null;
      public static Asset<Texture2D> BoneArm = (Asset<Texture2D>) null;
      public static Asset<Texture2D> BoneArm2 = (Asset<Texture2D>) null;
      public static Asset<Texture2D> BoneLaser = (Asset<Texture2D>) null;
      public static Asset<Texture2D> BoneEyes = (Asset<Texture2D>) null;
      public static Asset<Texture2D> Chain12 = (Asset<Texture2D>) null;
      public static Asset<Texture2D> Chain26 = (Asset<Texture2D>) null;
      public static Asset<Texture2D> Chain27 = (Asset<Texture2D>) null;
      public static Asset<Texture2D> Wof = (Asset<Texture2D>) null;
    }

    internal enum PacketID : byte
    {
      RequestGuttedCreeper,
      RequestPerfumeHeart,
      SyncCultistDamageCounterToServer,
      RequestCreeperHeal,
      RequestDeviGift,
      SpawnFishronEX,
      SyncFishronEXLife,
      SyncTogglesOnJoin,
      SyncOneToggle,
      SyncDefaultToggles,
      SyncCanPlayMaso,
      SyncNanoCoreMode,
      HealNPC,
    }
  }
}
