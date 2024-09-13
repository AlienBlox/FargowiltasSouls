// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.StupidSnowmanEvent.SnowmanGangsta
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.StupidSnowmanEvent
{
  public class SnowmanGangsta : EModeNPCBehaviour
  {
    public int Counter;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(143);

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (++this.Counter <= 300)
        return;
      this.Counter = 0;
      if (FargoSoulsUtil.HostCheck && npc.HasPlayerTarget)
      {
        for (int index = 0; index < 6; ++index)
        {
          Vector2 vector2 = Vector2.op_Multiply(Vector2.UnitX, ((Entity) Main.player[npc.target]).Center.X - ((Entity) npc).Center.X);
          vector2.X += (float) Main.rand.Next(-40, 41);
          vector2.Y += (float) Main.rand.Next(-40, 41);
          ((Vector2) ref vector2).Normalize();
          vector2 = Vector2.op_Multiply(vector2, 11f);
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center.X, ((Entity) npc).Center.Y, vector2.X, vector2.Y, 110, 20, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
      }
      SoundEngine.PlaySound(ref SoundID.Item38, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<HypothermiaBuff>(), 300, true, false);
      target.AddBuff(44, 300, true, false);
    }
  }
}
