// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.GiveDevianttGiftsCall
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Luminance.Core.ModCalls;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable enable
namespace FargowiltasSouls
{
  internal sealed class GiveDevianttGiftsCall : ModCall
  {
    public virtual 
    #nullable disable
    IEnumerable<string> GetCallCommands()
    {
      yield return "GiveDevianttGifts";
    }

    public virtual IEnumerable<Type> GetInputTypes() => (IEnumerable<Type>) null;

    protected virtual object SafeProcess(params object[] argsWithoutCommand)
    {
      Main.LocalPlayer.FargoSouls().ReceivedMasoGift = true;
      switch (Main.netMode)
      {
        case 0:
          FargowiltasSouls.FargowiltasSouls.DropDevianttsGift(Main.LocalPlayer);
          break;
        case 1:
          ModPacket packet = FargowiltasSouls.FargowiltasSouls.Instance.GetPacket(256);
          ((BinaryWriter) packet).Write((byte) 4);
          ((BinaryWriter) packet).Write((byte) ((Entity) Main.LocalPlayer).whoAmI);
          packet.Send(-1, -1);
          break;
      }
      Main.npcChatText = Language.GetTextValue("Mods.Fargowiltas.NPCs.Deviantt.Chat.GiveGifts");
      return ModCallManager.DefaultObject;
    }
  }
}
