// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Sky.MoonLordSky
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.VanillaEternity;
using FargowiltasSouls.Core.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Effects;

#nullable disable
namespace FargowiltasSouls.Content.Sky
{
  public class MoonLordSky : CustomSky
  {
    private bool isActive;

    public virtual void Update(GameTime gameTime)
    {
      int vulState = -1;
      int num = 0;
      bool flag = false;
      if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.moonBoss, 398))
      {
        vulState = Main.npc[EModeGlobalNPC.moonBoss].GetGlobalNPC<MoonLordCore>().VulnerabilityState;
        num = (int) Main.npc[EModeGlobalNPC.moonBoss].GetGlobalNPC<MoonLordCore>().VulnerabilityTimer;
        flag = true;
      }
      if (Main.dedServ || num % 30 != 0 || !(HandleScene("Solar", 0) & HandleScene("Vortex", 1) & HandleScene("Nebula", 2) & HandleScene("Stardust", 3) & !flag))
        return;
      ((GameEffect) this).Deactivate(Array.Empty<object>());

      bool HandleScene(string name, int neededState)
      {
        if (!((EffectManager<Filter>) Filters.Scene)["FargowiltasSouls:" + name].IsActive())
          return true;
        if (vulState != neededState)
          ((EffectManager<Filter>) Filters.Scene).Deactivate("FargowiltasSouls:" + name, Array.Empty<object>());
        return false;
      }
    }

    public virtual void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
    {
    }

    public virtual float GetCloudAlpha() => base.GetCloudAlpha();

    public virtual void Activate(Vector2 position, params object[] args) => this.isActive = true;

    public virtual void Deactivate(params object[] args) => this.isActive = false;

    public virtual void Reset() => this.isActive = false;

    public virtual bool IsActive() => this.isActive;

    public virtual Color OnTileColor(Color inColor) => base.OnTileColor(inColor);
  }
}
