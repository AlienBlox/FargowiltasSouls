// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.SlimeSwarm
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs
{
  public class SlimeSwarm : ModNPC
  {
    public virtual string Texture => "Terraria/Images/NPC_1";

    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = Main.npcFrameCount[1];
      NPCID.Sets.CantTakeLunchMoney[this.Type] = true;
      NPCID.Sets.SpecificDebuffImmunity[this.Type] = NPCID.Sets.SpecificDebuffImmunity[50];
      Luminance.Common.Utilities.Utilities.ExcludeFromBestiary((ModNPC) this);
    }

    public virtual void SetDefaults()
    {
      this.NPC.CloneDefaults(1);
      this.NPC.aiStyle = -1;
      this.NPC.knockBackResist = 0.0f;
      this.NPC.timeLeft = 600;
      this.NPC.noTileCollide = false;
      this.NPC.noGravity = false;
      this.NPC.lifeMax *= 3;
      this.NPC.damage = 32;
      NPC npc = this.NPC;
      npc.GravityMultiplier = MultipliableFloat.op_Multiply(npc.GravityMultiplier, 2f);
    }

    public virtual void AI()
    {
      ref float local1 = ref this.NPC.ai[0];
      ref float local2 = ref this.NPC.ai[1];
      float num1 = 0.1f;
      int num2 = 12;
      int num3 = 5;
      if ((double) Math.Abs(((Entity) this.NPC).velocity.X) < (double) num2)
        ((Entity) this.NPC).velocity.X += num1 * local1;
      if ((double) ((Entity) this.NPC).velocity.Y == 0.0)
        ((Entity) this.NPC).velocity.Y = (float) -num3;
      bool flag = true;
      int closest = (int) Player.FindClosest(((Entity) this.NPC).Center, 0, 0);
      if (closest.IsWithinBounds((int) byte.MaxValue) && (double) ((Entity) Main.player[closest]).Distance(((Entity) this.NPC).Center) < 1500.0)
        flag = false;
      this.NPC.damage = (double) local2 > 60.0 ? this.NPC.defDamage : 0;
      ref float local3 = ref local2;
      float num4 = local2 + 1f;
      double num5 = (double) num4;
      local3 = (float) num5;
      if (!((double) num4 > 900.0 | flag))
        return;
      this.NPC.alpha += 17;
      if (this.NPC.alpha < 250)
        return;
      ((Entity) this.NPC).active = false;
    }

    public virtual void FindFrame(int frameHeight)
    {
      ++this.NPC.frameCounter;
      if (this.NPC.frameCounter > 4.0)
      {
        this.NPC.frame.Y += frameHeight;
        this.NPC.frameCounter = 0.0;
      }
      if (this.NPC.frame.Y < Main.npcFrameCount[this.NPC.type] * frameHeight)
        return;
      this.NPC.frame.Y = 0;
    }
  }
}
