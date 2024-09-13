// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Sky.AbomSky
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Sky
{
  public class AbomSky : CustomSky
  {
    private bool isActive;
    private float intensity;

    public virtual void Update(GameTime gameTime)
    {
      if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.abomBoss, ModContent.NPCType<FargowiltasSouls.Content.Bosses.AbomBoss.AbomBoss>()))
      {
        this.intensity += 0.01f;
        if ((double) this.intensity <= 1.0)
          return;
        this.intensity = 1f;
      }
      else
      {
        this.intensity -= 0.01f;
        if ((double) this.intensity >= 0.0)
          return;
        this.intensity = 0.0f;
        ((GameEffect) this).Deactivate(Array.Empty<object>());
      }
    }

    public virtual void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
    {
      if ((double) maxDepth < 0.0 || (double) minDepth >= 0.0)
        return;
      spriteBatch.Draw(ModContent.Request<Texture2D>("FargowiltasSouls/Content/Sky/AbomSky", (AssetRequestMode) 1).Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.op_Multiply(Color.op_Multiply(Color.White, this.intensity), 0.75f));
    }

    public virtual float GetCloudAlpha() => 1f - this.intensity;

    public virtual void Activate(Vector2 position, params object[] args) => this.isActive = true;

    public virtual void Deactivate(params object[] args) => this.isActive = false;

    public virtual void Reset() => this.isActive = false;

    public virtual bool IsActive() => this.isActive;

    public virtual Color OnTileColor(Color inColor)
    {
      return new Color(Vector4.Lerp(new Vector4(1f, 0.9f, 0.6f, 1f), ((Color) ref inColor).ToVector4(), 1f - this.intensity));
    }
  }
}
