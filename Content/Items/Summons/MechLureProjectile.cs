// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Summons.MechLureProjectile
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Summons
{
  public class MechLureProjectile : ModProjectile
  {
    public virtual void SetStaticDefaults() => Main.projFrames[this.Type] = 6;

    public virtual void SetDefaults()
    {
      base.SetDefaults();
      ((Entity) this.Projectile).width = 33;
      ((Entity) this.Projectile).height = 42;
      this.Projectile.hostile = false;
      this.Projectile.friendly = false;
      this.Projectile.tileCollide = false;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 480;
      this.Projectile.light = 1f;
    }

    public virtual void AI()
    {
      int num = ModContent.NPCType<FargowiltasSouls.Content.Bosses.BanishedBaron.BanishedBaron>();
      if (++this.Projectile.frameCounter >= 5)
      {
        this.Projectile.frameCounter = 0;
        this.Projectile.frame = ++this.Projectile.frame % Main.projFrames[this.Projectile.type];
      }
      int firstNpc = NPC.FindFirstNPC(num);
      if ((double) this.Projectile.ai[0] == 120.0)
      {
        if (firstNpc >= 0 && firstNpc < Main.maxNPCs)
        {
          this.Projectile.Kill();
          return;
        }
        SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/BaronSummon", (SoundType) 0);
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        int closest = (int) Player.FindClosest(((Entity) this.Projectile).Center, 1, 1);
        if (FargoSoulsUtil.HostCheck && Main.player[closest] != null && ((Entity) Main.player[closest]).active)
          NPC.SpawnOnPlayer(closest, ModContent.NPCType<FargowiltasSouls.Content.Bosses.BanishedBaron.BanishedBaron>());
      }
      Projectile projectile = this.Projectile;
      ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 0.978f);
      if ((double) Utils.ToRotation(((Entity) this.Projectile).velocity) > 3.1415927410125732)
        this.Projectile.rotation = (float) (0.0 - 3.1415927410125732 * (double) ((Entity) this.Projectile).velocity.X / 25.0);
      else
        this.Projectile.rotation = (float) (0.0 + 3.1415927410125732 * (double) ((Entity) this.Projectile).velocity.X / 25.0);
      if (firstNpc >= 0 && firstNpc < Main.maxNPCs)
      {
        NPC npc = Main.npc[firstNpc];
        if (npc.TypeAlive<FargowiltasSouls.Content.Bosses.BanishedBaron.BanishedBaron>() && this.Projectile.Colliding(((Entity) this.Projectile).Hitbox, ((Entity) npc).Hitbox))
        {
          SoundEngine.PlaySound(ref SoundID.Item2, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          this.Projectile.Kill();
        }
      }
      ++this.Projectile.ai[0];
    }
  }
}
