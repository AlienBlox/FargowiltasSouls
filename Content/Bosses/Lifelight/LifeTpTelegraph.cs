// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Lifelight.LifeTpTelegraph
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Lifelight
{
  public class LifeTpTelegraph : ModProjectile
  {
    public ref float Timer => ref this.Projectile.ai[0];

    public ref float ParentIndex => ref this.Projectile.ai[1];

    public virtual string Texture => "FargowiltasSouls/Assets/Effects/LifeStar";

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 150;
      ((Entity) this.Projectile).height = 150;
      this.Projectile.aiStyle = -1;
      this.Projectile.penetrate = 1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.alpha = 0;
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.Projectile.localAI[0]);
      base.SendExtraAI(writer);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.Projectile.localAI[0] = (float) reader.Read();
      base.ReceiveExtraAI(reader);
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = Math.Abs(this.Timer);
        SoundStyle soundStyle = SoundID.Item29;
        ((SoundStyle) ref soundStyle).Volume = 1.5f;
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        this.Projectile.netUpdate = true;
      }
      if ((double) this.Timer > 0.0)
        this.Projectile.Kill();
      float num = (float) Math.Sin(1.5707963705062866 * (1.0 - (double) Math.Abs(this.Timer) / (double) this.Projectile.localAI[0]));
      this.Projectile.scale = (float) (0.10000000149011612 + 1.3999999761581421 * (double) num);
      this.Projectile.scale *= Utils.NextFloat(Main.rand, 0.8f, 1.2f);
      this.Projectile.rotation = 12.566371f * num;
      NPC npc = FargoSoulsUtil.NPCExists(this.ParentIndex);
      if (npc != null)
        ((Entity) this.Projectile).Center = new Vector2(npc.localAI[0], npc.localAI[1]);
      ++this.Timer;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual Color? GetAlpha(Color lightColor)
    {
      Color hotPink = Color.HotPink;
      ((Color) ref hotPink).A = (byte) 50;
      return new Color?(hotPink);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Type].Value;
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Vector2 vector2 = Vector2.op_Division(Utils.Size(texture2D), 2f);
      for (int index = 0; index < 3; ++index)
        Main.EntitySpriteDraw(texture2D, Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Rectangle?(), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
