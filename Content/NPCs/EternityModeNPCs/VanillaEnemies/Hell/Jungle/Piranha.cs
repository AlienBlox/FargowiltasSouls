// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Jungle.Piranha
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Jungle
{
  public class Piranha : EModeNPCBehaviour
  {
    public int JumpTimer;
    public int SwarmTimer;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(58);

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      this.JumpTimer = Main.rand.Next(120);
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if ((!npc.HasValidTarget || !Main.player[npc.target].bleed ? 0 : (Main.player[npc.target].ZoneJungle ? 1 : 0)) != 0 && ((Entity) npc).wet)
      {
        if (++this.SwarmTimer >= 90)
        {
          this.SwarmTimer = 0;
          if (Utils.NextBool(Main.rand) && NPC.CountNPCS(58) <= 6)
            EModeGlobalNPC.Horde(npc, 1);
        }
        if (++this.JumpTimer > 240)
        {
          this.JumpTimer = 0;
          int index = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
          if (Utils.NextBool(Main.rand) && index != -1 && FargoSoulsUtil.HostCheck)
          {
            Vector2 vector2;
            if (((Entity) Main.player[index]).active && !Main.player[index].dead && !Main.player[index].ghost)
            {
              vector2 = Vector2.op_Subtraction(((Entity) Main.player[index]).Center, ((Entity) npc).Center);
            }
            else
            {
              // ISSUE: explicit constructor call
              ((Vector2) ref vector2).\u002Ector((double) ((Entity) npc).Center.X < (double) ((Entity) Main.player[index]).Center.X ? -300f : 300f, -100f);
            }
            vector2.X /= 120f;
            vector2.Y = (float) ((double) vector2.Y / 120.0 - 18.0);
            npc.ai[1] = 120f;
            npc.ai[2] = vector2.X;
            npc.ai[3] = vector2.Y;
            npc.netUpdate = true;
          }
        }
      }
      if ((double) npc.ai[1] > 0.0)
      {
        --npc.ai[1];
        npc.noTileCollide = true;
        ((Entity) npc).velocity.X = npc.ai[2];
        ((Entity) npc).velocity.Y = npc.ai[3];
        npc.ai[3] += 0.3f;
        int num = 5;
        for (int index1 = 0; index1 < num; ++index1)
        {
          Vector2 vector2 = Vector2.op_Multiply(Utils.ToRotationVector2((float) (Main.rand.NextDouble() * 3.14159274101257) - 1.57079637f), (float) Main.rand.Next(3, 8));
          int index2 = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 172, vector2.X * 2f, vector2.Y * 2f, 100, new Color(), 1.4f);
          Main.dust[index2].noGravity = true;
          Main.dust[index2].noLight = true;
          Dust dust1 = Main.dust[index2];
          dust1.velocity = Vector2.op_Division(dust1.velocity, 4f);
          Dust dust2 = Main.dust[index2];
          dust2.velocity = Vector2.op_Subtraction(dust2.velocity, ((Entity) npc).velocity);
        }
      }
      else
      {
        if (!npc.noTileCollide)
          return;
        npc.noTileCollide = Collision.SolidCollision(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height);
      }
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(30, 240, true, false);
    }

    public virtual void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) => base.ModifyNPCLoot(npc, npcLoot);
  }
}
