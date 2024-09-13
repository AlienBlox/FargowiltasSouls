// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Dungeon.SpikeBall
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Dungeon
{
  public class SpikeBall : EModeNPCBehaviour
  {
    public int Counter;
    public bool OutsideDungeon;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(70);

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      Tile tileSafely = Framing.GetTileSafely(((Entity) npc).Center);
      if (((Tile) ref tileSafely).WallType == (ushort) 87)
      {
        npc.damage *= 3;
        npc.defDamage *= 3;
      }
      int closestPlayer = npc.FindClosestPlayer();
      if (closestPlayer == -1 || Main.player[closestPlayer].ZoneDungeon)
        return;
      this.OutsideDungeon = true;
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (!this.OutsideDungeon)
        return;
      if (++this.Counter > 1800)
      {
        if (FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<FusedExplosion>(), npc.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        npc.life = 0;
        npc.HitEffect(0, 10.0, new bool?());
        npc.SimpleStrikeNPC(int.MaxValue, 0, false, 0.0f, (DamageClass) null, false, 0.0f, true);
      }
      else
      {
        if (this.Counter <= 1500)
          return;
        int index = Dust.NewDust(((Entity) npc).Center, 0, 0, 6, ((Entity) npc).velocity.X * 0.4f, ((Entity) npc).velocity.Y * 0.4f, 0, new Color(), 2f);
        Dust dust = Main.dust[index];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 2f);
        if (!Utils.NextBool(Main.rand, 4))
          return;
        Main.dust[index].scale += 0.5f;
        Main.dust[index].noGravity = true;
      }
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(36, 600, true, false);
      if (!this.OutsideDungeon)
        return;
      target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 600, true, false);
    }
  }
}
