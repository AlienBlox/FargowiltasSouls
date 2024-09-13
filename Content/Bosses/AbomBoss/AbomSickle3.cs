// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.AbomBoss.AbomSickle3
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.AbomBoss
{
  public class AbomSickle3 : AbomSickle
  {
    public virtual string Texture => "FargowiltasSouls/Content/Bosses/AbomBoss/AbomSickle";

    public override void SetDefaults() => base.SetDefaults();

    public override void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = ((Entity) this.Projectile).Center.X;
        this.Projectile.localAI[1] = ((Entity) this.Projectile).Center.Y;
        SoundEngine.PlaySound(ref SoundID.Item8, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      this.Projectile.rotation += 0.8f;
      if ((double) this.Projectile.ai[1] == 0.0)
      {
        Player player = FargoSoulsUtil.PlayerExists(this.Projectile.ai[0]);
        if (player == null)
          return;
        Vector2 vector2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2).\u002Ector(this.Projectile.localAI[0], this.Projectile.localAI[1]);
        if ((double) ((Entity) this.Projectile).Distance(vector2) <= (double) ((Entity) player).Distance(vector2) - 160.0)
          return;
        this.Projectile.ai[1] = 1f;
        ((Vector2) ref ((Entity) this.Projectile).velocity).Normalize();
        this.Projectile.timeLeft = 300;
        this.Projectile.netUpdate = true;
      }
      else
      {
        if ((double) ++this.Projectile.ai[1] >= 60.0)
          return;
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.065f);
      }
    }
  }
}
