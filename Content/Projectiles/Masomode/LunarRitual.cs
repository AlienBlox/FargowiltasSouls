// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.LunarRitual
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.VanillaEternity;
using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class LunarRitual : BaseArena
  {
    private const float maxSize = 1600f;

    public virtual string Texture => "Terraria/Images/Projectile_454";

    public LunarRitual()
      : base((float) Math.PI / 140f, 1600f, 398)
    {
    }

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      Main.projFrames[this.Projectile.type] = 2;
    }

    protected override void Movement(NPC npc)
    {
      Vector2 vector2 = ((Entity) npc).Center;
      if (npc.HasValidTarget)
        vector2 = Vector2.op_Addition(vector2, Vector2.op_Division(Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center), 2f));
      if ((double) ((Entity) this.Projectile).Distance(vector2) <= 1.0)
        ((Entity) this.Projectile).Center = vector2;
      else if ((double) ((Entity) this.Projectile).Distance(vector2) > (double) this.threshold)
        ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Subtraction(vector2, ((Entity) this.Projectile).Center), 30f);
      else if (npc.GetGlobalNPC<MoonLordCore>().VulnerabilityState == 4 && (double) npc.GetGlobalNPC<MoonLordCore>().VulnerabilityTimer < 60.0 && !npc.dontTakeDamage)
        ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) this.Projectile).Center), 0.05f);
      else
        ((Entity) this.Projectile).velocity = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, vector2);
      this.threshold += 6f;
      if ((double) this.threshold <= 1600.0)
        return;
      this.threshold = 1600f;
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

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      base.OnHitPlayer(target, info);
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 300, true, false);
    }
  }
}
