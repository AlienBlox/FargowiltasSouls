// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Desert.SandSharks
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Desert
{
  public class SandSharks : EModeNPCBehaviour
  {
    public bool selfdestruct;
    public int deathTimer;

    public override NPCMatcher CreateMatcher()
    {
      return new NPCMatcher().MatchTypeRange(542, 543, 544, 545);
    }

    public virtual void OnSpawn(NPC npc, IEntitySource source)
    {
      base.OnSpawn(npc, source);
      if (!(source is EntitySource_Parent entitySourceParent) || !(entitySourceParent.Entity is Projectile entity) || entity.type != 657)
        return;
      this.selfdestruct = true;
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      if (!Utils.NextBool(Main.rand, 4))
        return;
      int num = Main.rand.Next(542, 546);
      if (num == npc.type)
        return;
      npc.Transform(num);
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (!this.selfdestruct || ++this.deathTimer <= 240)
        return;
      npc.life = 0;
      npc.HitEffect(0, 10.0, new bool?());
      ((Entity) npc).active = false;
      if (Main.netMode != 2)
        return;
      NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) npc).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
    }
  }
}
