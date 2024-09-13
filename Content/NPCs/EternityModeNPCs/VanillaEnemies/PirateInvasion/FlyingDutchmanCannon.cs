// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.PirateInvasion.FlyingDutchmanCannon
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.NPCs.EternityModeNPCs.VanillaEnemies.PirateInvasion
{
  public class FlyingDutchmanCannon : EModeNPCBehaviour
  {
    public int PhaseTimer;
    public int Gun;
    public bool AttackFlag;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(492);

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      this.Gun = Main.rand.Next(10);
    }

    public virtual void AI(NPC npc)
    {
      base.AI(npc);
      if (!this.AttackFlag && this.PhaseTimer == 270 && NPC.FindFirstNPC(npc.type) == ((Entity) npc).whoAmI && FargoSoulsUtil.HostCheck)
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<TargetingReticle>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, 0.0f, 0.0f);
      if (++this.PhaseTimer > 360)
      {
        this.AttackFlag = !this.AttackFlag;
        this.PhaseTimer = this.AttackFlag ? 180 : 0;
        EModeNPCBehaviour.NetSync(npc);
      }
      if (!this.AttackFlag || ++this.Gun <= 10)
        return;
      this.Gun = -Main.rand.Next(5);
      if (!npc.HasPlayerTarget)
        return;
      Vector2 vector2 = Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center);
      vector2.X += (float) Main.rand.Next(-40, 41);
      vector2.Y += (float) Main.rand.Next(-40, 41);
      ((Vector2) ref vector2).Normalize();
      vector2 = Vector2.op_Multiply(vector2, 14f);
      if (FargoSoulsUtil.HostCheck)
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, 180, 15, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      SoundEngine.PlaySound(ref SoundID.Item11, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
    }

    public virtual void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
      base.ModifyNPCLoot(npc, npcLoot);
      FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(ModContent.ItemType<SecurityWallet>(), 5, 1, 1));
      FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(905, 50, 1, 1));
      FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(855, 50, 1, 1));
    }
  }
}
