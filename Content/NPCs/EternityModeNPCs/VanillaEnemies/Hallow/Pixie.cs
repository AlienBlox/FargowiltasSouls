// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Hallow.Pixie
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Hallow
{
  public class Pixie : EModeNPCBehaviour
  {
    public int Counter;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(75);

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.noTileCollide = true;
      npc.lifeMax = (int) ((double) npc.lifeMax * 1.5);
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (npc.HasPlayerTarget)
      {
        float num = ((Entity) Main.player[npc.target]).Center.Y - ((Entity) npc).Center.Y;
        if ((double) num < 0.0)
        {
          if ((double) num < -300.0)
            num = -300f;
          NPC npc1 = npc;
          ((Entity) npc1).velocity = Vector2.op_Addition(((Entity) npc1).velocity, Vector2.op_Division(Vector2.op_Multiply(Vector2.UnitY, num), 1250f));
        }
        if ((double) Vector2.Distance(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center) < 200.0)
          ++this.Counter;
      }
      if (this.Counter >= 60)
      {
        if (!Main.dedServ)
        {
          SoundStyle soundStyle;
          // ISSUE: explicit constructor call
          ((SoundStyle) ref soundStyle).\u002Ector("FargowiltasSouls/Assets/Sounds/Navi", (SoundType) 0);
          ((SoundStyle) ref soundStyle).Pitch = 0.5f;
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
        }
        this.Counter = 0;
      }
      EModeGlobalNPC.Aura(npc, 100f, ModContent.BuffType<SqueakyToyBuff>(), color: new Color());
    }

    public virtual void OnHitNPC(NPC npc, NPC target, NPC.HitInfo hit)
    {
      base.OnHitNPC(npc, target, hit);
      target.AddBuff(ModContent.BuffType<UnluckyBuff>(), 1800, false);
    }
  }
}
