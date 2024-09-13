// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Souls.OriPoisonBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Souls
{
  public class OriPoisonBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.buffNoSave[this.Type] = true;
      BuffID.Sets.NurseCannotRemoveDebuff[this.Type] = true;
      Main.debuff[this.Type] = true;
    }

    public virtual void Update(NPC npc, ref int buffIndex) => npc.FargoSouls().OriPoison = true;
  }
}
