// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Teleporters
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using System;
using System.IO;
using Terraria;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies
{
  public abstract class Teleporters : EModeNPCBehaviour
  {
    public int TeleportThreshold = 180;
    public int TeleportTimer;
    public bool DoTeleport;

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.TeleportTimer);
      bitWriter.WriteBit(this.DoTeleport);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.TeleportTimer = binaryReader.Read7BitEncodedInt();
      this.DoTeleport = bitReader.ReadBit();
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      int num1 = npc.type == 45 || npc.type == 172 ? 90 : 180;
      if (this.DoTeleport && ++this.TeleportTimer > num1)
      {
        this.TeleportTimer -= 20;
        if (FargoSoulsUtil.HostCheck && npc.HasPlayerTarget)
        {
          npc.ai[0] = 1f;
          int num2 = (int) ((Entity) Main.player[npc.target]).position.X / 16;
          int num3 = (int) ((Entity) Main.player[npc.target]).position.Y / 16;
          int num4 = (int) ((Entity) npc).position.X / 16;
          int num5 = (int) ((Entity) npc).position.Y / 16;
          int num6 = 20;
          int num7 = 0;
          bool flag1 = false;
          if ((double) Math.Abs(((Entity) npc).position.X - ((Entity) Main.player[npc.target]).position.X) + (double) Math.Abs(((Entity) npc).position.Y - ((Entity) Main.player[npc.target]).position.Y) > 2000.0)
          {
            num7 = 100;
            flag1 = true;
          }
          while (!flag1 && num7 < 100)
          {
            ++num7;
            int num8 = Main.rand.Next(num2 - num6, num2 + num6);
            for (int index1 = Main.rand.Next(num3 - num6, num3 + num6); index1 < num3 + num6; ++index1)
            {
              if ((index1 < num3 - 4 || index1 > num3 + 4 || num8 < num2 - 4 || num8 > num2 + 4) && (index1 < num5 - 1 || index1 > num5 + 1 || num8 < num4 - 1 || num8 > num4 + 1))
              {
                Tile tile = ((Tilemap) ref Main.tile)[num8, index1];
                if (((Tile) ref tile).HasUnactuatedTile)
                {
                  bool flag2 = true;
                  if (npc.HasValidTarget && Main.player[npc.target].ZoneDungeon && (npc.type == 32 || npc.type >= 281 && npc.type <= 286))
                  {
                    bool[] wallDungeon = Main.wallDungeon;
                    tile = ((Tilemap) ref Main.tile)[num8, index1 - 1];
                    int index2 = (int) ((Tile) ref tile).WallType;
                    if (!wallDungeon[index2])
                      flag2 = false;
                  }
                  tile = ((Tilemap) ref Main.tile)[num8, index1 - 1];
                  if (((Tile) ref tile).LiquidType == 1)
                  {
                    tile = ((Tilemap) ref Main.tile)[num8, index1 - 1];
                    if (((Tile) ref tile).LiquidAmount > (byte) 0)
                      flag2 = false;
                  }
                  if (flag2)
                  {
                    bool[] tileSolid = Main.tileSolid;
                    tile = ((Tilemap) ref Main.tile)[num8, index1];
                    int index3 = (int) ((Tile) ref tile).TileType;
                    if (tileSolid[index3] && !Collision.SolidTiles(num8 - 1, num8 + 1, index1 - 4, index1 - 1))
                    {
                      npc.ai[1] = 20f;
                      npc.ai[2] = (float) num8;
                      npc.ai[3] = (float) index1;
                      flag1 = true;
                      break;
                    }
                  }
                }
              }
            }
          }
          if (flag1)
          {
            this.DoTeleport = false;
            this.TeleportTimer = 0;
          }
          npc.netUpdate = true;
          EModeNPCBehaviour.NetSync(npc);
        }
      }
      if ((double) npc.ai[0] == 0.0 && (double) npc.ai[1] == 20.0 && (double) npc.ai[2] > 0.0 && (double) npc.ai[3] > 0.0)
      {
        this.TeleportTimer = 0;
        this.DoTeleport = false;
      }
      else
      {
        if (!npc.justHit || this.DoTeleport)
          return;
        this.DoTeleport = true;
        npc.netUpdate = true;
        EModeNPCBehaviour.NetSync(npc);
      }
    }
  }
}
