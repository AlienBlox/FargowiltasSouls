// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Souls.SolarFlareBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Souls
{
  public class SolarFlareBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.buffNoSave[this.Type] = true;
      BuffID.Sets.NurseCannotRemoveDebuff[this.Type] = true;
      Main.debuff[this.Type] = true;
    }

    public virtual string Texture => "FargowiltasSouls/Content/Buffs/PlaceholderDebuff";

    public virtual void Update(NPC npc, ref int buffIndex)
    {
      npc.FargoSouls().SolarFlare = true;
      if (npc.buffTime[buffIndex] >= 2)
        return;
      int index = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<Explosion>(), 1000, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      if (index == Main.maxProjectiles)
        return;
      Main.projectile[index].FargoSouls().CanSplit = false;
    }

    public virtual bool ReApply(NPC npc, int time, int buffIndex) => true;
  }
}
