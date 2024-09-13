// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Hell.LavaSlime
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Hell
{
  public class LavaSlime : EModeNPCBehaviour
  {
    public bool CanDoLavaJump;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(59);

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if ((double) ((Entity) npc).velocity.Y < 0.0)
        this.CanDoLavaJump = true;
      else if ((double) ((Entity) npc).velocity.Y > 0.0)
      {
        if (!this.CanDoLavaJump || !FargoSoulsUtil.HostCheck || !npc.HasValidTarget || (double) ((Entity) npc).Bottom.Y <= (double) ((Entity) Main.player[npc.target]).Bottom.Y || Utils.ToTileCoordinates(((Entity) npc).Center).Y <= Main.maxTilesY - 200 || !Collision.CanHitLine(((Entity) npc).Center, 0, 0, ((Entity) Main.player[npc.target]).Center, 0, 0))
          return;
        this.CanDoLavaJump = false;
        int num1 = (int) ((double) ((Entity) npc).Center.X + (double) ((Entity) npc).velocity.X) / 16;
        int num2 = (int) ((double) ((Entity) npc).Center.Y + (double) ((Entity) npc).velocity.Y) / 16;
        Tile tileSafely = Framing.GetTileSafely(num1, num2);
        if (!Tile.op_Inequality(tileSafely, (ArgumentException) null) || ((Tile) ref tileSafely).HasTile || ((Tile) ref tileSafely).LiquidAmount != (byte) 0)
          return;
        ((Tile) ref tileSafely).LiquidType = 1;
        if (Main.netMode == 2)
          NetMessage.SendTileSquare(-1, num1, num2, 1, (TileChangeType) 0);
        WorldGen.SquareTileFrame(num1, num2, true);
        ((Entity) npc).velocity.Y = 0.0f;
        npc.netUpdate = true;
      }
      else
        this.CanDoLavaJump = false;
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<OiledBuff>(), 900, true, false);
      target.AddBuff(24, 300, true, false);
    }
  }
}
