// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Hell.Demons
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Hell
{
  public class Demons : EModeNPCBehaviour
  {
    public int Counter;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchTypeRange(62, 66, 156);

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      if (!Main.hardMode || !Utils.NextBool(Main.rand, 4) || !npc.FargoSouls().CanHordeSplit)
        return;
      EModeGlobalNPC.Horde(npc, Main.rand.Next(5) + 1);
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (npc.HasValidTarget)
        npc.noTileCollide = !Collision.CanHitLine(((Entity) npc).Center, 0, 0, ((Entity) Main.player[npc.target]).Center, 0, 0);
      if (npc.type == 62 && (double) npc.ai[0] == 100.0 || npc.type == 156 && ++this.Counter > 300)
      {
        this.Counter = 0;
        int index = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
        if (index != -1 && (double) ((Entity) npc).Distance(((Entity) Main.player[index]).Center) < 800.0 && FargoSoulsUtil.HostCheck)
        {
          int num1 = npc.type == 156 ? 9 : 6;
          int num2 = FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, npc.type == 156 ? 1.33333337f : 1f);
          IEntitySource sourceFromThis = ((Entity) npc).GetSource_FromThis((string) null);
          Vector2 center = ((Entity) npc).Center;
          int damage = num2;
          FargoSoulsUtil.XWay(num1, sourceFromThis, center, 44, 1f, damage, 0.5f);
        }
      }
      if (npc.type != 66)
        return;
      if (((Entity) npc).lavaWet && npc.HasValidTarget && ((double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) < 450.0 || Collision.CanHitLine(((Entity) npc).Center, 0, 0, ((Entity) Main.player[npc.target]).Center, 0, 0)))
      {
        npc.buffImmune[24] = false;
        npc.buffImmune[323] = false;
        npc.AddBuff(24, 780, false);
      }
      if (!npc.onFire && !npc.onFire3)
        return;
      SoundStyle npcDeath10 = SoundID.NPCDeath10;
      ((SoundStyle) ref npcDeath10).Pitch = 0.5f;
      SoundEngine.PlaySound(ref npcDeath10, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 3; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 6, 0.0f, 0.0f, 0, new Color(), (float) ((double) this.Counter / 720.0 * 5.0));
        Main.dust[index2].noGravity = !Utils.NextBool(Main.rand, 5);
        Main.dust[index2].noLight = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, Utils.NextFloat(Main.rand, 12f));
      }
      if (++this.Counter <= 720)
        return;
      npc.Transform(62);
      int firstNpc = NPC.FindFirstNPC(22);
      if (firstNpc == -1 || !((Entity) Main.npc[firstNpc]).active || !FargoSoulsUtil.HostCheck)
        return;
      Main.npc[firstNpc].SimpleStrikeNPC(int.MaxValue, 0, false, 0.0f, (DamageClass) null, false, 0.0f, true);
      if ((npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer()) == -1 || FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.wallBoss, 113))
        return;
      NPC.SpawnWOF(((Entity) Main.player[npc.target]).Center);
    }

    public virtual void UpdateLifeRegen(NPC npc, ref int damage)
    {
      base.UpdateLifeRegen(npc, ref damage);
      if (npc.type != 66 || !npc.onFire)
        return;
      damage /= 2;
    }

    public virtual void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
      base.ModifyNPCLoot(npc, npcLoot);
      FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(888, 50, 1, 1));
    }
  }
}
