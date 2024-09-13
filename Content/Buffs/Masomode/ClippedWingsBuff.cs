// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Masomode.ClippedWingsBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Masomode
{
  public class ClippedWingsBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.debuff[this.Type] = true;
      Main.pvpBuff[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      player.wingTime = 0.0f;
      player.wingTimeMax = 0;
      player.rocketTime = 0;
    }

    public virtual void Update(NPC npc, ref int buffIndex)
    {
      if (npc.boss)
      {
        if (npc.buffTime[buffIndex] <= 1)
          return;
        npc.buffTime[buffIndex] = 1;
      }
      else
      {
        NPC npc1 = npc;
        ((Entity) npc1).position = Vector2.op_Subtraction(((Entity) npc1).position, Vector2.op_Division(((Entity) npc).velocity, 2f));
        if ((double) ((Entity) npc).velocity.Y >= 0.0)
          return;
        ((Entity) npc).velocity.Y = 0.0f;
      }
    }
  }
}
