// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Cosmos.CosmosRitual
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.Systems;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Cosmos
{
  public class CosmosRitual : BaseArena
  {
    private const float maxSize = 1200f;
    private const float minSize = 600f;

    public virtual string Texture => "Terraria/Images/Projectile_454";

    public CosmosRitual()
      : base((float) Math.PI / 140f, 1000f, ModContent.NPCType<CosmosChampion>())
    {
    }

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      Main.projFrames[this.Projectile.type] = 2;
    }

    protected override void Movement(NPC npc)
    {
      ((Entity) this.Projectile).Center = ((Entity) npc).Center;
      float num1 = (float) npc.life / ((float) npc.lifeMax * 0.2f);
      if ((double) num1 > 1.0)
        num1 = 1f;
      if ((double) num1 < 0.0)
        num1 = 0.0f;
      float num2 = (float) (600.0 + 600.0 * (double) num1);
      if ((double) this.threshold > (double) num2)
      {
        this.threshold -= 4f;
        if ((double) this.threshold < (double) num2)
          this.threshold = num2;
      }
      if ((double) this.threshold >= (double) num2)
        return;
      this.threshold += 4f;
      if ((double) this.threshold <= (double) num2)
        return;
      this.threshold = num2;
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      base.OnHitPlayer(target, info);
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(24, 300, true, false);
      target.AddBuff(144, 300, true, false);
      target.AddBuff(ModContent.BuffType<HexedBuff>(), 300, true, false);
      target.AddBuff(44, 300, true, false);
    }
  }
}
