// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.GuttedHeartEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.NPCs.EternityModeNPCs;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  public class GuttedHeartEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) null;

    public override int ToggleItemType => ModContent.ItemType<GuttedHeart>();

    public override void PostUpdateEquips(Player player)
    {
      Player player1 = player;
      if (((Entity) player1).whoAmI != Main.myPlayer)
        return;
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      --fargoSoulsPlayer.GuttedHeartCD;
      if (Vector2.op_Equality(((Entity) player1).velocity, Vector2.Zero) && player1.itemAnimation == 0)
        --fargoSoulsPlayer.GuttedHeartCD;
      if (fargoSoulsPlayer.GuttedHeartCD > 0)
        return;
      int num1 = (int) Math.Round(Utils.Lerp(600.0, 900.0, (double) player1.statLife / (double) player1.statLifeMax2));
      fargoSoulsPlayer.GuttedHeartCD = num1;
      if (!player1.HasEffect<GuttedHeartMinions>())
        return;
      int num2 = 0;
      for (int index = 0; index < Main.maxNPCs; ++index)
      {
        if (((Entity) Main.npc[index]).active && Main.npc[index].type == ModContent.NPCType<CreeperGutted>() && (double) Main.npc[index].ai[0] == (double) ((Entity) player1).whoAmI)
          ++num2;
      }
      if (num2 < 5)
      {
        int num3 = 1;
        if (fargoSoulsPlayer.PureHeart)
          num3 = 2;
        if (fargoSoulsPlayer.MasochistSoul)
          num3 = 5;
        switch (Main.netMode)
        {
          case 0:
            int index = NPC.NewNPC(NPC.GetBossSpawnSource(((Entity) player1).whoAmI), (int) ((Entity) player1).Center.X, (int) ((Entity) player1).Center.Y, ModContent.NPCType<CreeperGutted>(), 0, (float) ((Entity) player1).whoAmI, 0.0f, (float) num3, 0.0f, (int) byte.MaxValue);
            if (index == Main.maxNPCs)
              break;
            ((Entity) Main.npc[index]).velocity = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitX, 2.0 * Math.PI), 8f);
            break;
          case 1:
            ModPacket packet = FargowiltasSouls.FargowiltasSouls.Instance.GetPacket(256);
            ((BinaryWriter) packet).Write((byte) 0);
            ((BinaryWriter) packet).Write((byte) ((Entity) player1).whoAmI);
            ((BinaryWriter) packet).Write((byte) num3);
            packet.Send(-1, -1);
            break;
        }
      }
      else
      {
        int index1 = -1;
        for (int index2 = 0; index2 < Main.maxNPCs; ++index2)
        {
          if (((Entity) Main.npc[index2]).active && Main.npc[index2].type == ModContent.NPCType<CreeperGutted>() && (double) Main.npc[index2].ai[0] == (double) ((Entity) player1).whoAmI)
          {
            if (index1 < 0)
              index1 = index2;
            else if (Main.npc[index2].life < Main.npc[index1].life)
              index1 = index2;
          }
        }
        if (Main.npc[index1].life >= Main.npc[index1].lifeMax)
          return;
        if (Main.netMode == 0)
        {
          int num4 = Main.npc[index1].lifeMax - Main.npc[index1].life;
          Main.npc[index1].life = Main.npc[index1].lifeMax;
          CombatText.NewText(((Entity) Main.npc[index1]).Hitbox, CombatText.HealLife, num4, false, false);
        }
        else
        {
          if (Main.netMode != 1)
            return;
          ModPacket packet = FargowiltasSouls.FargowiltasSouls.Instance.GetPacket(256);
          ((BinaryWriter) packet).Write((byte) 3);
          ((BinaryWriter) packet).Write((byte) ((Entity) player1).whoAmI);
          ((BinaryWriter) packet).Write((byte) index1);
          packet.Send(-1, -1);
        }
      }
    }
  }
}
