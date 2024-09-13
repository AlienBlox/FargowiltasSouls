// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Souls.CorruptingBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Souls
{
  public class CorruptingBuff : ModBuff
  {
    public virtual void SetStaticDefaults() => Main.buffNoSave[this.Type] = true;

    public virtual string Texture => "FargowiltasSouls/Content/Buffs/PlaceholderBuff";

    public virtual void Update(NPC npc, ref int buffIndex)
    {
      ++npc.FargoSouls().EbonCorruptionTimer;
    }
  }
}
