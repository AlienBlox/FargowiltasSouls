// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Desert.Antlion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.Desert
{
  public class Antlion : EModeNPCBehaviour
  {
    public int AttackTimer;
    public int VacuumTimer;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(69);

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      ++this.VacuumTimer;
      if (this.VacuumTimer >= 30)
      {
        foreach (Player player1 in ((IEnumerable<Player>) Main.player).Where<Player>((Func<Player, bool>) (x => ((Entity) x).active && !x.dead)))
        {
          if (player1.HasBuff(ModContent.BuffType<StunnedBuff>()) && (double) ((Entity) npc).Distance(((Entity) player1).Center) < 250.0)
          {
            Vector2 vector2 = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(((Entity) npc).Center, ((Entity) player1).Center)), 5f);
            Player player2 = player1;
            ((Entity) player2).velocity = Vector2.op_Addition(((Entity) player2).velocity, vector2);
          }
        }
        this.VacuumTimer = 0;
      }
      if (this.AttackTimer > 0)
      {
        if (this.AttackTimer == 75)
          SoundEngine.PlaySound(ref SoundID.Item5, new Vector2?(((Entity) npc).position), (SoundUpdateCallback) null);
        --this.AttackTimer;
      }
      if (this.AttackTimer <= 0)
      {
        Vector2 vector2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2).\u002Ector(((Entity) npc).position.X + (float) ((Entity) npc).width * 0.5f, ((Entity) npc).position.Y + (float) ((Entity) npc).height * 0.5f);
        float num1 = ((Entity) Main.player[npc.target]).position.X + (float) (((Entity) Main.player[npc.target]).width / 2) - vector2.X;
        float num2 = ((Entity) Main.player[npc.target]).position.Y - vector2.Y;
        float num3 = (float) (12.0 / Math.Sqrt((double) num1 * (double) num1 + (double) num2 * (double) num2));
        float num4 = num1 * (num3 * 1.5f);
        float num5 = num2 * (num3 * 1.5f);
        if (FargoSoulsUtil.HostCheck && (double) ((Entity) Main.player[npc.target]).Center.Y <= (double) ((Entity) npc).Center.Y && Collision.CanHit(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, ((Entity) Main.player[npc.target]).position, ((Entity) Main.player[npc.target]).width, ((Entity) Main.player[npc.target]).height))
        {
          int num6 = 10;
          int num7 = 31;
          int index = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2.X, vector2.Y, num4, num5, num7, num6, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          if (index != Main.maxProjectiles)
          {
            Main.projectile[index].ai[0] = 2f;
            Main.projectile[index].timeLeft = 300;
            Main.projectile[index].friendly = false;
            NetMessage.SendData(27, -1, -1, (NetworkText) null, index, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          }
          npc.netUpdate = true;
          this.AttackTimer = 75;
        }
      }
      npc.ai[0] = 10f;
    }
  }
}
