// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Pets.DeviTalkGlobalNPC
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Pets
{
  public class DeviTalkGlobalNPC : GlobalNPC
  {
    public virtual void OnKill(NPC npc)
    {
      if (!npc.boss || !Main.LocalPlayer.FargoSouls().ChibiDevi)
        return;
      Projectile projectile = ((IEnumerable<Projectile>) Main.projectile).FirstOrDefault<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && p.owner == Main.myPlayer && p.type == ModContent.ProjectileType<ChibiDevi>()));
      if (projectile == null || !(projectile.ModProjectile is ChibiDevi modProjectile))
        return;
      modProjectile.TryTalkWithCD(ChibiDevi.TalkType.KillBoss, ChibiDevi.MediumCD);
    }
  }
}
