// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Night.Zombies
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Night
{
  public class Zombies : EModeNPCBehaviour
  {
    public override NPCMatcher CreateMatcher()
    {
      return new NPCMatcher().MatchTypeRange(3, 430, 436, 432, 433, 434, 435, 132, 200, 186, 187, 189, 321, 223, 320, 332, 331, 188, -34, -35, 319, 161, 431, 254, (int) byte.MaxValue, 338, 339, 340, -32, -33, 586);
    }

    private static void transformZombie(NPC npc, int armedId = -1)
    {
      if (Main.LocalPlayer.ZoneSnow && Utils.NextBool(Main.rand))
        npc.Transform(161);
      if (Utils.NextBool(Main.rand, 8) && npc.FargoSouls().CanHordeSplit)
        EModeGlobalNPC.Horde(npc, 6);
      if (armedId == -1 || !Utils.NextBool(Main.rand, 5))
        return;
      npc.Transform(armedId);
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      switch (npc.type)
      {
        case 3:
          Zombies.transformZombie(npc, 430);
          break;
        case 161:
          Zombies.transformZombie(npc, 431);
          break;
        case 186:
          Zombies.transformZombie(npc, 432);
          break;
        case 187:
          Zombies.transformZombie(npc, 433);
          break;
        case 188:
          Zombies.transformZombie(npc, 434);
          break;
        case 189:
          Zombies.transformZombie(npc, 435);
          break;
        case 200:
          Zombies.transformZombie(npc, 436);
          break;
      }
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (npc.type == 223)
        npc.aiStyle = !((Entity) npc).wet ? 3 : 1;
      if ((double) npc.ai[2] < 45.0 || (double) npc.ai[3] != 0.0 || !FargoSoulsUtil.HostCheck)
        return;
      int num1 = (int) ((double) ((Entity) npc).position.X + (double) (((Entity) npc).width / 2) + (double) (15 * ((Entity) npc).direction)) / 16;
      int num2 = (int) ((double) ((Entity) npc).position.Y + (double) ((Entity) npc).height - 15.0) / 16 - 1;
      Tile tileSafely = Framing.GetTileSafely(num1, num2);
      if (((Tile) ref tileSafely).TileType != (ushort) 10 && ((Tile) ref tileSafely).TileType != (ushort) 388)
        return;
      WorldGen.OpenDoor(num1, num2, ((Entity) npc).direction);
      if (Main.netMode != 2)
        return;
      NetMessage.SendData(17, -1, -1, (NetworkText) null, 0, (float) num1, (float) num2, 0.0f, 0, 0, 0);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<RottingBuff>(), 300, true, false);
      switch (npc.type)
      {
        case 161:
        case 431:
          target.AddBuff(ModContent.BuffType<HypothermiaBuff>(), 300, true, false);
          break;
        case 254:
        case (int) byte.MaxValue:
          target.AddBuff(ModContent.BuffType<InfestedBuff>(), 300, true, false);
          break;
        case 338:
        case 339:
        case 340:
          target.AddBuff(ModContent.BuffType<RottingBuff>(), 900, true, false);
          break;
      }
    }

    public virtual void OnHitNPC(NPC npc, NPC target, NPC.HitInfo hit)
    {
      base.OnHitNPC(npc, target, hit);
      if (!target.townNPC || !hit.InstantKill && target.life >= ((NPC.HitInfo) ref hit).Damage)
        return;
      target.Transform(npc.type);
    }

    public virtual void OnKill(NPC npc)
    {
      base.OnKill(npc);
      switch (npc.type)
      {
        case -33:
        case -32:
        case 187:
        case 433:
          if (!Utils.NextBool(Main.rand) || !FargoSoulsUtil.HostCheck)
            break;
          FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, 1, velocity: new Vector2());
          break;
      }
    }
  }
}
