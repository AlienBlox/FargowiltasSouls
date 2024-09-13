// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.GuttedHeartMinions
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.NPCs.EternityModeNPCs;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using System.IO;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  public class GuttedHeartMinions : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<PureHeartHeader>();

    public override int ToggleItemType => ModContent.ItemType<GuttedHeart>();

    public override bool MinionEffect => true;

    public static void NurseHeal(Player player)
    {
      if (((Entity) player).whoAmI != Main.myPlayer)
        return;
      for (int index = 0; index < Main.maxNPCs; ++index)
      {
        NPC npc = Main.npc[index];
        if (npc.type == ModContent.NPCType<CreeperGutted>() && (double) npc.ai[0] == (double) ((Entity) player).whoAmI)
        {
          int num = npc.lifeMax - npc.life;
          switch (Main.netMode)
          {
            case 0:
              if (num > 0)
              {
                npc.HealEffect(num, true);
                npc.life = npc.lifeMax;
                continue;
              }
              continue;
            case 1:
              ModPacket packet = FargowiltasSouls.FargowiltasSouls.Instance.GetPacket(256);
              ((BinaryWriter) packet).Write((byte) 3);
              ((BinaryWriter) packet).Write((byte) ((Entity) player).whoAmI);
              ((BinaryWriter) packet).Write((byte) index);
              packet.Send(-1, -1);
              continue;
            default:
              continue;
          }
        }
      }
    }
  }
}
