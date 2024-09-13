// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.CultistRitual
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class CultistRitual : BaseArena
  {
    public virtual string Texture => "Terraria/Images/Projectile_454";

    public CultistRitual()
      : base(-1f * (float) Math.PI / 140f, 1600f, 439)
    {
    }

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      Main.projFrames[this.Projectile.type] = 2;
    }

    protected override void Movement(NPC npc)
    {
      if ((double) npc.ai[0] != 5.0)
        return;
      int index = (int) npc.ai[2];
      if (index <= -1 || index >= Main.maxProjectiles || !((Entity) Main.projectile[index]).active || Main.projectile[index].type != 490)
        return;
      ((Entity) this.Projectile).Center = ((Entity) Main.projectile[index]).Center;
    }

    public override void AI()
    {
      base.AI();
      ++this.Projectile.frameCounter;
      if (this.Projectile.frameCounter < 6)
        return;
      this.Projectile.frameCounter = 0;
      ++this.Projectile.frame;
      if (this.Projectile.frame <= 1)
        return;
      this.Projectile.frame = 0;
    }

    public override void PostAI()
    {
      base.PostAI();
      this.Projectile.hide = true;
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      base.OnHitPlayer(target, info);
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 300, true, false);
    }
  }
}
