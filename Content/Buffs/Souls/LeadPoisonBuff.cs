// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Souls.LeadPoisonBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Souls
{
  public class LeadPoisonBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.buffNoSave[this.Type] = true;
      Main.debuff[this.Type] = true;
    }

    public virtual void Update(NPC npc, ref int buffIndex)
    {
      npc.FargoSouls().LeadPoison = true;
      if (npc.buffTime[buffIndex] != 2)
        return;
      for (int index = 0; index < Main.maxNPCs; ++index)
      {
        NPC npc1 = Main.npc[index];
        if (index != ((Entity) npc).whoAmI && npc1 != null && ((Entity) npc1).active && !npc1.townNPC && !npc1.friendly && npc1.lifeMax > 5 && (double) Vector2.Distance(((Entity) npc).Center, ((Entity) npc1).Center) < 50.0)
          npc1.AddBuff(ModContent.BuffType<LeadPoisonBuff>(), 30, false);
      }
    }
  }
}
