// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.EatersAndCrimeras
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies
{
  public class EatersAndCrimeras : Shooters
  {
    public EatersAndCrimeras()
      : base(420, ModContent.ProjectileType<CursedFlameHostile2>(), 8f, 0.8f, 75, 600f, 45)
    {
    }

    public override NPCMatcher CreateMatcher()
    {
      return new NPCMatcher().MatchTypeRange(6, -12, -11, 173, -23, -22);
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      if (!NPC.downedBoss2 || !Utils.NextBool(Main.rand, 5) || !npc.FargoSouls().CanHordeSplit)
        return;
      EModeGlobalNPC.Horde(npc, 5);
    }

    public override void AI(NPC npc)
    {
      base.AI(npc);
      if (npc.noTileCollide && Collision.SolidCollision(((Entity) npc).Center, 0, 0))
      {
        int index = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, npc.type == 6 ? 27 : 6, ((Entity) npc).velocity.X, ((Entity) npc).velocity.Y, 0, new Color(), 1f);
        Main.dust[index].noGravity = true;
        NPC npc1 = npc;
        ((Entity) npc1).position = Vector2.op_Subtraction(((Entity) npc1).position, Vector2.op_Division(((Entity) npc).velocity, 2f));
        if (this.AttackTimer > this.AttackThreshold - 120)
          this.AttackTimer = this.AttackThreshold - 120;
      }
      if (npc.type == 6)
        return;
      this.AttackTimer = 0;
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(33, 300, true, false);
    }
  }
}
